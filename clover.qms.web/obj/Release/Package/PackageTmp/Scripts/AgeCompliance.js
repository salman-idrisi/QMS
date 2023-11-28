$(document).ready(function () {

    var arr = [];

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
                    }
                    else if (PreviousValue == 0) {
                        dynamicVariable[ddlId + "_Yes"]++;
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
                    }
                    else if (PreviousValue == 0) {
                        dynamicVariable[ddlId + "_No"]++;
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

                    else if (PreviousValue == 0) {
                        dynamicVariable[ddlId + "_NA"] = 0;
                    }

                }
                else {
                    if (PreviousValue == 1) {
                        dynamicVariable[ddlId + "_Yes"]--;
                    }
                    else if (PreviousValue == 2) {
                        dynamicVariable[ddlId + "_NA"]--;
                    }
                    else if (PreviousValue == 3) {
                        dynamicVariable[ddlId + "_NA"] = 0;
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
            for (i = 0, x = 0; i < array.length, x < array.length; i++, x++) {
               
                if ($("." + array[x]).text() != "NA" && $("." + array[x]).text().length != 37 && $("." + array[x]).text().length != 49) {
                    arr[i] = $("." + array[x]).text().replace("%", "");
                }
                else { arr.splice(i, 1); i--; }
            }
            var total1 = 0;
            for (var i = 0; i < arr.length; i++) {
                total1 += parseFloat(arr[i]);
            }
            var avg = total1 / arr.length;

            if (avg >= 75) {
                $('.compliance').html(avg.toFixed(2) + '%');
                $('.compliance').css('background-color', '#00b300');
            }
            else if (avg < 75) {
                $('.compliance').html(avg.toFixed(2) + '%');
                $('.compliance').css('background-color', '#ff3333');
            }
            else {
                $('.compliance').html('NA');
                $('.compliance').css('background-color', '#FFFFFF');
            }

        });



    });
})
