namespace HDExcelConverter
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.txtImport = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExport = new DevExpress.XtraEditors.TextEdit();
            this.btnDone = new DevExpress.XtraEditors.SimpleButton();
            this.btnChooseImport = new DevExpress.XtraEditors.SimpleButton();
            this.btnChooseExport = new DevExpress.XtraEditors.SimpleButton();
            this.bsConfig = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // txtImport
            // 
            this.txtImport.Location = new System.Drawing.Point(16, 40);
            this.txtImport.Name = "txtImport";
            this.txtImport.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImport.Properties.Appearance.Options.UseFont = true;
            this.txtImport.Size = new System.Drawing.Size(546, 24);
            this.txtImport.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thư mục Import:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Thư mục Export:";
            // 
            // txtExport
            // 
            this.txtExport.Location = new System.Drawing.Point(15, 98);
            this.txtExport.Name = "txtExport";
            this.txtExport.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExport.Properties.Appearance.Options.UseFont = true;
            this.txtExport.Size = new System.Drawing.Size(547, 24);
            this.txtExport.TabIndex = 2;
            // 
            // btnDone
            // 
            this.btnDone.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Appearance.Options.UseFont = true;
            this.btnDone.Location = new System.Drawing.Point(291, 138);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(112, 42);
            this.btnDone.TabIndex = 4;
            this.btnDone.Text = "Xong";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnChooseImport
            // 
            this.btnChooseImport.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseImport.Appearance.Options.UseFont = true;
            this.btnChooseImport.Location = new System.Drawing.Point(568, 37);
            this.btnChooseImport.Name = "btnChooseImport";
            this.btnChooseImport.Size = new System.Drawing.Size(89, 30);
            this.btnChooseImport.TabIndex = 6;
            this.btnChooseImport.Text = "Chọn...";
            this.btnChooseImport.Click += new System.EventHandler(this.btnChooseImport_Click);
            // 
            // btnChooseExport
            // 
            this.btnChooseExport.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseExport.Appearance.Options.UseFont = true;
            this.btnChooseExport.Location = new System.Drawing.Point(568, 92);
            this.btnChooseExport.Name = "btnChooseExport";
            this.btnChooseExport.Size = new System.Drawing.Size(89, 30);
            this.btnChooseExport.TabIndex = 7;
            this.btnChooseExport.Text = "Chọn...";
            this.btnChooseExport.Click += new System.EventHandler(this.btnChooseExport_Click);
            // 
            // bsConfig
            // 
            this.bsConfig.DataSource = typeof(HDExcelConverter.View.Config);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(680, 202);
            this.Controls.Add(this.btnChooseExport);
            this.Controls.Add(this.btnChooseImport);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "Cấu hình phần mềm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtExport;
        private DevExpress.XtraEditors.SimpleButton btnDone;
        private System.Windows.Forms.BindingSource bsConfig;
        private DevExpress.XtraEditors.SimpleButton btnChooseImport;
        private DevExpress.XtraEditors.SimpleButton btnChooseExport;
    }
}