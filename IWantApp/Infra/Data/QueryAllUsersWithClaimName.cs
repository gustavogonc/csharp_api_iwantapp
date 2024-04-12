using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        private readonly IConfiguration _configuration;
        public QueryAllUsersWithClaimName(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
        {
            var db = new SqlConnection(_configuration["Database:SqlServer"]);
            string query = @"select Email, ClaimValue as Name 
                         from IWantDb.dbo.AspNetUsers u 
                         inner join IWantDb.dbo.AspNetUserClaims c
                         on u.id = c.UserId and claimType = 'Name'
                         order by name
                         OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
        }
    }
}
