//Set like

toastr.options = {
    "closeButton": true
};

$(document).ready(function () {
    $('.aa-add-card-btn-mb.action.like').off('click').on('click', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var like = '#like_' + id;
        $.ajax({
            url: "/Home/SetLike",
            data: { id: id },
            type: "POST",
            dataType: "JSON",
            success: function (res) {
                if (res.status == true) {
                    //$(aapro).removeClass('like').append('<a data-toggle="tooltip" data-placement="top" title="Đã thích"><span class="fa fa-heart"></span></a>')
                    $(like).removeAttr("href");
                    $(like).empty().append('<i class="fa fa-heart"></i>');
                }
            }
        })
    })
});
//get product
$(document).ready(function () {
    $('.aa-add-card-btn-mb.action.quick-view')
        .off('click')
        .on('click',
            function(e) {
                e.preventDefault();
                var id = $(this).data('id');
                $('#hidProductId').val($(this).data('id'));
                $('.loading').removeClass('hide');
                $.ajax({
                    url: '/Home/GetProduct',
                    data: { id: id },
                    type: 'POST',
                    dataType: 'JSON',
                    success: function(res) {
                        $('.loading').addClass('hide');
                        var data = res.product;
                        var cate = res.category;
                        $('#quick-view-modal').modal('show');
                        $('.aa-product-view-slider').html('<img class="img-responsive" src="' + data.image + '" />');
                        $('.aa-product-view-content h3').html('' + data.productName);
                        if (data.description !== null) {
                            $('.aa-product-view-content p').html('' + data.description);
                        }
                        $('.aa-product-view-price').html('' + addPeriod(data.price) + '<sup><u>đ</u></sup>');
                        $('.aa-prod-category')
                            .html('Danh mục:' +
                                ' ' +
                                '<a href="/san-pham/' +
                                cate.metatTitle +
                                '-' +
                                cate.ID +
                                '">' +
                                cate.name +
                                '</a>');
                        $('#aa-cart-modal-detail').attr("href", "/chi-tiet/" + data.metatTitle + "-" + data.ID + "");
                        if (data.quantity > 0 && data.status == true) {
                            $('.aa-prod-view-bottom')
                                .append('<a href="#" class="aa-add-to-cart-btn" id="add-cart-modal"><span class="fa fa-shopping-cart"></span>Thêm vào giỏ</a>');
                        }

                        //region add cart modal
                        $('#add-cart-modal').off('click').on('click', function (e) {
                            e.preventDefault();
                            var id = $('#hidProductId').val();
                            var qty = $('#quantity').val();
                            if (qty > 0) {
                                $.ajax({
                                    url: '/Cart/AddCartItem',
                                    data: { id: id, qty: qty },
                                    type: 'POST',
                                    dataType: 'JSON',
                                    success: function (res) {
                                        if (res.productItem != null) {                                            
                                            setTimeout(function () {
                                                location.href = '/gio-hang';
                                            },
                                            500);
                                        }
                                    },
                                    error: function (errormessage) {
                                        alert(errormessage.responseText);
                                    }
                                });
                            } else {
                                toastr.error('Số lượng không đúng', '');
                            }
                        });
                        //#end region add cart modal
                    }
                });
            });
});
$('#quick-view-modal')
    .on('hidden.bs.modal',
        function(e) {
            $('#add-cart-modal').remove();
        });
