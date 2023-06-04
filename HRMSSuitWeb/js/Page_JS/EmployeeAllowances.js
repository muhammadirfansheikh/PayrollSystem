
function GetMappedAllowances() { 
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetMappedAllowancesByEmployeeId(EmployeeId, onGetMappedAllowances, null, null);
} 
function onGetMappedAllowances(result) { 
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyMappedAllowances').html('');
    $('#MappedAllowances').tmpl(res).appendTo(divTbodyGoalFund);
    GetAllowances();
}



function onEditAllowances(selector) {    
    var Measure = $(selector).closest('tr').find('.MeasureTD');
    if ($(selector).closest('tr').find('.txtMeasure').val() == undefined) {
        if (Measure.html() != 'Formula') {
            val = Measure.html();
            Measure.html('');
            Measure.append('<input type="text" class="txtMeasure" value="' + val + '" /><input type="button" value="Update" onclick="onUpdateAllowance(this)" class="btn btn-xs btn-primary" /> ');
        }
        else {
            showError('Allowances Based On Formula Cannot Be Edit');
        }
    }

} 
function onUpdateAllowance(selector) {
    objRow = $(selector).closest('tr');
    Measure = objRow.find('.txtMeasure');

    valMeasure = Measure.val();
    //AllowanceID = objRow.find('.AllowanceID').val();
    AllowanceID = objRow.find('.SetupAllowanceID').val();

    UpdateAllowanceById(AllowanceID, valMeasure);

    Measure.remove();
    var Measure = objRow.find('.MeasureTD').append(valMeasure);
    $(selector).remove();

} 
function UpdateAllowanceById(AllowanceID, valMeasure) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.UpdateAllowanceById(CompanyId,EmployeeId,AllowanceID, valMeasure, onUpdateAllowanceById, null, null);
}
function onUpdateAllowanceById(result) {
    if (result.ResponseMessageType == 1) {
        GetMappedAllowances();
        showSuccess(result.ResponseMessage);
    }
    else {
        showError(result.ResponseMessage);
    }
}


function SetLoanGivenDate() {
    
    let _sanctionalDate = $('.txtSanctionDate').val();
    $('.txtLoanGivenDate').val(_sanctionalDate);
}

 

function DeleteAllowanceById(AllowanceID) { 
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.DeleteAllowanceById(CompanyId,AllowanceID, EmployeeId, onDeleteAllowanceById, null, null);
} 
function onDeleteAllowanceById(result) { 
    if (result == 1)
        showSuccess('Successfully Deleted!');
    GetMappedAllowances();
    //GetAllowances();
    ProgressBarHide();
} 
function onDeleteAllowance(selector) { 
    if (confirm("Are you sure you want to delete?") == true) {
        objRow = $(selector).closest('tr');
        //AllowanceID = objRow.find('.AllowanceID').val();
        AllowanceID = objRow.find('.SetupAllowanceID').val();
        DeleteAllowanceById(AllowanceID);
    }
}



function GetAllowances() {    
    ListOfIds = getCommaSeparatedValues('.SetupAllowanceID');
    var service = new HrmsSuiteHcmService.HcmService();
   // service.getAllowances(CompanyId, ListOfIds, onGetAllowances, null, null);
    service.getAllowances(CompanyId, EmployeeId, onGetAllowances, null, null);
    
}

function onGetAllowances(result) {    
    resAllowances = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlAllowancesList', resAllowances);
    $('.ddlAllowancesList').change();
}


function onAllownceSelect() { 
    $('.divReleaseAtOnce').hide();
    AllowanceId = $('.ddlAllowancesList').val();
    HasFormula = null;
    IsTaxableIncome = null;
    $(resAllowances).each(function (a, b) { $(b).each(function (k, v) { if (v.Id == AllowanceId) { HasFormula = v.IsFormulaExist; IsTaxableIncome = v.IsTaxableIncome } }) })

    if (HasFormula || AllowanceId == 0)
        $('.divMeasure').hide();
    else
        $('.divMeasure').show();

    if (IsTaxableIncome != null) {
        // $('.divCheckTaxable').show();
    }
    else {
        $('.divCheckTaxable').hide();

    }
}

function forecast_MapAllowance() {
   
    if ($('.txtMeasure').is(':visible')) {
        Measure = $('.txtMeasure').val();
        if (!validateForm('.frmControlsMappingAll'))
            return;
    }
    else {
        Measure = null;
        if (!validateForm('.divAllowances'))
            return;
    }
    if (!validateForm('.divReleaseAtOnce'))
        return;

    ProgressBarShow();
    AllowanceId = $('.ddlAllowancesList').val();
    YearId = $('.ddlTaxYearAllowanceMapping').val();
    ReleaseMonth = $('.txtReleaseTaxMonth').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.Forecast_MapAllowance(EmployeeId, AllowanceId, Measure, ReleaseMonth, onForecastandMap, null, null);
}

function onForecastandMap(result) {
    //if (result == 1)
    //    showSuccess('Successfully Completed!');

    GetMappedAllowances();
    //GetAllowances();
    ProgressBarHide();
}


function onMapAllowance() {
    if ($('#chkTaxable').is(':checked')) {
        forecast_MapAllowance();
        return;
    }

    if ($('.txtMeasure').is(':visible')) {
        Measure = $('.txtMeasure').val();
        if (!validateForm('.frmControlsMapping'))
            return;
    }
    else {
        Measure = null;
        if (!validateForm('.divAllowances'))
            return;
    }

    ProgressBarShow();
    AllowanceId = $('.ddlAllowancesList').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.MapAllowance(EmployeeId, AllowanceId, Measure, onMap, null, null);
}


function onMap(result) {
    if (result == 1)
        showSuccess('Allowance Has Been Mapped Successfully');
    $('.txtMeasure').val('');
    GetMappedAllowances();
    //GetAllowances();
    ProgressBarHide();
}

function toggleReleaseTaxMonth(selector) {
    if ($(selector).is(':checked')) {
        $('.divReleaseAtOnce').show();
    }
    else {
        $('.divReleaseAtOnce').hide();
    }
}
