// <!--DelItem-->

$(document).ready(function () {
    $('.remove').off('click').on('click', function (e) {
        e.preventDefault();
        var cf = confirm("Bạn chắc chắn muốn xóa sản phẩm này?");
        var id = $(this).data('id');
        var cartitem = '#cartitem_' + id;
        if (cf) {
            $.ajax({
                url: "/Cart/DelItemCart",
                data:{id:id},
                type:"POST",
                dataType:"JSON",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang';
                    }
                },
                error: function (errormessage) {
                    window.location.href = '/gio-hang';
                    toastr.error('Xóa thất bại', '');
                }
            })
                }
    })
return false;
});

//<!--DelAll-->
$(document).ready(function () {
    $('#dellAll').click(function (e) {
        e.preventDefault();
        var cf = confirm("Bạn chắc chắn muốn hủy mua hàng?");
        if (cf) {
            $.ajax({
                url: '/Cart/DelAll',
                type:'POST',
                dataType:'JSON',
                success: function (res) {
                    if (res.status == true) {
                       window.location.href='/gio-hang'
                    }
                }
            })
                }
    })
return false;
});


// <!--Update Cart-->
$(document).ready(function () {
    $('.aa-cart-quantity').bind({
        change: function () {
            var id = $(this).data('id');
            var qty = $(this).val();
            var gia_now = '#thanhtien_' + id;
            if (qty > 0 && $.isNumeric(qty)) {
                $.ajax({
                    url: "/Cart/UpdateCart",
                    data: { id: id, qty: qty },
                    type: "POST",
                    dataType: "JSON",
                    success: function (res) {
                        if (res.status == true) {
                            location.href = '/gio-hang';
                        }                       
                    }
                });
            } else {
                    toastr.error('Số lượng không đúng', '');
                setTimeout(function () {
                    location.href = '/gio-hang';
                }, 1200);
            }
            }            
});
});


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
