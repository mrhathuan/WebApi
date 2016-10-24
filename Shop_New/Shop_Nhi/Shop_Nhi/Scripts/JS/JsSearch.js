var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent:function(){
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function( request, response ) {
                $.ajax({
                    url: "/Home/ListName",
                    type:"GET",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.name);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.name);
                return false;
            }
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
              .append("<div>" + item.name + "</div>")
              .appendTo(ul);
        };
    }
}
common.init();