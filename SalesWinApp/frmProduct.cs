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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }
        //---------------------------------------------
        IProductRepository productRepository = new ProductRepository();
        BindingSource source;
        //---------------------------------------------
        private void frmProduct_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            dgwDataProduct.CellDoubleClick += DgvProductList_CellDoubleClick;
        }
        //-----------------------------------------------------
        private void DgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmProductDetail frmMember = new frmProductDetail
            {
                Text = "Update Product",
                InsertOrUpdate = true,
                ProductInfo = GetProductObject(),
                ProductRepository = productRepository,
            };
            if (frmMember.ShowDialog() == DialogResult.OK)
            {
                loadProductList();
                source.Position = source.Count - 1;
            }
        }
        //-------------------------------------------------------
        private void ClearText()
        {
            txtProductID.Text = string.Empty;
            txtCategoryID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtUnitInStock.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
        }
        //-------------------------------------------------------
        private ProductObject GetProductObject()
        {   ProductObject productObject = null;
            try
            {
                productObject = new ProductObject
                {
                    ProductID = int.Parse(txtProductID.Text),
                    CategoryID = int.Parse(txtCategoryID.Text),
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitInStock = int.Parse(txtUnitInStock.Text)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return productObject;
            }

        //---------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadProductList();
        }

        private void loadProductList()
        {
            var mbs = productRepository.GetProductList();
            try
            {
                source = new BindingSource();
                source.DataSource = mbs;
                txtProductID.DataBindings.Clear();
                txtCategoryID.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtWeight.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();
                txtUnitInStock.DataBindings.Clear();

                txtProductID.DataBindings.Add("Text", source, "ProductID");
                txtCategoryID.DataBindings.Add("Text", source, "CategoryID");
                txtProductName.DataBindings.Add("Text", source, "ProductName");
                txtWeight.DataBindings.Add("Text", source, "Weight");
                txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                txtUnitInStock.DataBindings.Add("Text", source, "UnitInStock");

                dgwDataProduct.DataSource = null;
                dgwDataProduct.DataSource = source;
                if (mbs.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmProductDetail frmProductDetail = new frmProductDetail
            {
                Text = "Add new Product",
                InsertOrUpdate= false,
                ProductRepository = productRepository
            };
            if (frmProductDetail.ShowDialog() == DialogResult.OK)
            {
                loadProductList();
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var product = GetProductObject();
                productRepository.DeleteProduct(product.ProductID);
                loadProductList();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Product");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
