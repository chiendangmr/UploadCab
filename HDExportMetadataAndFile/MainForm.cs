using HDControl;
using HDCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Net;
using System.Threading;

namespace HDExportMetadataAndFile
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        string connectionStr = "";
        string sqlQuery = "";
        string sqlQueryFiles = "";
        string sqlQueryPic = "";
        string logFilePath = Path.Combine(Application.StartupPath, "Logs");
        bool isRunning = true;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtSaveFolder.Text.Trim().Length < 3) && ((txtNasIP.Text == "") && (txtNasUsername.Text == "") && (txtNasPass.Text == "") && (txtNasPath.Text == "")))
            {
                HDMessageBox.Show("Chưa cấu hình thư mục lưu trữ!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!ckExcel.Checked && !ckFullPic.Checked && !ckMediaHighres.Checked && !ckMediaLowres.Checked && !ckXml.Checked)
            {
                HDMessageBox.Show("Chưa cấu hình file cần xuất!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                RegisterForm rForm = new RegisterForm();
                rForm.ShowDialog();
                userConfigPath = Path.Combine(configFolder, rForm.userConfigPath);
                if (userConfigPath != "" && userConfigPath.Length > 0)
                {
                    bsConfig.List.Clear();
                    bsConfig.List.Add(new View.GlobalObj
                    {
                        NasIP = txtNasIP.Text,
                        NasPort = (int)nNasPort.Value,
                        NasUsername = txtNasUsername.Text,
                        NasPwd = txtNasPass.Text,
                        NasPath = txtNasPath.Text,
                        SaveFolder = txtSaveFolder.Text,
                        Symbol = txtSymbol.Text.Trim().Length > 0 ? txtSymbol.Text : " ",
                        exMediaLowres = ckMediaLowres.Checked,
                        exMediaHighres = ckMediaHighres.Checked,
                        exFullPic = ckFullPic.Checked,
                        exExcel = ckExcel.Checked,
                        exXml = ckXml.Checked,
                        isHead = ckHeader.Checked,
                        useBroadcastDate = ckDatePS.Checked,
                        useCreateDate = ckDateCreate.Checked,
                        useEpisode = ckEpisode.Checked,
                        useSeason = ckSeason.Checked,
                        useTenCTAdd = ckTenCTAdd.Checked,
                        useTenCT = ckTenCT.Checked,
                        useMaBang = ckMaBang.Checked 
                    });
                    (bsConfig.List as BindingList<View.GlobalObj>).ToList().SaveObject(userConfigPath);
                    HDMessageBox.Show("Lưu cấu hình thành công!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    ckEditMode.Checked = false;
                    this.WindowState = FormWindowState.Minimized;
                    if (thrMain != null)
                    {
                        isRunning = false;
                        thrMain.Join();
                    }
                    isRunning = true;
                    thrMain = new Thread(MainThread);
                    thrMain.IsBackground = true;
                    thrMain.Start();
                }
                else
                {
                    HDMessageBox.Show("Không lưu được cấu hình do chưa đăng nhập!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                HDMessageBox.Show(ex.ToString(), "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //string dbConfigPath = "";
        string dbCommandPath = "";
        string userConfigPath = "";
        string logFile = "";
        string configFolder = Path.Combine(Application.StartupPath, "Config");
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName(Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                HDMessageBox.Show("Phần mềm hiện đang chạy trên windows!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }
            dbCommandPath = Path.Combine(configFolder, "DBCommand.xml");
            if (File.Exists(dbCommandPath))
                try
                {
                    var lstConfig = Utils.GetObject<List<View.DBCommandObj>>(dbCommandPath);
                    foreach (var temp in lstConfig)
                    {
                        sqlQuery = temp.SQLQuery;
                        sqlQueryFiles = temp.SQLQueryFiles;
                        sqlQueryPic = temp.SQLQueryPic;
                    }
                }
                catch { }
            connectionStr = AppSettings.Default.ConnectionStr;

            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            DBConfigForm frmConfig = new DBConfigForm();
            frmConfig.Show();
            frmConfig.Activate();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.Show();
            frmAbout.Activate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            if (thrMain != null)
                thrMain.Join();
            Application.Exit();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveFolder = new FolderBrowserDialog();
            if (txtSaveFolder.Text.Trim().Length > 2)
                if (Directory.Exists(Path.GetFullPath(txtSaveFolder.Text)))
                {
                    saveFolder.SelectedPath = txtSaveFolder.Text;
                }
            if (saveFolder.ShowDialog() == DialogResult.OK)
            {
                txtSaveFolder.Text = saveFolder.SelectedPath;
            }
        }

        private void ckHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (ckHeader.Checked)
                ckFooter.Checked = false;
            else ckFooter.Checked = true;
        }

        private void ckFooter_CheckedChanged(object sender, EventArgs e)
        {
            if (ckFooter.Checked)
                ckHeader.Checked = false;
            else ckHeader.Checked = true;
        }
        private bool isNasPath(string path)
        {
            if (path.Contains("ftp://")) return true;
            return false;
        }
        Thread thrMain = null;
        void MainThread()
        {
            while (isRunning)
            {
                try
                {
                    string[] files = Directory.GetFiles(logFilePath, "*.txt", SearchOption.TopDirectoryOnly);
                    if (files.Count() > 80)
                    {
                        for (var i = 0; i < 50; i++)
                        {
                            System.IO.File.Delete(files[i]);
                        }
                    }
                    logFile = Path.Combine(logFilePath, DateTime.Now.Date.ToString("yyyy:MM:dd").Replace(":", "") + ".txt");
                    if (!File.Exists(logFile))
                    {
                        File.Create(logFile).Dispose();
                    }
                    using (var db = new SqlConnection(connectionStr))
                    {
                        try
                        {
                            if (sqlQuery != "" && sqlQuery != null)
                            {
                                var lstExportDB = db.Query<DAO.EXPORT_JOB>(sqlQuery).ToList();
                                if (lstExportDB.Count > 0)
                                {
                                    Task[] tasks = new Task[lstExportDB.Count];
                                    int i = 0;
                                    foreach (var exportDB in lstExportDB)
                                    {
                                        if (exportDB != null)
                                        {
                                            //Load config for each user
                                            var tempUserID = exportDB.USER_ID;
                                            userConfigPath = Path.Combine(configFolder, "user" + tempUserID.ToString() + "Config.xml");
                                            if (!ckEditMode.Checked)
                                            {
                                                if (File.Exists(userConfigPath))
                                                {
                                                    try
                                                    {
                                                        var lstConfig = Utils.GetObject<List<View.GlobalObj>>(userConfigPath);
                                                        foreach (var temp in lstConfig)
                                                        {
                                                            bsConfig.List.Add(temp);
                                                            txtNasIP.Text = temp.NasIP;
                                                            nNasPort.Value = temp.NasPort;
                                                            txtNasUsername.Text = temp.NasUsername;
                                                            txtNasPass.Text = temp.NasPwd;
                                                            txtNasPath.Text = temp.NasPath;
                                                            txtSaveFolder.Text = temp.SaveFolder;
                                                            ckMediaLowres.Checked = temp.exMediaLowres;
                                                            ckMediaHighres.Checked = temp.exMediaHighres;
                                                            ckFullPic.Checked = temp.exFullPic;
                                                            ckExcel.Checked = temp.exExcel;
                                                            ckXml.Checked = temp.exXml;
                                                            txtSymbol.Text = temp.Symbol;
                                                            ckHeader.Checked = temp.isHead == true ? true : false;
                                                            ckFooter.Checked = temp.isHead == false ? true : false;
                                                            ckMaBang.Checked = temp.useMaBang;
                                                            ckTenCT.Checked = temp.useTenCT;
                                                            ckTenCTAdd.Checked = temp.useTenCTAdd;
                                                            ckSeason.Checked = temp.useSeason;
                                                            ckDateCreate.Checked = temp.useCreateDate;
                                                            ckDatePS.Checked = temp.useBroadcastDate;
                                                            ckEpisode.Checked = temp.useEpisode;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        addLog(logFile, "Khong load dc user config: " + ex.ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    addLog(logFile, "Khong co user config");
                                                }
                                            }
                                            tasks[i] = Task.Run(() =>
                                            {
                                                try
                                                {
                                                    var filesDB = db.Query<View.FileObj>(sqlQueryFiles, new { id_clip = exportDB.ID_CLIP }).FirstOrDefault();
                                                    if (filesDB != null)
                                                    {
                                                        var ProgramNameAdd = filesDB.TEN_CHUONG_TRINH == null ? " " : filesDB.TEN_CHUONG_TRINH;
                                                        var ProgramName = filesDB.CommingNextNow == null ? " " : filesDB.CommingNextNow;
                                                        ProgramName = ProgramName.Replace("-", "").Replace("*", "").Replace("\'", "").Replace(":", "").Replace("\\", "").Replace("/", "").Replace("@", "").Replace("$", "").Replace("\"", "").Trim();
                                                        var creatDate = DateTime.Now;//filesDB.CREATE_DATE == null ? DateTime.Now : filesDB.CREATE_DATE;
                                                    var broadcastDate = DateTime.Now;//filesDB.DATE_TO_BROADCAST == null ? DateTime.Now : filesDB.DATE_TO_BROADCAST;
                                                    var startRight = filesDB.START_RIGHTS == null ? DateTime.Now : filesDB.START_RIGHTS;
                                                        var endRight = filesDB.END_RIGHTS == null ? DateTime.Now.AddYears(1) : filesDB.END_RIGHTS;
                                                        var season = filesDB.Season == null ? " " : filesDB.Season;
                                                        var episode = filesDB.EPISODE_NUMBER == null ? 0 : filesDB.EPISODE_NUMBER;
                                                        var maBang = filesDB.MA_BANG == null ? " " : filesDB.MA_BANG;

                                                        string OriginalFileName = ckMaBang.Checked ? maBang.Trim() : " ";
                                                        if (ckTenCT.Checked)
                                                        {
                                                            OriginalFileName += "_" + Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").Replace("-", "_").Replace("*", "_").Replace("\'", "_").Replace(":", "_").Replace("\\", "_").Replace("/", "_").Trim();
                                                        }
                                                        if (ckTenCTAdd.Checked)
                                                        {
                                                            OriginalFileName += "_" + Utils.ConvertToVietnameseNonSign(ProgramNameAdd).Replace(" ", "").Replace("-", "_").Replace("*", "_").Replace("\'", "_").Replace(":", "_").Replace("\\", "_").Replace("/", "_").Trim();
                                                        }
                                                        if (ckDatePS.Checked)
                                                        {
                                                            OriginalFileName += "_" + Utils.ConvertToVietnameseNonSign(broadcastDate.ToString("yyyy-MM-dd HH:mm:ss")).Replace(" ", "").Replace("-", "_").Replace("*", "_").Replace("\'", "_").Replace(":", "_").Replace("\\", "_").Replace("/", "_").Trim();
                                                        }
                                                        if (ckDateCreate.Checked)
                                                        {
                                                            OriginalFileName += "_" + Utils.ConvertToVietnameseNonSign(creatDate.ToString("yyyy-MM-dd HH:mm:ss")).Replace(" ", "").Replace("-", "_").Replace("*", "_").Replace("\'", "_").Replace(":", "_").Replace("\\", "_").Replace("/", "_").Trim();
                                                        }
                                                        if (ckSeason.Checked)
                                                        {
                                                            OriginalFileName += "_" + Utils.ConvertToVietnameseNonSign(season).Replace(" ", "").Replace("-", "_").Replace("*", "_").Replace("\'", "_").Replace(":", "_").Replace("\\", "_").Replace("/", "_").Trim();
                                                        }
                                                        if (ckEpisode.Checked)
                                                        {
                                                            OriginalFileName += "_" + Utils.ConvertToVietnameseNonSign(episode.ToString()).Replace(" ", "").Replace("-", "_").Replace("*", "_").Replace("\'", "_").Replace("\\", "_").Replace(":", "_").Replace("/", "_").Trim();
                                                        }
                                                        var tempHighres = filesDB.FILE_NAME == null ? " " : filesDB.FILE_NAME;
                                                        var tempThumbPicture = filesDB.THUMB_FILE_NAME == null ? " " : filesDB.THUMB_FILE_NAME;
                                                        var tempLowres = filesDB.HD_CLIP == null ? " " : filesDB.HD_CLIP;
                                                        var nasHighresPath = filesDB.DATA1_DIRECTORY == null ? " " : filesDB.DATA1_DIRECTORY;
                                                        var uncHighresPath = filesDB.UNC_BASE_PATH_DATA1 == null ? " " : filesDB.UNC_BASE_PATH_DATA1;
                                                        var nasLowresPath = filesDB.DATA3_DIRECTORY == null ? " " : filesDB.DATA3_DIRECTORY;
                                                        var uncLowresPath = filesDB.UNC_BASE_PATH_DATA3 == null ? " " : filesDB.UNC_BASE_PATH_DATA3;
                                                        string addSymbol = (txtSymbol.Text != " " && txtSymbol.Text != "") ? txtSymbol.Text : "";
                                                        string srcPath = "ftp://" + filesDB.NAS_IP + ":" + filesDB.PORT;

                                                    #region Export từ nas này sang nas khác
                                                    if (txtNasIP.Text != null && txtNasIP.Text != "" && txtNasUsername.Text != null && txtNasUsername.Text != "" && txtNasPass.Text != null && txtNasPass.Text != "" && txtNasPath.Text != null && txtNasPath.Text != "")
                                                        {
                                                            var tempTargetPath = "ftp://" + txtNasIP.Text + ":" + nNasPort.Value.ToString() + txtNasPath.Text;

                                                        #region Export XML
                                                        if (ckXml.Checked)
                                                            {
                                                                int epiNum = getEpisodeNUmber(ProgramName);
                                                            #region Phim le
                                                            if (epiNum == 0)
                                                                {
                                                                    try
                                                                    {
                                                                        View.XMLChildObject xmlChild = new View.XMLChildObject()
                                                                        {
                                                                            rootID = "GLOBAL",
                                                                            rootScheduleDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",

                                                                            contentAction = "override",
                                                                            contentActors = filesDB.ACTOR,
                                                                            contentAspect = filesDB.ASPECT_RATIO,
                                                                            contentCategories = "",
                                                                            contentContentAction = "override",
                                                                            contentContentDefinition = "HD",
                                                                            contentContentEncProfileName = "Envivio_STB_ENC_WOBI_ST_NAS_PS1",
                                                                            contentContentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "p",
                                                                            contentContentPreLoaded = "false",
                                                                            contentContentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            contentContentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            contentCountries = "",
                                                                            contentServiceId = filesDB.SECTOR_NAME,
                                                                            contentDirectors = filesDB.DIRECTOR,
                                                                            contentDuration = ((int)TimeSpan.Parse(filesDB.TC_OUT).TotalSeconds).ToString(),
                                                                            contentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                            contentIsRecordable = "0",
                                                                            contentLanguage = "",
                                                                            contentMediaAction = "override",
                                                                            contentMediaFilename = Path.GetFileName(filesDB.FILE_NAME),
                                                                            contentMediaFileSize = filesDB.FILE_SIZE.ToString(),
                                                                            contentMediaFormat = "AV_ClearTS",
                                                                            contentMediaFrameDuration = "3000",
                                                                            contentMediaID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "f",
                                                                            contentMediaRelativePath = Path.GetDirectoryName(Path.Combine(filesDB.UNC_BASE_PATH_DATA1, filesDB.FILE_NAME)).Substring(14).Replace("\\", "/") + "/",
                                                                            contentMediaSrcAssetType = "Clear_Asset_HD",
                                                                            contentMediaStorageDevice = "NAS169",
                                                                            contentMediaType = "Source",
                                                                            contentPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                                            contentRating = "1",
                                                                            contentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            contentStudio = "VTVcab",
                                                                            contentSubtitles = "",
                                                                            contentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            contentViCopyright = "",
                                                                            contentViDescription = filesDB.NOI_DUNG,
                                                                            contentViSynopsis = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            contentViTitle = ProgramName,
                                                                            contentYear = broadcastDate.Year.ToString(),

                                                                            imageAction = "override",
                                                                            imageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            imageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Main",
                                                                            imageIllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                            imageImageAction = "override",
                                                                            imageImageEncProfileName = "Image_transfer_HD1",
                                                                            imageImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            imageImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Child",
                                                                            imageImagePreLoaded = "false",
                                                                            imageImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            imageImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            imageMediaComment = "",
                                                                            imageMediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + ".jpg",
                                                                            imageMediaFileSize = "1",
                                                                            imageMediaFormat = "Image_Basic",
                                                                            imageMediaFrameDuration = "3000",
                                                                            imageMediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Media",
                                                                            imageMediaRelativePath = "AllImages/",
                                                                            imageMediaSrcAssetType = "Clear_Asset_HD",
                                                                            imageMediaStorageDevice = "WLA",
                                                                            imageMediaType = "Source",
                                                                            imageStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            imageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                            image2Action = "override",
                                                                            image2ExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            image2ID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Main",
                                                                            image2IllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                            image2ImageAction = "override",
                                                                            image2ImageEncProfileName = "Image_transfer_HD1",
                                                                            image2ImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            image2ImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Child",
                                                                            image2ImagePreLoaded = "false",
                                                                            image2ImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            image2ImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            image2MediaComment = "",
                                                                            image2MediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster.jpg",
                                                                            image2MediaFileSize = "1",
                                                                            image2MediaFormat = "Image_Basic",
                                                                            image2MediaFrameDuration = "3000",
                                                                            image2MediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Media",
                                                                            image2MediaRelativePath = "AllImages/",
                                                                            image2MediaSrcAssetType = "Clear_Asset_HD",
                                                                            image2MediaStorageDevice = "WLA",
                                                                            image2MediaType = "Source",
                                                                            image2StartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            image2Title = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                            vodItemContentRef = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "p",
                                                                            vodItemAction = "override",
                                                                            vodItemDisplayPriority = "LYS000024084/0",
                                                                            vodItemID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                            vodItemNodeRefList = "LYS000024084",
                                                                            vodItemPeriodEnd = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            vodItemPeriodStart = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            vodItemTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                            productAction = "override",
                                                                            productCurrency = "VND",
                                                                            productElementId = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                            productElementKind = "VodItem",
                                                                            productID = "LYS000041271",
                                                                            productPrice = "0",
                                                                            productTitle = "AVOD",
                                                                            productType = "subscription"

                                                                        };
                                                                        View.XMLObject xmlObject = new View.XMLObject();
                                                                        xmlObject.GenerateXml(xmlChild);
                                                                        var temp = Path.Combine(txtSaveFolder.Text, OriginalFileName + ".xml");
                                                                        xmlObject.SaveXmlFile(temp);
                                                                        addLog(logFile, "Xuat xml sang unc path tu nas thanh cong");
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        addLog(logFile, "Xuat xml sang unc path tu nas ko thanh cong: " + ex.ToString());
                                                                    }
                                                                #endregion
                                                            }
                                                            #region Tap 1 phim bo
                                                            else //if (epiNum == 1)
                                                            {
                                                                    try
                                                                    {
                                                                        View.XMLLongChildObject xmlChild = new View.XMLLongChildObject()
                                                                        {
                                                                            rootID = "GLOBAL",
                                                                            rootScheduleDate = creatDate.ToString("yyyy-MM-dd") + "T" + creatDate.ToString("HH:mm:ss") + "Z",

                                                                            seriesCategories = "",
                                                                            seriesEnTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            seriesID = Utils.ConvertToVietnameseNonSign(ProgramName.Remove(ProgramName.LastIndexOf(epiNum.ToString()))).Replace(" ", "").ToLower() + "s",
                                                                            seriesPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                                            seriesRating = "1",
                                                                            seriesViSynopsis = "",
                                                                            seriesViTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            seriesTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            seriesAction = "override",

                                                                            contentNumber = "1",
                                                                            contentSeriesRef = Utils.ConvertToVietnameseNonSign(ProgramName.Remove(ProgramName.LastIndexOf(epiNum.ToString()))).Replace(" ", "").ToLower() + "s",
                                                                            contentAction = "override",
                                                                            contentActors = filesDB.ACTOR,
                                                                            contentAspect = filesDB.ASPECT_RATIO,
                                                                            contentCategories = "",
                                                                            contentContentAction = "override",
                                                                            contentContentDefinition = filesDB.VIDEO_FORMAT,
                                                                            contentContentEncProfileName = "Envivio_STB_ENC_WOBI_ST_NAS_PS1",
                                                                            contentContentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "p",
                                                                            contentContentPreLoaded = "false",
                                                                            contentContentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            contentContentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            contentCountries = "",
                                                                            contentServiceId = filesDB.SECTOR_NAME,
                                                                            contentDirectors = filesDB.DIRECTOR,
                                                                            contentDuration = ((int)TimeSpan.Parse(filesDB.TC_OUT).TotalSeconds).ToString(),
                                                                            contentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                            contentIsRecordable = "0",
                                                                            contentLanguage = "",
                                                                            contentMediaAction = "override",
                                                                            contentMediaFilename = Path.GetFileName(filesDB.FILE_NAME),
                                                                            contentMediaFileSize = filesDB.FILE_SIZE.ToString(),
                                                                            contentMediaFormat = "AV_ClearTS",
                                                                            contentMediaFrameDuration = "3000",
                                                                            contentMediaID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "f",
                                                                            contentMediaRelativePath = Path.GetDirectoryName(Path.Combine(filesDB.UNC_BASE_PATH_DATA1, filesDB.FILE_NAME)).Substring(14).Replace("\\", "/") + "/",
                                                                            contentMediaSrcAssetType = "Clear_Asset_HD",
                                                                            contentMediaStorageDevice = "NAS169",
                                                                            contentMediaType = "Source",
                                                                            contentPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                                            contentRating = "1",
                                                                            contentStartDate = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                            contentStudio = "VTVcab",
                                                                            contentSubtitles = "",
                                                                            contentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            contentViCopyright = "",
                                                                            contentViDescription = filesDB.NOI_DUNG,
                                                                            contentViSynopsis = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            contentViTitle = ProgramName,
                                                                            contentYear = broadcastDate.Year.ToString(),

                                                                            imageAction = "override",
                                                                            imageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            imageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Main",
                                                                            imageIllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                            imageImageAction = "override",
                                                                            imageImageEncProfileName = "Image_transfer_HD1",
                                                                            imageImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            imageImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Child",
                                                                            imageImagePreLoaded = "false",
                                                                            imageImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            imageImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            imageMediaComment = "",
                                                                            imageMediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + ".jpg",
                                                                            imageMediaFileSize = "1",
                                                                            imageMediaFormat = "Image_Basic",
                                                                            imageMediaFrameDuration = "3000",
                                                                            imageMediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Media",
                                                                            imageMediaRelativePath = "AllImages/",
                                                                            imageMediaSrcAssetType = "Clear_Asset_HD",
                                                                            imageMediaStorageDevice = "WLA",
                                                                            imageMediaType = "Source",
                                                                            imageStartDate = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                            imageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                            image2Action = "override",
                                                                            image2ExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            image2ID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Main",
                                                                            image2IllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                            image2ImageAction = "override",
                                                                            image2ImageEncProfileName = "Image_transfer_HD1",
                                                                            image2ImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            image2ImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Child",
                                                                            image2ImagePreLoaded = "false",
                                                                            image2ImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            image2ImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                            image2MediaComment = "",
                                                                            image2MediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster.jpg",
                                                                            image2MediaFileSize = "1",
                                                                            image2MediaFormat = "Image_Basic",
                                                                            image2MediaFrameDuration = "3000",
                                                                            image2MediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Media",
                                                                            image2MediaRelativePath = "AllImages/",
                                                                            image2MediaSrcAssetType = "Clear_Asset_HD",
                                                                            image2MediaStorageDevice = "WLA",
                                                                            image2MediaType = "Source",
                                                                            image2StartDate = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                            image2Title = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                            vodItemContentRef = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "p",
                                                                            vodItemAction = "override",
                                                                            vodItemDisplayPriority = "LYS003582537/0",
                                                                            vodItemID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                            vodItemNodeRefList = "LYS003582537",
                                                                            vodItemPeriodEnd = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                            vodItemPeriodStart = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                            vodItemTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                            productAction = "override",
                                                                            productCurrency = "VND",
                                                                            productElementId = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                            productElementKind = "VodItem",
                                                                            productID = "LYS000041271",
                                                                            productPrice = "0",
                                                                            productTitle = "AVOD",
                                                                            productType = "subscription"

                                                                        };
                                                                        View.XMLLongObject xmlObject = new View.XMLLongObject();
                                                                        xmlObject.GenerateXml(xmlChild);
                                                                        var temp = Path.Combine(txtSaveFolder.Text, OriginalFileName + ".xml");
                                                                        xmlObject.SaveXmlFile(temp);
                                                                        addLog(logFile, "Xuat xml sang unc path tu nas thanh cong");
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        addLog(logFile, "Xuat xml sang unc path tu nas ko thanh cong: " + ex.ToString());
                                                                    }
                                                                #endregion
                                                            }
                                                            #region tap 2 tro len phim bo
                                                            //else
                                                            //{
                                                            //    try
                                                            //    {
                                                            //        View.XMLShortChildObject xmlChild = new View.XMLShortChildObject()
                                                            //        {
                                                            //            rootID = "GLOBAL",
                                                            //            rootScheduleDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",

                                                            //            contentNumber = epiNum.ToString(),
                                                            //            contentSeriesRef = Utils.ConvertToVietnameseNonSign(ProgramName.Remove(ProgramName.LastIndexOf(epiNum.ToString()))).Replace(" ", "").ToLower() + "s",
                                                            //            contentAction = "override",
                                                            //            contentActors = filesDB.ACTOR,
                                                            //            contentAspect = filesDB.ASPECT_RATIO,
                                                            //            contentCategories = "",
                                                            //            contentContentAction = "override",
                                                            //            contentContentDefinition = filesDB.VIDEO_FORMAT,
                                                            //            contentContentEncProfileName = "",
                                                            //            contentContentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "p",
                                                            //            contentContentPreLoaded = "false",
                                                            //            contentContentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                            //            contentContentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                            //            contentCountries = "",
                                                            //            contentDirectors = filesDB.DIRECTOR,
                                                            //            contentDuration = ((int)TimeSpan.Parse(filesDB.TC_OUT).TotalSeconds).ToString(),
                                                            //            contentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                            //            contentIsRecordable = "0",
                                                            //            contentLanguage = "",
                                                            //            contentMediaAction = "override",
                                                            //            contentMediaFilename = Path.GetFileName(filesDB.FILE_NAME),
                                                            //            contentMediaFileSize = filesDB.FILE_SIZE.ToString(),
                                                            //            contentMediaFormat = "AV_ClearTS",
                                                            //            contentMediaFrameDuration = "3000",
                                                            //            contentMediaID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "f",
                                                            //            contentMediaRelativePath = Path.GetDirectoryName(Path.Combine(filesDB.UNC_BASE_PATH_DATA3, filesDB.FILE_NAME)).Substring(14).Replace("\\", "/") + "/",
                                                            //            contentMediaSrcAssetType = "Clear_Asset_HD",
                                                            //            contentMediaStorageDevice = "NAS",
                                                            //            contentMediaType = "Source",
                                                            //            contentPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                            //            contentRating = "1",
                                                            //            contentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                            //            contentStudio = "VTVcab",
                                                            //            contentSubtitles = "",
                                                            //            contentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                            //            contentViCopyright = "",
                                                            //            contentViDescription = filesDB.NOI_DUNG,
                                                            //            contentViSynopsis = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                            //            contentViTitle = ProgramName,
                                                            //            contentYear = Convert.ToDateTime(filesDB.DATE_TO_BROADCAST).Year.ToString(),

                                                            //            vodItemContentRef = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "p",
                                                            //            vodItemAction = "override",
                                                            //            vodItemDisplayPriority = "LYS003582537/0",
                                                            //            vodItemID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                            //            vodItemNodeRefList = "LYS003582537",
                                                            //            vodItemPeriodEnd = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                            //            vodItemPeriodStart = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                            //            vodItemTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                            //            productAction = "override",
                                                            //            productCurrency = "VND",
                                                            //            productElementId = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                            //            productElementKind = "VodItem",
                                                            //            productID = "LYS000041271",
                                                            //            productPrice = "0",
                                                            //            productTitle = "AVOD",
                                                            //            productType = "subscription"

                                                            //        };
                                                            //        View.XMLShortObject xmlObject = new View.XMLShortObject();
                                                            //        xmlObject.GenerateXml(xmlChild);
                                                            //        var temp = Path.Combine(txtSaveFolder.Text, OriginalFileName + ".xml");
                                                            //        xmlObject.SaveXmlFile(temp);
                                                            //        addLog(logFile, "Xuat xml sang unc path tu nas thanh cong");
                                                            //    }
                                                            //    catch (Exception ex)
                                                            //    {
                                                            //        addLog(logFile, "Xuat xml sang unc path tu nas ko thanh cong: " + ex.ToString());
                                                            //    }
                                                            //}
                                                            #endregion
                                                        }
                                                        #endregion

                                                        #region Export Excel         
                                                        if (ckExcel.Checked)
                                                            {
                                                                try
                                                                {
                                                                    string tempProgramName = ProgramName.Replace(" ", "").Trim();
                                                                    var index = Utils.ConvertToVietnameseNonSign(tempProgramName).LastIndexOf("Tap");
                                                                    var tapStr = index >= 0 ? tempProgramName.Substring(index).Substring(3).Trim() : " ";
                                                                    if (tapStr.IndexOf(' ') > 0)
                                                                        tapStr = tapStr.Substring(0, tapStr.IndexOf(' '));
                                                                    tapStr = index >= 0 ? "Tap" + tapStr : " ";
                                                                    string temp = Path.Combine(Application.StartupPath, OriginalFileName + ".xlsx");
                                                                    List<View.exObj> lstEx = new List<View.exObj>();
                                                                    View.exObj ex = new View.exObj()
                                                                    {
                                                                        auto_play_succesor = Utils.ConvertToVietnameseNonSign(tempProgramName),
                                                                        externalReference = Utils.ConvertToVietnameseNonSign(tempProgramName).ToLower(),
                                                                        parent = Utils.ConvertToVietnameseNonSign(tempProgramName).ToLower(),
                                                                        thumbnail = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "").ToLower() + ".jpg",
                                                                        media_file = Utils.ConvertToVietnameseNonSign(tempProgramName) + ".mp4",
                                                                        image_file_name1 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + "_Poster.jpg",
                                                                        image_file_name2 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + ".jpg",
                                                                        movideo_params_required1 = Utils.ConvertToVietnameseNonSign(tempProgramName) + ".mp4",
                                                                        movideo_params_required2 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + ".jpg",
                                                                        movideo_params_required3 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + "_Poster.jpg",
                                                                        movideo_params_required4 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + ".jpg",
                                                                        movideo_params_required5 = Utils.ConvertToVietnameseNonSign(tempProgramName) + ".srt",
                                                                        media_duration = filesDB.TC_OUT,
                                                                        linkTranscode = tempHighres,
                                                                        tagProfileId = filesDB.TYPE,
                                                                        media_description = filesDB.NOI_DUNG,
                                                                        product_description = filesDB.NOI_DUNG,

                                                                    };
                                                                    lstEx.Add(ex);
                                                                    View.infoXlsx InfoObj = new View.infoXlsx();
                                                                    InfoObj.DataSource = lstEx;
                                                                    InfoObj.CreateDocument();
                                                                    InfoObj.ExportToXlsx(temp);
                                                                    if (uploadFromUnc(temp, Path.GetFileName(temp), txtNasIP.Text, nNasPort.Value.ToString(), txtNasPath.Text, txtNasUsername.Text, txtNasPass.Text))
                                                                    {
                                                                    //ghi log
                                                                    addLog(logFile, "Xuat excel thanh cong");
                                                                        File.Delete(temp);
                                                                    }
                                                                    else
                                                                    {
                                                                    //ghi log
                                                                    addLog(logFile, "Xuat excel ko thanh cong");
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    addLog(logFile, "Co loi khi xuat excel: " + ex.ToString());
                                                                }
                                                            }
                                                        #endregion

                                                        #region Export Media Highres
                                                        bool uncHighresSuccess = false;
                                                            if (ckMediaHighres.Checked && uncHighresPath != null && uncHighresPath.Length > 0)
                                                            {
                                                                try
                                                                {
                                                                    if (uploadFromUnc(uncHighresPath + tempHighres, OriginalFileName + ".mxf", txtNasIP.Text, nNasPort.Value.ToString(), txtNasPath.Text, txtNasUsername.Text, txtNasPass.Text))
                                                                    {
                                                                        uncHighresSuccess = true;
                                                                    //Ghi log
                                                                    addLog(logFile, "Xuat highres bang unc thanh cong");
                                                                    }
                                                                    else
                                                                    {
                                                                    //Ghi log
                                                                    addLog(logFile, "Ko xuat dc highres bang unc");
                                                                    }
                                                                }
                                                                catch (Exception ex) { addLog(logFile, "Loi khi xuat highres bang unc: " + ex.ToString()); }
                                                            }
                                                            if (ckMediaHighres.Checked && nasHighresPath != null && nasHighresPath.Length > 0 && !uncHighresSuccess)
                                                            {
                                                                try
                                                                {
                                                                    var tempSourcePath = srcPath + nasHighresPath;
                                                                    if (copyFile(tempHighres, OriginalFileName + ".mxf", tempSourcePath, filesDB.USERNAME, filesDB.PASSWORD, tempTargetPath, txtNasUsername.Text, txtNasPass.Text))
                                                                    {
                                                                    //Ghi log
                                                                    addLog(logFile, "Xuat highres bang ftp thanh cong");
                                                                    }
                                                                    else
                                                                    {
                                                                    //Ghi log
                                                                    addLog(logFile, "Ko xuat dc highres bang ftp");
                                                                    }
                                                                }
                                                                catch (Exception ex) { addLog(logFile, "Loi khi xuat highres bang ftp: " + ex.ToString()); }
                                                            }
                                                        #endregion

                                                        #region Export Media Lowres
                                                        bool uncLowresSuccess = false;
                                                            if (ckMediaLowres.Checked && uncLowresPath != null && uncLowresPath.Length > 0)
                                                            {
                                                                try
                                                                {
                                                                    if (uploadFromUnc(uncLowresPath + tempLowres, OriginalFileName + ".mp4", txtNasIP.Text, nNasPort.Value.ToString(), txtNasPath.Text, txtNasUsername.Text, txtNasPass.Text))
                                                                    {
                                                                        uncLowresSuccess = true;
                                                                    //Ghi log
                                                                    addLog(logFile, "Xuat Lowres bang unc thanh cong");
                                                                    }
                                                                    else
                                                                    {
                                                                    //Ghi log
                                                                    addLog(logFile, "Ko xuat dc Lowres bang unc");
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    addLog(logFile, "Loi khi xuat Lowres bang unc: " + ex.ToString());
                                                                }
                                                            }
                                                            if (ckMediaLowres.Checked && nasLowresPath != null && nasLowresPath.Length > 0 && !uncLowresSuccess)
                                                            {
                                                                try
                                                                {
                                                                    var tempSourcePath = srcPath + nasLowresPath;
                                                                    if (copyFile(tempLowres, OriginalFileName + ".mp4", tempSourcePath, filesDB.USERNAME, filesDB.PASSWORD, tempTargetPath, txtNasUsername.Text, txtNasPass.Text))
                                                                    {
                                                                    //Ghi log
                                                                    addLog(logFile, "Xuat highres bang ftp thanh cong");
                                                                    }
                                                                    else
                                                                    {
                                                                    //Ghi log
                                                                    addLog(logFile, "ko xuat dc Lowres bang ftp");
                                                                    }
                                                                }
                                                                catch (Exception ex) { addLog(logFile, "Loi khi xuat Lowres bang ftp: " + ex.ToString()); }
                                                            }
                                                        #endregion

                                                        #region Export ảnh
                                                        bool uncPicSucess = false;
                                                            if (ckFullPic.Checked)
                                                            {
                                                                try
                                                                {
                                                                    var picDb = db.Query<View.PosterObj>(sqlQueryPic, new { id_clip = exportDB.ID_CLIP, file_type = 11 }).FirstOrDefault();
                                                                    var picPathDb = db.Query<DAO.INFORTAPE_FILE_TYPE>(@"select * in INFORTAPE_FILE_TYPE where ID = 11").FirstOrDefault();
                                                                    if (picDb != null && picPathDb != null)
                                                                    {
                                                                    //unc
                                                                    try
                                                                        {
                                                                            if (uploadFromUnc("\\\\" + picDb.NAS_IP + "\\" + picPathDb.NAS_DATA_PATH + "\\" + picDb.FILE_NAME, OriginalFileName + "." + Path.GetExtension(picDb.FILE_NAME), txtNasIP.Text, nNasPort.Value.ToString(), txtNasPath.Text, txtNasUsername.Text, txtNasPass.Text))
                                                                            {
                                                                                uncPicSucess = true;
                                                                            //Ghi log
                                                                            addLog(logFile, "Xuat poster bang unc thanh cong");
                                                                            }
                                                                            else
                                                                            {
                                                                            //Ghi log
                                                                            addLog(logFile, "Xuat poster bang unc ko thanh cong");
                                                                            }
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            addLog(logFile, "Xuat poster bang unc ko thanh cong: " + ex.ToString());
                                                                        }
                                                                    //nas
                                                                    if (!uncPicSucess)
                                                                        {
                                                                            try
                                                                            {
                                                                                if (copyFile(picDb.FILE_NAME, OriginalFileName + "." + Path.GetExtension(picDb.FILE_NAME), "ftp://" + picDb.NAS_IP + ":" + picDb.PORT + "/" + picPathDb.NAS_DATA_PATH + "/" + picDb.FILE_NAME, picDb.USERNAME, picDb.PASSWORD, tempTargetPath, txtNasUsername.Text, txtNasPass.Text))
                                                                                {
                                                                                //Ghi log
                                                                                addLog(logFile, "Xuat poster bang ftp thanh cong");
                                                                                }
                                                                                else
                                                                                {
                                                                                //Ghi log
                                                                                addLog(logFile, "Xuat poster bang ftp ko thanh cong");
                                                                                }
                                                                            }
                                                                            catch (Exception ex) { addLog(logFile, "Xuat poster bang ftp ko thanh cong: " + ex.ToString()); }
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex) { addLog(logFile, "Loi khi xuat poster: " + ex.ToString()); }
                                                            }
                                                        #endregion

                                                    }
                                                    #endregion

                                                    #region Export từ nas sang unc path
                                                    if (txtSaveFolder.Text != null && txtSaveFolder.Text != "")
                                                        {
                                                            try
                                                            {
                                                                if (!Directory.Exists(txtSaveFolder.Text))
                                                                {
                                                                    Directory.CreateDirectory(txtSaveFolder.Text);
                                                                }
                                                            #region Export XML
                                                            if (ckXml.Checked)
                                                                {
                                                                    int epiNum = getEpisodeNUmber(ProgramName);
                                                                #region Phim le
                                                                if (epiNum == 0)
                                                                    {
                                                                        try
                                                                        {
                                                                            View.XMLChildObject xmlChild = new View.XMLChildObject()
                                                                            {
                                                                                rootID = "GLOBAL",
                                                                                rootScheduleDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",

                                                                                contentAction = "override",
                                                                                contentActors = filesDB.ACTOR,
                                                                                contentAspect = filesDB.ASPECT_RATIO,
                                                                                contentCategories = "",
                                                                                contentContentAction = "override",
                                                                                contentContentDefinition = "HD",
                                                                                contentContentEncProfileName = "Envivio_STB_ENC_WOBI_ST_NAS_PS1",
                                                                                contentContentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "p",
                                                                                contentContentPreLoaded = "false",
                                                                                contentContentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                contentContentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                contentCountries = "",
                                                                                contentServiceId = filesDB.SECTOR_NAME,
                                                                                contentDirectors = filesDB.DIRECTOR,
                                                                                contentDuration = ((int)TimeSpan.Parse(filesDB.TC_OUT).TotalSeconds).ToString(),
                                                                                contentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                                contentIsRecordable = "0",
                                                                                contentLanguage = "",
                                                                                contentMediaAction = "override",
                                                                                contentMediaFilename = Path.GetFileName(filesDB.FILE_NAME),
                                                                                contentMediaFileSize = filesDB.FILE_SIZE.ToString(),
                                                                                contentMediaFormat = "AV_ClearTS",
                                                                                contentMediaFrameDuration = "3000",
                                                                                contentMediaID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "f",
                                                                                contentMediaRelativePath = Path.GetDirectoryName(Path.Combine(filesDB.UNC_BASE_PATH_DATA1, filesDB.FILE_NAME)).Substring(14).Replace("\\", "/") + "/",
                                                                                contentMediaSrcAssetType = "Clear_Asset_HD",
                                                                                contentMediaStorageDevice = "NAS169",
                                                                                contentMediaType = "Source",
                                                                                contentPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                                                contentRating = "1",
                                                                                contentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                contentStudio = "VTVcab",
                                                                                contentSubtitles = "",
                                                                                contentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                contentViCopyright = "",
                                                                                contentViDescription = filesDB.NOI_DUNG,
                                                                                contentViSynopsis = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                contentViTitle = ProgramName,
                                                                                contentYear = broadcastDate.Year.ToString(),

                                                                                imageAction = "override",
                                                                                imageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                imageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Main",
                                                                                imageIllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                                imageImageAction = "override",
                                                                                imageImageEncProfileName = "Image_transfer_HD1",
                                                                                imageImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                imageImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Child",
                                                                                imageImagePreLoaded = "false",
                                                                                imageImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                imageImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                imageMediaComment = "",
                                                                                imageMediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + ".jpg",
                                                                                imageMediaFileSize = "1",
                                                                                imageMediaFormat = "Image_Basic",
                                                                                imageMediaFrameDuration = "3000",
                                                                                imageMediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Media",
                                                                                imageMediaRelativePath = "AllImages/",
                                                                                imageMediaSrcAssetType = "Clear_Asset_HD",
                                                                                imageMediaStorageDevice = "WLA",
                                                                                imageMediaType = "Source",
                                                                                imageStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                imageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                                image2Action = "override",
                                                                                image2ExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                image2ID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Main",
                                                                                image2IllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                                image2ImageAction = "override",
                                                                                image2ImageEncProfileName = "Image_transfer_HD1",
                                                                                image2ImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                image2ImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Child",
                                                                                image2ImagePreLoaded = "false",
                                                                                image2ImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                image2ImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                image2MediaComment = "",
                                                                                image2MediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster.jpg",
                                                                                image2MediaFileSize = "1",
                                                                                image2MediaFormat = "Image_Basic",
                                                                                image2MediaFrameDuration = "3000",
                                                                                image2MediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Media",
                                                                                image2MediaRelativePath = "AllImages/",
                                                                                image2MediaSrcAssetType = "Clear_Asset_HD",
                                                                                image2MediaStorageDevice = "WLA",
                                                                                image2MediaType = "Source",
                                                                                image2StartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                image2Title = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                                vodItemContentRef = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "p",
                                                                                vodItemAction = "override",
                                                                                vodItemDisplayPriority = "LYS000024084/0",
                                                                                vodItemID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                                vodItemNodeRefList = "LYS000024084",
                                                                                vodItemPeriodEnd = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                vodItemPeriodStart = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                vodItemTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                                productAction = "override",
                                                                                productCurrency = "VND",
                                                                                productElementId = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                                productElementKind = "VodItem",
                                                                                productID = "LYS000041271",
                                                                                productPrice = "0",
                                                                                productTitle = "AVOD",
                                                                                productType = "subscription"

                                                                            };
                                                                            View.XMLObject xmlObject = new View.XMLObject();
                                                                            xmlObject.GenerateXml(xmlChild);
                                                                            var temp = Path.Combine(txtSaveFolder.Text, OriginalFileName + ".xml");
                                                                            xmlObject.SaveXmlFile(temp);
                                                                            addLog(logFile, "Xuat xml sang unc path tu unc path thanh cong");
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            addLog(logFile, "Xuat xml sang unc path tu unc path ko thanh cong: " + ex.ToString());
                                                                        }
                                                                    #endregion
                                                                }
                                                                #region Tap 1 phim bo
                                                                else //if (epiNum == 1)
                                                                {
                                                                        try
                                                                        {
                                                                            View.XMLLongChildObject xmlChild = new View.XMLLongChildObject()
                                                                            {
                                                                                rootID = "GLOBAL",
                                                                                rootScheduleDate = creatDate.ToString("yyyy-MM-dd") + "T" + creatDate.ToString("HH:mm:ss") + "Z",

                                                                                seriesCategories = "",
                                                                                seriesEnTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                seriesID = Utils.ConvertToVietnameseNonSign(ProgramName.Remove(ProgramName.LastIndexOf(epiNum.ToString()))).Replace(" ", "").ToLower() + "s",
                                                                                seriesPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                                                seriesRating = "1",
                                                                                seriesViSynopsis = "",
                                                                                seriesViTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                seriesTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                seriesAction = "override",

                                                                                contentNumber = "1",
                                                                                contentSeriesRef = Utils.ConvertToVietnameseNonSign(ProgramName.Remove(ProgramName.LastIndexOf(epiNum.ToString()))).Replace(" ", "").ToLower() + "s",
                                                                                contentAction = "override",
                                                                                contentActors = filesDB.ACTOR,
                                                                                contentAspect = filesDB.ASPECT_RATIO,
                                                                                contentCategories = "",
                                                                                contentContentAction = "override",
                                                                                contentContentDefinition = filesDB.VIDEO_FORMAT,
                                                                                contentContentEncProfileName = "Envivio_STB_ENC_WOBI_ST_NAS_PS1",
                                                                                contentContentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "p",
                                                                                contentContentPreLoaded = "false",
                                                                                contentContentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                contentContentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                contentCountries = "",
                                                                                contentServiceId = filesDB.SECTOR_NAME,
                                                                                contentDirectors = filesDB.DIRECTOR,
                                                                                contentDuration = ((int)TimeSpan.Parse(filesDB.TC_OUT).TotalSeconds).ToString(),
                                                                                contentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                                contentIsRecordable = "0",
                                                                                contentLanguage = "",
                                                                                contentMediaAction = "override",
                                                                                contentMediaFilename = Path.GetFileName(filesDB.FILE_NAME),
                                                                                contentMediaFileSize = filesDB.FILE_SIZE.ToString(),
                                                                                contentMediaFormat = "AV_ClearTS",
                                                                                contentMediaFrameDuration = "3000",
                                                                                contentMediaID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "f",
                                                                                contentMediaRelativePath = Path.GetDirectoryName(Path.Combine(filesDB.UNC_BASE_PATH_DATA1, filesDB.FILE_NAME)).Substring(14).Replace("\\", "/") + "/",
                                                                                contentMediaSrcAssetType = "Clear_Asset_HD",
                                                                                contentMediaStorageDevice = "NAS169",
                                                                                contentMediaType = "Source",
                                                                                contentPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                                                contentRating = "1",
                                                                                contentStartDate = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                                contentStudio = "VTVcab",
                                                                                contentSubtitles = "",
                                                                                contentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                contentViCopyright = "",
                                                                                contentViDescription = filesDB.NOI_DUNG,
                                                                                contentViSynopsis = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                contentViTitle = ProgramName,
                                                                                contentYear = broadcastDate.Year.ToString(),

                                                                                imageAction = "override",
                                                                                imageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                imageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Main",
                                                                                imageIllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                                imageImageAction = "override",
                                                                                imageImageEncProfileName = "Image_transfer_HD1",
                                                                                imageImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                imageImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Child",
                                                                                imageImagePreLoaded = "false",
                                                                                imageImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                imageImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                imageMediaComment = "",
                                                                                imageMediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + ".jpg",
                                                                                imageMediaFileSize = "1",
                                                                                imageMediaFormat = "Image_Basic",
                                                                                imageMediaFrameDuration = "3000",
                                                                                imageMediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_BG_Media",
                                                                                imageMediaRelativePath = "AllImages/",
                                                                                imageMediaSrcAssetType = "Clear_Asset_HD",
                                                                                imageMediaStorageDevice = "WLA",
                                                                                imageMediaType = "Source",
                                                                                imageStartDate = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                                imageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                                image2Action = "override",
                                                                                image2ExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                image2ID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Main",
                                                                                image2IllustratedRef = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                                image2ImageAction = "override",
                                                                                image2ImageEncProfileName = "Image_transfer_HD1",
                                                                                image2ImageExpiryDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                image2ImageID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Child",
                                                                                image2ImagePreLoaded = "false",
                                                                                image2ImageStartdate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                image2ImageTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                                image2MediaComment = "",
                                                                                image2MediaFileName = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster.jpg",
                                                                                image2MediaFileSize = "1",
                                                                                image2MediaFormat = "Image_Basic",
                                                                                image2MediaFrameDuration = "3000",
                                                                                image2MediaID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "_Poster_Media",
                                                                                image2MediaRelativePath = "AllImages/",
                                                                                image2MediaSrcAssetType = "Clear_Asset_HD",
                                                                                image2MediaStorageDevice = "WLA",
                                                                                image2MediaType = "Source",
                                                                                image2StartDate = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                                image2Title = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                                vodItemContentRef = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "p",
                                                                                vodItemAction = "override",
                                                                                vodItemDisplayPriority = "LYS003582537/0",
                                                                                vodItemID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                                vodItemNodeRefList = "LYS003582537",
                                                                                vodItemPeriodEnd = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                                vodItemPeriodStart = broadcastDate.ToString("yyyy-MM-dd") + "T" + broadcastDate.ToString("HH:mm:ss") + "Z",
                                                                                vodItemTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                                productAction = "override",
                                                                                productCurrency = "VND",
                                                                                productElementId = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                                productElementKind = "VodItem",
                                                                                productID = "LYS000041271",
                                                                                productPrice = "0",
                                                                                productTitle = "AVOD",
                                                                                productType = "subscription"

                                                                            };
                                                                            View.XMLLongObject xmlObject = new View.XMLLongObject();
                                                                            xmlObject.GenerateXml(xmlChild);
                                                                            var temp = Path.Combine(txtSaveFolder.Text, OriginalFileName + ".xml");
                                                                            xmlObject.SaveXmlFile(temp);
                                                                            addLog(logFile, "Xuat xml sang unc path tu unc path thanh cong");
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            addLog(logFile, "Xuat xml sang unc path tu unc path ko thanh cong: " + ex.ToString());
                                                                        }
                                                                    #endregion
                                                                }
                                                                //#region tap 2 tro len phim bo
                                                                //else
                                                                //{
                                                                //    try
                                                                //    {
                                                                //        View.XMLShortChildObject xmlChild = new View.XMLShortChildObject()
                                                                //        {
                                                                //            rootID = "GLOBAL",
                                                                //            rootScheduleDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",

                                                                //            contentNumber = epiNum.ToString(),
                                                                //            contentSeriesRef = Utils.ConvertToVietnameseNonSign(ProgramName.Remove(ProgramName.LastIndexOf(epiNum.ToString()))).Replace(" ", "").ToLower() + "s",
                                                                //            contentAction = "override",
                                                                //            contentActors = filesDB.ACTOR,
                                                                //            contentAspect = filesDB.ASPECT_RATIO,
                                                                //            contentCategories = "",
                                                                //            contentContentAction = "override",
                                                                //            contentContentDefinition = filesDB.VIDEO_FORMAT,
                                                                //            contentContentEncProfileName = "",
                                                                //            contentContentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "p",
                                                                //            contentContentPreLoaded = "false",
                                                                //            contentContentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                //            contentContentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                //            contentCountries = "",
                                                                //            contentDirectors = filesDB.DIRECTOR,
                                                                //            contentDuration = ((int)TimeSpan.Parse(filesDB.TC_OUT).TotalSeconds).ToString(),
                                                                //            contentID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "m",
                                                                //            contentIsRecordable = "0",
                                                                //            contentLanguage = "",
                                                                //            contentMediaAction = "override",
                                                                //            contentMediaFilename = Path.GetFileName(filesDB.FILE_NAME),
                                                                //            contentMediaFileSize = filesDB.FILE_SIZE.ToString(),
                                                                //            contentMediaFormat = "AV_ClearTS",
                                                                //            contentMediaFrameDuration = "3000",
                                                                //            contentMediaID = Utils.ConvertToVietnameseNonSign(ProgramName).Replace(" ", "").ToLower() + "f",
                                                                //            contentMediaRelativePath = Path.GetDirectoryName(Path.Combine(filesDB.UNC_BASE_PATH_DATA3, filesDB.FILE_NAME)).Substring(14).Replace("\\", "/") + "/",
                                                                //            contentMediaSrcAssetType = "Clear_Asset_HD",
                                                                //            contentMediaStorageDevice = "NAS",
                                                                //            contentMediaType = "Source",
                                                                //            contentPromoImages = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").ToLower().Trim()) + ".jpg",
                                                                //            contentRating = "1",
                                                                //            contentStartDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                //            contentStudio = "VTVcab",
                                                                //            contentSubtitles = "",
                                                                //            contentTitle = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                //            contentViCopyright = "",
                                                                //            contentViDescription = filesDB.NOI_DUNG,
                                                                //            contentViSynopsis = Utils.ConvertToVietnameseNonSign(ProgramName),
                                                                //            contentViTitle = ProgramName,
                                                                //            contentYear = Convert.ToDateTime(filesDB.DATE_TO_BROADCAST).Year.ToString(),

                                                                //            vodItemContentRef = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "p",
                                                                //            vodItemAction = "override",
                                                                //            vodItemDisplayPriority = "LYS003582537/0",
                                                                //            vodItemID = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                //            vodItemNodeRefList = "LYS003582537",
                                                                //            vodItemPeriodEnd = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                //            vodItemPeriodStart = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "Z",
                                                                //            vodItemTitle = Utils.ConvertToVietnameseNonSign(ProgramName),

                                                                //            productAction = "override",
                                                                //            productCurrency = "VND",
                                                                //            productElementId = Utils.ConvertToVietnameseNonSign(ProgramName.Replace(" ", "").Trim()).ToLower() + "v",
                                                                //            productElementKind = "VodItem",
                                                                //            productID = "LYS000041271",
                                                                //            productPrice = "0",
                                                                //            productTitle = "AVOD",
                                                                //            productType = "subscription"

                                                                //        };
                                                                //        View.XMLShortObject xmlObject = new View.XMLShortObject();
                                                                //        xmlObject.GenerateXml(xmlChild);
                                                                //        var temp = Path.Combine(txtSaveFolder.Text, OriginalFileName + ".xml");
                                                                //        xmlObject.SaveXmlFile(temp);
                                                                //        addLog(logFile, "Xuat xml sang unc path tu unc path thanh cong");
                                                                //    }
                                                                //    catch (Exception ex)
                                                                //    {
                                                                //        addLog(logFile, "Xuat xml sang unc path tu unc path ko thanh cong: " + ex.ToString());
                                                                //    }
                                                                //}
                                                                //#endregion
                                                            }
                                                            #endregion

                                                            #region Export Excel
                                                            if (ckExcel.Checked)
                                                                {
                                                                    try
                                                                    {
                                                                        string tempProgramName = ProgramName.Replace(" ", "").Trim();
                                                                        var index = Utils.ConvertToVietnameseNonSign(tempProgramName).LastIndexOf("Tap");
                                                                        var tapStr = index >= 0 ? tempProgramName.Substring(index).Substring(3).Trim() : " ";
                                                                        if (tapStr.IndexOf(' ') > 0)
                                                                            tapStr = tapStr.Substring(0, tapStr.IndexOf(' '));
                                                                        tapStr = index >= 0 ? "Tap" + tapStr : " ";
                                                                        string temp = Path.Combine(txtSaveFolder.Text, OriginalFileName + ".xlsx");
                                                                        List<View.exObj> lstEx = new List<View.exObj>();
                                                                        View.exObj ex = new View.exObj()
                                                                        {
                                                                            auto_play_succesor = Utils.ConvertToVietnameseNonSign(tempProgramName),
                                                                            externalReference = Utils.ConvertToVietnameseNonSign(tempProgramName).ToLower(),
                                                                            parent = Utils.ConvertToVietnameseNonSign(tempProgramName).ToLower(),
                                                                            thumbnail = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "").ToLower() + ".jpg",
                                                                            media_file = Utils.ConvertToVietnameseNonSign(tempProgramName) + ".mp4",
                                                                            image_file_name1 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + "_Poster.jpg",
                                                                            image_file_name2 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + ".jpg",
                                                                            movideo_params_required1 = Utils.ConvertToVietnameseNonSign(tempProgramName) + ".mp4",
                                                                            movideo_params_required2 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + ".jpg",
                                                                            movideo_params_required3 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + "_Poster.jpg",
                                                                            movideo_params_required4 = Utils.ConvertToVietnameseNonSign(tempProgramName).Replace(tapStr, "") + ".jpg",
                                                                            movideo_params_required5 = Utils.ConvertToVietnameseNonSign(tempProgramName) + ".srt",
                                                                            media_duration = filesDB.TC_OUT,
                                                                            linkTranscode = tempHighres,
                                                                            tagProfileId = filesDB.TYPE,
                                                                            media_description = filesDB.NOI_DUNG,
                                                                            product_description = filesDB.NOI_DUNG,
                                                                        };
                                                                        lstEx.Add(ex);
                                                                        View.infoXlsx InfoObj = new View.infoXlsx();
                                                                        InfoObj.DataSource = lstEx;
                                                                        InfoObj.CreateDocument();
                                                                        InfoObj.ExportToXlsx(temp);
                                                                        addLog(logFile, "Xuat excel sang unc path tu unc path thanh cong");
                                                                    }
                                                                    catch (Exception ex) { addLog(logFile, "Xuat excel sang unc path tu unc path ko thanh cong: " + ex.ToString()); }
                                                                }
                                                            #endregion

                                                            #region copy highres
                                                            if (ckMediaHighres.Checked)
                                                                {
                                                                //unc
                                                                bool uncSuccess = false;
                                                                    try
                                                                    {
                                                                        var t = Task.Run(() =>
                                                                        {
                                                                            uncSuccess = FCopy(uncHighresPath + tempHighres, Path.Combine(txtSaveFolder.Text, OriginalFileName + ".mxf"));
                                                                        });
                                                                        t.Wait();
                                                                        if (uncSuccess)
                                                                        {
                                                                        //Ghi log
                                                                        addLog(logFile, "Xuat highres sang unc path tu unc path thanh cong");
                                                                        }
                                                                        else
                                                                        {
                                                                            addLog(logFile, "Xuat highres sang unc path tu unc path ko thanh cong");
                                                                        }
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        uncSuccess = false;
                                                                    //Ghi log
                                                                    addLog(logFile, "Xuat highres sang unc path tu unc path ko thanh cong: " + ex.ToString());
                                                                    }
                                                                //ftp
                                                                if (!uncSuccess)
                                                                    {
                                                                        try
                                                                        {
                                                                            var tempSourcePath = srcPath + nasHighresPath;
                                                                            using (WebClient ftpClient = new WebClient())
                                                                            {
                                                                                ftpClient.Credentials = new NetworkCredential(filesDB.USERNAME, filesDB.PASSWORD);
                                                                                var t = Task.Run(() =>
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        ftpClient.DownloadFile(tempSourcePath, Path.Combine(txtSaveFolder.Text, OriginalFileName + ".mxf"));
                                                                                    }
                                                                                    catch (Exception ex) { addLog(logFile, "Loi xuat highres sang unc path tu ftp path: " + ex.ToString()); }
                                                                                });
                                                                                t.Wait();
                                                                            //Ghi log
                                                                            addLog(logFile, "Xuat highres sang unc path tu ftp path thanh cong");
                                                                            }
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                        //Ghi log
                                                                        addLog(logFile, "Xuat highres sang unc path tu ftp path ko thanh cong: " + ex.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            #endregion

                                                            #region copy lowres
                                                            if (ckMediaLowres.Checked)
                                                                {
                                                                //unc
                                                                bool uncSuccess = false;
                                                                    try
                                                                    {
                                                                        var t = Task.Run(() =>
                                                                        {
                                                                            uncSuccess = FCopy(uncLowresPath + tempLowres, Path.Combine(txtSaveFolder.Text, OriginalFileName + ".mp4"));
                                                                        });
                                                                        t.Wait();
                                                                        if (uncSuccess)
                                                                        {
                                                                        //Ghi log
                                                                        addLog(logFile, "Xuat lowres sang unc path tu unc path thanh cong");
                                                                        }
                                                                        else
                                                                        {
                                                                            addLog(logFile, "Xuat lowres sang unc path tu unc path ko thanh cong");
                                                                        }
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        uncSuccess = false;
                                                                    //Ghi log
                                                                    addLog(logFile, "Xuat lowres sang unc path tu unc path ko thanh cong: " + ex.ToString());
                                                                    }
                                                                //ftp
                                                                if (!uncSuccess)
                                                                    {
                                                                        try
                                                                        {
                                                                            var tempSourcePath = srcPath + nasLowresPath;
                                                                            using (WebClient ftpClient = new WebClient())
                                                                            {
                                                                                ftpClient.Credentials = new NetworkCredential(filesDB.USERNAME, filesDB.PASSWORD);
                                                                                var t = Task.Run(() =>
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        ftpClient.DownloadFile(tempSourcePath, Path.Combine(txtSaveFolder.Text, OriginalFileName + ".mp4"));
                                                                                    }
                                                                                    catch (Exception ex)
                                                                                    {
                                                                                        addLog(logFile, "Loi khi xuat lowres sang unc path tu ftp path: " + ex.ToString());
                                                                                    }
                                                                                });
                                                                                t.Wait();
                                                                            //Ghi log
                                                                            addLog(logFile, "Xuat lowres sang unc path tu ftp path thanh cong");
                                                                            }
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                        //Ghi log
                                                                        addLog(logFile, "Xuat lowres sang unc path tu ftp path ko thanh cong: " + ex.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            #endregion

                                                            #region Export ảnh
                                                            if (ckFullPic.Checked)
                                                                {
                                                                    try
                                                                    {
                                                                        var picDb = db.Query<View.PosterObj>(sqlQueryPic, new { id_clip = exportDB.ID_CLIP, file_type = 11 }).FirstOrDefault();
                                                                        var picPathDb = db.Query<DAO.INFORTAPE_FILE_TYPE>(@"select * from INFORTAPE_FILE_TYPE where ID = 11").FirstOrDefault();
                                                                        if (picDb != null && picPathDb != null)
                                                                        {
                                                                        //unc
                                                                        bool uncSuccess = false;
                                                                            try
                                                                            {
                                                                                File.Copy("\\\\" + picDb.NAS_IP + "\\" + picPathDb.NAS_DATA_PATH + "\\" + picDb.FILE_NAME, Path.Combine(txtSaveFolder.Text, OriginalFileName + "." + Path.GetExtension(picDb.FILE_NAME)));
                                                                                uncSuccess = true;
                                                                            //Ghi log
                                                                            addLog(logFile, "Xuat poster sang unc path tu unc path thanh cong");
                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                uncSuccess = false;
                                                                            //Ghi log
                                                                            addLog(logFile, "Xuat poster sang unc path tu unc path ko thanh cong: " + ex.ToString());
                                                                            }
                                                                        //ftp
                                                                        if (!uncSuccess)
                                                                            {
                                                                                try
                                                                                {
                                                                                    using (WebClient ftpClient = new WebClient())
                                                                                    {
                                                                                        ftpClient.Credentials = new NetworkCredential(picDb.USERNAME, picDb.PASSWORD);
                                                                                        ftpClient.DownloadFile("ftp://" + picDb.NAS_IP + ":" + picDb.PORT + "/" + picPathDb.NAS_DATA_PATH + "/" + picDb.FILE_NAME, Path.Combine(txtSaveFolder.Text, OriginalFileName + "." + Path.GetExtension(picDb.FILE_NAME)));
                                                                                    //Ghi log
                                                                                    addLog(logFile, "Xuat poster sang unc path tu ftp path thanh cong");
                                                                                    }
                                                                                }
                                                                                catch (Exception ex)
                                                                                {
                                                                                //Ghi log
                                                                                addLog(logFile, "Xuat poster sang unc path tu ftp path ko thanh cong: " + ex.ToString());
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        addLog(logFile, "Xuat poster sang unc path ko thanh cong: " + ex.ToString());
                                                                    }
                                                                #endregion
                                                            }

                                                            }
                                                            catch (Exception ex) { addLog(logFile, "Loi khi export tu nas sang unc path: " + ex.ToString()); }
                                                        }
                                                    #endregion
                                                }
                                                    else
                                                    {
                                                        addLog(logFile, "Khong tim duoc file trong he thong");
                                                    }
                                                    db.Execute(@"Update EXPORT_JOB set STATUS = 2, LAST_UPDATE = @start_time where ID_CLIP = @id_clip and STATUS = 1", new { start_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id_clip = exportDB.ID_CLIP });
                                                }
                                                catch (Exception ex)
                                                {
                                                    addLog(logFile, "Loi khi thuc hien task " + i.ToString() + ": " + ex.ToString());
                                                }
                                            });
                                        }
                                        else
                                        {
                                            addLog(logFile, "Khong co file can xuat");
                                        }
                                        i++;
                                    }
                                    try
                                    {
                                        Task.WaitAll(tasks);
                                    }
                                    catch (Exception ex)
                                    {
                                        addLog(logFile, "Loi khi Wait All tasks: " + ex.ToString());
                                    }
                                }
                                else
                                {
                                    addLog(logFile, "Khong co file can xuat");
                                }
                            }
                            else
                            {
                                addLog(logFile, "Chua co lenh Query EXPORT_JOB");
                            }
                        }
                        catch (Exception ex)
                        {
                            addLog(logFile, "Loi khi ket noi db: " + ex.ToString());
                        }
                    }

                }
                catch (Exception ex)
                {
                    addLog(logFile, "Loi ko xac dinh: " + ex.ToString());
                }
            }
        }
        private void ckEditMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEditMode.Checked)
            {
                ckEditMode.Text = "Đang bật chế độ sửa cấu hình";
                groupControl1.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                ckEditMode.Text = "Đã tắt chế độ sửa cấu hình";
                groupControl1.Enabled = false;
                btnSave.Enabled = false;
            }
        }
        private string getNumber(string str)
        {
            var number = System.Text.RegularExpressions.Regex.Match(str, @"\d+").Value;

            return number;
        }
        private int getEpisodeNUmber(string programName)
        {
            return System.Text.RegularExpressions.Regex.Match(programName, @"\d+$").Value == "" ? 0 : int.Parse(System.Text.RegularExpressions.Regex.Match(programName, @"\d+$").Value);
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.Activate();
            this.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
            this.WindowState = FormWindowState.Normal;
        }
        /// <summary>
        /// Export file từ ftp server đến ftp server khác
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="targetFileName"></param>
        /// <param name="sourceURI"></param>
        /// <param name="sourceUser"></param>
        /// <param name="sourcePass"></param>
        /// <param name="targetURI"></param>
        /// <param name="targetUser"></param>
        /// <param name="targetPass"></param>
        /// <returns></returns>
        public bool copyFile(string fileName, string targetFileName, string sourceURI, string sourceUser, string sourcePass, string targetURI, string targetUser, string targetPass)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sourceURI + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(sourceUser, sourcePass);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                bool temp = Upload(targetFileName, ToByteArray(responseStream), targetURI, targetUser, targetPass);
                responseStream.Close();
                if (temp)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                addLog(logFile, "Loi trong copyFile: " + ex.ToString());
                return false;
            }
        }

        public Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        public bool Upload(string FileName, byte[] Image, string targetURI, string targetUser, string targetPass)
        {
            try
            {
                FtpWebRequest clsRequest = (FtpWebRequest)WebRequest.Create(targetURI + FileName);
                clsRequest.Credentials = new NetworkCredential(targetUser, targetPass);
                clsRequest.Method = WebRequestMethods.Ftp.UploadFile;
                Stream clsStream = clsRequest.GetRequestStream();
                clsStream.Write(Image, 0, Image.Length);
                clsStream.Close();
                clsStream.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                addLog(logFile, "Loi trong Upload: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Upload file từ đường dẫn local  (unc) lên FTP Server
        /// </summary>
        /// <param name="inputFilePath">Đường dẫn file muốn upload</param>
        /// <param name="targetFileName">Tên file muốn đặt khi upload xong</param>
        /// <param name="nasIP"></param>
        /// <param name="nasPort"></param>
        /// <param name="nasPath"></param>
        /// <param name="nasUsername"></param>
        /// <param name="nasPassword"></param>
        public bool uploadFromUnc(string inputFilePath, string targetFileName, string nasIP, string nasPort, string nasPath, string nasUsername, string nasPassword)
        {
            try
            {
                string ftpfullpath = "ftp://" + nasIP + ":" + nasPort + nasPath + targetFileName;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(nasUsername, nasPassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream fs = File.OpenRead(inputFilePath);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                return true;
            }
            catch (Exception ex)
            {
                addLog(logFile, "Loi trong uploadFromUnc: " + ex.ToString());
                return false;
            }
        }
        private string getTimeNow()
        {
            return DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss");
        }

        private void addLog(string filePath, string content)
        {
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filePath, true))
                {
                    file.WriteLine(" -\n - " + getTimeNow() + ":" + content + "\n");
                }
            }
            catch //(Exception ex)
            {
                //HDMessageBox.Show(ex.ToString());
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Clicks == 0)
            {
                popupMenu1.ShowPopup(MousePosition);
            }
        }

        private void barBtnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Show();
            this.Activate();
            this.WindowState = FormWindowState.Normal;
        }

        private void barBtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Copy file từ đường dẫn source sang đường dẫn destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private bool FCopy(string source, string destination)
        {
            int array_length = (int)Math.Pow(2, 19);
            byte[] dataArray = new byte[array_length];
            try
            {
                using (FileStream fsread = new FileStream
                (source, FileMode.Open, FileAccess.Read, FileShare.None, array_length))
                {
                    using (BinaryReader bwread = new BinaryReader(fsread))
                    {
                        using (FileStream fswrite = new FileStream
                        (destination, FileMode.Create, FileAccess.Write, FileShare.None, array_length))
                        {
                            using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                            {
                                for (;;)
                                {
                                    int read = bwread.Read(dataArray, 0, array_length);
                                    if (0 == read)
                                        break;
                                    bwwrite.Write(dataArray, 0, read);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                addLog(logFile, "Loi trong FCopy: " + ex.ToString());
                return false;
            }
            return true;
        }
    }
}
