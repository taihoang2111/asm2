using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<OrderObject> GetOrderList();
        OrderObject GetOrderByID(int id);
        void InsertOrder(OrderObject order);
        void UpdateOrder(OrderObject order);
        void DeleteOrder(int id);

    }
}
