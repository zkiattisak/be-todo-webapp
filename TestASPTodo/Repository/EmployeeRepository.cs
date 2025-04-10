using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TestASPTodo.Data;
using TestASPTodo.Models;
using TestASPTodo.Models.Entities;
using Dapper; // Make sure to install the Dapper NuGet package

namespace TestASPTodo.Repository
{
    public class EmployeeRepository : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                using (var db = dbContext()) 
                {
                    var response = await db.QueryMultipleAsync(
                        sql: "_p_M_Role_Get",
                        commandType: CommandType.StoredProcedure
                    );
                    var result = (await response.ReadAsync<RoleListOut>()).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception for better debugging
                throw new Exception($"Error retrieving employees: {ex.Message}");
            }
        }
    }
}