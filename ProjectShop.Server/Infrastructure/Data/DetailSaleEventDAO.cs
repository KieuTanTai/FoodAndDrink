using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailSaleEventDAO : BaseNoneUpdateDAO<DetailSaleEventModel>, IGetAllByIdAsync<DetailSaleEventModel>, IGetByRangePriceAsync<DetailSaleEventModel>
    {
        public DetailSaleEventDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "detail_sale_event", "detail_sale_event_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (sale_event_id, product_barcode, discount_type, discount_percent, discount_amount, max_discount_price, min_price_to_use) 
                            VALUES (@SaleEventId, @ProductBarcode, @DiscountType, @DiscountPercent, @DiscountAmount, @MaxDiscountPrice, @MinPriceToUse;";
        }

        private string GetAllByIdQuery(string colIdName)
        {
            CheckColumnName(colIdName);
            return $@"SELECT * FROM {TableName} WHERE {colIdName} = @Input";
        }

        public async Task<List<DetailSaleEventModel>> GetAllByIdAsync(string id, string colIdName)
        {
            try
            {
                string query = GetAllByIdQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailSaleEventModel> result = await connection.QueryAsync<DetailSaleEventModel>(query, new { Input = id });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailSaleEventModels by ID: {ex.Message}", ex);
            }
        }

        //public async Task<List<DetailSaleEventModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice)
        //{
        //    try
        //    {
        //        string query = $@"SELECT * FROM {TableName} 
        //                          WHERE min_price_to_use >= @MinPrice AND max_discount_price <= @MaxPrice";
        //        using IDbConnection connection = ConnectionFactory.CreateConnection();
        //        IEnumerable<DetailSaleEventModel> result = await connection.QueryAsync<DetailSaleEventModel>(query, new { MinPrice = minPrice, MaxPrice = maxPrice });
        //        return result.AsList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error retrieving DetailSaleEventModels by price range: {ex.Message}", ex);
        //    }
        //}

        //overloading
        public async Task<List<DetailSaleEventModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, string colPrice = "min_price_to_use")
        {
            try
            {
                CheckColumnName(colPrice);
                string query = $@"SELECT * FROM {TableName} WHERE {colPrice} BETWEEN @MinPrice AND @MaxPrice";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailSaleEventModel> result = await connection.QueryAsync<DetailSaleEventModel>(query, new { MinPrice = minPrice, MaxPrice = maxPrice });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailSaleEventModels by price range: {ex.Message}", ex);
            }
        }

    }
}
