
//Change Status
$(document).ready(function () {
    $('.trangthai').off('click').on('click', function (e) {
        e.preventDefault();
        var status = $(this);
        var id = status.data('id');
        $.ajax({
            url: "/Pn/Category/ChangeStatus",
            type: "POST",
            data: { id: id },
            dataType: "JSON",
            success: function (res) {
                if (res.status == true) {
                    status.empty().append('<span class="btn btn-success btn-xs">Kích hoạt</span>');
                } else {
                    status.empty().append('<span class="btn btn-danger btn-xs">Khóa</span>');
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        })
    });
});

//Change Showhome
$(document).ready(function () {
    $('.showhome').off('click').on('click', function (e) {
        e.preventDefault();
        var showhome = $(this);
        var id = showhome.data('id');
        $.ajax({
            url: "/Pn/Category/ShowHome",
            data: { id: id },
            type: "POST",
            dataType: "JSON",
            success: function (res) {
                if (res.status == true) {
                    showhome.empty().append('<span class="btn btn-success btn-xs">Kích hoạt</span>');
                } else {
                    showhome.empty().append('<span class="btn btn-danger btn-xs">Khóa</span>');
                }
            }
        })
    });
});

//Xóa
$(document).ready(function () {
    $('.del-cate').off('click').on('click', function (e) {
        e.preventDefault();
        var cf = confirm("Bạn chắc chắn muốn xóa danh mục này?");
        var id = $(this).data('id');
        var remove_row = '#row_' + id;
        if (cf) {
            $.ajax({
                url: "/Pn/Category/Delete",
                data: { id: id },
                dataType: "JSON",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        alert("Xóa danh mục thành công");
                        $(remove_row).remove();
                    } else {
                        alert("Xóa danh mục thất bại");
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            })
        }
    });
});