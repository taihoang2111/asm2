using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO : DbContext
    {
        public static OrderDetailDAO instance = null;
        public static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetailObject>()
                .HasKey(o => new { o.OrderID, o.ProductID });
        }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                }
                return instance;
            }
        }


        public DbSet<OrderDetailObject> OrderDetail { get; set; }
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
        public void AddNew(OrderDetailObject Order)
        {
            try
            {
                this.OrderDetail.Add(Order);
                this.SaveChanges();
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

        public IEnumerable<OrderDetailObject> GetOrderObjectsList()
        {
            var mbl = this.OrderDetail.ToList();
            return mbl;
        }

        public IEnumerable<OrderDetailObject> GetOrderDetailByID(int OrderID)
        {
            var mb = this.OrderDetail.Where(c => c.OrderID == OrderID).ToList();
            if (mb != null)
            {
                
                return mb;
            }
            else return null;
        }
        //--------------------------------------------------------
        public void Update(OrderDetailObject Order)
        {
            using (var context = new OrderDetailDAO())
            {
                var mb = context.OrderDetail.FirstOrDefault(c => c.OrderID == Order.OrderID);
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
            using (var context = new OrderDetailDAO())
            {
                var or = context.OrderDetail.FirstOrDefault(c => c.OrderID == OrderID);
                if (or != null)
                {
                    context.OrderDetail.Remove(or);
                    context.SaveChanges();
                }
            }
        }
    }
}
