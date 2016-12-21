namespace ClientReport
{
    partial class rpDemo
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            this.detail = new Telerik.Reporting.DetailSection();
            this.actionNameDataTextBox = new Telerik.Reporting.TextBox();
            this.codeDataTextBox = new Telerik.Reporting.TextBox();
            this.isApprovedDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataMember = "Demo";
            this.objectDataSource1.DataSource = typeof(ClientReport.ReportData);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.3333001136779785D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.actionNameDataTextBox,
            this.codeDataTextBox,
            this.isApprovedDataTextBox,
            this.textBox1,
            this.pictureBox1});
            this.detail.Name = "detail";
            // 
            // actionNameDataTextBox
            // 
            this.actionNameDataTextBox.CanGrow = false;
            this.actionNameDataTextBox.CanShrink = false;
            this.actionNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.0666500329971314D));
            this.actionNameDataTextBox.Name = "actionNameDataTextBox";
            this.actionNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.actionNameDataTextBox.Value = "= Fields.ActionName";
            // 
            // codeDataTextBox
            // 
            this.codeDataTextBox.CanGrow = false;
            this.codeDataTextBox.CanShrink = false;
            this.codeDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.4666500091552734D));
            this.codeDataTextBox.Name = "codeDataTextBox";
            this.codeDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.codeDataTextBox.Value = "= Fields.Code";
            // 
            // isApprovedDataTextBox
            // 
            this.isApprovedDataTextBox.CanGrow = false;
            this.isApprovedDataTextBox.CanShrink = false;
            this.isApprovedDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.8666499853134155D));
            this.isApprovedDataTextBox.Name = "isApprovedDataTextBox";
            this.isApprovedDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.isApprovedDataTextBox.Value = "= Fields.IsApproved";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(0.40000009536743164D));
            this.textBox1.Value = "= Parameters.Address.Value";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8999999761581421D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.pictureBox1.MimeType = "";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.pictureBox1.Value = "= Parameters.Image";
            // 
            // rpDemo
            // 
            this.DataSource = this.objectDataSource1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "rpDemo";
            this.PageSettings.ColumnCount = 2;
            this.PageSettings.ColumnSpacing = Telerik.Reporting.Drawing.Unit.Inch(0.1875D);
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.15620000660419464D), Telerik.Reporting.Drawing.Unit.Inch(0.1559063047170639D), Telerik.Reporting.Drawing.Unit.Inch(0.53119999170303345D), Telerik.Reporting.Drawing.Unit.Inch(0.46850642561912537D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "UserName";
            reportParameter2.AllowNull = true;
            reportParameter2.Name = "Address";
            reportParameter3.AllowNull = true;
            reportParameter3.Name = "Copyright";
            reportParameter4.Name = "Image";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(4D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource objectDataSource1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox actionNameDataTextBox;
        private Telerik.Reporting.TextBox codeDataTextBox;
        private Telerik.Reporting.TextBox isApprovedDataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.PictureBox pictureBox1;

    }
}