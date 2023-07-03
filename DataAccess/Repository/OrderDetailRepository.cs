using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository:IOrderRepository
    {
        public OrderObject GetOrderByID(int id) => OrderDAO.Instance.GetOrderObjectByID(id);
        public IEnumerable<OrderObject> GetOrderList() => OrderDAO.Instance.GetOrderObjectsList();
        public void InsertOrder(OrderObject order)=> OrderDAO.Instance.AddNew(order);
        public void UpdateOrder(OrderObject order)=> OrderDAO.Instance.Update(order);
        public void DeleteOrder(int ID) => OrderDAO.Instance.Delete(ID);
    }
}
