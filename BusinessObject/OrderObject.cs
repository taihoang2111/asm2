using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderObject
    {
        [Key]public int OrderID { get; set; }
        public int MemberID { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate     { get; set; }
        public decimal Freight { get; set; }
    }
}
