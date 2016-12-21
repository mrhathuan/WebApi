using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using DTO;
using CacheManager.Core;
using System.Web;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Presentation;
using Microsoft.SqlServer;
using System.IO;
using OfficeOpenXml;
using IServices;
using OfficeOpenXml.Style;
using System.Drawing;
using System.ServiceModel;
using System.Reflection;

namespace ClientWeb
{
    public class FINController : BaseController
    {
        #region FINRefresh
         [HttpPost]
        public FINFreightAudit FINFreightAuditRead()
        {
            try
            {
                var result = new FINFreightAudit();
                result.TOMasterID = 1;
                result.Credit = 12345;
                return result;
            }
             catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public FINRefresh FINRefresh_List(dynamic dynParam)
        {
            try
            {
                var result = new FINRefresh();
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_List(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void FINRefresh_RefreshRoute_Order(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());

                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINRefresh_RefreshRoute_Order(dtfrom, dtto, lstID);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void FINRefresh_RefreshRoute_TO(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());

                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINRefresh_RefreshRoute_TO(dtfrom, dtto, lstID);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string FINRefresh_Refresh(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dt = Convert.ToDateTime(dynParam.date.ToString());
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());

                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_Refresh(dt, lstID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpPost]
        public List<DTOCATContract> FINRefresh_Contract_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATContract>();
                int cusID = (int)dynParam.cusID;
                int serID = (int)dynParam.serID;
                int transID = (int)dynParam.transID;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_Contract_List(cusID, serID, transID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATContract> FINRefresh_Contract_Master_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATContract>();
                int vendorid = (int)dynParam.vendorid;
                int transID = (int)dynParam.transID;
                int? serID = (int?)dynParam.serID;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_Contract_Master_List(vendorid, serID, transID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSRouting> FINRefresh_Routing_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCUSRouting>();
                int cusID = (int)dynParam.cusID;
                int? contractID = (int?)dynParam.contractID;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_Routing_List(cusID, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOContractTerm> FINRefresh_ContractTerm_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOContractTerm>();
                int contractID = (int)dynParam.contractID;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_ContractTerm_List(contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSRouting> FINRefresh_Routing_Master_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCUSRouting>();
                int vendorid = (int)dynParam.vendorid;
                int? contractID = (int?)dynParam.contractID;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_Routing_Master_List(vendorid, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATRouting> FINRefresh_OPSRouting_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATRouting>();
                int cusID = (int)dynParam.cusID;
                int? contractID = (int?)dynParam.contractID;
                int? routingID = (int?)dynParam.routingID;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_OPSRouting_List(routingID,cusID, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOCATRouting> FINRefresh_OPSGroupRouting_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATRouting>();
                int vendorID = (int)dynParam.vendorid;
                int? contractID = (int?)dynParam.contractID;
                int? routingID = (int?)dynParam.routingID;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_OPSGroupRouting_List(routingID, vendorID, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult FINRefresh_ORD_Group_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_ORD_Group_List(request, from.Date, to.Date.AddDays(1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult FINRefresh_ORD_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_ORD_Container_List(request, from.Date, to.Date.AddDays(1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINRefresh_ORD_Group_Save(dynamic dynParam)
        {
            try
            {
                FINRefresh_ORDGroup item = Newtonsoft.Json.JsonConvert.DeserializeObject<FINRefresh_ORDGroup>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINRefresh_ORD_Group_Save(item);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void FINRefresh_ORD_Container_Save(dynamic dynParam)
        {
            try
            {
                FINRefresh_ORDContainer item = Newtonsoft.Json.JsonConvert.DeserializeObject<FINRefresh_ORDContainer>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINRefresh_ORD_Container_Save(item);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult FINRefresh_OPS_Group_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_OPS_Group_List(request, from.Date, to.Date.AddDays(1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult FINRefresh_OPS_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_OPS_Container_List(request, from.Date, to.Date.AddDays(1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINRefresh_OPS_Group_Save(dynamic dynParam)
        {
            try
            {
                FINRefresh_OPSGroup item = Newtonsoft.Json.JsonConvert.DeserializeObject<FINRefresh_OPSGroup>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINRefresh_OPS_Group_Save(item);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINRefresh_OPS_Container_Save(dynamic dynParam)
        {
            try
            {
                FINRefresh_OPSContainer item = Newtonsoft.Json.JsonConvert.DeserializeObject<FINRefresh_OPSContainer>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINRefresh_OPS_Container_Save(item);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult FINRefresh_OPS_Master_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINRefresh_OPS_Master_List(request, from, to);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINRefresh_OPS_Master_Save(dynamic dynParam)
        {
            try
            {
                FINRefresh_OPSMaster item = Newtonsoft.Json.JsonConvert.DeserializeObject<FINRefresh_OPSMaster>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINRefresh_OPS_Master_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #region FIN FreightAudit

        #region container
        [HttpPost]
        public DTOResult FINFreightAuditCON_List(dynamic dynParam)
        {

            try
            {
                var result = new DTOResult();
                DateTime dtfrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.DateTo.ToString());
                int statusID = Convert.ToInt32(dynParam.statusID.ToString());
                //string request = Convert.ToDateTime(dynParam.item.request.ToString());
                string request = dynParam.request.ToString();
                bool IsOwner = (bool)dynParam.IsOwner;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCON_List(dtfrom, dtto, statusID, IsOwner, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FINFreightAuditCON_Detail FINFreightAudit_CONDetail_List(dynamic dynParam)
        {
            try
            {
                var result = new FINFreightAuditCON_Detail();
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAudit_CONDetail_List(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCON_Reject(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCON_Reject(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCON_Accept(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCON_Accept(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCON_Waiting(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCON_Waiting(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCON_Approved(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCON_Approved(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        [HttpPost]
        public DTOResult FINFreightAudit_List(dynamic dynParam)
        {
            
            try
            {
                var result = new DTOResult();
                DateTime dtfrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.DateTo.ToString());
                int statusID = Convert.ToInt32(dynParam.statusID.ToString());
                //string request = Convert.ToDateTime(dynParam.item.request.ToString());
                string request = dynParam.request.ToString();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAudit_List(dtfrom, dtto, statusID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FINFreightAudit_Detail FINFreightAudit_DetailList(dynamic dynParam)
        {
            try
            {
                var result = new FINFreightAudit_Detail();
                int masterid = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAudit_DetailList(masterid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> FINFreightAudit_StatusList()
        {
            
            try
            {
                var result = new List<SYSVar>();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAudit_StatusList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAudit_Reject(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAudit_Reject(lst,Note);
                });
                return true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAudit_Accept(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAudit_Accept(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAudit_Waiting(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAudit_Waiting(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAudit_Approved(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAudit_Approved(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string FIN_FreightAudit_Export(dynamic dynParam)
        {
            try
            {
                int statusID = (int)dynParam.statusID;
                DateTime pDateFrom = Convert.ToDateTime(dynParam.pDateFrom.ToString());
                DateTime pDateTo = Convert.ToDateTime(dynParam.pDateTo.ToString());

                List<FINFreightAudit_Export> resBody = new List<FINFreightAudit_Export>();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    resBody = sv.FINFreightAudit_Export(pDateFrom, pDateTo, statusID);
                });

                string file = "/Uploads/temp/FreightAudit_Status" + statusID + "_From" + pDateFrom.ToString("yyyyMMdd") + "_To" + pDateTo.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(f))
                {
                    #region Sheet 1
                    ExcelWorksheet worksheet1 = pk.Workbook.Worksheets.Add("Đối chiếu nhà vận tải");
                    int col1 = 0, row1 = 1; 

                    col1++; worksheet1.Cells[row1, col1].Value = "STT"; worksheet1.Column(col1).Width = 5;
                    col1++; worksheet1.Cells[row1, col1].Value = "Trạng thái"; worksheet1.Column(col1).Width = 15;
                    col1++; worksheet1.Cells[row1, col1].Value = "Số chuyến"; worksheet1.Column(col1).Width = 15;
                    col1++; worksheet1.Cells[row1, col1].Value = "Ngày"; worksheet1.Column(col1).Width = 15;
                    col1++; worksheet1.Cells[row1, col1].Value = "Doanh thu"; worksheet1.Column(col1).Width = 15;
                    col1++; worksheet1.Cells[row1, col1].Value = "Chi phí"; worksheet1.Column(col1).Width = 15;

                    ExcelHelper.CreateCellStyle(worksheet1, row1, 1, row1, col1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet1.Cells[1, 1, row1, col1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[1, 1, row1, col1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet1.Cells[1, 1, row1, col1].Style.Border.Right.Color.SetColor(Color.White);
                    #endregion

                    #region Sheet 2
                    ExcelWorksheet worksheet2 = pk.Workbook.Worksheets.Add("Doanh thu vận chuyển");
                    int col2 = 0, row2 = 1;

                    col2++; worksheet2.Cells[row2, col2].Value = "STT"; worksheet2.Column(col2).Width = 5;
                    col2++; worksheet2.Cells[row2, col2].Value = "Số chuyến"; worksheet2.Column(col2).Width = 15;
                    col2++; worksheet2.Cells[row2, col2].Value = "Ngày tính"; worksheet2.Column(col2).Width = 15;
                    col2++; worksheet2.Cells[row2, col2].Value = "STT Doanh thu"; worksheet2.Column(col2).Width = 15;
                    col2++; worksheet2.Cells[row2, col2].Value = "Khoản thu"; worksheet2.Column(col2).Width = 40;
                    col2++; worksheet2.Cells[row2, col2].Value = "Hàng hóa"; worksheet2.Column(col2).Width = 20;
                    col2++; worksheet2.Cells[row2, col2].Value = "Đơn vị tính"; worksheet2.Column(col2).Width = 20;
                    col2++; worksheet2.Cells[row2, col2].Value = "Số lượng"; worksheet2.Column(col2).Width = 15;
                    col2++; worksheet2.Cells[row2, col2].Value = "Đơn giá"; worksheet2.Column(col2).Width = 15;
                    col2++; worksheet2.Cells[row2, col2].Value = "Thành tiền"; worksheet2.Column(col2).Width = 15;
                    col2++; worksheet2.Cells[row2, col2].Value = "Điểm lấy hàng"; worksheet2.Column(col2).Width = 20;
                    col2++; worksheet2.Cells[row2, col2].Value = "Điểm giao hàng"; worksheet2.Column(col2).Width = 20;

                    ExcelHelper.CreateCellStyle(worksheet2, row2, 1, row2, col2 , false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet2.Cells[1, 1, row2, col2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[1, 1, row2, col2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet2.Cells[1, 1, row2, col2].Style.Border.Right.Color.SetColor(Color.White);
                    #endregion

                    #region Sheet 3
                    ExcelWorksheet worksheet3 = pk.Workbook.Worksheets.Add("Doanh thu khác");
                    int col3 = 0, row3 = 1;

                    col3++; worksheet3.Cells[row3, col3].Value = "STT"; worksheet3.Column(col3).Width = 5;
                    col3++; worksheet3.Cells[row3, col3].Value = "Số chuyến"; worksheet3.Column(col3).Width = 15;
                    col3++; worksheet3.Cells[row3, col3].Value = "Ngày tính"; worksheet3.Column(col3).Width = 15;
                    col3++; worksheet3.Cells[row3, col3].Value = "STT Doanh thu"; worksheet3.Column(col3).Width = 15;
                    col3++; worksheet3.Cells[row3, col3].Value = "Khoản thu"; worksheet3.Column(col3).Width = 15;
                    col3++; worksheet3.Cells[row3, col3].Value = "Thành tiền"; worksheet3.Column(col3).Width = 15;

                    ExcelHelper.CreateCellStyle(worksheet3, row3, 1, row3, col3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet3.Cells[1, 1, row3, col3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet3.Cells[1, 1, row3, col3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet3.Cells[1, 1, row3, col3].Style.Border.Right.Color.SetColor(Color.White);
                    #endregion

                    #region Sheet 4
                    ExcelWorksheet worksheet4 = pk.Workbook.Worksheets.Add("Chi phí phát sinh");
                    int col4 = 0, row4 = 1;

                    col4++; worksheet4.Cells[row4, col4].Value = "STT"; worksheet4.Column(col4).Width = 5;
                    col4++; worksheet4.Cells[row4, col4].Value = "Số chuyến"; worksheet4.Column(col4).Width = 15;
                    col4++; worksheet4.Cells[row4, col4].Value = "Ngày tính"; worksheet4.Column(col4).Width = 15;
                    col4++; worksheet4.Cells[row4, col4].Value = "STT Chi phí"; worksheet4.Column(col4).Width = 15;
                    col4++; worksheet4.Cells[row4, col4].Value = "Tên chi phí"; worksheet4.Column(col4).Width = 30;
                    col4++; worksheet4.Cells[row4, col4].Value = "Chi phí chịu"; worksheet4.Column(col4).Width = 15;
                    col4++; worksheet4.Cells[row4, col4].Value = "Ghi chú"; worksheet4.Column(col4).Width = 40;
                    col4++; worksheet4.Cells[row4, col4].Value = "Trạng thái"; worksheet4.Column(col4).Width = 15;

                    ExcelHelper.CreateCellStyle(worksheet4, row4, 1, row4, col4, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet4.Cells[1, 1, row4, col4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet4.Cells[1, 1, row4, col4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet4.Cells[1, 1, row4, col4].Style.Border.Right.Color.SetColor(Color.White);
                    #endregion

                    #region Body

                    int stt1 = 0, stt2 = 0, stt3 = 0, stt4 = 0;
                    row1 = 1;row2 = 2;row3 = 2;row4 = 2;
                    
                    foreach (var item in resBody)
                    {
                        #region Body Sheet 1
                        row1++; col1 = 0; stt1++; stt2++; stt3++; stt4++;
                        col1++; worksheet1.Cells[row1, col1].Value = stt1;
                        col1++; worksheet1.Cells[row1, col1].Value = item.StatusName;
                        col1++; worksheet1.Cells[row1, col1].Value = item.TOMasterCode;
                        col1++; worksheet1.Cells[row1, col1].Value = item.DateConfig.ToString("dd/MM/yyyy");
                        col1++; worksheet1.Cells[row1, col1].Value = item.Credit;
                        ExcelHelper.CreateFormat(worksheet1, row1, col1, ExcelHelper.FormatMoney);
                        col1++; worksheet1.Cells[row1, col1].Value = item.Debit;
                        ExcelHelper.CreateFormat(worksheet1, row1, col1, ExcelHelper.FormatMoney);
                        #endregion

                        #region Body Sheet 2
                        col2 = 0;
                        col2++; worksheet2.Cells[row2, col2].Value = stt2;
                        col2++; worksheet2.Cells[row2, col2].Value = item.TOMasterCode;
                        col2++; worksheet2.Cells[row2, col2].Value = item.DateConfig.ToString("dd/MM/yyyy");
                        ExcelHelper.CreateCellStyle(worksheet2, row2, 1, row2, 12, false, true, "#F4B084", ExcelHelper.ColorBlack, 0, "");
                        row2++;
                        if (item.lstCredit == null || item.lstCredit.Count() == 0)
                        {
                            row2++;
                        }
                        int sttdt = 0;
                        foreach (var credit in item.lstCredit)
                        {
                            sttdt++;
                            col2++; worksheet2.Cells[row2, col2].Value = sttdt;
                            col2++; worksheet2.Cells[row2, col2].Value = credit.CostName;
                            col2++; worksheet2.Cells[row2, col2].Value = credit.GroupOfProductNameCUS;
                            col2++; worksheet2.Cells[row2, col2].Value = credit.Unit;
                            col2++; worksheet2.Cells[row2, col2].Value = credit.Quantity;
                            ExcelHelper.CreateFormat(worksheet2, row2, col2, ExcelHelper.FormatNumber);
                            col2++; worksheet2.Cells[row2, col2].Value = credit.UnitPrice;
                            ExcelHelper.CreateFormat(worksheet2, row2, col2, ExcelHelper.FormatMoney);
                            col2++; worksheet2.Cells[row2, col2].Value =  credit.Cost;
                            ExcelHelper.CreateFormat(worksheet2, row2, col2, ExcelHelper.FormatMoney);
                            col2++; worksheet2.Cells[row2, col2].Value = credit.LocationFromName;
                            col2++; worksheet2.Cells[row2, col2].Value = credit.LocationToName;

                            row2++; col2 = 3;
                        }
                        #endregion

                        //#region Body Sheet 3
                        //col3 = 0;
                        //col3++; worksheet3.Cells[row3, col3].Value = stt3;
                        //col3++; worksheet3.Cells[row3, col3].Value = item.TOMasterCode;
                        //col3++; worksheet3.Cells[row3, col3].Value = item.DateConfig.ToString("dd/MM/yyyy");
                        //ExcelHelper.CreateCellStyle(worksheet3, row3, 1, row3, 6, false, true, "#F4B084", ExcelHelper.ColorBlack, 0, "");
                        //row3++;
                        //if (item.lstOther == null || item.lstOther.Count() == 0)
                        //{
                        //    row3++;
                        //}
                        //int sttother = 0;
                        //foreach (var other in item.lstOther)
                        //{
                        //    sttother++;
                        //    col3++; worksheet3.Cells[row3, col3].Value = sttother;
                        //    col3++; worksheet3.Cells[row3, col3].Value = other.CostName;
                        //    ExcelHelper.CreateFormat(worksheet3, row3, col3, ExcelHelper.FormatNumber);
                        //    col3++; worksheet3.Cells[row3, col3].Value = other.Cost;
                        //    ExcelHelper.CreateFormat(worksheet3, row3, col3, ExcelHelper.FormatNumber);

                        //    row3++; col3 = 3;
                        //}
                        //#endregion

                        #region Body Sheet 4
                        col4 = 0;
                        col4++; worksheet4.Cells[row4, col4].Value = stt4;
                        col4++; worksheet4.Cells[row4, col4].Value = item.TOMasterCode;
                        col4++; worksheet4.Cells[row4, col4].Value = item.DateConfig.ToString("dd/MM/yyyy");
                        ExcelHelper.CreateCellStyle(worksheet4, row4, 1, row4, 8, false, true, "#F4B084", ExcelHelper.ColorBlack, 0, "");
                        row4++;
                        if (item.lstTrouble == null || item.lstTrouble.Count() == 0)
                        {
                            row4++;
                        }
                        int stttrouble = 0;
                        foreach (var trouble in item.lstTrouble)
                        {
                            //stttrouble++;
                            //col4++; worksheet4.Cells[row4, col4].Value = stttrouble;
                            //col4++; worksheet4.Cells[row4, col4].Value = trouble.GroupOfTroubleName;
                            //col4++; worksheet4.Cells[row4, col4].Value = trouble.CostOfVendor;
                            //ExcelHelper.CreateFormat(worksheet4, row4, col4, ExcelHelper.FormatNumber);
                            //col4++; worksheet4.Cells[row4, col4].Value = trouble.Description;
                            //col4++; worksheet4.Cells[row4, col4].Value = trouble.TroubleCostStatusName;

                            //row4++; col4 = 3;
                        }
                        #endregion
                    }
                    #endregion

                    
                    pk.Save();
                }

                return file;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FIN FreightAudit CusCO
        [HttpPost]
        public DTOResult FINFreightAuditCus_OrderCON_List(dynamic dynParam)
        {

            try
            {
                var result = new DTOResult();
                DateTime dtfrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.DateTo.ToString());
                int statusID = Convert.ToInt32(dynParam.statusID.ToString());
                string request = dynParam.request.ToString();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_OrderCON_List(dtfrom, dtto, statusID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FINFreightAuditCus_OrderCON_Detail FINFreightAuditCus_OrderCONDetail_List(dynamic dynParam)
        {
            try
            {
                var result = new FINFreightAuditCus_OrderCON_Detail();
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_OrderCONDetail_List(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_OrderCON_Reject(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_OrderCON_Reject(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_OrderCON_Accept(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_OrderCON_Accept(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOContractTerm> FINFreightAudit_ContractTerm_List(dynamic dynParam)
        {
            try
            {

                int contractID = (int)dynParam.contractID;
                List<DTOContractTerm> result = new List<DTOContractTerm>();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAudit_ContractTerm_List(contractID);
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSRouting> FINFreightAudit_Routing_List(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                int contractID = (int)dynParam.contractID;
                int termID = (int)dynParam.termID;
                List<DTOCUSRouting> result = new List<DTOCUSRouting>();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAudit_Routing_List(customerID, contractID, termID);
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINFreightAuditCus_OrderCONDetail_Save(dynamic dynParam)
        {
            try
            {

                FINFreightAuditCus_OrderCON_Detail item = Newtonsoft.Json.JsonConvert.DeserializeObject<FINFreightAuditCus_OrderCON_Detail>(dynParam.item.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_OrderCONDetail_Save(item);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_OrderCON_Approved(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_OrderCON_Approved(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<SYSVar> FINFreightAuditCus_OrderCON_StatusList()
        {
            try
            {

                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_OrderCON_StatusList();
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FIN FreightAudit Cus

        [HttpPost]
        public DTOResult FINFreightAuditCus_Order_List(dynamic dynParam)
        {

            try
            {
                var result = new DTOResult();
                DateTime dtfrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.DateTo.ToString());
                int statusID = Convert.ToInt32(dynParam.statusID.ToString());
                string request = dynParam.request.ToString();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_Order_List(dtfrom, dtto, statusID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FINFreightAuditCus_Order_Detail FINFreightAuditCus_Order_DetailList(dynamic dynParam)
        {
            try
            {
                var result = new FINFreightAuditCus_Order_Detail();
                int masterid = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_Order_DetailList(masterid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FINFreightAuditCus_Order_Detail FINFreightAuditCus_OrderDetail_List(dynamic dynParam)
        {
            try
            {
                var result = new FINFreightAuditCus_Order_Detail();
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_OrderDetail_List(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> FINFreightAuditCus_Order_StatusList()
        {

            try
            {
                var result = new List<SYSVar>();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_Order_StatusList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_Order_Reject(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_Order_Reject(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_Order_Accept(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_Order_Accept(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_Order_Approved(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_Order_Approved(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult FINFreightAuditCus_Group_List(dynamic dynParam)
        {

            try
            {
                var result = new DTOResult();
                DateTime dtfrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.DateTo.ToString());
                int statusID = Convert.ToInt32(dynParam.statusID.ToString());
                //string request = Convert.ToDateTime(dynParam.item.request.ToString());
                string request = dynParam.request.ToString();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_Group_List(dtfrom, dtto, statusID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FINFreightAuditCus_Group_Detail FINFreightAuditCus_Group_DetailList(dynamic dynParam)
        {
            try
            {
                var result = new FINFreightAuditCus_Group_Detail();
                int masterid = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_Group_DetailList(masterid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> FINFreightAuditCus_Group_StatusList()
        {

            try
            {
                var result = new List<SYSVar>();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINFreightAuditCus_Group_StatusList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_Group_Reject(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_Group_Reject(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_Group_Accept(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_Group_Accept(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FINFreightAuditCus_Group_Approved(dynamic dynParam)
        {
            try
            {

                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString()); ;
                string Note = dynParam.Note;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_Group_Approved(lst, Note);
                });
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ManualFix
        [HttpPost]
        public DTOResult FINManualFix_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                DateTime pDateFrom = Convert.ToDateTime(dynParam.pDateFrom.ToString());
                DateTime pDateTo = Convert.ToDateTime(dynParam.pDateTo.ToString());
                string request = dynParam.request.ToString();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINManualFix_List(pDateFrom, pDateTo, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINManualFix_Save(dynamic dynParam)
        {
            try
            {
                DTOFINManualFix item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFINManualFix>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINManualFix_Save(item);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINManualFix_Delete(dynamic dynParam)
        {
            try
            {
                DTOFINManualFix item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFINManualFix>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                    {
                        sv.FINManualFix_Delete(item);
                    });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult FINManualFix_ChooseList(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime pDateFrom = Convert.ToDateTime(dynParam.pDateFrom.ToString());
                DateTime pDateTo = Convert.ToDateTime(dynParam.pDateTo.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINManualFix_ChooseList(request, pDateFrom, pDateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINManualFix_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOFINManualFix> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOFINManualFix>>(dynParam.lst.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINManualFix_SaveList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region FINSettingManual
        [HttpPost]
        public DTOResult FINSettingManual_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINSettingManual_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOFINSettingManual FINSettingManual_Get(dynamic dynParam)
        {
            try
            {
                DTOFINSettingManual result = new DTOFINSettingManual();
                int id = (int)dynParam.id;
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.FINSettingManual_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void FINSettingManual_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOFINSettingManual item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFINSettingManual>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINSettingManual_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void FINSettingManual_Delete(dynamic dynParam)
        {
            try
            {
                DTOFINSettingManual item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFINSettingManual>(dynParam.item.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINSettingManual_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Excel
        [HttpPost]
        public string FINManualFix_ExcelDownload(dynamic dynParam)
        {
            try
            {
                string file = "";
                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOFINSettingManual objSetting = new DTOFINSettingManual();
                List<DTOFINManualFixImport> data = new List<DTOFINManualFixImport>();

                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    data = sv.FINManualFix_ExcelData(dtfrom, dtto, customerID);
                    objSetting = sv.FINSettingManual_Get(templateID);
                });

                string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "DITOGroupProductStatusPODName", "DITOGroupProductStatusPODID", "VehicleID", "IsNew" };
                List<string> sValue = new List<string>(aValue);

                Dictionary<string, string> dicName = FINManualFix_GetDataName();

                if (objSetting != null)
                {
                    file = "/Uploads/temp/" + objSetting.Name.Replace(' ', '-') + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                    file = file.Replace("+", "");
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                    FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                    using (ExcelPackage package = new ExcelPackage(exportfile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(objSetting.Name);
                       
                        if (objSetting.RowStart > 1)
                        {
                            Dictionary<string, int> dicPos = new Dictionary<string, int>();

                            #region header
                            int row = 1;
                            foreach (var prop in objSetting.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSetting, null);
                                        if (v > 0)
                                        {
                                            if (dicName.ContainsKey(p))
                                                worksheet.Cells[row, v].Value = dicName[p];
                                            else
                                                worksheet.Cells[row, v].Value = p;

                                            dicPos.Add(p, v);
                                            worksheet.Column(v).Width = 20;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            #endregion

                            row = 2;
                            foreach (var item in data)
                            {
                                int type = 0;
                                foreach (var pos in dicPos)
                                {
                                    try
                                    {
                                        var property = pos.Key;
                                        var value = GetObjProperty(item, property, ref type);
                                        worksheet.Cells[row, pos.Value].Value = value;
                                        if (type == 1) //Loại datetime
                                        {
                                            if (property == "DateFromLoadStart" || property == "DateFromLoadEnd" || property == "DateToLoadStart" || property == "DateToLoadEnd")
                                            {
                                                ExcelHelper.CreateFormat(worksheet, row, pos.Value, ExcelHelper.FormatHHMM);
                                            }
                                            else if (property == "MasterETDDatetime" || property == "OrderGroupETDDatetime")
                                            {
                                                ExcelHelper.CreateFormat(worksheet, row, pos.Value, ExcelHelper.FormatDMYHM);
                                            }
                                            else
                                            {
                                                ExcelHelper.CreateFormat(worksheet, row, pos.Value, ExcelHelper.FormatDDMMYYYY);
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }

                                row++;
                            }
                        }

                        package.Save();
                    }
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetObjProperty(object obj, string property, ref int type)
        {
            Type t = obj.GetType();
            PropertyInfo p = t.GetProperty(property);
            var value = p.GetValue(obj, null);
            if (t.Equals(typeof(bool)))
            {
                bool flag = (bool)value;
                if (flag == true)
                {
                    value = "x";
                }
                else
                {
                    value = "";
                }
            }

            type = 0;
            if(value != null)
            {
                Type t1 = value.GetType();
                if (t1.Equals(typeof(DateTime)))
                {
                    type = 1;
                }
                else if (t1.Equals(typeof(bool)))
                {
                    bool flag = (bool)value;
                    if (flag == true)
                    {
                        value = "x";
                    }
                }
            }

            return value;
        }

        [HttpPost]
        public void FINManualFix_ExcelImport(dynamic dynParam)
        {
            try
            {
                int templateID = (int)dynParam.TemplateID;

                List<DTOFINManualFixImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOFINManualFixImport>>(dynParam.Data.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINManualFix_ExcelImport(templateID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFINManualFixImport> FINManualFix_ExcelCheck(dynamic dynParam)
        {
            try
            {
                string file = "/" + dynParam.file.ToString();

                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOFINSettingManual objSetting = new DTOFINSettingManual();
                List<DTOFINManualFixImport> data = new List<DTOFINManualFixImport>();

                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    data = sv.FINManualFix_ExcelData(dtfrom, dtto, customerID);
                    objSetting = sv.FINSettingManual_Get(templateID);
                });



                var dataRes = new List<DTOFINManualFixImport>();


                if (objSetting != null)
                {
                    //Check các required.

                    string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "VehicleID", "IsNew", "VENLoadCodeID", "VENUnLoadCodeID" };

                    var sValue = new List<string>(aValue);

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                for (row = objSetting.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var obj = new DTOFINManualFixImport();
                                    var lstError = new List<string>();

                                    var excelInput = FINManualFix_GetDataValue(worksheet, objSetting, row, sValue);
                                    if (excelInput.Count(c => !string.IsNullOrEmpty(c.Value)) > 0)
                                    {
                                        #region check dữ liệu
                                        var ID = excelInput["ID"];

                                        obj.CustomerCode = excelInput["CustomerCode"];
                                        obj.OrderCode = excelInput["OrderCode"];
                                        obj.DNCode = excelInput["DNCode"];
                                        obj.SOCode = excelInput["SOCode"];
                                        obj.LocationFromCode = excelInput["LocationFromCode"];
                                        obj.LocationToCode = excelInput["LocationToCode"];
                                        var RequestDate = excelInput["RequestDate"];
                                        if (!string.IsNullOrEmpty(RequestDate))
                                        {
                                            try
                                            {
                                                obj.RequestDate = ExcelHelper.ValueToDate(RequestDate);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.RequestDate = Convert.ToDateTime(RequestDate);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày gửi y/c [" + RequestDate + "] không chính xác");
                                                }

                                            }
                                        }
                                        else
                                            obj.RequestDate = null;

                                        var IsInvoiceStr = excelInput["IsInvoice"];
                                        if (IsInvoiceStr.ToLower().Trim() == "x")
                                        {
                                            obj.IsInvoice = true;
                                        }
                                        else
                                        {
                                            obj.IsInvoice = false;
                                        }

                                        var DateFromCome = excelInput["DateFromCome"];
                                        if (!string.IsNullOrEmpty(DateFromCome))
                                        {
                                            try
                                            {
                                                obj.DateFromCome = ExcelHelper.ValueToDate(DateFromCome);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateFromCome = Convert.ToDateTime(DateFromCome);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày đến kho [" + DateFromCome + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateFromCome = null;

                                        var DateFromLoadStart = excelInput["DateFromLoadStart"];
                                        if (!string.IsNullOrEmpty(DateFromLoadStart) && obj.DateFromCome != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateFromLoadStart, out time))
                                            {
                                                lstError.Add("Thời gian vào máng [" + DateFromLoadStart + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateFromCome.Value.Date + time;
                                                obj.DateFromLoadStart = temp;
                                            }
                                        }
                                        else obj.DateFromLoadStart = null;

                                        var DateFromLeave = excelInput["DateFromLeave"];

                                        if (!string.IsNullOrEmpty(DateFromLeave))
                                        {
                                            try
                                            {
                                                obj.DateFromLeave = ExcelHelper.ValueToDate(DateFromLeave);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateFromLeave = Convert.ToDateTime(DateFromLeave);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày rời kho [" + DateFromLeave + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateFromLeave = null;

                                        var DateFromLoadEnd = excelInput["DateFromLoadEnd"];
                                        if (!string.IsNullOrEmpty(DateFromLoadEnd) && obj.DateFromLeave != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateFromLoadEnd, out time))
                                            {
                                                lstError.Add("Thời gian ra máng [" + DateFromLoadEnd + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateFromLeave.Value.Date + time;
                                                obj.DateFromLoadEnd = temp;
                                            }
                                        }
                                        else obj.DateFromLoadEnd = null;

                                        var DateToCome = excelInput["DateToCome"];
                                        if (!string.IsNullOrEmpty(DateToCome))
                                        {
                                            try
                                            {
                                                obj.DateToCome = ExcelHelper.ValueToDate(DateToCome);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateToCome = Convert.ToDateTime(DateToCome);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày đến NPP [" + DateToCome + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateToCome = null;

                                        var DateToLoadStart = excelInput["DateToLoadStart"];
                                        if (!string.IsNullOrEmpty(DateToLoadStart) && obj.DateToCome != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateToLoadStart, out time))
                                            {
                                                lstError.Add("Thời gian ra máng [" + DateToLoadStart + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateToCome.Value.Date + time;
                                                obj.DateToLoadStart = temp;
                                            }
                                        }
                                        else obj.DateToLoadStart = null;

                                        var DateToLeave = excelInput["DateToLeave"];
                                        if (!string.IsNullOrEmpty(DateToLeave))
                                        {
                                            try
                                            {
                                                obj.DateToLeave = ExcelHelper.ValueToDate(DateToLeave);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateToLeave = Convert.ToDateTime(DateToLeave);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày rời NPP [" + DateToLeave + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateToLeave = null;

                                        var DateToLoadEnd = excelInput["DateToLoadEnd"];
                                        if (!string.IsNullOrEmpty(DateToLoadEnd) && obj.DateToLeave != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateToLoadEnd, out time))
                                            {
                                                lstError.Add("Thời gian ra máng [" + DateToLoadEnd + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateToLeave.Value.Date + time;
                                                obj.DateToLoadEnd = temp;
                                            }
                                        }
                                        else obj.DateToLoadEnd = null;

                                        //obj.InvoiceBy = excelInput["InvoiceBy"];

                                        var InvoiceDate = excelInput["InvoiceDate"];
                                        if (!string.IsNullOrEmpty(InvoiceDate))
                                        {
                                            try
                                            {
                                                obj.InvoiceDate = ExcelHelper.ValueToDate(InvoiceDate);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                                                }
                                                catch
                                                {
                                                    lstError.Add("InvoiceDate [" + InvoiceDate + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.InvoiceDate = null;

                                        var InvoiceReturnDate = excelInput["InvoiceReturnDate"];
                                        if (!string.IsNullOrEmpty(InvoiceReturnDate))
                                        {
                                            try
                                            {
                                                obj.InvoiceReturnDate = ExcelHelper.ValueToDate(InvoiceReturnDate);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.InvoiceReturnDate = Convert.ToDateTime(InvoiceReturnDate);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày chứng từ trả về [" + InvoiceReturnDate + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.InvoiceReturnDate = null;

                                        obj.InvoiceNote = excelInput["InvoiceNote"];
                                        obj.Note = excelInput["Note"];
                                        obj.OPSGroupNote1 = excelInput["OPSGroupNote1"];
                                        obj.OPSGroupNote2 = excelInput["OPSGroupNote2"];
                                        obj.ORDGroupNote1 = excelInput["ORDGroupNote1"];
                                        obj.ORDGroupNote2 = excelInput["ORDGroupNote2"];
                                        obj.TOMasterNote1 = excelInput["TOMasterNote1"];
                                        obj.TOMasterNote2 = excelInput["TOMasterNote2"];
                                        obj.ChipNo = excelInput["ChipNo"];
                                        obj.Temperature = excelInput["Temperature"];

                                        var Ton = excelInput["Ton"];
                                        var CBM = excelInput["CBM"];
                                        var Quantity = excelInput["Quantity"];
                                        var Kg = excelInput["Kg"];

                                        //if (string.IsNullOrEmpty(Ton) && string.IsNullOrEmpty(CBM) && string.IsNullOrEmpty(Quantity))
                                        //{
                                        //    lstError.Add("Tấn, Khối, Số lượng phải có ít nhất 1 cột không được trống");
                                        //}

                                        if (!string.IsNullOrEmpty(Ton))
                                        {
                                            try
                                            {
                                                obj.Ton = Convert.ToDouble(Ton);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn [" + Ton + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.Ton > 0)
                                                obj.Ton = 0;
                                        }


                                        if (!string.IsNullOrEmpty(CBM))
                                        {
                                            try
                                            {
                                                obj.CBM = Convert.ToDouble(CBM);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối [" + CBM + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBM > 0)
                                                obj.CBM = 0;
                                        }

                                        if (!string.IsNullOrEmpty(Quantity))
                                        {
                                            try
                                            {
                                                obj.Quantity = Convert.ToDouble(Quantity);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng [" + Quantity + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.Quantity > 0)
                                                obj.Quantity = 0;
                                        }

                                        if (!string.IsNullOrEmpty(Kg))
                                        {
                                            try
                                            {
                                                obj.Kg = Convert.ToDouble(Kg);
                                            }
                                            catch
                                            {
                                                lstError.Add("Kg [" + Kg + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.Kg > 0)
                                                obj.Kg = 0;
                                        }

                                        var TonTranfer = excelInput["TonTranfer"];
                                        var CBMTranfer = excelInput["CBMTranfer"];
                                        var QuantityTranfer = excelInput["QuantityTranfer"];
                                        var KgTranfer = excelInput["KgTranfer"];

                                        //if (string.IsNullOrEmpty(TonTranfer) && string.IsNullOrEmpty(CBMTranfer) && string.IsNullOrEmpty(QuantityTranfer))
                                        //{
                                        //    lstError.Add("Tấn lấy, Khối lấy, Số lượng lấy phải có ít nhất 1 cột không được trống");
                                        //}

                                        if (!string.IsNullOrEmpty(TonTranfer))
                                        {
                                            try
                                            {
                                                obj.TonTranfer = Convert.ToDouble(TonTranfer);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn lấy [" + TonTranfer + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.TonTranfer > 0)
                                                obj.TonTranfer = 0;
                                        }


                                        if (!string.IsNullOrEmpty(CBMTranfer))
                                        {
                                            try
                                            {
                                                obj.CBMTranfer = Convert.ToDouble(CBMTranfer);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối lấy [" + CBMTranfer + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBMTranfer > 0)
                                                obj.CBMTranfer = 0;
                                        }

                                        if (!string.IsNullOrEmpty(QuantityTranfer))
                                        {
                                            try
                                            {
                                                obj.QuantityTranfer = Convert.ToDouble(QuantityTranfer);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng lấy [" + QuantityTranfer + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.QuantityTranfer > 0)
                                                obj.QuantityTranfer = 0;
                                        }

                                        if (!string.IsNullOrEmpty(KgTranfer))
                                        {
                                            try
                                            {
                                                obj.KgTranfer = Convert.ToDouble(KgTranfer);
                                            }
                                            catch
                                            {
                                                lstError.Add("Kg lấy [" + KgTranfer + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.KgTranfer > 0)
                                                obj.KgTranfer = 0;
                                        }

                                        var TonReturn = excelInput["TonReturn"];
                                        var CBMReturn = excelInput["CBMReturn"];
                                        var QuantityReturn = excelInput["QuantityReturn"];
                                        var KgReturn = excelInput["KgReturn"];

                                        if (!string.IsNullOrEmpty(TonReturn))
                                        {
                                            try
                                            {
                                                obj.TonReturn = Convert.ToDouble(TonReturn);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn trả về [" + TonReturn + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.TonReturn > 0)
                                                obj.TonReturn = 0;
                                        }


                                        if (!string.IsNullOrEmpty(CBMReturn))
                                        {
                                            try
                                            {
                                                obj.CBMReturn = Convert.ToDouble(CBMReturn);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối trả về [" + CBMReturn + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBMReturn > 0)
                                                obj.CBMReturn = 0;
                                        }

                                        if (!string.IsNullOrEmpty(QuantityReturn))
                                        {
                                            try
                                            {
                                                obj.QuantityReturn = Convert.ToDouble(QuantityReturn);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng trả về [" + QuantityReturn + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.QuantityReturn > 0)
                                                obj.QuantityReturn = 0;
                                        }

                                        if (!string.IsNullOrEmpty(KgReturn))
                                        {
                                            try
                                            {
                                                obj.KgReturn = Convert.ToDouble(KgReturn);
                                            }
                                            catch
                                            {
                                                lstError.Add("Kg trả về [" + KgReturn + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.KgReturn > 0)
                                                obj.KgReturn = 0;
                                        }

                                        var TonBBGN = excelInput["TonBBGN"];
                                        var CBMBBGN = excelInput["CBMBBGN"];
                                        var QuantityBBGN = excelInput["QuantityBBGN"];
                                        var KgBBGN = excelInput["KgBBGN"];

                                        if (!string.IsNullOrEmpty(TonBBGN))
                                        {
                                            try
                                            {
                                                obj.TonBBGN = Convert.ToDouble(TonBBGN);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn giao [" + TonBBGN + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.TonBBGN > 0)
                                                obj.TonBBGN = 0;
                                        }

                                        if (!string.IsNullOrEmpty(CBMBBGN))
                                        {
                                            try
                                            {
                                                obj.CBMBBGN = Convert.ToDouble(CBMBBGN);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối giao [" + CBMBBGN + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBMBBGN > 0)
                                                obj.CBMBBGN = 0;
                                        }

                                        if (!string.IsNullOrEmpty(QuantityBBGN))
                                        {
                                            try
                                            {
                                                obj.QuantityBBGN = Convert.ToDouble(QuantityBBGN);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng giao [" + QuantityBBGN + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.QuantityBBGN > 0)
                                                obj.QuantityBBGN = 0;
                                        }

                                        if (!string.IsNullOrEmpty(KgBBGN))
                                        {
                                            try
                                            {
                                                obj.KgBBGN = Convert.ToDouble(KgBBGN);
                                            }
                                            catch
                                            {
                                                lstError.Add("Kg giao [" + KgBBGN + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.KgBBGN > 0)
                                                obj.KgBBGN = 0;
                                        }

                                        var DateDN = excelInput["DateDN"];
                                        if (!string.IsNullOrEmpty(DateDN))
                                        {
                                            try
                                            {
                                                obj.DateDN = ExcelHelper.ValueToDate(DateDN);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateDN = Convert.ToDateTime(DateDN);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày DN [" + DateDN + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateDN = null;

                                        var TonManual = excelInput["TonManual"];
                                        var CBMManual = excelInput["CBMManual"];
                                        var QuantityManual = excelInput["QuantityManual"];

                                        //if (string.IsNullOrEmpty(Ton) && string.IsNullOrEmpty(CBM) && string.IsNullOrEmpty(Quantity))
                                        //{
                                        //    lstError.Add("Tấn, Khối, Số lượng phải có ít nhất 1 cột không được trống");
                                        //}

                                        if (!string.IsNullOrEmpty(TonManual))
                                        {
                                            try
                                            {
                                                obj.TonManual = Convert.ToDouble(TonManual);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn nhập tay [" + TonManual + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            obj.TonManual = 0;
                                        }


                                        if (!string.IsNullOrEmpty(CBMManual))
                                        {
                                            try
                                            {
                                                obj.CBMManual = Convert.ToDouble(CBMManual);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối nhập tay [" + CBMManual + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            obj.CBMManual = 0;
                                        }

                                        if (!string.IsNullOrEmpty(QuantityManual))
                                        {
                                            try
                                            {
                                                obj.QuantityManual = Convert.ToDouble(QuantityManual);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng nhập tay [" + QuantityManual + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            obj.QuantityManual = 0;
                                        }

                                        var Credit = excelInput["Credit"];
                                        var Debit = excelInput["Debit"];

                                        if (!string.IsNullOrEmpty(Credit))
                                        {
                                            try
                                            {
                                                obj.Credit = Convert.ToDecimal(Credit);
                                            }
                                            catch
                                            {
                                                lstError.Add("Thu [" + Credit + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            obj.Credit = 0;
                                        }

                                        if (!string.IsNullOrEmpty(Debit))
                                        {
                                            try
                                            {
                                                obj.Debit = Convert.ToDecimal(Debit);
                                            }
                                            catch
                                            {
                                                lstError.Add("Thu [" + Debit + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            obj.Debit = 0;
                                        }

                                        obj.ManualNote = excelInput["ManualNote"];
                                        #endregion
                                        List<DTOFINManualFixImport> lstDetail = null;
                                        lstDetail = FINManualFix_GetDetail_List(excelInput, data, objSetting, ref lstError, obj);
                                        if (lstDetail == null || lstDetail.Count == 0)
                                        {
                                            lstError.Add("Không tìm thấy dữ liệu");
                                        }
                                        else
                                        {
                                            var lstID = lstDetail.Select(c => new DTOFINManualFixImportID
                                            {
                                                ID = c.ID,
                                                Value = c.ID.ToString(),
                                            }).ToList();
                                            obj.ListID = lstID;
                                            obj.ID = lstDetail[0].ID;
                                            obj.ListDetail = lstDetail;

                                            var itemF = lstDetail[0];
                                            obj.ListManualFix = itemF.ListManualFix;
                                            obj.CustomerCode = itemF.CustomerCode;
                                            obj.SOCode = itemF.SOCode;
                                            obj.DNCode = itemF.DNCode;
                                            obj.RegNo = itemF.RegNo;
                                            obj.OrderCode = itemF.OrderCode;
                                            obj.RequestDate = itemF.RequestDate;
                                            obj.LocationFromCode = itemF.LocationFromCode;
                                            obj.TonTranfer = itemF.TonTranfer;
                                            obj.CBMTranfer = itemF.CBMTranfer;
                                            obj.QuantityTranfer = itemF.QuantityTranfer;
                                            obj.KgTranfer = itemF.KgTranfer;

                                            obj.IsDuplicate = false;
                                            if (dataRes != null && dataRes.Count > 0)
                                            {
                                                if (obj.ListDetail.Count == 1 && dataRes.Count(c => c.ID == obj.ID && c.ListDetail.Count == 1) > 0)
                                                {
                                                    obj.IsDuplicate = true;
                                                    var lst = dataRes.Where(c => c.ID == obj.ID && c.ListDetail.Count == 1).ToList();
                                                    foreach (var item in lst)
                                                    {
                                                        item.IsDuplicate = true;
                                                    }
                                                }
                                            }
                                        }

                                        lstError.Distinct();
                                        obj.ExcelError = string.Join(", ", lstError);
                                        if (!string.IsNullOrEmpty(obj.ExcelError))
                                            obj.ExcelSuccess = false;
                                        else
                                        {
                                            obj.ExcelSuccess = true;
                                        }
                                        dataRes.Add(obj);
                                    }
                                }
                            }
                        }
                    }
                }
                return dataRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DTOFINManualFixImport> FINManualFix_GetDetail_List(Dictionary<string, string> excelInput, List<DTOFINManualFixImport> data, DTOFINSettingManual objSetting, ref List<string> lstError, DTOFINManualFixImport obj)
        {
            #region Data
            var ID = excelInput["ID"];
            var DNCode = excelInput["DNCode"];
            var SOCode = excelInput["SOCode"];
            var DateDN = excelInput["DateDN"];
            var OrderCode = excelInput["OrderCode"];
            var ETARequest = excelInput["ETARequest"];
            var MasterETDDate = excelInput["MasterETDDate"];
            var MasterETDDatetime = excelInput["MasterETDDatetime"];
            var OrderGroupETDDate = excelInput["OrderGroupETDDate"];
            var OrderGroupETDDatetime = excelInput["OrderGroupETDDatetime"];
            var CustomerCode = excelInput["CustomerCode"];
            var CustomerName = excelInput["CustomerName"];
            var CreatedDate = excelInput["CreatedDate"];
            var MasterCode = excelInput["MasterCode"];
            var DriverName = excelInput["DriverName"];
            var DriverTel = excelInput["DriverTel"];
            var DriverCard = excelInput["DriverCard"];
            var RegNo = excelInput["RegNo"];
            var RequestDate = excelInput["RequestDate"];
            var LocationFromCode = excelInput["LocationFromCode"];
            var LocationToCode = excelInput["LocationToCode"];
            var LocationToName = excelInput["LocationToName"];
            var LocationToAddress = excelInput["LocationToAddress"];
            var LocationToProvince = excelInput["LocationToProvince"];
            var LocationToDistrict = excelInput["LocationToDistrict"];
            var DistributorCode = excelInput["DistributorCode"];
            var DistributorName = excelInput["DistributorName"];
            var DistributorCodeName = excelInput["DistributorCodeName"];
            var IsInvoice = excelInput["IsInvoice"];
            var DateFromCome = excelInput["DateFromCome"];
            var DateFromLeave = excelInput["DateFromLeave"];
            var DateFromLoadStart = excelInput["DateFromLoadStart"];
            var DateFromLoadEnd = excelInput["DateFromLoadEnd"];
            var DateToCome = excelInput["DateToCome"];
            var DateToLeave = excelInput["DateToLeave"];
            var DateToLoadStart = excelInput["DateToLoadStart"];
            var DateToLoadEnd = excelInput["DateToLoadEnd"];
            var EconomicZone = excelInput["EconomicZone"];
            var DITOGroupProductStatusPODName = excelInput["DITOGroupProductStatusPODName"];
            var IsOrigin = excelInput["IsOrigin"];
            var InvoiceBy = excelInput["InvoiceBy"];
            var InvoiceDate = excelInput["InvoiceDate"];
            var InvoiceNote = excelInput["InvoiceNote"];
            var Note = excelInput["Note"];
            var OPSGroupNote1 = excelInput["OPSGroupNote1"];
            var OPSGroupNote2 = excelInput["OPSGroupNote2"];
            var ORDGroupNote1 = excelInput["ORDGroupNote1"];
            var ORDGroupNote2 = excelInput["ORDGroupNote2"];
            var TOMasterNote1 = excelInput["TOMasterNote1"];
            var TOMasterNote2 = excelInput["TOMasterNote2"];
            var VendorName = excelInput["VendorName"];
            var VendorCode = excelInput["VendorCode"];
            var Description = excelInput["Description"];
            var GroupOfProductCode = excelInput["GroupOfProductCode"];
            var GroupOfProductName = excelInput["GroupOfProductName"];
            var ChipNo = excelInput["ChipNo"];
            var Temperature = excelInput["Temperature"];
            var Ton = excelInput["Ton"];
            var CBM = excelInput["CBM"];
            var Quantity = excelInput["Quantity"];
            var Kg = excelInput["Kg"];
            var TonTranfer = excelInput["TonTranfer"];
            var CBMTranfer = excelInput["CBMTranfer"];
            var QuantityTranfer = excelInput["QuantityTranfer"];
            var KgTranfer = excelInput["KgTranfer"];
            var TonBBGN = excelInput["TonBBGN"];
            var CBMBBGN = excelInput["CBMBBGN"];
            var QuantityBBGN = excelInput["QuantityBBGN"];
            var KgBBGN = excelInput["KgBBGN"];
            var Packing = excelInput["Packing"];
            var VENLoadCode = excelInput["VENLoadCode"];
            var VENUnLoadCode = excelInput["VENUnLoadCode"];
            var TonReturn = excelInput["TonReturn"];
            var CBMReturn = excelInput["CBMReturn"];
            var QuantityReturn = excelInput["QuantityReturn"];
            var KgReturn = excelInput["KgReturn"];
            var InvoiceReturnNote = excelInput["InvoiceReturnNote"];
            var InvoiceReturnDate = excelInput["InvoiceReturnDate"];
            var ReasonCancelNote = excelInput["ReasonCancelNote"];
            var RoutingCode = excelInput["RoutingCode"];
            #endregion
            DateTime _date = new DateTime();
            List<DTOFINManualFixImport> temp = null;
            temp = data;

            if (objSetting.RegNoKey && objSetting.RegNo > 0)
            {
                if (string.IsNullOrEmpty(RegNo))
                {
                    lstError.Add("Số xe không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.RegNo == RegNo).ToList();
                }
            }

            if (objSetting.MasterETDDateKey && objSetting.MasterETDDate > 0)
            {
                if (string.IsNullOrEmpty(MasterETDDate))
                {
                    lstError.Add("Ngày xuất kho không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(MasterETDDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(MasterETDDate);
                        }
                        catch
                        {
                            lstError.Add("Ngày xuất kho [" + MasterETDDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.MasterETDDate.HasValue && c.MasterETDDate.Value.Date == _date.Date).ToList();
                }
            }

            if (objSetting.MasterETDDatetimeKey && objSetting.MasterETDDatetime > 0)
            {
                if (string.IsNullOrEmpty(MasterETDDatetime))
                {
                    lstError.Add("Ngày giờ xuất kho không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(MasterETDDatetime);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(MasterETDDatetime);
                        }
                        catch
                        {
                            lstError.Add("Ngày giờ xuất kho [" + MasterETDDatetime + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.MasterETDDatetime.HasValue && c.MasterETDDatetime.Value == _date).ToList();
                }
            }

            if (objSetting.OrderGroupETDDateKey && objSetting.OrderGroupETDDate > 0)
            {
                if (string.IsNullOrEmpty(OrderGroupETDDate))
                {
                    lstError.Add("Ngày ETD chi tiết đơn không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(OrderGroupETDDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(OrderGroupETDDate);
                        }
                        catch
                        {
                            lstError.Add("Ngày ETD chi tiết đơn [" + OrderGroupETDDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.OrderGroupETDDate.HasValue && c.OrderGroupETDDate.Value.Date == _date.Date).ToList();
                }
            }

            if (objSetting.OrderGroupETDDatetimeKey && objSetting.OrderGroupETDDatetime > 0)
            {
                if (string.IsNullOrEmpty(OrderGroupETDDatetime))
                {
                    lstError.Add("Ngày giờ ETD chi tiết đơn không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(OrderGroupETDDatetime);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(OrderGroupETDDatetime);
                        }
                        catch
                        {
                            lstError.Add("Ngày giờ ETD chi tiết đơn [" + OrderGroupETDDatetime + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.OrderGroupETDDatetime.HasValue && c.OrderGroupETDDatetime.Value == _date).ToList();
                }
            }

            if (objSetting.OrderCodeKey && objSetting.OrderCode > 0)
            {
                if (string.IsNullOrEmpty(OrderCode))
                {
                    lstError.Add("Mã đơn hàng không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.OrderCode == OrderCode).ToList();
                }
            }

            if (objSetting.LocationToCodeKey && objSetting.LocationToCode > 0)
            {
                if (string.IsNullOrEmpty(LocationToCode))
                {
                    lstError.Add("Mã Điểm giao không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.LocationToCode == LocationToCode).ToList();
                }
            }

            if (objSetting.DistributorCodeKey && objSetting.DistributorCode > 0)
            {
                if (string.IsNullOrEmpty(DistributorCode))
                {
                    lstError.Add("Mã NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorCode == DistributorCode).ToList();
                }
            }

            if (objSetting.DistributorNameKey && objSetting.DistributorName > 0)
            {
                if (string.IsNullOrEmpty(DistributorName))
                {
                    lstError.Add("NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorName == DistributorName).ToList();
                }
            }

            if (objSetting.DistributorCodeNameKey && objSetting.DistributorCodeName > 0)
            {
                if (string.IsNullOrEmpty(DistributorCodeName))
                {
                    lstError.Add("Mã tên NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorCodeName == DistributorCodeName).ToList();
                }
            }

            if (objSetting.PackingKey && objSetting.Packing > 0)
            {
                if (string.IsNullOrEmpty(Packing))
                {
                    lstError.Add("Packing không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.Packing == Packing).ToList();
                }
            }
            if (objSetting.DNCodeKey && objSetting.DNCode > 0)
            {
                if (string.IsNullOrEmpty(DNCode))
                    lstError.Add("Số DN không được trống");
                else
                    temp = temp.Where(c => c.DNCode == DNCode).ToList();
            }


            if (objSetting.SOCodeKey && objSetting.SOCode > 0)
            {
                if (string.IsNullOrEmpty(SOCode))
                    lstError.Add("Số SO không được trống");
                else
                    temp = temp.Where(c => c.SOCode == SOCode).ToList();
            }

            if (objSetting.ETARequestKey && objSetting.ETARequest > 0)
            {
                if (string.IsNullOrEmpty(ETARequest))
                    lstError.Add("Ngày y/c giao hàng không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(ETARequest);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(ETARequest);
                        }
                        catch
                        {
                            lstError.Add("Ngày y/c giao hàng [" + ETARequest + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.ETARequest == _date).ToList();
                }
            }

            if (objSetting.CustomerCodeKey && objSetting.CustomerCode > 0)
            {
                if (string.IsNullOrEmpty(CustomerCode))
                    lstError.Add("Mã Khách hàng không được trống");
                else
                    temp = temp.Where(c => c.CustomerCode == CustomerCode).ToList();
            }

            if (objSetting.CustomerNameKey && objSetting.CustomerName > 0)
            {
                if (string.IsNullOrEmpty(CustomerName))
                    lstError.Add("Tên khách hàng không được trống");
                else
                    temp = temp.Where(c => c.CustomerName == CustomerName).ToList();
            }

            if (objSetting.CreatedDateKey && objSetting.CreatedDate > 0)
            {
                if (string.IsNullOrEmpty(CreatedDate))
                    lstError.Add("Ngày tạo không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(CreatedDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(CreatedDate);
                        }
                        catch
                        {
                            lstError.Add("Ngày tạo [" + CreatedDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.CreatedDate == _date).ToList();
                }
            }

            if (objSetting.MasterCodeKey && objSetting.MasterCode > 0)
            {
                if (string.IsNullOrEmpty(MasterCode))
                    lstError.Add("Mã chuyến không được trống");
                else
                    temp = temp.Where(c => c.MasterCode == MasterCode).ToList();
            }

            if (objSetting.DriverNameKey && objSetting.DriverName > 0)
            {
                if (string.IsNullOrEmpty(DriverName))
                    lstError.Add("Tài xế không được trống");
                else
                    temp = temp.Where(c => c.DriverName == DriverName).ToList();
            }

            if (objSetting.DriverTelKey && objSetting.DriverTel > 0)
            {
                if (string.IsNullOrEmpty(DriverTel))
                    lstError.Add("SĐT Tài xế không được trống");
                else
                    temp = temp.Where(c => c.DriverTel == DriverTel).ToList();
            }

            if (objSetting.DriverCardKey && objSetting.DriverCard > 0)
            {
                if (string.IsNullOrEmpty(DriverCard))
                    lstError.Add("CMND không được trống");
                else
                    temp = temp.Where(c => c.DriverCard == DriverCard).ToList();
            }

            if (objSetting.RequestDateKey && objSetting.RequestDate > 0)
            {
                if (string.IsNullOrEmpty(RequestDate))
                    lstError.Add("Ngày gửi y/c không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(RequestDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(RequestDate);
                        }
                        catch
                        {
                            lstError.Add("ETD [" + RequestDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.RequestDate == _date).ToList();
                }
            }

            if (objSetting.LocationFromCodeKey && objSetting.LocationFromCode > 0)
            {
                if (string.IsNullOrEmpty(LocationFromCode))
                    lstError.Add("Mã kho không được trống");
                else
                    temp = temp.Where(c => c.LocationFromCode == LocationFromCode).ToList();
            }

            if (objSetting.LocationToNameKey && objSetting.LocationToName > 0)
            {
                if (string.IsNullOrEmpty(LocationToName))
                    lstError.Add("NPP không được trống");
                else
                    temp = temp.Where(c => c.LocationToName == LocationToName).ToList();
            }

            if (objSetting.LocationToAddressKey && objSetting.LocationToAddress > 0)
            {
                if (string.IsNullOrEmpty(LocationToAddress))
                    lstError.Add("Địa chỉ không được trống");
                else
                    temp = temp.Where(c => c.LocationToAddress == LocationToAddress).ToList();
            }

            if (objSetting.LocationToProvinceKey && objSetting.LocationToProvince > 0)
            {
                if (string.IsNullOrEmpty(LocationToProvince))
                    lstError.Add("Tỉnh không được trống");
                else
                    temp = temp.Where(c => c.LocationToProvince == LocationToProvince).ToList();
            }

            if (objSetting.LocationToDistrictKey && objSetting.LocationToDistrict > 0)
            {
                if (string.IsNullOrEmpty(LocationToDistrict))
                    lstError.Add("Quận huyện không được trống");
                else
                    temp = temp.Where(c => c.LocationToDistrict == LocationToDistrict).ToList();
            }

            if (objSetting.IsInvoiceKey && objSetting.IsInvoice > 0)
            {
                temp = temp.Where(c => c.IsInvoice == obj.IsInvoice).ToList();
            }

            if (objSetting.DateFromComeKey && objSetting.DateFromCome > 0)
            {
                if (string.IsNullOrEmpty(DateFromCome))
                    lstError.Add("Ngày đến kho không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromCome == obj.DateFromCome).ToList();
                }
            }

            if (objSetting.DateFromLeaveKey && objSetting.DateFromLeave > 0)
            {
                if (string.IsNullOrEmpty(DateFromLeave))
                    lstError.Add("Ngày rời kho không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromLeave == obj.DateFromLeave).ToList();
                }
            }

            if (objSetting.DateFromLoadStartKey && objSetting.DateFromLoadStart > 0)
            {
                if (string.IsNullOrEmpty(DateFromLoadStart))
                    lstError.Add("Thời gian vào máng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromLoadStart == obj.DateFromLoadStart).ToList();
                }
            }

            if (objSetting.DateFromLoadEndKey && objSetting.DateFromLoadEnd > 0)
            {
                if (string.IsNullOrEmpty(DateFromLoadEnd))
                    lstError.Add("Thời gian ra máng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromLoadEnd == obj.DateFromLoadEnd).ToList();
                }
            }

            if (objSetting.DateToComeKey && objSetting.DateToCome > 0)
            {
                if (string.IsNullOrEmpty(DateToCome))
                    lstError.Add("Ngày đến NPP không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToCome == c.DateToCome).ToList();
                }
            }

            if (objSetting.DateToLeaveKey && objSetting.DateToLeave > 0)
            {
                if (string.IsNullOrEmpty(DateToLeave))
                    lstError.Add("Ngày rời NPP không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToLeave == obj.DateToLeave).ToList();
                }
            }

            if (objSetting.DateToLoadStartKey && objSetting.DateToLoadStart > 0)
            {
                if (string.IsNullOrEmpty(DateToLoadStart))
                    lstError.Add("Thời gian b.đầu dỡ hàng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToLoadStart == obj.DateToLoadStart).ToList();
                }
            }

            if (objSetting.DateToLoadEndKey && objSetting.DateToLoadEnd > 0)
            {
                if (string.IsNullOrEmpty(DateToLoadEnd))
                    lstError.Add("Thời gian k.thúc dỡ hàng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToLoadEnd == obj.DateToLoadEnd).ToList();
                }
            }

            if (objSetting.InvoiceByKey && objSetting.InvoiceBy > 0)
            {
                if (string.IsNullOrEmpty(InvoiceBy))
                    lstError.Add("Người tạo chứng từ không được trống");
                else
                    temp = temp.Where(c => c.InvoiceBy == InvoiceBy).ToList();
            }

            if (objSetting.InvoiceDateKey && objSetting.InvoiceDate > 0)
            {
                if (string.IsNullOrEmpty(InvoiceDate))
                    lstError.Add("Ngày tạo c/t không được trống");
                else
                {
                    temp = temp.Where(c => c.InvoiceDate == obj.InvoiceDate).ToList();
                }
            }

            if (objSetting.InvoiceNoteKey && objSetting.InvoiceNote > 0)
            {
                if (string.IsNullOrEmpty(InvoiceNote))
                    lstError.Add("Ghi chú c/t không được trống");
                else
                    temp = temp.Where(c => c.InvoiceNote == InvoiceNote).ToList();
            }

            if (objSetting.NoteKey && objSetting.Note > 0)
            {
                if (string.IsNullOrEmpty(Note))
                    lstError.Add("Ghi chú không được trống");
                else
                    temp = temp.Where(c => c.Note == Note).ToList();
            }

            if (objSetting.OPSGroupNote1Key && objSetting.OPSGroupNote1 > 0)
            {
                if (string.IsNullOrEmpty(OPSGroupNote1))
                    lstError.Add("Ghi chú 1 không được trống");
                else
                    temp = temp.Where(c => c.OPSGroupNote1 == OPSGroupNote1).ToList();
            }

            if (objSetting.OPSGroupNote2Key && objSetting.OPSGroupNote2 > 0)
            {
                if (string.IsNullOrEmpty(OPSGroupNote2))
                    lstError.Add("Ghi chú 2 không được trống");
                else
                    temp = temp.Where(c => c.OPSGroupNote2 == OPSGroupNote2).ToList();
            }

            if (objSetting.ORDGroupNote1Key && objSetting.ORDGroupNote1 > 0)
            {
                if (string.IsNullOrEmpty(ORDGroupNote1))
                    lstError.Add("Ghi chú Đ/h 1 không được trống");
                else
                    temp = temp.Where(c => c.ORDGroupNote1 == ORDGroupNote1).ToList();
            }

            if (objSetting.ORDGroupNote2Key && objSetting.ORDGroupNote2 > 0)
            {
                if (string.IsNullOrEmpty(ORDGroupNote2))
                    lstError.Add("Ghi chú Đ/h 2 không được trống");
                else
                    temp = temp.Where(c => c.ORDGroupNote2 == ORDGroupNote2).ToList();
            }

            if (objSetting.TOMasterNote1Key && objSetting.TOMasterNote1 > 0)
            {
                if (string.IsNullOrEmpty(TOMasterNote1))
                    lstError.Add("Ghi chú chuyến 1 không được trống");
                else
                    temp = temp.Where(c => c.TOMasterNote1 == TOMasterNote1).ToList();
            }

            if (objSetting.TOMasterNote2Key && objSetting.TOMasterNote2 > 0)
            {
                if (string.IsNullOrEmpty(TOMasterNote2))
                    lstError.Add("Ghi chú chuyến 2 không được trống");
                else
                    temp = temp.Where(c => c.TOMasterNote2 == TOMasterNote2).ToList();
            }

            if (objSetting.VendorNameKey && objSetting.VendorName > 0)
            {
                if (string.IsNullOrEmpty(VendorName))
                    lstError.Add("Nhà vận tải không được trống");
                else
                    temp = temp.Where(c => c.VendorName == VendorName).ToList();
            }

            if (objSetting.VendorCodeKey && objSetting.VendorCode > 0)
            {
                if (string.IsNullOrEmpty(VendorCode))
                    lstError.Add("Mã nhà vận tải không được trống");
                else
                    temp = temp.Where(c => c.VendorCode == VendorCode).ToList();
            }

            if (objSetting.DescriptionKey && objSetting.Description > 0)
            {
                if (string.IsNullOrEmpty(Description))
                    lstError.Add("Mô tả không được trống");
                else
                    temp = temp.Where(c => c.Description == Description).ToList();
            }

            if (objSetting.GroupOfProductCodeKey && objSetting.GroupOfProductCode > 0)
            {
                if (string.IsNullOrEmpty(GroupOfProductCode))
                    lstError.Add("Mã nhóm sản phẩm không được trống");
                else
                    temp = temp.Where(c => c.GroupOfProductCode == GroupOfProductCode).ToList();
            }

            if (objSetting.GroupOfProductNameKey && objSetting.GroupOfProductName > 0)
            {
                if (string.IsNullOrEmpty(GroupOfProductName))
                    lstError.Add("Nhóm sản phẩm không được trống");
                else
                    temp = temp.Where(c => c.GroupOfProductName == GroupOfProductName).ToList();
            }

            if (objSetting.ChipNoKey && objSetting.ChipNo > 0)
            {
                if (string.IsNullOrEmpty(ChipNo))
                    lstError.Add("ChipNo không được trống");
                else
                    temp = temp.Where(c => c.ChipNo == ChipNo).ToList();
            }

            if (objSetting.TemperatureKey && objSetting.Temperature > 0)
            {
                if (string.IsNullOrEmpty(Temperature))
                    lstError.Add("Temperature không được trống");
                else
                    temp = temp.Where(c => c.Temperature == Temperature).ToList();
            }

            if (objSetting.TonKey && objSetting.Ton > 0)
            {
                if (string.IsNullOrEmpty(Ton))
                    lstError.Add("Số tấn kế hoạch không được trống");
                else
                {
                    temp = temp.Where(c => c.Ton == obj.Ton).ToList();
                }
            }

            if (objSetting.CBMKey && objSetting.CBM > 0)
            {
                if (string.IsNullOrEmpty(CBM))
                    lstError.Add("Số khối kế hoạch không được trống");
                else
                {
                    temp = temp.Where(c => c.CBM == obj.CBM).ToList();
                }
            }

            if (objSetting.QuantityKey && objSetting.Quantity > 0)
            {
                if (string.IsNullOrEmpty(Quantity))
                    lstError.Add("Số lượng kế hoạch không được trống");
                else
                {
                    temp = temp.Where(c => c.Quantity == obj.Quantity).ToList();
                }
            }

            if (objSetting.KgKey && objSetting.Kg > 0)
            {
                if (string.IsNullOrEmpty(Kg))
                    lstError.Add("Số Kg kế hoạch không được trống");
                else
                {
                    temp = temp.Where(c => c.Kg == obj.Kg).ToList();
                }
            }

            if (objSetting.TonTranferKey && objSetting.TonTranfer > 0)
            {
                if (string.IsNullOrEmpty(TonTranfer))
                    lstError.Add("Tấn lấy không được trống");
                else
                {
                    temp = temp.Where(c => c.TonTranfer == obj.TonTranfer).ToList();
                }
            }

            if (objSetting.CBMTranferKey && objSetting.CBMTranfer > 0)
            {
                if (string.IsNullOrEmpty(CBMTranfer))
                    lstError.Add("Khối lấy không được trống");
                else
                {
                    temp = temp.Where(c => c.CBMTranfer == obj.CBMTranfer).ToList();
                }
            }

            if (objSetting.QuantityTranferKey && objSetting.QuantityTranfer > 0)
            {
                if (string.IsNullOrEmpty(QuantityTranfer))
                    lstError.Add("Số lượng lấy không được trống");
                else
                {
                    temp = temp.Where(c => c.QuantityTranfer == obj.QuantityTranfer).ToList();
                }
            }

            if (objSetting.KgTranferKey && objSetting.KgTranfer > 0)
            {
                if (string.IsNullOrEmpty(KgTranfer))
                    lstError.Add("Số Kg lấy không được trống");
                else
                {
                    temp = temp.Where(c => c.KgTranfer == obj.KgTranfer).ToList();
                }
            }

            if (objSetting.TonBBGNKey && objSetting.TonBBGN > 0)
            {
                if (string.IsNullOrEmpty(TonBBGN))
                    lstError.Add("Tấn giao không được trống");
                else
                {
                    temp = temp.Where(c => c.TonBBGN == obj.TonBBGN).ToList();
                }
            }

            if (objSetting.CBMBBGNKey && objSetting.CBMBBGN > 0)
            {
                if (string.IsNullOrEmpty(CBMBBGN))
                    lstError.Add("Khối giao không được trống");
                else
                {
                    temp = temp.Where(c => c.CBMBBGN == obj.CBMBBGN).ToList();
                }
            }

            if (objSetting.QuantityBBGNKey && objSetting.QuantityBBGN > 0)
            {
                if (string.IsNullOrEmpty(QuantityBBGN))
                    lstError.Add("Số lượng giao không được trống");
                else
                {
                    temp = temp.Where(c => c.QuantityBBGN == obj.QuantityBBGN).ToList();
                }
            }

            if (objSetting.KgBBGNKey && objSetting.KgBBGN > 0)
            {
                if (string.IsNullOrEmpty(KgBBGN))
                    lstError.Add("Số Kg giao không được trống");
                else
                {
                    temp = temp.Where(c => c.KgBBGN == obj.KgBBGN).ToList();
                }
            }

            if (objSetting.VENLoadCodeKey && objSetting.VENLoadCode > 0)
            {
                if (string.IsNullOrEmpty(VENLoadCode))
                    lstError.Add("Vendor bốc xếp lên không được trống");
                else
                    temp = temp.Where(c => c.VENLoadCode == VENLoadCode).ToList();
            }

            if (objSetting.VENUnLoadCodeKey && objSetting.VENUnLoadCode > 0)
            {
                if (string.IsNullOrEmpty(VENUnLoadCode))
                    lstError.Add("Vendor bốc xếp xuống không được trống");
                else
                    temp = temp.Where(c => c.VENUnLoadCode == VENUnLoadCode).ToList();
            }

            if (objSetting.TonReturnKey && objSetting.TonReturn > 0)
            {
                if (string.IsNullOrEmpty(TonReturn))
                    lstError.Add("Tấn trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.TonReturn == obj.TonReturn).ToList();
                }
            }

            if (objSetting.CBMReturnKey && objSetting.CBMReturn > 0)
            {
                if (string.IsNullOrEmpty(CBMReturn))
                    lstError.Add("Khối trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.CBMReturn == obj.CBMReturn).ToList();
                }
            }

            if (objSetting.QuantityReturnKey && objSetting.QuantityReturn > 0)
            {
                if (string.IsNullOrEmpty(QuantityReturn))
                    lstError.Add("Số lượng trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.QuantityReturn == obj.QuantityReturn).ToList();
                }
            }

            if (objSetting.KgReturnKey && objSetting.KgReturn > 0)
            {
                if (string.IsNullOrEmpty(KgReturn))
                    lstError.Add("Số Kg trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.KgReturn == obj.KgReturn).ToList();
                }
            }

            if (objSetting.InvoiceReturnNoteKey && objSetting.InvoiceReturnNote > 0)
            {
                if (string.IsNullOrEmpty(InvoiceReturnNote))
                    lstError.Add("Ghi chú chứng từ không được trống");
                else
                    temp = temp.Where(c => c.InvoiceReturnNote == InvoiceReturnNote).ToList();
            }

            if (objSetting.InvoiceReturnDateKey && objSetting.InvoiceReturnDate > 0)
            {
                if (string.IsNullOrEmpty(InvoiceReturnDate))
                    lstError.Add("Ngày chứng từ trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.InvoiceReturnDate == obj.InvoiceReturnDate).ToList();
                }
            }

            if (objSetting.ReasonCancelNoteKey && objSetting.ReasonCancelNote > 0)
            {
                if (string.IsNullOrEmpty(ReasonCancelNote))
                    lstError.Add("Ghi chú lí do không được trống");
                else
                    temp = temp.Where(c => c.ReasonCancelNote == ReasonCancelNote).ToList();
            }

            if (objSetting.DateDNKey && objSetting.DateDN > 0)
            {
                if (string.IsNullOrEmpty(DateDN))
                    lstError.Add("Ngày DN không được trống");
                else
                {
                    temp = temp.Where(c => c.DateDN == obj.DateDN).ToList();
                }
            }

            if (objSetting.RoutingCodeKey && objSetting.RoutingCode > 0)
            {
                if (string.IsNullOrEmpty(RoutingCode))
                    lstError.Add("Mã cung đường không được trống");
                else
                {
                    temp = temp.Where(c => c.RoutingCode == RoutingCode).ToList();
                }
            }

            if (objSetting.IDKey && objSetting.ID > 0)
            {
                var _id = -1;
                if (string.IsNullOrEmpty(ID))
                    lstError.Add("ID không được trống");
                else
                {
                    try
                    {
                        _id = Convert.ToInt32(ID);
                    }
                    catch
                    {
                        lstError.Add("ID [" + _id + "] không chính xác");
                    }
                    temp = temp.Where(c => c.ID == _id).ToList();
                }
            }
            return temp;
        }

        private Dictionary<string, string> FINManualFix_GetDataName()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("ID", "Số thứ tự");
            result.Add("DNCode", "Số DN");
            result.Add("SOCode", "Số SO");
            result.Add("DateDN", "Ngày DN");
            result.Add("OrderCode", "Mã đơn hàng");
            result.Add("ETARequest", "Ngày y/c giao hàng");
            result.Add("MasterETDDate", "Ngày xuất kho");
            result.Add("MasterETDDatetime", "Ngày giờ xuất kho");
            result.Add("OrderGroupETDDate", "Ngày ETD chi tiết đơn");
            result.Add("OrderGroupETDDatetime", "Ngày giờ ETD chi tiết đơn");
            result.Add("CustomerCode", "Mã Khách hàng");
            result.Add("CustomerName", "Tên khách hàng");
            result.Add("CreatedDate", "Ngày tạo");
            result.Add("MasterCode", "Mã chuyến");
            result.Add("DriverName", "Tài xế");
            result.Add("DriverTel", "SĐT Tài xế");
            result.Add("DriverCard", "CMND");
            result.Add("RegNo", "Xe");
            result.Add("RequestDate", "Ngày gửi y/c");
            result.Add("LocationFromCode", "Mã kho");
            result.Add("LocationToCode", "Mã điểm giao");
            result.Add("LocationToName", "Điểm giao");
            result.Add("LocationToAddress", "Địa chỉ");
            result.Add("LocationToProvince", "Tỉnh");
            result.Add("LocationToDistrict", "Quận huyện");
            result.Add("DistributorCode", "Mã NPP");
            result.Add("DistributorName", "NPP");
            result.Add("DistributorCodeName", "Mã - Tên NPP");
            result.Add("IsInvoice", "Nhận chứng từ");
            result.Add("DateFromCome", "Ngày đến kho");
            result.Add("DateFromLeave", "Ngày rời kho");
            result.Add("DateFromLoadStart", "Thời gian vào máng");
            result.Add("DateFromLoadEnd", "Thời gian ra máng");
            result.Add("DateToCome", "Ngày đến NPP");
            result.Add("DateToLeave", "Ngày rời NPP");
            result.Add("DateToLoadStart", "Thời gian b.đầu dỡ hàng");
            result.Add("DateToLoadEnd", "Thời gian k.thúc dỡ hàng");
            result.Add("InvoiceBy", "Người nhận chứng từ");
            result.Add("InvoiceDate", "Ngày nhận c/t");
            result.Add("InvoiceNote", "Ghi chú c/t");
            result.Add("Note", "Ghi chú");
            result.Add("OPSGroupNote1", "Ghi chú 1");
            result.Add("OPSGroupNote2", "Ghi chú 2");
            result.Add("ORDGroupNote1", "Ghi chú Đ/h 1");
            result.Add("ORDGroupNote2", "Ghi chú Đ/h 2");
            result.Add("TOMasterNote1", "Ghi chú chuyến 1");
            result.Add("TOMasterNote2", "Ghi chú chuyến 2");
            result.Add("VendorName", "Nhà vận tải");
            result.Add("VendorCode", "Mã nhà vận tải");
            result.Add("Description", "Description");
            result.Add("GroupOfProductCode", "Mã nhóm sản phẩm");
            result.Add("GroupOfProductName", "Nhóm sản phẩm");
            result.Add("ChipNo", "ChipNo");
            result.Add("Temperature", "Temperature");
            result.Add("Ton", "Ton");
            result.Add("CBM", "CBM");
            result.Add("Quantity", "Số lượng");
            result.Add("TonTranfer", "Tấn lấy");
            result.Add("CBMTranfer", "Khối lấy");
            result.Add("QuantityTranfer", "Số lượng lấy");
            result.Add("TonBBGN", "Tấn giao");
            result.Add("CBMBBGN", "Khối giao");
            result.Add("QuantityBBGN", "Số lượng giao");
            result.Add("Packing", "Packing");
            result.Add("VENLoadCode", "Vendor bốc xếp lên");
            result.Add("VENUnLoadCode", "Vendor bốc xếp xuống");
            result.Add("TonReturn", "Tấn trả về");
            result.Add("CBMReturn", "Khối trả về");
            result.Add("QuantityReturn", "Số lượng trả về");
            result.Add("InvoiceReturnNote", "Số chứng từ trả về");
            result.Add("InvoiceReturnDate", "Ngày chứng từ trả về");
            result.Add("ReasonCancelNote", "Ghi chú lí do");
            result.Add("Kg", "Kg kế hoạch");
            result.Add("KgTranfer", "Kg lấy");
            result.Add("KgBBGN", "Kg giao");
            result.Add("KgReturn", "Kg trả về");
            result.Add("RoutingCode", "Mã cung đường");
            result.Add("TonManual", "Tấn nhập tay");
            result.Add("CBMManual", "Khối nhập tay");
            result.Add("QuantityManual", "Số lượng nhập tay");
            result.Add("UnitPrice", "Đơn giá");
            result.Add("Credit", "Thu");
            result.Add("Debit", "Chi");
            result.Add("ManualNote", "Ghi chú nhập tay");

            return result;
        }

        private Dictionary<string, string> FINManualFix_GetDataValue(ExcelWorksheet ws, object obj, int row, List<string> sValue)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    var p = prop.Name;
                    if (!sValue.Contains(p))
                    {
                        var v = (int)prop.GetValue(obj, null);
                        result.Add(p, v > 0 ? ExcelHelper.GetValue(ws, row, v) : string.Empty);
                    }
                }
                catch (Exception)
                {
                }
            }
            return result;
        }
        #endregion
        #endregion

        //#region common
        [HttpPost]
        public void FINFreightAuditCus_ImportReject(dynamic dynParam)
        {
            try
            {
                string Note = dynParam.Note.ToString();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_ImportReject(lst, Note);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FINFreightAuditCus_ImportAccept(dynamic dynParam)
        {
            try
            {
                string Note = dynParam.Note.ToString();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    sv.FINFreightAuditCus_ImportAccept(lst, Note);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Customer_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    result = sv.Customer_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public string FINFreightAuditCus_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                string filepath = "/" + FolderUpload.Export + "export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã đơn hàng";
                    col++; worksheet.Cells[row, col].Value = "Chi phí";
                    row++;

                    col = 1;
                    col++; worksheet.Cells[row, col].Value = "[STT]";
                    worksheet.Cells[row, col].Value = "[OrderCode]";
                    col++; worksheet.Cells[row, col].Value = "[Credit]";
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, 1, i, row, i, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;

                    int stt = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = stt;
                    col++; worksheet.Cells[row, col].Value = ""; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = ""; worksheet.Column(col).Width = 20;
                    stt++;
                    row++;

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<FINFreightAuditCus_Import> FINFreightAuditCus_ExcelCheck(dynamic dynParam)
        {
            try
            {
                string file = "/" + dynParam.File.ToString();
                int customerID = (int)dynParam.customerID;
                var result = new List<FINFreightAuditCus_Import>();
                var FINFreightAuditCus = new FINFreightAuditCus_ImportData();
                ServiceFactory.SVFinance((ISVFinance sv) =>
                {
                    FINFreightAuditCus = sv.FINFreightAuditCus_ImportData(customerID);
                });
                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {
                            int row = 0, col = 0, rowStart = 0, colStart = 0, colOrder = 0, CreditCount = 0;
                            int[] colCredit = new int[200];
                            for (row = 1; row <= worksheet.Dimension.End.Row && row < 200; row++)
                            {
                                if (rowStart == 0)
                                {
                                    for (col = 1; col <= worksheet.Dimension.End.Column && col < 200; col++)
                                    {
                                        var str = ExcelHelper.GetValue(worksheet, row, col);
                                        if (str == "[STT]")
                                        {
                                            rowStart = row;
                                            colStart = col;
                                        }
                                        if (str == "[OrderCode]")
                                        {
                                            colOrder = col;
                                        }
                                        if (str == "[Credit]")
                                        {
                                            colCredit[CreditCount] = col;
                                            CreditCount++;
                                        }
                                    }
                                }

                                if (rowStart > 0) break;
                            }

                            if (rowStart < 1) throw new Exception("Kiểm tra [STT] có tồn tại và nằm ở sheet đầu tiên hay không");
                            if (colOrder < 1) throw new Exception("Mã đơn hàng [OrderCode] có tồn tại và nằm ở sheet đầu tiên hay không");
                            if (CreditCount < 1) throw new Exception("Chi phí [Credit] có tồn tại và nằm ở sheet đầu tiên hay không");
                            rowStart++;
                            for (row = rowStart; row <= worksheet.Dimension.End.Row; row++)
                            {

                                int flag = 0;
                                FINFreightAuditCus_Import obj = new FINFreightAuditCus_Import();
                                List<string> lstError = new List<string>();
                                string OrderCode = ExcelHelper.GetValue(worksheet, row, colOrder);
                                obj.ExcelRow = row;
                                obj.ExcelSuccess = true;
                                obj.ExcelError = "";
                                if (!string.IsNullOrEmpty(OrderCode))
                                {
                                    try
                                    {
                                        foreach (var itemCus in FINFreightAuditCus.ListOrder)
                                        {
                                            if (itemCus.OrderCode == OrderCode)
                                            {
                                                flag = 2;
                                                obj.OrderID = itemCus.OrderID;
                                                obj.OrderCode = OrderCode;
                                                for (int i = 0; i < CreditCount; i++)
                                                {
                                                    string Credit = ExcelHelper.GetValue(worksheet, row, colCredit[i]);
                                                    decimal ReCredit = 0;
                                                    try
                                                    {
                                                        if(Credit != "" )
                                                        {
                                                            ReCredit = Convert.ToDecimal(Credit);
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        obj.ExcelError = obj.ExcelError + " " + "[Credit - " + colCredit[i] + "] không chính xác";
                                                    }
                                                    obj.Credit = obj.Credit + ReCredit;
                                                }
                                            }
                                        }
                                        if (result != null)
                                        {
                                            foreach (var itemCus in result)
                                            {
                                                if (itemCus.OrderCode == OrderCode)
                                                {
                                                    flag = 1;
                                                    for (int i = 0; i < CreditCount; i++)
                                                    {
                                                        string Credit = ExcelHelper.GetValue(worksheet, row, colCredit[i]);
                                                        decimal ReCredit = 0;
                                                        try
                                                        {
                                                            if(Credit != "" )
                                                            {
                                                                ReCredit = Convert.ToDecimal(Credit);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            itemCus.ExcelError = itemCus.ExcelError + " " + "[Credit - " + colCredit[i] + "] không chính xác";
                                                        }
                                                        itemCus.Credit = itemCus.Credit + ReCredit;
                                                    }
                                                }
                                            }
                                        }
                                        if (flag == 0)
                                        {
                                            obj.OrderID = -1;
                                            obj.OrderCode = OrderCode;
                                            for (int i = 0; i < CreditCount; i++)
                                            {
                                                string Credit = ExcelHelper.GetValue(worksheet, row, colCredit[i]);
                                                decimal ReCredit = 0;
                                                try
                                                {
                                                    if(Credit != "" )
                                                    {
                                                        ReCredit = Convert.ToDecimal(Credit);
                                                    }
                                                }
                                                catch
                                                {
                                                    obj.ExcelError = obj.ExcelError + "[Credit - " + colCredit[i] + "] không chính xác";
                                                }
                                                obj.Credit = obj.Credit + ReCredit;
                                            }
                                            obj.ExcelError = obj.ExcelError + " " + "Đơn hàng không tồn tại";
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    obj.ExcelError = obj.ExcelError +" " + "Mã đơn hàng không được rỗng";
                                };

                                if (obj.ExcelError != "" ) obj.ExcelSuccess = false;
                                else obj.ExcelSuccess = true;
                                if (flag != 1)
                                    result.Add(obj);
                            }
                        }
                    }
                }
                try{
                        ServiceFactory.SVFinance((ISVFinance sv) =>
                        {
                            result = sv.FINFreightAuditCus_ImportCheck(result, customerID);
                        });
                        return result;
                   }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //#endregion
    }
}