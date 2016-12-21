using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using IServices;
using Presentation;
using System.ServiceModel;
using System.Threading;

namespace ClientReport
{
    public class ReportData
    {
        public List<SYSAction> Demo()
        {
            var result = default(DTOResult);
            ServiceFactory.SVSystem((ISVSystem sv) =>
            {
                result = sv.SYSAction_List(new DTORequest());
            });
            return result.Data.Cast<SYSAction>().ToList();
        }

        public List<DTOVendor> DemoWithParam(string headerkey, int param2)
        {
            var result = default(DTOResult);
            ServiceFactory.SVReportData(headerkey, (ISVReport sv) =>
            {
                result = sv.Vendor_List();
                //result = sv.SYSAction_List(new DTORequest());
            });
            return result.Data.Cast<DTOVendor>().ToList();
        }
        public List<DTOReportVOUMaterialIn> QuickInputFuel(int voumaterialinid)
        {
            var result = new List<DTOReportVOUMaterialIn>();
            ServiceFactory.SVReport((ISVReport sv) =>
            {
                //result = sv.VOUMaterialIn_List(voumaterialinid);
            });
            return result;
            #region data test
            //List<DTOReportVOUMaterialIn> lst = new List<DTOReportVOUMaterialIn>();
            //DTOReportVOUMaterialIn obj = new DTOReportVOUMaterialIn();
            //obj.Code = "02173/TDV/PCD";
            //obj.SupplierID = 1;
            //obj.SupplierName = "Vinamilk";
            //obj.RegNo = "59A12345";
            //obj.VehicleID = 9;
            //obj.KMStart = 100;
            //obj.KMEnd = 199.8;
            //obj.DateStart = new DateTime(2015, 6, 15);
            //obj.DateEnd = new DateTime(2015, 6, 25);
            //obj.DateVoucher = new DateTime(2015, 6, 15);
            //obj.ID = 27;
            //obj.GroupOfMaterialName = "22";
            //obj.MaterialID = 1;
            //obj.MaterialCode = "DO";
            //obj.MaterialName = "Dầu Diesel DO";
            //obj.PackingName = "lít";
            //obj.Price = 20000;
            //obj.Quantity = 45;
            //obj.UserName = "Trần Thị Bích Liên";
            //lst.Add(obj);
            //DTOReportVOUMaterialIn obj2 = new DTOReportVOUMaterialIn();
            //obj2.Code = "02173/TDV/PCD";
            //obj2.SupplierID = 1;
            //obj2.SupplierName = "Vinamilk";
            //obj2.RegNo = "59A12345";
            //obj2.VehicleID = 9;
            //obj2.KMStart = 100;
            //obj2.KMEnd = 199.8;
            //obj2.DateStart = new DateTime(2015, 6, 15);
            //obj2.DateEnd = new DateTime(2015, 6, 25);
            //obj2.DateVoucher = new DateTime(2015, 6, 15);
            //obj2.ID = 27;
            //obj2.GroupOfMaterialName = "333";
            //obj2.MaterialID = 2;
            //obj2.MaterialCode = "X95";
            //obj2.MaterialName = "Xăng 95";
            //obj2.PackingName = "lít";
            //obj2.Price = 25000;
            //obj2.Quantity = 65;
            //obj.UserName = "Trần Thị Bích Liên";
            //lst.Add(obj2);
            //return lst; 
            #endregion
        }

