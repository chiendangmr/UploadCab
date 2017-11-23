using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using HDControl;
using System.Security.Cryptography;
using Dapper;
using System.Linq;

namespace HDExportMetadataAndFile
{
    public partial class RegisterForm : Form
    {
        private string connectionStr = "";
        private string userConfigFileName = "";
        public string userConfigPath
        {
            get { return userConfigFileName; }
        }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            #region Đăng nhập
            using (var db = new SqlConnection(connectionStr))
            {
                try
                {
                    var userDB = db.Query<DAO.SYSTEM_USERS>(@"Select * from SYSTEM_USERS where USER_NAME=@username and PASSWORD=@password", new { username = txtUsername.Text, password = MD5Hash(MD5Hash(txtPwd.Text)) }).FirstOrDefault();
                    if (userDB != null)
                    {
                        userConfigFileName = "user" + userDB.USER_ID.ToString() + "Config.xml";
                        HDMessageBox.Show("Lưu cấu hình cho người dùng " + userDB.USER_NAME + " thành công!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        HDMessageBox.Show("Sai thông tin đăng nhập, mời thử lại!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Error);                       
                    }
                }
                catch (Exception ex)
                {
                    HDMessageBox.Show(ex.ToString());
                }
            }            
            #endregion            
        }
        public string MD5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            connectionStr = AppSettings.Default.ConnectionStr;
        }

        private void ckShowPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShowPwd.Checked)
            {
                txtPwd.Properties.UseSystemPasswordChar = false;
            }
            else
            {
                txtPwd.Properties.UseSystemPasswordChar = true;
            }
        }
    }
}
