using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmOderdetails : Form
    {
        public frmOderdetails()
        {
            InitializeComponent();
        }
        IOrderDetailRepository orderDetailsRepository = new OrderDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        BindingSource source;
        //---------------------------------------------------------------
        private void LoadOrderDetails()
        {
            if (!string.IsNullOrEmpty(txtOrderID.Text))
            {
                if (int.TryParse(txtOrderID.Text, out int orderId))
                {
                    var mbs = orderDetailsRepository.GetOrderDetailByID(orderId);

                    if (mbs != null && mbs.Any())
                    {
                        
                        source = new BindingSource();
                        source.DataSource = mbs;
                        dgvProduct.DataSource = null;
                        dgvProduct.DataSource = source;

                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No order details found for the specified order ID.");
                        btnDelete.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Order ID. Please enter a valid numeric value.");
                    btnDelete.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please enter an Order ID.");
                btnDelete.Enabled = false;
            }
        }
        //---------------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOrderID.Text)) { LoadOrderDetails(); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
