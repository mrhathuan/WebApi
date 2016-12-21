/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingOrderDetail = {
    URL: {
        Get_StockByCus: 'CUSSettingORD_StockByCus_List',
        Get: 'CUSSettingOrder_Get',
        Save: 'CUSSettingOrder_Save',
        Customer_List: 'Customer_AllList',
    },
    Data: {
        DataCustomer: [],
        DataTransport: [
            { ValueOfVar: 'FTL', ID: 33 , Type: 1 },
            { ValueOfVar: 'LTL', ID: 34 , Type: 1 },
            { ValueOfVar: 'FCL', ID: 31 , Type: 2 },
            { ValueOfVar: 'LCL', ID: 32 , Type: 2 },
        ],
        DataContract: [],
        GridAllSetting: [
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "ServiceOfOrder", Name: "Loại dịch vụ" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "TypeOfTransportMode", Name: "Loại vận chuyển" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "OrderCode", Name: "Mã đơn hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "SOCode", Name: "Số SO" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DNCode", Name: "Số DN" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "RequestDate", Name: "Ngày gửi y/c" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "RequestTime", Name: "Giờ gửi y/c" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "RequestDate_Time", Name: "Ngày-giờ y/c" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "ETD", Name: "ETD" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETDTime_RequestDate", Name: "ETD tính theo giờ" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETDRequest", Name: "Ngày y/c lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETDRequestTime", Name: "Giờ y/c lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "ETA", Name: "ETA" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETATime_RequestDate", Name: "ETA tính theo giờ" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETARequest", Name: "Ngày y/c giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETARequestTime", Name: "Giờ y/c giao hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "CustomerCode", Name: "Mã Khách hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CustomerName", Name: "Tên khách hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "CustomerCodeName", Name: "Mã-Tên khách hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "DistributorCode", Name: "Mã Nhà phân phối" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DistributorName", Name: "Tên Nhà phân phối" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "DistributorCodeName", Name: "Mã-Tên Nhà phân phối" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationFromCode", Name: "Mã điểm lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationFromName", Name: "Tên điểm lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationFromCodeName", Name: "Mã-Tên điểm lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationToCode", Name: "Mã điểm giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToName", Name: "Tên điểm giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToAddress", Name: "Địa chỉ" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationToCodeName", Name: "Mã-Tên điểm giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToNote", Name: "Ghi chú điểm đến" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToNote1", Name: "Ghi chú điểm đến 1" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "GroupProductCode", Name: "Mã nhóm hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "GroupProductCodeNotUnicode", Name: "Mã nhóm hàng không dấu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TemperatureMax", Name: "Nhiệt độ tối đa" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TemperatureMin", Name: "Nhiệt độ tối thiểu" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "Packing", Name: "Đơn vị tính" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "PackingNotUnicode", Name: "Đơn vị tính không dấu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Ton", Name: "Ton" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "IsHot", Name: "Đơn hàng gấp" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CBM", Name: "CBM" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Quantity", Name: "Số lượng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "GroupVehicle", Name: "Loại xe" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "RoutingCode", Name: "Mã cung đường" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Note", Name: "Ghi chú" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "HasCashCollect", Name: "Thu hộ" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "EconomicZone", Name: "EconomicZone" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceTOMaster", Name: "Giá chuyến" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceTon", Name: "Giá Tấn" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceCBM", Name: "Giá khối" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceQuantity", Name: "Giá số lượng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine1", Name: "UserDefine1" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine2", Name: "UserDefine2" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine3", Name: "UserDefine3" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine4", Name: "UserDefine4" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine5", Name: "UserDefine5" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine6", Name: "UserDefine6" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine7", Name: "UserDefine7" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine8", Name: "UserDefine8" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine9", Name: "UserDefine9" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Note1", Name: "Ghi chú 1" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Note2", Name: "Ghi chú 2" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CutOffTime", Name: "CutOffTime" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "CarrierCode", Name: "Mã Hãng Tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CarrierName", Name: "Tên Hãng Tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CarrierCodeName", Name: "Mã - Tên Hãng Tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "VesselNo", Name: "Số hiệu tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "VesselName", Name: "Tên tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TripNo", Name: "Số chuyến" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ContainerNo", Name: "Số container" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "SealNo1", Name: "Số Seal thứ nhất" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "SealNo2", Name: "Số Seal thứ hai" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "TypeOfContainerName", Name: "Loại container" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationDepotCode", Name: "Mã điểm lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationDepotName", Name: "Tên điểm lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationReturnCode", Name: "Mã điểm trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationReturnName", Name: "Tên điểm trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DateGetEmpty", Name: "Ngày lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Date_TimeGetEmpty", Name: "Ngày-giờ lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TimeGetEmpty", Name: "Giờ lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DateReturnEmpty", Name: "Ngày trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TimeReturnEmpty", Name: "Giờ trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Date_TimeReturnEmpty", Name: "Ngày-Giờ trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TypeOfWAInspectionStatus", Name: "Miễn kiểm hoá" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "InspectionDate", Name: "Thời gian gửi tờ khai kiểm hoá" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ProductCodeWithoutGroup", Name: "Mã hàng hóa không cần nhóm" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Ton_SKU", Name: "Tấn SKU" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CBM_SKU", Name: "Khối SKU" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Quantity_SKU", Name: "Số lượng SKU" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "RoutingAreaCode", Name: "Mã khu vực" },
        ],
        GridSelected: [
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'A', Code: "", Name: "", IndexColumn: 1, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'B', Code: "", Name: "", IndexColumn: 2, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'C', Code: "", Name: "", IndexColumn: 3, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'D', Code: "", Name: "", IndexColumn: 4, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'E', Code: "", Name: "", IndexColumn: 5, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'F', Code: "", Name: "", IndexColumn: 6, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'G', Code: "", Name: "", IndexColumn: 7, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'H', Code: "", Name: "", IndexColumn: 8, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'I', Code: "", Name: "", IndexColumn: 9, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'J', Code: "", Name: "", IndexColumn: 10, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'K', Code: "", Name: "", IndexColumn: 11, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'L', Code: "", Name: "", IndexColumn: 12, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'M', Code: "", Name: "", IndexColumn: 13, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'N', Code: "", Name: "", IndexColumn: 14, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'O', Code: "", Name: "", IndexColumn: 15, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'P', Code: "", Name: "", IndexColumn: 16, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'Q', Code: "", Name: "", IndexColumn: 17, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'R', Code: "", Name: "", IndexColumn: 18, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'S', Code: "", Name: "", IndexColumn: 19, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'T', Code: "", Name: "", IndexColumn: 20, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'U', Code: "", Name: "", IndexColumn: 21, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'V', Code: "", Name: "", IndexColumn: 22, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'W', Code: "", Name: "", IndexColumn: 23, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'X', Code: "", Name: "", IndexColumn: 24, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'Y', Code: "", Name: "", IndexColumn: 25, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'Z', Code: "", Name: "", IndexColumn: 26, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AA', Code: "", Name: "", IndexColumn: 27, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AB', Code: "", Name: "", IndexColumn: 28, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AC', Code: "", Name: "", IndexColumn: 29, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AD', Code: "", Name: "", IndexColumn: 30, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AE', Code: "", Name: "", IndexColumn: 31, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AF', Code: "", Name: "", IndexColumn: 32, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AG', Code: "", Name: "", IndexColumn: 33, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AH', Code: "", Name: "", IndexColumn: 34, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AI', Code: "", Name: "", IndexColumn: 35, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AJ', Code: "", Name: "", IndexColumn: 36, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AK', Code: "", Name: "", IndexColumn: 37, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AL', Code: "", Name: "", IndexColumn: 38, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AM', Code: "", Name: "", IndexColumn: 39, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AN', Code: "", Name: "", IndexColumn: 40, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AO', Code: "", Name: "", IndexColumn: 41, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AP', Code: "", Name: "", IndexColumn: 42, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AQ', Code: "", Name: "", IndexColumn: 43, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AR', Code: "", Name: "", IndexColumn: 44, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AS', Code: "", Name: "", IndexColumn: 45, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AT', Code: "", Name: "", IndexColumn: 46, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AU', Code: "", Name: "", IndexColumn: 47, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AV', Code: "", Name: "", IndexColumn: 48, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AW', Code: "", Name: "", IndexColumn: 49, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AX', Code: "", Name: "", IndexColumn: 50, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AY', Code: "", Name: "", IndexColumn: 51, StockID: 0 },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, Column: 'AZ', Code: "", Name: "", IndexColumn: 52, StockID: 0 },
        ],
        TypeTransport: [
            { ValueOfVar: 'FTL', ID: 33, Type: 1 },
            { ValueOfVar: 'LTL', ID: 34, Type: 1 },
            { ValueOfVar: 'FCL', ID: 31, Type: 2 },
            { ValueOfVar: 'LCL', ID: 32, Type: 2 },
            { ValueOfVar: 'Tự thiết lập', ID: 0 },
        ],
        Service: [
            { ValueOfVar: 'Tự thiết lập', ID: 0 },
            { ValueOfVar: 'Nhập khẩu', ID: 26 },
            { ValueOfVar: 'Xuất khẩu', ID: 27 },
            { ValueOfVar: 'Nội địa', ID: 28 },
        ],
        Stock: [],
        StockDetail: [],
        Packing: []
    },
    Param: { id: 0 }
}

