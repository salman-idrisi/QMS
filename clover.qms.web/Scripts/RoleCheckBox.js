$(document).ready(function () {


    $('#OneCheckAll').click(function () {
        if ($(this).prop("checked")) {
            $(".check-box").prop("checked", true);
        } else {
            $(".check-box").prop("checked", false);
        }
    });

    $('#checkSubmit').on('click', function () {
        var roles = new Array();
        $("input[name='childHeader']:checked").each(function () {
            roles.push($(this).attr("value"));
        });
        if ($("input[name='childHeader']:checked").length == 0) {
            alert('Please check atleast one checkbox');
            return false;
        }
        var obj = {
            roleIDs: roles,
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