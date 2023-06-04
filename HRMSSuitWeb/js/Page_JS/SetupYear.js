

var _YearId = null;


function pageLoad() {
    GetGroup();
}

function GetGroup() {
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getGroup(onGetGroup, null, null);
}

function onGetGroup(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlGroup", res);
    $(".ddlGroup").val(res[0].Id).change();
    ProgressBarHide();
}

function GetCompany(Group) {

    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getCompanyByGroupId($(Group).val(), onGetCompany, null, null);
}

function onGetCompany(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCompany", res);
    $(".ddlCompany").change();
    ProgressBarHide();
}


function DeleteYearSlab(selector) {
    var YearId = $(selector).closest('tr').find('.hdnYearId').val();
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.DeleteSlabYear(YearId, onDeleteYearSlab, null, null);
}

function onDeleteYearSlab(result) {
    ProgressBarHide();
    if (result == 1)
        showSuccess('Successfully Deleted!');
    GetTaxYear();
}

function EditYearSlab(selector) {
    var objRow = $(selector).closest('tr');
    var YearId = objRow.find('.hdnYearId').val();
    var HasTransaction = objRow.find('.hdnHasTransaction').val();
    var YearFrom = reFormatDate(objRow.find('.hdnYearFrom').val());
    var YearTo = reFormatDate(objRow.find('.hdnYearTo').val());
    var IsCurrentActiveYear = objRow.find('.hdnIsCurrentActiveYear').val();
    var GroupId = objRow.find('.hdnGroupId').val();
    var CompanyId = objRow.find('.hdnCompanyId').val();
    _YearId = YearId;
    $('.ddlGroup').val(GroupId);
    $('.txtYearFrom').val(YearFrom).keyup();
    $('.txtYearTo').val(YearTo).keyup();
    $('.ddlCompany').val(CompanyId);
    $('#chkActiveYear').prop('checked', IsCurrentActiveYear == 'true' ? true : false);

}

function reFormatDate(val) {
    var year = val.substr(0, 4);
    var month = val.substr(val.indexOf('-') + 1, 2);
    var day = val.substr(8, 2);
    return month + '/' + day + '/' + year;
}

function GetTaxYear() {
    $('.tbodyYearSlabListing').html('');
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getTaxYearListing($('.ddlCompany').val(), onGetTaxYear, null, null);
}

function onGetTaxYear(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyYearSlabListing').html('');
    $('#YearSlabListing').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}

function SaveTaxYear() {
    if (!validateForm('.frmSlabYear'))
        return;

    var ActiveYear = false;
    if ($('#chkActiveYear').is(':checked'))
        ActiveYear = true;

    var CompanyId = $('.ddlCompany').val();
    var FromYear = formatDate($('.txtYearFrom').val());
    var ToYear = formatDate($('.txtYearTo').val());
    var ValidateSlab = true;
    var intFromYear = parseInt(FromYear.replace('-', '').replace('-', ''));
    var intToYear = parseInt(ToYear.replace('-', '').replace('-', ''));
    if (intFromYear >= intToYear) {
        showError('(Slab From) cannot be greater and equal to (Slab To)');
        return;
    }

    $('.tbodyYearSlabListing').find('tr').each(function () {

        var chYearFrom = parseInt(formatDate($(this).find('.hdnYearFrom').val()).replace('-', '').replace('-', ''));
        var chYearTo = parseInt(formatDate($(this).find('.hdnYearTo').val()).replace('-', '').replace('-', ''));
        if (intFromYear >= chYearFrom && intFromYear <= chYearTo)
            ValidateSlab = false;
    });

    if (!ValidateSlab) {
        showError('Invalid Year Slab!');
        return;
    }

    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.saveTaxYear(CompanyId, FromYear, ToYear, ActiveYear, _YearId, onSaveTaxYear, null, null);
}

function onSaveTaxYear(result) {
    if (result == 1)
        ProgressBarHide();
    showSuccess('Successfully Updated!');
    _YearId = null;
    resetControls('.frmSlabYear');
    $('#chkActiveYear').val(1);
    GetTaxYear();
}

function ReActiveYearSlab(selector) {
    var YearId = $(selector).closest('tr').find('.hdnYearId').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.reactiveSlabYear(YearId, onReActiveYearSlab, null, null);
}

function onReActiveYearSlab(result) {
    if (result == 1)
        showSuccess('Successfully Updated!');
    GetTaxYear();
}

function ClearFields() {
    $(".txtYearFrom").val('');
    $(".txtYearTo").val('');
    $(".ddlCompany").val('0');
    $(".ddlCompany").change();
    $('.tbodyYearSlabListing').html('');
}


