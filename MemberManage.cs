using BusinessObject;
using DataAccess.Repository;

namespace SalesWinApp
{
    public partial class frmMemberManager : Form
    {
        public frmMemberManager()
        {
            InitializeComponent();
        }
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }

        private void MemberManager_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            dgwDataMember.CellDoubleClick += DgvMemberList_CellDoubleClick;
        }
        //-----------------------------------------------------
        private void DgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetails frmMember = new frmMemberDetails
            {
                Text = "Update Member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository,
            };
            if(frmMember.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }
        }
        //---------------------------------------------------------
        private void ClearText()
        {
            txtMemberID.Text= string.Empty;
            txtEmail.Text= string.Empty;
            txtCompanyName.Text= string.Empty;
            txtCity.Text= string.Empty;
            txtCountry.Text= string.Empty;
            txtPassword.Text= string.Empty;
        }
        //----------------------------------------------------------
        private MemberObject GetMemberObject()
        {
            MemberObject obj = null;
            try
            {
                obj = new MemberObject
                {
                    MemberID= int.Parse(txtMemberID.Text),
                    Email= txtEmail.Text,
                    CompanyName= txtCompanyName.Text,
                    City= txtCity.Text,
                    Country= txtCountry.Text,
                    Password= txtPassword.Text,

                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Get Member");
            }
            return obj;
        }
        //---------------------------------------------------------------
        public void LoadMemberList()
        {
            var mbs = memberRepository.GetMemberObjects();
            try
            {
                source = new BindingSource();
                source.DataSource = mbs;
                txtMemberID.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtCompanyName.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();
                txtPassword.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtCompanyName.DataBindings.Add("Text", source, "CompanyName");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");
                txtPassword.DataBindings.Add("Text", source, "Password");

                dgwDataMember.DataSource = null;
                dgwDataMember.DataSource = source;
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
                MessageBox.Show(ex.Message,"Load Car List");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)=>Close();

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Add Member",
                InsertOrUpdate= false,
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.MemberID);
                LoadMemberList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Member");
            }
        }


        //---------------------------------------------------------------------------

    }
}