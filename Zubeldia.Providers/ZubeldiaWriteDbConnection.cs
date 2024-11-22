namespace Grogu.Providers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Zubeldia.Domain.Interfaces.Providers;

    [ExcludeFromCodeCoverage]
    public class ZubeldiaWriteDbConnection : IZubeldiaWriteDbConnection
    {
        private readonly IZubeldiaDbContext context;

        public ZubeldiaWriteDbConnection(IZubeldiaDbContext context)
        {
            this.context = context;
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return await this.context.Connection.ExecuteAsync(sql, param, transaction);
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return (await this.context.Connection.QueryAsync<T>(sql, param, transaction)).AsList();
        }

        public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, TResult>(string sql, Func<T1, T2, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
        {
            return await this.context.Connection.QueryAsync(sql, map, param, transaction, true, splitOn);
        }

        public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, T3, TResult>(string sql, Func<T1, T2, T3, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
        {
            return await this.context.Connection.QueryAsync(sql, map, param, transaction, true, splitOn);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return await this.context.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return await this.context.Connection.QuerySingleAsync<T>(sql, param, transaction);
        }
    }
}