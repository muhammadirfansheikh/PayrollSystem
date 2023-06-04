using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO.Compression;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.UI;
using DAL;
using System.Data;
using System.IO;
using System.Data.EntityClient;
using System.Data.SqlClient;


/// <summary>
/// Summary description for Common
/// </summary>
public class CommonHelper
{
    public static string URL = System.Configuration.ConfigurationManager.AppSettings["URL"];
    Base objBase = new Base();
    public CommonHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static List<int> GetDropDownValuesArray(DropDownList DropDowItem)
    {
        List<int> ObjLstString = new List<int>();

        foreach (ListItem item in DropDowItem.Items)
        {
            ObjLstString.Add(Convert.ToInt32(item.Value));
        }

        return ObjLstString;
    }
    public static ResponseHelper GetSampleUploadTypeFormat(Constant.FileUploadType file_Upload_type,int company_id) {
        ResponseHelper _ObjResponseHelper = new ResponseHelper();
        string _FileName = "";
        switch (file_Upload_type)
        { 
            case Constant.FileUploadType.Overtime:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 1);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                
                break;
            case Constant.FileUploadType.AbsentLog:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 2);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.Separation:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 3);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.ContractRenewal:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 4);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.Allowance:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 5);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.BankAccount:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 6);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.Increment:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 7);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.NewEmployee:
                if (company_id == 2021)
                {
                    _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 10);
                    _ObjResponseHelper = CheckAndGetFileData(_FileName);
                }
                else
                {
                    _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 8);
                    _ObjResponseHelper = CheckAndGetFileData(_FileName);
                }
                
                break;
            case Constant.FileUploadType.EmployeeEducationDetail:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 9);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.IncrementLetter:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 11);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.ConfirmationLetter:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 12);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.LeaveEncashment:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 13);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            case Constant.FileUploadType.GeneralData:
                _FileName = Enum.GetName(typeof(Constant.FileUploadTypeName), 14);
                _ObjResponseHelper = CheckAndGetFileData(_FileName);
                break;
            default:
                _ObjResponseHelper.ResponseMessage = "Sample Format File Not Found According To File Upload Type";
                _ObjResponseHelper.ResponseMessageType = Constant.ResponseType.WARNING;
                _ObjResponseHelper.ResponseData = "";
                break;
        }

        return _ObjResponseHelper;
    }
    private static ResponseHelper CheckAndGetFileData(string file_name)
    {
        ResponseHelper _objResponseHelper = new ResponseHelper();

        string _ServerFilePath = "/Uploads/File_Upload_Type_Sample_Format/"+file_name+"";
        string _ServerFilePathHostingEnvironment = System.Web.Hosting.HostingEnvironment.MapPath("~" + _ServerFilePath);

        if (File.Exists(_ServerFilePathHostingEnvironment+ ".xls") || File.Exists(_ServerFilePathHostingEnvironment + ".xlsx"))
        {
            _objResponseHelper.ResponseMessage = "File Found";
            _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
            if(File.Exists(_ServerFilePathHostingEnvironment + ".xls"))
             _objResponseHelper.ResponseData = _ServerFilePath+".xls";
            else if(File.Exists(_ServerFilePathHostingEnvironment + ".xlsx"))
                _objResponseHelper.ResponseData = _ServerFilePath + ".xlsx";
        }
        else
        {
            _objResponseHelper.ResponseMessage = "Sample File Format Of Upload Type Does Not Exist.";
            _objResponseHelper.ResponseMessageType = Constant.ResponseType.WARNING;
            _objResponseHelper.ResponseData = "";
        }

        return _objResponseHelper;
    }
    public static void BindCheckBoxList(CheckBoxList cbl, Object dataSource, string dataTextField, string dataValueField, bool hasAllItem = false, bool hasAllItemsSelected = false)
    {
        cbl.Items.Clear();
        cbl.DataSource = dataSource;
        cbl.DataTextField = dataTextField;
        cbl.DataValueField = dataValueField;
        cbl.DataBind();

        if (hasAllItem == true)
        {
            cbl.Items.Insert(0, new ListItem("All", "0"));
            //cbl.Items[0].Selected = true;
        }
        if (hasAllItemsSelected == true)
        {
            foreach (ListItem li in cbl.Items)
            {
                li.Selected = true;
            }
        }
    }
    public static string GetHash(string value)
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5");
    }
    public static void BindRadioButtonList(RadioButtonList rdbtnLstGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    {
        rdbtnLstGeneral.DataSource = dataSource;
        rdbtnLstGeneral.DataTextField = dataTextField;
        rdbtnLstGeneral.DataValueField = dataValueField;
        rdbtnLstGeneral.DataBind();

        if (hasSelectItem == true)
        {
            rdbtnLstGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        if (hasOtherItem == true)
        {
            rdbtnLstGeneral.Items.Add(new ListItem("Other", "-100"));
        }
    }
    public static bool IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table SapStagingTable,string ValueID,int PrimaryKey, Sybrid_DatabaseEntities ObjContext)
    {
        bool _Status = true;
        int _Count = 0;
         
        if (!String.IsNullOrEmpty(ValueID) && PrimaryKey == 0)
        {
            if (Constant.Sap_staging_Table.HRMS_Setup_BloodGroup == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_BloodGroup.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HRMS_Setup_EducationType == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_EducationType.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HRMS_Setup_Gender == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_Gender.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HRMS_Setup_MartialStatus == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_MartialStatus.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.TS_Setup_BusinessUnit == SapStagingTable)
                _Count = ObjContext.TS_Setup_BusinessUnit.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Category == SapStagingTable)
                _Count = ObjContext.Setup_Category.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_City == SapStagingTable)
                _Count = ObjContext.Setup_City.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_CostCenter == SapStagingTable)
                _Count = ObjContext.Setup_CostCenter.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Department == SapStagingTable)
                _Count = ObjContext.Setup_Department.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_EmployeeType == SapStagingTable)
                _Count = ObjContext.Setup_EmployeeType.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Location == SapStagingTable)
                _Count = ObjContext.Setup_Location.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Province == SapStagingTable)
                _Count = ObjContext.Setup_Province.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HCM_SETUP_SapCostCenter == SapStagingTable)
                _Count = ObjContext.HCM_SETUP_SapCostCenter.Where(x => x.ThirdPartyMappingId == ValueID && x.IsActitve == true).Count();


            if (_Count > 0)
                _Status = false;
            else if(_Count <= 0)
                _Status = true;

        }
        else if (!String.IsNullOrEmpty(ValueID) && PrimaryKey > 0)
        {
            if (Constant.Sap_staging_Table.HRMS_Setup_BloodGroup == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_BloodGroup.Where(x => x.ThirdPartyMappingId == ValueID && x.BloodGroupId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HRMS_Setup_EducationType == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_EducationType.Where(x => x.ThirdPartyMappingId == ValueID && x.educationTypeId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HRMS_Setup_Gender == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_Gender.Where(x => x.ThirdPartyMappingId == ValueID && x.GenderId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HRMS_Setup_MartialStatus == SapStagingTable)
                _Count = ObjContext.HRMS_Setup_MartialStatus.Where(x => x.ThirdPartyMappingId == ValueID && x.MartialStatusId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.TS_Setup_BusinessUnit == SapStagingTable)
                _Count = ObjContext.TS_Setup_BusinessUnit.Where(x => x.ThirdPartyMappingId == ValueID && x.BusinessUnitId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Category == SapStagingTable)
                _Count = ObjContext.Setup_Category.Where(x => x.ThirdPartyMappingId == ValueID && x.CategoryId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_City == SapStagingTable)
                _Count = ObjContext.Setup_City.Where(x => x.ThirdPartyMappingId == ValueID && x.CityId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_CostCenter == SapStagingTable)
                _Count = ObjContext.Setup_CostCenter.Where(x => x.ThirdPartyMappingId == ValueID && x.CostCenterId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Department == SapStagingTable)
                _Count = ObjContext.Setup_Department.Where(x => x.ThirdPartyMappingId == ValueID && x.DepartmentId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_EmployeeType == SapStagingTable)
                _Count = ObjContext.Setup_EmployeeType.Where(x => x.ThirdPartyMappingId == ValueID && x.EmployeeTypeId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Location == SapStagingTable)
                _Count = ObjContext.Setup_Location.Where(x => x.ThirdPartyMappingId == ValueID && x.LocationId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.Setup_Province == SapStagingTable)
                _Count = ObjContext.Setup_Province.Where(x => x.ThirdPartyMappingId == ValueID && x.ProvinceId != PrimaryKey && x.IsActive == true).Count();

            else if (Constant.Sap_staging_Table.HCM_SETUP_SapCostCenter == SapStagingTable)
                _Count = ObjContext.HCM_SETUP_SapCostCenter.Where(x => x.ThirdPartyMappingId == ValueID && x.SapCostId != PrimaryKey && x.IsActitve == true).Count();


            if (_Count > 0)
                _Status = false;
            else if (_Count <= 0)
                _Status = true;

        }

       

        return _Status;



    }
    public static void BindDropDown(ListBox lstGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    {
        lstGeneral.DataSource = dataSource;
        lstGeneral.DataTextField = dataTextField;
        lstGeneral.DataValueField = dataValueField;
        lstGeneral.DataBind();

        if (hasSelectItem == true)
        {
            lstGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        if (hasOtherItem == true)
        {
            lstGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
        }
    }
    public static void BindDropDown(CheckBoxList lstGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    {
        lstGeneral.DataSource = dataSource;
        lstGeneral.DataTextField = dataTextField;
        lstGeneral.DataValueField = dataValueField;
        lstGeneral.DataBind();

        if (hasSelectItem == true)
        {
            lstGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        if (hasOtherItem == true)
        {
            lstGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
        }
    }
    public static void BindDropDown(DropDownList ddlGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    {
        try
        { 
        ddlGeneral.DataSource = dataSource;
        ddlGeneral.DataTextField = dataTextField;
        ddlGeneral.DataValueField = dataValueField;
        ddlGeneral.DataBind();

        if (hasSelectItem == true)
        {
            ddlGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        if (hasOtherItem == true)
        {
            ddlGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
        }
        }
        catch (Exception ex)
        {
             
        }
    }
    public static string getSubstring(string content, int length)
    {
        string substr = content;
        if (content.Length > length)
        {
            substr = content.Substring(0, length) + "...";
        }

        return substr;
    }
    public static object Deserialize(string json, Type type)
    {
        return JsonConvert.DeserializeObject(json, type);
    }
    public static bool SalaryForeCast(int CompanyId, int EmployeeId, int UserKey)
    {
        Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeSalaryForecaster", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout =  Constant.ConnectionTimeout; 
        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
        da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
        da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserKey;
        da.Fill(dt);
        return true;
    }
    public static DataSet Get_Control_Data_EmployeeSearchFilter(string Type, int GroupId, int CompanyId, int BusinessUnitId, int JobCategoryId,
    int HasEmployeeType, int HasLocation, int HasBusinessUnit, int HasDepartment, int HasCostCenter,int HasSapCostCenter, int HasJobCategory, int HasDesignation,int UserKey)
    {
        Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
        DataSet ds = new DataSet();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);

        SqlDataAdapter da = new SqlDataAdapter("Get_Control_Data_EmployeeSearchFilter", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = Constant.ConnectionTimeout;
        da.SelectCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
        da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
        da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
        da.SelectCommand.Parameters.Add("@JobCategoryId", SqlDbType.Int).Value = JobCategoryId;
        da.SelectCommand.Parameters.Add("@LoginEmployeeId", SqlDbType.Int).Value = UserKey;
        da.SelectCommand.Parameters.Add("@HasEmployeeType", SqlDbType.Int).Value = HasEmployeeType;
        da.SelectCommand.Parameters.Add("@HasLocation", SqlDbType.Int).Value = HasLocation;
        da.SelectCommand.Parameters.Add("@HasSapCostCenter", SqlDbType.Int).Value = HasSapCostCenter;
        da.SelectCommand.Parameters.Add("@HasBusinessUnit", SqlDbType.Int).Value = HasBusinessUnit;
        da.SelectCommand.Parameters.Add("@HasDepartment", SqlDbType.Int).Value = HasDepartment;
        da.SelectCommand.Parameters.Add("@HasCostCenter", SqlDbType.Int).Value = HasCostCenter;
        da.SelectCommand.Parameters.Add("@HasJobCategory", SqlDbType.Int).Value = HasJobCategory;
        da.SelectCommand.Parameters.Add("@HasDesignation", SqlDbType.Int).Value = HasDesignation;
        da.Fill(ds);
        return ds;
    }
    public static string Encrypt_New(string Data)
    {
        AES_SHA2 security = new AES_SHA2();
        return security.Encrypt(Constant.SecurityKey, Data);
    }
    public static string Decrypt_New(string EncryptedData)
    {
        AES_SHA2 security = new AES_SHA2();
        return security.Decrypt(Constant.SecurityKey, EncryptedData);
    }
    public static string DecryptById(string EncryptedData, string Id)
    {
        string DecryptedQueryString = Decrypt_New(EncryptedData);

        DecryptedQueryString = DecryptedQueryString.After(Id + "=");
        int EndIndex = DecryptedQueryString.IndexOf("&");

        if (EndIndex == -1)
        {
            return DecryptedQueryString;
        }
        else
        {
            return DecryptedQueryString.Substring(0, EndIndex);
        }
    }
    public static void SetCertificateData(DataSet ds)
    {
        HttpContext.Current.Session["CertificateData"] = ds;
    }
    public static DataTable usp_UserLogin_UpdatePassword(int UserId, string OldPassword, string NewPassword, bool IsResetPassword, string UserIp, bool IsAdminReset, int AdminUserId)
    {
        DataTable dt = new DataTable();
        try
        {
            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            string connstr = context.Database.Connection.ConnectionString;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("Usp_UserLogin_UpdatePassword", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@User_Code", SqlDbType.Int).Value = UserId;
                da.SelectCommand.Parameters.Add("@OldHashPassword", SqlDbType.VarChar).Value = OldPassword;
                da.SelectCommand.Parameters.Add("@NewHashPassword", SqlDbType.VarChar).Value = NewPassword;
                da.SelectCommand.Parameters.Add("@IsResetPassword", SqlDbType.Bit).Value = IsResetPassword;
                da.SelectCommand.Parameters.Add("@UserIp", SqlDbType.VarChar).Value = UserIp;
                da.SelectCommand.Parameters.Add("@IsAdminReset", SqlDbType.VarChar).Value = IsAdminReset;
                da.SelectCommand.Parameters.Add("@AdminUserId", SqlDbType.VarChar).Value = AdminUserId;
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(string.Format("Error occured during Usp_UserLogin_UpdatePassword : {0}", ex.Message), ex);
        }
        return dt;
    }
    public static DataTable INSERT_INTO_AuditLog(int? ParentKey, string Primarykey, string TableName, int OperationTypeId)
    {
        DataTable dt = new DataTable();
        Base objBase = new Base();
        try
        {
            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            string connstr = context.Database.Connection.ConnectionString;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("INSERT_INTO_AuditLog", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@ParentKey", SqlDbType.Int).Value = ParentKey;
                da.SelectCommand.Parameters.Add("@Primarykey", SqlDbType.VarChar).Value = Primarykey;
                da.SelectCommand.Parameters.Add("@TableName", SqlDbType.VarChar).Value = TableName;
                da.SelectCommand.Parameters.Add("@OperationTypeId", SqlDbType.Int).Value = OperationTypeId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserId;
                da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = objBase.UserIP; 
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(string.Format("Error occured during INSERT_INTO_AuditLog : {0}", ex.Message), ex);
        }
        return dt;
    }

}