        public List<DTOReportOpsPodContainer> rpOpsPodContainer(int OpsMaterID)
        {
            var result = new List<DTOReportOpsPodContainer>();
            ServiceFactory.SVReport((ISVReport sv) =>
            {
                //result = sv.OpsPodContainer_List(OpsMaterID);
            });
            return result;
            #region data test
            //DTOReportOpsPodContainer obj = new DTOReportOpsPodContainer();
            //obj.CustomerID = 1;
            //obj.CustomerName = "SmartLog";
            //obj.VoucherNo = "TMSV2SML";
            //obj.TypeOfContainerName = "20DC";
            //obj.SealNo = "SEAL6";
            //obj.ContainerNo = "CON140920151";
            //obj.BLNo = "BL14092015";
            //result.Add(obj);

            //DTOReportOpsPodContainer obj1 = new DTOReportOpsPodContainer();
            //obj1.CustomerID = 1;
            //obj1.CustomerName = "SmartLog";
            //obj1.VoucherNo = "TMSV2";
            //obj1.TypeOfContainerName = "40DC";
            //obj1.SealNo = "SEAL69";
            //obj1.ContainerNo = "CON14092015177";
            //obj1.BLNo = "BL1409201566";
            //result.Add(obj1);

            //DTOReportOpsPodContainer obj2 = new DTOReportOpsPodContainer();
            //obj2.CustomerID = 2;
            //obj2.CustomerName = "SmartLog2";
            //obj2.VoucherNo = "TMSV2SML2";
            //obj2.TypeOfContainerName = "20DC";
            //obj2.SealNo = "SEAL6";
            //obj2.ContainerNo = "CON140920151";
            //obj2.BLNo = "BL14092015";
            //result.Add(obj2);

            //DTOReportOpsPodContainer obj12 = new DTOReportOpsPodContainer();
            //obj12.CustomerID = 2;
            //obj12.CustomerName = "SmartLog2";
            //obj12.VoucherNo = "TMSV2";
            //obj12.TypeOfContainerName = "40DC";
            //obj12.SealNo = "SEAL69";
            //obj12.ContainerNo = "CON14092015177";
            //obj12.BLNo = "BL1409201566";
            //result.Add(obj12);
            //return result;
            #endregion
        }
        public List<DTOReportOpsPodDistributor> rpOpsPodDistributor(int OpsMasterID)
        {
            var result = new List<DTOReportOpsPodDistributor>();
            ServiceFactory.SVReport((ISVReport sv) =>
            {
                //result = sv.OpsPodDistributor_List(OpsMasterID);
            });
            return result;
            #region data test
            //DTOReportOpsPodDistributor obj = new DTOReportOpsPodDistributor();
            //obj.CustomerID = 1;
            //obj.CustomerName = "BigC Hoang Van Thu";
            //obj.BLNo = "DIBIGC1";
            //obj.ProductName = "nuoc mía";
            //obj.PackingName = "Lít";
            //obj.VoucherNo = "PHIEU1";
            //result.Add(obj);

            //DTOReportOpsPodDistributor obj1 = new DTOReportOpsPodDistributor();
            //obj1.CustomerID = 2;
            //obj1.CustomerName = "METRO Q2";
            //obj1.BLNo = "DIMETRO01";
            //obj1.ProductName = "Ketamin";
            //obj1.PackingName = "Kg";
            //obj1.VoucherNo = "PHIEU2";
            //result.Add(obj1);

            //return result;
            #endregion
        }
        public List<DTOReportOpsLocation> rpOpsLocation(string dateFrom, string dateTo, int isETD, int transfer, int? provinceid, int? districtid, int? locationid)
        {
            DateTime from = Convert.ToDateTime(dateFrom);
            DateTime to = Convert.ToDateTime(dateTo);
            bool paramETD = isETD == 1 ? true : false;
            var result = new List<DTOReportOpsLocation>();
            ServiceFactory.SVReport((ISVReport sv) =>
            {
                //result = sv.OpsLocation_List(from, to, paramETD, transfer, provinceid, districtid, locationid);
            });
            return result;
        }
        public List<DTOReportDriverSchedule> rpScheduleDriverFee(string headerkey, string lstid, int month,int year)
        {
            var result = new List<DTOReportDriverSchedule>();
            List<int> ListDriverID = new List<int>();
            string[] lst = lstid.Split(',');
            foreach (var item in lst)
            {
                int x = Convert.ToInt32(item);
                ListDriverID.Add(x);
            }
            ServiceFactory.SVReportData(headerkey, (ISVReport sv) =>
            {
                result = sv.REPDriverScheduleFee_Data(ListDriverID,month,year);
            });
            return result;
        }

