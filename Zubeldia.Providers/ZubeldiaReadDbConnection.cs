namespace Zubeldia.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Data.SqlClient;
    using Zubeldia.Domain.Interfaces.Providers;

    [ExcludeFromCodeCoverage]
    public class ZubeldiaReadDbConnection : IZubeldiaReadDbConnection, IDisposable
    {
        private readonly IDbConnection connection;

        public ZubeldiaReadDbConnection()
        {
            this.connection = new SqlConnection(Environment.GetEnvironmentVariable("ZUBELDIA_DB_CONNECTION"));
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return (await this.connection.QueryAsync<T>(sql, param, transaction)).AsList();
        }

        public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, TResult>(string sql, Func<T1, T2, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
        {
            return await this.connection.QueryAsync(sql, map, param, transaction, true, splitOn);
        }

        public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, T3, TResult>(string sql, Func<T1, T2, T3, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
        {
            return await this.connection.QueryAsync(sql, map, param, transaction, true, splitOn);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return await this.connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return await this.connection.QuerySingleAsync<T>(sql, param, transaction);
        }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}