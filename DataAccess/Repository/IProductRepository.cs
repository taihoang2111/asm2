using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetProductList();
        ProductObject GetProductByID(int id);
        void InsertProduct(ProductObject Product);
        void UpdateProduct(ProductObject Product);
        void DeleteProduct(int id);

    }
}