//Add cart
$(document).ready(function () {
    toastr.options = {
        "closeButton": true
    };
    $('.action-addcart').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);     
        var id = $(this).data('id');       
        $.ajax({
            url: '/Cart/AddCartItem',
            type: 'GET',
            dataType: 'JSON',
            data: { id: id, qty: 1 },
            success: function (res) {               
                var html = '';
                var productItem = res.productItem;
                $.each(productItem, function (key, item) {
                    html += '<li id="aa-cartbox-item_' + item.ID + '">';
                    html += '<a class="aa-cartbox-img"><img alt="' + item.productName + '" src="' + item.image + '"/></a>';
                    html += '<div class="aa-cartbox-info">' +
                        '<h4><a>' +
                        item.productName +
                        '</a></h4>' +
                        '<p>' +
                        item.quantity +
                        ' x ' +
                        addPeriod(item.price) +
                        ' <sup><u>đ</u></sup></span>' +
                        '</p>' +
                        '</div>';
                    html += '<a class="aa-remove-product" data-id="' + item.ID + '" href="#"><span class="fa fa-times"></span></a>';
                    html += '</li>';
                });
                $('.aa-cartbox-summary ul').empty().append(html+'<li>'+
                                   ' <span class="aa-cartbox-total-title">'+
                                       ' Tổng tiền  </span>'+                        
                                   ' <span class="aa-cartbox-total-price">'+
                                       addPeriod(res.tongtien)+'<sup><u>đ</u></sup>'+
                                    '</span>'+
                               ' </li>');


                $('.aa-cart-notify').html(res.soluong);                
                $('.aa-cartbox-total-price').empty().append(addPeriod(res.tongtien+'<sup><u>đ</u></sup>'));
                //$(btn).empty().append('<i class="fa fa-refresh fa-spin fa-3x fa-fw"></i>');
                $(btn).empty().append('<span class="fa fa-refresh fa-pulse fa-1x fa-fw"></span> Thêm vào giỏ');
                setTimeout(function () {                    
                    $('.aa-add-card-btn-mb').removeClass('action-addcart');
                    $(btn).empty().append('<span class="fa fa-check"></span> Xem giỏ hàng');
                    $(btn).attr('href', '/gio-hang');
                    toastr.success('Thêm sản phẩm vào giỏ thành công', '');
                }, 1100);
                $(btn).unbind('click');
                $('.aa-remove-product').off('click').on('click', function (e) {
                    e.preventDefault();
                    var id = $(this).data('id');
                    $(this).parent().remove();
                    $.ajax({
                        url: "/Cart/DelItemCart",
                        data: { id: id },
                        type: "POST",
                        dataType: "JSON",
                        success: function(res) {
                            if (res.tongtien != 0) {
                                $(this).remove();
                                $('#cart-total').empty().append('' + addPeriod(res.tongtien) + '<sup><u>đ</u></sup>');
                                $('.aa-cartbox-total-price').empty().append(addPeriod(res.tongtien));
                                $('.aa-cart-notify').empty().append(res.soluong);

                            } else {
                                $(this).parent().remove();
                                $('.aa-cart-notify').empty().append('0');
                                $('.aa-cartbox-total-price').empty().append('0<sup><u>đ</u></sup>');
                                $('.cart-view-table').remove();
                                $('.cart-view-area').empty().append('<p>Chưa có sản phẩm nào trong giỏ</p>');
                                $('.checkout-area').empty().append('<p>Chưa có sản phẩm nào trong giỏ</p>');
                            }
                        },
                        error: function(errormessage) {
                            alert(errormessage.responseText);
                            toastr.error('Xóa thất bại', '');
                        }
                    });
                    // $(this).parent().remove();
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    });
});

//Định dạng tiền
function addPeriod(nStr) {
    nStr += '';
    var x = nStr.split(',');
    var x1 = x[0];
    var x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

//Add detail
$(document).ready(function () {
    $('#AddCartDetail').click(function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var qty = $('#quantity').val();
        if (qty > 0) {
            $.ajax({
                url: '/Cart/AddCartItem',
                data: { id: id, qty: qty },
                type: 'POST',
                dataType: 'JSON',
                success: function (res) {
                    var html = '';
                    var productItem = res.productItem;
                    $.each(productItem, function (key, item) {
                        html += '<li id="aa-cartbox-item_' + item.ID + '">';
                        html += '<a class="aa-cartbox-img"><img alt="' + item.productName + '" src="' + item.image + '"/></a>';
                        html += '<div class="aa-cartbox-info">' +
                            '<h4><a>' +
                            item.productName +
                            '</a></h4>' +
                            '<p>' +
                            item.quantity +
                            ' x ' +
                            addPeriod(item.price) +
                            ' <sup><u>đ</u></sup></span>' +
                            '</p>' +
                            '</div>';
                        html += '<a class="aa-remove-product" data-id="' + item.ID + '" href="#"><span class="fa fa-times"></span></a>';
                        html += '</li>'
                    });
                    $('.aa-cartbox-summary ul').empty().append(html + '<li>' +
                                       ' <span class="aa-cartbox-total-title">' +
                                           ' Tổng tiền  </span>' +
                                       ' <span class="aa-cartbox-total-price">' +
                                           addPeriod(res.tongtien) + '<sup><u>đ</u></sup>' +
                                        '</span>' +
                                   ' </li>');
                    $('.aa-cart-notify').html(res.soluong);
                    $('.aa-cartbox-total-price').empty().append(addPeriod(res.tongtien));
                    $('#btn-detail').empty().append('<a class="aa-add-card-btn-mb"><span class="fa fa-refresh fa-pulse fa-1x fa-fw"></span> Thêm vào giỏ</a>')
                    setTimeout(function () {
                        $('#btn-detail').empty().append('<a class="aa-add-card-btn-mb" href="/gio-hang"><span class="fa fa-check"></span> Xem giỏ hàng</a>');
                        toastr.success('Thêm sản phẩm vào giỏ thành công', '');
                    }, 1100);
                   
                    $('.aa-remove-product').off('click').on('click', function (e) {
                        e.preventDefault();
                        $(this).parent().remove();
                    });
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });//End:ajax
        } else {
            toastr.error('Số lượng không đúng', '');           
        }
        
    });
    return false;
});

//Addcart popup
$(document).ready(function () {
    $('#add-cart-modal').off('click').on('click', function (e) {
        e.preventDefault();
        var id = $('#hidProductId').val();
        var qty = $('#quantity').val();
        if (qty > 0) {
            $.ajax({
                url: '/Cart/AddCartItem',
                data: { id: id, qty: qty },
                type: 'POST',
                dataType: 'JSON',
                success: function(res) {
                    if (res.productItem != null) {
                        toastr.success('Thêm sản phẩm vào giỏ thành công', '');
                        setTimeout(function() {
                                location.href = '/gio-hang';
                            },
                            1000);
                    }
                },
                error: function(errormessage) {
                    alert(errormessage.responseText);
                }
            });
        } else {
            toastr.error('Số lượng không đúng', '');
        }
    });

});

