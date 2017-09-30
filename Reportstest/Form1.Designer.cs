namespace Reportstest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，则为 True；否则为 False。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer 生成的代码

        /// <summary>
        /// Designer 支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.webbasicDataSet = new Reportstest.webbasicDataSet();
            this.AccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AccountTableAdapter = new Reportstest.webbasicDataSetTableAdapters.AccountTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.webbasicDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.AccountBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reportstest.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 356);
            this.reportViewer1.TabIndex = 0;
            // 
            // webbasicDataSet
            // 
            this.webbasicDataSet.DataSetName = "webbasicDataSet";
            this.webbasicDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AccountBindingSource
            // 
            this.AccountBindingSource.DataMember = "Account";
            this.AccountBindingSource.DataSource = this.webbasicDataSet;
            // 
            // AccountTableAdapter
            // 
            this.AccountTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 356);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webbasicDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AccountBindingSource;
        private webbasicDataSet webbasicDataSet;
        private webbasicDataSetTableAdapters.AccountTableAdapter AccountTableAdapter;

        private webbasicDataSetTableAdapters.Menu_SelectTableAdapter Menu_SelectTableAdapter;
    }
}

