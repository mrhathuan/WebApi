
//Change Quantity
$(document).ready(function () {
    $('.quantity').off('click').on('change', function () {
        var id = $(this).data('id');
        var qty = $(this).val();
        $.ajax({
            url: '/Pn/Product/ChangeQuantity',
            type: 'POST',
            data: { id: id ,qty:qty},
            dataType: 'JSON',
            success: function (res) {
                if (res.status == true) {
                   alert('Thành công')
                } else {
                    alert('Thất bại')
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    });
});


//Change status
$(document).ready(function () {
    $('.trangthai').off('click').on('click', function (e) {
        e.preventDefault();
        var status = $(this);
        var id = status.data('id');
        $.ajax({
            url: '/Pn/Product/ChangeStatus',
            type: 'POST',
            data: { id: id },
            dataType: 'JSON',
            success: function (res) {
                if (res.status == true) {
                    status.empty().append('<span class="btn btn-success btn-xs">Đã về</span>')
                } else {
                    status.empty().append('<span class="btn btn-warning btn-xs">Sắp về</span>');
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    });
});


//Xóa
$(document).ready(function () {
    $('.del-pro').off('click').on('click', function (e) {
        e.preventDefault();
        var cf = confirm("Bạn chắc chắn muốn xóa sản phẩm này?");
        var id = $(this).data('id');
        var remove_row = '#row_' + id;
        if (cf) {
            $.ajax({
                url: "/Pn/Product/Delete",
                data: { id: id },
                dataType: "JSON",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        alert("Xóa sản phẩm thành công");
                        $(remove_row).remove();
                    } else {
                        alert("Xóa sản phẩm thất bại");
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            })
        }
    });
});


//Quản lý ảnh
$(document).ready(function () {
    //
    $('.btn-image').off('click').on('click', function (e) {
        e.preventDefault();
        $('#hidProductId').val($(this).data('id'));
        var id = $(this).data('id');
        $.ajax({
            url: "/Pn/Product/LoadImages",
            type: "GET",
            data: { id: id },
            dataType: "JSON",
            success: function (res) {
                var data = res.data;
                var html = '';
                if (res.status == true) {
                    $('#imagesManage').modal('show');
                    $.each(data, function (i, item) {
                        html += '<div style="float:left"><img src="' + item + '" width="100" /><a class="btn-delImage" href="#"><i class="glyphicon glyphicon-remove"></i></a></div>';
                    });
                    $('#imageList').html(html);

                    $('.btn-delImage').off('click').on('click', function (e) {
                        e.preventDefault();
                        $(this).parent().remove();
                    });
                } else {
                    $('#imagesManage').modal('show');
                    $('#imageList').empty();
                }
            }
        });
        //
    });
    //
    $('#btnChooImage').off('click').on('click', function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            $('#imageList').append('<div style="float:left"><img src="' + url + '" width="100" /><a class="btn-delImage" href="#"><i class="glyphicon glyphicon-remove"></i></a></div>');

            $('.btn-delImage').off('click').on('click', function (e) {
                e.preventDefault();
                $(this).parent().remove();
            });
        };
        finder.popup();
    });
    //
    $('#btnSaveImage').off('click').on('click', function () {
        var images = [];
        var id = $('#hidProductId').val();
        $.each($('#imageList img'), function (i, item) {
            images.push($(item).prop('src'));
        })

        $.ajax({
            url: "/Pn/Product/SaveImages",
            type: "POST",
            data: {
                id: id,
                images: JSON.stringify(images)
            },
            dataType: "JSON",
            success: function (res) {
                $('#imagesManage').modal('hide');
                $('#imageList').empty();
                alert("Cập nhật ảnh thành công");
            }
        })
    });
    //
});



//Detail
$(document).ready(function () {
    $('.viewDetail').off('click').on('click', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        $.ajax({
            url: "/Pn/Product/Detail",
            type: "POST",
            dataType: "JSON",
            data: {
                id: id             
            },
            success: function (res) {
                var product = res.product;                
                var objProduct = {
                    "Mã": product.code,
                    "Yêu thích": product.like,
                    "Lượt mua": product.viewCount,
                    "Ngày up": new Date(product.createDate).toUTCString(),
                    "Người up": product.createByID,
                    "Ngày sửa": new Date(product.modifiedByDate).toUTCString(),
                    "Người sửa": product.modifiedByID,
                    "MetatTitle": product.metatTitle,
                    "MetaKeywords": product.metaKeywords,
                    "MetaDescription": product.metaDescription
                };
                $('#detailProduct').modal('show');
                $('#myModalLabel').empty().append(product.productName);
                $('.img-produc-detail').empty().append('<img style="max-width:100%" alt="' + product.productName + '" src="' + product.image + '"/>');
                $('.product-detail').empty();
                $.each(objProduct, function (key, val) {
                    $('.product-detail').append(key + ": " + val+'<br>');
                })
            }
        })
        //
    })
})