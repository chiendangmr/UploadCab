using Dapper;
using DevExpress.XtraGrid.Views.Grid;
using HDControl;
using HDCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ValidateInfo;

namespace HDValidator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        string fileListPath = "";
        //string filterPath = "";
        const string filterOption = "Video filter";
        private void MainForm_Load(object sender, EventArgs e)
        {
            fileListPath = Path.Combine(Application.StartupPath, "fileList.xml");
            //filterPath = Path.Combine(Application.StartupPath, "Config.xml");
            //if (File.Exists(filterPath))
            //    try
            //    {
            //        var lstConfig = Utils.GetObject<List<View.Config>>(filterPath);
            //        foreach (var anhTinh in lstConfig)
            //        {
            //            AppSettings.Default.FileFilter = anhTinh.fileFilter;
            //            AppSettings.Default.ConnectionString = anhTinh.connStr;
            //        }
            //    }
            //    catch { }
        }
        private void btnChooseFiles_Click(object sender, EventArgs e)
        {
            string fileFilters = AppSettings.Default.FileFilter;
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Filter = "Videos (" + fileFilters + ")|" + fileFilters + "|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Chọn file media...";
            if (fileFilters.Trim().Length <= 3)
            {
                HDMessageBox.Show("Chưa có chuỗi lọc Files, cấu hình phần mềm trước!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var lstFile = bsFiles.List as BindingList<View.FileView>;

                foreach (var i in openFileDialog1.FileNames)
                {
                    if (lstFile.Where(f => f.Name.ToLower() == i.ToLower()).FirstOrDefault() != null)
                    {
                        HDMessageBox.Show("File " + i + " đã có trong danh sách công việc!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        bsFiles.List.Add(new View.FileView
                        {
                            Name = i,
                            Status = "Đang chờ..."
                        });
                        txtFiles.Text += i + ";";
                    }
                }
                this.txtFiles.ToolTip = txtFiles.Text;
                (bsFiles.List as BindingList<View.FileView>).ToList().SaveObject(fileListPath);
            }
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var lstFiles = Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath).Where(f => AppSettings.Default.FileFilter.Contains(Path.GetExtension(f)));
                var lstFile = bsFiles.List as BindingList<View.FileView>;
                foreach (var i in lstFiles)
                {
                    if (lstFile.Where(f => f.Name.ToLower() == i.ToLower()).FirstOrDefault() != null)
                    {
                        HDMessageBox.Show("File " + i + " đã có trong danh sách công việc!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        bsFiles.List.Add(new View.FileView
                        {
                            Name = i,
                            Status = "Đang chờ..."
                        });
                    }
                }
                txtFolder.Text = folderBrowserDialog1.SelectedPath;
                (bsFiles.List as BindingList<View.FileView>).ToList().SaveObject(fileListPath);
            }
        }
        List<string> lstResult = new List<string>();
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                lstResult.Clear();
                using (var db = new SqlConnection(AppSettings.Default.ConnectionString))
                {
                    var lstConfig = db.Query<string>(@"select CONTENT from SYSTEM_USER_FILTER_PROFILE where NAME=@Name", new { Name = filterOption });
                    String miConfig = lstConfig.FirstOrDefault();
                    Dictionary<String, String> mediaInfo = new Dictionary<string, string>();
                    MIConfig MIConfig = JsonConvert.DeserializeObject<MIConfig>(miConfig);
                    bool isValid = false;
                    MIUtil mi = new MIUtil(MIConfig.Inform, MIConfig.Filter);

                    try
                    {
                        for (int i = 0; i < gvFiles.DataRowCount; i++)
                        {
                            var tempFile = gvFiles.GetRow(i) as View.FileView;
                            mi.Infor(Path.GetFullPath(tempFile.Name));
                            mediaInfo = mi.MediaInfo();
                            isValid = mi.Validate();
                            if (isValid)
                            {
                                tempFile.Status = "Hợp lệ";
                                String log = "";
                                log += "File: " + tempFile.Name + "; Can read: " + new FileInfo(tempFile.Name).Length;
                                //log += "\n" + MIConfig.Inform.ToMediaInfom();
                                log += "\n" + PrintMediaInfo(mediaInfo);
                                ResultLoggingLocal(tempFile.Name, log);
                                lstResult.Add(log);
                            }
                            else
                            {
                                List<InvalidInfo> invalids = mi.Invalids;
                                String log = "";
                                log += "File: " + tempFile.Name + "; Can read: " + new FileInfo(tempFile.Name).Length;
                                //log += "\n\n" + MIConfig.Inform.ToMediaInfom();
                                log += "\n\n" + PrintMediaInfo(mediaInfo);
                                log += "\n\n" + PrintInvalids(invalids);
                                ResultLoggingLocal(tempFile.Name, log);
                                tempFile.Status = "Không hợp lệ";
                                lstResult.Add(log);
                            }                            
                        }
                        gvFiles.RefreshData();
                        rtbReport.Text = lstResult[gvFiles.FocusedRowHandle];

                    }
                    catch (Exception ex)
                    {
                        HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }        

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.Show();
            frmAbout.Activate();
        }
        void ResultLoggingLocal(String FileName, String Log, bool TrySrcDir = true)
        {
            String ms = "";
            if (TrySrcDir)
            {
                String logFileName = FileName + ".log";
                try
                {
                    // Store the script names and test results in a output text file.
                    using (StreamWriter writer = new StreamWriter(new FileStream(logFileName, FileMode.OpenOrCreate)))
                    {
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        writer.WriteLine(Log);
                    }
                }
                catch (Exception ex)
                {
                    ms = ex.Message;
                    TrySrcDir = false;
                }
            }
            if (!TrySrcDir)
            {
                String logFileName = Application.StartupPath + "/Logs/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + Path.GetFileName(FileName) + ".log";
                String logDir = new FileInfo(logFileName).Directory.FullName;
                try
                {
                    if (!Directory.Exists(logDir))
                    {
                        Directory.CreateDirectory(logDir);
                    }
                    try
                    {
                        // Store the script names and test results in a output text file.
                        using (StreamWriter writer = new StreamWriter(new FileStream(logFileName, FileMode.OpenOrCreate)))
                        {
                            writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                            writer.WriteLine(Log);
                            if (ms != "")
                            {
                                writer.WriteLine(ms);
                            }
                        }
                    }
                    catch //(Exception ex)
                    {
                    }
                }
                catch //(Exception ex) 
                { }
            }
        }
        string PrintMediaInfo(Dictionary<string, string> mediaInfo)
        {
            String info = "\nMediaInfo\n";
            List<List<string>> rows = new List<List<string>>();

            foreach (KeyValuePair<String, String> temp in mediaInfo)
            {
                rows.Add(new List<String>() { temp.Key, temp.Value });
            }
            ConsoleTableUtil ctu = new ConsoleTableUtil(null, rows, null);
            ctu.ColsAlign = new ColAlign[] { ColAlign.left, ColAlign.left };
            ctu.Width = 6 * 12;
            info += ctu.PrintTable();
            return info;
        }
        string PrintInvalids(List<InvalidInfo> invalids)
        {
            String invalidMessage = "";
            Dictionary<MIRuleGroup, List<InvalidInfo>> invalidRules = new Dictionary<MIRuleGroup, List<InvalidInfo>>();
            if (invalids.Count > 0)
            {
                invalidMessage += "Invalid standards\n";
                List<String> headers = new List<String>() { "Param", "Standard", "File's info" };
                List<List<string>> rows = new List<List<string>>();
                for (var i = 0; i < invalids.Count; i++)
                {
                    InvalidInfo invalid = invalids[i];
                    if (invalid.ruleGroup != null)
                    {
                        if (!invalidRules.ContainsKey(invalid.ruleGroup))
                        {
                            invalidRules.Add(invalid.ruleGroup, new List<InvalidInfo>());
                        }
                        invalidRules[invalid.ruleGroup].Add(invalid);
                    }
                    else
                    {
                        rows.Add(new List<String>() { invalid.name, invalid.StandardS, invalid.info });
                    }
                }
                ConsoleTableUtil ctu = new ConsoleTableUtil(null, new List<List<String>>() { headers }, rows);
                ctu.Width = 12 * 10 + 4;
                String invalidStandards = ctu.PrintTable();
                invalidMessage += invalidStandards + "\n";
            }
            if (invalidRules.Count > 0)
            {
                invalidMessage += "Invalid rules\n";

                foreach (KeyValuePair<MIRuleGroup, List<InvalidInfo>> invalidRule in invalidRules)
                {
                    MIRuleGroup group = invalidRule.Key;
                    List<List<String>> headers = new List<List<String>>() { new List<String>() { "Rule: " + group }, new List<String>() { "Param", "Allow", "File's info" } };
                    List<List<string>> rows = new List<List<string>>();

                    for (var j = 0; j < invalidRule.Value.Count; j++)
                    {
                        InvalidInfo invalid = invalidRule.Value[j];
                        rows.Add(new List<String>() { invalid.name, invalid.StandardS, invalid.info });
                    }
                    ConsoleTableUtil ctu = new ConsoleTableUtil(null, headers, rows);
                    ctu.Cols = new int[] { 12 * 5, 12 * 3, 12 * 2 };
                    String invalidRuleStr = ctu.PrintTable();
                    invalidMessage += invalidRuleStr + "\n";
                }
            }
            return invalidMessage;
        }

        private void gvFiles_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, gridColumn2);
                if (category == "Hợp lệ")
                {
                    e.Appearance.BackColor = Color.Green;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
                else if (category == "Không hợp lệ")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void gvFiles_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    rtbReport.Text = lstResult[gvFiles.FocusedRowHandle];
                }
            }
            catch
            {
                HDMessageBox.Show("Bạn chưa kiểm tra file này, không thể xem được thông tin!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                lstResult.Clear();
                bsFiles.Clear();
                txtFiles.ResetText();
                txtFolder.ResetText();
                rtbReport.ResetText();
            }
            catch (Exception ex)
            {
                HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
