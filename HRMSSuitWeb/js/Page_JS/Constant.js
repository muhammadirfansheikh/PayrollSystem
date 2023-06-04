

ProgressBarHide();
DatePicker();
DatePickerMonth();
DatePickerMonthComplete();
DatePickerComplete();
DatePickerYear();
DatePickerMonth_Year();
stopKeyPress();

var HCM_SetupMaster = {
    Vehicle: 10,
    Manufacturer: 11,
    ArrearType: 13,
    LoanType: 6,
    OverTimeRate: 14,
    SalaryStandard: 18,
    FuelType: 22,
    VehicleVariant: 23,
    YearSlabs: 34,
    IncrementType: 33,
    VehicleName: 12,
};


var HCM_SetupDetail =
{
    BasicInterest: 78,
    PFLoanTypeId: 97
}

var HCM_TaxLaw =
{
    TaxLaw34: 8
}

var addSerialNumber = function () {
    $('table tbody .trList').each(function (index) {
        $(this).find('td:nth-child(1)').html(index + 1);
    });
};

var HCM_ArrGroupBy = [{
    Id: 'clsLocation',
    Value: 'Location'
},
{
    Id: 'clsCompany',
    Value: 'Company'
},
{
    Id: 'clsDepartment',
    Value: 'Department'
}, {
    Id: 'clsDesignation',
    Value: 'Designation'
}, {
    Id: 'clsCostCenter',
    Value: 'Cost Center'
}, {
    Id: 'clsSapCostCenter',
    Value: 'Sap Cost Center'
},
{
    Id: 'clsBankName',
    Value: 'Bank'
}
];

var HCM_UploadType =
    [
        {
            Id: 'Overtime',
            Value: 'Overtime'
        },
        {
            Id: 'AbsentLog',
            Value: 'AbsentLog'
        },
        {
            Id: 'Separation',
            Value: 'Separation'
        },
        {
            Id: 'ContractRenewal',
            Value: 'ContractRenewal'
        },
        {
            Id: 'Allowance',
            Value: 'Allowance'
        },
        {
            Id: 'BankAccount',
            Value: 'BankAccount'
        },
        {
            Id: 'Increment',
            Value: 'Increment'
        },
        {
            Id: 'NewEmployee',
            Value: 'NewEmployee'
        },
        {
            Id: 'EmployeeEducationDetail',
            Value: 'Employee Education Detail'
        },
        {
            Id: 'IncrementLetter',
            Value: 'Increment Letter (Temporary)'
        },
        {
            Id: 'ConfirmationLetter',
            Value: 'Confirmation Letter'
        },
        {
            Id: 'LeaveEncashment',
            Value: 'Leave Encashment'
        },
        {
            Id: 'GeneralData',
            Value: 'General Data'
        }
    ];

function toggleDiv(div) {
    $(div).toggle();
}

function GetURLVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function formatDate(_date) {
    if (_date == null)
        return;

    if (_date.indexOf('/') != -1) {
        var retDate = _date.substr(6, 4) + '-' + _date.substr(0, 2) + '-' + _date.substr(3, 2);
        return retDate;
    }
    else {
        if (_date == '' || _date == null || _date == undefined)
            return "";

        var d = new Date(_date);
        var month = (d.getMonth() + 1);
        var date = d.getDate();

        if (month >= 1 && month <= 9)
            month = '0' + month;

        if (date >= 1 && date <= 9)
            date = '0' + date;

        return d.getFullYear() + '-' + month + '-' + date;
    }
}

function serializeData(selector) {
    data = "";
    $(selector).find('input, select, textarea').each(function () {
        data += $(this).attr("name") + "=" + $(this).val() + "&";
    });
    return data.replace(/&$/g, "");
}

function CSS_BorderAndColor(selector, color) {
    $(selector).css("border-style", "solid");
    $(selector).css("border-weight", "5px");
    $(selector).css("border-color", color);
    $(selector).css("border-width", "1px");
}

