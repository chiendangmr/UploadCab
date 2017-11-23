namespace HDExcelConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bsExcelInfo = new System.Windows.Forms.BindingSource();
            this.gvInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExcelInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnImport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Appearance.Options.UseFont = true;
            this.btnImport.Location = new System.Drawing.Point(676, 41);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(113, 32);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Nhập file Excel ...";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConfig,
            this.btnAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1132, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnConfig
            // 
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(67, 20);
            this.btnConfig.Text = "Cấu hình";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(63, 20);
            this.btnAbout.Text = "Trợ giúp";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.bsExcelInfo;
            this.gridControl1.Location = new System.Drawing.Point(12, 100);
            this.gridControl1.MainView = this.gvInfo;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1108, 533);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInfo});
            // 
            // bsExcelInfo
            // 
            this.bsExcelInfo.DataSource = typeof(HDExcelConverter.View.exObj);
            // 
            // gvInfo
            // 
            this.gvInfo.ActiveFilterEnabled = false;
            this.gvInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colField,
            this.colDetail});
            this.gvInfo.GridControl = this.gridControl1;
            this.gvInfo.Name = "gvInfo";
            this.gvInfo.OptionsView.ShowGroupPanel = false;
            // 
            // colField
            // 
            this.colField.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colField.AppearanceHeader.Options.UseFont = true;
            this.colField.AppearanceHeader.Options.UseTextOptions = true;
            this.colField.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colField.Caption = "Trường Metadata";
            this.colField.FieldName = "Field";
            this.colField.Name = "colField";
            this.colField.OptionsColumn.AllowEdit = false;
            this.colField.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colField.OptionsColumn.ReadOnly = true;
            this.colField.OptionsFilter.AllowAutoFilter = false;
            this.colField.OptionsFilter.AllowFilter = false;
            this.colField.Visible = true;
            this.colField.VisibleIndex = 0;
            this.colField.Width = 320;
            // 
            // colDetail
            // 
            this.colDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDetail.AppearanceHeader.Options.UseFont = true;
            this.colDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDetail.Caption = "Thông tin chương trình";
            this.colDetail.FieldName = "Detail";
            this.colDetail.Name = "colDetail";
            this.colDetail.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDetail.OptionsFilter.AllowFilter = false;
            this.colDetail.Visible = true;
            this.colDetail.VisibleIndex = 1;
            this.colDetail.Width = 910;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(372, 659);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(152, 53);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu file Excel";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFileName.Location = new System.Drawing.Point(372, 46);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Properties.Appearance.Options.UseFont = true;
            this.txtFileName.Size = new System.Drawing.Size(298, 22);
            this.txtFileName.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(313, 47);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 18);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Tên file:";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.Location = new System.Drawing.Point(546, 659);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(152, 53);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Xuất XML";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 741);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "HDVietnam - Chuyển đổi định dạng file";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExcelInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnImport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnConfig;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInfo;
        private System.Windows.Forms.BindingSource bsExcelInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colField;
        private DevExpress.XtraGrid.Columns.GridColumn colDetail;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnExport;
    }
}

