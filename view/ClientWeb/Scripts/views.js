var views = {
    ORDOrder: [
        { Code: 'Index', Name: 'Đơn hàng' },
        { Code: 'DN', Name: 'Chi tiết đơn hàng' },
        { Code: 'TrackingView', Name: 'Tracking ĐH' },
        { Code: 'Packet', Name: 'Gói đơn hàng' },
        { Code: 'COTemp', Name: 'ĐH tạm (HDL)' },
        { Code: 'DICancel', Name: 'ĐH xe tải bị hủy' },
        { Code: 'COCancel', Name: 'ĐH xe container bị hủy' },
        { Code: 'Route', Name: 'Đa phương thức' },
        { Code: 'ExcelOnline', Name: 'Gửi đơn hàng' },
        { Code: 'Plan_ExcelOnline', Name: 'Gửi đơn hàng giám sát' },
    ],
    ORDOrder_Inspection: [
        { Code: 'Index', Name: 'Kiểm hóa' },
        { Code: 'Container', Name: 'Dịch vụ chi tiết' },                            
    ],
    ORDDashboard: [
        { Code: 'Index', Name: 'Dashboard' },
        { Code: 'Map', Name: 'Bản đồ' }
    ],
    PODInput: [
        { Code: 'Index', Name: 'Chứng từ' },
        { Code: 'Check', Name: 'Chứng từ xe tải' },
        { Code: 'COCheck', Name: 'Chứng từ container' },
        { Code: 'COORDCheck', Name: 'Chứng từ container(ĐH)' },
    ],
    PODInput_DIClose: [
        { Code: 'Index', Name: 'Chứng từ' },
        { Code: 'Check', Name: 'Nhận chứng từ' },
        { Code: 'Barcode', Name: 'Nhập bằng Barcode' },
        { Code: 'ExtReturn', Name: 'Công nợ trả về' },
        { Code: 'ExtReport', Name: 'Báo cáo công nợ' },
        { Code: 'Quick', Name: 'Nhập nhanh chứng từ' },
        { Code: 'FLMDI', Name: 'Chi phí xe tải nhà' },
        { Code: 'FLMCO', Name: 'Chi phí xe container' },
        { Code: 'Container', Name: 'Chứng từ container' },
        { Code: 'COClose', Name: 'Duyệt chứng từ container' },
    ],
    PODInput_COClose: [
       { Code: 'Index', Name: 'Chứng từ' },
       { Code: 'Barcode', Name: 'Nhập bằng Barcode' },
       { Code: 'ExtReturn', Name: 'Công nợ trả về' },
       { Code: 'ExtReport', Name: 'Báo cáo công nợ' },
       { Code: 'Quick', Name: 'Nhập nhanh chứng từ' },
       { Code: 'FLMDI', Name: 'Chi phí xe tải nhà' },
       { Code: 'FLMCO', Name: 'Chi phí xe container' },
       { Code: 'Container', Name: 'Chứng từ container' },
       { Code: 'DIClose', Name: 'Duyệt chứng từ xe tải' },
    ],
    PODInput_ExtReturn: [
        { Code: 'Index', Name: 'Chứng từ' },
        { Code: 'Barcode', Name: 'Nhập bằng Barcode' },
        { Code: 'ExtReport', Name: 'Báo cáo công nợ' },
        { Code: 'Quick', Name: 'Nhập nhanh chứng từ' },
    ],
    PODInput_ExtReport: [
        { Code: 'Index', Name: 'Chứng từ' },
        { Code: 'Barcode', Name: 'Nhập bằng Barcode' },
        { Code: 'ExtReturn', Name: 'Công nợ trả về' },
        { Code: 'Quick', Name: 'Nhập nhanh chứng từ' },
    ],
    PODInput_Quick: [
       { Code: 'Index', Name: 'Chứng từ' },
       { Code: 'Barcode', Name: 'Nhập bằng Barcode' },
       { Code: 'ExtReturn', Name: 'Công nợ trả về' },
       { Code: 'ExtReport', Name: 'Báo cáo công nợ' },
    ],
    OPSAppointment: [
        { Code: 'Index', Name: 'Màn hình điều phối' }
        //{ Code: 'DI', Name: 'Đơn hàng xe tải' },
        //{ Code: 'DIRoute', Name: 'Lập lệnh kéo thả' },
        ////{ Code: 'DIRouteMaster', Name: 'Lập lệnh LTL' },
        //{ Code: 'DIRouteMasterFTL', Name: 'Lập lệnh FTL' },
        //{ Code: 'DIRouteMasterVEN', Name: 'Lập lệnh Tender' },
        //{ Code: 'DIRouteTOMaster', Name: 'Lập lệnh' },
        //{ Code: 'DIRouteDN', Name: 'Nhập DN' },
        //{ Code: 'CO', Name: 'Đơn hàng xe container' },
        //{ Code: 'CORouteTOMaster', Name: 'Lập lệnh xe container' },
        //{ Code: 'COOptimizer', Name: 'Tối ưu xe container' },
        //{ Code: 'DIOptimizer', Name: 'Tối ưu xe tải' } ,
        //{ Code: 'DIPacket', Name: 'Đơn hàng gửi đối tác' },
        //{ Code: 'SettingTender', Name: 'Thiết lập Tender' },
        //{ Code: 'DIImportPacket', Name: 'Import chuyến xe tải' },
        //{ Code: 'DIViewOnMap', Name: 'Lập chuyến bản đồ - Xe tải' },
        //{ Code: 'COViewOnMap', Name: 'Lập chuyến bản đồ - Xe container' },
        //{ Code: 'DICancel', Name: 'Đơn hàng bị hủy - Xe tải' },
        //{ Code: 'COCancel', Name: 'Đơn hàng bị hủy - Xe container' },
    ],
    OPSAppointmentCO: [
        { Code: 'Index', Name: 'Màn hình điều phối' },
        { Code: 'COViewOnMap', Name: 'Dạng 1' },
        { Code: 'COViewOnMapV2', Name: 'Dạng 2' },
        { Code: 'COViewOnMapV3', Name: 'Dạng 3' },
        { Code: 'COViewOnMapV4', Name: 'Dạng 4 - TimeLine' },
        { Code: 'COViewOnMapV5', Name: 'Dạng 5 - TimeLine' }
    ],
    OPSAppointmentDI: [
        { Code: 'Index', Name: 'Màn hình điều phối' },
        { Code: 'DIViewOnMap', Name: 'Dạng 1' },
        { Code: 'DIViewOnMapV2', Name: 'Dạng 2' },
        { Code: 'DIViewOnMapV3', Name: 'Dạng 3' },
        { Code: 'DIViewOnMapV4', Name: 'Dạng 4 - TimeLine' }
    ],
    OPSAppointment_Ven: [
        { Code: 'Index', Name: 'Màn hình điều phối' },
        { Code: 'DIRouteMasterVEN', Name: 'Lệnh xe tải' },
        { Code: 'DIPacket_Ven', Name: 'Đơn hàng xe tải' }
    ],
    OPSAppointmentCO_Ven: [
        { Code: 'Index', Name: 'Màn hình điều phối' },
        { Code: 'COViewVendor', Name: 'Lệnh xe container' }
    ],
    OPSAppointmentDI_TEN: [
        { Code: 'Index', Name: 'Màn hình điều phối' },
        { Code: 'SettingTender', Name: 'Thiết lập tender' }
    ],
    FINFreightAuditCustomer: [
        { Code: 'Index', Name: 'Đơn hàng theo chuyến' },
        { Code: 'Con', Name: 'Đơn hàng theo container' },
        { Code: 'Group', Name: 'Đơn hàng lẻ' },
    ],
    FreightAuditVendor: [
        { Code: 'Index', Name: 'Đối chiếu xe tải' },
        { Code: 'Con', Name: 'Đối chiếu xe container' },
    ],
    MONMonitor: [
        { Code: 'Index', Name: 'Giám sát' },
        { Code: 'ControlTowerDI', Name: 'Giám sát Xe tải' },
        { Code: 'ControlTowerCO', Name: 'Giám sát Container' },
        { Code: 'ControlTowerCOTimeline', Name: 'Container timeline' },
        { Code: 'Input_ImportExt', Name: 'Nhập liệu giám sát mở rộng' },
    ],
    MONCost: [
        { Code: 'Index', Name: 'Giám sát' },
        { Code: 'Approve_DICost', Name: 'Duyệt chi phí 1' },
        { Code: 'Approve_DICost2', Name: 'Duyệt chi phí 2' },
    ],
    REPDIPL: [
        { Code: 'Index', Name: 'Lợi nhuận' },

        { Code: 'Detail', Name: 'Lợi nhuận chuyến' },
        { Code: 'DetailColumn', Name: 'Lợi nhuận chuyến theo cột' },
        { Code: 'DetailGroupStock', Name: 'Lợi nhuận chuyến theo nhóm kho' },
        { Code: 'DetailMOQ', Name: 'Lợi nhuận chuyến theo nhóm phụ thu' },
        { Code: 'DetailGroupProduct', Name: 'Lợi nhuận chuyến theo nhóm sản phẩm' },

        { Code: 'Order', Name: 'Lợi nhuận đơn hàng' },
        { Code: 'OrderColumn', Name: 'Lợi nhuận đơn hàng theo cột' },
        { Code: 'OrderGroupStock', Name: 'Lợi nhuận đơn hàng theo nhóm kho' },
        { Code: 'OrderMOQ', Name: 'Lợi nhuận đơn hàng theo nhóm phụ thu' },
        { Code: 'OrderGroupProduct', Name: 'Lợi nhuận đơn hàng theo nhóm sản phẩm' },

        { Code: 'SpotRate', Name: 'Lợi nhuận không hợp đồng' },
    ],

    REPDIReturn: [
        { Code: 'Index', Name: 'Công nợ trả về' },

        { Code: 'Detail', Name: 'Công nợ trả về chi tiết' },
    ],

    REPDIOPSPlan: [
        { Code: 'Index', Name: 'Kế hoạch' },

        { Code: 'Detail', Name: 'Kế hoạch chuyến' },
        { Code: 'DetailColumn', Name: 'Kế hoạch chuyến theo cột' },
        { Code: 'DetailGroupStock', Name: 'Kế hoạch chuyến theo nhóm hàng' },

        { Code: 'Order', Name: 'Kế hoạch đơn hàng' },
        { Code: 'OrderColumn', Name: 'Kế hoạch đơn hàng theo cột' },
        { Code: 'OrderGroupStock', Name: 'Kế hoạch đơn hàng theo nhóm hàng' },
    ],
    REPDIPOD: [
        { Code: 'Index', Name: 'Chứng từ' },

        { Code: 'Detail', Name: 'Chứng từ chi tiết' },
        { Code: 'DetailPODData', Name: 'Chứng từ chi tiết khác' },
        { Code: 'DetailColumn', Name: 'Chứng từ theo cột' },
    ],
    REPMAP: [
        { Code: 'Index', Name: 'Phân tích theo điểm giao' },
        { Code: 'Get', Name: 'Phân tích theo điểm nhận' },
        { Code: 'Route', Name: 'Phân tích theo đơn hàng và xe' },
        { Code: 'Area', Name: 'Phân tích theo khu vực' },
    ],
    REPCOPL: [
        { Code: 'Index', Name: 'Lợi nhuận' },

        { Code: 'Detail', Name: 'Lợi nhuận chuyến' },
        { Code: 'DetailColumn', Name: 'Lợi nhuận chuyến theo cột' },

        { Code: 'Order', Name: 'Lợi nhuận đơn hàng' },
        { Code: 'OrderColumn', Name: 'Lợi nhuận đơn hàng theo cột' },
    ],
    REPCOOPSPlan: [
        { Code: 'Index', Name: 'Kế hoạch' },

        { Code: 'Detail', Name: 'Kế hoạch chuyến' },
        { Code: 'DetailColumn', Name: 'Kế hoạch chuyến theo cột' },
        { Code: 'Order', Name: 'Kế hoạch đơn hàng' },
    ],
    REPOwnerDriverRole: [
        { Code: 'Detail', Name: 'Chi tiết phân và từ chối lệnh' },
        { Code: 'Index', Name: 'Vai trò tài xế' },
    ],
    REPOwnerWorkOrder: [
        { Code: 'Index', Name: 'Phiếu hoạt động' },
        { Code: 'ReceiptDetail', Name: 'Phiếu nhiên liệu chi tiết' },
        { Code: 'Receipt', Name: 'Phiếu nhiên liệu theo cột' },
        { Code: 'Repair', Name: 'Phiếu sửa chữa chi tiết' },
    ],
    REPOwnerEquiment: [
        { Code: 'Index', Name: 'Báo cáo thiết bị' },
        { Code: 'Detail', Name: 'Báo cáo thiết bị chi tiết' },
    ],

    REPOwnerGPS: [
        { Code: 'Index', Name: "Báo cáo GPS" },
        { Code: "Detail", Name: "Báo cáo GPS chi tiết" },
    ],
    REPOwnerFixedCost: [
       { Code: 'Index', Name: "Báo cáo khấu hao" },
       { Code: 'Vehicle', Name: 'Báo cáo theo xe' },
       { Code: "Detail", Name: "Báo cáo khấu hao chi tiết" },
    ],
    REPOwnerMaintenance: [
        { Code: 'Index', Name: "Báo cáo bảo trì, đăng kiểm" },
        { Code: "Detail", Name: "Báo cáo bảo trì, đăng kiểm chi tiết" },
    ],
    REPCOInspection: [
        { Code: 'Index', Name: "Báo cáo tờ khai" },
        { Code: "Detail", Name: "Báo cáo tờ khai chi tiết" },
    ],
    REPOwnerAsset:[
         { Code: 'Index', Name: "Tài sản" },
         { Code: "Asset", Name: "Báo cáo Tài sản" },
         { Code: "Quota", Name: "Báo cáo định mức xe" },

    ],
    REPDIKPI: [
         { Code: 'Index', Name: "KPI" },
         { Code: "TimeData", Name: "Chi tiết chuyến" },
         { Code: "OrderData", Name: "Chi tiết đơn hàng" },
         { Code: "TimeDateData", Name: "Chi tiết chuyến mới" },
         { Code: "OrderDateData", Name: "Chi tiết đơn hàng mới" },
    ],
    KPITime: [
        { Code: 'Index', Name: "Thời gian theo ngày" },
        { Code: 'DateIndex', Name: "KPI thời gian" },
        { Code: 'Vendor', Name: "KPI thời gian đối tác" },
    ],
    CATKPITime: [

    ],
    SYSUserResource: [

    ],
    CATSYSCustomerTroubleDetail: [

    ],
    CATPackingGOPTU: [

    ],
    CatReason: [

    ],
    CATDistrict: [

    ],
    CatCurrency: [

    ],
    CATCountry: [

    ],
    CATProvince: [

    ],

    CatRouting: [

    ],
    CATCarrier: [


    ],
    CATDistributor: [


    ],
    CATSeaPort: [


    ],
    CATLocation: [
        { Code: 'Index', Name: 'Danh sách địa điểm' },
         { Code: 'Test', Name: 'Test Spreadsheet' },

    ],
    CATStation: [
         { Code: 'Price', Name: 'Giá vé chuyến' },
         { Code: 'Month', Name: 'Giá vé tháng' },
    ],
    CATStationPrice: [
        { Code: 'Index', Name: 'Danh sách trạm' },
        { Code: 'Price', Name: 'Giá vé chuyến' },
        { Code: 'Month', Name: 'Giá vé tháng' },
    ]
    ,
    CATStationMonth: [
       { Code: 'Index', Name: 'Danh sách trạm' },
       { Code: 'Price', Name: 'Giá vé chuyến' },
       { Code: 'Month', Name: 'Giá vé tháng' },
    ]
    ,
    CATLocationMatrix: [
         { Code: 'Index', Name: 'Ma trận điểm' },
         { Code: 'Station', Name: 'Danh sách trạm theo tuyến đường' },
    ],
    CATConstraint: [


    ],
    CATService: [


    ],
    CATGroupOfService: [


    ],
    CATGroupOfPartner: [


    ],
    CATShiftEdit: [


    ],
    CATGroupOfTrouble: [


    ],
    CATTypeBusiness: [


    ],
    CATScale: [


    ],
    CATGroupOfCost: [


    ],
    CATCost: [


    ],
    CATGroupOfRomooc: [


    ],
    CATGroupOfVehicle: [


    ],
    CATGroupOfEquipment: [


    ],
    CATGroupOfMaterial: [


    ],
    CATMaterial: [


    ],
    CATStock: [


    ],
    CATVehicle: [


    ],
    SYSUser: [


    ],
    SYSConfig: [


    ],
    SYSConfigDetail: [


    ],

    SYSFunction: [


    ],
    SYSUser: [


    ],
    SYSGroup: [


    ],
    SYSCustomer: [


    ],
    SYSResource: [

    ],
    WFLPacket: [
        { Code: 'Index', Name: 'Dữ liệu gói' },
        { Code: 'Setting', Name: 'Thiết lập gói dữ liệu' },
    ],
    WFLSetting: [
        { Code: 'Index', Name: 'Workflow' },
        { Code: 'Function', Name: 'Thiết lập hệ thống' },
        { Code: 'Field', Name: 'Thiết lập dữ liệu' },
        { Code: 'Template', Name: 'Thiết lập nội dung' },
        { Code: 'Event', Name: 'Thiết lập sự kiện' },
        { Code: 'Define', Name: 'Thiết lập sự kiện(cũ)' }
    ],
    FLMSetting: [
        { Code: 'Index', Name: 'Đội xe' },
        { Code: 'PHTDI', Name: 'Phiếu hành trình xe tải' },
        { Code: 'PHTCO', Name: 'Phiếu hành trình xe container' },
        { Code: 'Vendor', Name: 'Đối tác' },
        { Code: 'Location', Name: 'Danh sách địa chỉ' },
        { Code: 'Product', Name: 'Nhóm hàng' },
    ],
    FLMFleetPlanning: [
        { Code: 'Column', Name: 'Kế hoạch phân tài(view cột)' },
        { Code: 'Vehicle', Name: 'Kế hoạch phân tài' },
        { Code: 'Index', Name: 'Kế hoạch xe' },
    ],
    FLMFleetPlanning_Column: [
        { Code: 'Column', Name: 'Kế hoạch phân tài(view cột)' },
        { Code: 'Vehicle', Name: 'Kế hoạch phân tài' },
        { Code: 'Index', Name: 'Kế hoạch xe' },
    ],
    FLTSetting: [
        { Code: 'PHTO', Name: 'Phiếu phát sinh xe công' },
        { Code: 'Index', Name: 'Đội xe' },
    ],

    CUSCustomer: [
        { Code: 'Index', Name: 'Danh sách khách hàng' },
    ],
    CUSSettingOrder: [
        { Code: 'Index', Name: 'Thiết lập đơn hàng' },
        { Code: 'Plan', Name: 'Thiết lập đơn hàng giám sát' },
        { Code: 'Code', Name: 'Thiết lập mã đơn hàng' },
    ],
    CUSSettingMON: [
        { Code: 'Index', Name: 'Thiết lập giám sát' },
        { Code: 'Input', Name: 'Thiết lập hàng bổ sung' },
        { Code: 'ExtReturn', Name: 'Thiết lập công nợ' },
    ],
    VENVendor: [
        { Code: 'Index', Name: 'Danh sách nhà xe' },
    ],
    REPOwnerDriverFee: [
        { Code: 'Index', Name: 'Lương tài xế' },
        { Code: 'Detail', Name: 'Lương tài xế chi tiết' },
        { Code: 'DetailColumn', Name: 'Lương tài xế theo cột' },
    ],
    REPOwnerCostVehicle: [
        { Code: 'Index', Name: 'Chi phí đội xe' },
        { Code: 'Truck', Name: 'Chi phí xe tải' },
        { Code: 'Container', Name: 'Chi phí xe container' },
        { Code: 'DetailColumn', Name: 'Chi phí theo cột' },
        { Code: "Quota", Name: 'Chi phí định mức của xe' },
    ],
    REPOwnerPLVehicle: [
        { Code: 'Index', Name: 'Lợi nhuận' },
        { Code: 'Detail', Name: 'Lợi nhuận chi tiết' },
        { Code: 'DetailColumn', Name: 'Lợi nhuận theo cột' },
        { Code: 'PriceDetail', Name: 'Lợi nhuận bảng giá chi tiết' },
        { Code: 'PriceDetailColumn', Name: 'Lợi nhuận bảng giá theo cột' },
    ],
    KPIKPI: [
        { Code: 'Index', Name: 'KPI' },
        { Code: 'Collection', Name: 'Tổng hợp' },
    ],
    FINManualFix: [
        { Code: 'Index', Name: 'ManualFix' },
        { Code: 'SettingIndex', Name: 'Thiết lập excel' },
    ],
};
