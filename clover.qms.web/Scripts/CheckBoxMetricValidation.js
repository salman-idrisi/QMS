$(document).ready(function () {
    $('#CheckAll').click(function () {
        if ($(this).prop("checked")) {
            $(".check-box").prop("checked", true);
        }
        else {
            $(".check-box").prop("checked", false);
        }
    });

    $(".select").each(function () {
        var dropvalue = $(this).val();
        if (dropvalue != 0) {
            $(this).parent().parent().find('.fifth-col').find('.cehck').prop('disabled', 'disabled');
        }
    });
});

var counter = 0;
function ValidateDDL(id) {
    var value = $(id).val();

    if (value != 0) {
        var txtMonth = $(id).parent().prev().find('input[type=text]').prop('required', true);
        $(id).parent().prev().find('input[type=text]').addClass('input-validation-error');
    }
    else {
        $(id).parent().prev().find('input[type=text]').prop('required', false);
        $(id).parent().prev().find('input[type=text]').removeClass('input-validation-error');
    }

    if (value == 2) {
        var next = $(id).parent().next().find('input[type=text]').prop('required', true);
        $(id).parent().next().find('input[type=text]').addClass('input-validation-error');
    }
    else {
        $(id).parent().next().find('input[type=text]').prop('required', false);
        $(id).parent().next().find('input[type=text]').removeClass('input-validation-error');
    }
}

function ValidateDDL1(id) {
    var value = $(id).parent().parent().find('.drop').find('.select').val();

    if ($(id).is(':checked')) {
        $(id).parent().parent().find('.drop').find('.select').prop('required', true);
        $(id).parent().parent().find('.drop').find('.select').addClass('input-validation-error');
    }
    else {
        var ddl = $(id).parent().parent().find('.drop').find('.select').prop('required', false);
        $(id).parent().parent().find('.drop').find('.select').removeClass('input-validation-error');
    }

    $(".cehck").each(function () {
        var count = $(".cehck").length;
        //alert(count);
        if ($(this).is(":checked")) {
            counter++;
        }

        //alert(counter);

        if (count == counter) {
            counter = 0;
            $('#CheckAll').prop("checked", true);
        }
        else {
            $("#CheckAll").prop("checked", false);
        }
    });
}