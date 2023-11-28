$(document).ready(function () {

    
    $('#OneCheckAll').click(function () {
        if ($(this).prop("checked")) {
            $(".check-box").prop("checked", true);
        } else {
            $(".check-box").prop("checked", false);
        }
    });

    $('#checkSubmit').on('click', function () {

        var CheckedIds = [];
        //$('input:checked').each(function () {
        $("input[name='assignChkBx']:checked").each(function () {
            CheckedIds.push($(this).attr("value"));
        });
        if ($("input[name='assignChkBx']:checked").length == 0) {
            alert('Please check atleast one checkbox');
            return false;
        }
         //var newUrl = '@Url.Action("PCRSchedule","PCRSchedule")';
        var url = $(this).data('url');
        $.ajax({
           // url: "PCRSchedule/PCRSchedule",
            url:url,
                type: "GET",
                data: { PcrId: CheckedIds },
                dataType: "html",
                traditional: true,
                success: function (response) {
                    $('#contentBody').html(response);
                    $('#myModal').modal({ backdrop: 'static', keyboard: false })
                    // $('#myModal').modal('show');
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
    });

    $(function () {
        $(".date-picker").datepicker({
            //  maxDate: '0'
            controlType: 'select',
            oneLine: true,
            changeMonth: true,
            changeYear: true,
           // yearRange: '-10:+10',
           // yearRange: "-100:+100", // You can set the year range as per as your need
            yearRange: 'c-100:c+100',
            dateFormat: 'dd-M-yy'
        });

    });


});