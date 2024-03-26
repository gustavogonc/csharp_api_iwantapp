using Dapper;
using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeeGetAll
    {
        public static string Template => "/employees";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(int page, int rows, IConfiguration configuration)
        {

            var db = new SqlConnection(configuration["Database:SqlServer"]);
            string query = @"select Email, ClaimValue as Name 
                         from IWantDb.dbo.AspNetUsers u 
                         inner join IWantDb.dbo.AspNetUserClaims c
                         on u.id = c.UserId and claimType = 'Name'
                         order by name
                         OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            var employees = db.Query<EmployeeResponse>(query, new {page, rows});

            return Results.Ok(employees);

        }
    }
}
