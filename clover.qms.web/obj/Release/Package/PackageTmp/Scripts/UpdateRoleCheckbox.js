//$(document).ready(function () {
//    $('#checkSubmit').on('click', function () {
//        var roles = new Array();
//        $("input[name='childHeader']:checked").each(function () {
//            roles.push($(this).attr("value"));
//        });
//        if ($("input[name='childHeader']:checked").length == 0) {
//            alert('Please check atleast one checkbox');
//            return false;
//        }
//        var obj = {
//            roleIDs: roles,
//        };
//        var url = $(this).data('url');
//        $.ajax({
//            url: url,
//            contentType: 'application/json; charset=utf-8',
//            type: "POST",
//            data: JSON.stringify(obj),
//            cache: false,
//            success: function (response) {
//                window.location.href = response.Url;
//            },
//            error: function (xhr, status, error) { alert("Error"); },

//        });
//    });
//});


$(document).ready(function () {
    $('#checkSubmit').on('click', function () {
        debugger;
        var roles = new Array();
        var dept = $('#ddlDepartmentID').val();//ADDED by Sushila 14-12-2022

        $("input[name='childHeader']:checked").each(function () {
            roles.push($(this).attr("value"));
        });
        if ($("input[name='childHeader']:checked").length == 0) {
            alert('Please check atleast one checkbox');
            return false;
        }
        var obj = {
            roleIDs: roles,
            deptids: dept,//ADDED by Sushila 14-12-2022
        };
        var url = $(this).data('url');
        $.ajax({
            url: url,
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            data: JSON.stringify(obj),
            cache: false,
            success: function (response) {
                window.location.href = response.Url;
            },
            error: function (xhr, status, error) { alert("Error"); },

        });
    });
});