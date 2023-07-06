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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        private void btnLogin_Click(object sender, EventArgs e)
        {  MemberObject member = 
           memberRepository.GetMemberByLogin(txtEmail.Text, txtPassword.Text);
            if (member != null)
            {
                frmMemberManager frmMemberManager = new frmMemberManager();
                frmMemberManager.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Login");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
          
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Register",
                InsertOrUpdate = false,
                MemberRepository= memberRepository
            };
            if(frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Register Success");
                source = new BindingSource();
                source.DataSource = memberRepository.GetMemberObjects();
            }
        }
    }
}
