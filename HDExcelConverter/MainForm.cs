using HDControl;
using HDCore;
using NPOI.XSSF.UserModel;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        string excelInfoPath = "";
        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigForm frmConfig = new ConfigForm();
            frmConfig.Show();
            frmConfig.Activate();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.Show();
            frmAbout.Activate();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(AppSettings.Default.ImportFolder))
            {
                HDMessageBox.Show("Thư mục Import không tồn tại", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                open.InitialDirectory = AppSettings.Default.ImportFolder;
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtFileName.Text = open.SafeFileName;
                    if (!open.FileName.EndsWith(".xlsx"))
                    {
                        HDMessageBox.Show("Định dạng file không đúng chuẩn .xlsx", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    else
                    {
                        try
                        {
                            using (var fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                            {
                                var wb = new XSSFWorkbook(fs);
                                var sh = (XSSFSheet)wb.GetSheetAt(0);

                                bsExcelInfo.List.Clear();
                                for (int i = 1; i <= sh.LastRowNum; i++)
                                {
                                    try
                                    {
                                        View.exObj temp = new View.exObj();
                                        temp.Field = sh.GetRow(i).GetCell(0).ToString();
                                        temp.Detail = sh.GetRow(i).GetCell(1).ToString();

                                        bsExcelInfo.List.Add(temp);
                                        (bsExcelInfo.List as BindingList<View.exObj>).ToList().SaveObject(excelInfoPath);

                                    }
                                    catch //(Exception ex)
                                    {
                                        //HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        string configPath = "";
        private void MainForm_Load(object sender, EventArgs e)
        {
            excelInfoPath = Path.Combine(Application.StartupPath, "excelInfo.xml");
            configPath = Path.Combine(Application.StartupPath, "Config.xml");
            if (File.Exists(configPath))
                try
                {
                    var lstConfig = Utils.GetObject<List<View.Config>>(configPath);
                    foreach (var temp in lstConfig)
                    {
                        AppSettings.Default.ImportFolder = temp.ImportFolder;
                        AppSettings.Default.ExportFolder = temp.ExportFolder;
                    }
                }
                catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = AppSettings.Default.ImportFolder;
                saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FileName = txtFileName.Text;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    List<View.exObj> lstInfo = new List<View.exObj>();
                    foreach (var i in bsExcelInfo.List as BindingList<View.exObj>)
                    {
                        lstInfo.Add(i);
                    }
                    var tempPath = Path.Combine(AppSettings.Default.ImportFolder, saveFileDialog1.FileName);
                    if (tempPath.EndsWith(".xlsx"))
                    {
                        View.infoXlsx InfoObj = new View.infoXlsx();
                        InfoObj.DataSource = lstInfo;
                        InfoObj.CreateDocument();
                        InfoObj.ExportToXlsx(tempPath);
                    }
                    else
                        (bsExcelInfo.List as BindingList<View.exObj>).ToList().SaveObject(tempPath);
                }
            }
            catch (Exception ex)
            {
                HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> dicXml = new Dictionary<string, string>();                
                foreach (var i in bsExcelInfo.List as BindingList<View.exObj>)
                {
                    dicXml.Add(i.Field.Trim(), i.Detail.Trim());
                }
                View.xmlObj xml = new View.xmlObj()
                {
                    Channel = dicXml["Kênh"],
                    Type = dicXml["Chuyên mục"],
                    ProgramName = dicXml["Tên chương trình"],
                    ExternalProgramName = dicXml["Tên chương trình mở rộng"],
                    Season = dicXml["Phần"],
                    Episode = dicXml["Tập"],
                    Content = dicXml["Nội dung"],
                    BroadcastDate = dicXml["Thời gian phát sóng"],
                    TimeToLive = dicXml["Thời gian tồn tại"],
                    Copyrighted = dicXml["Bản quyền"],
                    CopyrightedStart = dicXml["Ngày bắt đầu bản quyền"],
                    CopyrightedEnd = dicXml["Ngày kết thúc bản quyền"],
                    StartTimeCode = dicXml["TCin"],
                    EndTimeCode = dicXml["TCout"],
                    WorkflowID = dicXml["Workflow"],
                    Director = dicXml["Đạo diễn"],
                    Actor = dicXml["Diễn viên"],
                    Year = dicXml["Năm khởi chiếu"],
                    CopyrightsScale = "Sử dụng cho VOD",
                    ProductionUnit = dicXml["Đơn vị sản xuất"],
                    ProductionCountry = dicXml["Nước sản xuất"],
                    Language = dicXml["Ngôn ngữ"],
                    Awards = " ",
                    Keyword = dicXml["Từ khóa"],
                    DistributorUnit = dicXml["Đơn vị phân phối"],
                    ParentalRating = dicXml["Giới hạn độ tuổi"],
                    StarRating = dicXml["Chấm điểm"]

                };
                if (!Directory.Exists(AppSettings.Default.ExportFolder))
                {
                    HDMessageBox.Show("Chưa cấu hình thư mục Export XML", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    xml.SaveObject(Path.Combine(AppSettings.Default.ExportFolder, txtFileName.Text + ".xml"));
                    System.Diagnostics.Process.Start(AppSettings.Default.ExportFolder);
                }
            }
            catch (Exception ex)
            {
                HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
