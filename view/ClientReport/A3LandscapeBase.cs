namespace ClientReport
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for A3LandscapeBase.
    /// </summary>
    public partial class A3LandscapeBase : BaseReport
    {
        public A3LandscapeBase()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.ItemDataBound += Report_ItemDataBound;
        }

        protected void Report_ItemDataBound(object sender, EventArgs e)
        {
            try
            {
                var rp = sender as Telerik.Reporting.Processing.Report;
                if (rp != null)
                {
                    string companyname = (rp.Report.Parameters.ContainsKey("CompanyName")) ? "" : rp.Report.Parameters["CompanyName"].Value.ToString();
                    string address = (rp.Report.Parameters.ContainsKey("Address")) ? "" : rp.Report.Parameters["Address"].Value.ToString();
                    string copyright = (rp.Report.Parameters.ContainsKey("Copyright")) ? "" : rp.Report.Parameters["Copyright"].Value.ToString();
                    string logo = (rp.Report.Parameters.ContainsKey("Logo")) ? "" : rp.Report.Parameters["Logo"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}