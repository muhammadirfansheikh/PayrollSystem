
function SetTriggers() {
    GetCompany();

    $(".ddlCompany").change(function () {
        GetBusinessUnit($(".ddlCompany").val());
    });

    $(".ddlBusinessUnit").change(function () {
        GetDepartment($(".ddlBusinessUnit").val());
    });

    GetList();

    /*
    GetProducts();
    GetDepartments();
    getLevel();
    getRequestType();
    getRequestMode();
    GetCustomerType();
    $(".ddlCustomerType").change(function () {
        GetCustomer($(this).val());
        $(".ddlCustomer").change();
    });
    $(".ddlCustomer").change(function () {
        GetCustomerAddress($(this).val());
        GetPOCEmails($(this).val());
    });
    $(".ddlDepartment").change(function () {
        getAssignee($(this).val(), 0);
        getLevel($(this).val());
        GetServiceCategory($(this).val());
    });
    $(".ddlLevel").change(function () {
        getAssignee($(".ddlDepartment").val(), $(this).val());
    });
    $(".ddlServiceCategory").change(function () {
        GetServiceSubcategory($(".ddlDepartment").val(), $(this).val());
    });
    $(".ddlServiceSubCategory").change(function () {
        GetServiceRequest($(".ddlDepartment").val(), $(this).val());
    });
    $(".ddlServiceRequest").change(function () {
        getPriority($(this).val());
    });
    $(".ddlProduct").change(function () {
        GetPreRequisite($(this).val());
    });
    */
}

function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function GetCompany() {
    //;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCompany(onGetCompany, null, null); 
}

function onGetCompany(result) {

    //alert(result);
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCompany", res);
    $(".ddlCompany").change();
}

function GetBusinessUnit(CompanyId) {
    //;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getBusinessUnit(CompanyId,onGetBusinessUnit, null, null);
}

function onGetBusinessUnit(result) {

    //alert(result);
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlBusinessUnit", res);
    $(".ddlBusinessUnit").change();
}

function GetDepartment(BusinessUnitId) {
    //;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDepartment(BusinessUnitId, onGetDepartment, null, null);
}

function onGetDepartment(result) {

    //alert(result);
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDepartment", res);

}

function GetList() {
    //;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getList(onGetList, null, null);
}

function onGetList(result) {

    //;
    var res = jQuery.parseJSON(result);

    //alert(Reference);
    //alert(result);

    //var Datalist = document.getElementById($(Reference).closest(".location").find("#gvEmployees").attr("id"));
    //var Response = [];

    $(res).each(function () {
        //Response.push(this.Value);

        //alert(this.Value);
        //alert(this.Id);

        $(".gvEmployees").append("<tr><td class='project-title'>" + this.Value +
                                           "</td><td class='project-title'>" + this.Id + "</td></tr>");
    });



    /*
    $(Reference).on('keyup', function () {
        if (this.value.length === 0) {
            return;
        }

        // Send Ajax request and loop of its result

        Datalist.textContent = '';
        for (var i = 0; i < Response.length; i++) {
            if (Response[i].indexOf(this.value) !== 0) {
                continue;
            }
            var option = document.createElement('option');
            option.value = Response[i];
            Datalist.appendChild(option);
        }
    });
    */
}

