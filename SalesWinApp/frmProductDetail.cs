using BusinessObject;
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
    public partial class frmProductDetail : Form
    {
        public frmProductDetail()
        {
            InitializeComponent();
        }
        //---------------------------------------
        public IProductRepository ProductRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public ProductObject ProductInfo { get; set; }
        //---------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var product = new ProductObject
                {
                    CategoryID= Int16.Parse(txtCategoryID.Text),
                    ProductName= txtProductName.Text,
                    Weight= txtWeight.Text,
                    UnitPrice= decimal.Parse(txtUnitPrice.Text),
                    UnitInStock= Int16.Parse(txtUnitsInStock.Text)
                };
                if (InsertOrUpdate == false)
                {
                    ProductRepository.InsertProduct(product);
                    MessageBox.Show("Save success");
                    txtCategoryID.Clear();
                    txtProductName.Clear();
                    txtWeight.Clear();
                    txtUnitPrice.Clear();
                    txtUnitsInStock.Clear();
                }
                else
                {
                    ProductRepository.UpdateProduct(product);
                    MessageBox.Show("Save success");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = InsertOrUpdate == false ? "Add New Product" : "Update New Product";
                string exceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                string fullErrorMessage = $"An error occurred while saving the changes: {exceptionMessage}";

                MessageBox.Show(fullErrorMessage, errorMessage);
            }
        }

        private void frmProductDetail_Load(object sender, EventArgs e)
        {
            txtCategoryID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)
            {

                txtCategoryID.Text = ProductInfo.CategoryID.ToString();
                txtProductName.Text = ProductInfo.ProductName.ToString();
                txtUnitPrice.Text = ProductInfo.UnitPrice.ToString();
                txtWeight.Text = ProductInfo.Weight.ToString();
                txtUnitsInStock.Text = ProductInfo.UnitInStock.ToString();

            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
      
    }
}
