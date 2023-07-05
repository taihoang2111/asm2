using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository: IOrderDetailRepository
    {
        public OrderDetailObject GetOrderDetailByID(int id) => OrderDetailDAO.Instance.GetOrderDetailByID(id);
        public IEnumerable<OrderDetailObject> GetOrderDetailList() => OrderDetailDAO.Instance.GetOrderObjectsList();
        public void InsertOrderDetail(OrderDetailObject order)=> OrderDetailDAO.Instance.AddNew(order);
        public void UpdateOrderDetail(OrderDetailObject order)=> OrderDetailDAO.Instance.Update(order);
        public void DeleteOrderDetail(int ID) => OrderDetailDAO.Instance.Delete(ID);
    }
}
