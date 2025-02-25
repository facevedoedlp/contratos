﻿namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class PlayerPosition : Entity<int>
    {
        public int PlayerId { get; set; }
        public int PositionId { get; set; }
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}