function validateForm(selector) {
    check = true;
    $(selector).find('select').each(function () {
        if ($(this).val() == 0 || $(this).val() == null) {
            CSS_BorderAndColor(this, 'red');
            showError('Check All Mandotary Fields');
            check = false;
        }
        else
            CSS_BorderAndColor(this, 'green');
    });

    $(selector).find('input, textarea').each(function () {
        if ($(this).val() == "") {
            CSS_BorderAndColor(this, 'red');
            showError('Check All Mandotary Fields');
            check = false;
        }
        else
            CSS_BorderAndColor(this, 'green');
    });
    return check;
}

function FillDropDownByReference(DropDownReference, res) {

    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function ProgressBarShow() {

    $('#waitProgressBar').show();
    $('.loader').show();
}

function ProgressBarHide() {
    $('#waitProgressBar').hide();
    $('.loader').hide();
}

function cloneDiv(divToClone, divToAppend) {
    if (!validateForm(divToAppend))
        return;
    var cloned = $(divToClone).last().clone();
    resetControls(cloned);
    cloned.appendTo(divToAppend)
    DatePicker();
}

function showError(message) {
    AlertBox('Error!', message, 'error');
}

function showSuccess(message) {
    AlertBox('Success!', message, 'success');
}

function AlertBox(title, Message, type) {
    swal(title, Message, type);
}


function DatePicker() {
    $('.DatePicker').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        //startDate: '2d+',
        autoclose: true
    });

    $('.DatePicker').keydown(function () {
        return false;
    });
}

function DatePickerComplete() {
    $('.DatePickerComplete').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: "dd-M-yyyy",
        todayHighlight: true,

    });
    $('.DatePickerComplete').keydown(function () {
        return false;
    });
}


function DatePickerMonth() {
    $('.DatePickerMonth').datepicker({
        minViewMode: 1,
        keyboardNavigation: false,
        forceParse: false,
        autoclose: true,
        startDate: '2d+',
        todayHighlight: true
    });
    $('.DatePickerMonth').keydown(function () {
        return false;
    });
}

function DatePickerYear() {
    $('.DatePickerYear').datepicker({
        minViewMode: 'years',
        keyboardNavigation: false,
        forceParse: false,
        autoclose: true,
        startDate: '2d+',
        todayHighlight: true,
        format: 'yyyy'
    });
    $('.DatePickerYear').keydown(function () {
        return false;
    });
}


function DatePickerMonthComplete() {
    $('.DatePickerMonthComplete').datepicker({
        minViewMode: 1,
        keyboardNavigation: false,
        forceParse: false,
        autoclose: true,
        todayHighlight: true,
        format: "dd-M-yyyy",
    });
    $('.DatePickerMonthComplete').keydown(function () {
        return false;
    });
}


function DatePickerMonth_Year() {
    $('.DatePickerMonth_Year').datepicker({
        minViewMode: "months",
        keyboardNavigation: false,
        forceParse: false,
        autoclose: true,
        todayHighlight: true,
        calendarWeeks: true,
        format: "M-yyyy",
        viewMode: "months"
    });
    $('.DatePickerMonth_Year').keydown(function () {
        return false;
    });
}



function removeCloneDivs(div) {
    $(div).not(':first').remove();
}

function removeSelectedDiv(child, div) {
    if ($(child).closest(div).siblings().size() != 0)
        $(child).closest(div).remove();
}

function resetControls(selector) {
    $(selector).find('input, textarea').each(function () {
        if ($(this).is(":button")) {
            // Do things
        }
        else {
            if ($(this).hasClass("txtInstAmt")) {
                $(this).val('0');
            }
            else {
                $(this).val('');
            }

        }
    });
    $(selector).find('select').each(function () {
        $(this).val(0).change();
    });
}



