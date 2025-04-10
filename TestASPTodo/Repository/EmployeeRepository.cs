using Authentication.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TestASPTodo.Data;
using TestASPTodo.Models;
using TestASPTodo.Models.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace TestASPTodo.Repository
{
    public class EmployeeRepository : BaseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<EmployeeRepository> _logger;  // Add logger

        public EmployeeRepository(ApplicationDbContext dbContext, IConfiguration config, ILogger<EmployeeRepository> logger) : base(config, logger) // Pass config and logger
        {
            _dbContext = dbContext;
            _logger = logger; // Store the logger
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                using (var db = CreateConnection())
                {
                    var response = await db.QueryMultipleAsync(
                                            sql: "GetEmployee",
                                            commandType: CommandType.StoredProcedure
                                         );
                    var result = (await response.ReadAsync<Employee>()).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
             
                throw new Exception($"Error retrieving employees: {ex.Message}");
            }
        }
    }
}