angular.module('myapp').controller('CUSSettingOrder_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingOrder_DetailCtrl');
    $rootScope.IsLoading = false;
    $scope.IsShowCbo = true;
    $scope.ItemContract = [];
    $scope.DataContract = {};
    _CUSSettingOrderDetail.Param = $.extend(true, _CUSSettingOrderDetail.Param, $state.params);

    $scope.TranferItem = null;
    $scope.RevertItem = null;
    $scope.PreItem = null;
    $scope.DataContractList = {};
    $scope.disableCus = false;

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                CustomerName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                $rootScope.IsLoading = true;
                var val = this.value();

                if (val > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSSettingOrderDetail.URL.Get_StockByCus,
                        data: { CusID: val },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            var data = $scope.AllSettingGrid.dataSource.data();
                            var dataFilter = [];
                            var newData = [];
                            Common.Data.Each(data, function (o) {
                                //if (o.Code != "CustomerCode" && o.Code != "CustomerName")
                                dataFilter.push(o);
                            })
                            _CUSSettingOrderDetail.Data.Stock = [];
                            _CUSSettingOrderDetail.Data.StockDetail = [];
                            _CUSSettingOrderDetail.Data.Packing = [];
                            _CUSSettingOrderDetail.Data.Stock = res.ListStock;
                            _CUSSettingOrderDetail.Data.StockDetail = res.ListDetail;
                            _CUSSettingOrderDetail.Data.Packing = res.ListPacking;
                            if ($scope.Item.HasStock) {
                                Common.Data.Each(dataFilter, function (o) {
                                    if (!o.IsStock) newData.push(o);
                                })
                                Common.Data.Each(_CUSSettingOrderDetail.Data.Stock, function (o) {
                                    newData.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_Ton", Name: o.LocationName + "-Tấn", StockID: o.ID, Type: 1 }),
                                    newData.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_CBM", Name: o.LocationName + "-Khối", StockID: o.ID, Type: 2 })
                                    newData.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_Quantity", Name: o.LocationName + "-Số lượng", StockID: o.ID, Type: 3 })
                                })
                                $scope.AllSettingGrid.dataSource.data(newData);
                            }
                            else if ($scope.Item.HasStockProduct) {
                                Common.Data.Each(dataFilter, function (o) {
                                    if (!o.IsStock) newData.push(o);
                                })
                                Common.Data.Each(_CUSSettingOrderDetail.Data.StockDetail, function (o) {
                                    newData.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Ton", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Tấn", StockID: o.StockID, Type: 1 }),
                                    newData.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_CBM", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Khối", StockID: o.StockID, Type: 2 })
                                    newData.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Quantity", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Số lượng", StockID: o.StockID, Type: 3 })
                                });
                                $scope.AllSettingGrid.dataSource.data(newData);
                            } else if ($scope.Item.HasContainer) {
                                Common.Data.Each(dataFilter, function (o) {
                                    if (!o.IsContainer) newData.push(o);
                                })
                                Common.Data.Each(_CUSSettingOrderDetail.Data.Packing, function (o) {
                                    dataAll.push({ PackingID: o.PackingID, Required: false, IsContainer: true, Code: o.PackingCode + "_Cont", Name: o.PackingCode + "-Loại cont" });
                                });
                                $scope.AllSettingGrid.dataSource.data(newData);
                            } else {
                                $scope.AllSettingGrid.dataSource.data(dataFilter);
                            }

                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                            this.value("");
                        }
                    });
                }
                else if (val == 0) {
                    var data = $scope.AllSettingGrid.dataSource.data();
                    var newData = [];
                    Common.Data.Each(data, function (o) {
                        if (!o.IsStock) newData.push(o);
                    })

                    $scope.Item.HasStock = false;

                    $scope.AllSettingGrid.dataSource.data(newData)
                    $rootScope.IsLoading = false;
                }
                //$timeout(function () {
                //    $scope.LoadContract()
                //}, 100)
            }
        }
    }

    //Common.ALL.Get($http, {
    //    url: Common.ALL.URL.Customer,
    //    success: function (data) {
    //        var newData = [];
    //        newData.push({ ID: 0, CustomerName: "Tất cả" })
    //        Common.Data.Each(data, function (o) {
    //            newData.push(o)
    //        });
    //        $scope.cboCustomer_Options.dataSource.data(newData);
    //    }
    //})
    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSSettingOrderDetail.URL.Customer_List,
        data: { },
        success: function (res) {
            var newData = [];
            newData.push({ ID: 0, CustomerName: "Tất cả" })
            Common.Data.Each(res.Data, function (o) {
                newData.push(o)
            });
            $scope.cboCustomer_Options.dataSource.data(newData);
        }
    });

    $scope.cboType_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            },
        }),
        change: function (e) {
            var val = this.value();
            Common.Log("TransportMode:" + val);
            var cbx = this;
            if (e.sender.selectedIndex >= 0) {
                var object = cbx.dataItem(cbx.select());
                if (object != null) {
                    $scope.Change_StockProduct();
                }
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (res) {
            $timeout(function () {
                var data = [];
                data.push({ ValueOfVar: 'Tự thiết lập', ID: 0 });
                angular.forEach(res, function (value, key) {
                    data.push(value);
                });
                $scope.cboType_Options.dataSource.data(data);
            }, 1);
        }
    });

    //$scope.cboType_Options.dataSource.data(_CUSSettingOrderDetail.Data.TypeTransport);

    $scope.cboService_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            var val = this.value();
            Common.Log("ServiceMode:" + val);
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (res) {
            $timeout(function () {
                var data = [];
                data.push({ ValueOfVar: 'Tự thiết lập', ID: 0 });
                angular.forEach(res, function (value, key) {
                    data.push(value);
                });
                $scope.cboService_Options.dataSource.data(data);
            }, 1);
        }
    });

    //$scope.cboService_Options.dataSource.data(_CUSSettingOrderDetail.Data.Service);

    $scope.splitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: false, size: "50%", min: '250px' },
            { collapsible: false, resizable: false, size: "50%", min: '250px' }
        ]
    }

    $scope.AllSettingGrid_Options = {
        dataSource: Common.DataSource.Local([], {
            model: {
                //id: 'id',
                fields: {
                    IsStock: { type: 'boolean' },
                    IsContainer: { type: 'boolean' },
                    Code: { type: 'string' },
                    Name: { type: 'string' },
                }
            },
            pageSize: 0
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: 'row',
        columns: [
            { field: 'Code', width: "100px", title: 'Mã', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Name', width: "150px", title: 'Tên', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ],
        change: function (e) {
            var selected = this.select();
            if (selected.length > 0) {
                $scope.TranferItem = this.dataItem(selected);
                $timeout(function () {
                    $scope.RevertItem = null;
                    $scope.SelectedSettingGrid.clearSelection();
                }, 1)
            }
        },
        dataBound: function (e) {
            Common.Log("dataBound")
            var grid = this;

            if (Common.HasValue(grid.tbody) && Common.HasValue(grid)) {
                var data = grid.dataSource.data();
                Common.Data.Each(data, function (o) {
                    if (o.Required)
                        $('tr[data-uid="' + o.uid + '"] ').css({ "background-color": "rgb(255, 199, 199)" });
                })
            }
        }
    };


    $scope.numRowStart_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0 }

    // $scope.AllSettingGrid_Options.dataSource.data(_CUSSettingOrderDetail.Data.GridAllSetting)

    $scope.SelectedSettingGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'IndexColumn',
                fields: {
                    IndexColumn: { type: 'number' },
                }
            },
            pageSize: 0
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: false, selectable: 'row',
        columns: [
            { field: 'Column', width: "100px", title: 'Cột Excel' },
            { field: 'Code', width: "100px", title: 'Mã' },
            { field: 'Name', width: "150px", title: 'Tên' },
        ],
        change: function (e) {
            var selected = this.select();

            if (selected.length > 0) {
                var item = this.dataItem(selected);
                //co item chuuyen
                if (Common.HasValue($scope.TranferItem)) {
                    if (item.Code == "" && item.Name == "") {//chi chuyen vao item chua dc chọn
                        item.Code = $scope.TranferItem.Code;
                        item.Name = $scope.TranferItem.Name;
                        item.StockID = $scope.TranferItem.StockID;
                        item.Type = $scope.TranferItem.Type;
                        item.GOPID = $scope.TranferItem.GOPID;
                        item.ProductID = $scope.TranferItem.ProductID;
                        item.PackingID = $scope.TranferItem.PackingID;
                        item.IsStock = $scope.TranferItem.IsStock;
                        item.Required = $scope.TranferItem.Required;
                        item.IsContainer = $scope.TranferItem.IsContainer;

                        $timeout(function () {
                            $scope.AllSettingGrid_Options.dataSource.remove($scope.TranferItem);
                            $scope.TranferItem = null;
                            $scope.AllSettingGrid.clearSelection()
                            $scope.SelectedSettingGrid.clearSelection()
                        }, 1)
                    }
                }
                else {
                    //neu ko co item revert thi select row
                    if (!Common.HasValue($scope.RevertItem)) {
                        if (item.Code != "" && item.Name != "") {
                            $scope.RevertItem = item;
                            $scope.PreItem = item;
                        }
                        else $scope.RevertItem = null
                    }
                    else {
                        // co item revert => change loction
                        var itemBackup = $.extend(true, itemBackup, item)
                        item.Code = $scope.RevertItem.Code;
                        item.Name = $scope.RevertItem.Name;
                        item.StockID = $scope.RevertItem.StockID;
                        item.Type = $scope.RevertItem.Type;
                        item.GOPID = $scope.RevertItem.GOPID;
                        item.ProductID = $scope.RevertItem.ProductID;
                        item.PackingID = $scope.RevertItem.PackingID;
                        item.IsStock = $scope.RevertItem.IsStock;
                        item.Required = $scope.RevertItem.Required;
                        item.IsContainer = $scope.RevertItem.IsContainer;

                        $scope.PreItem.Code = itemBackup.Code;
                        $scope.PreItem.Name = itemBackup.Name;
                        $scope.PreItem.StockID = itemBackup.StockID;
                        $scope.PreItem.Type = itemBackup.Type;
                        $scope.PreItem.GOPID = itemBackup.GOPID;
                        $scope.PreItem.ProductID = itemBackup.ProductID;
                        $scope.PreItem.PackingID = itemBackup.PackingID;
                        $timeout(function () {
                            $scope.RevertItem = null;
                            $scope.PreItem = null;
                            $scope.AllSettingGrid.clearSelection()
                            $scope.SelectedSettingGrid.clearSelection()
                        }, 1)
                    }
                }
            }

        },
        dataBound: function (e) {
            Common.Log("dataBound")
            var grid = this;

            if (Common.HasValue(grid.tbody) && Common.HasValue(grid)) {
                var data = grid.dataSource.data();
                Common.Data.Each(data, function (o) {
                    if (o.Required)
                        $('tr[data-uid="' + o.uid + '"] ').css({ "background-color": "rgb(255, 199, 199)" });
                })
            }
        }
    }

    $scope.SelectedSettingGrid_Options.dataSource.data(_CUSSettingOrderDetail.Data.GridSelected)

    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        if (Common.HasValue($scope.Item.Name) && $scope.Item.Name != "") {
            if (Common.HasValue($scope.Item.ServiceOfOrderID) && $scope.Item.ServiceOfOrderID >= 0) {
                $rootScope.IsLoading = true;
                var data = $scope.SelectedSettingGrid.dataSource.data();

                $scope.Item.ListStock = [];
                var ItemSend = {
                    ListStock: [],
                    ListStockWithProduct: [],
                    ListContainer: [],
                    ID: $scope.Item.ID,
                    Name: $scope.Item.Name,
                    RowStart: $scope.Item.RowStart,
                    CustomerID: $scope.Item.CustomerID,
                    TypeOfTransportModeID: $scope.Item.TypeOfTransportModeID,
                    ContractID: $scope.Item.ContractID > 0 ? $scope.Item.ContractID : 0,
                    HasStock: $scope.Item.HasStock,
                    HasStockProduct: $scope.Item.HasStockProduct,
                    HasContainer: $scope.Item.HasContainer,
                    ServiceOfOrderID: $scope.Item.ServiceOfOrderID,
                    HasUseContract: $scope.Item.HasUseContract,
                    SuggestLocationToCode: $scope.Item.SuggestLocationToCode,
                    IsSkipDuplicate: $scope.Item.IsSkipDuplicate,
                }
                var objStock = {};
                var objStockProduct = {};
                var objContainer = {};
                var hasAddress = false;
                var isError = false;

                Common.Data.Each(data, function (o) {
                    if (o.Code != "" && o.Name != "") {
                        if (o.Code == "LocationToAddress")
                            hasAddress = true;
                        if (o.IsStock) {
                            if (o.StockID > 0) {// thuoc kho
                                if ($scope.Item.HasStock) {
                                    if (!Common.HasValue(objStock[o.StockID])) {
                                        objStock[o.StockID] = {
                                            Ton: 0,
                                            CBM: 0,
                                            Quantity: 0,
                                            StockID: o.StockID
                                        }
                                        ItemSend.ListStock.push(objStock[o.StockID])
                                    }
                                    if (o.Type == 1)//tấn
                                        objStock[o.StockID]['Ton'] = o.IndexColumn;
                                    else if (o.Type == 2)//khoi
                                        objStock[o.StockID]['CBM'] = o.IndexColumn;
                                    else objStock[o.StockID]['Quantity'] = o.IndexColumn;
                                }

                                if ($scope.Item.HasStockProduct) {
                                    if (!Common.HasValue(objStockProduct[o.StockID + "_" + o.GOPID + "_" + o.ProductID])) {
                                        objStockProduct[o.StockID + "_" + o.GOPID + "_" + o.ProductID] = {
                                            Ton: 0,
                                            CBM: 0,
                                            Quantity: 0,
                                            StockID: o.StockID,
                                            GroupOfProductID: o.GOPID,
                                            ProductID: o.ProductID
                                        }
                                        ItemSend.ListStockWithProduct.push(objStockProduct[o.StockID + "_" + o.GOPID + "_" + o.ProductID])
                                    }
                                    if (o.Type == 1)//tấn
                                        objStockProduct[o.StockID + "_" + o.GOPID + "_" + o.ProductID]['Ton'] = o.IndexColumn;
                                    else if (o.Type == 2)//khoi
                                        objStockProduct[o.StockID + "_" + o.GOPID + "_" + o.ProductID]['CBM'] = o.IndexColumn;
                                    else objStockProduct[o.StockID + "_" + o.GOPID + "_" + o.ProductID]['Quantity'] = o.IndexColumn;
                                }
                            }
                        } else {
                            if (o.IsContainer && $scope.Item.HasContainer) {
                                if (!Common.HasValue(objContainer[o.PackingID])) {
                                    objContainer[o.PackingID] = {
                                        PackingID: o.PackingID,
                                        PackingCOQuantity: 0,
                                    }
                                    ItemSend.ListContainer.push(objContainer[o.PackingID])
                                }
                                objContainer[o.PackingID]['PackingCOQuantity'] = o.IndexColumn;
                            } else {
                                ItemSend[o.Code] = o.IndexColumn;
                            }
                        }
                    }
                })

                if ($scope.Item.SuggestLocationToCode && !hasAddress) {
                    $rootScope.Message({ Msg: 'Chức năng gợi ý mã đ/giao: Chưa chọn cột địa chỉ giao', NotifyType: Common.Message.NotifyType.ERROR });
                    isError = true;
                }
                if (!isError) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSSettingOrderDetail.URL.Save,
                        data: { item: ItemSend, id: _CUSSettingOrderDetail.Param.id },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $state.go("main.CUSSettingOrder.Index")
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                } else {
                    $rootScope.IsLoading = false;
                }
            }
            else $rootScope.Message({ Msg: 'Chưa chọn dịch vụ', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else $rootScope.Message({ Msg: 'Thiếu tên', NotifyType: Common.Message.NotifyType.ERROR });

    }

    $scope.Change_Stock = function () {
        if ($scope.Item.HasStock) {

            if ($scope.Item.CustomerID > 0) {
                $rootScope.IsLoading = true;
                var data = $scope.AllSettingGrid.dataSource.data();
                Common.Data.Each(_CUSSettingOrderDetail.Data.Stock, function (o) {
                    data.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_Ton", Name: o.LocationName + "-Tấn", StockID: o.ID, Type: 1 }),
                    data.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_CBM", Name: o.LocationName + "-Khối", StockID: o.ID, Type: 2 }),
                    data.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_Quantity", Name: o.LocationName + "-Số lượng", StockID: o.ID, Type: 3 })
                })
                var newData = [];
                Common.Data.Each(data, function (o) {
                    if (o.Code != "Ton" && o.Code != "CBM" && o.Code != "Quantity")
                        newData.push(o)
                })
                $scope.AllSettingGrid.dataSource.data(newData);
                $rootScope.IsLoading = false;
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn khách hàng', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
        else {
            $rootScope.IsLoading = true;
            var data = $scope.AllSettingGrid.dataSource.data();
            var newData = [];
            Common.Data.Each(data, function (o) {
                if (!o.IsStock) newData.push(o);
            })
            newData.push({ GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Ton", Name: "Ton" })
            newData.push({ GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CBM", Name: "CBM" })
            newData.push({ GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Quantity", Name: "Số lượng" })
            $scope.AllSettingGrid.dataSource.data(newData);

            //reset settingselected grid
            Common.Data.Each($scope.SelectedSettingGrid.dataSource.data(), function (o) {
                if (o.StockID > 0) {
                    o.Name = ""; o.Code = ""; o.StockID = 0; o.Type = 0; o.GOPID = -1;
                    o.ProductID = -1;
                }
            })

            $rootScope.IsLoading = false;
        }
    }

    $scope.Change_StockProduct = function () {
        if ($scope.Item.HasStockProduct) {
            if ($scope.Item.CustomerID > 0) {
                $rootScope.IsLoading = true;
                var data = $scope.AllSettingGrid.dataSource.data();

                var data = $.grep(data, function (o) {
                    return o.IsStock == false;
                })
                Common.Data.Each(_CUSSettingOrderDetail.Data.StockDetail, function (o) {
                    data.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Ton", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Tấn", StockID: o.StockID, Type: 1 }),
                    data.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_CBM", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Khối", StockID: o.StockID, Type: 2 }),
                    data.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Quantity", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Số lượng", StockID: o.StockID, Type: 3 })
                })
                var newData = [];
                Common.Data.Each(data, function (o) {
                    if (o.Code != "Ton" && o.Code != "CBM" && o.Code != "Quantity")
                        newData.push(o)
                })
                $scope.AllSettingGrid.dataSource.data(newData);
                $rootScope.IsLoading = false;
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn khách hàng', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
        else {
            $rootScope.IsLoading = true;
            var data = $scope.AllSettingGrid.dataSource.data();
            var newData = [];
            Common.Data.Each(data, function (o) {
                if (!o.IsStock) newData.push(o);
            })

            $scope.AllSettingGrid.dataSource.data(newData);

            //reset settingselected grid
            Common.Data.Each($scope.SelectedSettingGrid.dataSource.data(), function (o) {
                if (o.StockID > 0) {
                    o.Name = ""; o.Code = ""; o.StockID = 0; o.Type = 0; o.GOPID = -1; o.ProductID = -1;
                }
            })

            $rootScope.IsLoading = false;
        }
    }

    $scope.Change_Container = function () {
        if ($scope.Item.HasContainer) {
            $rootScope.IsLoading = true;
            var data = $scope.AllSettingGrid.dataSource.data();
            var data = $.grep(data, function (o) {
                return o.IsStock == false;
            })
            Common.Data.Each(_CUSSettingOrderDetail.Data.Packing, function (o) {
                data.push({ PackingID: o.PackingID, Required: false, IsContainer: true, Code: o.PackingCode + "_Cont", Name: o.PackingCode + "-Loại cont"});
            })
            var newData = [];
            Common.Data.Each(data, function (o) {
                if (o.Code != "Ton" && o.Code != "CBM" && o.Code != "Quantity")
                    newData.push(o)
            })
            $scope.AllSettingGrid.dataSource.data(newData);
            $rootScope.IsLoading = false;
        } else {
            $rootScope.IsLoading = true;
            var data = $scope.AllSettingGrid.dataSource.data();
            var newData = [];
            Common.Data.Each(data, function (o) {
                if (!o.IsStock && !o.IsContainer) newData.push(o);
            })

            $scope.AllSettingGrid.dataSource.data(newData);

            //reset settingselected grid
            Common.Data.Each($scope.SelectedSettingGrid.dataSource.data(), function (o) {
                if (o.PackingID > 0) {
                    o.Name = ""; o.Code = ""; o.PackingID = 0; o.PackingCOQuantity = 0;
                }
            })

            $rootScope.IsLoading = false;
        }
    };

    $scope.Revert_Click = function ($event) {
        if (Common.HasValue($scope.RevertItem)) {
            var item = { Code: $scope.RevertItem.Code, Name: $scope.RevertItem.Name, IsStock: $scope.RevertItem.IsStock, IsContainer: $scope.RevertItem.IsContainer, StockID: $scope.RevertItem.StockID, Required: $scope.RevertItem.Required, GOPID: $scope.RevertItem.GOPID, ProductID: $scope.RevertItem.ProductID, PackingID: $scope.RevertItem.PackingID };
            $scope.AllSettingGrid_Options.dataSource.insert(0, item);

            var itemrevert = $scope.SelectedSettingGrid.dataItem($scope.SelectedSettingGrid.select())

            if (Common.HasValue(itemrevert)) {
                itemrevert.Code = "";
                itemrevert.Name = "";
                itemrevert.StockID = 0;
                itemrevert.Type = 0;
                itemrevert.GOPID = -1;
                itemrevert.ProductID = -1;
                itemrevert.PackingID = 0;
            }
            $scope.RevertItem = null;
            $scope.SelectedSettingGrid.clearSelection()
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSSettingOrderDetail.URL.Get,
        data: { id: _CUSSettingOrderDetail.Param.id },
        success: function (res) {

            $scope.Item = res;

            var lstStock = res.ListStock;

            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSSettingOrderDetail.URL.Get_StockByCus,
                data: { CusID: $scope.Item.CustomerID },
                success: function (stock) {

                    _CUSSettingOrderDetail.Data.Stock = stock.ListStock;
                    _CUSSettingOrderDetail.Data.StockDetail = stock.ListDetail;
                    _CUSSettingOrderDetail.Data.Packing = stock.ListPacking;

                    var dataAll = [];
                    var dataSource = $scope.SelectedSettingGrid.dataSource;

                    var dataGridAllSetting = $.extend([], true, _CUSSettingOrderDetail.Data.GridAllSetting)


                    Common.Data.Each(dataGridAllSetting, function (o) {
                        if (!$scope.Item[o.Code] > 0) {
                            dataAll.push(o);
                        }
                        else {
                            var dataItem = dataSource.get($scope.Item[o.Code]);
                            dataItem.Code = o.Code; dataItem.Name = o.Name;
                        }
                    })

                    var objStock = {};
                    if (res.HasStock) {
                        Common.Data.Each($scope.Item.ListStock, function (o) {
                            if (!Common.HasValue(objStock[o.StockID])) objStock[o.StockID] = o;
                        })

                        Common.Data.Each(stock.ListStock, function (o) {
                            if (Common.HasValue(objStock[o.ID])) {
                                if (objStock[o.ID].Ton > 0) {
                                    var dataItem = dataSource.get([objStock[o.ID].Ton]);
                                    dataItem.Code = o.Code + "_Ton"; dataItem.Name = o.LocationName + "-Tấn"; dataItem.StockID = o.ID; dataItem.Type = 1; dataItem.IsStock = true;
                                } else
                                    dataAll.push({ IsStock: true, Code: o.Code + "_Ton", Name: o.LocationName + "-Tấn", StockID: o.ID, Type: 1 })
                                if (objStock[o.ID].CBM > 0) {
                                    var dataItem = dataSource.get([objStock[o.ID].CBM]);
                                    dataItem.Code = o.Code + "_CBM"; dataItem.Name = o.LocationName + "-Khối"; dataItem.StockID = o.ID; dataItem.Type = 2; dataItem.IsStock = true;
                                } else
                                    dataAll.push({ IsStock: true, Code: o.Code + "_CBM", Name: o.LocationName + "-Khối", StockID: o.ID, Type: 2 })
                                if (objStock[o.ID].Quantity > 0) {
                                    var dataItem = dataSource.get([objStock[o.ID].Quantity]);
                                    dataItem.Code = o.Code + "_Quantity"; dataItem.Name = o.LocationName + "-Số lượng"; dataItem.StockID = o.ID; dataItem.Type = 3; dataItem.IsStock = true;
                                } else
                                    dataAll.push({ IsStock: true, Code: o.Code + "_Quantity", Name: o.LocationName + "-Số lượng", StockID: o.ID, Type: 3 })
                            }
                            else {
                                dataAll.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_Ton", Name: o.LocationName + "-Tấn", StockID: o.ID, Type: 1 });
                                dataAll.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_CBM", Name: o.LocationName + "-Khối", StockID: o.ID, Type: 2 });
                                dataAll.push({ GOPID: -1, ProductID: -1, Required: false, IsStock: true, Code: o.Code + "_Quantity", Name: o.LocationName + "-Số lượng", StockID: o.ID, Type: 3 })
                            }
                        })
                    }
                    var objStockProduct = {};
                    if (res.HasStockProduct) {
                        Common.Data.Each($scope.Item.ListStockWithProduct, function (o) {
                            if (!Common.HasValue(objStockProduct[o.StockID + "_" + o.GroupOfProductID + "_" + o.ProductID]))
                                objStockProduct[o.StockID + "_" + o.GroupOfProductID + "_" + o.ProductID] = o;
                        })

                        Common.Data.Each(stock.ListDetail, function (o) {
                            if (Common.HasValue(objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID])) {
                                if (objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID].Ton > 0) {
                                    var dataItem = dataSource.get([objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID].Ton]);
                                    dataItem.Code = o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Ton";
                                    dataItem.Name = o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Tấn";
                                    dataItem.StockID = o.StockID;
                                    dataItem.IsStock = true;
                                    dataItem.Type = 1;
                                    dataItem.GOPID = o.GroupProductID;
                                    dataItem.ProductID = o.ProductID;
                                } else
                                    dataAll.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Ton", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Tấn", StockID: o.StockID, Type: 1 });
                                if (objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID].CBM > 0) {
                                    var dataItem = dataSource.get([objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID].CBM]);
                                    dataItem.Code = o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_CBM";
                                    dataItem.Name = o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Khối";
                                    dataItem.StockID = o.StockID;
                                    dataItem.IsStock = true;
                                    dataItem.Type = 2;
                                    dataItem.GOPID = o.GroupProductID;
                                    dataItem.ProductID = o.ProductID;
                                } else
                                    dataAll.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_CBM", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Khối", StockID: o.StockID, Type: 2 });
                                if (objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID].Quantity > 0) {
                                    var dataItem = dataSource.get([objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID].Quantity]);
                                    dataItem.Code = o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Quantity";
                                    dataItem.Name = o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Số lượng";
                                    dataItem.StockID = o.StockID;
                                    dataItem.IsStock = true;
                                    dataItem.Type = 3;
                                    dataItem.GOPID = o.GroupProductID;
                                    dataItem.ProductID = o.ProductID;
                                } else
                                    dataAll.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Quantity", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Số lượng", StockID: o.StockID, Type: 3 });
                            }
                            else {
                                dataAll.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Ton", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Tấn", StockID: o.StockID, Type: 1 });
                                dataAll.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_CBM", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Khối", StockID: o.StockID, Type: 2 });
                                dataAll.push({ GOPID: o.GroupProductID, ProductID: o.ProductID, Required: false, IsStock: true, Code: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "_Quantity", Name: o.StockCode + "_" + o.GroupProductCode + "_" + o.ProductCode + "-Số lượng", StockID: o.StockID, Type: 3 })
                            }
                        })
                    }

                    var objContainer = {};
                    if (res.HasContainer) {
                        Common.Data.Each($scope.Item.ListContainer, function (o) {
                            if (!Common.HasValue(objContainer[o.PackingID]))
                                objContainer[o.PackingID] = o;
                        })

                        Common.Data.Each(stock.ListPacking, function (o) {
                            if (Common.HasValue(objContainer[o.PackingID])) {
                                if (objContainer[o.PackingID].PackingCOQuantity > 0) {
                                    var dataItem = dataSource.get([objContainer[o.PackingID].PackingCOQuantity]);
                                    dataItem.Code = o.PackingCode + "_Cont";
                                    dataItem.Name = o.PackingCode + "-Loại cont";
                                    dataItem.PackingID = o.PackingID;
                                    dataItem.IsContainer = true;
                                } else
                                    dataAll.push({ PackingID: o.PackingID, Required: false, IsContainer: true, Code: o.PackingCode + "_Cont", Name: o.PackingCode + "-Loại cont" });
                            }
                            else {
                                dataAll.push({ PackingID: o.PackingID, Required: false, IsContainer: true, Code: o.PackingCode + "_Cont", Name: o.PackingCode + "-Loại cont" });
                            }
                        })
                    }

                    $scope.AllSettingGrid.dataSource.data(dataAll);
                },
                error: function (stock) {
                }
            });

        }
    });

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.CUSSettingOrder.Index")
    }
}]);