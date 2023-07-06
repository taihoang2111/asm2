using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailObject> GetOrderDetailList();
        IEnumerable<OrderDetailObject> GetOrderDetailByID(int id);
        void InsertOrderDetail(OrderDetailObject order);
        void UpdateOrderDetail(OrderDetailObject order);
        void DeleteOrderDetail(int id);

    }
}