function resetControlsWithoutChangeEvent(selector) {
    $(selector).find('input, textarea').each(function () {
        if ($(this).is(":button")) {
            // Do things
        }
        else {
            $(this).val('');
        }
    });
    $(selector).find('select').each(function () {
        $(this).val(0);
    });
}

function getCommaSeparatedValues(selector) {
    returnString = "";
    $(selector).each(function () {
        if ($(this).val() != null)
            returnString += $(this).val() + ','
    });
    return returnString + "0";

}


function getCommaSeparatedValues_CheckedBoxes(selector) {
    returnString = "";
    $(selector).each(function () {
        if ($(this).prop('checked')) {
            if ($(this).val() != null)
                returnString += $(this).val() + ','
        }
    });
    return returnString + "0";
}


function validateNumeric() {
    $('.numericOnly').keydown(function (e) {
        if (e.keyCode != 8) {
            if ($(this).val().indexOf(".") > 0 && e.keyCode == 110)
                return false;

            if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || (e.keyCode == 110) || (e.keyCode == 190))) {
                return false;
            }
        }
    });
}


function paginateTable(cls, lengthPerPage) {

    var totalRows = $(cls).find('tbody tr:has(td)').length;
    var recordPerPage = lengthPerPage;
    var totalPages = Math.ceil(totalRows / recordPerPage);

    var $pages = $(cls).find('tfoot');

    $pages.html('');

    var $select = $('<tr  ><td colspan="2"><select  class="form-control" id="tableSelect"><select></td><td><button type="button" id="prevv" class="btn btn-white"><i class="fa fa-chevron-left"></i> </button><button id="nextt" type="button" class="btn btn-white"><i class="fa fa-chevron-right"></i> </button></td>  </tr>').appendTo($pages);


    for (i = 0; i < totalPages; i++) {
        $('<option value = "' + (i + 1) + '">' + 'Page: ' + (i + 1) + '</option>').appendTo('#tableSelect');
    }



    $(cls).find('tbody tr:has(td)').hide();
    var tr = $('table tbody tr:has(td)');

    for (var i = 0; i <= recordPerPage - 1; i++) {
        $(tr[i]).show();
    }


    $('#prevv').click(function () {
        if (parseInt($('#tableSelect').val()) != 1)
            $('#tableSelect').val($('#tableSelect').val() - 1).change();
    });


    $('#nextt').click(function () {
        if (!(parseInt($('#tableSelect').val()) + 1 > totalPages))
            $('#tableSelect').val(parseInt($('#tableSelect').val()) + 1).change();
    });

    $('#tableSelect').change(function (event) {
        $(cls).find('tbody tr:has(td)').hide();
        var nBegin = ($(this).val() - 1) * recordPerPage;
        var nEnd = $(this).val() * recordPerPage - 1;
        $('#recordDisp').text(nBegin + ' ' + nEnd);
        for (var i = nBegin; i <= nEnd; i++) {
            $(tr[i]).show();
        }
    });



}




function removeBlankandNulls(val) {
    if (val == '' || val == null || val == undefined)
        return '----';
    else
        return val;
}


function stopKeyPress() {
    $('.keylock').keypress(function (event) {
        event.preventDefault();
    })
}


function FileUploadHandler(fileselector) {
    ProgressBarShow();
    var fileUpload = $(fileselector).get(0);
    var files = fileUpload.files;
    var data = new FormData();
    if (files.length <= 0) {
        showError('No File Uploaded!');
        ProgressBarHide();
        return '';
    }

    var filename = '';
    var isError = false;
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }
    $.ajax({
        url: "UploadHandler.ashx",
        type: "POST",
        data: data,
        async: false,
        contentType: false,
        processData: false,
        success: function (result) { filename = result },
        error: function (err) { showError(err); isError = true; }
    });

    ProgressBarHide();
    return filename;
}


//function excelOutWithouHiddenFields(selector) {
//    $(selector).table2excel({
//        name: "Data",
//        filename: "myFileName",
//        fileext: ".xls"
//    });



//}


