using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //TODO: make this SOLID/DRY/Better
            //Start filling in sale detail models that we will save to the db
            List<SaleDetailDbModel> details = new List<SaleDetailDbModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate();

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail =new SaleDetailDbModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                //Get the info about this product
                var productInfo = products.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the database.");
                }

                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;
                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * taxRate;
                }

                details.Add(detail);
                
            }
            //Create the sale model
            SaleDbModel sale = new SaleDbModel
            {
                SubTotal = details.Sum(x=>x.PurchasePrice),
                Tax = details.Sum(x=>x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;
            
            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("RMData");

                    //Save the sale model
                    sql.SaveDataInTransaction("dbo.spSale_Insert", sale);

                    //Get the ID from the sale model
                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                    //Finish filling in the sale detail models
                    foreach (var item in details)
                    {
                        item.SaleId = sale.Id;
                        //Save the sale details models
                        sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                    }

                    sql.CommitTransaction();
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }

            }

        }

        public List<SaleReportModel> GetSaleReport()
        {
            var sql = new SqlDataAccess();

            var output = sql.LoadData<SaleReportModel, dynamic>("dbo.spSale_SaleReport", new {}, "RMData");

            return output;
        }
    }
}
