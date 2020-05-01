using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            // TODO: Make this better    
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData product = new ProductData();
            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };


                var productInfo = product.GetProductById(detail.ProductId);
                if (productInfo == null) throw new Exception($"ProductID {detail.ProductId} can't be found");
                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;
                if(productInfo.IsTaxable)
                         detail.Tax = detail.PurchasePrice * ConfigHelper.GetTaxRate();
                details.Add(detail);
            }

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spSale_Insert", sale, "RMData");

            sale.Id = sql.LoadData<int, dynamic>("dbo.spSale_Lookup", new {  sale.CashierId,  sale.SaleDate }, "RMData").FirstOrDefault();

            foreach(var item in details)
            {
                item.SaleId = sale.Id;
                sql.SaveData("dbo.spSaleDetail_Insert", item, "RMData");
            }

        }

    }
}
