using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductObject GetProductByID(int ProductID) => ProductDAO.Instance.GetOrderObjectByID(ProductID);
        public IEnumerable<ProductObject> GetProductList() => ProductDAO.Instance.GetProductObjectsList();
        public void InsertProduct(ProductObject Product) => ProductDAO.Instance.AddNew(Product);
        public void DeleteProduct(int ProductID) => ProductDAO.Instance.Delete(ProductID);
        public void UpdateProduct(ProductObject Product) => ProductDAO.Instance.Update(Product);
    }
}
