$(document).ready(function () {
    
    $(function () {
       
        $(".date-picker").datepicker({
            
            minDate: '0',
            controlType: 'select',
            oneLine: true,
            changeMonth: true,
            changeYear: true,
            yearRange: "c-100:c+100", // You can set the year range as per as your need
            dateFormat: 'dd-M-yy'
            
        });

    });
    

});