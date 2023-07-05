using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public class OrderDAO:DbContext
    {
        public static OrderDAO instance = null;
        public static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                }
                return instance;
            }
        }


        public DbSet<OrderObject> Order { get; set; }
        //--------------------------------------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MySaleDB"));
        }
        //--------------------------------------------------------
        public void AddNew(OrderObject Order)
        {
            try
            {
                using (var context = new OrderDAO())
                {
                    context.Order.Add(Order);
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("An error occurred while saving the changes:");
                Console.WriteLine(ex.ToString());

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception:");
                    Console.WriteLine(ex.InnerException.ToString());
                }

                throw;
            }
        }
        //--------------------------------------------------------
        public IEnumerable<OrderObject> GetOrderObjectsList()
        {
            using (var context = new OrderDAO())
            {
                var mbl = context.Order.ToList();
                return mbl;
            }
        }
        //--------------------------------------------------------
        public OrderObject? GetOrderObjectByID(int OrderID)
        {
            using (var context = new OrderDAO())
            {
                var mb = context.Order.FirstOrDefault(c => c.OrderID == OrderID);
                if (mb != null)
                {
                    return mb;
                }
                else return null;
            }
        }
        //--------------------------------------------------------
        public void Update(OrderObject Order)
        {
            using (var context = new OrderDAO())
            {
                var mb = context.Order.FirstOrDefault(c => c.OrderID == Order.OrderID);
                if (mb != null)
                {
                    mb = Order;
                    context.SaveChanges();
                }
            }
        }
        //--------------------------------------------------------
        public void Delete(int OrderID)
        {
            using (var context = new OrderDAO())
            {
                var or = context.Order.FirstOrDefault(c => c.OrderID == OrderID);
                if (or != null)
    {
                    context.Order.Remove(or);
                    context.SaveChanges();
                }
            }
        }
    }
}
