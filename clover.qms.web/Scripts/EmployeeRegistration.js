
//$(document).ready(
//    function (){
//        alert("called");
//        $(".date-picker").datepicker({
//            yearRange: "-100:+0", // You can set the year range as per as your need
//            dateFormat: 'dd-M-yy',
//            changeMonth: true,
//            changeYear: true,
//            maxDate: '0'
//        });
    
//    function lettersOnly(event) {
//        var charCode = event.keyCode;
//        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8)

//            return true;
//        else
//            return false;
//    };
//    function NumbersOnly(event) {
//        if (event.keyCode == '9' || event.keyCode == '16') {
//            return;
//        }
//        var code;
//        if (event.keyCode) code = event.keyCode;
//        else if (event.which) code = event.which;
//        if (event.which == 46)
//            return false;
//        if (code == 8 || code == 46)
//            return true;
//        if (code < 48 || code > 57)
//            return false;
//    };
//    //Checking EmailID already exist or not
//    function CheckEmailAvailability() {
//        $.post("@Url.Action("CheckEmailAvailability", "User")",
//            {
//                EmailId: $("#EmailId").val()
//            },
//            function (data) {
//                if (data > 0) {
//                    $('#EmailStatus').delay(2000).fadeOut(function (data) {
//                        $("#EmailStatus").load(location.href + "#EmailStatus");
//                    });
//                    $('#EmailId').val("");
//                    $("#EmailStatus").html('<font color="Red">EmailID already exist.</font>');

//                }
//            });
//    }
//    //Checking UserName already exist or not
//    function CheckUserNameAvailability() {
//        $.post("@Url.Action("CheckUserNameAvailability", "User")",
//            {
//                name: $("#username").val()
//            },
//            function (data) {
//                if (data > 0) {
//                    //setInterval(function (data) {
//                    //    $("#UserNameStatus").load(location.href + "#UserNameStatus");
//                    //}, 3000);
//                    $('#UserNameStatus').delay(2000).fadeOut(function (data) {
//                        $("#UserNameStatus").load(location.href + "#UserNameStatus");
//                    });
//                    $('#username').val("");
//                    $("#UserNameStatus").html('<font color="Red">Username already exist.</font>');

//                }
//            });
//    }
//    //Checking Mobile number already exist or not
//    function CheckMobileNumberAvailability() {
//        $.post("@Url.Action("CheckMobileNOAvailability", "User")",
//            {
//                number: $("#mobileNo").val()
//            },
//            function (data) {
//                if (data > 0) {
//                    $('#MobileStatus').delay().fadeOut(2000, function (data) {
//                        //location.reload(true);
//                        $("#MobileStatus").load(location.href + "#MobileStatus");
//                    });
//                    $("#mobileNo").val("");
//                    $("#MobileStatus").html('<font color="Red">Mobile number already exist.</font>');
//                }
//            });
//    }
//});

    
