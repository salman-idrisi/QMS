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
        //  var newUrl = '@Url.Action("PCRSchedule","PCRSchedule")';
        $.ajax({
                url: "/PCRSchedule/PCRSchedule",
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
            yearRange: "-100:+0", // You can set the year range as per as your need
            dateFormat: 'dd-M-yy'
        });

    });
    $(function () {
        var dynamicVariable = {};
        var arrayIds = [];
        var arrayIndex = [];
        var i;
        var total = 0;
        var newArray = [];
        
        $('select').change(function () {
            i = $(this).index('select');
            var arraySelectedIds = [];
            var ddlId = $(this).attr("id");

            $("select").each(function () {
                arraySelectedIds.push($(this).val());
            });

            if ($.inArray(i, arrayIndex) >= 0) {
                var PreviousValue = newArray[i];
                if (arraySelectedIds[i] == 1) {
                    if (PreviousValue == 3) {
                        dynamicVariable[ddlId + "_Yes"]++;
                        dynamicVariable[ddlId + "_NA"] = 0;
                        dynamicVariable[ddlId + "_No"] = 0;
                    }
                    else {
                        dynamicVariable[ddlId + "_Yes"]++;
                        dynamicVariable[ddlId + "_No"]--;
                        dynamicVariable[ddlId + "_NA"] = 0;
                    }
                }
                else if (arraySelectedIds[i] == 2) {
                    if (PreviousValue == 3) {
                        dynamicVariable[ddlId + "_No"]++;
                        dynamicVariable[ddlId + "_NA"] = 0;
                        dynamicVariable[ddlId + "_Yes"] = 0;
                    }
                    else {
                        dynamicVariable[ddlId + "_No"]++;
                        dynamicVariable[ddlId + "_Yes"]--;
                        dynamicVariable[ddlId + "_NA"] = 0;

                    }

                }

                else if (arraySelectedIds[i] == 3) {
                    if (PreviousValue == 1) {
                        dynamicVariable[ddlId + "_NA"] = 0;
                        dynamicVariable[ddlId + "_Yes"]--;
                    }
                    else if (PreviousValue == 2) {
                        dynamicVariable[ddlId + "_NA"] = 0;
                        dynamicVariable[ddlId + "_No"]--;
                    }
                }
            }
            else {
                arrayIndex.push(i);
                if ($.inArray(ddlId, arrayIds) >= 0) {
                    if (arraySelectedIds[i] == 1) {
                        dynamicVariable[ddlId + "_Yes"]++;
                    }
                    else if (arraySelectedIds[i] == 2) {
                        dynamicVariable[ddlId + "_No"]++;
                    }
                    else if (arraySelectedIds[i] == 3) {
                        dynamicVariable[ddlId + "_NA"] = 0;
                    }
                }
                else {
                    arrayIds.push(ddlId);
                    if (arraySelectedIds[i] == 1) {
                        dynamicVariable[ddlId + "_Yes"] = 1;
                        dynamicVariable[ddlId + "_No"] = 0;
                        dynamicVariable[ddlId + "_NA"] = 0;
                    }
                    else if (arraySelectedIds[i] == 2) {
                        dynamicVariable[ddlId + "_No"] = 1;
                        dynamicVariable[ddlId + "_Yes"] = 0;
                        dynamicVariable[ddlId + "_NA"] = 0;
                    }
                    else if (arraySelectedIds[i] == 3) {
                        dynamicVariable[ddlId + "_NA"] = 0;
                        dynamicVariable[ddlId + "_No"] = 0;
                        dynamicVariable[ddlId + "_Yes"] = 0;
                    }
                }
            }
            total = dynamicVariable[ddlId + "_Yes"] / (dynamicVariable[ddlId + "_Yes"] + dynamicVariable[ddlId + "_No"] + dynamicVariable[ddlId + "_NA"]) * 100;
            if (total >= 75) {
                $('.' + ddlId).html(total.toFixed(2) + '%');
                $('.' + ddlId).css('background-color', '#00b300');
            }
            else if (total < 75) {
                $('.' + ddlId).html(total.toFixed(2) + '%');
                $('.' + ddlId).css('background-color', '#ff3333');
            }
            else {
                $('.' + ddlId).html('NA');
                $('.' + ddlId).css('background-color', '#FFFFFF');
            }
            newArray = arraySelectedIds.slice();
        });
    });
});