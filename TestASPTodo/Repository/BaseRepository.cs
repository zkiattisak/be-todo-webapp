using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging; // Add this
using System;
using Microsoft.Data.SqlClient;

namespace Authentication.Repositories.Repository
{
    public class BaseRepository
    {
        protected IDbConnection? db;
        protected readonly IConfiguration config;
        protected IDbConnection? dbConnectionMock = null;
        private readonly ILogger<BaseRepository> _logger; // Add this

        public BaseRepository(IConfiguration config, ILogger<BaseRepository> logger) // Modify this
        {
            this.config = config;
            _logger = logger; // Store the logger

            _logger.LogInformation("BaseRepository constructor called."); // Add logging

            string connectionStringName = "DefaultConnection"; // Use the name from appsettings.json
            _connectionString = config.GetConnectionString(connectionStringName);

            _logger.LogInformation($"Connection string name: {connectionStringName}");
            _logger.LogInformation($"Connection string value: {_connectionString}"); // Log the connection string

            if (string.IsNullOrEmpty(_connectionString))
            {
                _logger.LogError("Connection string is null or empty!"); // Log an error
                throw new InvalidOperationException($"Connection string '{connectionStringName}' is not configured.");
            }
        }

        protected string _connectionString { get; set; } //changed to protected

        public void DBConnectionMock(IDbConnection _dbConnectionMock)
        {
            dbConnectionMock = _dbConnectionMock;
        }

        public IDbConnection CreateConnection()
        {
            if (dbConnectionMock != null)
            {
                return dbConnectionMock;
            }
            else
            {
                db = new SqlConnection(_connectionString);
                return db;
            }
        }

        public IDbConnection GetOrCreateConnection()
        {
            if (dbConnectionMock != null)
            {
                return dbConnectionMock;
            }
            else if (db != null && db.State == System.Data.ConnectionState.Open)
            {
                return db;
            }
            else
            {
                db = CreateConnection();
            }

            return db;
        }
    }
}