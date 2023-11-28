//function EditUser(Id) {
   
//   // var url = '@Url.Content("~/")' + "User/EditUser";
// url = "/User/EditUser";
//    $.ajax({
//        type: "GET",
//        url: url,
//        data: { 'UserId': Id },
//        contentType: "application/json; charset=utf-8",
//        dataType: "html",
//        success: function (response) {
            
//            $('#contentBody').html(response);
//            $('#myModal').modal('show');
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}
//function CreateUser() {
//    alert('hello');
//    //var url = '@Url.Content("~/")' + "User/AddUser";
//    url = "/User/AddUser";
//    $.ajax({
//        type: "GET",
//        url: url,
//        // data: '{RegistrationID: "' + + '" }',
//        contentType: "application/json; charset=utf-8",
//        dataType: "html",
//        success: function (response) {
//            $('#contentBody').html(response);
//            $('#myModal').modal('show');
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}

//function ShowUserDetails(id) {
//    //alert(id);
//    var url = '@Url.Content("~/")' + "User/UserDetails";
//    $.ajax({
//        type: "GET",
//        url: url,
//        data: { 'ID': id },
//        contentType: "application/json; charset=utf-8",
//        dataType: "html",
//        success: function (response) {
//            $('#contentBody').html(response);
//            $('#myModal').modal('show');
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}

//function Delete(id) {
//   // alert(id);
//    // var url = '@Url.Content("~/")' + "User/EditUser";
//    url = "/User/DeleteUser";
//    $.ajax({
//        type: "GET",
//        url: url,
//        data: { 'UserId': id },
//        contentType: "application/json; charset=utf-8",
//        dataType: "html",
//        success: function (response) {
//          //  alert(id);
//            $('#contentBody').html(response);
//            $('#myModal').modal('show');
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });

//    function ForgotPassword() {
//            alert('hello');
//            //var url = '@Url.Content("~/")' + "User/AddUser";
//            url = "/ForgotPassword/ForgotPassword";
//            $.ajax({
//                type: "GET",
//                url: url,
//                // data: '{RegistrationID: "' + + '" }',
//                contentType: "application/json; charset=utf-8",
//                dataType: "html",
//                success: function (response) {
//                    $('#contentBody').html(response);
//                    $('#myModal').modal('show');
//                },
//                failure: function (response) {
//                    alert(response.responseText);
//                },
//                error: function (response) {
//                    alert(response.responseText);
//                }
//            });
//        }
//}