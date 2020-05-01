using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
       public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            return sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll",new { },"RMData");
        }

        public ProductModel GetProductById(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            return sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = id }, "RMData").FirstOrDefault();
        }
    }
}
