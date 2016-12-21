using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientReport
{
    public class BaseReport : Telerik.Reporting.Report
    {
        public BaseReport()
        {
            this.ItemDataBinding += BaseReport_ItemDataBinding;
        }

        protected void BaseReport_ItemDataBinding(object sender, EventArgs e)
        {
            try
            {
                //var rp = sender as Telerik.Reporting.Processing.Report;
                //if (rp != null && rp.Report.Parameters.ContainsKey("KeySession"))
                //{
                //    var str = rp.Report.Parameters["KeySession"].Value.ToString();
                //    Thread.SetData(Thread.GetNamedDataSlot("KeySession"), str);
                    
                //}
            }
            catch { }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // BaseReport
            // 
            this.Name = "BaseReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
