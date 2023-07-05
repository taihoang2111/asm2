using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository:IOrderRepository
    {
        public OrderObject GetOrderByID(int OrderID) => OrderDAO.Instance.GetOrderObjectByID(OrderID);
        public IEnumerable<OrderObject> GetOrderList() => OrderDAO.Instance.GetOrderObjectsList();
        public void InsertOrder(OrderObject Order) => OrderDAO.Instance.AddNew(Order);
        public void DeleteOrder(int OrderID) => OrderDAO.Instance.Delete(OrderID);
        public void UpdateOrder(OrderObject order) => OrderDAO.Instance.Update(order);
    }
}
