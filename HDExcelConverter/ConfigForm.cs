using HDControl;
using HDCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDExcelConverter
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }
        string configPath = "";
        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtImport.Text.Trim().Length > 2 && txtExport.Text.Trim().Length > 3)
                {
                    AppSettings.Default.ImportFolder = txtImport.Text;
                    AppSettings.Default.ExportFolder = txtExport.Text;
                    bsConfig.Clear();
                    bsConfig.List.Add(new View.Config
                    {
                        ImportFolder = txtImport.Text,
                        ExportFolder = txtExport.Text
                    });

                    (bsConfig.List as BindingList<View.Config>).ToList().SaveObject(configPath);
                    HDMessageBox.Show("Cấu hình thành công!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    HDMessageBox.Show("Chưa cấu hình phần mềm", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                HDMessageBox.Show("Cấu hình thất bại! Mời cấu hình lại hoặc khởi động lại phần mềm!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            configPath = Path.Combine(Application.StartupPath, "Config.xml");
            txtImport.Text = AppSettings.Default.ImportFolder;
            txtExport.Text = AppSettings.Default.ExportFolder;
        }

        private void btnChooseImport_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (txtImport.Text != null && txtImport.Text != "")
                {
                    if (Directory.Exists(Path.GetFullPath(txtImport.Text)))
                    {
                        fbd.SelectedPath = Path.GetFullPath(txtImport.Text);
                    }
                }
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtImport.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnChooseExport_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (txtExport.Text != null && txtExport.Text != "")
                {
                    if (Directory.Exists(Path.GetFullPath(txtExport.Text)))
                    {
                        fbd.SelectedPath = Path.GetFullPath(txtExport.Text);
                    }
                }
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtExport.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
