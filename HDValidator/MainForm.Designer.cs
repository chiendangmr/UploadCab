namespace HDValidator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFiles = new DevExpress.XtraEditors.TextEdit();
            this.txtFolder = new DevExpress.XtraEditors.TextEdit();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.grdFiles = new DevExpress.XtraGrid.GridControl();
            this.bsFiles = new System.Windows.Forms.BindingSource(this.components);
            this.gvFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnChooseFolder = new DevExpress.XtraEditors.SimpleButton();
            this.btnChooseFiles = new DevExpress.XtraEditors.SimpleButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtFiles.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn (nhiều) file:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label2.Location = new System.Drawing.Point(26, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chọn thư mục:";
            // 
            // txtFiles
            // 
            this.txtFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiles.Location = new System.Drawing.Point(138, 41);
            this.txtFiles.Name = "txtFiles";
            this.txtFiles.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtFiles.Properties.Appearance.Options.UseFont = true;
            this.txtFiles.Size = new System.Drawing.Size(526, 24);
            this.txtFiles.TabIndex = 2;
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(138, 84);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtFolder.Properties.Appearance.Options.UseFont = true;
            this.txtFolder.Size = new System.Drawing.Size(526, 24);
            this.txtFolder.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStart.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.btnStart.Appearance.Options.UseFont = true;
            this.btnStart.Location = new System.Drawing.Point(255, 123);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 54);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Bắt đầu";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // grdFiles
            // 
            this.grdFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFiles.DataSource = this.bsFiles;
            this.grdFiles.Location = new System.Drawing.Point(0, 192);
            this.grdFiles.MainView = this.gvFiles;
            this.grdFiles.Name = "grdFiles";
            this.grdFiles.Size = new System.Drawing.Size(874, 208);
            this.grdFiles.TabIndex = 5;
            this.grdFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFiles});
            // 
            // bsFiles
            // 
            this.bsFiles.DataSource = typeof(HDValidator.View.FileView);
            // 
            // gvFiles
            // 
            this.gvFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvFiles.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gvFiles.GridControl = this.grdFiles;
            this.gvFiles.Name = "gvFiles";
            this.gvFiles.OptionsBehavior.Editable = false;
            this.gvFiles.OptionsBehavior.ReadOnly = true;
            this.gvFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvFiles.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvFiles.OptionsView.ShowGroupPanel = false;
            this.gvFiles.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvFiles_RowCellClick);
            this.gvFiles.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvFiles_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "File";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 539;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Trạng thái";
            this.gridColumn2.FieldName = "Status";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 317;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnRefresh);
            this.panelControl1.Controls.Add(this.btnChooseFolder);
            this.panelControl1.Controls.Add(this.btnChooseFiles);
            this.panelControl1.Controls.Add(this.btnStart);
            this.panelControl1.Controls.Add(this.txtFiles);
            this.panelControl1.Controls.Add(this.txtFolder);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.menuStrip1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(874, 195);
            this.panelControl1.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRefresh.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.btnRefresh.Appearance.Options.UseFont = true;
            this.btnRefresh.Location = new System.Drawing.Point(433, 123);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 54);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseFolder.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnChooseFolder.Appearance.Options.UseFont = true;
            this.btnChooseFolder.Location = new System.Drawing.Point(700, 77);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(100, 28);
            this.btnChooseFolder.TabIndex = 6;
            this.btnChooseFolder.Text = "Chọn...";
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // btnChooseFiles
            // 
            this.btnChooseFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseFiles.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.btnChooseFiles.Appearance.Options.UseFont = true;
            this.btnChooseFiles.Location = new System.Drawing.Point(700, 39);
            this.btnChooseFiles.Name = "btnChooseFiles";
            this.btnChooseFiles.Size = new System.Drawing.Size(100, 28);
            this.btnChooseFiles.TabIndex = 5;
            this.btnChooseFiles.Text = "Chọn...";
            this.btnChooseFiles.Click += new System.EventHandler(this.btnChooseFiles_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbout});
            this.menuStrip1.Location = new System.Drawing.Point(2, 2);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(870, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(63, 20);
            this.btnAbout.Text = "Trợ giúp";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Chọn...";
            // 
            // rtbReport
            // 
            this.rtbReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbReport.Location = new System.Drawing.Point(2, 30);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.Size = new System.Drawing.Size(867, 246);
            this.rtbReport.TabIndex = 7;
            this.rtbReport.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label3.Location = new System.Drawing.Point(5, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Thông tin file:";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.label3);
            this.panelControl2.Controls.Add(this.rtbReport);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 394);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(874, 281);
            this.panelControl2.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 675);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.grdFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "HDVietnam - Kiểm tra định dạng file";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFiles.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtFiles;
        private DevExpress.XtraEditors.TextEdit txtFolder;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraGrid.GridControl grdFiles;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFiles;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnChooseFolder;
        private DevExpress.XtraEditors.SimpleButton btnChooseFiles;
        private System.Windows.Forms.BindingSource bsFiles;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.RichTextBox rtbReport;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
    }
}

