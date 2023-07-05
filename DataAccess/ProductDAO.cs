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
    public class ProductDAO : DbContext
    {
        public static ProductDAO instance = null;
        public static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                }
                return instance;
            }
        }


        public DbSet<ProductObject> Product { get; set; }
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
        public void AddNew(ProductObject Product)
        {
            try
            {
                using (var context = new ProductDAO())
                {
                    context.Product.Add(Product);
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
        public IEnumerable<ProductObject> GetProductObjectsList()
        {
            using (var context = new ProductDAO())
            {
                var mbl = context.Product.ToList();
                return mbl;
            }
        }
        //--------------------------------------------------------
        public ProductObject? GetOrderObjectByID(int ProductID)
        {
            using (var context = new ProductDAO())
            {
                var mb = context.Product.FirstOrDefault(c => c.ProductID == ProductID);
                if (mb != null)
                {
                    return mb;
                }
                else return null;
            }
        }
        //--------------------------------------------------------
        public void Update(ProductObject Order)
        {
            using (var context = new ProductDAO())
            {
                var mb = context.Product.FirstOrDefault(c => c.ProductID == Order.ProductID);
                if (mb != null)
                {
                    mb = Order;
                    context.SaveChanges();
                }
            }
        }
        //--------------------------------------------------------
        public void Delete(int ProductID)
        {
            using (var context = new ProductDAO())
            {
                var or = context.Product.FirstOrDefault(c => c.ProductID == ProductID);
                if (or != null)
                {
                    context.Product.Remove(or);
                    context.SaveChanges();
                }
            }
        }
    }
}
