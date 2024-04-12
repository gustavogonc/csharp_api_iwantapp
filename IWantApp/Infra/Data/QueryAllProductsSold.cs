﻿using Dapper;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data
{
    public class QueryAllProductsSold
    {
        private readonly IConfiguration configuration;
        public QueryAllProductsSold(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<ProductSoldResponse>> Execute()
        {
            var db = new SqlConnection(configuration["Database:SqlServer"]);
            string query = @"select    p.Id,
                                       p.Name,
	                                count(*) Amount
                                from 
	                                Orders o inner join OrderProducts op on o.Id = op.OrdersId
	                                inner join Products p on p.Id = op.ProductsId
                                group BY
	                                p.Id, p.Name
                                order by qtd desc;";
            return await db.QueryAsync<ProductSoldResponse>(query);
        }
    }
}