        public List<DTOReportWorkOrderFuel> rpWorkOrderFuel(string headerkey, string UserName, string lstid)
        {
            var result = new List<DTOReportWorkOrderFuel>();
            List<int> lstReceiptID = new List<int>();
            string[] lst = lstid.Split(',');
            foreach (var item in lst)
            {
                int x = Convert.ToInt32(item);
                lstReceiptID.Add(x);
            }
            ServiceFactory.SVReportData(headerkey, (ISVReport sv) =>
            {
                result = sv.REPWorkOrderFuel_Data(lstReceiptID);
            });
            return result;
            #region datatest
            //var lst = new List<DTOReportWorkOrderFuel>();
            //var obj = new DTOReportWorkOrderFuel();
            //obj.ReceiptNo = "02173/TDV/PCD";
            //obj.RegNo = "59A12345";
            //obj.KMStart = 100;
            //obj.KMEnd = 199.8;
            //obj.DateReceipt = new DateTime(2015, 6, 15);
            //obj.Note = "02173/TDV/PCD";
            //obj.ListMaterial = new List<DTOReportWorkOrderFuelDetail>();
            //for (int i = 0; i < 6; i++)
            //{
            //    var obj1 = new DTOReportWorkOrderFuelDetail();
            //    obj1.MaterialID = 1;
            //    obj1.MaterialCode = "MaterialCode" + i;
            //    obj1.MaterialName = "MaterialName" + i;
            //    obj1.Quantity = i;
            //    obj1.UnitPirce = i * 1000;
            //    obj.ListMaterial.Add(obj1);
            //}
            //lst.Add(obj);
            //return lst;
            #endregion
        }

        public List<DTOReportOPSSotrans> rpOPSSotrans(string headerkey, int masterid)
        {
            var result = new List<DTOReportOPSSotrans>();

            ServiceFactory.SVReportData(headerkey, (ISVReport sv) =>
            {
                result = sv.REPOPSSotrans_Data(masterid);
            });
            foreach (var item in result)
            {
                item.ImageUrl = "./Images/rep/sotrans.png";
            }
            return result;
            #region datatest
            //var lst = new List<DTOReportOPSSotrans>();
            //var obj = new DTOReportOPSSotrans();
            //obj.ReceiptNo = "0000001";
            //obj.DriverName = "Nguyễn trần xuân xxxx";
            //obj.RegNo = "51C-45123";
            //obj.RomoocNo = "51R-0998";
            //obj.CustomerCode = "CustomerCode";
            //obj.CustomerName = "Cty TNHH KH11111 111 111";
            //obj.CustomerShortName = "CUSCode";
            //obj.GroupProductCode = "XANG XO";
            //obj.GroupProductName = "Xăng xe tank";
            //obj.Ton = (decimal)12.432;
            //obj.BillingNo = "BillingNo";
            //obj.ContainerNo = "ContainerNo";
            //obj.TypeContCode = "20DC";
            //obj.TypeContName = "20 DC";
            //obj.SealNo = "SealNo 0001";
            //obj.LocationFrom = "CÁT Lái";
            //obj.LocationTo = "274 UVK";
            //obj.LocationReturn = "Sông SG";

            //lst.Add(obj);

            //var obj2 = new DTOReportOPSSotrans();
            //obj2.ReceiptNo = "0000001";
            //obj2.DriverName = "Nguyễn trần xuân xxxx";
            //obj2.RegNo = "51C-45123";
            //obj2.RomoocNo = "51R-0998";
            //obj2.CustomerCode = "CustomerCode";
            //obj2.CustomerName = "Cty TNHH KH11111 111 111";
            //obj2.CustomerShortName = "CUSCode";
            //obj2.GroupProductCode = "XANG XO";
            //obj2.GroupProductName = "Xăng xe tank";
            //obj2.Ton = (decimal)12.432;
            //obj2.BillingNo = "BillingNo";
            //obj2.ContainerNo = "ContainerNo";
            //obj2.TypeContCode = "20DC";
            //obj2.TypeContName = "20 DC";
            //obj2.SealNo = "SealNo 0001";
            //obj2.LocationFrom = "CÁT Lái";
            //obj2.LocationTo = "274 UVK";
            //obj2.LocationReturn = "Sông SG";

            //lst.Add(obj2);
            //return lst;
            #endregion
        }
    }
}
