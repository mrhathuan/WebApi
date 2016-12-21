using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Kendo.Mvc.Extensions;
using DTO;
using IServices;
using Business;

namespace Services
{
    public partial class SVFleetManage : SVBase, ISVFleetManage
    {
        #region FLMAsset
        #region common
        public DTOResult FLMAsset_Location_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Location_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMAsset_LocationNotIn_Save(int locationID, int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMAsset_LocationNotIn_Save(locationID,assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMAsset_Location_Save(CATLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATLocation FLMAsset_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMAsset_Location_Delete(CATLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMAsset_Location_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMAsset_History_DepreciationList(string request, int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_History_DepreciationList(request, assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMAsset_History_OPSList(string request, int assetID, DateTime dtFrom, DateTime dtTo, bool isDI)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_History_OPSList(request, assetID, dtFrom, dtTo, isDI);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMAsset_History_RepairList(string request, int assetID, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_History_RepairList(request, assetID, dtFrom, dtTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region excel moi
        public SYSExcel FLMAsset_Truck_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Truck_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMAsset_Truck_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Truck_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMAsset_Truck_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Truck_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMAsset_Truck_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Truck_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMAsset_Tractor_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Tractor_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMAsset_Tractor_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Tractor_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMAsset_Tractor_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Tractor_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMAsset_Tractor_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Tractor_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMAsset_Romooc_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Romooc_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMAsset_Romooc_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Romooc_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMAsset_Romooc_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Romooc_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMAsset_Romooc_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Romooc_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #endregion

        #region Truck
        public DTOResult Truck_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Truck_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMTruck Truck_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Truck_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Truck_Save(DTOFLMTruck item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Truck_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Truck_Delete(DTOFLMTruck item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Truck_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMTruckImport> Truck_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Truck_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Truck_Import(List<DTOFLMTruckImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Truck_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Tractor
        public DTOResult Tractor_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMTractor Tractor_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Tractor_Save(DTOFLMTractor item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Tractor_Delete(DTOFLMTractor item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Tractor_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMTractorExcel> Tractor_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Tractor_Import(List<DTOFLMTractorExcel> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Tractor_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region CAT_RomoocDefault
        public DTOResult Tractor_RomoocDefault_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATRomoocDefault Tractor_RomoocDefault_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int Tractor_RomoocDefault_Save(DTOCATRomoocDefault item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Tractor_RomoocDefault_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Tractor_RomoocDefault_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMTractor> Tractor_RomoocDefault_TractorList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_TractorList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMRomooc> Tractor_RomoocDefault_RomoocList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_RomoocList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel Tractor_RomoocDefault_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row Tractor_RomoocDefault_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel Tractor_RomoocDefault_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool Tractor_RomoocDefault_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_RomoocDefault_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #endregion

        #region Romooc
        public DTOResult RomoocAll_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.RomoocAll_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Vendor_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Romooc_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Romooc_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMRomooc Romooc_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Romooc_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Romooc_Save(DTOFLMRomooc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Romooc_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Romooc_Delete(DTOFLMRomooc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Romooc_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMRomoocExcel> Romooc_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Romooc_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Romooc_Import(List<DTOFLMRomoocExcel> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Romooc_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Container
        public DTOResult Container_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Container_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMContainer Container_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Container_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Container_Save(DTOFLMContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Container_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Container_Delete(DTOFLMContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Container_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Equipment
        public DTOResult Equipment_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Equipment_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult Equipment_ListByVehicleID(string request, int vehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Equipment_ListByVehicleID(request, vehicleid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Equipment_PastListByVehicleID(string request, int vehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Equipment_PastListByVehicleID(request, vehicleid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMEquipment Equipment_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Equipment_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public int Equipment_Save(DTOFLMEquipment item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Equipment_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult EquipmentHistory_ListByID(int assetid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.EquipmentHistory_ListByID(assetid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        //public void Equipment_Delete(DTOFLMEquipment item)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLFleetManage>())
        //        {
        //            bl.Equipment_Delete(item);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        public List<DTOFLMEquipmentExcel> Eqm_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Eqm_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Eqm_Import(List<DTOFLMEquipmentExcel> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Eqm_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMEquipmentLocation Equipment_GetLocation()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Equipment_GetLocation();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FLM_ScheduleFeeDefault (phụ phí tháng trong chi tiết asset)
        public DTOResult FLMScheduleFeeDefault_List(string request, int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMScheduleFeeDefault_List(request, assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMScheduleFeeDefault FLMScheduleFeeDefault_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMScheduleFeeDefault_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMScheduleFeeDefault_Save(DTOFLMScheduleFeeDefault item, int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMScheduleFeeDefault_Save(item, assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMScheduleFeeDefault_Delete(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMScheduleFeeDefault_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Operation
        public List<DTOFLMVehicle> Operation_Vehicle()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Operation_Vehicle();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FLMDriver> Operation_Driver()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Operation_Driver();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMFleetPlanning> Operation_List(DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Operation_List(dtFrom, dtTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Operation_Save(List<FLMFleetPlanning> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Operation_Save(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Supplier
        public DTOResult MaterialBySupplierID_List(int supplierid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.MaterialBySupplierID_List(supplierid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult MaterialAll_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.MaterialAll_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion


        #region Material in Vehicle (MaterialQuota)
        public DTOResult MaterialQuota_List(string request, int vehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.MaterialQuota_List(request, vehicleid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void MaterialQuota_SaveList(List<DTOFLMMaterialQuota> lst, int vehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.MaterialQuota_SaveList(lst, vehicleid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int MaterialQuota_Save(DTOFLMMaterialQuota item, int vehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.MaterialQuota_Save(item, vehicleid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void MaterialQuota_Delete(DTOFLMMaterialQuota item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.MaterialQuota_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult MaterialByVehicleID_List(int vehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.MaterialByVehicleID_List(vehicleid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Variable Cost
        public DTOResult VehicleAutoComplete_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.VehicleAutoComplete_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region FLMDriver

        public DTOResult FLMDriver_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMDriver_Save(DTOFLMDriver item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMDriver FLMDriver_Get(int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_Get(driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMDriver_Delete(DTOFLMDriver item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriver_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMDriverExcel> FLMDriverExport()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverExport();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMDriverImport(List<DTOFLMDriverExcel> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriverImport(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMDriverData FLMDriver_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_Data();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMDriver_TransportHistory_Read(string request, DateTime from, DateTime to, int driverID, int typeTrans)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_TransportHistory_Read(request, from, to, driverID, typeTrans);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMTruck Truck_GetByDriver(int driverid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Truck_GetByDriver(driverid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDriver_DrivingLicence_List(string request, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_DrivingLicence_List(request, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATDriverLicence FLMDriver_DrivingLicence_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_DrivingLicence_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMDriver_DrivingLicence_Save(DTOCATDriverLicence item, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriver_DrivingLicence_Save(item, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMDriver_DrivingLicence_Delete(DTOCATDriverLicence item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriver_DrivingLicence_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMDriverScheduleData FLMDriver_Schedule_Data(int month, int year, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_Schedule_Data(month, year, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDriver_FLMScheduleFeeDefault_List(string request, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_FLMScheduleFeeDefault_List(request, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMScheduleFeeDefault FLMDriver_FLMScheduleFeeDefault_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_FLMScheduleFeeDefault_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMDriver_FLMScheduleFeeDefault_Save(DTOFLMScheduleFeeDefault item, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriver_FLMScheduleFeeDefault_Save(item, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMDriver_FLMScheduleFeeDefault_Delete(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriver_FLMScheduleFeeDefault_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDriver_FLMDriverRole_List(string request, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_FLMDriverRole_List(request, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FLMDriverRole FLMDriver_FLMDriverRole_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_FLMDriverRole_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMDriver_FLMDriverRole_Save(FLMDriverRole item, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriver_FLMDriverRole_Save(item, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMDriver_FLMDriverRole_Delete(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriver_FLMDriverRole_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMDriver_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMDriver_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMDriver_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMDriver_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriver_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Maintenance
        public DTOFLMMaintenanceSchedulerData FLMMaintenance_Data(List<int> lstAssetID, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaintenance_Data(lstAssetID, dateFrom, dateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMActivity FLMMaintenance_VehicleTimeGet(int actID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaintenance_VehicleTimeGet(actID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMaintenance_VehicleTimeSave(DTOFLMActivity item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMMaintenance_VehicleTimeSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMaintenance_VehicleTimeDelete(int actID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMMaintenance_VehicleTimeDelete(actID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMMaintenance_AssetList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaintenance_AssetList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMMaintenance_LocationList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaintenance_LocationList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMMaintenance_CostList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaintenance_CostList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FLMTransferReceipt (phiếu chuyển thiết bị)
        public DTOResult FLMTransferReceipt_List(string request, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_List(request, dateFrom, dateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMEquipmentHistory FLMTransferReceipt_GetEQMHistory(int assetid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_GetEQMHistory(assetid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMEquipmentHistory FLMTransferReceipt_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMTransferReceipt_GetEQMList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_GetEQMList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMTransferReceipt_Save(DTOFLMEquipmentHistory item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMTransferReceipt_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMTransferReceiptLocation FLMTransferReceipt_GetEQMLocation()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_GetEQMLocation();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Thanh lý Xe/Thiết bị
        public DTOResult FLMDisposal_Vehicle_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDisposal_Vehicle_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDisposal_EQMByVehicle_List(int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDisposal_EQMByVehicle_List(vehicleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDisposal_EQM_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDisposal_EQM_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDisposal_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDisposal_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMDisposal FLMDisposal_Get(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDisposal_Get(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMDisposal_Save(DTOFLMDisposal item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDisposal_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Lịch sử cấp phát vật tư
        public DTOResult FuelHistory_List(string request, int assetid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FuelHistory_List(request, assetid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Common
        public DTOResult CATDrivingLicence_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.CATDrivingLicence_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATDepartment_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.CATDepartment_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMRomooc_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMRomooc_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult Stock_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Stock_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult AllSupplier_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.AllSupplier_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMAsset_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMReceiptAll_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceiptAll_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMVehicle_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMVehicle_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDriverAll_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverAll_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATVehicle_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.CATVehicle_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATGroupOfVehicle_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.CATGroupOfVehicle_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMAsset> VehicleOwn_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.VehicleOwn_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMMaterial_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterial_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATGroupOfRomooc_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.CATGroupOfRomooc_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATRomooc_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.CATRomooc_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATGroupOfEquipment_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.CATGroupOfEquipment_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMAsset> EquipmentOwn_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.EquipmentOwn_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Schedule
        //danh sach bang
        public DTOResult FLMSchedule_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMSchedule FLMSchedule_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Save(DTOFLMSchedule item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMSchedule_Copy(List<int> lstId)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Copy(lstId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMSchedule_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //chi tiet 1 bang
        public DTOFLMScheduleData FLMSchedule_Detail_Data(int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_Detail_Data(scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Detail_Save(List<DTOFLMScheduleDetail> lst, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Detail_Save(lst, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Detail_Import(List<DTOFLMScheduleImport> lst, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Detail_Import(lst, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //tai xe 1 bang
        public DTOResult FLMSchedule_Driver_List(string request, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_Driver_List(request, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMScheduleDriver FLMSchedule_Driver_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_Driver_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Driver_Save(DTOFLMScheduleDriver item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Driver_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Driver_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Driver_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMSchedule_Driver_NotInList(string request, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_Driver_NotInList(request, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Driver_NotInSave(List<int> lstDriver, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Driver_NotInSave(lstDriver, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Driver_UpdateInfo(int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Driver_UpdateInfo(scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //loai ngay trong bang cham
        public DTOResult FLMSchedule_Date_List(string request, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_Date_List(request, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMScheduleDate FLMSchedule_Date_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_Date_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_Date_Save(DTOFLMScheduleDate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_Date_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //phi theo tai xe
        public DTOResult FLMSchedule_DriverFee_List(string request, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_DriverFee_List(request, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMScheduleFee FLMSchedule_DriverFee_Get(int id, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_DriverFee_Get(id, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_DriverFee_Save(DTOFLMScheduleFee item, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_DriverFee_Save(item, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_DriverFee_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_DriverFee_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMSchedule_DriverFee_DriverList(int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_DriverFee_DriverList(scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //phi theo xe
        public DTOResult FLMSchedule_AssetFee_List(string request, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_AssetFee_List(request, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMScheduleFee FLMSchedule_AssetFee_Get(int id, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_AssetFee_Get(id, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_AssetFee_Save(DTOFLMScheduleFee item, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_AssetFee_Save(item, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_AssetFee_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_AssetFee_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMSchedule_AssetFee_AsestList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_AssetFee_AsestList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMScheduleFeeData FLMSchedule_ScheduleFee_Data(int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_ScheduleFee_Data(scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_DriverFee_Import(List<DTOFLMScheduleFeeImport> lst, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_DriverFee_Import(lst, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_AssetFee_Import(List<DTOFLMScheduleFeeImport> lst, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_AssetFee_Import(lst, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        //chi phi lai/ phu xe
        public DTOResult FLMSchedule_AssistantFee_List(string request, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_AssistantFee_List(request, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMScheduleFee FLMSchedule_AssistantFee_Get(int id, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSchedule_AssistantFee_Get(id, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_AssistantFee_Save(DTOFLMScheduleFee item, int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_AssistantFee_Save(item, scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSchedule_AssistantFee_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSchedule_AssistantFee_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //tinh luong
        public void FLM_Schedule_Detail_CalculateFee(int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLM_Schedule_Detail_CalculateFee(scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLM_Schedule_Detail_RefreshFee(int scheduleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLM_Schedule_Detail_RefreshFee(scheduleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Khấu hao
        //public DTOResult FLMFixedCost_List(DTORequest request, int Month, int Year)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLFleetManage>())
        //        {
        //            return bl.FLMFixedCost_List(request.ToKendo(), Month, Year);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        //public void FLMFixedCost_SaveList(List<DTOFLMFixedCost> lst)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLFleetManage>())
        //        {
        //            bl.FLMFixedCost_SaveList(lst);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}
        #endregion

        #region FLMDriverTimeSheet
        public List<DTOFLMVehicle> FLMDriverTimeSheet_VehicleList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverTimeSheet_VehicleList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMAssetTimeSheet> FLMDriverTimeSheet_VehicleTimeList(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverTimeSheet_VehicleTimeList(dateFrom, dateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMDriverTimeSheet FLMDriverTimeSheet_VehicleTimeGet(int actID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverTimeSheet_VehicleTimeGet(actID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMDriverTimeSheet_DriverSave(int timeID, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriverTimeSheet_DriverSave(timeID, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMDriverTimeSheet_DriverDelete(int timeDriverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriverTimeSheet_DriverDelete(timeDriverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMDriver> FLMDriverTimeSheet_DriverList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverTimeSheet_DriverList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMDriverTimeSheet_ChangeType(int timeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriverTimeSheet_ChangeType(timeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FLMTransferReceipt new
        public DTOResult FLMTransferReceipt_StockList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_StockList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMTransferReceipt_EQMByStock(string request, int stockid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_EQMByStock(request, stockid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOEquipment FLMTransferReceipt_EQMGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_EQMGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMTransferReceipt_EQMSave(DTOEquipment item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMTransferReceipt_EQMSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOEquipment> FLMTransferReceipt_EQMData()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTransferReceipt_EQMData();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMTransferReceipt_EQMImport(List<DTOEquipmentImport> data, int stockID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMTransferReceipt_EQMImport(data, stockID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region work order
        public DTOResult FLMReceipt_List(string request, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_List(request, dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_ApprovedList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_ApprovedList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_UnApprovedList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_UnApprovedList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOReceipFuel FLMReceipt_FuelRequestGet(int receiptID, int VehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_FuelRequestGet(receiptID, VehicleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<double> FLMReceipt_QuantityPerKMGet(int MaterialID, int VehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_QuantityPerKMGet(MaterialID, VehicleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOReceipFuel FLMReceipt_FuelGet(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_FuelGet(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_FuelSave(DTOReceipFuel item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_FuelSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOReceipTransfer FLMReceipt_TranfersGet(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_TranfersGet(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOEquipment> FLMReceipt_TranfersEQMByStock(int stockID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_TranfersEQMByStock(stockID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOEquipment> FLMReceipt_TranfersEQMByVehicle(int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_TranfersEQMByVehicle(vehicleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_TranfersSave(DTOReceipTransfer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_TranfersSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOReceiptDisposal FLMReceipt_DisposalGet(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_DisposalGet(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_DisposalSave(DTOReceiptDisposal item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_DisposalSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOReceiptDisposalEquipment> FLMReceipt_DisposalEQMList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_DisposalEQMList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOReceiptDisposalVehicle> FLMReceipt_DisposalVehicleList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_DisposalVehicleList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOReceiptDisposalEquipment> FLMReceipt_DisposalEQMByVehicle(int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_DisposalEQMByVehicle(vehicleID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOReceiptRepairLarge FLMReceipt_RepairLargeGet(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_RepairLargeGet(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMReceipt_RepairLargeSave(DTOReceiptRepairLarge item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_RepairLargeSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMFixedCost> FLMReceipt_RepairLargeListFixCost(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_RepairLargeListFixCost(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMFixedCost> FLMReceipt_RepairLargeGenerateFixCost(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_RepairLargeGenerateFixCost(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_RepairLargeSaveFixCost(List<DTOFLMFixedCost> lst, int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_RepairLargeSaveFixCost(lst, receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMReceipt_RepairLargeDeleteFixCost(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMReceipt_RepairLargeDeleteFixCost(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMReceipt_RepairChangeToLarge(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_RepairChangeToLarge(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMReceipt_RepairChangeToSmall(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_RepairChangeToSmall(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region Receipt import equipment
        public DTOReceiptImportEQM FLMReceipt_ImportEQM_Get(int receiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_Get(receiptID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMReceipt_ImportEQM_Save(DTOReceiptImportEQM item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMReceipt_ImportEQM_StockList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_StockList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMReceipt_ImportEQM_VehicleList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_VehicleList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMReceipt_ImportEQM_ExcelInit(int functionid, string functionkey,bool isreload, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_ExcelInit(functionid, functionkey,isreload, dtFrom, dtTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMReceipt_ImportEQM_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_ExcelChange(id, row, cells,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMReceipt_ImportEQM_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMReceipt_ImportEQM_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMReceipt_ImportEQM_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #endregion

        #region hợp đồng tài xế
        public DTOFLMContract_Data FLMContract_Data(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Data(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATContract FLMContract_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMContract_Save(DTOCATContract item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_DriverFee_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATDriverFee FLMContract_DriverFee_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMContract_DriverFee_Save(DTOCATDriverFee item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region CODefault
        public DTOResult FLMContract_CODefault_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_CODefault_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_CODefault_NotInList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_CODefault_NotInList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_CODefault_NotIn_SaveList(List<DTOCATPacking> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_CODefault_NotIn_SaveList(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_CODefault_Delete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_CODefault_Delete(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_CODefault_Update(List<DTOCATContractCODefault> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_CODefault_Update(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region group location
        public DTOResult FLMContract_DriverFee_GroupLocation_List(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_GroupLocation_List(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_GroupLocation_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_GroupLocation_SaveList(List<int> lst, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_GroupLocation_SaveList(lst, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_DriverFee_GroupLocation_GroupNotInList(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_GroupLocation_GroupNotInList(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region customer
        public DTOResult FLMContract_DriverFee_Customer_List(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Customer_List(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Customer_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Customer_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Customer_SaveList(List<int> lst, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Customer_SaveList(lst, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_DriverFee_Customer_GroupNotInList(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Customer_GroupNotInList(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region  location
        public DTOResult FLMContract_DriverFee_Location_List(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Location_List(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATDriverFeeLocation FLMContract_DriverFee_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Location_Save(DTOCATDriverFeeLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Location_LocationNotInSaveList(List<int> lst, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Location_LocationNotInSaveList(lst, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_DriverFee_Location_LocationNotInList(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Location_LocationNotInList(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region gop
        public DTOResult FLMContract_DriverFee_GroupProduct_List(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_GroupProduct_List(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATDriverFeeGroupProduct FLMContract_DriverFee_GroupProduct_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_GroupProduct_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_GroupProduct_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_GroupProduct_Save(DTOCATDriverFeeGroupProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_GroupProduct_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_DriverFee_GroupProduct_NotInList(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_GroupProduct_NotInList(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_GroupProduct_NotInSaveList(List<int> lst, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_GroupProduct_NotInSaveList(lst, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region route
        public DTOResult FLMContract_DriverFee_Route_List(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Route_List(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_Route_SaveList(List<int> lst, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_Route_SaveList(lst, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_DriverFee_Route_RouteNotInList(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_Route_RouteNotInList(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region parent route
        public DTOResult FLMContract_DriverFee_ParentRoute_List(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_ParentRoute_List(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_DriverFee_ParentRoute_SaveList(List<int> lst, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_DriverFee_ParentRoute_SaveList(lst, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_DriverFee_ParentRoute_RouteNotInList(string request, int driverFeeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_DriverFee_ParentRoute_RouteNotInList(request, driverFeeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Routing
        public DTOResult FLMContract_Routing_List(int contractID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_List(contractID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_Routing_Save(DTOCATContractRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Routing_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_Routing_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Routing_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_Routing_NotIn_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_NotIn_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Routing_NotIn_Delete(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Routing_NotIn_Delete(id, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Routing_Insert(List<DTOCATRouting> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Routing_Insert(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMContractRouting_Import> FLMContract_Routing_Export(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_Export(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_Routing_Import(List<DTOFLMContractRouting_Import> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Routing_Import(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMContractRoutingData FLMContract_RoutingByCus_List(int customerID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_RoutingByCus_List(customerID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_KPI_Save(List<DTOContractKPITime> data, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_KPI_Save(data, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_KPI_Routing_List(string request, int contractID, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPI_Routing_List(request, contractID, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DateTime? FLMContract_KPI_Check_Expression(string sExpression, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPI_Check_Expression(sExpression, item, zone, leadTime, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool? FLMContract_KPI_Check_Hit(string sExpression, string sField, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPI_Check_Hit(sExpression, sField, item, zone, leadTime, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_KPI_Routing_Apply(List<DTOCATContractRouting> data, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_KPI_Routing_Apply(data, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_Routing_CATNotIn_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_CATNotIn_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Routing_CATNotIn_Save(List<DTOCATRouting> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Routing_CATNotIn_Save(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOContractTerm> FLMContract_Routing_ContractTermList(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_ContractTermList(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region tao routing moi
        public DTOCATRouting FLMContract_NewRouting_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMContract_NewRouting_Save(DTOCATRouting item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //route by location
        public DTOResult FLMContract_NewRouting_LocationList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_LocationList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //route by area
        public DTOResult FLMContract_NewRouting_AreaList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATRoutingArea FLMContract_NewRouting_AreaGet(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaGet(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMContract_NewRouting_AreaSave(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_NewRouting_AreaDelete(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_NewRouting_AreaDelete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_NewRouting_AreaRefresh(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_NewRouting_AreaRefresh(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_NewRouting_AreaDetailList(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaDetailList(request, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATRoutingAreaDetail FLMContract_NewRouting_AreaDetailGet(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaDetailGet(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMContract_NewRouting_AreaDetailSave(DTOCATRoutingAreaDetail item, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaDetailSave(item, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_NewRouting_AreaDetailDelete(DTOCATRoutingAreaDetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_NewRouting_AreaDetailDelete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_NewRouting_AreaLocation_List(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaLocation_List(request, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_NewRouting_AreaLocationNotIn_List(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_NewRouting_AreaLocationNotIn_List(request, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_NewRouting_AreaLocationNotIn_Save(int areaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_NewRouting_AreaLocationNotIn_Save(areaID, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_NewRouting_AreaLocation_Delete(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_NewRouting_AreaLocation_Delete(lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_NewRouting_AreaLocation_Copy(int areaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_NewRouting_AreaLocation_Copy(areaID, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        public SYSExcel FLMContract_Routing_ExcelOnline_Init(int contractID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_ExcelOnline_Init(contractID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMContract_Routing_ExcelOnline_Change(int contractID, int customerID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_ExcelOnline_Change(contractID, customerID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMContract_Routing_ExcelOnline_Import(int contractID, int customerID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_ExcelOnline_Import(contractID, customerID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMContract_Routing_ExcelOnline_Approve(long id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Routing_ExcelOnline_Approve(id, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Contract Setting
        public void FLMContract_Setting_TypeOfRunLevelSave(int typeID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Setting_TypeOfRunLevelSave(typeID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_Setting_TypeOfSGroupProductChangeSave(int typeID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Setting_TypeOfSGroupProductChangeSave(typeID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContractSetting_Save(string setting, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContractSetting_Save(setting, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_Setting_GOVList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Setting_GOVList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATContractGroupVehicle FLMContract_Setting_GOVGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Setting_GOVGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Setting_GOVSave(DTOCATContractGroupVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Setting_GOVSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Setting_GOVDeleteList(List<int> lst, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Setting_GOVDeleteList(lst, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMContract_Setting_GOVNotInList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Setting_GOVNotInList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Setting_GOVNotInSave(List<int> lst, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Setting_GOVNotInSave(lst, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_Setting_LevelList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Setting_LevelList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATContractLevel FLMContract_Setting_LevelGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Setting_LevelGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Setting_LevelSave(DTOCATContractLevel item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Setting_LevelSave(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Setting_LevelDeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Setting_LevelDeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATGroupOfVehicle> FLMContract_Setting_Level_GOVList(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Setting_Level_GOVList(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Contract term(phụ lục hợp đồng)
        public DTOResult FLMContract_ContractTerm_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOContractTerm FLMContract_ContractTerm_Get(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_Get(id, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMContract_ContractTerm_Save(DTOContractTerm item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_ContractTerm_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_ContractTerm_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_ContractTerm_Price_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_Price_List(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_ContractTerm_Open(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_ContractTerm_Open(termID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_ContractTerm_Close(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_ContractTerm_Close(termID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMTerm_Change_RemoveWarning(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMTerm_Change_RemoveWarning(termID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region KPI Term
        public DTOResult FLMContract_ContractTerm_KPITime_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_KPITime_List(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_ContractTerm_KPITime_SaveExpr(DTOContractTerm_KPITime item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_ContractTerm_KPITime_SaveExpr(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_ContractTerm_KPITime_NotInList(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_KPITime_NotInList(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DateTime? FLMContract_KPITime_Check_Expression(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPITime_Check_Expression(item, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool? FLMContract_KPITime_Check_Hit(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPITime_Check_Hit(item, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_ContractTerm_KPITime_SaveNotInList(List<DTOContractTerm_TypeOfKPI> lst, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_ContractTerm_KPITime_SaveNotInList(lst, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_ContractTerm_KPIQuantity_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_KPIQuantity_List(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_ContractTerm_KPIQuantity_SaveExpr(DTOContractTerm_KPIQuantity item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_ContractTerm_KPIQuantity_SaveExpr(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMContract_ContractTerm_KPIQuantity_NotInList(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_ContractTerm_KPIQuantity_NotInList(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_ContractTerm_KPIQuantity_SaveNotInList(List<DTOContractTerm_TypeOfKPI> lst, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_ContractTerm_KPIQuantity_SaveNotInList(lst, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIQuantityDate> FLMContract_KPIQuantity_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPIQuantity_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public KPIQuantityDate FLMContract_KPIQuantity_Check_Expression(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPIQuantity_Check_Expression(item, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool? FLMContract_KPIQuantity_Check_Hit(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_KPIQuantity_Check_Hit(item, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion
        #endregion

        #region Price

        #region Common

        public DTOResult FLMContract_Price_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Price_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPrice FLMContract_Price_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Price_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMPrice_Data FLMContract_Price_Data(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Price_Data(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMContract_Price_Save(DTOPrice item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Price_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_Price_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Price_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMContract_Price_Copy(List<DTOFLMPrice_ItemCopy> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_Price_Copy(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMPrice_ExcelData FLMContract_Price_ExcelData(int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_Price_ExcelData(contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMPriceCO_Data FLMContract_PriceCO_Data(int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_PriceCO_Data(contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceCOContainerExcelData FLMPrice_CO_COContainer_ExcelData(int priceid, int contractid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_COContainer_ExcelData(priceid, contractid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_COContainer_ExcelImport(List<DTOPriceCOContainerImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_COContainer_ExcelImport(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public SYSExcel FLMPrice_CO_GroupContainer_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
               using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_GroupContainer_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row FLMPrice_CO_GroupContainer_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
               using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_GroupContainer_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_CO_GroupContainer_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
               using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_GroupContainer_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMPrice_CO_GroupContainer_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
               using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_GroupContainer_ExcelApprove(isFrame, priceID, contractTermID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region price co EX
        #region info
        public DTOResult FLMPrice_CO_Ex_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMPrice_CO_Ex_Save(DTOPriceCOEx item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_CO_Ex_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceCOEx FLMPrice_CO_Ex_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region  location
        public DTOResult FLMPrice_CO_Ex_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceCOExLocation FLMPrice_CO_Ex_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_Location_Save(DTOPriceCOExLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_CO_Ex_Location_LocationNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Location_LocationNotInList(request, priceExID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region route
        public DTOResult FLMPrice_CO_Ex_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_CO_Ex_Route_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Route_RouteNotInList(request, priceExID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region parent route
        public DTOResult FLMPrice_CO_Ex_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_CO_Ex_ParentRoute_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_ParentRoute_RouteNotInList(request, priceExID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region partner
        public DTOResult FLMPrice_CO_Ex_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Partner_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Partner_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_Ex_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Ex_Partner_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_CO_Ex_Partner_PartnerNotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Ex_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion
        #endregion
        #endregion

        #region DI_GroupVehicle

        #region old
        public List<DTOPriceGroupVehicle> FLMPrice_DI_GroupVehicle_GetData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupVehicle_GetData(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_GroupVehicle_SaveList(List<DTOPriceGroupVehicle> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_GroupVehicle_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceGroupVehicleData FLMPrice_DI_GroupVehicle_ExcelData(int priceID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupVehicle_ExcelData(priceID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_GroupVehicle_ExcelImport(List<DTOPriceGroupVehicleImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_GroupVehicle_ExcelImport(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_GroupVehicle_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupVehicle_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row FLMPrice_DI_GroupVehicle_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupVehicle_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_GroupVehicle_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupVehicle_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMPrice_DI_GroupVehicle_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupVehicle_ExcelApprove(isFrame, priceID, contractTermID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region new

        public List<DTOPriceGVLevelGroupVehicle> FLMPrice_DI_PriceGVLevel_DetailData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceGVLevel_DetailData(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_PriceGVLevel_Save(List<DTOPriceGVLevelGroupVehicle> lst, int priceid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceGVLevel_Save(lst, priceid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceGVLevelData FLMPrice_DI_PriceGVLevel_ExcelData(int priceid, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceGVLevel_ExcelData(priceid, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceGVLevel_ExcelImport(List<DTOPriceGVLevelImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceGVLevel_ExcelImport(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_PriceGVLevel_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceGVLevel_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row FLMPrice_DI_PriceGVLevel_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceGVLevel_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_PriceGVLevel_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceGVLevel_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMPrice_DI_PriceGVLevel_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceGVLevel_ExcelApprove(isFrame, priceID, contractTermID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #endregion

        #region CO_PackingPrice

        public List<DTOPriceRouting> FLMPrice_CO_COPackingPrice_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_COPackingPrice_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_CO_COPackingPrice_SaveList(List<DTOPriceRouting> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_COPackingPrice_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceContainer_Export FLMPrice_CO_COPackingPrice_Export(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_COPackingPrice_Export(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_CO_COPackingPrice_Import(List<DTOPrice_COPackingPrice_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_COPackingPrice_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region CO_Service

        public DTOResult FLMPrice_CO_Service_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Service_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_CO_ServicePacking_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_ServicePacking_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPriceCOService FLMPrice_CO_Service_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Service_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPriceCOService FLMPrice_CO_ServicePacking_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_ServicePacking_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMPrice_CO_Service_Save(DTOCATPriceCOService item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_Service_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_CO_Service_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_Service_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_CO_CATService_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_CATService_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_CO_CATServicePacking_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_CATServicePacking_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_CO_CATCODefault_List(int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_CATCODefault_List(contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region price CO container
        public DTOPriceCOContainerData FLMPrice_CO_COContainer_Data(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_COContainer_Data(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_CO_COContainer_SaveList(List<DTOPriceCOContainer> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_COContainer_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_CO_COContainer_ContainerList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_COContainer_ContainerList(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_COContainer_ContainerNotInSave(List<int> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_COContainer_ContainerNotInSave(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_CO_COContainer_ContainerNotInList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_CO_COContainer_ContainerNotInList(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_CO_COContainer_ContainerDelete(List<int> lstPacking, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_CO_COContainer_ContainerDelete(lstPacking, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region loading new

        #region comon
        public void FLMPrice_DI_Load_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Load_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Load_DeleteList(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Load_DeleteList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Load_DeleteAllList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Load_DeleteAllList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Location
        public List<DTOPriceTruckDILoad> FLMPrice_DI_LoadLocation_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadLocation_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_LoadLocation_LocationNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadLocation_LocationNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadLocation_LocationNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadLocation_LocationNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadLocation_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadLocation_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_LoadLocation_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadLocation_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        //Excel
        public DTOPriceTruckDILoad_Export FLMPrice_DI_LoadLocation_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadLocation_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_LoadLocation_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadLocation_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Route
        public List<DTOPriceTruckDILoad> FLMPrice_DI_LoadRoute_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadRoute_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_LoadRoute_RouteNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadRoute_RouteNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadRoute_RouteNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadRoute_RouteNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadRoute_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadRoute_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_LoadRoute_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadRoute_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //Excel
        public DTOPriceTruckDILoad_Export FLMPrice_DI_LoadRoute_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadRoute_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadRoute_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadRoute_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region TypeOfPartner
        public List<DTOPriceTruckDILoad> FLMPrice_DI_LoadPartner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadPartner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_LoadPartner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadPartner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadPartner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadPartner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadPartner_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadPartner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadPartner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadPartner_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        //Excel
        public DTOPriceTruckDILoad_Export FLMPrice_DI_LoadPartner_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadPartner_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadPartner_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadPartner_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Partner
        public List<DTOPriceDILoadPartner> FLMPrice_DI_LoadPartner_Partner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadPartner_Partner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_LoadPartner_Partner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_LoadPartner_Partner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadPartner_Partner_SaveList(List<DTOPriceDILoadPartner> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadPartner_Partner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_LoadPartner_Partner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_LoadPartner_Partner_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region MOQ

        #region info
        public DTOResult FLMPrice_DI_PriceMOQLoad_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMPrice_DI_PriceMOQLoad_Save(DTOPriceDIMOQLoad item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQLoad FLMPrice_DI_PriceMOQLoad_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region group location
        public DTOResult FLMPrice_DI_PriceMOQLoad_GroupLocation_List(string request, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_GroupLocation_List(request, priceMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_GroupLocation_SaveList(List<int> lst, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_GroupLocation_SaveList(lst, priceMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(string request, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(request, priceMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region groupproduct
        public DTOResult FLMPrice_DI_PriceMOQLoad_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_GroupProduct_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQLoadGroupProduct FLMPrice_DI_PriceMOQLoad_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_GroupProduct_Get(id, cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_GroupProduct_Save(DTOPriceDIMOQLoadGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_GroupProduct_Save(item, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQLoad_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_GroupProduct_GOPList(cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region  location
        public DTOResult FLMPrice_DI_PriceMOQLoad_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQLoad_Location_LocationNotInList(string request, int PriceMOQLoadID, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Location_LocationNotInList(request, PriceMOQLoadID, customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIMOQLoadLocation FLMPrice_DI_PriceMOQLoad_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_PriceMOQLoad_Location_Save(DTOPriceDIMOQLoadLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region route
        public DTOResult FLMPrice_DI_PriceMOQLoad_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQLoad_Route_RouteNotInList(string request, int PriceMOQLoadID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Route_RouteNotInList(request, PriceMOQLoadID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region parent route
        public DTOResult FLMPrice_DI_PriceMOQLoad_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(string request, int PriceMOQLoadID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(request, PriceMOQLoadID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region province

        public DTOResult FLMPrice_DI_PriceMOQLoad_Province_List(string request, int PriceDIMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Province_List(request, PriceDIMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Province_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQLoad_Province_SaveList(List<int> lst, int PriceDIMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQLoad_Province_SaveList(lst, PriceDIMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQLoad_Province_NotInList(string request, int PriceDIMOQLoadID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQLoad_Province_NotInList(request, PriceDIMOQLoadID, contractID, CustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #endregion

        #region Area

        #endregion

        #endregion

        #region price di Ex

        #region info
        public DTOResult FLMPrice_DI_Ex_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMPrice_DI_Ex_Save(DTOPriceDIEx item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_Ex_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIEx FLMPrice_DI_Ex_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region group location
        public DTOResult FLMPrice_DI_Ex_GroupLocation_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_GroupLocation_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_GroupLocation_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_GroupLocation_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_GroupLocation_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_Ex_GroupLocation_GroupNotInList(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_GroupLocation_GroupNotInList(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region groupproduct
        public DTOResult FLMPrice_DI_Ex_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_GroupProduct_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_GroupProduct_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIExGroupProduct FLMPrice_DI_Ex_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_GroupProduct_Get(id, cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_GroupProduct_Save(DTOPriceDIExGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_GroupProduct_Save(item, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_Ex_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_GroupProduct_GOPList(cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region  location
        public DTOResult FLMPrice_DI_Ex_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIExLocation FLMPrice_DI_Ex_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Location_Save(DTOPriceDIExLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_Ex_Location_LocationNotInList(string request, int priceExID, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Location_LocationNotInList(request, priceExID, customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region route
        public DTOResult FLMPrice_DI_Ex_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_Ex_Route_RouteNotInList(string request, int priceExID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Route_RouteNotInList(request, priceExID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region parent route
        public DTOResult FLMPrice_DI_Ex_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_Ex_ParentRoute_RouteNotInList(string request, int priceExID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_ParentRoute_RouteNotInList(request, priceExID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region partner
        public DTOResult FLMPrice_DI_Ex_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Partner_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Partner_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Partner_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_Ex_Partner_PartnerNotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region province

        public DTOResult FLMPrice_DI_Ex_Province_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Province_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Province_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_Ex_Province_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_Ex_Province_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_Ex_Province_NotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_Ex_Province_NotInList(request, priceExID, contractID, CustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #endregion

        #region moq new

        #region info
        public DTOResult FLMPrice_DI_PriceMOQ_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMPrice_DI_PriceMOQ_Save(DTOCATPriceDIMOQ item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_PriceMOQ_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPriceDIMOQ FLMPrice_DI_PriceMOQ_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region partner
        public DTOResult FLMPrice_DI_PriceMOQ_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Partner_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Partner_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Partner_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQ_Partner_PartnerNotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region province

        public DTOResult FLMPrice_DI_PriceMOQ_Province_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Province_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Province_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Province_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Province_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQ_Province_NotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Province_NotInList(request, priceExID, contractID, CustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region group location
        public DTOResult FLMPrice_DI_PriceMOQ_GroupLocation_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_GroupLocation_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_GroupLocation_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_GroupLocation_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_GroupLocation_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region groupproduct
        public DTOResult FLMPrice_DI_PriceMOQ_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_GroupProduct_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_GroupProduct_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQGroupProduct FLMPrice_DI_PriceMOQ_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_GroupProduct_Get(id, cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_GroupProduct_Save(DTOPriceDIMOQGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_GroupProduct_Save(item, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQ_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_GroupProduct_GOPList(cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region  location
        public DTOResult FLMPrice_DI_PriceMOQ_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQLocation FLMPrice_DI_PriceMOQ_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Location_Save(DTOPriceDIMOQLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQ_Location_LocationNotInList(string request, int priceMOQID, int custormerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Location_LocationNotInList(request, priceMOQID, custormerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region route
        public DTOResult FLMPrice_DI_PriceMOQ_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQ_Route_RouteNotInList(string request, int priceMOQID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_Route_RouteNotInList(request, priceMOQID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region parent route
        public DTOResult FLMPrice_DI_PriceMOQ_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQ_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQ_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(string request, int priceMOQID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(request, priceMOQID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region level

        #endregion

        #endregion

        #region Unload

        #region comon
        public void FLMPrice_DI_UnLoad_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoad_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_UnLoad_DeleteList(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoad_DeleteList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_UnLoad_DeleteAllList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoad_DeleteAllList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Location
        public List<DTOPriceTruckDILoad> FLMPrice_DI_UnLoadLocation_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadLocation_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_UnLoadLocation_LocationNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadLocation_LocationNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadLocation_LocationNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadLocation_LocationNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadLocation_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadLocation_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_UnLoadLocation_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadLocation_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceTruckDILoad_Export FLMPrice_DI_UnLoadLocation_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadLocation_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadLocation_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadLocation_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Route
        public List<DTOPriceTruckDILoad> FLMPrice_DI_UnLoadRoute_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadRoute_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_UnLoadRoute_RouteNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadRoute_RouteNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadRoute_RouteNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadRoute_RouteNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadRoute_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadRoute_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_UnLoadRoute_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadRoute_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceTruckDILoad_Export FLMPrice_DI_UnLoadRoute_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadRoute_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadRoute_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadRoute_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region TypeOfPartner
        public List<DTOPriceTruckDILoad> FLMPrice_DI_UnLoadPartner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadPartner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_UnLoadPartner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadPartner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadPartner_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadPartner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_UnLoadPartner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadPartner_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceTruckDILoad_Export FLMPrice_DI_UnLoadPartner_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadPartner_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadPartner_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadPartner_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Partner
        public List<DTOPriceDILoadPartner> FLMPrice_DI_UnLoadPartner_Partner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadPartner_Partner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_UnLoadPartner_Partner_SaveList(List<DTOPriceDILoadPartner> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadPartner_Partner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_UnLoadPartner_Partner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_UnLoadPartner_Partner_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region MOQ
        #region info
        public DTOResult FLMPrice_DI_PriceMOQUnLoad_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQUnLoad_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMPrice_DI_PriceMOQUnLoad_Save(DTOPriceDIMOQLoad item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQUnLoad_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_PriceMOQUnLoad_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQUnLoad_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIMOQLoad FLMPrice_DI_PriceMOQUnLoad_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceMOQUnLoad_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceMOQUnLoad_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceMOQUnLoad_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion
        #endregion

        #region Area

        #endregion

        #endregion

        #endregion
        #endregion

        #region khấu hao cua xe(new)
        public List<DTOFLMFixedCost> FLMFixedCost_ByAssetList(int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMFixedCost_ByAssetList(assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMFixedCost> FLMFixedCost_Generate(int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMFixedCost_Generate(assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMFixedCost_Save(List<DTOFLMFixedCost> lst, int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMFixedCost_Save(lst, assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMFixedCost_ByAssetDelete(int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMFixedCost_ByAssetDelete(assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public bool FLMFixedCost_ByAsset_CheckExpr(string sExpression, int assetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMFixedCost_ByAsset_CheckExpr(sExpression, assetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region tính khấu hao
        public DTOResult FLMFixedCost_List(string request, int month, int year)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMFixedCost_List(request, month, year);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMFixedCost_SaveList(List<int> lstAsset, int month, int year)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMFixedCost_SaveList(lstAsset, month, year);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMFixedCost_DeleteList(List<int> lstAsset, int month, int year)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMFixedCost_DeleteList(lstAsset, month, year);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMFixedCost> FLMFixedCost_ReceiptList(int assetID, int month, int year)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMFixedCost_ReceiptList(assetID, month, year);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Bảng lương tài xế
        public void FLMDriverFee_Import(int contractID, List<DTOCATDriverFee_Import> lstData)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriverFee_Import(contractID, lstData);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATDriverFee_Export_Data FLMDriverFee_Export(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverFee_Export(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATDriverFee_Import_Data FLMDriverFee_Import_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverFee_Import_Data();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOCATDriverFeeTemp_Export> FLMDriverFeeTemp_Export(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMDriverFeeTemp_Export(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMDriverFeeTemp_Import(int contractID, List<DTOCATDriverFeeTemp_Import> lstData)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMDriverFeeTemp_Import(contractID, lstData);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FLM_VehiclePlan
        public DTOFLMVehiclePlanData FLMVehiclePlan_Data(List<int> lstAssetID, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMVehiclePlan_Data(lstAssetID, dateFrom, dateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMVehiclePlan FLMVehiclePlan_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMVehiclePlan_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMVehiclePlan_Save(DTOFLMVehiclePlan item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMVehiclePlan_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMVehiclePlan_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMVehiclePlan_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMVehiclePlan_VehicleList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMVehiclePlan_VehicleList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMVehiclePlan_DriverList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMVehiclePlan_DriverList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FLM_ScheduleFeeDefault (phụ phí tháng trong chi tiết asset)
        public DTOResult FLMTypeDriverCost_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTypeDriverCost_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMTypeDriverCost FLMTypeDriverCost_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMTypeDriverCost_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMTypeDriverCost_Save(DTOFLMTypeDriverCost item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMTypeDriverCost_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMTypeDriverCost_Delete(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMTypeDriverCost_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Phiếu hành trình
        public DTOResult FLMPHTDIMaster_List(string request, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTDIMaster_List(request, dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMPHTDIMaster_Save(DTOFLMMasterPHT item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTDIMaster_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTDIMaster_DeleteList(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTDIMaster_DeleteList(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public List<DTOFLMMasterPHT_Trouble> FLMPHTDIMaster_TroubleList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTDIMaster_TroubleList(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTDIMaster_TroubleSaveList(List<DTOFLMMasterPHT_Trouble> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTDIMaster_TroubleSaveList(lst, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTDIMaster_TroubleDeleteList(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTDIMaster_TroubleDeleteList(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public List<DTOFLMMasterPHT_Station> FLMPHTDIMaster_StationList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTDIMaster_StationList(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTDIMaster_StationSaveList(List<DTOFLMMasterPHT_Station> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTDIMaster_StationSaveList(lst, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPHTDIMaster_StationNotInList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTDIMaster_StationNotInList(request, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTDIMaster_StationDeleteList(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTDIMaster_StationDeleteList(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public DTOResult FLMPHTCOMaster_List(string request, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTCOMaster_List(request, dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMPHTCOMaster_Save(DTOFLMMasterPHT item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTCOMaster_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTCOMaster_DeleteList(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTCOMaster_DeleteList(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOFLMMasterPHT_Trouble> FLMPHTCOMaster_TroubleList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTCOMaster_TroubleList(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTCOMaster_TroubleSaveList(List<DTOFLMMasterPHT_Trouble> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTCOMaster_TroubleSaveList(lst, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTCOMaster_TroubleDeleteList(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTCOMaster_TroubleDeleteList(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOFLMMasterPHT_Station> FLMPHTCOMaster_StationList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTCOMaster_StationList(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTCOMaster_StationSaveList(List<DTOFLMMasterPHT_Station> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTCOMaster_StationSaveList(lst, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMPHTCOMaster_StationNotInList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTCOMaster_StationNotInList(request, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTCOMaster_StationDeleteList(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTCOMaster_StationDeleteList(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMMasterPHT_Import_Data FLMPHTDIMaster_Import_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTDIMaster_Import_Data();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMMasterPHT_Export FLMPHTDIMaster_Export(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTDIMaster_Export(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTDIMaster_Import(List<DTOFLMMasterPHT_Import> lstData)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTDIMaster_Import(lstData);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMMasterPHT_Import_Data FLMPHTCOMaster_Import_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTCOMaster_Import_Data();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMMasterPHT_Export FLMPHTCOMaster_Export(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPHTCOMaster_Export(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPHTCOMaster_Import(List<DTOFLMMasterPHT_Import> lstData)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPHTCOMaster_Import(lstData);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FLMSetting_Vendor
        public DTOResult FLMSetting_Vendor_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Vendor_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMSetting_VendorNotIn_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_VendorNotIn_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_Vendor_SaveList(List<DTOCUSCompany> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_Vendor_SaveList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_Vendor_DeleteList(List<DTOCUSCompany> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_Vendor_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMSetting_Price_Copy(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Price_Copy(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_PriceCopy_SaveList(int ContractID, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_PriceCopy_SaveList(ContractID, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region danh sách địa chỉ
        public DTOResult FLMSetting_Location_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_Location_SaveList( List<CATLocation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_Location_SaveList( lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_Location_Delete(int cuslocationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_Location_Delete(cuslocationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public AddressSearchItem AddressSearch_List(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.AddressSearch_List(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<AddressSearchItem> AddressSearch_ByCustomerList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.AddressSearch_ByCustomerList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public DTOResult FLMSetting_Location_NotInList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_NotInList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMSetting_Location_HasRun(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_HasRun(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMSetting_Location_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMSetting_Location_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMSetting_Location_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMSetting_Location_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMSetting_Location_RoutingContract_List(string request, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_RoutingContract_List(request, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_Location_RoutingContract_SaveList(List<int> lstAreaClear, List<int> lstAreaAdd, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_Location_RoutingContract_SaveList(lstAreaClear, lstAreaAdd, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_Location_RoutingContract_NewRoutingSave(DTOCUSPartnerNewRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_Location_RoutingContract_NewRoutingSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCUSPartnerNewRouting FLMSetting_Location_RoutingContract_NewRoutingGet()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_RoutingContract_NewRoutingGet();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOCATContract> FLMSetting_Location_RoutingContract_ContractData()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_RoutingContract_ContractData();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMSetting_Location_RoutingContract_NewAreaSave(CATRoutingArea item, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_Location_RoutingContract_NewAreaSave(item, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMSetting_Location_RoutingContract_AreaList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_Location_RoutingContract_AreaList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region  FLMAssetWarning
        public List<DTOFLMAssetTypeWarning> Get_TypeWarning()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Get_TypeWarning();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMAsset_Warning_List(string request, int TypeWarningID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Warning_List(request, TypeWarningID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMAsset_Warning_NoInList(int TypeWarningID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAsset_Warning_NoInList(TypeWarningID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMAsset_Warning_SaveNoInList(List<DTOFLMAsset> lst, int TypeWarningID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMAsset_Warning_SaveNoInList(lst, TypeWarningID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMAsset_Warning_SaveList(List<DTOFLMAssetWarning> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMAsset_Warning_SaveList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMAsset_Warning_Delete(DTOFLMAssetWarning item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMAsset_Warning_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMAssetWarning_ExcelInit(int TypeWarningID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAssetWarning_ExcelInit(TypeWarningID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMAssetWarning_ExcelChange(int TypeWarningID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAssetWarning_ExcelChange(TypeWarningID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMAssetWarning_ExcelImport(int TypeWarningID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAssetWarning_ExcelImport(TypeWarningID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMAssetWarning_ExcelApprove(int TypeWarningID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMAssetWarning_ExcelApprove(TypeWarningID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FLMSetting_GroupOfProduct
        public DTOResult FLMSetting_GroupOfProductAll_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_GroupOfProductAll_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMSetting_GroupOfProduct_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_GroupOfProduct_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMSetting_GroupOfProduct_Save(DTOCUSGroupOfProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_GroupOfProduct_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMSetting_GroupOfProduct_Delete(DTOCUSGroupOfProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_GroupOfProduct_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSGroupOfProduct FLMSetting_GroupOfProduct_GetByCode(string Code)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_GroupOfProduct_GetByCode(Code);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMSetting_GroupOfProduct_ResetPrice()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_GroupOfProduct_ResetPrice();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMSetting_GroupOfProductMapping_List(string request, int groupOfProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_GroupOfProductMapping_List(request, groupOfProductID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMSetting_GroupOfProductMappingNotIn_List(string request, int groupOfProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMSetting_GroupOfProductMappingNotIn_List(request, groupOfProductID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMSetting_GroupOfProductMapping_SaveList(List<DTOCUSGroupOfProductMapping> lst, int groupOfProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_GroupOfProductMapping_SaveList(lst, groupOfProductID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMSetting_GroupOfProductMapping_Delete(DTOCUSGroupOfProductMapping item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMSetting_GroupOfProductMapping_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region GroupOfProduct

        public DTOResult FLMContract_GroupOfProduct_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_GroupOfProduct_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATContractGroupOfProduct FLMContract_GroupOfProduct_Get(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_GroupOfProduct_Get(id, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_GroupOfProduct_Save(DTOCATContractGroupOfProduct item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_GroupOfProduct_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMContract_GroupOfProduct_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMContract_GroupOfProduct_Delete(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public double? FLMContract_GroupOfProduct_Check(DTOCATContractGroupOfProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_GroupOfProduct_Check(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMContract_GroupOfProduct_ExcelInit(int contractID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_GroupOfProduct_ExcelInit(contractID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row FLMContract_GroupOfProduct_ExcelChange(int contractID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_GroupOfProduct_ExcelChange(contractID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMContract_GroupOfProduct_ExcelImport(int contractID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_GroupOfProduct_ExcelImport(contractID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMContract_GroupOfProduct_ExcelApprove(int contractID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMContract_GroupOfProduct_ExcelApprove(contractID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region GroupProduct
        public List<DTOPriceDIGroupOfProduct> FLMPrice_DI_GroupProduct_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupProduct_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_GroupProduct_SaveList(List<DTOPriceDIGroupOfProduct> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_GroupProduct_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIGroupOfProductData FLMPrice_DI_GroupProduct_Export(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupProduct_Export(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMPrice_DI_GroupProduct_Import(List<DTOPriceDIGroupOfProductImport> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_GroupProduct_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_GroupProduct_ExcelInit(bool isFrame, int priceID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupProduct_ExcelInit(isFrame, priceID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row FLMPrice_DI_GroupProduct_ExcelChange(bool isFrame, int priceID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupProduct_ExcelChange(isFrame, priceID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_GroupProduct_ExcelOnImport(bool isFrame, int priceID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupProduct_ExcelOnImport(isFrame, priceID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMPrice_DI_GroupProduct_ExcelApprove(bool isFrame, int priceID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_GroupProduct_ExcelApprove(isFrame, priceID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region PriceDI level
        public List<DTOPriceDILevelGroupProduct> FLMPrice_DI_PriceLevel_DetailData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceLevel_DetailData(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceLevel_Save(List<DTOPriceDILevelGroupProduct> lst, int priceid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceLevel_Save(lst, priceid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDILevelData FLMPrice_DI_PriceLevel_ExcelData(int priceid, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceLevel_ExcelData(priceid, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMPrice_DI_PriceLevel_ExcelImport(List<DTOPriceDILevelImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMPrice_DI_PriceLevel_ExcelImport(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_PriceLevel_ExcelInit(int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceLevel_ExcelInit(priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row FLMPrice_DI_PriceLevel_ExcelChange(int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceLevel_ExcelChange(priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMPrice_DI_PriceLevel_OnExcelImport(int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceLevel_OnExcelImport(priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMPrice_DI_PriceLevel_ExcelApprove(int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMPrice_DI_PriceLevel_ExcelApprove(priceID, contractTermID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Location
        #region Đối tác
        public DTOResult Partner_List(string request, int typeofpartnerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Partner_List(request, typeofpartnerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult PartnerNotIn_List(string request, int typePartner)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerNotIn_List(request, typePartner);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<int> Partner_SaveList(List<DTOCUSPartnerAll> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Partner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Partner_Delete(int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.Partner_Delete(cuspartnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int Partner_Save(DTOCUSPartnerAllCustom item, int typeOfPartner)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Partner_Save(item, typeOfPartner);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOPartnerLocation_Excel> PartnerLocation_Export( bool isCarrier, bool isSeaport, bool isDistributor)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_Export(isCarrier, isSeaport, isDistributor);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPartnerLocation_Check PartnerLocation_Check()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_Check();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<AddressSearchItem> PartnerLocation_Import(List<DTOPartnerImport> lst, bool isCarrier, bool isSeaport, bool isDistributor)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_Import(lst, isCarrier, isSeaport, isDistributor);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult PartnerLocation_List(string request, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_List(request, cuspartnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOCUSLocation> PartnerLocation_SaveList(List<DTOCUSLocation> data, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_SaveList(data, cuspartnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult PartnerLocation_NotInList(string request, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_NotInList(request, cuspartnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<int> PartnerLocation_SaveNotinList(List<int> lst, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_SaveNotinList(lst, cuspartnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCUSPartnerLocation PartnerLocation_Save(DTOCUSPartnerLocation item, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_Save(item, cuspartnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCUSPartnerLocation PartnerLocation_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.PartnerLocation_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void PartnerLocation_Delete(DTOCUSLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.PartnerLocation_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void PartnerLocation_DeleteList(List<DTOCUSLocation> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.PartnerLocation_DeleteList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion
        public List<DTOCUSPartnerLocationAll> FLM_Partner_List(List<int> lstPartner, List<int> lstLocation, bool isUseLocation)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_List(lstPartner, lstLocation, isUseLocation);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCUSPartnerAllCustom FLM_Partner_Get(int id, int typeid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_Get(id, typeid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLM_Partner_CUSLocationSaveCode(DTOCUSPartnerLocationAll item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_CUSLocationSaveCode(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region excel onl mới
        public SYSExcel FLM_Partner_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLM_Partner_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLM_Partner_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLM_Partner_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region filter
        public DTOResult FLM_Partner_FilterByPartner_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_FilterByPartner_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLM_Partner_FilterByLocation_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_FilterByLocation_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<int> FLM_Partner_FilterByPartner_GetNumOfCusLocation()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_FilterByPartner_GetNumOfCusLocation();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region routing contract
        public DTOResult FLM_Partner_RoutingContract_List(string request, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_RoutingContract_List(request, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLM_Partner_RoutingContract_SaveList(List<int> lstAreaClear, List<int> lstAreaAdd, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLM_Partner_RoutingContract_SaveList(lstAreaClear, lstAreaAdd, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLM_Partner_RoutingContract_NewRoutingSave(DTOCUSPartnerNewRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLM_Partner_RoutingContract_NewRoutingSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCUSPartnerNewRouting FLM_Partner_RoutingContract_NewRoutingGet()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_RoutingContract_NewRoutingGet();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOCATContract> FLM_Partner_RoutingContract_ContractData()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_RoutingContract_ContractData();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLM_Partner_RoutingContract_NewAreaSave(CATRoutingArea item, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLM_Partner_RoutingContract_NewAreaSave(item, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLM_Partner_RoutingContract_AreaList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLM_Partner_RoutingContract_AreaList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #endregion

        #region Danh sách chuyến
        public DTOFLMOwner FLMOwner_MasterList(DateTime? dtfrom, DateTime? dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMOwner_MasterList(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMOwner_Master FLMOwner_MasterDetailList(int vehicleID, DateTime? dtfrom, DateTime? dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMOwner_MasterDetailList(vehicleID, dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMOwner_Receipt> FLMOwner_GenerateReceipt(List<DTOFLMOwner_Asset> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMOwner_GenerateReceipt(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMOwner_AcceptReceipt(List<DTOFLMOwner_Receipt> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMOwner_AcceptReceipt(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Bãi xe
        public DTOResult FLMStand_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMStand_Save(DTOCATStand item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATStand FLMStand_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMStand_Delete(DTOCATStand item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMStand_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMStand_Location_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_Location_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Truck_NotInList(string request, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Truck_NotInList(request,standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMStand_Truck_List(string request, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_Truck_List(request, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMStand_TruckSave(List<DTOFLMTruck> lstFLMTruck, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMStand_TruckSave(lstFLMTruck, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMStand_Truck_Delete(List<DTOFLMTruck> lstFLMTruck, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMStand_Truck_Delete(lstFLMTruck, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMStand_Tractor_List(string request, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_Tractor_List(request, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult Tractor_NotInList(string request, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Tractor_NotInList(request, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMStand_TractorSave(List<DTOFLMTractor> lstFLMTractor, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMStand_TractorSave(lstFLMTractor, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMStand_Tractor_Delete(List<DTOFLMTractor> lstFLMTractor, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMStand_Tractor_Delete(lstFLMTractor, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Romooc_NotInList(string request, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.Romooc_NotInList(request, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMStand_Romooc_List(string request, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_Romooc_List(request, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMStand_RomoocSave(List<DTOFLMRomooc> lstFLMRomooc, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMStand_RomoocSave(lstFLMRomooc, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMStand_Romooc_Delete(List<DTOFLMRomooc> lstFLMRomooc, int standID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMStand_Romooc_Delete(lstFLMRomooc, standID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMStand_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMStand_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMStand_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMStand_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMStand_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Đối tượng nhiên liệu
        public DTOResult FLMMaterialAudit_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterialAudit_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMMaterialAudit_Save(DTOFLMMaterialAudit item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterialAudit_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMMaterialAudit FLMMaterialAudit_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterialAudit_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMMaterialAudit_Generate(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMMaterialAudit_Generate(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMMaterialAudit_Close(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMMaterialAudit_Close(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMMaterialAudit_Open(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMMaterialAudit_Open(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMMaterialAudit_Delete(DTOFLMMaterialAudit item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    bl.FLMMaterialAudit_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_FLMMaterialAuditStatus()
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.ALL_FLMMaterialAuditStatus();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMMaterialAudit_DITOMaster_List(string request, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterialAudit_DITOMaster_List(request, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMMaterialAudit_COTOMaster_List(string request, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterialAudit_COTOMaster_List(request, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMMaterialAudit_Receipt_List(string request, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterialAudit_Receipt_List(request, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FLMMaterialAudit_Result_List(string request, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFleetManage>())
                {
                    return bl.FLMMaterialAudit_Result_List(request, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion
    }
}

