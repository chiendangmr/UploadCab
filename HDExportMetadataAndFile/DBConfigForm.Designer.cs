namespace HDExportMetadataAndFile
{
    partial class DBConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBConfigForm));
            this.btnDone = new DevExpress.XtraEditors.SimpleButton();
            this.txtSQLQuery = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSQLQueryFiles = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSQLQueryPic = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.bsDBConfig = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLQueryFiles.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLQueryPic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDBConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDone.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.btnDone.Appearance.Options.UseFont = true;
            this.btnDone.Location = new System.Drawing.Point(261, 232);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(107, 46);
            this.btnDone.TabIndex = 5;
            this.btnDone.Text = "Xong";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQLQuery.Location = new System.Drawing.Point(26, 45);
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtSQLQuery.Properties.Appearance.Options.UseFont = true;
            this.txtSQLQuery.Size = new System.Drawing.Size(602, 24);
            this.txtSQLQuery.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Location = new System.Drawing.Point(26, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 17);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "SQLQuery(*):";
            // 
            // txtSQLQueryFiles
            // 
            this.txtSQLQueryFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQLQueryFiles.Location = new System.Drawing.Point(30, 108);
            this.txtSQLQueryFiles.Name = "txtSQLQueryFiles";
            this.txtSQLQueryFiles.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtSQLQueryFiles.Properties.Appearance.Options.UseFont = true;
            this.txtSQLQueryFiles.Size = new System.Drawing.Size(602, 24);
            this.txtSQLQueryFiles.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl3.Location = new System.Drawing.Point(30, 85);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(110, 17);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "SQLQueryFiles(*):";
            // 
            // txtSQLQueryPic
            // 
            this.txtSQLQueryPic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQLQueryPic.Location = new System.Drawing.Point(30, 178);
            this.txtSQLQueryPic.Name = "txtSQLQueryPic";
            this.txtSQLQueryPic.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtSQLQueryPic.Properties.Appearance.Options.UseFont = true;
            this.txtSQLQueryPic.Size = new System.Drawing.Size(602, 24);
            this.txtSQLQueryPic.TabIndex = 13;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl5.Location = new System.Drawing.Point(30, 155);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(110, 17);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "SQLQueryPicture:";
            // 
            // bsDBConfig
            // 
            this.bsDBConfig.DataSource = typeof(HDExportMetadataAndFile.View.DBCommandObj);
            // 
            // DBConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 292);
            this.Controls.Add(this.txtSQLQueryPic);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtSQLQueryFiles);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtSQLQuery);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnDone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DBConfigForm";
            this.Text = "Cấu hình phần mềm";
            this.Load += new System.EventHandler(this.DBConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLQueryFiles.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLQueryPic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDBConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnDone;
        private System.Windows.Forms.BindingSource bsDBConfig;
        private DevExpress.XtraEditors.TextEdit txtSQLQuery;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSQLQueryFiles;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSQLQueryPic;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}