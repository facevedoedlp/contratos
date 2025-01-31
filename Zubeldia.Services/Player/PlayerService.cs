namespace Zubeldia.Services.Player
{
    using AutoMapper;
    using FluentValidation;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Player;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    public class PlayerService(IPlayerDao playerDao, ICountryDao countryDao, IAgentDao agentDao, IDisciplineDao disciplineDao, IHealthcarePlanDao healthcarePlanDao, IMapper mapper, IFileStorageService fileStorageService, IValidator<CreatePlayerRequest> playerValidator)
              : IPlayerService
    {
        public async Task<List<KeyNameDto>> GetDropdownAsync(string? filter) => (await playerDao.GetDropdownAsync(filter)).ToList();

        public async Task<SearchResultPage<GetPlayersResponse>> GetByFiltersWithPaginationAsync(GetPlayersRequest request)
        {
            var searchResult = await playerDao.GetByFiltersWithPaginationAsync(request);
            return mapper.Map<SearchResultPage<GetPlayersResponse>>(searchResult);
        }

        public async Task<CreatePlayerRequest> GetByIdAsync(int id)
        {
            var player = await playerDao.GetByIdAsync(id);

            var response = mapper.Map<CreatePlayerRequest>(player);

            if (!string.IsNullOrEmpty(player?.Photo))
            {
                var photoBytes = await fileStorageService.GetFileAsBytesAsync(player.Photo, "Players");
                response.PhotoUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(photoBytes)}";
            }

            return response;
        }

        public async Task<GetPlayerFormDropdownResponse> GetFormDropdownsAsync()
        {
            return new GetPlayerFormDropdownResponse
            {
                Countries = mapper.Map<List<KeyNameDto>>(await countryDao.GetAllOrderedAsync()),
                Disciplines = mapper.Map<List<KeyNameDto>>(await disciplineDao.GetAllOrderedAsync()),
                HealthcarePlans = mapper.Map<List<KeyNameDto>>(await healthcarePlanDao.GetAllOrderedAsync()),
                Agents = mapper.Map<List<KeyNameDto>>(await agentDao.GetAllOrderedAsync()),
            };
        }

        public async Task<ValidatorResultDto> CreateOrEdit(CreatePlayerRequest request)
        {
            try
            {
                var validatorResult = await playerValidator.ValidateAsync(request);
                if (validatorResult.IsValid)
                {
                    if (request.Id == null || request.Id.Value == 0)
                    {
                        await SaveAsync(request);
                    }
                    else
                    {
                        await UpdateAsync(request);
                    }
                }

                var response = mapper.Map<ValidatorResultDto>(validatorResult);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateAsync(CreatePlayerRequest request)
        {
            var existingPlayer = await playerDao.GetByIdAsync(request.Id.Value);
            if (existingPlayer == null) throw new KeyNotFoundException($"Player with ID {request.Id.Value} not found");

            mapper.Map(request, existingPlayer);

            if (request.Identifications != null)
            {
                foreach (var identification in request.Identifications.Where(i => !string.IsNullOrEmpty(i.Number)))
                {
                    var existingIdentification = existingPlayer.Identifications.FirstOrDefault(x => x.Type == identification.Type);

                    if (existingIdentification == null)
                    {
                        existingIdentification = new PlayerIdentification
                        {
                            PlayerId = existingPlayer.Id,
                            Type = identification.Type
                        };
                        existingPlayer.Identifications.Add(existingIdentification);
                    }

                    mapper.Map(identification, existingIdentification);

                    if (identification.FrontPhoto?.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(existingIdentification.FrontPhoto))
                        {
                            await fileStorageService.DeleteFileAsync(existingIdentification.FrontPhoto, "PlayerIdentifications");
                        }
                        existingIdentification.FrontPhoto = await fileStorageService.SaveFileAsync(identification.FrontPhoto, "PlayerIdentifications");
                    }

                    if (identification.BackPhoto?.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(existingIdentification.BackPhoto))
                        {
                            await fileStorageService.DeleteFileAsync(existingIdentification.BackPhoto, "PlayerIdentifications");
                        }
                        existingIdentification.BackPhoto = await fileStorageService.SaveFileAsync(identification.BackPhoto, "PlayerIdentifications");
                    }
                }

                var identificationTypesToKeep = request.Identifications
                    .Where(i => !string.IsNullOrEmpty(i.Number))
                    .Select(i => i.Type);
                var identificationsToRemove = existingPlayer.Identifications
                    .Where(i => !identificationTypesToKeep.Contains(i.Type))
                    .ToList();
                foreach (var idToRemove in identificationsToRemove)
                {
                    existingPlayer.Identifications.Remove(idToRemove);
                }
            }

            existingPlayer.Positions.Clear();
            if (request.Positions != null)
            {
                foreach (var positionId in request.Positions)
                {
                    existingPlayer.Positions.Add(new PlayerPosition
                    {
                        PositionId = positionId,
                        PlayerId = existingPlayer.Id
                    });
                }
            }

            if (request.HealthcarePlan != null)
            {
                var healthcarePlan = existingPlayer.HealthcarePlan ?? new PlayerHealthcarePlan();
                mapper.Map(request.HealthcarePlan, healthcarePlan);
                healthcarePlan.PlayerId = existingPlayer.Id;

                if (request.HealthcarePlan.FrontPhoto?.Length > 0)
                {
                    if (!string.IsNullOrEmpty(healthcarePlan.FrontPhoto))
                    {
                        await fileStorageService.DeleteFileAsync(healthcarePlan.FrontPhoto, "HealthcarePlans");
                    }
                    healthcarePlan.FrontPhoto = await fileStorageService.SaveFileAsync(request.HealthcarePlan.FrontPhoto, "HealthcarePlans");
                }

                if (request.HealthcarePlan.BackPhoto?.Length > 0)
                {
                    if (!string.IsNullOrEmpty(healthcarePlan.BackPhoto))
                    {
                        await fileStorageService.DeleteFileAsync(healthcarePlan.BackPhoto, "HealthcarePlans");
                    }
                    healthcarePlan.BackPhoto = await fileStorageService.SaveFileAsync(request.HealthcarePlan.BackPhoto, "HealthcarePlans");
                }

                if (existingPlayer.HealthcarePlan == null)
                {
                    existingPlayer.HealthcarePlan = healthcarePlan;
                }
            }

            if (request.Photo?.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingPlayer.Photo))
                {
                    await fileStorageService.DeleteFileAsync(existingPlayer.Photo, "Players");
                }
                existingPlayer.Photo = await fileStorageService.SaveFileAsync(request.Photo, "Players");
            }

            await playerDao.UpdateAsync(existingPlayer);
        }

        private async Task SaveAsync(CreatePlayerRequest request)
        {
            string photoUri = string.Empty;
            if (request.Photo != null && request.Photo.Length > 0)
            {
                photoUri = await fileStorageService.SaveFileAsync(request.Photo, "Players");
            }

            Player player = mapper.Map<Player>(request);
            player.Photo = photoUri;

            if (request.Identifications != null)
            {
                foreach (var requestIdentification in request.Identifications.Where(i => !string.IsNullOrEmpty(i.Number)))
                {
                    var playerIdentification = player.Identifications.Where(x => x.Type == requestIdentification.Type).FirstOrDefault();

                    if (requestIdentification.FrontPhoto != null && requestIdentification.FrontPhoto.Length > 0)
                    {
                        playerIdentification.FrontPhoto = await fileStorageService.SaveFileAsync(
                            requestIdentification.FrontPhoto,
                            "PlayerIdentifications");
                    }

                    if (requestIdentification.BackPhoto != null && requestIdentification.BackPhoto.Length > 0)
                    {
                        playerIdentification.BackPhoto = await fileStorageService.SaveFileAsync(
                            requestIdentification.BackPhoto,
                            "PlayerIdentifications");
                    }
                }
            }

            if (request.HealthcarePlan != null)
            {
                PlayerHealthcarePlan healthcarePlan = mapper.Map<PlayerHealthcarePlan>(request.HealthcarePlan);

                if (request.HealthcarePlan.FrontPhoto != null && request.HealthcarePlan.FrontPhoto.Length > 0)
                {
                    healthcarePlan.FrontPhoto = await fileStorageService.SaveFileAsync(
                        request.HealthcarePlan.FrontPhoto,
                        "HealthcarePlans"
                    );
                }

                if (request.HealthcarePlan.BackPhoto != null && request.HealthcarePlan.BackPhoto.Length > 0)
                {
                    healthcarePlan.BackPhoto = await fileStorageService.SaveFileAsync(
                        request.HealthcarePlan.BackPhoto,
                        "HealthcarePlans"
                    );
                }

                player.HealthcarePlan = healthcarePlan;
            }

            await playerDao.AddAsync(player);
        }
    }
}