function excelOutWithouHiddenFields(selector, fileName = "myFileName") { 
    var copyTable = $(selector).clone(false).attr('id', '_copy_dailySales');
    copyTable.insertAfter($(selector))
    copyTable.find('td input[type=hidden]').remove();

    let final = copyTable;
    copyTable.remove();
    final.table2excel({
        name: "Data",
        filename: fileName,
        fileext: ".xls"
    });

}


function excelExportWithFileSaver(selector, fileName) { 
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById(selector); // id of table

    for (j = 0; j < tab.rows.length; j++) {

        var _val = $(tab.rows[j]).find('.ABC').text().toString();

        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var Qlik = new Blob([tab_text], {
        type: "application/vnd.ms-excel;charset=utf-8"
    });

    saveAs(Qlik, fileName + ".xls");

}

function excelExport(tableid, fileName) {

    saveAsExcel(tableid, fileName)


}


function sendPrint(selector) {
    $(selector).printThis();
}

function BindGroupByDDL() {
    //alert(HCM_ArrGroupBy.length);
    FillDropDownByReference('.ddlGroupBy', HCM_ArrGroupBy);
}

function GetCurrentDate(selector, text) {
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = d.getFullYear() + '-' +
        (('' + month).length < 2 ? '0' : '') + month + '-' +
        (('' + day).length < 2 ? '0' : '') + day;

    $(selector).text(text + output);
}

var sortByProperty = function (property) {

    return function (x, y) {

        return ((x[property] === y[property]) ? 0 : ((x[property] > y[property]) ? 1 : -1));

    };

};


$(".decimals").on("keypress", function (evt) {
    var $txtBox = $(this);
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
        return false;
    else {
        var len = $txtBox.val().length;
        var index = $txtBox.val().indexOf('.');
        if (index > 0 && charCode == 46) {
            return false;
        }
        if (index > 0) {
            var charAfterdot = (len + 1) - index;
            if (charAfterdot > 3) {
                return false;
            }
        }
    }
    return $txtBox; //for chaining
});

$(".integers").on("keypress", function (evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
});

$('.integers').on("cut copy paste", function (e) {
    e.preventDefault();
});

$('.decimals').on("cut copy paste", function (e) {
    e.preventDefault();
});

$('.DatePickerMonth_Year').on("cut copy paste", function (e) {
    e.preventDefault();
});

$('.DatePicker').on("cut copy paste", function (e) {
    e.preventDefault();
});

$('.DatePickerMonth').on("cut copy paste", function (e) {
    e.preventDefault();
});

$('.DatePickerMonthComplete').on("cut copy paste", function (e) {
    e.preventDefault();
});

$('.DatePickerComplete').on("cut copy paste", function (e) {
    e.preventDefault();
});

$('.DatePickerYear').on("cut copy paste", function (e) {
    e.preventDefault();
});


function _GetDate(date) {
    //;
    var date_ = new window.Date(date);
    //alert(date_);
    var Day = date_.getDate();
    var Month = date_.getMonth();
    var Year = date_.getFullYear();

    if (Month == '0') {
        Month = 'Jan';
    }
    else if (Month == '1') {
        Month = 'Feb';
    }
    else if (Month == '2') {
        Month = 'Mar';
    }
    else if (Month == '3') {
        Month = 'Apr';
    }
    else if (Month == '4') {
        Month = 'May';
    }
    else if (Month == '5') {
        Month = 'Jun';
    }
    else if (Month == '6') {
        Month = 'Jul';
    }
    else if (Month == '7') {
        Month = 'Aug';
    }
    else if (Month == '8') {
        Month = 'Sep';
    }
    else if (Month == '9') {
        Month = 'Oct';
    }
    else if (Month == '10') {
        Month = 'Nov';
    }
    else if (Month == '11') {
        Month = 'Dec';
    }
    var _Date = Day + "-" + Month + "-" + Year;
    return _Date;
}
