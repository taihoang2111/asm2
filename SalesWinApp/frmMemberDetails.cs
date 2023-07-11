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
    public partial class frmMemberDetails : Form
    {
        public frmMemberDetails()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public MemberObject MemberInfo{ get; set; }



        //--------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (InsertOrUpdate == false)
                {
                    var member = new MemberObject
                    {

                        Email = txtEmail.Text,
                        CompanyName = txtCompanyName.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text,
                        Password = txtPassword.Text,
                    };
                    MemberRepository.InsertMember(member);
                    MessageBox.Show("Save success");
                    txtEmail.Clear();
                    txtCompanyName.Clear();
                    txtCountry.Clear();
                    txtCity.Clear();
                    txtPassword.Clear();
                }
                else
                {
                    var member = new MemberObject
                    {
                        MemberID= int.Parse(txtMemberID.Text),
                        Email = txtEmail.Text,
                        CompanyName = txtCompanyName.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text,
                        Password = txtPassword.Text,
                    };
                    MemberRepository.UpdateMember(member);
                    MessageBox.Show("Save success");
                    txtMemberID.Clear();
                    txtEmail.Clear();
                    txtCompanyName.Clear();
                    txtCountry.Clear();
                    txtCity.Clear();
                    txtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = InsertOrUpdate == false ? "Add New Member" : "Update New Member";
                string exceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                string fullErrorMessage = $"An error occurred while saving the changes: {exceptionMessage}";

                MessageBox.Show(fullErrorMessage, errorMessage);
            }
        }
        //---------------------------------------------------------
        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            txtEmail.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)
            {
                txtMemberID.Text= MemberInfo.MemberID.ToString();
                txtEmail.Text= MemberInfo.Email.ToString();
                txtCompanyName.Text= MemberInfo.CompanyName.ToString();
                txtCity.Text= MemberInfo.City.ToString();
                txtCountry.Text= MemberInfo.Country.ToString();
                txtPassword.Text= MemberInfo.Password.ToString();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------
    }
}
