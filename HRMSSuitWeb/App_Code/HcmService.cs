using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Transactions;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using DAL;
using System.Data.SqlClient;


/// <summary>
/// Summary description for HcmService
/// </summary>
/// 

[ServiceContract(Namespace = "HrmsSuiteHcmService")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

public class HcmService
{
    DAL.Sybrid_DatabaseEntities context = new DAL.Sybrid_DatabaseEntities();
    Base objBase = new Base();
    int? IntNull = null;
    int ConnectionTimeout = Constant.ConnectionTimeout;

    [OperationContract]
    public string getGroup()
    {
        var List = context.Setup_Group.Where(x => x.IsActive == true).OrderBy(x => x.GroupName).Select(s => new
        {
            Value = s.GroupName,
            Id = s.GroupId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getCompany()
    {
        //var List = context.Setup_Company.Where(x => x.IsActive == true).OrderBy(x => x.CompanyName).Select(s => new
        //{
        //    Value = s.CompanyName,
        //    Id = s.CompanyId
        //}).ToList();

        var List = context.Setup_EmployeeCompanyMapping.Where(a => a.IsActive == true && a.EmployeeId == objBase.UserKey && a.CompanyId == objBase.CompanyId)
            .Select(a => new
            {
                Value = a.Setup_Company.CompanyName,
                Id = a.Setup_Company.CompanyId
            }).ToList();

        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getCompanyByGroupId(int GroupId)
    {

        //var List = context.Setup_EmployeeCompanyMapping.Where(a => a.IsActive == true && a.EmployeeId == objBase.UserKey &&
        //a.Setup_Company.GroupId == GroupId && a.Setup_Company.IsActive == true)
        //    .Select(a => new
        //    {
        //        Value = a.Setup_Company.CompanyName,
        //        Id = a.Setup_Company.CompanyId
        //    })
        //    .OrderBy(b => b.Value)
        //    .ToList();

        ////var _lst = List.Where(Id )
        //var JSON = JsonConvert.SerializeObject(List);
        var JSON = Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        return JSON;
    }

    [OperationContract]
    public string getBusinessUnit(int CompanyId)
    {
        var List = context.TS_Setup_BusinessUnit.Where(x => x.IsActive == true && (x.CompanyId == CompanyId || CompanyId == 0)).OrderBy(x => x.BusinessUnitName).Select(s => new
        {
            CompanyId = s.CompanyId,
            Value = s.BusinessUnitName,
            Id = s.BusinessUnitId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getDepartment(int BusinessUnitId)
    {
        var List = context.Setup_Department.Where(x => x.IsActive == true && (x.BusinessUnitId == BusinessUnitId || BusinessUnitId == 0)).OrderBy(x => x.DepartmentName).Select(s => new
        {
            CompanyId = s.CompanyId,
            BUId = s.BusinessUnitId,
            Value = s.DepartmentName,
            Id = s.DepartmentId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getList()
    {
        //DAL.Sybrid_DatabaseEntities context = new DAL.Sybrid_DatabaseEntities();

        var List = context.Setup_Company.Where(x => x.IsActive == true).OrderBy(x => x.CompanyName).Select(s => new
        {
            Value = s.CompanyName,
            Id = s.CompanyId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getYear(int CompanyId)
    {
        var List = context.HCM_Setup_Year.Where(x => x.IsActive == true && x.CompanyId == CompanyId)
            .AsEnumerable()
            .Select(s => new
            {
                Id = s.YearId,
                Value = s.YearId != null ? Convert.ToDateTime(s.YearFrom).Year.ToString() + " - " + Convert.ToDateTime(s.YearTo).Year.ToString() : "",
            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getCategory(int CompanyId)
    {
        var List = context.Setup_Category.Where(x => x.IsActive == true && x.CompanyId == CompanyId)
            .OrderBy(x => x.CategoryName).Select(s => new
            {
                Value = s.CategoryName,
                Id = s.CategoryId
            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getLocation(int CompanyId)
    {
        var List = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId == objBase.UserKey && (a.Setup_Location.CompanyId == CompanyId || CompanyId == 0))
          .Select(a => new
          {
              Id = a.Setup_Location.LocationId,
              Value = a.Setup_Location.LocationName,

          })
          .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getGender()
    {
        var List = context.HRMS_Setup_Gender.Where(a => a.IsActive == true)
          .Select(a => new
          {
              Id = a.GenderId,
              Value = a.GenderTitle,

          })
          .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getReligion()
    {
        var List = context.HRMS_Setup_Religion.Where(a => a.IsActive == true)
          .Select(a => new
          {
              Id = a.ReligionId,
              Value = a.ReligionTitle,
          })
          .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getMaritalStatus()
    {
        var List = context.HRMS_Setup_MartialStatus.Where(a => a.IsActive == true)
          .Select(a => new
          {
              Id = a.MartialStatusId,
              Value = a.MartialStatusTitle,
          })
          .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getCostCenter(int CompanyId)
    {
        var List = context.Setup_CostCenter.Where(a => a.IsActive == true && (a.CompanyId == CompanyId || CompanyId == 0))
            .Select(a => new
            {
                Id = a.CostCenterId,
                Value = a.CostCenterName,

            })
            .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getDesignation(int CompanyId)
    {
        var JSON = "";
        if (CompanyId != 0)
        {
            var List = context.Setup_Designation.Where(c => c.IsActive == true && (c.Setup_Category.CompanyId == CompanyId || CompanyId == 0))
                .Select(a => new
                {
                    CompanyId = a.Setup_Category.CompanyId,
                    Id = a.DesignationId,
                    Value = a.DesignationName,

                })
                .OrderBy(c => c.Value).ToList();

            JSON = JsonConvert.SerializeObject(List);

        }
        return JSON;
    }

    [OperationContract]
    public string getDesignationByCategoryId(int CategoryId)
    {
        var List = context.Setup_Designation.Where(c => c.IsActive == true && c.CategoryId == CategoryId)
            .Select(a => new
            {
                Id = a.DesignationId,
                Value = a.DesignationName,

            })
            .OrderBy(c => c.Value).ToList();

        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getEmployee(int GroupId, int CompanyId, int LocationId, int BUId, int CostCenterId, int DesignationId, int DepartmentId,
        string FirstName, string LastName, string EmployeeCode, int CategoryId)
    {

        FirstName = FirstName == null ? string.Empty : FirstName;
        LastName = LastName == null ? string.Empty : LastName;
        EmployeeCode = EmployeeCode == null ? string.Empty : EmployeeCode;
        int InterestSetting = (int)Constant.HCMSetupMaster.InterestSetting;
        int? IntNull = null;


        try
        {
            var List = context.Setup_Employee.Where(c => c.IsActive == true && c.CompanyId == CompanyId
                && (c.DesignationId == DesignationId || DesignationId == 0)
                && (c.DepartmentId == DepartmentId || DepartmentId == 0)
                //&& (c.FirstName.Contains(FirstName) || FirstName == string.Empty)
                //&& (c.LastName.Contains(LastName) || LastName == string.Empty)

                && ((c.FirstName.Contains(FirstName) || FirstName == string.Empty)
                || (c.MiddleName.Contains(FirstName) || FirstName == string.Empty)
                || (c.LastName.Contains(FirstName) || FirstName == string.Empty))
                && (c.EmployeeCode == EmployeeCode || EmployeeCode == string.Empty)

                && (c.LocationId == LocationId || LocationId == 0)
                && (c.Setup_Department.BusinessUnitId == BUId || BUId == 0)
                && (c.CostCenterId == CostCenterId || CostCenterId == 0)
                && (c.Setup_Designation.CategoryId == CategoryId || CategoryId == 0))
                .Select(c => new
                {
                    EmployeeId = c.EmployeeId,
                    CompanyId = c.CompanyId,
                    EmployeeCode = c.EmployeeCode,
                    FirstName = (c.MiddleName != null || c.MiddleName != string.Empty) ? c.FirstName + " " + c.MiddleName : c.FirstName,
                    LastName = c.LastName,
                    CNIC = c.CNIC,
                    Designation = c.Setup_Designation.DesignationName,
                    DesignationId = c.DesignationId,
                    Department = c.Setup_Department.DepartmentName,
                    DepartmentId = c.DepartmentId,
                    DateOfBirth = c.DateOfBirth,
                    JoiningDate = c.JoiningDate,
                    ResignedDate = c.ResignedDate,
                    c.IsFinalSettlement,
                    PersonalEmail = c.PersonalEmailAddress,
                    OfficalEmail = c.OfficeEmailAddress,
                    Gender = c.HRMS_Setup_Gender.GenderTitle,
                    GenderId = c.GenderId,
                    EmployeeType = c.Setup_EmployeeType.TypeName,
                    IsActive = c.IsActive,
                    Company = c.Setup_Location.Setup_Company.CompanyName,
                    Location = c.Setup_Location.LocationName,
                    LocationId = c.LocationId == null ? 0 : c.LocationId,
                    CostCenterId = c.CostCenterId,
                    MaritalStatusId = c.MatrialStatusId,
                    BuisnessUnitId = c.Setup_Department.BusinessUnitId == null ? 0 : c.Setup_Department.BusinessUnitId,
                    ReligionId = c.ReligionId,
                    EmployeeImage = c.PictureName,
                    Extension = c.Extension,
                    PhoneNumber = c.Phone,
                    MobileNumber = c.OfficialMobileNumber,
                    IsAllowInterest = c.IsAllowInterest,


                    OpeningBalance = c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderBy(b => b.ProvidentFundId).FirstOrDefault().TotalBalance == null ? 0 :
                    c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderBy(b => b.ProvidentFundId).FirstOrDefault().TotalBalance,

                    Salary = c.HCM_EmployeeSalary.FirstOrDefault(x => x.IsActive == true && x.IsIncrement == false).GrossSalary,

                    InterestStandard = c.Setup_Company.HCM_Company_Settings.FirstOrDefault(x => x.IsActive == true && x.SetupID == InterestSetting).Value,

                    Balance = c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance == null ? 0 :
                    c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance,

                    EmployeeIdSettlement = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().EmployeeId == null ? IntNull :
                    c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().EmployeeId,

                    LoanBalance = context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                    .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance) == null ? 0 : context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                    .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance),

                    TotalArrearAmount = c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount) == null ? 0 :
                    c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount),

                    IsSettled = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().IsSettled == null ? 0 :
                    c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().IsSettled == false ? 1 : 2,

                    //SESSILimit = context.HCM_EmployeeSESSI_Details.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == c.EmployeeId) == null ? 0 :
                    //context.HCM_EmployeeSESSI_Details.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == c.EmployeeId).SESSILimit,

                    SettlementId = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().SettlementId == null ? 0 :
                    c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().SettlementId,

                    LastInterestIncome = c.HCM_InterestIncome.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).OrderByDescending(b => b.InterestIncomeId).FirstOrDefault().InterestIncome == null ? 0 :
                    c.HCM_InterestIncome.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).OrderByDescending(b => b.InterestIncomeId).FirstOrDefault().InterestIncome,
                }).ToList();
            var JSON = JsonConvert.SerializeObject(List);
            return JSON;

        }
        catch (Exception ex)
        {

            var a = ex.Message;
            var JSON = JsonConvert.SerializeObject(a);
            return JSON;
        }


    }

    //New Work After Last Check In

    [OperationContract]
    public string getWithdrawHistory(int EmployeeId)
    {
        var List = context.HCM_PFWithdraw.Where(c => c.IsActive == true && c.EmployeeId == EmployeeId)
                  .Select(a => new
                  {
                      EmployeeCode = a.Setup_Employee.EmployeeCode,
                      FirstName = a.Setup_Employee.FirstName,
                      LastName = a.Setup_Employee.LastName,
                      WithdrawDate = a.WithdrawDate,
                      WithdrawAmount = a.WithdrawAmount,
                      FundWithdrawId = a.FundWithdrawID,
                  })
                  .OrderBy(c => c.WithdrawDate).ToList();

        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string insertWithdraw(int EmployeeId, double EmployeeWithdraw, double CompanyWithdraw, double InterestIncomeWithdraw, string WithdrawDate)
    {
        string returnvar = "0";
        try
        {
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_ProvidentFund_Withdrawal", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@EmployeeWithdraw", SqlDbType.Float).Value = EmployeeWithdraw;
            da.SelectCommand.Parameters.Add("@CompanyWithdraw", SqlDbType.Float).Value = CompanyWithdraw;
            da.SelectCommand.Parameters.Add("@InterestIncomeWithdraw", SqlDbType.Float).Value = InterestIncomeWithdraw;
            da.SelectCommand.Parameters.Add("@WithdrawDate", SqlDbType.Date).Value = Convert.ToDateTime(WithdrawDate).Date;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                returnvar = dt.Rows[0]["Row_Count"].ToString();
            }
            return returnvar;
        }
        catch (Exception e)
        {
            returnvar = "0";
            return returnvar;
        }
    }

    [OperationContract]
    public ResponseHelper ReversePF_Fund(int PF_WithDrawId)
    {
        ResponseHelper _ObjResponseHleper = new ResponseHelper();
        try
        {
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SP_HCM_Reverse_Provident_Fund", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@PFWithDrawFundId", SqlDbType.Int).Value = PF_WithDrawId;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                _ObjResponseHleper.ResponseMessageType = (Constant.ResponseType)dt.Rows[0]["MsgType"];
                _ObjResponseHleper.ResponseMessage = dt.Rows[0]["Msg"].ToString();
            }
            return _ObjResponseHleper;
        }
        catch (Exception e)
        {
            _ObjResponseHleper.ResponseMessageType = Constant.ResponseType.ERROR;
            _ObjResponseHleper.ResponseMessage = e.Message;

            return _ObjResponseHleper;
        }
    }

    [OperationContract]
    public string DeleteWithdraw(int FundWithdrawId)
    {


        var JSON = JsonConvert.SerializeObject("1");
        return JSON;
    }

    //Get From Setup Detail 

    [OperationContract]
    public string getFromSetupDetail(int CompanyId, int ParentId, int MasterId)
    {

        try
        {
            var lst = context.HCM_Setup_Detail.Where(x => x.IsActive == true && x.IsDisplay == true && x.SetupMasterID == MasterId && (x.ParentId == ParentId || ParentId == 0)
                /*&& (x.CompanyID == CompanyId || CompanyId == 0)*/)
                .OrderBy(x => x.ColumnValue)
                .Select(x => new
                {
                    Id = x.SetupDetailID,
                    Value = x.ColumnValue,
                    ParentId = x.ParentId

                }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string insertVehicleMasterInformation(string JSon)
    {
        ResponseHelper _ObjResponseHlper = new ResponseHelper();
        List<DAL.HCM_Vehicle_Master> ResponseDetails = (List<DAL.HCM_Vehicle_Master>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_Vehicle_Master>));
        int? ParentId = null;

        try
        {
            HCM_Vehicle_Master objRes = ResponseDetails.FirstOrDefault();
            int EmployeeId = objRes.EmployeeId;
            ParentId = objRes.ParentId;
            if (ParentId > 0)
            {
                var obj = context.HCM_Vehicle_Master.FirstOrDefault(x => x.IsActive == true && x.VehicleMasterId == objRes.ParentId);

                if (obj != null)
                {
                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(obj.VehicleMasterId), "HCM_Vehicle_Master", 2);
                    #endregion

                    obj.EmployeeId = objRes.EmployeeId;
                    obj.VehicleInformationId = objRes.VehicleInformationId;
                    obj.PurchaseDate = objRes.PurchaseDate;
                    obj.AlowanceDate = objRes.AlowanceDate;
                    obj.ChasisNumber = objRes.ChasisNumber;
                    obj.EngineNumber = objRes.EngineNumber;
                    obj.RegistrationNumber = objRes.RegistrationNumber;
                    obj.IsOwnerShipDeduction = objRes.IsOwnerShipDeduction;
                    obj.IsUpgraded = objRes.IsUpgraded;
                    obj.UpgradedAmount = objRes.UpgradedAmount;
                    obj.PurchaseAmount = objRes.PurchaseAmount;
                    obj.WrittenDownValue = objRes.WrittenDownValue;
                    obj.ChequeNumber = objRes.ChequeNumber;
                    obj.InstallmentAmount = objRes.InstallmentAmount;
                    obj.Balance = objRes.Balance;
                    obj.ModifiedBy = objBase.UserKey;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ParentId = null;
                    obj.IsActive = true;
                    obj.IsVehiclePayment = false;
                    obj.IsCompleted = false;
                    obj.IsHold = false;
                    obj.CarSettlementDate = objRes.CarSettlementDate;
                    obj.CurrentMonthInstallment = objRes.CurrentMonthInstallment;
                    obj.UpgradedPurchaseAmount = objRes.UpgradedPurchaseAmount;
                    context.SaveChanges();

                    _ObjResponseHlper.ResponseMessage = "Employee Assign Vehicle Information Updated.";
                    _ObjResponseHlper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                }
                else
                {
                    _ObjResponseHlper.ResponseMessage = "Vehicle Master Data Not Found For Update Employee Assign Vehicle Information.";
                    _ObjResponseHlper.ResponseMessageType = Constant.ResponseType.WARNING;

                }
            }
            else
            {
                var lstExistingVehicle = context.HCM_Vehicle_Master.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId && a.CarSettlementDate == null).ToList();
                if (lstExistingVehicle != null && lstExistingVehicle.Count > 0)
                {
                    lstExistingVehicle.ForEach(a => { a.CarSettlementDate = objRes.AlowanceDate; a.ModifiedBy = objBase.UserKey; a.ModifiedDate = DateTime.Now; a.IsCompleted = true; });
                    context.SaveChanges();
                }
                var lstCarAllowanceMap = context.HCM_EmployeeAllowanceMapping.Where(a => a.IsActive == true && a.EmployeeID == EmployeeId
                    && a.HCM_Setup_Allowance.SpecialTypeId == (int)Constant.HCMSetupDetail.CarAllowance).ToList();
                if (lstCarAllowanceMap != null && lstCarAllowanceMap.Count > 0)
                {
                    objRes.IsCarAllowance = true;
                    lstCarAllowanceMap.ForEach(a => { a.IsActive = false; a.ModifiedBy = objBase.UserKey; a.ModifiedDate = DateTime.Now; });
                }

                objRes.CreatedDate = DateTime.Now;
                objRes.CreatedBy = objBase.UserKey;
                objRes.IsActive = true;

                //if (objRes.AlowanceDate != null)
                {
                    objRes.IsVehiclePayment = false;
                }
                //else
                {
                    objRes.IsHold = false;
                }

                context.HCM_Vehicle_Master.Add(objRes);
                context.SaveChanges();


                var _CarAllownces = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.CompanyId == 2021 && (a.SpecialTypeId == (int)Constant.HCMSetupDetail.FuelAmount || a.SpecialTypeId == (int)Constant.HCMSetupDetail.RepairMaintenance))
                   .ToList();

                if (_CarAllownces != null)
                {
                    for (int i = 0; i < _CarAllownces.Count; i++)
                    {
                        int EmpId = objRes.EmployeeId;
                        int AllowanceId = _CarAllownces[i].AllowanceID;

                        bool AllowanceExist = context.HCM_EmployeeAllowanceMapping.Any(a => a.IsActive == true && a.AllowanceID == AllowanceId && a.EmployeeID == EmpId);
                        //.Count() > 0 ? true : false;

                        //bool AllowanceExist = context.HCM_EmployeeAllowanceMapping.Where(a => a.IsActive == true && a.AllowanceID == AllowanceId && a.EmployeeID == EmpId)
                        //    .Count() > 0 ? true : false;

                        if (!AllowanceExist)
                        {
                            HCM_EmployeeAllowanceMapping empMap = new HCM_EmployeeAllowanceMapping();

                            empMap.AllowanceID = AllowanceId;
                            empMap.EmployeeID = EmpId;
                            empMap.IsSpecialAllowance = false;
                            empMap.CreatedBy = objBase.UserKey;
                            empMap.CreatedDate = DateTime.Now;
                            empMap.IsActive = true;

                            context.HCM_EmployeeAllowanceMapping.Add(empMap);
                            context.SaveChanges();
                        }
                    }
                }

                _ObjResponseHlper.ResponseMessage = "Employee Assign Vehicle Information Inserted.";
                _ObjResponseHlper.ResponseMessageType = Constant.ResponseType.SUCCESS;
            }
            var JSON = JsonConvert.SerializeObject(_ObjResponseHlper);
            return JSON;
        }
        catch (Exception e)
        {
            _ObjResponseHlper.ResponseMessage = e.Message;
            _ObjResponseHlper.ResponseMessageType = Constant.ResponseType.ERROR;
            var JSON = JsonConvert.SerializeObject(_ObjResponseHlper);
            return JSON;
        }

    }

    [OperationContract]
    public string ChangeVehicleInstallmentAmount(int vehicleMasterId, int changedInstallmentAmount, string currentMonthDeductionTillDate)
    {

        ResponseHelper _ObjResponse = new ResponseHelper();
        try
        {

            if (vehicleMasterId > 0)
            {
                var obj = context.HCM_Vehicle_Master.FirstOrDefault(x => x.IsActive == true && x.VehicleMasterId == vehicleMasterId);

                if (obj != null)
                {
                    obj.CurrentMonthInstallment = changedInstallmentAmount;
                    obj.CurrentMonthDeductionTillDate = Convert.ToDateTime(currentMonthDeductionTillDate);
                    obj.ModifiedBy = objBase.UserKey;
                    obj.ModifiedDate = DateTime.Now;
                    context.SaveChanges();

                    _ObjResponse.ResponseMessage = "Vehicle Master Changed Installment Till Date : " + currentMonthDeductionTillDate + "";
                    _ObjResponse.ResponseMessageType = Constant.ResponseType.SUCCESS;
                    _ObjResponse.ResponseData = vehicleMasterId;
                }
            }
            else
            {
                _ObjResponse.ResponseMessage = "Vehicle Master ID is Null Or Zero";
                _ObjResponse.ResponseMessageType = Constant.ResponseType.WARNING;
                _ObjResponse.ResponseData = vehicleMasterId;
            }

            var JSON = JsonConvert.SerializeObject(_ObjResponse);
            return JSON;
        }
        catch (Exception e)
        {
            _ObjResponse.ResponseMessage = e.Message;
            _ObjResponse.ResponseMessageType = Constant.ResponseType.ERROR;
            _ObjResponse.ResponseData = 0;

            var JSON = JsonConvert.SerializeObject(_ObjResponse);
            return JSON;
        }

    }

    [OperationContract]
    public string saveArrearHistory(string JSon)
    {
        List<DAL.HCM_ArrearHistory> ResponseDetails = (List<DAL.HCM_ArrearHistory>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_ArrearHistory>));

        using (TransactionScope scope = new TransactionScope())
        {
            foreach (DAL.HCM_ArrearHistory obj in ResponseDetails)
            {
                obj.ArrearDate = DateTime.Now;
                obj.SalaryMasterId = null;
                obj.IsActive = true;
                obj.CreatedBy = objBase.UserKey;
                obj.CreatedDate = DateTime.Now;

                context.HCM_ArrearHistory.Add(obj);
                context.SaveChanges();
            }

            scope.Complete();
        }
        return "1";
    }

    [OperationContract]
    public string getArrearHistory(int EmployeeId)
    {
        try
        {
            var lst = context.HCM_ArrearHistory.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId)
               .OrderByDescending(x => x.CreatedDate)
               .Select(x => new
               {
                   ID = x.ArrearId,
                   FirstName = x.Setup_Employee.FirstName,
                   LastName = x.Setup_Employee.LastName,
                   IsDispersed = x.IsDispersed,
                   ArrearAmount = x.ArrearAmount,
                   ArrearDate = x.ArrearDate,
                   DispersedDate = x.DispersedDate
               }).ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string removeArrear(int ArrearId)
    {
        try
        {
            var obj = context.HCM_ArrearHistory.FirstOrDefault(x => x.IsActive == true && x.ArrearId == ArrearId);
            obj.IsActive = false;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getArrearListNonDispersed(int CompanyId)
    {
        try
        {
            var lst = context.HCM_ArrearHistory.Where(x => x.IsActive == true && x.IsDispersed == false && x.Setup_Employee.CompanyId == CompanyId)
                .Select(x => new
                {
                    ArrearTypeId = x.ArrearTypeId,
                    ArrearName = x.HCM_Setup_Detail.ColumnValue
                }).Distinct().ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string payrollTransactions(int CompanyId, string PayrolDate, bool IsFullPayroll, bool IncRelease, string ListIncIds)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            //SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeAllowances", con);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Payroll_Execute", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@DateOfPayroll", SqlDbType.DateTime).Value = DateTime.Parse(PayrolDate);
            //da.SelectCommand.Parameters.Add("@IsFullSalary", SqlDbType.Bit).Value = IsFullPayroll;
            //da.SelectCommand.Parameters.Add("@ReleaseInc", SqlDbType.Bit).Value = IncRelease;
            //da.SelectCommand.Parameters.Add("@IncrementIds", SqlDbType.VarChar).Value = ListIncIds;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = objBase.UserIP;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string payrollBonusRelease(int CompanyId, int BonusId, bool IsRelease)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);

            SqlDataAdapter da = new SqlDataAdapter("HCM_ExecuteBonus_Separate", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@BonusId", SqlDbType.Int).Value = BonusId;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            da.SelectCommand.Parameters.Add("@IsRelease", SqlDbType.Bit).Value = IsRelease;

            da.Fill(dt);

            if (dt != null)
            {
                if (dt.Columns.Contains("MSG"))
                {
                    if (dt.Rows[0]["MSG"].ToString() == "NO BONUS ON GIVEN DATE")
                    {
                        return "0";
                    }
                }
            }

            var JSON = JsonConvert.SerializeObject(dt);

            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getCompanySettings(int SetupId, int CompanyId)
    {
        try
        {
            var lst = context.HCM_Company_Settings.Where(x => x.IsActive == true && x.SetupID == SetupId && x.CompanyID == CompanyId)
                .Select(x => new
                {
                    ID = x.CompanySettingsID,
                    Value = x.Value
                }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getEmployeeSalaryAllowances(int EmployeeId, int SalaryMasterId)
    {
        try
        {
            var lst = context.HCM_Payroll_Detail.Where(x => x.IsActive == true && x.HCM_Payroll_Master.PayrollLogId == SalaryMasterId && x.HCM_Payroll_Master.EmployeeId == EmployeeId)
                .Select(x => new
                {
                    ID = x.AllowanceId,
                    AllowanceName = x.HCM_Setup_Allowance.AllowanceName,
                    AllowanceAmount = x.AllowanceAmount
                }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveUpdatedAllowances(string JSon)
    {
        try
        {
            List<DAL.HCM_Payroll_Detail> ResponseDetails = (List<DAL.HCM_Payroll_Detail>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_Payroll_Detail>));
            List<DAL.HCM_Additional_Allowances> ResponseDetailAddAllowance = (List<DAL.HCM_Additional_Allowances>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_Additional_Allowances>));
            int EmployeeID = Convert.ToInt32(ResponseDetailAddAllowance[0].EmployeeId);
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (DAL.HCM_Payroll_Detail obj in ResponseDetails)
                {
                    var lst = context.HCM_Payroll_Detail.FirstOrDefault(x => x.IsActive == true && x.HCM_Payroll_Master.PayrollLogId == obj.PayrollMasterId && x.HCM_Payroll_Master.EmployeeId == EmployeeID && x.AllowanceId == obj.AllowanceId);
                    if (lst != null)
                    {
                        lst.IsActive = false;
                        lst.ModifiedBy = objBase.UserKey;
                        lst.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                    }

                    HCM_Payroll_Detail objUpdate = new HCM_Payroll_Detail();
                    objUpdate.IsActive = true;
                    objUpdate.CreatedDate = DateTime.Now;
                    objUpdate.CreatedBy = objBase.UserKey;
                    objUpdate.AllowanceId = obj.AllowanceId;
                    objUpdate.AllowanceAmount = obj.AllowanceAmount;
                    objUpdate.PayrollMasterId = context.HCM_Payroll_Master.Where(x => x.PayrollLogId == obj.PayrollMasterId && x.EmployeeId == EmployeeID).FirstOrDefault().PayrollMasterId;
                    context.HCM_Payroll_Detail.Add(objUpdate);

                    context.SaveChanges();
                }

                foreach (DAL.HCM_Additional_Allowances obj in ResponseDetailAddAllowance)
                {
                    var lst = context.HCM_Additional_Allowances.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == obj.EmployeeId && x.Month == obj.Month && x.AllowanceId == obj.AllowanceId);
                    if (lst != null)
                    {
                        lst.IsActive = false;
                        lst.ModifiedBy = objBase.UserKey;
                        lst.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                    }

                    HCM_Additional_Allowances objUpdate = new HCM_Additional_Allowances();
                    objUpdate.IsActive = true;
                    objUpdate.CreatedDate = DateTime.Now;
                    objUpdate.CreatedBy = objBase.UserKey;
                    objUpdate.EmployeeId = obj.EmployeeId;
                    objUpdate.AllowanceId = obj.AllowanceId;
                    objUpdate.Amount = obj.Amount;
                    objUpdate.Month = obj.Month;
                    context.HCM_Additional_Allowances.Add(objUpdate);

                    context.SaveChanges();
                }

                scope.Complete();
            }
            return "1";
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    [OperationContract]
    public string executePayroll(string ListOfIds, int salaryId, string HtmlPayroll, string monthofpayroll)
    {

        string dt = DateTime.Parse(monthofpayroll).ToString("yyyyMM");
        //int PayrollLogId = context.HCM_Payroll_Log.AsEnumerable()
        //                        .FirstOrDefault(x => x.IsActive == true && x.PayrollDate.ToString("yyyyMM") == dt).PayrollLogId;

        int PayrollLogId = context.HCM_Payroll_Log.AsEnumerable()
                        .LastOrDefault(x => x.IsActive == true && x.PayrollDate.ToString("yyyyMM") == dt).PayrollLogId;

        bool? IsBonus = context.HCM_Payroll_Log.FirstOrDefault(a => a.IsActive == true && a.PayrollLogId == PayrollLogId).IsBonusRelease;
        IsBonus = IsBonus == null ? false : IsBonus;

        var objD = new HCM_PayrollDisplay();
        objD.PayrollLogId = PayrollLogId;
        objD.PayrollHtml = HtmlPayroll;
        objD.CreatedDate = DateTime.Now;
        objD.CreatedBy = objBase.UserKey;
        objD.IsActive = true;
        context.HCM_PayrollDisplay.Add(objD);

        List<int> idLst = ListOfIds.Split(',').Select(int.Parse).ToList();

        var _obj = context.HCM_Payroll_Master.FirstOrDefault(x => x.IsActive == true && x.PayrollMasterId == salaryId);
        var lst = context.HCM_Payroll_Master.Where(x => x.IsActive == true && x.PayrollLogId == _obj.PayrollLogId).ToList();

        foreach (DAL.HCM_Payroll_Master obj in lst)
        {
            if (idLst.Contains(obj.PayrollMasterId))
            {
                obj.IsDispersed = false;
                HCM_ArrearHistory objArrear = new HCM_ArrearHistory();
                objArrear.SalaryMasterId = obj.PayrollMasterId;
                objArrear.ArrearAmount = obj.TotalSalary;
                objArrear.ArrearDate = DateTime.Now;
                objArrear.ArrearTypeId = (int)Constant.HCM_Arrear.Salary;
                objArrear.EmployeeId = obj.EmployeeId;
                objArrear.IsDispersed = false;
                objArrear.CreatedDate = DateTime.Now;
                objArrear.CreatedBy = objBase.UserKey;
                objArrear.IsActive = true;
                objArrear.DispersedDate = DateTime.Now;
                context.HCM_ArrearHistory.Add(objArrear);

            }
            else
            {
                obj.IsDispersed = true;
            }
        }

        var Payroll = context.HCM_Payroll_Log.FirstOrDefault(x => x.IsActive == true && x.PayrollLogId == _obj.PayrollLogId);
        Payroll.IsLocked = true;
        context.SaveChanges();

        if (Convert.ToBoolean(IsBonus))
        {
            int? CompanyId = context.HCM_Payroll_Log.FirstOrDefault(a => a.IsActive == true && a.PayrollLogId == PayrollLogId).CompanyId;
            DataTable DT = getBonusReleaseEmployeeAllowanceId(Convert.ToInt32(CompanyId), PayrollLogId);

            if (DT != null)
            {
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        int EmpId = Convert.ToInt32(Convert.ToString(DT.Rows[i]["EmployeeId"]));
                        int AllowanceId = Convert.ToInt32(Convert.ToString(DT.Rows[i]["AllowanceID"]));

                        var LstUpdate = context.HCM_EmployeeAllowanceMapping.Where(a => a.IsActive == true && a.EmployeeID == EmpId && a.AllowanceID == AllowanceId).ToList();
                        LstUpdate.ForEach(a => { a.IsActive = false; a.ModifiedBy = objBase.UserKey; a.ModifiedDate = DateTime.Now; });
                        context.SaveChanges();
                    }
                }
            }
        }

        return "1";
    }

    [OperationContract]
    public bool getLockStatus(string dateofpayroll, int CompanyId)
    {
        string dt = DateTime.Parse(dateofpayroll).ToString("yyyyMM");
        //var _obj = context.HCM_Payroll_Log.AsEnumerable()
        //    //.FirstOrDefault(x => x.IsActive == true && x.PayrollDate.ToString("yyyyMM") == dt && x.IsBonusRelease == false);
        //  .Where(x => x.IsActive == true && x.CompanyId == CompanyId && x.PayrollDate.ToString("yyyyMM") == dt && x.IsBonusRelease == false).ToList();

        var obj = context.HCM_Payroll_Log.AsEnumerable()
            .FirstOrDefault(x => x.IsActive == true && x.CompanyId == CompanyId && x.PayrollDate.ToString("yyyyMM") == dt && x.IsBonusRelease == false);
        //.Where(x => x.IsActive == true && x.PayrollDate.ToString("yyyyMM") == dt && x.IsBonusRelease == false);
        if (obj == null)
            return false;
        else
            return obj.IsLocked;
    }

    [OperationContract]
    public bool getLockStatusBonus(string dateofpayroll, int CompanyId)
    {
        string dt = DateTime.Parse(dateofpayroll).ToString("yyyyMM");
        var obj = context.HCM_Payroll_Log.AsEnumerable()
            .FirstOrDefault(x => x.IsActive == true && x.CompanyId == CompanyId && x.PayrollDate.ToString("yyyyMM") == dt && x.IsBonusRelease == true);
        if (obj == null)
            return false;
        else
            return obj.IsLocked;
    }

    [OperationContract]
    public string getDesignationFuelMapping(int GroupId, int CompanyId, int CategoryId, int DesignationId, string DesignationName)
    {
        try
        {
            var lst = context.Setup_Designation.Where(x => x.IsActive == true)
                .Where(x => x.Setup_Category.Setup_Company.Setup_Group.GroupId == GroupId)
                .Where(x => x.Setup_Category.Setup_Company.CompanyId == CompanyId)
                .Where(x => x.CategoryId == CategoryId || CategoryId == 0)
                .Where(x => x.DesignationId == DesignationId || DesignationId == 0)
                //.Where(x => x.DesignationName == DesignationName || DesignationName == "")
                .Where(x => x.DesignationName.Contains(DesignationName) || DesignationName == "")
                .Select(x => new
                {
                    CompanyId = x.Setup_Category.CompanyId,
                    DesignationId = x.DesignationId,
                    CategoryName = x.Setup_Category.CategoryName,
                    CompanyName = x.Setup_Category.Setup_Company.CompanyName,
                    DesignationName = x.DesignationName,

                    FuelInLitres = context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId) == null ? 0 :
                   context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId).FuelInLitres,

                    First = context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId) == null ? 0 :
                  context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId).RM_FirstYear,

                    Second = context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId) == null ? 0 :
                    context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId).RM_SecondYear,

                    Third = context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId) == null ? 0 :
                 context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId).RM_ThirdYear,

                    Fourth = context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId) == null ? 0 :
                    context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId).RM_ForthYear,

                    Fifth = context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId) == null ? 0 :
                  context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId).RM_FifthYear,

                    IsOnActual = context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId) == null ? false :
                    context.HCM_Setup_RM.FirstOrDefault(y => y.IsActive == true && y.DesignationId == x.DesignationId).IsOnActual,

                }).ToList().Where(a => a.CompanyId == CompanyId).ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }
    }

    [OperationContract]
    public string getDesignationList(int GroupId, int CompanyId, int CategoryId, int DesignationId, string DesignationName)
    {
        if (GroupId == 0 || CompanyId == 0)
            return "";

        DesignationName = DesignationName == null ? string.Empty : DesignationName;
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_GetDesignationWiseVehicle", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@DesignationId", SqlDbType.Int).Value = DesignationId;
            da.SelectCommand.Parameters.Add("@DesignationName", SqlDbType.VarChar).Value = DesignationName;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }


    }

    [OperationContract]
    public string getCategoryWiseVehicleList(int GroupId, int CompanyId, int CategoryId, string CategoryName)
    {
        if (GroupId == 0 || CompanyId == 0)
            return "";

        CategoryName = CategoryName == null ? string.Empty : CategoryName;
        try
        {
            Cls_VehicleInformation Obj_VehicleInformation = new Cls_VehicleInformation();
            DataTable dt = Obj_VehicleInformation.GetCategoryWiseVehicleList(GroupId, CompanyId, CategoryId, CategoryName).ResponseDataTable;

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }


    }

    [OperationContract]
    public string getDesignationVehicleMapping(int DesignationId, bool Upgrade)
    {
        try
        {
            var lst = context.HCM_VehicleDesignationMapping.Where(x => x.IsActive == true && x.IsUpgradeVehicle == Upgrade && x.DesignationId == DesignationId)
                .Select(x => new
                {
                    x.VehicleDesignatiionMappingId,
                    x.VehicleId,
                    x.HCM_Setup_Detail.ColumnValue
                }).ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }

    }

    [OperationContract]
    public string getVehicleList(int EmployeeId, int? isUpgrade)
    {
        try
        {
            //var objEmp = context.Setup_Employee.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().DesignationId;
            //var lst = context.HCM_VehicleDesignationMapping.Where(x => x.IsActive == true && x.IsUpgradeVehicle == isUpgrade && x.DesignationId == objEmp.Value && x.VehicleInformationId != null)
            //    .Select(x => new
            //    {
            //        Value = x.HCM_VehicleInformation.HCM_Setup_Detail1.ColumnValue + " " + x.HCM_VehicleInformation.HCM_Setup_Detail.ColumnValue + " " + x.HCM_VehicleInformation.HCM_Setup_Detail4.ColumnValue,
            //        Id = x.VehicleInformationId,
            //        //Value = x.HCM_Setup_Detail.HCM_Setup_Detail2.ColumnValue + " " + x.HCM_Setup_Detail.ColumnValue + " " + context.HCM_VehicleInformation.FirstOrDefault(y => y.IsActive == true && y.VehicleId == x.HCM_Setup_Detail.SetupDetailID).HCM_Setup_Detail1.ColumnValue,
            //        //Id = context.HCM_VehicleInformation.FirstOrDefault(y => y.IsActive == true && y.VehicleId == x.HCM_Setup_Detail.SetupDetailID).VehicleInformationId,
            //        //PurchaseAmount = context.HCM_VehicleInformation.FirstOrDefault(z => z.VehicleId == x.VehicleId).PurchaseAmount,
            //        //BookValue = context.HCM_VehicleInformation.FirstOrDefault(z => z.VehicleId == x.VehicleId).WrittenDownValue
            //    }).ToList();

            Cls_VehicleInformation ObjCls_VehicleInfo = new Cls_VehicleInformation();
            var objEmp = context.Setup_Employee.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault(x => x.Setup_Designation.Setup_Category.CategoryId == x.Setup_Designation.CategoryId).Setup_Designation.CategoryId;
            DataTable dt = ObjCls_VehicleInfo.GetVehicleDesignationMapping(objEmp.Value, isUpgrade == null ? -1 : Convert.ToInt32(isUpgrade)).ResponseDataTable;

            var lst = dt.AsEnumerable()
    .Select(row => new
    {
        Value = row.Field<string>("Vehicle"),
        Id = row.Field<Int32>("VehicleInfoId")
        // Other properties....      
    })
     .ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }
    }

    [OperationContract]
    public string getUpgradeDifference(int EmployeeId)
    {
        try
        {
            var objEmp = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId);

            var objRegular = context.HCM_VehicleDesignationMapping.FirstOrDefault(x => x.IsActive == true && x.IsUpgradeVehicle == false && x.DesignationId == objEmp.DesignationId);
            var objUpgrade = context.HCM_VehicleDesignationMapping.FirstOrDefault(x => x.IsActive == true && x.IsUpgradeVehicle == true && x.DesignationId == objEmp.DesignationId);


            //var regular = context.HCM_VehicleInformation.FirstOrDefault(x => x.IsActive == true && x.VehicleId == objRegular.VehicleId).PurchaseAmount;
            //var upgd = context.HCM_VehicleInformation.FirstOrDefault(x => x.IsActive == true && x.VehicleId == objUpgrade.VehicleId).PurchaseAmount;

            var regular = 0;
            var upgd = 0;

            var JSON = JsonConvert.SerializeObject((upgd - regular));
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }

    }

    [OperationContract]
    public string saveUpdatedDesignationVehiclesMapping(string JSon)
    {
        List<DAL.HCM_VehicleDesignationMapping> ResponseDetails = (List<DAL.HCM_VehicleDesignationMapping>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_VehicleDesignationMapping>));
        int DesignationId = 0;
        List<int> lstVehicles = new List<int>();

        using (TransactionScope scope = new TransactionScope())
        {
            foreach (DAL.HCM_VehicleDesignationMapping obj in ResponseDetails)
            {
                int VariantId = 0;
                int VehicleNameId = 0;
                int ManuId = 0;
                int VehicleTypeId = 0;

                DesignationId = obj.DesignationId;
                lstVehicles.Add(obj.VehicleId);

                if (obj.VehicleId != 0)
                {
                    var lst = context.HCM_VehicleDesignationMapping.FirstOrDefault(x => x.IsActive == true && x.DesignationId == obj.DesignationId && x.VehicleId == obj.VehicleId);
                    if (lst == null)
                    {
                        obj.IsActive = true;
                        obj.CreatedBy = objBase.UserKey;
                        obj.CreatedDate = DateTime.Now;
                        context.HCM_VehicleDesignationMapping.Add(obj);
                        context.SaveChanges();
                    }
                }
            }

            scope.Complete();
        }

        var lstUpdate = context.HCM_VehicleDesignationMapping.Where(x => x.IsActive == true && x.DesignationId == DesignationId && !lstVehicles.Contains(x.VehicleId)).ToList();

        foreach (var obj in lstUpdate)
        {
            obj.IsActive = false;
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = objBase.UserKey;
        }

        context.SaveChanges();

        return "1";
    }

    [OperationContract]
    public string SaveVehicleInformation(int VariantId, int VehicleNameId, int ManufacturerId, int VehicleTypeId, int FuelTypeId, int ModelYear, int HP, int? VehicleInfoId)
    {
        if (VehicleInfoId != null)
        {
            var _obj = context.HCM_VehicleInformation.FirstOrDefault(x => x.IsActive == true && x.VehicleInformationId == VehicleInfoId);

            //_obj.PurchaseAmount = PurchaseAmount;
            //_obj.WrittenDownValue = BookValue;

            _obj.VariantId = VariantId;
            _obj.VehicleNameId = VehicleNameId;
            _obj.ManufacturerId = ManufacturerId;
            _obj.VehicleTypeId = VehicleTypeId;
            _obj.FuelTypeId = FuelTypeId;

            _obj.HoursePower = HP;
            _obj.ModelYear = ModelYear;
            _obj.IsActive = true;
            _obj.CreatedBy = objBase.UserKey;
            _obj.CreatedDate = DateTime.Now;

            context.SaveChanges();

            return "1";
        }
        else
        {
            HCM_VehicleInformation objInfo = new HCM_VehicleInformation();

            //objInfo.VehicleId = SetupDetailId;
            //objInfo.PurchaseAmount = PurchaseAmount;
            //objInfo.WrittenDownValue = BookValue;

            objInfo.VariantId = VariantId;
            objInfo.VehicleNameId = VehicleNameId;
            objInfo.ManufacturerId = ManufacturerId;
            objInfo.VehicleTypeId = VehicleTypeId;
            objInfo.FuelTypeId = FuelTypeId;

            objInfo.HoursePower = HP;
            objInfo.ModelYear = ModelYear;
            objInfo.IsActive = true;
            objInfo.CreatedBy = objBase.UserKey;
            objInfo.CreatedDate = DateTime.Now;
            //objInfo.CompanyId = CompanyId;

            context.HCM_VehicleInformation.Add(objInfo);
            context.SaveChanges();

            return "1";
        }

    }

    [OperationContract]
    public string GetVehicleInformationListing(int vehicletypeid = 0, int munufacturerid = 0, int vehicleid = 0, int vehiclevariantid = 0)
    {

        var lst = context.HCM_VehicleInformation.Where(x => x.IsActive == true && (vehicletypeid == 0 ? true : x.VehicleTypeId == vehicletypeid)
        && (munufacturerid == 0 ? true : x.ManufacturerId == munufacturerid)
         && (vehicleid == 0 ? true : x.VehicleNameId == vehicleid)
          && (vehiclevariantid == 0 ? true : x.VariantId == vehiclevariantid)
        )
             .Select(x => new
             {
                 //VehicleInformationId = x.VehicleInformationId,

                 //GroupId = x.HCM_Setup_Detail.HCM_Setup_Detail2.Setup_Company.GroupId,
                 //CompanyId = x.HCM_Setup_Detail.CompanyID,

                 //CategoryId = x.HCM_Setup_Detail.HCM_Setup_Detail2.HCM_Setup_Detail2.ParentId,
                 //Category = x.HCM_Setup_Detail.HCM_Setup_Detail2.HCM_Setup_Detail2.HCM_Setup_Detail2.ColumnValue,

                 //ManufacturerId = x.HCM_Setup_Detail.HCM_Setup_Detail2.ParentId,
                 //Manufacturer = x.HCM_Setup_Detail.HCM_Setup_Detail2.HCM_Setup_Detail2.ColumnValue,

                 //VehicleId = x.HCM_Setup_Detail.ParentId,
                 //VehicleName = x.HCM_Setup_Detail.HCM_Setup_Detail2.ColumnValue,

                 //VariantName = x.HCM_Setup_Detail.ColumnValue,

                 //FuelType = x.HCM_Setup_Detail1.ColumnValue,
                 //FuelTypeId = x.FuelTypeId,

                 //ModelYear = x.ModelYear,
                 //HorsePower = x.HoursePower,
                 ////PurchaseAmount = x.PurchaseAmount,
                 ////BookValue = x.WrittenDownValue

                 VehicleInformationId = x.VehicleInformationId,

                 VehicleTypeId = x.VehicleTypeId,
                 VehicleType = context.HCM_Setup_Detail.Where(a => a.SetupDetailID == x.VehicleTypeId).FirstOrDefault().ColumnValue,

                 ManufacturerId = x.ManufacturerId,
                 Manufacturer = context.HCM_Setup_Detail.Where(a => a.SetupDetailID == x.ManufacturerId).FirstOrDefault().ColumnValue,

                 VehicleNameId = x.VehicleNameId,
                 VehicleName = context.HCM_Setup_Detail.Where(a => a.SetupDetailID == x.VehicleNameId).FirstOrDefault().ColumnValue,

                 VariantId = x.VariantId,
                 Variant = context.HCM_Setup_Detail.Where(a => a.SetupDetailID == x.VariantId).FirstOrDefault().ColumnValue,

                 FuelType = x.HCM_Setup_Detail1.ColumnValue,
                 FuelTypeId = x.FuelTypeId,

                 ModelYear = x.ModelYear,
                 HorsePower = x.HoursePower,


             }).ToList();

        var JSON = JsonConvert.SerializeObject(lst);
        return JSON;
    }

    [OperationContract]
    public string GetVehicleHistory(int EmployeeId)
    {
        DateTime? DateNull = null;
        var lst = context.HCM_Vehicle_Master.Where(x => x.EmployeeId == EmployeeId)
            //.AsEnumerable()
            .Select(x => new
            {
                VehicleName = x.HCM_VehicleInformation.HCM_Setup_Detail.ColumnValue,
                x.ChasisNumber,
                x.EngineNumber,
                x.RegistrationNumber,
                x.PurchaseDate,
                AlowanceDate = x.AlowanceDate,
                IsOwnerShipDeduction = x.IsOwnerShipDeduction == true ? 1 : 0,
                x.ChequeNumber,
                x.UpgradedAmount,
                IsHold = x.IsHold == true ? 1 : 0,
                IsVehiclePayment = x.IsVehiclePayment == true ? 1 : 0,
                x.PurchaseAmount,
                PurchaseAmountUpgraded = x.UpgradedPurchaseAmount == null ? 0 : x.UpgradedPurchaseAmount,
                BookValue = x.WrittenDownValue,
                x.VehicleMasterId,
                x.IsCompleted,
                IsLock = x.HCM_Vehicle_Detail.Where(a => a.VehicleMasterId == x.VehicleMasterId && a.HCM_Payroll_Log.IsLocked == true).Count(),
                //InstallmentAmount = (x.CurrentMonthInstallment == null || x.CurrentMonthInstallment == 0) ? x.InstallmentAmount : x.CurrentMonthInstallment,
                InstallmentAmount = x.InstallmentAmount,
                CurrentMonthInstallment = (x.CurrentMonthInstallment == null || x.CurrentMonthInstallment == 0) ? 0 : x.CurrentMonthInstallment,
                IsUpgraded = x.IsUpgraded == true ? 2 : 1,
                VehicleId = x.VehicleInformationId,
                CurrentMonthDeductionTillDate = x.CurrentMonthDeductionTillDate,
                IsCarAllowanceExist = context.HCM_EmployeeAllowanceMapping.Where(a => a.IsActive == true && a.EmployeeID == EmployeeId && a.HCM_Setup_Allowance.SpecialTypeId == (int)Constant.HCMSetupDetail.CarAllowance).Count() == 0 ? 0 : 1,
                Balance = x.Balance,
                RecoveredInstallment = context.HCM_Vehicle_Detail.Where(a => a.VehicleMasterId == x.VehicleMasterId && a.IsActive == true).Sum(b => b.InstallmentAmount) == null ? 0 : context.HCM_Vehicle_Detail.Where(a => a.VehicleMasterId == x.VehicleMasterId && a.IsActive == true).Sum(b => b.InstallmentAmount),x.CarSettlementDate,IsActive = x.IsActive,x.Comments}).ToList();

        var JSON = JsonConvert.SerializeObject(lst);
        return JSON;
    }

    [OperationContract]
    public string GetVehicleTransactionalHistory(int EmployeeId)
    {
        var lst = context.HCM_Vehicle_Detail.Where(x => x.IsActive == true && x.HCM_Vehicle_Master.EmployeeId == EmployeeId && x.HCM_Payroll_Log.IsActive == true)
            .Select(x => new
            {
                VehicleName = x.HCM_Vehicle_Master.HCM_VehicleInformation.HCM_Setup_Detail.ColumnValue,
                x.Balance,
                x.InstallmentAmount,
                x.CreatedDate,
                IsLock = x.HCM_Payroll_Log.IsLocked == false ? 0 : 1,

            }).OrderByDescending(x => x.CreatedDate).ToList();

        var JSON = JsonConvert.SerializeObject(lst);
        return JSON;
    }

    [OperationContract]
    public string GetMappedAllowancesByEmployeeId(int EmployeeId)
    {
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeAllowancesCrud", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = ConnectionTimeout;

        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = null;
        da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = null;
        da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
        da.SelectCommand.Parameters.Add("@OperationId", SqlDbType.Int).Value = 2;
        da.SelectCommand.Parameters.Add("@Measure", SqlDbType.Decimal).Value = null;
        da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = null;
        da.Fill(dt);
        var JSON = JsonConvert.SerializeObject(dt);
        return JSON;
    }

    [OperationContract]
    public ResponseHelper UpdateAllowanceById(int CompanyId, int EmployeeId, int EmployeeAllowanceMappingID, float updMeasure)
    {
        ResponseHelper ObjResponse = new ResponseHelper();
        try
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeAllowancesCrud", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = EmployeeAllowanceMappingID;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@OperationId", SqlDbType.Int).Value = 4;
            da.SelectCommand.Parameters.Add("@Measure", SqlDbType.Decimal).Value = updMeasure;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            da.Fill(dt);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "0")
            {
                ObjResponse.ResponseData = null;
                ObjResponse.ResponseMessageType = Constant.ResponseType.SUCCESS;
                ObjResponse.ResponseMessage = "Allowence Updated Successfully.";
            }
            else
            {
                ObjResponse.ResponseData = null;
                ObjResponse.ResponseMessageType = Constant.ResponseType.SUCCESS;
                ObjResponse.ResponseMessage = "UnSuccessfull.";
            }
        }
        catch (Exception ex)
        {
            ObjResponse.ResponseData = null;
            ObjResponse.ResponseMessageType = Constant.ResponseType.ERROR;
            ObjResponse.ResponseMessage = ex.Message;

        }
        return ObjResponse;
    }

    [OperationContract]
    public string DeleteAllowanceById(int CompanyId, int EmployeeAllowanceMappingID, int EmployeeId)
    {
        try
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeAllowancesCrud", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = EmployeeAllowanceMappingID;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@OperationId", SqlDbType.Int).Value = 5;
            da.SelectCommand.Parameters.Add("@Measure", SqlDbType.Decimal).Value = null;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            da.Fill(dt);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "0")
            { var JSON = JsonConvert.SerializeObject(1); return JSON; }
            else
            { var JSON = JsonConvert.SerializeObject(0); return JSON; }
        }
        catch (Exception ex)
        {

            return "Transaction Rollback";
        }
    }

    [OperationContract]
    public string MapAllowance(int EmployeeId, int AllowanceId, float? Measure)
    {
        int CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId).CompanyId;
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeAllowancesCrud", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = ConnectionTimeout;

        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
        da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = AllowanceId;
        da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
        da.SelectCommand.Parameters.Add("@OperationId", SqlDbType.Int).Value = 3;
        da.SelectCommand.Parameters.Add("@Measure", SqlDbType.Decimal).Value = Measure;
        da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
        da.Fill(dt);
        var JSON = JsonConvert.SerializeObject(1);
        return JSON;
    }

    [OperationContract]
    public string Forecast_MapAllowance(int EmployeeId, int AllowanceId, float? Measure, string ReleaseMonth)
    {
        try
        {
            int CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId).CompanyId;
            var obj = new HCM_EmployeeAllowanceMapping();
            obj.AllowanceID = AllowanceId;
            obj.Measure = Measure;
            obj.EmployeeID = EmployeeId;
            obj.CreatedDate = DateTime.Now;
            obj.CreatedBy = objBase.UserKey;
            obj.IsActive = true;
            context.HCM_EmployeeAllowanceMapping.Add(obj);
            context.SaveChanges();

            int YearId = context.HCM_Setup_Year.Where(x => x.IsCurrentActiveYear == true && x.IsActive == true && x.CompanyId == CompanyId).FirstOrDefault().YearId;

            #region Forecasting Stored Procedure
            ForecastSalaries(CompanyId, EmployeeId, objBase.UserKey);
            saveTaxForcast(YearId, CompanyId);
            #endregion

            var _Forecast = context.HCM_EmployeeTaxForecast.Where(x => x.YearId == YearId && x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault();

            double? TaxPercent = _Forecast.HCM_Setup_Tax_Slab.TaxPercent;
            double _TaxPercent = TaxPercent == null ? 0 : Convert.ToDouble(TaxPercent);
            double TaxAmount = (GetAllowanceMeasureForEmployeeId(AllowanceId, EmployeeId) * 12.0) * (_TaxPercent / 100.0);


            var objDeduction = new HCM_EmployeeTaxDirectDeduction();
            objDeduction.IsActive = true;
            objDeduction.EmployeeId = EmployeeId;
            objDeduction.DeductionMonth = DateTime.Parse(ReleaseMonth);
            objDeduction.CreatedDate = DateTime.Now;
            objDeduction.CreatedBy = objBase.UserKey;
            objDeduction.TaxSlabId = _Forecast.TaxSlabId;
            objDeduction.TaxAmount = TaxAmount;
            objDeduction.AllowanceID = AllowanceId;
            objDeduction.UserIP = objBase.UserIP;

            context.HCM_EmployeeTaxDirectDeduction.Add(objDeduction);
            context.SaveChanges();

            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }
    }

    [OperationContract]
    public string Get_Payroll_Lock_Count(int EmployeeId)
    {
        string Return = "0";
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_Get_Payroll_Lock_Count", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = ConnectionTimeout;
        da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
        da.Fill(dt);
        if (dt != null && dt.Rows.Count > 0)
        {
            Return = Convert.ToString(dt.Rows[0]["Payroll_Lock_Count"].ToString());
        }
        return Return;

    }

    [OperationContract]
    public string getEmployeeSalaryHistory(int EmployeeId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_GET_EMPLOYEESALARY_HISTORY_NEW", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@EMPLOYEEID", SqlDbType.Int).Value = EmployeeId;
            da.Fill(dt);

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getCompanySettingsList(int CompanyId)
    {
        try
        {
            var lst = context.HCM_Setup_Master.Where(x => x.IsActive == true && x.IsSetting == true).Select(
                x => new
                {
                    x.SetupMasterID,
                    x.IsDisplayInMenu,
                    x.SetupName,
                    x.HCM_Setup_Definitions.FirstOrDefault(y => y.IsActive == true).Definition,
                    x.HCM_Setup_Definitions.FirstOrDefault(y => y.IsActive == true).SetupClass,
                    context.HCM_Company_Settings.FirstOrDefault(y => y.IsActive == true && y.CompanyID == CompanyId && y.SetupID == x.SetupMasterID).Value

                }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveCompanySettings(string JSon)
    {
        List<DAL.HCM_Company_Settings> ResponseDetails = (List<DAL.HCM_Company_Settings>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_Company_Settings>));

        using (TransactionScope scope = new TransactionScope())
        {
            foreach (DAL.HCM_Company_Settings obj in ResponseDetails)
            {
                var objCheck = context.HCM_Company_Settings.FirstOrDefault(x => x.IsActive == true && x.CompanyID == obj.CompanyID && x.SetupID == obj.SetupID);
                if (objCheck != null)
                {
                    if (objCheck.Value != obj.Value)
                    {
                        objCheck.IsActive = false;
                        objCheck.ModifiedBy = objBase.UserKey;
                        objCheck.ModifiedDate = DateTime.Now;

                        obj.IsActive = true;
                        obj.CreatedBy = objBase.UserKey;
                        obj.CreatedDate = DateTime.Now;
                        context.HCM_Company_Settings.Add(obj);
                    }
                }
                else
                {
                    obj.IsActive = true;
                    obj.CreatedBy = objBase.UserKey;
                    obj.CreatedDate = DateTime.Now;
                    context.HCM_Company_Settings.Add(obj);
                }

            }
            scope.Complete();
        }

        context.SaveChanges();

        return "1";
    }

    [OperationContract]
    public string savePFOpening(int EmployeeId, float PFOpening)
    {
        try
        {
            var objExist = context.HCM_ProvidentFund.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId);
            var objLogEx = context.HCM_PFLog.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId);

            if (objExist != null)
            {
                objExist.IsActive = false;
                objExist.ModifiedBy = objBase.UserKey;
                objExist.ModifiedDate = DateTime.Now;
            }

            if (objLogEx == null)
            {
                var objLog = new HCM_PFLog();
                objLog.EmployeeId = EmployeeId;
                objLog.IsActive = true;
                objLog.CreatedBy = objBase.UserKey;
                objLog.CreatedDate = DateTime.Now;
                objLog.OnHold = false;
                context.HCM_PFLog.Add(objLog);
                context.SaveChanges();
            }

            var obj = new HCM_ProvidentFund();

            obj.EmployeeId = EmployeeId;
            obj.CompanyContribution = 0;
            obj.EmployeeContribution = 0;
            obj.EmployeeBalance = 0;
            obj.CompanyBalance = 0;
            obj.TotalBalance = PFOpening;
            obj.CreatedDate = DateTime.Now;
            obj.CreatedBy = objBase.UserKey;
            obj.IsActive = true;

            context.HCM_ProvidentFund.Add(obj);
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getProvidentOpening(int EmployeeId)
    {
        try
        {
            DataTable dt = dt_getProvidentOpening(EmployeeId);
            var JSON = JsonConvert.SerializeObject(dt);
            //var lst = context.HCM_Pf.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId)
            //    .Select(x => new
            //    {
            //        x.TotalBalance,
            //        x.CreatedDate,
            //    })
            //    .OrderBy(x => x.CreatedDate);
            //var JSON = JsonConvert.SerializeObject(lst);

            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    public DataTable dt_getProvidentOpening(int EmployeeId)
    {
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("Get_getProvidentOpening", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = ConnectionTimeout;
        da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
        da.Fill(dt);
        return dt;
    }

    [OperationContract]
    public string getProvidentHistory(int EmployeeId)
    {
        try
        {
            DataTable dt = dt_getProvidentOpening(EmployeeId);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getEmployeeSalary(int EmployeeId)
    {
        try
        {
            var CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId).CompanyId;
            var Standard = context.HCM_Company_Settings.FirstOrDefault(x => x.IsActive == true && x.CompanyID == CompanyId && x.SetupID == (int)Constant.HCMSetupMaster.SalaryStandard).Value;
            double? Salary = 0;

            var _Data = context.HCM_EmployeeSalary.Where(x => x.IsActive == true && x.IsIncrement == false /*&& x.IsGranted == false*/ && x.EmployeeID == EmployeeId).ToList();

            if (_Data.Count > 0)
            {
                if (Convert.ToInt32(Standard) == (int)Constant.HCM_Salary_Standards.BasicSalary)
                {
                    Salary = context.HCM_EmployeeSalary.FirstOrDefault(x => x.IsActive == true && x.IsIncrement == false && x.EmployeeID == EmployeeId).BasicSalary;
                }
                else
                {
                    Salary = context.HCM_EmployeeSalary.SingleOrDefault(x => x.IsActive == true && x.IsIncrement == false /*&& x.IsGranted == false*/ && x.EmployeeID == EmployeeId).GrossSalary;
                }
            }

            return Convert.ToString(Salary);
        }
        catch (Exception e)
        {
            return "";
        }
    }

    [OperationContract]
    public string getPayrollCount(string payrolDate, int CompanyId_)
    {
        try
        {
            string dt = DateTime.Parse(payrolDate).ToString("yyyyMM");
            var count = context.HCM_Payroll_Log
                .AsEnumerable()
                .Where(x => x.CompanyId == CompanyId_ && x.PayrollDate.ToString("yyyyMM") == dt).Count();
            return Convert.ToString(count);
        }
        catch (Exception e)
        {
            return "-1";
        }
    }

    [OperationContract]
    public string getPayrollCountBonus(string payrolDate)
    {
        try
        {
            string dt = DateTime.Parse(payrolDate).ToString("yyyyMM");
            var count = context.HCM_Payroll_Log
                .AsEnumerable()
                .Where(x => x.PayrollDate.ToString("yyyyMM") == dt).Count();
            return Convert.ToString(count);
        }
        catch (Exception e)
        {
            return "-1";
        }
    }

    [OperationContract]
    public string getSalaryStandard(int CompanyId)
    {
        try
        {
            int SalaryStandard = (int)Constant.HCMSetupMaster.SalaryStandard;
            int Value = Convert.ToInt32(context.HCM_Company_Settings.FirstOrDefault(x => x.IsActive == true && x.SetupID == SalaryStandard).Value);
            var lst = context.HCM_Company_Settings.Where(x => x.IsActive == true && x.SetupID == SalaryStandard)
                .Select(x => new
                {
                    SetupDetailId = x.Value,
                    Standard = context.HCM_Setup_Detail.FirstOrDefault(y => y.IsActive == true && y.SetupDetailID == Value).ColumnValue
                }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getSalaryChangeHistory(int EmployeeId)
    {
        try
        {
            dynamic lst = context.HCM_EmployeeSalary.Where(x => x.IsActive == true && x.EmployeeID == EmployeeId)
                .Select(x => new
                {
                    x.EmployeeSalaryID,
                    x.IsIncrement,
                    x.IsGranted,
                    x.GrossSalary,
                    x.IncrementProcessStartDate,
                    x.IncrementAppliedDate,
                    IncrementType = x.HCM_Setup_Detail.ColumnValue,
                    x.IncrementTypeId,
                    x.CreatedDate
                }).OrderByDescending(x => x.CreatedDate).ToList();


            //        dynamic lstIncrement = context.HCM_EmployeeSalary.Where(x => x.IsIncrement == true && x.EmployeeID == EmployeeId)
            //.Select(x => new
            //{
            //    x.EmployeeSalaryID,
            //    x.IsIncrement,
            //    x.IsGranted,
            //    x.GrossSalary,
            //    x.IncrementProcessStartDate,
            //    x.IncrementAppliedDate,
            //    IncrementType = x.HCM_Setup_Detail.ColumnValue,
            //    x.IncrementTypeId,
            //    x.CreatedDate
            //}).OrderByDescending(x => x.CreatedDate).ToList();


            dynamic lstIncrement = context.HCM_EmployeeSalary.Where(x => x.IncrementProcessStartDate != null && x.EmployeeID == EmployeeId)
    .Select(x => new
    {
        x.EmployeeSalaryID,
        x.IsIncrement,
        x.IsGranted,
        x.GrossSalary,
        x.IncrementProcessStartDate,
        x.IncrementAppliedDate,
        IncrementType = x.HCM_Setup_Detail.ColumnValue,
        x.IncrementTypeId,
        x.CreatedDate
    }).Distinct().OrderByDescending(x => x.IncrementProcessStartDate).ToList();



            var lstDynamic = new List<dynamic>();
            lstDynamic.Add(lst);
            lstDynamic.Add(lstIncrement);



            var JSON = JsonConvert.SerializeObject(lstDynamic);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveSalaryInformation(int EmployeeId, int SalaryStandard, float Salary, bool IsIncrement, string IncrementStartDate, int? IncrementTypeId)
    {
        try
        {
            decimal Salary_ = Math.Round(Convert.ToDecimal(Salary), 2);
            string Return = "0";
            DateTime? IncrementDate = null;
            if (IncrementStartDate != "")
                IncrementDate = DateTime.Parse(IncrementStartDate);
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Insert_Employee_SalaryInformation", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@Salary", SqlDbType.Float).Value = Salary_;
            da.SelectCommand.Parameters.Add("@IsIncrement", SqlDbType.Bit).Value = IsIncrement;
            da.SelectCommand.Parameters.Add("@IncrementProcessStartDate", SqlDbType.DateTime).Value = IncrementDate;
            da.SelectCommand.Parameters.Add("@IncrementTypeId", SqlDbType.Int).Value = IncrementTypeId;
            da.SelectCommand.Parameters.Add("@UserKey", SqlDbType.Int).Value = objBase.UserKey;
            da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.NVarChar).Value = objBase.UserIP;

            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                Return = Convert.ToString(dt.Rows[0]["Id"].ToString());
            }

            //var objAdvTax = context.HCM_EmployeeSalary.Where(x => x.IsActive == true && x.EmployeeID == EmployeeId).ToList();
            //if (objAdvTax != null && objAdvTax.Count > 0)
            //{
            //    objAdvTax.ForEach(a => { a.AdvanceTaxPercent = 0; a.ModifiedBy = objBase.UserKey; a.ModifiedDate = DateTime.Now; });
            //    context.SaveChanges();
            //}

            //var obj = context.HCM_EmployeeSalary.FirstOrDefault(x => x.IsActive == true && x.EmployeeID == EmployeeId && x.IsGranted == false && x.IsIncrement == IsIncrement && x.IncrementTypeId == IncrementTypeId);
            //if (obj != null)
            //{
            //    //if (obj.IsGranted == false && obj.IsIncrement == IsIncrement && obj.IncrementTypeId == IncrementTypeId)
            //    {
            //        obj.IsActive = false;
            //        obj.ModifiedBy = objBase.UserKey;
            //        obj.ModifiedDate = DateTime.Now;
            //        context.SaveChanges();
            //    }
            //}


            //DateTime? IncrementDate = null;
            //if (IncrementStartDate != "")
            //    IncrementDate = DateTime.Parse(IncrementStartDate);

            //var objSalary = new HCM_EmployeeSalary();
            //objSalary.EmployeeID = EmployeeId;
            //objSalary.IsGranted = false;
            //objSalary.IsActive = true;
            //objSalary.IsIncrement = IsIncrement;
            //objSalary.CreatedBy = objBase.UserKey;
            //objSalary.CreatedDate = DateTime.Now;
            //objSalary.IncrementProcessStartDate = IncrementDate;
            //objSalary.UserIP = objBase.UserIP;
            //objSalary.IncrementTypeId = IncrementTypeId;
            //objSalary.AdvanceTaxPercent = 0;
            //if (SalaryStandard == (int)Constant.HCM_Salary_Standards.BasicSalary)
            //{
            //    objSalary.BasicSalary = Salary;
            //}
            //else
            //{
            //    objSalary.BasicSalary = 0;
            //    objSalary.GrossSalary = Salary;
            //}

            //context.HCM_EmployeeSalary.Add(objSalary);
            //context.SaveChanges();

            //if (IsIncrement)
            //{
            //    var _objIncHs = context.HCM_IncreamentHistory.FirstOrDefault(a => a.IsActive == true && a.EmployeeId == EmployeeId);
            //    if (_objIncHs != null)
            //    {
            //        _objIncHs.IsActive = false;
            //        _objIncHs.ModifiedBy = objBase.UserKey;
            //        _objIncHs.ModifiedDate = DateTime.Now;
            //        context.SaveChanges();
            //    }

            //    double? PrevGross = context.HCM_SalaryHistory_Master.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId && a.HCM_Payroll_Log.IsActive == true
            //        && a.HCM_Payroll_Log.IsLocked == true).ToList().OrderByDescending(b => b.PayrollLogId).FirstOrDefault().GrossSalary == null ? 0 :
            //        context.HCM_SalaryHistory_Master.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId && a.HCM_Payroll_Log.IsActive == true
            //        && a.HCM_Payroll_Log.IsLocked == true).ToList().OrderByDescending(b => b.PayrollLogId).FirstOrDefault().GrossSalary;

            //    //double NewSalaryDiff = Convert.ToDouble(Salary) - Convert.ToDouble(obj.GrossSalary);
            //    double NewSalaryDiff = Convert.ToDouble(Salary) - Convert.ToDouble(PrevGross);
            //    double IncPercent = (NewSalaryDiff * 100) / Convert.ToDouble(Salary);

            //    HCM_IncreamentHistory objIncHs = new HCM_IncreamentHistory();
            //    objIncHs.CreatedBy = objBase.UserKey;
            //    objIncHs.CreatedDate = DateTime.Now;
            //    objIncHs.EmployeeId = EmployeeId;
            //    //objIncHs.PrevGrossSalary = Convert.ToDouble(obj.GrossSalary);
            //    objIncHs.PrevGrossSalary = Convert.ToDouble(PrevGross);
            //    objIncHs.IncreamentRate = IncPercent;
            //    objIncHs.IncreamentTypeId = Convert.ToInt32(IncrementTypeId);
            //    objIncHs.IncrementProcessStartDate = Convert.ToDateTime(IncrementStartDate);
            //    objIncHs.IsActive = true;
            //    objIncHs.IncreamentedGross = Convert.ToDouble(Salary);
            //    context.HCM_IncreamentHistory.Add(objIncHs);
            //    context.SaveChanges();
            //    try
            //    {
            //        int CompanyId = 0;
            //        var lstCompany = context.Setup_Employee.Where(a => a.EmployeeId == EmployeeId).ToList();
            //        if (lstCompany != null && lstCompany.Count > 0)
            //        {
            //            CompanyId = lstCompany[0].CompanyId;
            //        }

            //        var lstLock = context.HCM_Payroll_Log.Where(a => a.IsActive == true && a.CompanyId == CompanyId && a.IsLocked == true)
            //            //.AsEnumerable()
            //            .Select(a => new
            //            {
            //                PayrollDate = a.PayrollDate,
            //                IsLocked = a.IsLocked,
            //                PayrollLogId = a.PayrollLogId,
            //            })
            //            .OrderByDescending(a => a.PayrollLogId)
            //            .ToList();

            //        if (lstLock != null && lstLock.Count > 0)
            //        {
            //            DateTime LastLockDate = lstLock[0].PayrollDate;
            //            DateTime NewMonthDate = LastLockDate.AddMonths(1);

            //            if (Convert.ToDateTime(IncrementDate) <= NewMonthDate)
            //            {
            //                IncrementSalaryForecaster(EmployeeId);
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        //var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            //        //return JSON;
            //    }
            //}

            return Return;
        }
        catch (Exception e)
        {
            return "0";
        }
    }

    public bool IncrementSalaryForecaster(int EmployeeId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeIncrementedSalaryForecaster", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;

            da.Fill(dt);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    [OperationContract]
    public string deleteVehicleById(int VehicleMasterId, string comments)
    {
        try
        {
            var objTractionsCheck = context.HCM_Vehicle_Detail.FirstOrDefault(x => x.VehicleMasterId == VehicleMasterId);

            if (objTractionsCheck != null)
            {
                return "0";
            }
            else
            {
                var obj = context.HCM_Vehicle_Master.FirstOrDefault(x => x.IsActive == true && x.VehicleMasterId == VehicleMasterId);

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(VehicleMasterId), "HCM_Vehicle_Master", 3);
                #endregion

                obj.Comments = comments;
                obj.IsActive = false;
                obj.ModifiedBy = objBase.UserKey;
                obj.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                return "1";
            }
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getLoanAttributeDetail(int LoanMasterId)
    {
        try
        {
            var lst = context.HCM_LoanAttributeDetail.Where(a => a.IsActive == true && a.LoanMasterId == LoanMasterId)
                .Select(a => new
                {
                    AttributeId = a.AttributeId,
                    AttributeValue = a.AttributeValue,
                })
                .ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getAttributeDependent(int AttributeId)
    {
        try
        {
            var lst = context.HCM_Setup_Atribute.Where(a => a.IsActive == true && a.AtributeId == AttributeId)
                .Select(a => new
                {
                    ParentId = a.ParentId,
                })
                .ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string DynamicDdlFill(int AttributeId, string Value)
    {
        try
        {
            Value = Value == null ? "" : Value;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Dynamic_DDL_Fill", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@ControlId", SqlDbType.Int).Value = AttributeId;
            da.SelectCommand.Parameters.Add("@Value", SqlDbType.NVarChar).Value = Value;
            da.Fill(dt);

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getAttributes(int LoanTypeId, int CompanyId)
    {
        try
        {
            string LoanTypeIdStr = Convert.ToString(LoanTypeId);
            int SetupMasterId = context.HCM_Setup_Master.Where(a => a.SetupMasterID == (int)Constant.HCMSetupMaster.Dynamic).FirstOrDefault().SetupMasterID;
            int SetupDetailIdParent = context.HCM_Setup_Detail.Where(a => a.SetupMasterID == SetupMasterId && a.ParentId == null && a.CompanyID == 2007).ToList().FirstOrDefault().SetupDetailID;
            int SetupDetailId = context.HCM_Setup_Detail.Where(a => a.ParentId == SetupDetailIdParent && a.ColumnValue == LoanTypeIdStr && a.CompanyID == 2007).ToList().FirstOrDefault().SetupDetailID;

            var lst = context.HCM_Atribute_Mapping
                .Where(a => a.HCM_Setup_Atribute.IsActive == true /*&& a.HCM_Setup_Atribute.CompanyId == CompanyId*/)
                .Where(x => x.IsActive == true && x.AllowanceDeductionId == SetupDetailId /*&& x.HCM_Setup_Atribute.CompanyId == CompanyId*/)

                .Select(x => new
                {
                    AttributeId = x.AtributeId,
                    AttributeName = x.HCM_Setup_Atribute.AtributeName,
                    CssClass = x.HCM_Setup_Atribute.CssClass,
                    ControlType = x.HCM_Setup_Atribute.ControlType,
                    TableName = x.HCM_Setup_Atribute.TableName,
                    DisplayMember = x.HCM_Setup_Atribute.DisplayMember,
                    ValueMember = x.HCM_Setup_Atribute.ValueMember,
                    SortOrder = x.HCM_Setup_Atribute.SortOrder,
                    FillByRefrenceClass = x.HCM_Setup_Atribute.FillByRefernceClass,
                    ParentClass = x.HCM_Setup_Atribute.ParentClass,
                    WhereColumnName = x.HCM_Setup_Atribute.WhereColumnName,

                }).OrderBy(a => a.SortOrder).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveLoanMaster(int LoanTypeId, int EmployeeId, double LoanAmount, double InstallmentAmount, string SanctionDate, string LoanGivenDate, string SettlementDate, int? ParentId,
        /*string JSon,*/ double InterestAmount, int InterestId_, string Reason, string Comments)
    {
        try
        {
            //List<DAL.HCM_LoanAttributeDetail> ResponseDetails = (List<DAL.HCM_LoanAttributeDetail>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_LoanAttributeDetail>));
            int? InterestId = InterestId_;
            if (InterestId_ == 0)
                InterestId = null;

            using (TransactionScope scope = new TransactionScope())
            {
                if (ParentId > 0)
                {
                    var objLoanMaster = context.HCM_Loan_Master.FirstOrDefault(a => a.IsActive == true && a.LoanMasterId == ParentId);
                    HCM_Loan_Master obj = new HCM_Loan_Master();
                    obj.LoanTypeId = objLoanMaster.LoanTypeId;
                    obj.EmployeeId = objLoanMaster.EmployeeId;
                    obj.LoanAmount = objLoanMaster.LoanAmount;
                    obj.LoanBalance = objLoanMaster.LoanBalance;
                    obj.InstallmentAmount = objLoanMaster.InstallmentAmount;
                    obj.CurrentMonthInstallment = objLoanMaster.CurrentMonthInstallment;
                    obj.SanctionDate = Convert.ToDateTime(objLoanMaster.SanctionDate);
                    obj.LoanGivenDate = Convert.ToDateTime(objLoanMaster.LoanGivenDate);
                    obj.SettlementDate = Convert.ToDateTime(objLoanMaster.SettlementDate);
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = objBase.UserKey;
                    obj.IsActive = false;
                    obj.ParentId = ParentId;
                    obj.InterestAmount = objLoanMaster.InterestAmount;
                    obj.InterestId = objLoanMaster.InterestId;
                    obj.Reason = objLoanMaster.Reason;
                    obj.Comments = objLoanMaster.Comments;
                    context.HCM_Loan_Master.Add(obj);
                    context.SaveChanges();
                    objLoanMaster.LoanTypeId = LoanTypeId;
                    objLoanMaster.EmployeeId = EmployeeId;
                    objLoanMaster.LoanAmount = LoanAmount;
                    objLoanMaster.LoanBalance = LoanAmount;
                    objLoanMaster.InstallmentAmount = InstallmentAmount;
                    objLoanMaster.CurrentMonthInstallment = InstallmentAmount;
                    objLoanMaster.SanctionDate = Convert.ToDateTime(SanctionDate);
                    objLoanMaster.LoanGivenDate = Convert.ToDateTime(LoanGivenDate);
                    objLoanMaster.SettlementDate = Convert.ToDateTime(SettlementDate);
                    objLoanMaster.ModifiedDate = DateTime.Now;
                    objLoanMaster.ModifiedBy = objBase.UserKey;
                    objLoanMaster.IsActive = true;
                    objLoanMaster.InterestAmount = InterestAmount;
                    objLoanMaster.InterestId = InterestId;
                    objLoanMaster.LoanAmountWithInterest = LoanAmount + InterestAmount;
                    objLoanMaster.Reason = Reason;
                    objLoanMaster.Comments = Comments;
                    context.SaveChanges();
                }
                else
                {
                    HCM_Loan_Master obj = new HCM_Loan_Master();
                    obj.LoanTypeId = LoanTypeId;
                    obj.EmployeeId = EmployeeId;
                    obj.LoanAmount = LoanAmount;
                    obj.LoanBalance = LoanAmount;
                    obj.InstallmentAmount = InstallmentAmount;
                    obj.CurrentMonthInstallment = InstallmentAmount;
                    obj.SanctionDate = Convert.ToDateTime(SanctionDate);
                    obj.LoanGivenDate = Convert.ToDateTime(LoanGivenDate);
                    obj.SettlementDate = Convert.ToDateTime(SettlementDate);
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = objBase.UserKey;
                    obj.IsActive = true;
                    obj.InterestAmount = InterestAmount;
                    obj.InterestId = InterestId;
                    obj.LoanAmountWithInterest = LoanAmount + InterestAmount;
                    obj.Reason = Reason;
                    obj.Comments = Comments;
                    context.HCM_Loan_Master.Add(obj);
                    context.SaveChanges();
                }

                scope.Complete();
            }


            return "1";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [OperationContract]
    public string Change_LoanInstallment_Amount(int LoanMasterId, double InstallmentAmount, string CurrentMonthInstallmentTillDate)
    {
        try
        {
            var objLoanMaster = context.HCM_Loan_Master.FirstOrDefault(a => a.IsActive == true && a.LoanMasterId == LoanMasterId);
            if (objLoanMaster != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(LoanMasterId), "HCM_Loan_Master", 2);
                #endregion

                objLoanMaster.CurrentMonthInstallment = InstallmentAmount;
                objLoanMaster.CurrentMonthInstallmentTillDate = Convert.ToDateTime(CurrentMonthInstallmentTillDate);
                objLoanMaster.ModifiedDate = DateTime.Now;
                objLoanMaster.ModifiedBy = objBase.UserKey;
                objLoanMaster.IsActive = true;
                context.SaveChanges();
                if (objLoanMaster.CurrentMonthInstallment == InstallmentAmount && objLoanMaster.CurrentMonthInstallmentTillDate == Convert.ToDateTime(CurrentMonthInstallmentTillDate))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }

        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [OperationContract]
    public string getLoanMaster(int EmployeeId)
    {
        //string result = "";
        try
        {

            var lst = context.HCM_Loan_Master.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId)
                .OrderByDescending(x => x.LoanMasterId)
                .Select(x => new
                {
                    LoanMasterId = x.LoanMasterId,
                    LoanTypeId = x.LoanTypeId,
                    LoanType = x.HCM_Setup_Detail.ColumnValue,
                    EmployeeId = x.EmployeeId,
                    LoanAmount = x.LoanAmount,
                    InstallmentAmount = x.InstallmentAmount,
                    SanctionDate = x.SanctionDate,
                    LoanGivenDate = x.LoanGivenDate,
                    SettlementDate = x.SettlementDate,
                    IsHold = x.IsHold,
                    Balance = x.LoanBalance, //x.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Payroll_Log.IsLocked == true).OrderByDescending(b => b.LoadDetailId).Take(1).FirstOrDefault().Balance == null ? -1 : x.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Payroll_Log.IsLocked == true).OrderByDescending(b => b.LoadDetailId).Take(1).FirstOrDefault().Balance,
                    Reason = x.Reason,
                    Comments = x.Comments,
                    Has_IsLocked_Transection = x.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Payroll_Log.IsLocked == true).Count(),
                })
                .ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

    }

    [OperationContract]
    public string deleteLoanById(int LoanMasterId)
    {
        try
        {
            var objTractionsCheck = context.HCM_Loan_Detail.FirstOrDefault(x => x.IsActive == true && x.LoanMasterId == LoanMasterId);

            if (objTractionsCheck != null)
            {
                return "0";
            }
            else
            {
                var obj = context.HCM_Loan_Master.FirstOrDefault(x => x.IsActive == true && x.LoanMasterId == LoanMasterId);
                obj.IsActive = false;
                obj.ModifiedBy = objBase.UserKey;
                obj.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                return "1";
            }
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string payrollArrearRelease(int CompanyId, string PayrolDate, string ArrearTypeIds)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Salary_ArrearRelease", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@DateOfPayroll", SqlDbType.DateTime).Value = DateTime.Parse(PayrolDate);
            da.SelectCommand.Parameters.Add("@ArrearTypeIds", SqlDbType.VarChar).Value = ArrearTypeIds;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveToSetupDetail(int _CompanyId, int MasterId, string Value, int? SetupDetailId, int? ParentId)
    {
        try
        {
            int? CompanyId = _CompanyId == 0 ? IntNull : _CompanyId;

            if (SetupDetailId == null)
            {
                HCM_Setup_Detail obj = new HCM_Setup_Detail();
                obj.ColumnValue = Value;
                obj.IsActive = true;
                obj.IsDisplay = true;
                obj.SetupMasterID = MasterId;
                obj.ParentId = ParentId;
                obj.CreatedBy = objBase.UserKey;
                obj.CompanyID = CompanyId;
                context.HCM_Setup_Detail.Add(obj);
                context.SaveChanges();
            }
            else
            {
                var objRes = context.HCM_Setup_Detail.FirstOrDefault(x => x.IsActive == true && x.SetupDetailID == SetupDetailId);
                objRes.ColumnValue = Value;
                objRes.ParentId = ParentId;
                objRes.ModifiedBy = objBase.UserKey;
                objRes.CompanyID = CompanyId;
                objRes.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }

            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string deleteFromSetupDetail(int SetupDetailId)
    {
        try
        {
            var objRes = context.HCM_Setup_Detail.FirstOrDefault(x => x.IsActive == true && x.SetupDetailID == SetupDetailId);

            objRes.ModifiedBy = objBase.UserKey;
            objRes.ModifiedDate = DateTime.Now;
            objRes.IsActive = false;

            context.SaveChanges();

            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getVehicleInfoListing(int SetupDetailId)
    {
        try
        {
            var lst = context.HCM_Setup_Detail.Where(x => x.IsActive == true && x.ParentId != SetupDetailId && x.SetupMasterID == (int)Constant.HCMSetupMaster.Manufacturer)
                 .Select(x => new
                 {
                     VehicleName = x.ColumnValue,
                     VehicleId = x.SetupDetailID,

                     ManufacturerName = x.HCM_Setup_Detail2.ColumnValue,
                     //ManufacturerId = x.HCM_Setup_Detail2.SetupDetailID,

                     //CategoryId = x.HCM_Setup_Detail2.HCM_Setup_Detail2.SetupDetailID
                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string multimanage_SavePFOpening(string JSon)
    {
        //var lst = context.Setup_Employee.Where(a => a.IsActive == true && a.CompanyId == 2009)
        //                .Select(a => new { EmployeeId = a.EmployeeId })
        //                .ToList();

        //for (int i = 0; i < lst.Count; i++)
        //{
        //    int EmployeeId = lst[i].EmployeeId;
        //    ForecastSalaries(2009, EmployeeId, objBase.UserKey);
        //}

        List<DAL.HCM_ProvidentFund> ResponseDetails = (List<DAL.HCM_ProvidentFund>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_ProvidentFund>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (DAL.HCM_ProvidentFund obj in ResponseDetails)
                {
                    var _obj = new HCM_ProvidentFund();
                    _obj.EmployeeId = obj.EmployeeId;
                    _obj.TotalBalance = obj.TotalBalance;
                    _obj.EmployeeContribution = 0;
                    _obj.EmployeeBalance = 0;
                    _obj.CompanyContribution = 0;
                    _obj.CompanyBalance = 0;
                    _obj.IsActive = true;
                    _obj.CreatedDate = DateTime.Now;
                    _obj.CreatedBy = objBase.UserKey;
                    context.HCM_ProvidentFund.Add(_obj);

                    //ForecastSalaries(int CompanyId, int EmployeeId, int UserKey)



                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string multimanage_SaveSESSILimit(string JSon)
    {
        List<DAL.HCM_EmployeeSESSI_Details> ResponseDetails = (List<DAL.HCM_EmployeeSESSI_Details>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_EmployeeSESSI_Details>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (DAL.HCM_EmployeeSESSI_Details obj in ResponseDetails)
                {
                    var objCheck = context.HCM_EmployeeSESSI_Details
                      .FirstOrDefault(x => x.IsActive == true && x.EmployeeId == obj.EmployeeId);

                    if (objCheck != null)
                    {
                        objCheck.IsActive = false;
                        objCheck.ModifiedBy = objBase.UserKey;
                        objCheck.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                    }
                    var _obj = new HCM_EmployeeSESSI_Details();
                    _obj.EmployeeId = obj.EmployeeId;
                    _obj.SESSIAmount = obj.SESSIAmount;
                    _obj.IsActive = true;
                    _obj.CreatedDate = DateTime.Now;
                    _obj.CreatedBy = objBase.UserKey;
                    context.HCM_EmployeeSESSI_Details.Add(_obj);
                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string multimanage_SaveOvertimeHours(string JSon)
    {
        List<DAL.HCM_EmployeeOvertimeDetail> ResponseDetails = (List<DAL.HCM_EmployeeOvertimeDetail>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_EmployeeOvertimeDetail>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (DAL.HCM_EmployeeOvertimeDetail obj in ResponseDetails)
                {
                    var objCheck = context.HCM_EmployeeOvertimeDetail
                      .FirstOrDefault(x => x.IsActive == true && x.EmployeeId == obj.EmployeeId && x.Month == obj.Month);

                    if (objCheck != null)
                    {
                        objCheck.IsActive = false;
                        objCheck.ModifiedBy = objBase.UserKey;
                        objCheck.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                    }
                    var _obj = new HCM_EmployeeOvertimeDetail();
                    _obj.EmployeeId = obj.EmployeeId;
                    _obj.Month = obj.Month;
                    _obj.OvertimeHours = obj.OvertimeHours;
                    _obj.IsActive = true;
                    _obj.CreatedDate = DateTime.Now;
                    _obj.CreatedBy = objBase.UserKey;
                    context.HCM_EmployeeOvertimeDetail.Add(_obj);
                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string multimanage_LockStatus(string monthofpayroll)
    {
        string dt = DateTime.Parse(monthofpayroll).ToString("yyyyMM");
        var obj = (from i in context.HCM_Payroll_Log.AsEnumerable()
                   where i.IsActive == true && i.IsArrearRelease == false && i.IsLocked == true && i.PayrollDate.ToString("yyyyMM") == dt
                   select i).FirstOrDefault();

        if (obj == null)
            return "1";
        else
            return "0";
    }

    [OperationContract]
    public string multimanage_Flexible(string monthofpayroll, bool IsAbsent)
    {
        string dt = DateTime.Parse(monthofpayroll).ToString("yyyyMM");
        var lst = context.HCM_AbsentFlexiLog.AsEnumerable()
            .Where(x => x.IsActive == true && x.AbsentFlexMonth.ToString("yyyyMM") == dt)
            .Select(x => new
            {
                EmployeeId = x.EmployeeId,
                AbsentFlexiCount = IsAbsent == true ? x.Absents : x.Flexi,
            }).ToList();
        var JSON = JsonConvert.SerializeObject(lst);
        return JSON;
    }    

    [OperationContract]
    public string multimanage_SaveFlexiAbsents(string JSon)
    {
        //List<DAL.HCM_AbsentFlexiLog> ResponseDetails = (List<DAL.HCM_AbsentFlexiLog>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_AbsentFlexiLog>));
        try
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    foreach (DAL.HCM_AbsentFlexiLog obj in ResponseDetails)
            //    {

            //        string dt = obj.AbsentFlexMonth.ToString("yyyyMM");
            //        var objCheck = context.HCM_AbsentFlexiLog.AsEnumerable().FirstOrDefault(x => x.IsActive == true && x.EmployeeId == obj.EmployeeId && x.IsAbsent == obj.IsAbsent && x.AbsentFlexMonth.ToString("yyyyMM") == dt);
            //        if (objCheck != null)
            //        {
            //            objCheck.IsActive = false;
            //            objCheck.ModifiedBy = objBase.UserKey;
            //            objCheck.ModifiedDate = DateTime.Now;
            //            context.SaveChanges();
            //        }

            //        var _obj = new HCM_AbsentFlexiLog();
            //        _obj.EmployeeId = obj.EmployeeId;
            //        _obj.AbsentFlexiCount = obj.AbsentFlexiCount;
            //        _obj.AbsentFlexMonth = obj.AbsentFlexMonth;
            //        _obj.IsAbsent = obj.IsAbsent;
            //        _obj.IsActive = true;
            //        _obj.CreatedDate = DateTime.Now;
            //        _obj.CreatedBy = objBase.UserKey;
            //        context.HCM_AbsentFlexiLog.Add(_obj);
            //    }
            //    scope.Complete();
            //}
            //context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string multimanage_SaveSalaries(string JSon)
    {
        List<DAL.HCM_EmployeeSalary> ResponseDetails = (List<DAL.HCM_EmployeeSalary>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_EmployeeSalary>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (DAL.HCM_EmployeeSalary obj in ResponseDetails)
                {
                    var objCheck = context.HCM_EmployeeSalary.FirstOrDefault(x => x.IsActive == true && x.EmployeeID == obj.EmployeeID && x.IsIncrement == false);
                    if (objCheck != null)
                    {
                        objCheck.IsActive = false;
                        objCheck.ModifiedBy = objBase.UserKey;
                        objCheck.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                    }
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = objBase.UserKey;
                    context.HCM_EmployeeSalary.Add(obj);
                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string multimanage_GetLoan()
    {
        try
        {
            var lst = context.HCM_Loan_Master
                .Where(x => x.IsActive == true && x.IsSettled == false)
                 .Select(x => new
                 {
                     LoanMasterID = x.LoanMasterId,
                     EmployeeId = x.EmployeeId,
                     LoanName = x.HCM_Setup_Detail.ColumnValue,
                     IsHold = x.IsHold,
                     Balance = x.HCM_Loan_Detail.Where(a => a.IsActive == true).OrderByDescending(b => b.LoadDetailId).Take(1).FirstOrDefault() == null
                     ? -1 : x.HCM_Loan_Detail.Where(a => a.IsActive == true).OrderByDescending(b => b.LoadDetailId).Take(1).FirstOrDefault().Balance,
                     Amount = x.LoanAmount,
                     Interest = x.InterestAmount
                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string holdProvidentFund(int PFLogId, bool Status)
    {
        try
        {
            var obj = context.HCM_PFLog.FirstOrDefault(x => x.PFLogId == PFLogId);
            obj.OnHold = Status;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string multimanage_GetPF()
    {
        try
        {
            var lst = context.HCM_PFLog
                .Where(x => x.IsActive == true)
                 .Select(x => new
                 {
                     x.PFLogId,
                     x.EmployeeId,
                     x.OnHold
                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string multimanage_GetOvertimeDetailRecords(string monthofpayroll)
    {
        try
        {
            string dt = DateTime.Parse(monthofpayroll).ToString("yyyyMM");
            var lst = context.HCM_EmployeeOvertimeDetail
                .Where(x => x.IsActive == true)
                .AsEnumerable()
                .Where(x => x.Month.ToString("yyyyMM") == dt)
                 .Select(x => new
                 {
                     x.EmployeeId,
                     x.OvertimeHours
                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }   

    [OperationContract]
    public string multimanage_GetInterestIncome(int YearOf, int CompanyId)
    {
        try
        {
            var lst = context.HCM_InterestIncome
                 .Where(x => x.IsActive == true && x.YearOf == YearOf && x.Setup_Employee.CompanyId == CompanyId)
                 .Select(x => new
                 {
                     x.EmployeeId,
                     x.InterestIncome,
                     x.PrevYearIntrestIncome,
                     x.CurrentPFBalance,
                     x.IncomeInterest_Rate,
                     x.YearId,

                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string multimanage_GrantInterestIncome(int YearOf, string EmployeeIds)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_InterestIncome_Grant", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@YearOf", SqlDbType.Int).Value = YearOf;
            da.SelectCommand.Parameters.Add("@EmployeeIds", SqlDbType.VarChar).Value = EmployeeIds;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string deleteVehicleInfo(int VehicleInfoId)
    {
        try
        {
            var objRes = context.HCM_VehicleInformation.FirstOrDefault(x => x.IsActive == true && x.VehicleInformationId == VehicleInfoId);
            objRes.ModifiedBy = objBase.UserKey;
            objRes.ModifiedDate = DateTime.Now;
            objRes.IsActive = false;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string holdLoanById(int LoanMasterId, bool Status)
    {
        try
        {
            var obj = context.HCM_Loan_Master.FirstOrDefault(x => x.LoanMasterId == LoanMasterId);

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(LoanMasterId), "HCM_Loan_Master", 2);
            #endregion

            obj.IsHold = Status;
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = objBase.UserKey;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getTax()
    {
        var lst = context.HCM_Setup_Tax.Where(a => a.IsActive == true)
                  .Select(a => new
                  {
                      Id = a.TaxID,
                      Value = a.TaxName,

                  })
          .ToList();
        var JSON = JsonConvert.SerializeObject(lst);
        return JSON;
    }

    [OperationContract]
    public string saveTaxSlab(int CompanyId, int TaxId, string TaxSlab, float SalaryRangeFrom, float SalaryRangeTo, float FixedValue, float TaxPercent, int YearId /*string YearFrom, string YearTo*/, int TaxSlabId)
    {
        try
        {
            if (TaxSlabId == 0)
            {
                HCM_Setup_Tax_Slab obj = new HCM_Setup_Tax_Slab();

                obj.CompanyId = CompanyId;
                obj.TaxId = TaxId;
                obj.TaxSlab = TaxSlab;
                obj.SalaryRangeStart = SalaryRangeFrom;
                obj.SalaryRangeEnd = SalaryRangeTo;
                obj.FixedValue = FixedValue;
                obj.TaxPercent = TaxPercent;
                //obj.TaxYearFrom = DateTime.Parse(YearFrom);
                //obj.TaxYearTo = DateTime.Parse(YearTo);
                obj.YearId = YearId;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = objBase.UserKey;
                obj.IsActive = true;

                context.HCM_Setup_Tax_Slab.Add(obj);
                context.SaveChanges();

                return "1";
            }
            else
            {
                var obj = context.HCM_Setup_Tax_Slab.FirstOrDefault(x => x.IsActive == true && x.TaxSlabId == TaxSlabId);

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(TaxSlabId), "HCM_Setup_Tax_Slab", 2);
                #endregion

                obj.CompanyId = CompanyId;
                obj.TaxId = TaxId;
                obj.TaxSlab = TaxSlab;
                obj.SalaryRangeStart = SalaryRangeFrom;
                obj.SalaryRangeEnd = SalaryRangeTo;
                obj.FixedValue = FixedValue;
                obj.TaxPercent = TaxPercent;
                //obj.TaxYearFrom = DateTime.Parse(YearFrom);
                //obj.TaxYearTo = DateTime.Parse(YearTo);
                obj.YearId = YearId;
                obj.ModifiedBy = objBase.UserKey;
                obj.ModifiedDate = DateTime.Now;

                context.SaveChanges();

                return "1";
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [OperationContract]
    public string getTaxSlabListing(int CompanyId)
    {
        try
        {
            var lst = context.HCM_Setup_Tax_Slab.Where(x => x.IsActive == true && x.CompanyId == CompanyId)
                .AsEnumerable()
                 .Select(x => new
                 {
                     TaxSlabId = x.TaxSlabId,
                     CompanyId = x.CompanyId,
                     TaxId = x.TaxId,

                     CompanyName = x.Setup_Company.CompanyName,
                     Tax = x.HCM_Setup_Tax.TaxName,
                     TaxSlab = x.TaxSlab,
                     SalaryRangeStart = x.SalaryRangeStart,
                     SalaryRangeEnd = x.SalaryRangeEnd,
                     FixedValue = x.FixedValue,
                     TaxPercent = x.TaxPercent,
                     YearId = x.YearId,
                     Year = x.HCM_Setup_Year.YearId != null ? Convert.ToDateTime(x.HCM_Setup_Year.YearFrom).Year.ToString() + " - " + Convert.ToDateTime(x.HCM_Setup_Year.YearTo).Year.ToString() : "",

                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string DeleteTaxSlabById(int TaxSlabId)
    {
        try
        {
            var obj = context.HCM_Setup_Tax_Slab.FirstOrDefault(x => x.IsActive == true && x.TaxSlabId == TaxSlabId);

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(TaxSlabId), "HCM_Setup_Tax_Slab", 3);
            #endregion

            obj.IsActive = false;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getInterestAmount(string DiffYear, string EmployeeId)
    {
        try
        {
            bool IsAllowInterest = context.Setup_Employee.AsEnumerable().Where(a => a.IsActive == true && a.EmployeeId == Convert.ToInt32(EmployeeId)).FirstOrDefault().IsAllowInterest;

            int DiffYea = Convert.ToInt32(DiffYear);
            int EmpId = Convert.ToInt32(EmployeeId);


            if (IsAllowInterest)
            {
                int CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmpId).CompanyId;
                var obj = context
                    .HCM_Interest
                    .Where(x => x.IsActive == true && x.HCM_Setup_Detail.SetupMasterID == (int)Constant.HCMSetupMaster.InterestYearSlabs)
                    .Select(x => x.HCM_Setup_Detail)
                    .AsEnumerable()
                    .OrderBy(x => Math.Abs(DiffYea - Convert.ToInt32(x.ColumnValue))).FirstOrDefault();

                var lst = context.HCM_Interest.Where(x => x.IsActive == true && x.CompanyId == CompanyId && x.InterestSlabYearID == obj.SetupDetailID)
                    .Select(x => new
                    {
                        InterestId = x.InterestId,
                        InterestRate = x.InterestRate
                    }).ToList();
                var JSON = JsonConvert.SerializeObject(lst);
                return JSON;

            }
            else
            {
                return "";
            }

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getInterestList(int CompanyId, int EmployeeId)
    {
        try
        {
            //bool IsAllowInterest = context.Setup_Employee.AsEnumerable().Where(a => a.IsActive == true && a.EmployeeId == EmployeeId).FirstOrDefault().IsAllowInterest;
            bool IsAllowInterest = context.Setup_Employee.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId).FirstOrDefault().IsAllowInterest;

            if (IsAllowInterest)
            {
                var lst = context
                        .HCM_Interest
                        .Where(x => x.IsActive == true && x.CompanyId == CompanyId)
                        .Select(x => new
                        {
                            x.InterestSlabYearID,
                            Slab = x.HCM_Setup_Detail.ColumnValue,
                            x.InterestId,
                            //x.InterestRate
                            InterestRate = context.HCM_Company_Settings.Where(a => a.IsActive == true && a.CompanyID == CompanyId &&
                                a.SetupID == (int)Constant.HCMSetupMaster.LoanInterestRate).FirstOrDefault().Value,
                        }).ToList();
                var JSON = JsonConvert.SerializeObject(lst);
                return JSON;
            }
            else
            {

                return "0";
            }

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getLoanAllow(string CompanyId, string EmployeeId)
    {
        try
        {
            var lstCountLoanAllow = context.HCM_Company_Settings.AsEnumerable()
                .Where(a => a.IsActive == true && a.SetupID == (int)Constant.HCMSetupMaster.NoOfLoansAllow && a.CompanyID == Convert.ToInt32(CompanyId)).ToList();

            int CurrentLoanEmployeeCount = context.HCM_Loan_Master.AsEnumerable().Where(a => a.IsActive == true && a.IsSettled == false && a.EmployeeId == Convert.ToInt32(EmployeeId)).Count();

            int? LoanAllowCount = lstCountLoanAllow[0].Value == null ? 0 : Convert.ToInt32(lstCountLoanAllow[0].Value);

            if (LoanAllowCount > CurrentLoanEmployeeCount)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string companysetting_GetInterestSlab(int CompanyId)
    {
        try
        {
            var lst = context.HCM_Interest.Where(x => x.IsActive == true && x.CompanyId == CompanyId)
                 .Select(x => new
                 {
                     InterestId = x.InterestId,
                     SlabYear = x.HCM_Setup_Detail.ColumnValue,
                     SlabYearId = x.InterestSlabYearID,
                     InterestRate = x.InterestRate
                 }).OrderBy(x => x.SlabYear).ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string companysetting_DeleteInterestSlab(int InterestId)
    {
        try
        {
            var obj = context.HCM_Interest.FirstOrDefault(x => x.InterestId == InterestId);
            obj.IsActive = false;
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = objBase.UserKey;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public bool ForecastSalaries(int CompanyId, int EmployeeId, int UserKey)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeSalaryForecaster", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserKey;
            da.Fill(dt);
            return true;

        }
        catch (Exception e)
        {
            return false;
        }
    }

    [OperationContract]
    public double GetAllowanceMeasureForEmployeeId(int AllowanceId, int EmployeeId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Get_MeasureByAllowanceId", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = AllowanceId;
            da.Fill(dt);
            return Convert.ToDouble(dt.Rows[0][0]);

        }
        catch (Exception e)
        {
            return -1;
        }
    }

    [OperationContract]
    public string increment_DeleteBySalaryId(int SalaryId)
    {
        try
        {
            var obj = context.HCM_EmployeeSalary.FirstOrDefault(x => x.EmployeeSalaryID == SalaryId);

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(SalaryId), "HCM_EmployeeSalary", 3);
            #endregion

            obj.IsActive = false;
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = objBase.UserKey;
            context.SaveChanges();


            var objDetail = context.HCM_EmployeeSalaryDetail.Where(x => x.EmployeeSalaryId == SalaryId).ToList();
            objDetail.ForEach(c =>
            {

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(SalaryId, Convert.ToString(c.EmployeeSalaryDetailId), "HCM_EmployeeSalaryDetail", 3);
                #endregion

                c.IsActive = false; c.ModifiedBy = objBase.UserKey; c.ModifiedDate = DateTime.Now;
            });
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;


        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string medical_GetReInbursment(int CompanyId, string MonthOf)
    {
        try
        {
            DateTime dtMonth = DateTime.Parse(MonthOf);
            string dt = dtMonth.ToString("yyyyMM");

            var lst = context.HCM_MedicalReinbursment
                       .AsEnumerable()
                       .Where(x => x.IsActive == true && x.CompanyId == CompanyId && x.MonthOfReInbursement.ToString("yyyyMM") == dt)
                       .Select(x => new
                       {
                           x.CompanyId,
                           x.Setup_Company.CompanyName,
                           x.EmployeeId,
                           x.Setup_Employee.FirstName,
                           x.Setup_Employee.LastName,
                           x.PayAmount,
                           x.ReinbursmentId,
                           x.MonthOfReInbursement,
                           x.EmployeeCode
                       })
                       .ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string medical_UploadFile(int CompanyId, string MonthOf, string Filename)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_MedicalReinbursement", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getTaxLaw()
    {
        var List = context.HCM_Setup_TaxLaw.Where(x => x.IsActive == true && x.YearId == 1045).Select(s => new
        {
            Value = s.TaxLaw,
            Id = s.TaxLawId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string saveTaxLawSettings(int TaxComputationId, int EmployeeId, int TaxLawId, float Amount, int YearId, string ActivityDate)
    {
        try
        {
            DateTime? dateNull = null;
            if (TaxComputationId == 0)
            {
                var lst = context.HCM_TaxComputation.FirstOrDefault(x => x.IsActive == true && x.TaxLawId == TaxLawId && x.YearId == YearId && x.EmployeeId == EmployeeId/*&& x.FromYear >= dtFromYear && x.ToYear <= dtToYear*/);

                if (lst != null)
                {
                    return "Already Exist";
                }

                HCM_TaxComputation obj = new HCM_TaxComputation();

                obj.EmployeeId = EmployeeId;
                obj.TaxLawId = TaxLawId;
                obj.Amount = Amount;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = objBase.UserKey;
                obj.IsActive = true;
                obj.YearId = YearId;
                obj.LawActivityDate = ActivityDate == null ? dateNull : DateTime.Parse(ActivityDate);
                context.HCM_TaxComputation.Add(obj);
                context.SaveChanges();
            }
            else if (TaxComputationId > 0)
            {
                var lst = context.HCM_TaxComputation.FirstOrDefault(x => x.IsActive == true && x.TaxComputationId == TaxComputationId);
                if (lst != null)
                {
                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(TaxComputationId), "HCM_TaxComputation", 2);
                    #endregion

                    lst.TaxLawId = TaxLawId;
                    lst.Amount = Amount;
                    lst.ModifiedBy = objBase.UserKey;
                    lst.ModifiedDate = DateTime.Now;
                    lst.YearId = YearId;
                    lst.LawActivityDate = ActivityDate == null ? dateNull : DateTime.Parse(ActivityDate);
                    context.SaveChanges();
                }
            }

            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string deleteTaxLaw(int TaxComputationId)
    {
        try
        {
            var lst = context.HCM_TaxComputation.FirstOrDefault(x => x.IsActive == true && x.TaxComputationId == TaxComputationId);
            if (lst != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(TaxComputationId), "HCM_TaxComputation", 3);
                #endregion

                lst.IsActive = false;
                lst.ModifiedBy = objBase.UserKey;
                lst.ModifiedDate = DateTime.Now;

                context.SaveChanges();
            }
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }


    }

    [OperationContract]
    public string getTaxLawComputation(int EmployeeId)
    {
        var List = context.HCM_TaxComputation.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId)
            .AsEnumerable()
            .Select(s => new
            {
                TaxComputationId = s.TaxComputationId,
                TaxLawId = s.TaxLawId,
                TaxLaw = s.HCM_Setup_TaxLaw.TaxLaw,
                Amount = s.Amount,
                TaxYearId = s.YearId,
                TaxYear = s.YearId != null ? Convert.ToDateTime(s.HCM_Setup_Year.YearFrom).Year.ToString() + " - " + Convert.ToDateTime(s.HCM_Setup_Year.YearTo).Year.ToString() : "",
                LawActivityDate = s.LawActivityDate

            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getTaxYear(int CompanyId)
    {
        var List = context.HCM_Setup_Year.Where(x => x.IsActive == true && x.CompanyId == CompanyId && x.IsCurrentActiveYear == true)
            .AsEnumerable()
            .Select(s => new
            {
                Id = s.YearId,
                Value = s.YearId != null ? Convert.ToDateTime(s.YearFrom).Year.ToString() + " - " + Convert.ToDateTime(s.YearTo).Year.ToString() : "",
            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getTaxYearListing(int CompanyId)
    {
        var List = context.HCM_Setup_Year.Where(x => x.IsActive == true && x.CompanyId == CompanyId)
                       .Select(s => new
                       {
                           s.Setup_Company.GroupId,
                           s.CompanyId,
                           YearId = s.YearId,
                           IsCurrentActiveYear = s.IsCurrentActiveYear,
                           YearFrom = s.YearFrom,
                           YearTo = s.YearTo,
                           HasTransactions = s.HCM_Setup_Tax_Slab.Where(x => x.IsActive == true).Count()
                       }).OrderByDescending(x => x.YearId).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getTaxableTransactions(int EmployeeId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_GetTaxableAllowanceByEmployeeId", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveTaxYear(int CompanyId, string YearFrom, string YearTo, bool isCurrentYear, int? YearId)
    {
        try
        {
            var _item = context.HCM_Setup_Year.Where(x => x.IsActive == true && x.CompanyId == CompanyId).ToList().Count;
            if (isCurrentYear)
            {
                var lst = context.HCM_Setup_Year.Where(x => x.IsActive == true && x.CompanyId == CompanyId).ToList();
                foreach (var _obj in lst)
                {
                    _obj.IsCurrentActiveYear = false;
                }
                context.SaveChanges();
            }

            if (_item == 0)
                isCurrentYear = true;

            if (YearId != null)
            {
                var objUpdate = context.HCM_Setup_Year.FirstOrDefault(x => x.YearId == YearId);
                objUpdate.YearFrom = DateTime.Parse(YearFrom);
                objUpdate.YearTo = DateTime.Parse(YearTo);
                objUpdate.IsCurrentActiveYear = isCurrentYear;
                objUpdate.ModifiedDate = DateTime.Now;
                objUpdate.ModifiedBy = objBase.UserKey;
                context.SaveChanges();
            }
            else
            {
                var obj = new HCM_Setup_Year();
                obj.CompanyId = CompanyId;
                obj.YearFrom = DateTime.Parse(YearFrom);
                obj.YearTo = DateTime.Parse(YearTo);
                obj.IsCurrentActiveYear = isCurrentYear;
                obj.IsActive = true;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = objBase.UserKey;
                context.HCM_Setup_Year.Add(obj);
                context.SaveChanges();
            }
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string reactiveSlabYear(int YearId)
    {
        try
        {
            var obj = context.HCM_Setup_Year.FirstOrDefault(x => x.YearId == YearId);
            int CompanyId = obj.CompanyId;
            var lst = context.HCM_Setup_Year.Where(x => x.IsActive == true && x.CompanyId == CompanyId).ToList();
            foreach (var _obj in lst)
            {
                _obj.IsCurrentActiveYear = false;
            }
            context.SaveChanges();

            obj.IsCurrentActiveYear = true;
            context.SaveChanges();

            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string DeleteSlabYear(int YearId)
    {
        try
        {
            var obj = context.HCM_Setup_Year.FirstOrDefault(x => x.YearId == YearId);
            obj.IsActive = false;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveFinalSettlement(int SettlementId, bool IsVehicleTakeHome, float VehiclePurchaseAmount, float VehicleUpgradedAmount, float VehicleBalanceAmount,
        float RecvDeducAmount, float TotalVehicleAmount, float PFAmount, float LoanBalanceAmount, float ArrearAmount, float BasicSalary, int LeavesRemaining,
        float LeaveEncashment, float Total, string ChequeNumber)
    {
        try
        {
            var obj = context.HCM_EmployeeSettlement.FirstOrDefault(x => x.IsActive == true && x.SettlementId == SettlementId && x.IsSettled == false);

            if (obj != null)
            {
                obj.IsVehicleTakeHome = IsVehicleTakeHome;
                obj.VehiclePurhaseAmont = VehiclePurchaseAmount;
                obj.VehicleUpgradedAmount = VehicleUpgradedAmount;
                obj.BalanceAmount = VehicleBalanceAmount;
                obj.ReceiveableDeductedAmount = RecvDeducAmount;
                obj.TotalVehicleAmount = TotalVehicleAmount;
                obj.PFAmount = PFAmount;
                obj.LoanBalanceAmount = LoanBalanceAmount;
                obj.ArrearAmount = ArrearAmount;
                obj.BasicSalary = BasicSalary;
                obj.LeavesRemaining = LeavesRemaining;
                obj.LeaveEncashment = LeaveEncashment;
                obj.TotalAmount = Total;
                obj.IsSettled = true;
                obj.ChequeNumber = ChequeNumber;

                obj.ModifiedBy = objBase.UserKey;
                obj.ModifiedDate = DateTime.Now;

                context.SaveChanges();
            }

            return "1";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [OperationContract]
    public string getTaxForcast(int CompanyId, int YearId)
    {
        try
        {
            int? IntNull = null;
            var List = context.HCM_EmployeeTaxForecast.Where(c => c.IsActive == true && c.Setup_Employee.CompanyId == CompanyId && c.YearId == YearId)
                //.AsEnumerable()
                .Select(c => new
                {
                    c.Setup_Employee.EmployeeId,
                    c.Setup_Employee.EmployeeCode,
                    c.Setup_Employee.FirstName,
                    c.Setup_Employee.MiddleName,
                    c.Setup_Employee.LastName,
                    c.Setup_Employee.Setup_Department.DepartmentName,
                    c.HCM_Setup_Tax_Slab.TaxSlab,
                    c.TaxableIncome,
                    c.TaxNotionalAdition,
                    c.TaxableReduction,
                    c.TotalTaxableIncome,
                    c.TaxPayable,
                    c.TaxCredit,
                    c.TotalTaxPayable,

                }).ToList().OrderBy(d => Convert.ToInt32(d.EmployeeCode)).ToList();

            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception ex)
        {
            var JSON = JsonConvert.SerializeObject(ex.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveTaxForcast(int YearId,/*int UserId,int UserKey,*/int CompanyId)
    {
        try
        {
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //string dbConnectionString = context.Database.Connection.ConnectionString;
            //SqlConnection con = new SqlConnection(dbConnectionString);
            //SqlDataAdapter da = new SqlDataAdapter("HCM_Tax_Forecaster", con);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.Add("@YearId_", SqlDbType.Int).Value = YearId;
            //da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            //da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = objBase.UserIP;
            //da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            //da.Fill(dt);
            //var JSON = JsonConvert.SerializeObject(dt);
            //return JSON;

            return "";
        }
        catch (Exception ex)
        {
            var JSON = JsonConvert.SerializeObject(ex.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveTaxForcastMain(int YearId,/*int UserId,int UserKey,*/int CompanyId, bool IsIncludeCurrentMonth, string advanceTaxPerc)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Tax_Forecaster", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@YearId", SqlDbType.Int).Value = YearId;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
            da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = objBase.UserIP;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@IsIncludeCurrentMonth", SqlDbType.Bit).Value = IsIncludeCurrentMonth;
            da.SelectCommand.Parameters.Add("@AdvanceTax", SqlDbType.Int).Value = Convert.ToInt32(advanceTaxPerc);

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;

            //return "";
        }
        catch (Exception ex)
        {
            var JSON = JsonConvert.SerializeObject(ex.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getFinalSettlementAmount(int EmployeeId)
    {
        try
        {
            int? IntNull = null;
            var List = context.Setup_Employee.Where(c => c.IsActive == true && c.EmployeeId == EmployeeId)
                //.AsEnumerable()
                .Select(c => new
                {
                    CompanyId = c.CompanyId,

                    PfBalance = c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance == null ? 0 :
                c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance,

                    IsSettled = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().IsSettled,

                    SettlementId = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().SettlementId,

                    EmployeeIdSettlement = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId /*&& a.IsSettled == false*/).FirstOrDefault().EmployeeId == null ? IntNull :
                    c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId /*&& a.IsSettled == false*/).FirstOrDefault().EmployeeId,

                    LoanBalance = context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                    .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance) == null ? 0 : context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                    .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance),

                    TotalArrearAmount = c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount) == null ? 0 :
                    c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount),

                    IsCarAllocated = true,

                    /*IsOwnerShipDeduction = c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().IsOwnerShipDeduction == null ? false :
                    c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().IsOwnerShipDeduction,

                    IsUpgraded = c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().IsUpgraded == null ? false :
                    c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().IsUpgraded,

                    IsCompleted = c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().IsCompleted == null ? false :
                    c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().IsCompleted,

                    UpgradedAmount = c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().UpgradedAmount == null ? 0 :
                    c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().UpgradedAmount,

                    VehicleBalance = c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().HCM_Vehicle_Detail.Where(b => b.IsActive == true).OrderByDescending(x => x.VehicleDetailId).Take(1).FirstOrDefault().Balance == null ? 0 :
                    c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().HCM_Vehicle_Detail.Where(b => b.IsActive == true).OrderByDescending(b => b.VehicleDetailId).Take(1).FirstOrDefault().Balance,

                    PurchaseAmount = c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().PurchaseAmount == null ? 0 :
                    c.HCM_Vehicle_Master.Where(x => x.IsActive == true && x.EmployeeId == EmployeeId).FirstOrDefault().PurchaseAmount,
                    */
                    AnnualLeavesRemaining = 0,
                })
                .ToList();

            if (List[0].IsSettled)
            {
                var lst = context.HCM_EmployeeSettlement.Where(c => c.IsActive == true && c.EmployeeId == EmployeeId).ToList();
                //.Select(a => new
                //{

                //});

                var JSON = JsonConvert.SerializeObject(lst);
                return JSON;
            }
            else
            {
                var JSON = JsonConvert.SerializeObject(List);
                return JSON;
            }

        }
        catch (Exception ex)
        {
            var JSON = JsonConvert.SerializeObject(ex.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getTaxInformation(int EmployeeId)
    {
        try
        {
            var lst = context.HCM_EmployeeTaxForecast.Where(x => x.EmployeeId == EmployeeId && x.IsActive == true)
                .Select(x => new
                {
                    x.TaxForecastId,
                    x.HCM_Setup_Year.YearFrom,
                    x.HCM_Setup_Year.YearTo,
                    x.TotalTaxPayable,
                    x.HCM_EmployeeTaxDetails.Where(y => y.IsActive == true).OrderByDescending(y => y.EmployeeTaxId).FirstOrDefault().Balance
                }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception ex)
        {
            var JSON = JsonConvert.SerializeObject(ex.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string GetAllowancesByEmployeeId(int EmployeeId)
    {
        var lst = context.HCM_EmployeeAllowanceMapping.Where(a => a.IsActive == true && a.EmployeeID == EmployeeId)
            .Select(a => new
            {
                Id = a.AllowanceID,
                Value = a.HCM_Setup_Allowance.AllowanceName,
                Formula = context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null ? "" :
                context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula,
                IsFormulaExist = context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null ||
                context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == "" ? false :
                true,
                Measure = a.Measure

            })
            .ToList();

        var JSON = JsonConvert.SerializeObject(lst);
        return JSON;
    }

    [OperationContract]
    public string isfinalsettlement(string EmployeeCode, int CompanyId)
    {
        int issucess = 0;
        try
        {
            var lst = context.Setup_Employee.FirstOrDefault(x => x.EmployeeCode == EmployeeCode && x.CompanyId == CompanyId);
            if (lst != null)
            {

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(lst.EmployeeId), "Setup_Employee", 2);
                #endregion

                if (lst.IsFinalSettlement == true) { lst.IsFinalSettlement = false; }
                else { lst.IsFinalSettlement = true; }
                context.SaveChanges();
                issucess = 1;
            }
            var JSON = JsonConvert.SerializeObject(issucess);
            return JSON;
        }
        catch
        {
            var JSON = JsonConvert.SerializeObject(issucess);
            return JSON;
        }
    }

    [OperationContract]
    public string saveMultipleAllowanceMapping(string JSon, bool IsPopupOpen)
    {
        try
        {
            List<DAL.HCM_EmployeeAllowanceMapping> ResponseDetails = (List<DAL.HCM_EmployeeAllowanceMapping>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_EmployeeAllowanceMapping>));

            {
                if (IsPopupOpen)
                {
                    foreach (DAL.HCM_EmployeeAllowanceMapping _obj in ResponseDetails)
                    {
                        int EmployeeId = _obj.EmployeeID;
                        int AllowanceId = _obj.AllowanceID;
                        double? Measure = _obj.Measure;

                        int CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId).CompanyId;
                        int YearId = context.HCM_Setup_Year.Where(a => a.IsActive == true && a.CompanyId == CompanyId && a.IsCurrentActiveYear == true).FirstOrDefault().YearId;

                        var lst = context.HCM_EmployeeAllowanceMapping.FirstOrDefault(x => x.IsActive == true && x.EmployeeID == EmployeeId && x.AllowanceID == AllowanceId
                            /*&& x.Measure == Measure*/);
                        if (lst != null)
                        {
                            lst.AllowanceID = AllowanceId;
                            lst.Measure = Measure;
                            lst.EmployeeID = EmployeeId;
                            lst.ModifiedDate = DateTime.Now;
                            lst.ModifiedBy = objBase.UserKey;
                            lst.IsActive = true;
                            //context.HCM_EmployeeAllowanceMapping.Add(obj);
                            context.SaveChanges();

                            ForecastSalaries(CompanyId, EmployeeId, objBase.UserKey);

                            if (YearId > 0)
                            {
                                saveTaxForcast(YearId, CompanyId);
                            }
                        }
                        else
                        {
                            var obj = new HCM_EmployeeAllowanceMapping();
                            obj.AllowanceID = AllowanceId;
                            obj.Measure = Measure;
                            obj.EmployeeID = EmployeeId;
                            obj.CreatedDate = DateTime.Now;
                            obj.CreatedBy = objBase.UserKey;
                            obj.IsActive = true;
                            context.HCM_EmployeeAllowanceMapping.Add(obj);
                            context.SaveChanges();

                            ForecastSalaries(CompanyId, EmployeeId, objBase.UserKey);

                            if (YearId > 0)
                            {
                                saveTaxForcast(YearId, CompanyId);
                            }
                        }
                    }
                }
                else
                {
                    int EmployeeId = 0;
                    int AllowanceId = 0;
                    double? Measure = null;
                    int CompanyId = 0;

                    int i = 0;
                    foreach (DAL.HCM_EmployeeAllowanceMapping _obj in ResponseDetails)
                    {
                        EmployeeId = _obj.EmployeeID;
                        AllowanceId = _obj.AllowanceID;
                        Measure = _obj.Measure;

                        if (i == 0)
                        {
                            CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId).CompanyId;

                            var lst = context.HCM_EmployeeAllowanceMapping.Where(x => x.IsActive == true && x.EmployeeID == EmployeeId).ToList();
                            if (lst != null)
                            {
                                lst.ForEach(a => { a.IsActive = false; a.ModifiedDate = DateTime.Now; a.ModifiedBy = objBase.UserKey; });

                                context.SaveChanges();
                            }
                        }

                        var obj = new HCM_EmployeeAllowanceMapping();
                        obj.AllowanceID = AllowanceId;
                        obj.Measure = Measure;
                        obj.EmployeeID = EmployeeId;
                        obj.CreatedDate = DateTime.Now;
                        obj.CreatedBy = objBase.UserKey;
                        obj.IsActive = true;
                        context.HCM_EmployeeAllowanceMapping.Add(obj);
                        context.SaveChanges();

                        ForecastSalaries(CompanyId, EmployeeId, objBase.UserKey);

                        int YearId = context.HCM_Setup_Year.Where(a => a.IsActive == true && a.CompanyId == CompanyId && a.IsCurrentActiveYear == true).FirstOrDefault().YearId;

                        if (YearId > 0)
                        {
                            saveTaxForcast(YearId, CompanyId);
                        }
                        i++;
                    }
                }



                //scope.Complete();
            }
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch
        {
            var JSON = JsonConvert.SerializeObject(0);
            return JSON;
        }
    }

    [OperationContract]
    public string removeMultipleAllowanceMapping(string JSon)
    {
        try
        {
            List<DAL.HCM_EmployeeAllowanceMapping> ResponseDetails = (List<DAL.HCM_EmployeeAllowanceMapping>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_EmployeeAllowanceMapping>));

            foreach (DAL.HCM_EmployeeAllowanceMapping _obj in ResponseDetails)
            {
                int EmployeeId = _obj.EmployeeID;
                int AllowanceId = _obj.AllowanceID;
                int CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeId).CompanyId;
                int YearId = context.HCM_Setup_Year.Where(a => a.IsActive == true && a.CompanyId == CompanyId && a.IsCurrentActiveYear == true).FirstOrDefault().YearId;

                var lst = context.HCM_EmployeeAllowanceMapping.FirstOrDefault(x => x.IsActive == true && x.EmployeeID == EmployeeId && x.AllowanceID == AllowanceId);

                if (lst != null)
                {
                    lst.AllowanceID = AllowanceId;
                    lst.EmployeeID = EmployeeId;
                    lst.ModifiedDate = DateTime.Now;
                    lst.ModifiedBy = objBase.UserKey;
                    lst.IsActive = false;

                    context.SaveChanges();

                    ForecastSalaries(CompanyId, EmployeeId, objBase.UserKey);

                    if (YearId > 0)
                    {
                        saveTaxForcast(YearId, CompanyId);
                    }
                }

            }

            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch
        {
            var JSON = JsonConvert.SerializeObject(0);
            return JSON;
        }
    }

    [OperationContract]
    public string companysetting_SaveInterestSlab(int CompanyId, int SlabYearId, float InterestRate)
    {
        try
        {
            var obj = context.HCM_Interest.FirstOrDefault(x => x.IsActive == true && x.CompanyId == CompanyId && x.InterestSlabYearID == SlabYearId);
            if (obj != null)
            {
                obj.IsActive = false;
                obj.ModifiedDate = DateTime.Now;
                obj.ModifiedBy = objBase.UserKey;
            }
            var objnew = new HCM_Interest();

            objnew.CompanyId = CompanyId;
            objnew.InterestSlabYearID = SlabYearId;
            objnew.InterestRate = InterestRate;
            objnew.CreatedBy = objBase.UserKey;
            objnew.CreatedDate = DateTime.Now;
            objnew.IsActive = true;

            context.HCM_Interest.Add(objnew);
            context.SaveChanges();

            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getPayrollLogId(string DateOfPayroll, int CompanyId)
    {
        try
        {
            DateTime dtMonth = DateTime.Parse(DateOfPayroll);
            string dt = dtMonth.ToString("yyyyMM");
            var PayrollLogId = context.HCM_Payroll_Log.Where(x => x.IsActive == true && x.CompanyId == CompanyId)
               .OrderByDescending(x => x.PayrollLogId)
                .AsEnumerable().FirstOrDefault(x => x.PayrollDate.ToString("yyyyMM") == dt).PayrollLogId;
            return Convert.ToString(PayrollLogId);
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getEmployeeSalaryRefresh(int PayrollLogId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RefreshOnAllowanceAdd", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@PayrollLogId", SqlDbType.Int).Value = PayrollLogId;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_VehicleDetailReport(int CompanyId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_VEHICLE_DETAIL_REPORT", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@COMPANYID", SqlDbType.Int).Value = CompanyId;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_SalaryRegister(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, string GroupBy)
    {
        try
        {
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //string dbConnectionString = context.Database.Connection.ConnectionString;
            //SqlConnection con = new SqlConnection(dbConnectionString);
            //SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_SalaryRegister", con);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.CommandTimeout = ConnectionTimeout;
            //da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            //da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            //da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            //da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            //da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            //da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            //da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            //da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            //da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            //da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            //da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            //da.Fill(dt);

            //DataSet ds_A = new DataSet();
            //DataTable dt_A = new DataTable();
            //SqlConnection con_A = new SqlConnection(dbConnectionString);
            //SqlDataAdapter da_A = new SqlDataAdapter("HCM_RPT_SalarySummaryRegister", con);
            //da_A.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da_A.SelectCommand.CommandTimeout = ConnectionTimeout;
            //da_A.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            //da_A.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            //da_A.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            //da_A.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            //da_A.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            //da_A.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            //da_A.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            //da_A.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            //da_A.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            //da_A.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            //da_A.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            //da_A.SelectCommand.Parameters.Add("@IsDeduction", SqlDbType.Bit).Value = false;
            //da_A.Fill(dt_A);


            //DataSet ds_D = new DataSet();
            //DataTable dt_D = new DataTable();
            //SqlConnection con_D = new SqlConnection(dbConnectionString);
            //SqlDataAdapter da_D = new SqlDataAdapter("HCM_RPT_SalarySummaryRegister", con);
            //da_D.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da_D.SelectCommand.CommandTimeout = ConnectionTimeout;
            //da_D.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            //da_D.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            //da_D.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            //da_D.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            //da_D.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            //da_D.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            //da_D.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            //da_D.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            //da_D.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            //da_D.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            //da_D.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            //da_D.SelectCommand.Parameters.Add("@IsDeduction", SqlDbType.Bit).Value = true;
            //da_D.Fill(dt_D);


            //DataSet ds_M = new DataSet();
            //DataTable dt_M = new DataTable();
            //SqlConnection con_M = new SqlConnection(dbConnectionString);
            //SqlDataAdapter da_M = new SqlDataAdapter("HCM_RPT_ProportinateList", con);
            //da_M.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da_M.SelectCommand.CommandTimeout = ConnectionTimeout;
            //da_M.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            //da_M.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            //da_M.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            //da_M.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            //da_M.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            //da_M.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            //da_M.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            //da_M.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            //da_M.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            //da_M.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            //da_M.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            //da_M.Fill(dt_M);


            //if (GroupBy != "0")
            //{
            //    DataRow drFinalTotal = dt.Rows[dt.Rows.Count - 1];

            //    string[] columnNames = dt.Columns.Cast<DataColumn>()
            //             .Select(x => x.ColumnName)
            //             .ToArray();

            //    for (int i = 0; i < columnNames.Length; i++)
            //    {
            //        dt.Columns[columnNames[i]].ColumnName = columnNames[i].Replace(' ', '_');
            //    }
            //    dt.AcceptChanges();

            //    DataTable _DT = dt;

            //    if (Convert.ToString(_DT.Rows[_DT.Rows.Count - 1]["SerialNo"]) == "Total")
            //    {
            //        //DataRow dr = _DT.Rows[_DT.Rows.Count - 1];
            //        //dr.Delete();
            //        //_DT.AcceptChanges();
            //    }

            //    DataView dv = _DT.DefaultView;
            //    dv.Sort = GroupBy + " asc";
            //    DataTable DT = dv.ToTable();
            //    DataView dv1 = DT.DefaultView;

            //    DT = dv.ToTable(true, GroupBy);

            //    DataTable DtFinal = _DT.Clone();

            //    for (int i = 0; i < DT.Rows.Count; i++)
            //    {
            //        if (DT.Rows[i][GroupBy].ToString() != "")
            //        {
            //            dv1.RowFilter = GroupBy + " = '" + DT.Rows[i][GroupBy].ToString() + "'";

            //            DataTable dtTemp = dv1.ToTable();

            //            if (dtTemp != null)
            //            {
            //                if (dtTemp.Rows.Count > 0)
            //                {
            //                    columnNames = dtTemp.Columns.Cast<DataColumn>()
            //                         .Select(x => x.ColumnName)
            //                         .ToArray();

            //                    dtTemp.Rows.Add("Total", dtTemp.Rows[0][GroupBy].ToString());

            //                    for (int ii = 6; ii < dtTemp.Columns.Count; ii++)
            //                    {
            //                        object objSum;
            //                        objSum = dtTemp.Compute("Sum(" + columnNames[ii] + ")", "");

            //                        dtTemp.Rows[dtTemp.Rows.Count - 1][ii] = objSum;
            //                    }

            //                    foreach (DataRow drtableOld in dtTemp.Rows)
            //                    {
            //                        DtFinal.ImportRow(drtableOld);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    DtFinal.ImportRow(drFinalTotal);
            //    DtFinal.Rows[DtFinal.Rows.Count - 1][0] = "Grand Total";

            //    int count = 0;
            //    for (int k = 0; k < DtFinal.Rows.Count; k++)
            //    {
            //        if (Convert.ToString(DtFinal.Rows[k][0]) != "Total" &&
            //            Convert.ToString(DtFinal.Rows[k][0]) != "Grand Total")
            //        {
            //            count++;
            //            DtFinal.Rows[k][0] = count;
            //        }
            //    }

            //    var JSON = JsonConvert.SerializeObject(DtFinal) + "#SPLIT#" + JsonConvert.SerializeObject(dt_A) + "#SPLIT#" + JsonConvert.SerializeObject(dt_D) + "#SPLIT#" + JsonConvert.SerializeObject(dt_M);
            //    return JSON;
            //}
            //else
            //{
            //    var JSON = JsonConvert.SerializeObject(dt) + "#SPLIT#" + JsonConvert.SerializeObject(dt_A) + "#SPLIT#" + JsonConvert.SerializeObject(dt_D) + "#SPLIT#" + JsonConvert.SerializeObject(dt_M);
            //    return JSON;
            //}



            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_SalaryRegister", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@DesignationId", SqlDbType.Int).Value = DesignationId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var JSON = "";
            if (ds != null && ds.Tables.Count >= 2)
            {
                DataTable dt = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName.Contains("_ColumnHide"))
                        {
                            dt.Columns.RemoveAt(i);
                            i--;
                        }
                    }
                    dt.AcceptChanges();
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string Coloumn_ = dt.Columns[i].ColumnName.Replace(" ", "_").Trim();
                    dt.Columns[i].ColumnName = Coloumn_;
                }
                dt.AcceptChanges();
                DataTable dtFinal = dt.Clone();
                string SortBy = "";
                if (GroupBy != "0")
                {
                    if (GroupBy == "clsLocation")
                    {
                        if (dt.Columns.Contains("clsLocation"))
                        {
                            SortBy = "clsLocation";
                        }
                    }
                    else if (GroupBy == "clsDepartment")
                    {
                        if (dt.Columns.Contains("clsDepartment"))
                        {
                            SortBy = "clsDepartment";
                        }
                    }
                    else if (GroupBy == "clsCostCenter")
                    {
                        if (dt.Columns.Contains("clsCostCenter"))
                        {
                            SortBy = "clsCostCenter";
                        }
                    }
                    else if (GroupBy == "clsSapCostCenter")
                    {
                        if (dt.Columns.Contains("clsSapCostCenter"))
                        {
                            SortBy = "clsSapCostCenter";
                        }
                    }
                }
                if (SortBy != "")
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = SortBy + " asc";
                    DataTable dtSort = dv.ToTable();
                    dv = dtSort.DefaultView;
                    DataTable dtdistinct = dv.ToTable(true, SortBy);
                    for (int i = 0; i < dtdistinct.Rows.Count; i++)
                    {
                        DataView dv1 = dtSort.DefaultView;
                        string query = "" + SortBy + " = '" + dtdistinct.Rows[i][0].ToString() + "'";
                        dv1.RowFilter = query;
                        DataTable dtFilter = dt.Clone();
                        dtFilter = dv1.ToTable();
                        DataTable dttemp = Create_TotalRow(dtFilter, dtdistinct.Rows[i][0].ToString());
                        dtFilter.Merge(dttemp);
                        dtFinal.Merge(dtFilter);
                    }
                }
                else
                {
                    dtFinal = dt.Copy();
                }

                DataTable dttempTotal = Create_TotalRow(dt, "Total");
                dtFinal.Merge(dttempTotal);
                for (int i = 0; i < dtFinal.Columns.Count; i++)
                {
                    string Coloumn_ = dtFinal.Columns[i].ColumnName.Replace("_", " ");
                    dtFinal.Columns[i].ColumnName = Coloumn_;
                }
                dtFinal.AcceptChanges();
                JSON = JsonConvert.SerializeObject(dtFinal) + "#SPLIT#" + JsonConvert.SerializeObject(dt1);
            }
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = "";
            return JSON;
        }
    }

    private DataTable Create_TotalRow(DataTable dtFilter, string FirstColoumnValue)
    {
        DataTable dtreturn = dtFilter.Clone();
        if (dtFilter != null && dtFilter.Rows.Count > 0)
        {
            dtreturn.Rows.Add();
            for (int i = 0; i < dtFilter.Columns.Count; i++)
            {
                string Coloumn = dtFilter.Columns[i].ColumnName;
                string Coloumn_Type = dtFilter.Columns[i].DataType.Name;
                if (i == 0)
                {
                    dtreturn.Rows[0][Coloumn] = FirstColoumnValue + "..";
                }
                else
                {
                    if (Coloumn_Type == "Double" || Coloumn_Type == "Decimal" || Coloumn_Type == "Single")
                    {
                        object sumObject;
                        sumObject = dtFilter.Compute("Sum([" + Coloumn + "])", string.Empty);
                        dtreturn.Rows[0][Coloumn] = sumObject;
                    }
                }
            }
        }

        return dtreturn;
    }

    [OperationContract]
    public string report_ProportionateList(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_ProportinateList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@DateOfPayroll", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_EOBIList(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_EOBIList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@DateOfPayroll", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_LoanSummary(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_LoanSummary", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_MasterSalary(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string GroupBy)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_MasterSalary", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.Fill(dt);

            //if (GroupBy != "0")
            //{
            //    DataRow drFinalTotal = dt.Rows[dt.Rows.Count - 1];

            //    string[] columnNames = dt.Columns.Cast<DataColumn>()
            //             .Select(x => x.ColumnName)
            //             .ToArray();

            //    for (int i = 0; i < columnNames.Length; i++)
            //    {
            //        dt.Columns[columnNames[i]].ColumnName = columnNames[i].Replace(' ', '_');
            //    }
            //    dt.AcceptChanges();

            //    DataTable _DT = dt;

            //    if (Convert.ToString(_DT.Rows[_DT.Rows.Count - 1]["SerialNo"]) == "Total")
            //    {
            //        //DataRow dr = _DT.Rows[_DT.Rows.Count - 1];
            //        //dr.Delete();
            //        //_DT.AcceptChanges();
            //    }

            //    DataView dv = _DT.DefaultView;
            //    dv.Sort = GroupBy + " asc";
            //    DataTable DT = dv.ToTable();
            //    DataView dv1 = DT.DefaultView;

            //    DT = dv.ToTable(true, GroupBy);

            //    DataTable DtFinal = _DT.Clone();

            //    for (int i = 0; i < DT.Rows.Count; i++)
            //    {
            //        if (DT.Rows[i][GroupBy].ToString() != "")
            //        {
            //            dv1.RowFilter = GroupBy + " = '" + DT.Rows[i][GroupBy].ToString() + "'";

            //            DataTable dtTemp = dv1.ToTable();

            //            if (dtTemp != null)
            //            {
            //                if (dtTemp.Rows.Count > 0)
            //                {
            //                    columnNames = dtTemp.Columns.Cast<DataColumn>()
            //                         .Select(x => x.ColumnName)
            //                         .ToArray();

            //                    dtTemp.Rows.Add("Total", dtTemp.Rows[0][GroupBy].ToString());

            //                    for (int ii = 6; ii < dtTemp.Columns.Count; ii++)
            //                    {
            //                        object objSum;
            //                        objSum = dtTemp.Compute("Sum(" + columnNames[ii] + ")", "");

            //                        dtTemp.Rows[dtTemp.Rows.Count - 1][ii] = objSum;
            //                    }

            //                    foreach (DataRow drtableOld in dtTemp.Rows)
            //                    {
            //                        DtFinal.ImportRow(drtableOld);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    DtFinal.ImportRow(drFinalTotal);
            //    DtFinal.Rows[DtFinal.Rows.Count - 1][0] = "Grand Total";

            //    int count = 0;
            //    for (int k = 0; k < DtFinal.Rows.Count; k++)
            //    {
            //        if (Convert.ToString(DtFinal.Rows[k][0]) != "Total" &&
            //            Convert.ToString(DtFinal.Rows[k][0]) != "Grand Total")
            //        {
            //            count++;
            //            DtFinal.Rows[k][0] = count;
            //        }
            //    }

            //    var JSON = JsonConvert.SerializeObject(DtFinal);
            //    return JSON;
            //}
            //else
            {

                var JSON = JsonConvert.SerializeObject(dt);
                return JSON;
            }
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_TaxForecast(int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, int YearId, bool IsIncludeCurrentMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_TaxForecastReport", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            //da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@YearId", SqlDbType.Int).Value = YearId;
            da.SelectCommand.Parameters.Add("@IsIncludeCurrentMonth", SqlDbType.Bit).Value = IsIncludeCurrentMonth;

            da.Fill(dt);

            /*
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               double? CarPercent = Convert.ToDouble(Convert.ToString(dt.Rows[i]["NotioAdd:CarPercent"]) == "" ? "0" : Convert.ToString(dt.Rows[i]["NotioAdd:CarPercent"]));
               double? PF = Convert.ToDouble(Convert.ToString(dt.Rows[i]["NotioAdd:PFContribut"]) == "" ? "0" : Convert.ToString(dt.Rows[i]["NotioAdd:PFContribut"]));
               double? Prev = Convert.ToDouble(Convert.ToString(dt.Rows[i]["TotalTaxableAmount"]) == "" ? "0" : Convert.ToString(dt.Rows[i]["TotalTaxableAmount"]));
               double? TotalTaxableAmount = CarPercent + PF + Prev;

               dt.Rows[i]["TotalTaxableAmount"] = TotalTaxableAmount;
           }
           */

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_ProvidentFund(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_ProvidentFundList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_PayData(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_PayData", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            //var JSON = JsonConvert.SerializeObject(e.ToString());
            //return JSON;
            return "";
        }
    }

    [OperationContract]
    public string report_SalarySlips(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            //SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_SalarySlips", con);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_SalarySlips_NEW", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);

            da.Fill(ds);

            DataTable dtMaster = ds.Tables[0];

            DataView dvAllowances = ds.Tables[1].DefaultView;
            dvAllowances.RowFilter = "AllowanceAmount <> '0'";
            DataTable dtAllowances = dvAllowances.ToTable();

            DataView dvDeduction = ds.Tables[2].DefaultView;
            dvDeduction.RowFilter = "AllowanceAmount <> '0'";
            DataTable dtDeductions = dvDeduction.ToTable();

            DataTable dtLoan = ds.Tables[3];
            DataTable dtTax = ds.Tables[4];
            DataTable dtPF = ds.Tables[5];
            DataTable dtVehicle = ds.Tables[6];

            //DataTable dtAllowances = new DataTable();
            //DataTable dtDeductions = new DataTable();

            //int Diff = 0;
            //int CountAllowance = _dtAllowances.Rows.Count;
            //int CountDeduction = _dtDeductions.Rows.Count;

            //if (CountAllowance > CountDeduction)
            //{
            //    Diff = CountAllowance - CountDeduction;

            //    dtAllowances = _dtAllowances;

            //    for (int i = 0; i < Diff; i++)
            //    {
            //        dtAllowances.Rows.Add();
            //    }
            //}
            //else if (CountAllowance < CountDeduction)
            //{
            //    Diff = -CountAllowance + CountDeduction;

            //    dtDeductions = _dtDeductions;

            //    for (int i = 0; i < Diff; i++)
            //    {
            //        dtDeductions.Rows.Add();
            //    }
            //}
            //else
            //{
            //    dtAllowances = _dtAllowances;
            //    dtDeductions = _dtDeductions;
            //}

            var JSON = JsonConvert.SerializeObject(dtMaster) + "#SPLIT#" + JsonConvert.SerializeObject(dtAllowances) + "#SPLIT#" + JsonConvert.SerializeObject(dtDeductions) +
                "#SPLIT#" + JsonConvert.SerializeObject(dtLoan) + "#SPLIT#" + JsonConvert.SerializeObject(dtTax) + "#SPLIT#" + JsonConvert.SerializeObject(dtPF)
                + "#SPLIT#" + JsonConvert.SerializeObject(dtVehicle);



            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_PayOrder(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId,
        int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, int BankId, int BankMasterId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_PayOrder", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
            da.SelectCommand.Parameters.Add("@BankMasterId", SqlDbType.Int).Value = BankMasterId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_Bonus_Pay_Order(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId,
    int CategoryId, int DesignationId, string Firstname, string Lastname, int BankId, int BankMasterId, int BonusId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_PayOrder_Bonus", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
            da.SelectCommand.Parameters.Add("@BankMasterId", SqlDbType.Int).Value = BankMasterId;
            da.SelectCommand.Parameters.Add("@BonusId", SqlDbType.Int).Value = BonusId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_BankAdvise(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId,
        int DesignationId, string Firstname, string Lastname, string PayrollMonth, int BankId, int B_Master, bool IsSepBonus)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_BankAdvise", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
            da.SelectCommand.Parameters.Add("@B_Master", SqlDbType.Int).Value = B_Master;
            da.SelectCommand.Parameters.Add("@checkB", SqlDbType.Bit).Value = IsSepBonus;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_Bonus_Bank_Adhvise(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId,
        int DesignationId, string Firstname, string Lastname, int BankId, int B_Master, int BonusId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_BankAdvise_Bonus", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
            da.SelectCommand.Parameters.Add("@B_Master", SqlDbType.Int).Value = B_Master;

            da.SelectCommand.Parameters.Add("@BonusId", SqlDbType.Int).Value = BonusId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_StaffLoan(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, int BankId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_StaffLoan", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);


            da.Fill(dt);

            DataTable DT = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                //if (Convert.ToDouble(row["Balance"]) > 0)
                {
                    DT.ImportRow(row);
                }
            }

            var JSON = JsonConvert.SerializeObject(DT);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_IncomeTax(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, int BankId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_IncomeTax", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_ResignedStaff(int EmployeeCode, int CompanyId, string fromDate, string toDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_ResignedStaff", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@fromdate", SqlDbType.Date).Value = DateTime.Parse(fromDate);
            da.SelectCommand.Parameters.Add("@todate", SqlDbType.Date).Value = DateTime.Parse(toDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_ExecutiveLis(int EmployeeCode, int CompanyId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_LIST_OF_EXECUTIVES", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Emp_List(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_EMP_LIST", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Car_Details(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_CarDetails", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Zakat_Calculation(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_CalculateZakatOnPF", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }
    [OperationContract]
    public string report_Employee_Increment(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_IncrementReport", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Arrear_New(int EmployeeCode, int CompanyId, string fromDate, string GroupBy)
    {
        try
        {
            DataSet ds = new DataSet();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_ArrearReport", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);
            da.Fill(ds);
            ds.Tables[0].TableName = "TableData";

            var JSON = "";
            if (ds != null && ds.Tables.Count >= 1)
            {
                DataTable dt = ds.Tables[0];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string Coloumn_ = dt.Columns[i].ColumnName.Replace(" ", "_").Trim();
                    dt.Columns[i].ColumnName = Coloumn_;
                }
                dt.AcceptChanges();
                DataTable dtFinal = dt.Clone();
                string SortBy = "";
                if (GroupBy != "0")
                {
                    if (GroupBy == "clsLocation")
                    {
                        if (dt.Columns.Contains("clsLocation"))
                        {
                            SortBy = "clsLocation";
                        }
                    }
                    else if (GroupBy == "clsDepartment")
                    {
                        if (dt.Columns.Contains("clsDepartment"))
                        {
                            SortBy = "clsDepartment";
                        }
                    }
                    else if (GroupBy == "clsCostCenter")
                    {
                        if (dt.Columns.Contains("clsCostCenter"))
                        {
                            SortBy = "clsCostCenter";
                        }
                    }
                    else if (GroupBy == "clsSapCostCenter")
                    {
                        if (dt.Columns.Contains("clsSapCostCenter"))
                        {
                            SortBy = "clsSapCostCenter";
                        }
                    }
                }
                if (SortBy != "")
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = SortBy + " asc";
                    DataTable dtSort = dv.ToTable();
                    dv = dtSort.DefaultView;
                    DataTable dtdistinct = dv.ToTable(true, SortBy);
                    for (int i = 0; i < dtdistinct.Rows.Count; i++)
                    {
                        DataView dv1 = dtSort.DefaultView;
                        string query = "" + SortBy + " = '" + dtdistinct.Rows[i][0].ToString() + "'";
                        dv1.RowFilter = query;
                        DataTable dtFilter = dt.Clone();
                        dtFilter = dv1.ToTable();
                        DataTable dttemp = Create_TotalRow(dtFilter, dtdistinct.Rows[i][0].ToString());
                        dtFilter.Merge(dttemp);
                        dtFinal.Merge(dtFilter);
                    }
                }
                else
                {
                    dtFinal = dt.Copy();
                }

                DataTable dttempTotal = Create_TotalRow(dt, "Total");
                dtFinal.Merge(dttempTotal);
                for (int i = 0; i < dtFinal.Columns.Count; i++)
                {
                    string Coloumn_ = dtFinal.Columns[i].ColumnName.Replace("_", " ");
                    dtFinal.Columns[i].ColumnName = Coloumn_;
                }
                dtFinal.AcceptChanges();
                dtFinal.Columns.Remove("clsLocation");
                dtFinal.Columns.Remove("clsDepartment");
                dtFinal.Columns.Remove("clsCostCenter");
                dtFinal.Columns.Remove("clsSapCostCenter");
                JSON = JsonConvert.SerializeObject(dtFinal);
            }
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Provident_Fund_Resigned_Employee(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_ProvidentFundResignedEmployees", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Rpt_report_Sessi(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_SESSIUpload", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Rpt_report_Pessi(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_PESSIUpload", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Monthly_Income_Tax_Report(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_MonthlyIncomeTaxUpload", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Tax_Statement(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_TaxStatementReport", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }
    [OperationContract]
    public string report_HOD_HR_LIST(int EmployeeCode, int CompanyId, string date)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_HR_HOD_LIST", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(date);

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_HR_List(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_HR_LIST", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Hajj_List(int EmployeeCode, int CompanyId, string fromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_HajjList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_GroupPersonalAccidentInsurance(int? EmployeeCode, int CompanyId, decimal PremiumRate, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_GroupPersonalAccidentInsurance", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@PremiumRate", SqlDbType.Float).Value = PremiumRate;

            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_GetBudgetDetailReportWithIncrease(int? EmployeeCode, int CompanyId, decimal PremiumRate, decimal PremiumRateGPA, decimal IncreaseRate, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_BudgetDetailReport_WithIncrease", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@IncreaseRate", SqlDbType.Float).Value = IncreaseRate;
            da.SelectCommand.Parameters.Add("@PremiumRateGLI", SqlDbType.Float).Value = PremiumRate;
            da.SelectCommand.Parameters.Add("@PremiumRateGPA", SqlDbType.Float).Value = PremiumRateGPA;
            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_GetBudgetDetailReportActual(int? EmployeeCode, int CompanyId, decimal PremiumRate, decimal PremiumRateGPA, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_BudgetDetailReport_AtActual", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@PremiumRateGLI", SqlDbType.Float).Value = PremiumRate;
            da.SelectCommand.Parameters.Add("@PremiumRateGPA", SqlDbType.Float).Value = PremiumRateGPA;

            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_WPPF_Payments(int CompanyId, int? EmployeeCode, string fromDate, string ToDate, decimal unitRate, decimal interestRate, string minimumwage, string resigned)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_WPPF_Payment", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@COMPANYID", SqlDbType.Int).Value = Convert.ToInt32(CompanyId);
            da.SelectCommand.Parameters.Add("@FROMDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);
            da.SelectCommand.Parameters.Add("@TODATE", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate);
            da.SelectCommand.Parameters.Add("@EMPLOYEECODE", SqlDbType.Int).Value = Convert.ToInt32(EmployeeCode);
            da.SelectCommand.Parameters.Add("@UnitRate", SqlDbType.Float).Value = Convert.ToDecimal(unitRate);
            da.SelectCommand.Parameters.Add("@InterestRate", SqlDbType.Float).Value = Convert.ToDecimal(interestRate);
            da.SelectCommand.Parameters.Add("@MinimunWage", SqlDbType.Float).Value = Convert.ToDecimal(minimumwage);
            da.SelectCommand.Parameters.Add("@IsResigned", SqlDbType.Bit).Value = resigned == "1" ? 1 : 0;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_PF_Interest_Allocation_Sheet(int CompanyId, int? EmployeeCode, string date, decimal Rate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_PFInteresAllocationSheet", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@COMPANYID", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EMPLOYEECODE", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@YEAR", SqlDbType.DateTime).Value = Convert.ToDateTime(date);

            da.SelectCommand.Parameters.Add("@RATE", SqlDbType.Decimal).Value = Rate;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_GetBudgetSummaryReportActual(int CompanyId, decimal PremiumRate, string PayrollMonth, string EmployeeCode, decimal PremiumRateGPA, int FilterBy)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_BudgetSummaryReport_AtActual", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@PremiumRateGLI", SqlDbType.Float).Value = PremiumRate;
            da.SelectCommand.Parameters.Add("@PremiumRateGPA", SqlDbType.Float).Value = PremiumRateGPA;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Float).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@FilterBy", SqlDbType.Int).Value = FilterBy;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_GetBudgetSummaryReportWithIncrease(int CompanyId, decimal PremiumRate, decimal IncreaseRate, string PayrollMonth, string EmployeeCode, decimal PremiumRateGPA, int GroupByValue)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_BudgetSummaryReport_WithIncrease", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@IncreaseRate", SqlDbType.Float).Value = IncreaseRate;
            da.SelectCommand.Parameters.Add("@PremiumRateGLI", SqlDbType.Float).Value = PremiumRate;
            da.SelectCommand.Parameters.Add("@PremiumRateGPA", SqlDbType.Float).Value = PremiumRateGPA;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@FilterBy", SqlDbType.Int).Value = GroupByValue;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_GroupLifeInsuranceBelowAndEqualSixtyFive(int? EmployeeCode, int CompanyId, decimal PremiumRate, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_GroupLifeInsuranceBelowAndEqual_SixtyFive", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@PremiumRate", SqlDbType.Float).Value = PremiumRate;

            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_GroupLifeInsuranceAboveSixtyFive(int? EmployeeCode, int CompanyId, decimal PremiumRate, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_GroupLifeInsuranceAbove_SixtyFive", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@PremiumRate", SqlDbType.Float).Value = PremiumRate;

            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_ListOfNonMedicalStaff(int? EmployeeCode, int CompanyId, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_ListOfNonMedicalStaff", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_EOBI_No(int? EmployeeCode, int? CompanyId, string FromDate, string ToDate)
    {
        try
        {

            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_EOBI_Number", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate);

            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_Quarterly(int? EmployeeCode, int? CompanyId, string FromDate, string ToDate)
    {
        try
        {

            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_QuarterlyReport", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate);

            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_Gratuity(int? EmployeeCode, int? CompanyId, string FromDate, string ToDate, int isGratuity)
    {
        try
        {

            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_GratuityPayment", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate);

            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@IsGratuityPayment", SqlDbType.Bit).Value = isGratuity == 1 ? true : false;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_Quarterly_Report(int? EmployeeCode, int? CompanyId, string FromDate, string ToDate)
    {
        try
        {

            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_QuarterlyReport", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate);

            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_WPPF_Salary_History(int? EmployeeCode, int? CompanyId, string FromDate, string ToDate, int EmployeeType)
    {
        try
        {

            DataSet ds = new DataSet();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_WPPF_SalaryHistory", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@COMPANYID", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@FROMDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@TODATE", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate);
            da.SelectCommand.Parameters.Add("@IsResigned", SqlDbType.Bit).Value = EmployeeType == 1 ? true : false;

            da.SelectCommand.Parameters.Add("@EMPLOYEECODE", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(ds);

            ///Working

            List<WPPFSummaryModel> _ConvertDTToList = ds.Tables[1].AsEnumerable()
             .Select(row => new WPPFSummaryModel
             {
                 // assuming column 0's type is Nullable<long>
                 ColumnName1 = row.Field<string>(0),
                 ColumnName2 = row.Field<string>(1).Replace("\t", " "),
                 Province = row.Field<string>(2),
                 Count = row.Field<int>(3),
             }).ToList();


            var _GroupByColumn1 = _ConvertDTToList.Select(x => new { ColumnName2 = x.ColumnName2 }).Distinct().ToList();


            DataTable _DtSummary = new DataTable();
            _DtSummary.Columns.Add("Description");
            _DtSummary.Columns.Add("Count");
            _DtSummary.Columns.Add("IsBold");
            int _ColumnName2Count = 0;
            DataRow _DR;
            for (int i = 0; i < _GroupByColumn1.Count; i++)
            {
                var _SelectedData = _ConvertDTToList.Where(x => x.ColumnName2 == _GroupByColumn1[i].ColumnName2).ToList();


                for (int j = 0; j < _SelectedData.Count; j++)
                {
                    _DR = _DtSummary.NewRow();

                    if (j == 0)
                    {
                        _DR[0] = _GroupByColumn1[i].ColumnName2;
                        _DR[2] = 1;

                        _ColumnName2Count += Convert.ToInt32(_DR[1] = _SelectedData[j].Count);
                    }

                    else
                    { _DR[0] = _SelectedData[j].Province; _DR[2] = 0; }


                    _DR[1] = _SelectedData[j].Count;

                    _DtSummary.Rows.Add(_DR);
                }
            }

            _DR = _DtSummary.NewRow();
            _DR[0] = "Total";

            _DR[1] = _ColumnName2Count;
            _DR[2] = 1;

            _DtSummary.Rows.Add(_DR);

            ///End Working

            ds.Tables.Add(_DtSummary);


            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_Leave_Encashment(int? EmployeeCode, int? CompanyId, string FromDate, string ToDate, int EmployeeType)
    {
        try
        {

            DataSet ds = new DataSet();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_LeaveEncashment", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate);
            if (EmployeeType == 0)
                da.SelectCommand.Parameters.Add("@IsResigned", SqlDbType.Bit).Value = false;
            else if (EmployeeType == 1)
                da.SelectCommand.Parameters.Add("@IsResigned", SqlDbType.Bit).Value = true;
            else
                da.SelectCommand.Parameters.Add("@IsResigned", SqlDbType.Bit).Value = null;

            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(ds);

            ds.Tables[0].TableName = "LeaveEncashmentData";
            ds.Tables[1].TableName = "LeaveEncashmentDataSummary";


            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_EOBI_Employee(int? EmployeeCode, int? CompanyId, string FromDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_EOBI_EmployeeList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = Convert.ToDateTime(FromDate);


            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_NewJoining(int EmployeeCode, int CompanyId, string fromDate, string toDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_NewJoining", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@fromdate", SqlDbType.Date).Value = DateTime.Parse(fromDate);
            da.SelectCommand.Parameters.Add("@todate", SqlDbType.Date).Value = DateTime.Parse(toDate);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_OverTime(int? EmployeeCode, int CompanyId, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_Over_Time", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Tax_Return(string PayrollMonth, int CompanyId, int? EmployeeCode)
    {
        try
        {


            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_TaxReturn2020", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = Convert.ToDateTime(PayrollMonth);
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Tax_Certificate(string FromDate, string ToDate, int CompanyId, int? EmployeeCode)
    {
        try
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_TaxCertificate", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime(ToDate);
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;



            da.Fill(ds);
            CommonHelper.SetCertificateData(ds);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return JsonConvert.SerializeObject(1);
            else
            {
                return JsonConvert.SerializeObject(0);
            }

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Contribution_Sheet(int? EmployeeCode, int CompanyId, string date)
    {
        try
        {

            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Rpt_ContributionSheet", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@COMPANYID", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EMPLOYEECODE", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@YEAR", SqlDbType.DateTime).Value = Convert.ToDateTime(date);


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_EOBI_Not_Assigned_Number(int? EmployeeCode, int CompanyId, string FromDate, string ToDate)
    {
        try
        {


            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_No_EOBI_Number", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@fromdate", SqlDbType.Date).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@todate", SqlDbType.Date).Value = Convert.ToDateTime(ToDate);
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string EOBI_Employee_List_Individual(int? EmployeeCode, int CompanyId, string FromDate, string ToDate)
    {
        try
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_EOBI_EmployeeList_Individual", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@fromdate", SqlDbType.Date).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@todate", SqlDbType.Date).Value = Convert.ToDateTime(ToDate);
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;


            da.Fill(ds);
            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_Common_Wealth_Tax(int? EmployeeCode, int CompanyId, string FromDate, string ToDate)
    {
        try
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_CommonWealthTaxReport", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            if (EmployeeCode == null)
                da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = 0;
            else
                da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@YearFrom", SqlDbType.Date).Value = Convert.ToDateTime(FromDate);
            da.SelectCommand.Parameters.Add("@YearTo", SqlDbType.Date).Value = Convert.ToDateTime(ToDate);


            da.Fill(ds);
            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Increment_Lettet(int? EmployeeCode, int CompanyId, string FromDate)
    {
        try
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_IncrementLetter", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = Convert.ToDateTime(FromDate);


            da.Fill(ds);
            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string Report_EOBI_Employee_Detail(int? EmployeeCode, int CompanyId, string Year)
    {
        try
        {

            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_Employees_EOBI_Details", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Date", SqlDbType.Date).Value = Convert.ToDateTime(Year);
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;




            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }
    [OperationContract]
    public string report_PF_Statement(int? EmployeeCode, int CompanyId, string Year)
    {
        try
        {
            int _Year = Convert.ToDateTime(Year).Year;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_PFStatement", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@COMPANYID", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EMPLOYEECODE", SqlDbType.Int).Value = EmployeeCode;

            da.SelectCommand.Parameters.Add("@YEAR", SqlDbType.Int).Value = _Year;


            da.Fill(ds);
            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_CarList(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, int BankId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_CarList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;

            da.Fill(dt);

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Sessi(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_SocialSecuritySchemeContribution", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            //da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_BonusList(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, int BankId, int BonusId, bool IsSepBonus)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_BonusList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            if (PayrollMonth == "")
            {
                da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = null;
            }
            else
            {
                da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            }

            da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
            da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = BonusId;
            da.SelectCommand.Parameters.Add("@checkB", SqlDbType.Bit).Value = IsSepBonus;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getTaxForecastedDetailsByEmployeeId(int EmployeeId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_GetEmployeeForecastedTaxDetails", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveFuelMapping(int DesignationId, float Fuel, float FirstYear, float SecondYear, float ThirdYear, float FourthYear, float FifthYear, string IsActual)
    {
        try
        {
            //var obj = context.HCM_Fuel_DesignationMapping.FirstOrDefault(x => x.IsActive == true && x.DesignationId == DesignationId);
            //if (obj != null)
            //{
            //    obj.IsActive = false;
            //    obj.ModifiedDate = DateTime.Now;
            //    obj.ModifiedBy = objBase.UserKey;
            //    context.SaveChanges();
            //}

            //var objNew = new HCM_Fuel_DesignationMapping();
            //objNew.DesignationId = DesignationId;
            //objNew.FuelInLitres = Fuel;
            //objNew.IsActive = true;
            //objNew.CreatedBy = objBase.UserKey;
            //objNew.CreatedDate = DateTime.Now;
            //context.HCM_Fuel_DesignationMapping.Add(objNew);
            //context.SaveChanges();

            var obj = context.HCM_Setup_RM.FirstOrDefault(x => x.IsActive == true && x.DesignationId == DesignationId);
            if (obj != null)
            {
                obj.IsActive = false;
                obj.ModifiedDate = DateTime.Now;
                obj.ModifiedBy = objBase.UserKey;
                context.SaveChanges();
            }

            var objNew = new HCM_Setup_RM();
            objNew.DesignationId = DesignationId;
            objNew.FuelInLitres = Fuel;
            objNew.RM_FirstYear = FirstYear;
            objNew.RM_SecondYear = SecondYear;
            objNew.RM_ThirdYear = ThirdYear;
            objNew.RM_ForthYear = FourthYear;
            objNew.RM_FifthYear = FifthYear;
            objNew.IsOnActual = Convert.ToBoolean(IsActual);
            objNew.IsActive = true;
            objNew.CreatedBy = objBase.UserKey;
            objNew.CreatedDate = DateTime.Now;
            context.HCM_Setup_RM.Add(objNew);
            context.SaveChanges();

            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getBankBranch(int BankMasterId)
    {
        var List = context.HRMS_Setup_Bank.Where(x => x.IsActive == true && x.BankMasterId == BankMasterId).Select(s => new
        {
            Value = s.BankDescription,
            Id = s.BankId
        })
       .OrderBy(a => a.Value)
        .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getBank()
    {
        var List = context.HRMS_Setup_BankMaster.Where(x => x.IsActive == true).Select(s => new
        {
            Value = s.BankName,
            Id = s.BankMasterId
        })
        .OrderBy(a => a.Value)
        .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getBonus(int CompanyId)
    {
        var List = context.HCM_Setup_Allowance.Where(a => a.IsActive == true)
             .Where(a => a.CompanyId == CompanyId)
             .Where(a => a.HCM_CompanyFormula.FirstOrDefault().HCM_Bonus.Count > 0)
               .Select(a => new
               {
                   Id = a.AllowanceID,
                   Value = a.AllowanceName,

               })
               .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }
      
    [OperationContract]
    public string getgeneraldata(int SetupMasterID)
    {
        var List = context.HCM_Setup_Detail.Where(a => a.IsActive == true)
             .Where(a => a.SetupMasterID == SetupMasterID)
               .Select(a => new
               {
                   Id = a.SetupDetailID,
                   Value = a.ColumnValue,
               })
               .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string save_UploadFile(int CompanyId, string MonthOf, string Filename, string UploadType, int AllowanceId, int YearId, int TypeId)
    {
        ResponseHelper _objResponseHelper = new ResponseHelper();
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);

            if (!Filename.Contains(".xls") && !Filename.Contains(".xlsx"))
            {
                _objResponseHelper.ResponseMessageType = Constant.ResponseType.WARNING;
                _objResponseHelper.ResponseMessage = "Only Excel Format Allowed";
                _objResponseHelper.ResponseData = "";
                var JSON1 = JsonConvert.SerializeObject(_objResponseHelper);
                return JSON1;
            }

            if (UploadType == "Overtime")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_OvertimeHours", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = AllowanceId;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "AbsentLog")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_AbsentLog", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "Separation")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_SeparationList", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "ContractRenewal")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_ContractRenewal", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "Allowance")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_AllowanceAmountUpload", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.SelectCommand.Parameters.Add("@AllownaceId", SqlDbType.Int).Value = AllowanceId;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "BankAccount")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_AccountsDetail", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "Increment")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_Increment", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "NewEmployee")
            {
                //SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_NewEmployees_Data_Insertion", con);
                SqlDataAdapter da = new SqlDataAdapter("Sp_Upload_New_EmployeeData", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                //da.Fill(dt);
                da.Fill(ds);
                if (ds.Tables[1].Rows.Count == 0)
                {
                    _objResponseHelper.ResponseMessageType = (Constant.ResponseType)ds.Tables[0].Rows[0]["MstType"];
                    _objResponseHelper.ResponseMessage = ds.Tables[0].Rows[0]["Msg"].ToString();
                    _objResponseHelper.ResponseData = "";
                }
                else
                {
                    _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                    _objResponseHelper.ResponseMessage = "NewEmployee Upload UnSuccessfull";
                    _objResponseHelper.ResponseData = "";
                }
            }
            else if (UploadType == "EmployeeEducationDetail")
            {
                //SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_NewEmployees_Data_Insertion", con);
                SqlDataAdapter da = new SqlDataAdapter("SP_Upload_Employee_Education_Detail", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@user", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.SelectCommand.Parameters.Add("@company", SqlDbType.Int).Value = CompanyId;
                //da.Fill(dt);  
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = (Constant.ResponseType)ds.Tables[0].Rows[0]["ErrorType"];
                        _objResponseHelper.ResponseMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "IncrementLetter")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_Increment_IncrementLetter", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserId;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.NVarChar).Value = MonthOf;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = Filename;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "ConfirmationLetter")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_Confirmation", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@MonthOF", SqlDbType.Date).Value = MonthOf;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "LeaveEncashment")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_LeaveEncashment", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.SelectCommand.Parameters.Add("@YearId", SqlDbType.Int).Value = YearId;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            else if (UploadType == "GeneralData")
            {
                SqlDataAdapter da = new SqlDataAdapter("HCM_UPD_GeneralData", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = ConnectionTimeout;
                da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = objBase.UserKey;
                da.SelectCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = Filename;
                da.SelectCommand.Parameters.Add("@YearId", SqlDbType.Int).Value = YearId;
                da.SelectCommand.Parameters.Add("@TypeId", SqlDbType.Int).Value = TypeId;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.SUCCESS;
                        _objResponseHelper.ResponseMessage = "Upload Successfully";
                        _objResponseHelper.ResponseData = "";
                    }
                    else
                    {
                        _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
                        _objResponseHelper.ResponseMessage = "Upload UnSuccessfull";
                        _objResponseHelper.ResponseData = "";
                    }
                }
            }
            var JSON_ = JsonConvert.SerializeObject(_objResponseHelper);
            return JSON_;
        }
        catch (Exception e)
        {
            _objResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
            _objResponseHelper.ResponseMessage = e.Message;
            _objResponseHelper.ResponseData = "";
            var JSON = JsonConvert.SerializeObject(_objResponseHelper);
            return JSON;
        }
    }

    [OperationContract]
    public string getLoanType()
    {
        var lst = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.SetupMasterID == (int)Constant.HCMSetupMaster.LoanType)
            .Select(a => new
            {
                Id = a.SetupDetailID,
                Value = a.ColumnValue,
            })
            .ToList();

        var JSON = JsonConvert.SerializeObject(lst);
        return JSON;
    }
       
    [OperationContract]
    public string ApproveRequest(string EmployeeChangeRequestId)
    {
        return EmployeeInfoUpdate(EmployeeChangeRequestId);
    }

    [OperationContract]
    public string ApproveMultiRequest(string MultiReqIds)
    {
        return EmployeeInfoUpdate(MultiReqIds);
    }

    public string EmployeeInfoUpdate(string EmployeeChangeRequestId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeInfoUpdate", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@changeRequestId", SqlDbType.VarChar).Value = EmployeeChangeRequestId;

            da.Fill(dt);
            var JSON = "1";
            return JSON;
        }
        catch (Exception ex)
        {
            var JSON = JsonConvert.SerializeObject(ex.ToString());
            return JSON;

        }
    }

    [OperationContract]
    public string multimanage_IncTaxForcast(string JSon)
    {
        List<IncTaxForcast> ResponseDetails = (List<IncTaxForcast>)CommonHelper.Deserialize(JSon, typeof(List<IncTaxForcast>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int i = 0;
                bool IsFlag = false;

                foreach (IncTaxForcast obj in ResponseDetails)
                {
                    var objCheck = context.HCM_EmployeeSalary.Where(a => a.IsActive == true && a.EmployeeID == obj.EmployeeId).ToList();

                    if (objCheck != null)
                    {
                        objCheck.ForEach(m => m.IsActive = false);
                        objCheck.ForEach(m => m.ModifiedBy = objBase.UserKey);
                        objCheck.ForEach(m => m.ModifiedDate = DateTime.Now);
                        context.SaveChanges();

                        IsFlag = true;
                        //i++;
                    }

                    if (IsFlag)
                    {
                        var _obj = new HCM_EmployeeSalary();
                        _obj.EmployeeID = obj.EmployeeId;
                        _obj.BasicSalary = objCheck[i].BasicSalary;
                        _obj.NetSalary = objCheck[i].NetSalary;
                        _obj.GrossSalary = objCheck[i].GrossSalary;
                        _obj.CreatedDate = DateTime.Now;
                        _obj.CreatedBy = objBase.UserKey;
                        _obj.UserIP = objBase.UserIP;
                        _obj.IsIncrement = objCheck[i].IsIncrement;
                        _obj.IsActive = true;
                        _obj.IsGranted = objCheck[i].IsGranted;
                        _obj.IncrementProcessStartDate = objCheck[i].IncrementProcessStartDate;
                        _obj.IncrementAppliedDate = objCheck[i].IncrementAppliedDate;
                        _obj.IncrementTypeId = objCheck[i].IncrementTypeId;
                        _obj.AdvanceTaxPercent = Convert.ToDouble(obj.IncPercent);

                        context.HCM_EmployeeSalary.Add(_obj);
                    }
                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string multimanage_GetIncTaxForcastRecords()
    {
        try
        {
            //string dt = DateTime.Parse(monthofpayroll).ToString("yyyyMM");
            var lst = context.HCM_EmployeeSalary
                .Where(x => x.IsActive == true)
                 //.AsEnumerable()
                 //.Where(x => x.Month.ToString("yyyyMM") == dt)
                 .Select(x => new
                 {
                     x.EmployeeID,
                     x.AdvanceTaxPercent
                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getEmployeeForIncTaxForcast(int GroupId, int CompanyId, int LocationId, int BUId, int CostCenterId, int DesignationId, int DepartmentId,
        string FirstName, string LastName, string EmployeeCode, int CategoryId)
    {
        FirstName = FirstName == null ? string.Empty : FirstName;
        LastName = LastName == null ? string.Empty : LastName;
        EmployeeCode = EmployeeCode == null ? string.Empty : EmployeeCode;
        int InterestSetting = (int)Constant.HCMSetupMaster.InterestSetting;
        int? IntNull = null;

        var List = context.Setup_Employee.Where(c => c.IsActive == true && c.CompanyId == CompanyId
                                && (c.DesignationId == DesignationId || DesignationId == 0)
                                 && (c.DepartmentId == DepartmentId || DepartmentId == 0)

                                  && (c.FirstName.Contains(FirstName) || FirstName == string.Empty)
                                  && (c.LastName.Contains(LastName) || LastName == string.Empty)
                                      && (c.EmployeeCode == EmployeeCode || EmployeeCode == string.Empty)

                                      //&& (c.InchargeId == UserKey || c.EmployeeId == UserKey

                                      //|| IsSuperAdmin == true || IsAdmin == true || (RoleCode == roleAdmin && c.CompanyId == CompanyId))

                                      && (c.LocationId == LocationId || LocationId == 0)
                                      && (c.Setup_Department.BusinessUnitId == BUId || BUId == 0)
                                      && (c.CostCenterId == CostCenterId || CostCenterId == 0)
                                      && (c.Setup_Designation.CategoryId == CategoryId || CategoryId == 0)
                                         && (c.Setup_EmployeeType.IsPermenant == true)
                                      && (
                                      (c.EmployeeTypeId != (int)Constant.EmployeeTypeId.Contract_No_Bonus)
                                      &&
                                      (c.EmployeeTypeId != (int)Constant.EmployeeTypeId.Contract_PF_Only)
                                      )


            ).Select(c => new
            {
                EmployeeId = c.EmployeeId,
                CompanyId = c.CompanyId,
                EmployeeCode = c.EmployeeCode,
                FirstName = c.FirstName,
                LastName = c.LastName,
                CNIC = c.CNIC,
                Designation = c.Setup_Designation.DesignationName,
                DesignationId = c.DesignationId,
                Department = c.Setup_Department.DepartmentName,
                DepartmentId = c.DepartmentId,
                DateOfBirth = c.DateOfBirth,
                JoiningDate = c.JoiningDate,
                PersonalEmail = c.PersonalEmailAddress,
                OfficalEmail = c.OfficeEmailAddress,
                Gender = c.HRMS_Setup_Gender.GenderTitle,
                GenderId = c.GenderId,
                EmployeeType = c.Setup_EmployeeType.TypeName,
                IsActive = c.IsActive,
                Company = c.Setup_Location.Setup_Company.CompanyName,
                Location = c.Setup_Location.LocationName,
                LocationId = c.LocationId,
                CostCenterId = c.CostCenterId,
                MaritalStatusId = c.MatrialStatusId,
                BuisnessUnitId = c.Setup_Department.BusinessUnitId,
                ReligionId = c.ReligionId,
                EmployeeImage = c.PictureName,
                Extension = c.Extension,
                PhoneNumber = c.Phone,
                MobileNumber = c.OfficialMobileNumber,
                IsAllowInterest = c.IsAllowInterest,
                OpeningBalance = c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance == null ? 0 :
                c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance,
                Salary = c.HCM_EmployeeSalary.FirstOrDefault(x => x.IsActive == true && x.IsIncrement == false).GrossSalary,
                InterestStandard = c.Setup_Company.HCM_Company_Settings.FirstOrDefault(x => x.IsActive == true && x.SetupID == InterestSetting).Value,
                Balance = c.HCM_ProvidentFund.Where(x => x.IsActive == true).OrderBy(b => b.ProvidentFundId).FirstOrDefault().TotalBalance,
                EmployeeIdSettlement = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().EmployeeId == null ? IntNull :
                c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().EmployeeId,
                LoanBalance = context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance) == null ? 0 : context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance),
                TotalArrearAmount = c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount) == null ? 0 :
                c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount),
                IsSettled = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().IsSettled == null ? 0 :
                c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().IsSettled == false ? 1 : 2,

                SESSILimit = context.HCM_EmployeeSESSI_Details.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == c.EmployeeId) == null ? 0 :
                context.HCM_EmployeeSESSI_Details.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == c.EmployeeId).SESSIAmount


            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string holdVehicleDeductionByVehicleMasterId(int VehicleMasterId, int Status)
    {
        try
        {
            var obj = context.HCM_Vehicle_Master.FirstOrDefault(x => x.VehicleMasterId == VehicleMasterId);
            obj.IsHold = Status == 1 ? true : false;
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = objBase.UserKey;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(Status);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getSettelmentType(int CompanyId)
    {
        var List = context.HCM_Setup_Settlement.Where(a => a.IsActive == true && a.CompanyId == CompanyId)
            .Select(a => new
            {
                Id = a.SettlementID,
                Value = a.SettlementName,

            })
            .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getPaymentType(int CompanyId)
    {
        var List = context.HCM_Setup_Pay_type.Where(a => a.IsActive == true && a.CompanyId == CompanyId)
            .Select(a => new
            {
                Id = a.Pay_typeID,
                Value = a.Pay_method,

            })
            .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string SaveLoanPayment(string SettlementTypeId, string PaymentTypeId, string EmployeeId, string SettlementAmount, string ChequeDate, string ChequeNo, string Bank, string _LoanMasterId, string _SettlementDetailId)
    {
        try
        {
            //int? IntNull = null;

            int LoanMasterId = Convert.ToInt32(_LoanMasterId);
            int SettlementDetailId = Convert.ToInt32(_SettlementDetailId);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("uspInsertUpdateLoanSettlement", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@SettlementTypeId", SqlDbType.Int).Value = SettlementTypeId;
            da.SelectCommand.Parameters.Add("@PaymentTypeId", SqlDbType.Int).Value = PaymentTypeId;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@SettlementAmount", SqlDbType.Float).Value = SettlementAmount;
            da.SelectCommand.Parameters.Add("@ChequeDate", SqlDbType.DateTime).Value = ChequeDate;
            da.SelectCommand.Parameters.Add("@ChequeNo", SqlDbType.NVarChar).Value = ChequeNo;
            da.SelectCommand.Parameters.Add("@Bank", SqlDbType.NVarChar).Value = Bank;
            da.SelectCommand.Parameters.Add("@_LoanMasterId", SqlDbType.Int).Value = _LoanMasterId;
            da.SelectCommand.Parameters.Add("@_SettlementDetailId", SqlDbType.Int).Value = _SettlementDetailId;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = new Base().UserId;

            da.Fill(dt);
            var JSON = Convert.ToString(dt.Rows[0]["MSG"]);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }
    [OperationContract]
    public string getLoanSettlementList(int LoanMasterId)
    {
        var List = context.HCM_Settlement_Detail.Where(a => a.IsActive == true && a.LoanMasterId == LoanMasterId &&
            (a.HCM_Payroll_Log.IsLocked == true ||
            context.HCM_Settlement_Detail.Where(x => x.IsActive == true).Max(s => s.SettlementDetailId) == null ? true :
            a.SettlementDetailId == context.HCM_Settlement_Detail.Where(x => x.IsActive == true).Max(s => s.SettlementDetailId))
            )

            .Select(a => new
            {
                SettlementDetailId = a.SettlementDetailId,
                SettlementTypeId = a.Settlement_typeId,
                PaymentTypeId = a.Pay_TypeId,
                SettlementType = a.HCM_Setup_Settlement.SettlementName,
                PaymentType = a.HCM_Setup_Pay_type.Pay_method,
                SettlementAmount = a.SettlementAmount,
                ChequeNo = a.cheque_No,
                ChequeDate = a.cheque_Date,
                Bank = a.Bank_Detail,
                IsLock = (a.HCM_Payroll_Log.IsLocked == null || a.HCM_Payroll_Log.IsLocked == false) ? 0 : 1 //a.HCM_Payroll_Log.IsLocked,

            })
            .OrderByDescending(b => b.SettlementDetailId)
            //.Take(1)
            .ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string DeleteSettlement(int SettlementDetailId)
    {
        //try
        //{
        //    var obj = context.HCM_Settlement_Detail.FirstOrDefault(x => x.IsActive == true && x.SettlementDetailId == SettlementDetailId);
        //    obj.IsActive = false;
        //    obj.ModifiedDate = DateTime.Now;
        //    obj.ModifiedBy = objBase.UserKey;

        //    context.SaveChanges();

        //    var JSON = JsonConvert.SerializeObject(1);
        //    return JSON;
        //}
        //catch (Exception e)
        //{
        //    return e.InnerException.ToString();
        //}

        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("uspDeleteLoanSettlement", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@_SettlementDetailId", SqlDbType.Int).Value = SettlementDetailId;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = new Base().UserId;
            da.Fill(dt);
            var JSON = Convert.ToString(dt.Rows[0]["MSG"]);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }
    [OperationContract]
    public string getDesignationForMultiAllowance(int CompanyId, int CategoryId, int DesignationId)
    {
        var List = context.Setup_Designation.Where(c => c.IsActive == true && c.Setup_Category.CompanyId == CompanyId
            && (CategoryId == 0 ? true : c.Setup_Category.CategoryId == CategoryId) && (DesignationId == 0 ? true : c.DesignationId == DesignationId))
            .Select(a => new
            {
                CompanyId = a.Setup_Category.CompanyId,
                Id = a.DesignationId,
                Value = a.DesignationName,

            })
            .OrderBy(c => c.Value).ToList();

        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getGratuityData(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_Gratuity", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    public DataTable getBonusReleaseEmployeeAllowanceId(int CompanyId, int PayrollLogId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Lock_BonusRelease", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@PayrollLogId", SqlDbType.Int).Value = PayrollLogId;

            da.Fill(dt);
            //var JSON = JsonConvert.SerializeObject(dt);
            return dt;
        }
        catch (Exception e)
        {
            //var JSON = JsonConvert.SerializeObject(e.ToString());
            return null;
        }
    }

    [OperationContract]
    public string saveWppfSlab(int CompanyId, string CategoryName, float SalaryRangeFrom, float SalaryRangeTo, float UnitValue, int YearId /*string YearFrom, string YearTo*/, int WppfSlabId)
    {
        try
        {
            if (WppfSlabId == 0)
            {
                HCM_Setup_Wppf_Slab obj = new HCM_Setup_Wppf_Slab();

                obj.CompanyId = CompanyId;
                //obj.TaxId = TaxId;
                obj.CategoryName = CategoryName;
                obj.SalaryRangeStart = SalaryRangeFrom;
                obj.SalaryRangeEnd = SalaryRangeTo;
                obj.Unit = UnitValue;
                //obj.TaxPercent = TaxPercent;
                //obj.TaxYearFrom = DateTime.Parse(YearFrom);
                //obj.TaxYearTo = DateTime.Parse(YearTo);
                obj.YearId = YearId;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = objBase.UserKey;
                obj.IsActive = true;

                context.HCM_Setup_Wppf_Slab.Add(obj);
                context.SaveChanges();

                return "1";
            }
            else
            {
                var obj = context.HCM_Setup_Wppf_Slab.FirstOrDefault(x => x.IsActive == true && x.WppfSlabId == WppfSlabId);

                obj.CompanyId = CompanyId;
                //obj.TaxId = TaxId;
                obj.CategoryName = CategoryName;
                obj.SalaryRangeStart = SalaryRangeFrom;
                obj.SalaryRangeEnd = SalaryRangeTo;
                obj.Unit = UnitValue;
                //obj.TaxPercent = TaxPercent;
                //obj.TaxYearFrom = DateTime.Parse(YearFrom);
                //obj.TaxYearTo = DateTime.Parse(YearTo);
                obj.YearId = YearId;
                obj.ModifiedBy = objBase.UserKey;
                obj.ModifiedDate = DateTime.Now;

                context.SaveChanges();

                return "1";
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [OperationContract]
    public string getWppfSlabListing(int CompanyId)
    {
        try
        {
            var lst = context.HCM_Setup_Wppf_Slab.Where(x => x.IsActive == true && x.CompanyId == CompanyId)
                .AsEnumerable()
                 .Select(x => new
                 {
                     WppfSlabId = x.WppfSlabId,
                     CompanyId = x.CompanyId,
                     //TaxId = x.TaxId,

                     CompanyName = x.Setup_Company.CompanyName,
                     //Tax = x.HCM_Setup_Tax.TaxName,
                     WppfSlab = x.CategoryName,
                     SalaryRangeStart = x.SalaryRangeStart,
                     SalaryRangeEnd = x.SalaryRangeEnd,
                     UnitValue = x.Unit,
                     //TaxPercent = x.TaxPercent,
                     YearId = x.YearId,
                     Year = x.HCM_Setup_Year.YearId != null ? Convert.ToDateTime(x.HCM_Setup_Year.YearFrom).Year.ToString() + " - " + Convert.ToDateTime(x.HCM_Setup_Year.YearTo).Year.ToString() : "",

                 }).ToList();
            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }


    }

    [OperationContract]
    public string getWppf(int CompanyId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("WPPF_Slabing", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@company", SqlDbType.Int).Value = CompanyId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string DeleteWppfSlabById(int WppfSlabId)
    {
        try
        {
            var obj = context.HCM_Setup_Wppf_Slab.FirstOrDefault(x => x.IsActive == true && x.WppfSlabId == WppfSlabId);
            obj.IsActive = false;
            context.SaveChanges();
            var JSON = JsonConvert.SerializeObject(1);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getEmployeeForWppf(int GroupId, int CompanyId, int LocationId, int BUId, int CostCenterId, int DesignationId, int DepartmentId,
        string FirstName, string LastName, string EmployeeCode, int CategoryId)
    {
        FirstName = FirstName == null ? string.Empty : FirstName;
        LastName = LastName == null ? string.Empty : LastName;
        EmployeeCode = EmployeeCode == null ? string.Empty : EmployeeCode;
        //int InterestSetting = (int)Constant.HCMSetupMaster.InterestSetting;
        int? IntNull = null;

        var List = context.Setup_Employee.Where(c => c.IsActive == true && c.CompanyId == CompanyId
                                && (c.DesignationId == DesignationId || DesignationId == 0)
                                 && (c.DepartmentId == DepartmentId || DepartmentId == 0)

                                      && (c.FirstName.Contains(FirstName) || FirstName == string.Empty)
                                      && (c.LastName.Contains(LastName) || LastName == string.Empty)

                                      && (c.EmployeeCode == EmployeeCode || EmployeeCode == string.Empty)

                                      //&& (c.InchargeId == UserKey || c.EmployeeId == UserKey

                                      //|| IsSuperAdmin == true || IsAdmin == true || (RoleCode == roleAdmin && c.CompanyId == CompanyId))

                                      && (c.LocationId == LocationId || LocationId == 0)
                                      && (c.Setup_Department.BusinessUnitId == BUId || BUId == 0)
                                      && (c.CostCenterId == CostCenterId || CostCenterId == 0)
                                      && (c.Setup_Designation.CategoryId == CategoryId || CategoryId == 0)



            ).Select(c => new
            {
                EmployeeId = c.EmployeeId,
                CompanyId = c.CompanyId,
                EmployeeCode = c.EmployeeCode,
                FirstName = c.FirstName,
                LastName = c.LastName,
                CNIC = c.CNIC,
                Designation = c.Setup_Designation.DesignationName,
                DesignationId = c.DesignationId,
                Department = c.Setup_Department.DepartmentName,
                DepartmentId = c.DepartmentId,
                DateOfBirth = c.DateOfBirth,
                JoiningDate = c.JoiningDate,
                PersonalEmail = c.PersonalEmailAddress,
                OfficalEmail = c.OfficeEmailAddress,
                Gender = c.HRMS_Setup_Gender.GenderTitle,
                GenderId = c.GenderId,
                EmployeeType = c.Setup_EmployeeType.TypeName,
                IsActive = c.IsActive,
                Company = c.Setup_Location.Setup_Company.CompanyName,
                Location = c.Setup_Location.LocationName,
                LocationId = c.LocationId,
                CostCenterId = c.CostCenterId,
                MaritalStatusId = c.MatrialStatusId,
                BuisnessUnitId = c.Setup_Department.BusinessUnitId,
                ReligionId = c.ReligionId,
                EmployeeImage = c.PictureName,
                Extension = c.Extension,
                PhoneNumber = c.Phone,
                MobileNumber = c.OfficialMobileNumber,
                IsAllowInterest = c.IsAllowInterest,
                OpeningBalance = c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance == null ? 0 :
                c.HCM_ProvidentFund.Where(a => a.IsActive == true).OrderByDescending(b => b.ProvidentFundId).FirstOrDefault().TotalBalance,
                Salary = c.HCM_EmployeeSalary.FirstOrDefault(x => x.IsActive == true && x.IsIncrement == false).GrossSalary,
                //InterestStandard = c.Setup_Company.HCM_Company_Settings.FirstOrDefault(x => x.IsActive == true && x.SetupID == InterestSetting).Value,
                Balance = c.HCM_ProvidentFund.Where(x => x.IsActive == true).OrderBy(b => b.ProvidentFundId).FirstOrDefault().TotalBalance,
                EmployeeIdSettlement = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().EmployeeId == null ? IntNull :
                c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().EmployeeId,
                LoanBalance = context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance) == null ? 0 : context.HCM_Loan_Detail.Where(a => a.IsActive == true && a.HCM_Loan_Master.EmployeeId == c.EmployeeId && a.HCM_Loan_Master.IsSettled == false)
                .OrderByDescending(a => a.LoadDetailId).Take(1).Sum(a => a.Balance),
                TotalArrearAmount = c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount) == null ? 0 :
                c.HCM_ArrearHistory.Where(a => a.IsActive == true && a.IsDispersed == false && a.EmployeeId == c.EmployeeId).Sum(b => b.ArrearAmount),
                IsSettled = c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().IsSettled == null ? 0 :
                c.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == c.EmployeeId).FirstOrDefault().IsSettled == false ? 1 : 2,

                SESSILimit = context.HCM_EmployeeSESSI_Details.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == c.EmployeeId) == null ? 0 :
                context.HCM_EmployeeSESSI_Details.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == c.EmployeeId).SESSIAmount,




            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string multimanage_Wppf(string JSon)
    {
        List<HCM_WPPF> ResponseDetails = (List<HCM_WPPF>)CommonHelper.Deserialize(JSon, typeof(List<HCM_WPPF>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int i = 0;
                bool IsFlag = false;
                int? IntNull = null;

                foreach (HCM_WPPF obj in ResponseDetails)
                {
                    IsFlag = true;
                    var objCheck = context.HCM_WPPF.Where(a => a.IsActive == true && a.EmployeeId == obj.EmployeeId && a.YearId == obj.YearId).ToList();

                    if (objCheck != null && objCheck.Count > 0)
                    {
                        objCheck.ForEach(m => m.IsActive = false);
                        objCheck.ForEach(m => m.ModifiedBy = objBase.UserKey);
                        objCheck.ForEach(m => m.ModifiedDate = DateTime.Now);
                        context.SaveChanges();

                        //IsFlag = true;
                        //i++;
                    }

                    if (IsFlag)
                    {
                        var _obj = new HCM_WPPF();
                        _obj.EmployeeId = obj.EmployeeId;
                        _obj.CompanyId = obj.CompanyId;
                        _obj.YearId = obj.YearId;
                        _obj.Slab_Id = obj.Slab_Id;
                        _obj.UnitRate = obj.UnitRate;
                        _obj.MaxUnitRate = obj.MaxUnitRate;
                        _obj.InterestRate = obj.InterestRate;
                        _obj.MaxInterestRate = obj.MaxInterestRate;
                        _obj.UnitRateAmount = obj.UnitRateAmount;
                        _obj.InterestRateAmount = obj.InterestRateAmount;
                        _obj.Total_WPPF = obj.Total_WPPF;
                        _obj.CreatedDate = DateTime.Now;
                        _obj.CreatedBy = objBase.UserKey;
                        //_obj.UserIP = objBase.UserIP;
                        _obj.IsActive = true;

                        context.HCM_WPPF.Add(_obj);

                        //var _obj = new HCM_WPPF();
                        //_obj.EmployeeId = obj.EmployeeId;
                        //_obj.CompanyId = obj.CompanyId;
                        //_obj.YearId = objCheck[i].YearId;
                        //_obj.Slab_Id = objCheck[i].Slab_Id;
                        //_obj.UnitRate = obj.UnitRate;
                        //_obj.MaxUnitRate = objCheck[i].MaxUnitRate;
                        //_obj.InterestRate = objCheck[i].InterestRate;
                        //_obj.MaxInterestRate = objCheck[i].MaxInterestRate;
                        //_obj.UnitRateAmount = objCheck[i].UnitRateAmount;
                        //_obj.InterestRateAmount = objCheck[i].InterestRateAmount;
                        //_obj.Total_WPPF = objCheck[i].Total_WPPF;
                        //_obj.CreatedDate = DateTime.Now;
                        //_obj.CreatedBy = objBase.UserKey;
                        ////_obj.UserIP = objBase.UserIP;
                        //_obj.IsActive = true;

                        //context.HCM_WPPF.Add(_obj);
                    }
                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string getEmployeeSettlement(int EmployeeId, int CompanyId/*, string Lastday*/)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Final_Seperation_Module", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@emp_id", SqlDbType.Int).Value = EmployeeId;
            da.SelectCommand.Parameters.Add("@companyId", SqlDbType.Int).Value = CompanyId;
            //da.SelectCommand.Parameters.Add("@Lastday", SqlDbType.DateTime).Value = Convert.ToDateTime( Lastday);
            //da.SelectCommand.Parameters.Add("@MkT_Value", SqlDbType.Int).Value = EmployeeId;
            //da.SelectCommand.Parameters.Add("@purchaseCar", SqlDbType.Bit).Value = EmployeeId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getNoticePeriodType()
    {
        try
        {
            var List = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.SetupMasterID == (int)Constant.HCMSetupMaster.NoticePeriodType).Select(s => new
            {
                Value = s.ColumnValue,
                Id = s.CompanyID
            }).ToList();
            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getGroupInsuranceType()
    {
        try
        {
            var List = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.SetupMasterID == (int)Constant.HCMSetupMaster.GroupInsuranceType).Select(s => new
            {
                Value = s.ColumnValue,
                Id = s.CompanyID
            }).ToList();
            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string saveEmployeeFinalSettlement(string SettlementId, string PFAmount, string LoanBalanceAmount, string ArrearAmount, string LeavesRemaining, string LeaveEncashment,
        string CarMktValue, string CarTotalValue, string CarTotalPaid, string OtherPayable, string OtherDesc, string GroupInsTypeId, string GroupInsAmount, string BonusRecovery,
        string PfChequeNumber, string PfChequeDate, string SettlementChequeNumber, string SettlementChequeDate, string EmployeePayableAmount, string EmployeePayableChequeNumber,
        string EmployeePayableChequeDate, string Final_Amount, string ResignedDate, string LastWorkingDate, string Notice_PeriodType, string EmployeeId, string IsCarTakeHome,
        string IsSettled, string BasicSalary, string StopSalaryDate, string DeductedAmount)
    {
        try
        {

            int? IntNull = null;
            DateTime? DateTimeNull = null;
            int _SettlementId = Convert.ToInt32(SettlementId);
            int EmpId = Convert.ToInt32(EmployeeId);

            var _obj = context.HCM_EmployeeSettlement.FirstOrDefault(x => x.IsActive == true && x.SettlementId == _SettlementId && x.IsSettled == false && x.EmployeeId == EmpId);

            if (_obj != null)
            {
                var objFinal = context.HCM_Final_Settlement.FirstOrDefault(x => x.IsActive == true && x.EmployeeID == EmpId);

                if (objFinal != null)
                {
                    objFinal.IsActive = false;
                    objFinal.ModifiedBy = objBase.UserId;
                    objFinal.ModifiedDate = DateTime.Now;
                    context.SaveChanges();
                }

                HCM_Final_Settlement obj = new HCM_Final_Settlement();

                obj.PF_Amount = (PFAmount == "0" || PFAmount == string.Empty) ? 0 : Convert.ToDouble(PFAmount);
                obj.Arrears = (ArrearAmount == "0" || ArrearAmount == string.Empty) ? 0 : Convert.ToDouble(ArrearAmount);
                obj.Leave_Encashment = (LeaveEncashment == "0" || LeaveEncashment == string.Empty) ? 0 : Convert.ToDouble(LeaveEncashment);
                obj.Car_MarketValue = (CarMktValue == "0" || CarMktValue == string.Empty) ? 0 : Convert.ToDouble(CarMktValue);
                obj.Car_Total_Paid = (CarTotalPaid == "0" || CarTotalPaid == string.Empty) ? 0 : Convert.ToDouble(CarTotalPaid);
                obj.Car_Payable = (CarTotalValue == "0" || CarTotalValue == string.Empty) ? 0 : Convert.ToDouble(CarTotalValue);
                obj.Other_Payable = (OtherPayable == "0" || OtherPayable == string.Empty) ? 0 : Convert.ToDouble(OtherPayable);
                obj.Other_discrip = OtherDesc;
                obj.Group_InsuranceAmount = (GroupInsAmount == "0" || GroupInsAmount == string.Empty) ? 0 : Convert.ToDouble(GroupInsAmount);
                obj.Insurance_type = (GroupInsTypeId == "0" || GroupInsTypeId == string.Empty) ? IntNull : Convert.ToInt32(GroupInsTypeId);
                obj.Notice_PeriodType = (Notice_PeriodType == "0" || Notice_PeriodType == string.Empty) ? IntNull : Convert.ToInt32(Notice_PeriodType);
                obj.Bonus_Recovery = (BonusRecovery == "0" || BonusRecovery == string.Empty) ? 0 : Convert.ToDouble(BonusRecovery);

                obj.PF_CheckNumber = PfChequeNumber;
                obj.PF_ChequeDate = PfChequeDate == string.Empty ? DateTimeNull : Convert.ToDateTime(PfChequeDate);
                obj.Final_SettlementChequeDate = SettlementChequeDate == string.Empty ? DateTimeNull : Convert.ToDateTime(SettlementChequeDate);
                obj.Settlement_ChequeNumber = SettlementChequeNumber;
                obj.Final_Amount = (Final_Amount == "0" || Final_Amount == string.Empty) ? 0 : Convert.ToDouble(Final_Amount);

                obj.Employee_PayableAmount = (EmployeePayableAmount == "0" || EmployeePayableAmount == string.Empty) ? 0 : Convert.ToDouble(EmployeePayableAmount);
                obj.Emp_PayableChequeNumber = EmployeePayableChequeNumber;
                obj.Emp_PayableChequeDate = EmployeePayableChequeDate == string.Empty ? DateTimeNull : Convert.ToDateTime(EmployeePayableChequeDate);

                obj.CreatedBy = objBase.UserKey;
                obj.CreatedDate = DateTime.Now;
                obj.UserIP = objBase.UserIP;
                obj.IsActive = true;

                obj.ResignDate = ResignedDate == string.Empty ? DateTimeNull : Convert.ToDateTime(ResignedDate);
                obj.LastWorkingDate = LastWorkingDate == string.Empty ? DateTimeNull : Convert.ToDateTime(LastWorkingDate);
                obj.EmployeeID = Convert.ToInt32(EmployeeId);
                obj.TotalLeaves = (LeavesRemaining == "0" || LeavesRemaining == string.Empty) ? 0 : Convert.ToInt32(LeavesRemaining);
                obj.LoanBalanceAmount = (LoanBalanceAmount == "0" || LoanBalanceAmount == string.Empty) ? 0 : Convert.ToDouble(LoanBalanceAmount);
                //obj.IsCarTakeHome = (IsCarTakeHome == "0" || IsCarTakeHome == string.Empty) ? false : Convert.ToBoolean(IsCarTakeHome );
                obj.IsCarTakeHome = Convert.ToBoolean(IsCarTakeHome);
                obj.BasicSalary = (BasicSalary == "0" || BasicSalary == string.Empty) ? 0 : Convert.ToDouble(BasicSalary);
                obj.Final_Amount = (Final_Amount == "0" || Final_Amount == string.Empty) ? 0 : Convert.ToDouble(Final_Amount);
                obj.Salary_StopDate = StopSalaryDate == string.Empty ? DateTimeNull : Convert.ToDateTime(StopSalaryDate);
                obj.DeductedAmount = (DeductedAmount == "0" || DeductedAmount == string.Empty) ? 0 : Convert.ToDouble(DeductedAmount);

                context.HCM_Final_Settlement.Add(obj);
                context.SaveChanges();

                _obj.IsSettled = Convert.ToBoolean(IsSettled);
                _obj.ModifiedBy = objBase.UserId;
                _obj.ModifiedDate = DateTime.Now;
                context.SaveChanges();

                var objEmp = context.Setup_Employee.FirstOrDefault(a => a.IsActive == true && a.EmployeeId == EmpId);

                if (objEmp != null)
                {
                    objEmp.LastworkingDate = LastWorkingDate == string.Empty ? DateTimeNull : Convert.ToDateTime(LastWorkingDate);
                    objEmp.ResignedDate = ResignedDate == string.Empty ? DateTimeNull : Convert.ToDateTime(ResignedDate);
                    objEmp.ModifiedBy = objBase.UserId;
                    objEmp.ModifiedDate = DateTime.Now;
                    context.SaveChanges();
                }
            }

            return "1";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [OperationContract]
    public string SaveResignInfoDates(string EmployeeId, string StopSalaryDate, string ResignedDate, string LastWorkingDate)
    {
        try
        {
            DateTime? DateTimeNull = null;
            int EmpId = Convert.ToInt32(EmployeeId);

            var objEmp = context.Setup_Employee.FirstOrDefault(a => a.IsActive == true && a.EmployeeId == EmpId);

            if (objEmp != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable DatatobjEmp = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(EmpId), "Setup_Employee", 2);
                #endregion

                objEmp.LastworkingDate = LastWorkingDate == string.Empty ? DateTimeNull : Convert.ToDateTime(LastWorkingDate);
                objEmp.ResignedDate = ResignedDate == string.Empty ? DateTimeNull : Convert.ToDateTime(ResignedDate);
                if (objEmp.ResignedDate != null) { objEmp.IsFinalSettlement = false; }
                objEmp.ModifiedBy = objBase.UserId;
                objEmp.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }

            var _obj = context.HCM_EmployeeSettlement.FirstOrDefault(x => x.IsActive == true && x.IsSettled == false && x.EmployeeId == EmpId);

            if (_obj != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat_obj = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(_obj.SettlementId), "HCM_EmployeeSettlement", 2);
                #endregion

                _obj.ResignDate = Convert.ToDateTime(ResignedDate);
                _obj.IsActive = true;
                _obj.ModifiedBy = objBase.UserId;
                _obj.ModifiedDate = DateTime.Now;

                context.SaveChanges();
            }
            else
            {
                HCM_EmployeeSettlement obj = new HCM_EmployeeSettlement();

                obj.EmployeeId = EmpId;
                obj.ResignDate = Convert.ToDateTime(ResignedDate);
                obj.IsSettled = false;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = objBase.UserId;
                obj.IsActive = true;

                context.HCM_EmployeeSettlement.Add(obj);
                context.SaveChanges();
            }

            var objFinal = context.HCM_Final_Settlement.FirstOrDefault(x => x.IsActive == true && x.EmployeeID == EmpId);

            if (objFinal != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable DatatobjFinal = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(objFinal.F_SettlementID), "HCM_Final_Settlement", 2);
                #endregion

                objFinal.IsActive = false;
                objFinal.ModifiedBy = objBase.UserId;
                objFinal.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }

            HCM_Final_Settlement _objFinal = new HCM_Final_Settlement();

            _objFinal.ResignDate = ResignedDate == string.Empty ? DateTimeNull : Convert.ToDateTime(ResignedDate);
            _objFinal.LastWorkingDate = LastWorkingDate == string.Empty ? DateTimeNull : Convert.ToDateTime(LastWorkingDate);
            _objFinal.Salary_StopDate = StopSalaryDate == string.Empty ? DateTimeNull : Convert.ToDateTime(StopSalaryDate);
            _objFinal.CreatedBy = objBase.UserKey;
            _objFinal.CreatedDate = DateTime.Now;
            _objFinal.UserIP = objBase.UserIP;
            _objFinal.IsActive = true;
            _objFinal.EmployeeID = EmpId;

            context.HCM_Final_Settlement.Add(_objFinal);
            context.SaveChanges();

            return "1";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [OperationContract]
    public string RevokeResignDates(string EmployeeId)
    {
        ResponseHelper _objResponse = new ResponseHelper();
        try
        {


            DateTime? DateTimeNull = null;
            int EmpId = Convert.ToInt32(EmployeeId);

            var objEmp = context.Setup_Employee.FirstOrDefault(a => a.IsActive == true && a.EmployeeId == EmpId && a.ResignedDate != null);

            if (objEmp != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(EmpId), "Setup_Employee", 2);
                #endregion

                objEmp.LastworkingDate = null;
                objEmp.ResignedDate = null;

                objEmp.ModifiedBy = objBase.UserId;
                objEmp.ModifiedDate = DateTime.Now;
                context.SaveChanges();


                var _obj = context.HCM_EmployeeSettlement.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmpId);

                if (_obj != null)
                {
                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(_obj.SettlementId), "HCM_EmployeeSettlement", 3);
                    #endregion

                    _obj.IsActive = false;
                    _obj.ModifiedBy = objBase.UserId;
                    _obj.ModifiedDate = DateTime.Now;

                    context.SaveChanges();
                }



                var objFinal = context.HCM_Final_Settlement.FirstOrDefault(x => x.IsActive == true && x.EmployeeID == EmpId);

                if (objFinal != null)
                {
                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(objFinal.F_SettlementID), "HCM_Final_Settlement", 3);
                    #endregion

                    objFinal.IsActive = false;
                    objFinal.ModifiedBy = objBase.UserId;
                    objFinal.ModifiedDate = DateTime.Now;
                    context.SaveChanges();
                }

                _objResponse.ResponseMessage = "Final Settlement Revoked Successfully.";
                _objResponse.ResponseMessageType = Constant.ResponseType.SUCCESS;
            }
            else
            {
                _objResponse.ResponseMessage = "Employee Not Found For Revoke Final Settlement.";
                _objResponse.ResponseMessageType = Constant.ResponseType.WARNING;

            }
        }
        catch (Exception ex)
        {
            _objResponse.ResponseMessage = ex.Message;
            _objResponse.ResponseMessageType = Constant.ResponseType.ERROR;
        }
        var JSON = JsonConvert.SerializeObject(_objResponse);
        return JSON;
    }

    [OperationContract]
    public string getFinalSettlement(int EmployeeId)
    {
        try
        {
            var List = context.HCM_Final_Settlement.Where(a => a.IsActive == true && a.EmployeeID == EmployeeId)
                .AsEnumerable()
                    .Select(s => new
                    {
                        BasicSalary = s.BasicSalary,
                        StopSalaryDate = s.Salary_StopDate.Value.Date.ToString("MM/dd/yyyy"),
                        ResignedDate = s.ResignDate.Value.Date.ToString("MM/dd/yyyy"),
                        LastDate = s.LastWorkingDate.Value.Date.ToString("MM/dd/yyyy"),
                        NoticePeriodTypeId = s.Notice_PeriodType,
                        CarTakeHome = s.IsCarTakeHome,
                        CarMarketValue = s.Car_MarketValue,
                        PfAmount = s.PF_Amount,
                        LoanBalanceAmount = s.LoanBalanceAmount,
                        TotalArrearAmount = s.Arrears,
                        TotalLeaves = s.TotalLeaves,
                        LeaveEncashment = s.Leave_Encashment,
                        CarTotalPaid = s.Car_Total_Paid,
                        CarTotalPayable = s.Car_Payable,
                        OtherPayable = s.Other_Payable,
                        OtherDesc = s.Other_discrip,
                        GroupInsTypeId = s.Insurance_type,
                        GroupInsAmount = s.Group_InsuranceAmount,
                        BonusRecovery = s.Bonus_Recovery,
                        PfChequeNumber = s.PF_CheckNumber,
                        PfChequeDate = s.PF_ChequeDate,
                        SettlementChequeNumber = s.Settlement_ChequeNumber,
                        SettlementChequeDate = s.Final_SettlementChequeDate,
                        EmployeePayableAmount = s.Employee_PayableAmount,
                        EmployeePayableChequeNumber = s.Emp_PayableChequeNumber,
                        EmployeePayableChequeDate = s.Emp_PayableChequeDate,
                        FinalAmount = s.Final_Amount,
                        DeductedAmount = s.DeductedAmount == null ? 0 : s.DeductedAmount,
                        IsSettled = context.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId).FirstOrDefault().IsSettled == null ? false :
                    context.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId).FirstOrDefault().IsSettled,
                        SettlementId = context.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId).FirstOrDefault().SettlementId == null ? 0 :
                    context.HCM_EmployeeSettlement.Where(a => a.IsActive == true && a.EmployeeId == EmployeeId).FirstOrDefault().SettlementId,
                    })

            .ToList();
            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getReportPfLoanClosingBalance(int CompanyId, string Month, int? EmployeeId)
    {
        try
        {
            int? IntNull = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_LOANCLOSING", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = Convert.ToDateTime(Month);
            da.SelectCommand.Parameters.Add("@empid", SqlDbType.Int).Value = (EmployeeId == null || EmployeeId == 0) ? IntNull : EmployeeId;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_PfYearlyMonthlyClosingData(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_YearlyMonthlyContirbution_Closingfinal", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            //da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            //da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            //da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            //da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            //da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            //da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            //da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            //da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            //da.SelectCommand.Parameters.Add("@empid", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;

        }
    }

    [OperationContract]
    public string multimanage_SaveInterestIncome(string JSon)
    {
        List<DAL.HCM_InterestIncome> ResponseDetails = (List<DAL.HCM_InterestIncome>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_InterestIncome>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (DAL.HCM_InterestIncome obj in ResponseDetails)
                {
                    var objCheck = context.HCM_InterestIncome.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == obj.EmployeeId && x.YearOf == obj.YearOf);
                    if (objCheck != null)
                    {
                        objCheck.IsActive = false;
                        objCheck.ModifiedBy = objBase.UserKey;
                        objCheck.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                    }

                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = objBase.UserKey;
                    context.HCM_InterestIncome.Add(obj);
                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string report_ProvidentFundStatement(int CompanyId, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("PFUND_STATEMENT", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);

            da.Fill(dt);

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string GetSalaryMonth(int CompanyId)
    {
        try
        {
            var List = context.HCM_Payroll_Log.Where(a => a.IsActive == true && a.IsLocked == false && a.CompanyId == CompanyId)
                .AsEnumerable()
                .Select(a => new
                {
                    Id = a.PayrollLogId,
                    Value = a.PayrollDate.ToString(Constant.DateFormat)
                })
                .ToList();

            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string GetBonusTransactionMaster(int CompanyId)
    {
        try
        {
            var List = context.HCM_Bonus.Where(a => a.IsActive == true && a.IsRelease == false && a.HCM_CompanyFormula.HCM_Setup_Allowance.CompanyId == CompanyId)
                 .AsEnumerable()
                .Select(a => new
                {
                    Id = a.BonusId,
                    Value = a.ReleaseDate == null ? "" : Convert.ToDateTime(a.ReleaseDate).ToString("yyyy-MM-dd") + "_" + a.HCM_CompanyFormula.HCM_Setup_Allowance.AllowanceName
                })

               .ToList();

            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string GetAllBonusTransactionMaster(int CompanyId)
    {
        try
        {
            var List = context.HCM_Bonus.Where(a => a.IsActive == true && a.HCM_CompanyFormula.HCM_Setup_Allowance.CompanyId == CompanyId)
                 .AsEnumerable()
                .Select(a => new
                {
                    Id = a.BonusId,
                    Value = a.ReleaseDate == null ? "" : Convert.ToDateTime(a.ReleaseDate).ToString("yyyy-MM-dd") + "_" + a.HCM_CompanyFormula.HCM_Setup_Allowance.AllowanceName
                })

               .ToList();

            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string MergeSalaryBonus(int CompanyId, string PayrolDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Bonus_Merge", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@companyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@DateOfPayroll", SqlDbType.DateTime).Value = DateTime.Parse(PayrolDate);

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_JV(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, bool IsSummary)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_JV", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@PayDate", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@IsSummary", SqlDbType.Bit).Value = IsSummary;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            return "";
        }
    }

    [OperationContract]
    public string report_JV_Bonus(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth, bool IsSummary)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_JV_Bonus", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@PayDate", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@IsSummary", SqlDbType.Bit).Value = IsSummary;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            return "";
        }
    }

    [OperationContract]
    public string report_LoanJV(int CompanyId, string PayrolDate)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_JV_StaffLoan", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = DateTime.Parse(PayrolDate);

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string PayrollTransForSeprateLock(int CompanyId)
    {
        try
        {
            var lst = context.HCM_Payroll_Log.Where(a => a.IsActive == true && a.CompanyId == CompanyId)
                .AsEnumerable()
                .Select(a => new
                {
                    PayrollDate = a.PayrollDate.Date.ToString(Constant.DateFormat),
                    PayrollStatus = a.IsLocked == false ? "Unlocked" : "Locked",
                    IsLocked = a.IsLocked,
                    PayrollLogId = a.PayrollLogId,
                })
                .OrderByDescending(a => a.PayrollLogId)
                .ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string PayrollLockSeparate(int PayrollLogId/*, int CompanyId*/)
    {
        try
        {
            //var obj = context.HCM_Payroll_Log.FirstOrDefault(x => x.IsActive == true && x.PayrollLogId == PayrollLogId);

            //UpdatePerformanceBonus(PayrollLogId);
            //UpdateEmployeeIncrementedSalary(PayrollLogId);

            //obj.IsLocked = true;
            //obj.Modifiedby = objBase.UserKey;
            //obj.ModifiedDate = DateTime.Now;
            //context.SaveChanges();

            LockPayroll(PayrollLogId);

            return "1";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    private void LockPayroll(int PayrollLogId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_Payroll_Lock", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = ConnectionTimeout;
        da.SelectCommand.Parameters.Add("@PayrollLogId", SqlDbType.Int).Value = PayrollLogId;
        da.Fill(dt);
    }

    private void UpdatePerformanceBonus(int PayrollLogId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_Update_PerformanceBonus", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = ConnectionTimeout;
        da.SelectCommand.Parameters.Add("@PayrollLogId", SqlDbType.Int).Value = PayrollLogId;
        da.Fill(dt);
    }

    private void UpdateEmployeeIncrementedSalary(int PayrollLogId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_Update_EmployeeSalary_Incremented", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = ConnectionTimeout;
        da.SelectCommand.Parameters.Add("@PayrollLogId", SqlDbType.Int).Value = PayrollLogId;
        da.Fill(dt);

    }

    [OperationContract]
    public string IncrementedEmployeesSalaryForcast(int CompanyId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("GET_PendingEmployeeIncrements", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string SalaryForcasterIncrement(int CompanyId, int EmpId, int UserId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeIncrementedSalaryForcaster", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmpId;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            da.Fill(dt);

            if (Convert.ToString(dt.Rows[0]["MSG"]) == "1")
            {
                return "1";
            }
            else
            {
                if (Convert.ToString(dt.Rows[0]["MSG"]) == "0")
                {
                    return "0";
                }
            }

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string multimanage_TaxRefund(string JSon, int YearId)
    {
        List<HCM_TaxRefund> ResponseDetails = (List<HCM_TaxRefund>)CommonHelper.Deserialize(JSon, typeof(List<HCM_TaxRefund>));
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int i = 0;
                bool IsFlag = false;

                foreach (HCM_TaxRefund obj in ResponseDetails)
                {
                    var objCheck = context.HCM_TaxRefund.Where(a => a.IsActive == true && a.EmployeeId == obj.EmployeeId && a.YearId == YearId && a.CompanyId == obj.CompanyId).ToList();

                    if (objCheck != null /*&& objCheck.Count > 0*/)
                    {
                        objCheck.ForEach(m => m.IsActive = false);
                        context.SaveChanges();

                        IsFlag = true;

                    }

                    if (IsFlag)
                    {
                        if (obj.RefundAmount > 0)
                        {
                            var _obj = new HCM_TaxRefund();
                            _obj.EmployeeId = obj.EmployeeId;
                            _obj.RefundAmount = obj.RefundAmount;
                            _obj.YearId = YearId;
                            _obj.CompanyId = obj.CompanyId;
                            _obj.CreatedDate = DateTime.Now;
                            _obj.CreatedBy = objBase.UserKey;
                            _obj.IsActive = true;

                            context.HCM_TaxRefund.Add(_obj);
                            context.SaveChanges();
                        }
                    }

                }
                scope.Complete();
            }
            context.SaveChanges();
            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string GetTaxDetailsForTaxUpdate(int CompanyId, int EmpId, int PayrollLogId)
    {
        try
        {
            //int TaxForcastId = 0;
            //int YearId = context.HCM_Setup_Year.Where(x => x.IsCurrentActiveYear == true && x.IsActive == true && x.CompanyId == CompanyId).FirstOrDefault().YearId;
            //var LstTaxForcastId = context.HCM_EmployeeTaxForecast.Where(a => a.IsActive == true && a.YearId == YearId && a.EmployeeId == EmpId).ToList();
            //TaxForcastId = LstTaxForcastId[0].TaxForecastId;


            //var Lst = context.HCM_EmployeeTaxDetails.Where(a => a.IsActive == true && a.TaxForecastId == TaxForcastId && a.PayrollLogId == PayrollLogId)
            //    .Select(a => new
            //    {
            //        TaxPaid = a.TaxPaid,
            //        TaxBalance = a.Balance,
            //        EmployeeTaxId = a.EmployeeTaxId,
            //    })
            //    .ToList();


            var Lst = context.HCM_EmployeeTaxDetails.Where(a => a.IsActive == true && a.PayrollLogId == PayrollLogId && a.HCM_EmployeeTaxForecast.EmployeeId == EmpId && a.HCM_EmployeeTaxForecast.IsActive == true)
               .Select(a => new
               {
                   TaxPaid = a.CurrentMonthTax,
                   TaxBalance = a.Balance,
                   EmployeeTaxId = a.EmployeeTaxId,
               }).ToList();
            var JSON = JsonConvert.SerializeObject(Lst);
            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string SaveTaxDeduction(int EmployeeTaxId, float NewTaxPaid)
    {
        try
        {
            var lst = context.HCM_EmployeeTaxDetails.FirstOrDefault(x => x.IsActive == true && x.EmployeeTaxId == EmployeeTaxId);
            // var Lst = context.HCM_EmployeeTaxDetails.FirstOrDefault(x => x.IsActive == true && x.EmployeeTaxId == EmployeeTaxId && x.HCM_EmployeeTaxForecast.IsActive == true);
            if (lst != null)
            {
                int _EmployeeId = lst.HCM_EmployeeTaxForecast.EmployeeId;
                int? _PayrollLogId = lst.PayrollLogId;
                if (_EmployeeId > 0 && _PayrollLogId > 0)
                {
                    var li_payrollMaster = context.HCM_Payroll_Master.FirstOrDefault(a => a.IsActive == true && a.PayrollLogId == _PayrollLogId && a.EmployeeId == _EmployeeId);
                    if (li_payrollMaster != null)
                    {
                        double CurrentBalance = Convert.ToDouble(lst.Balance);
                        double Diff = ((lst.CurrentMonthTax == null ? 0 : Convert.ToDouble(lst.CurrentMonthTax)) - NewTaxPaid);
                        double NewBalance = Convert.ToDouble(lst.Balance) + Diff;
                        lst.CurrentMonthTax = NewTaxPaid;
                        lst.Balance = NewBalance;
                        /*
                        double Diff = Convert.ToDouble(NewTaxPaid) - lst.TaxPaid;
                        double NewBalance = Convert.ToDouble(lst.Balance == null ? 0 : lst.Balance) - Diff;
                        lst.TaxPaid = NewTaxPaid;
                        lst.Balance = NewBalance;
                        */
                        lst.ModifiedBy = objBase.UserKey;
                        lst.ModifiedDate = DateTime.Now;

                        li_payrollMaster.OtherDeduction = (li_payrollMaster.OtherDeduction == null ? 0 : li_payrollMaster.OtherDeduction) - (Diff);
                        li_payrollMaster.ModifiedBy = objBase.UserKey;
                        li_payrollMaster.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "Data not found";
            }
        }
        catch (Exception e)
        {
            //var JSON = JsonConvert.SerializeObject(e.ToString());
            //return JSON;
            return "Error occurred while adding tax information";
        }
    }

    [OperationContract]
    public string GetCarDetailsForCarDeductionUpdate(int CompanyId, int EmpId, int PayrollLogId)
    {
        try
        {
            var lst = context.HCM_Vehicle_Detail.Where(a => a.IsActive == true && a.HCM_Vehicle_Master.EmployeeId == EmpId && a.PayrollLogId == PayrollLogId) //&& a.HCM_Vehicle_Master.IsActive == true && a.HCM_Vehicle_Master.IsCompleted == false
                .Select(a => new
                {
                    VehicleMasterId = a.VehicleMasterId,
                    Balance = a.Balance,
                    //InstallmentAmount = a.InstallmentAmount, Old
                    InstallmentAmount = a.InstallmentAmount,
                    VehicleDetailId = a.VehicleDetailId,
                })
                .ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;

        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string SaveInstallmentDeduction(int VehicleDetailId, float NewInstallmentAmount)
    {
        try
        {
            var lst = context.HCM_Vehicle_Detail.FirstOrDefault(x => x.IsActive == true && x.VehicleDetailId == VehicleDetailId);

            if (lst != null)
            {
                double Diff = 0;
                double NewBalance = 0;

                double CurrInstallment = Convert.ToDouble(lst.InstallmentAmount == null ? 0 : lst.InstallmentAmount);
                Diff = Convert.ToDouble(NewInstallmentAmount) - CurrInstallment;

                if (NewInstallmentAmount > Convert.ToDouble(CurrInstallment))
                {
                    NewBalance = Convert.ToDouble(lst.Balance == null ? 0 : lst.Balance) - Diff;
                }
                else if (NewInstallmentAmount < Convert.ToDouble(CurrInstallment))
                {
                    Diff = Diff * (-1);
                    NewBalance = Convert.ToDouble(lst.Balance == null ? 0 : lst.Balance) + Diff;
                }

                //double Diff = Convert.ToDouble(NewTaxPaid) - lst.TaxPaid;
                //double NewBalance = Convert.ToDouble(lst.Balance == null ? 0 : lst.Balance) - Diff;

                lst.InstallmentAmount = NewInstallmentAmount;
                lst.Balance = NewBalance;
                lst.ModifiedBy = objBase.UserKey;
                lst.ModifiedDate = DateTime.Now;

                context.SaveChanges();
            }

            return "1";
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_Increment(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable DT = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_IncrementList", con);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@Return", SqlDbType.Bit).Value = true;

            da.Fill(dt);

            SqlDataAdapter da1 = new SqlDataAdapter("HCM_RPT_IncrementSummary", con);

            da1.SelectCommand.CommandType = CommandType.StoredProcedure;
            da1.SelectCommand.CommandTimeout = ConnectionTimeout;
            da1.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da1.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);

            da1.Fill(DT);

            var JSON = JsonConvert.SerializeObject(dt) + "#SPLIT#" + JsonConvert.SerializeObject(DT);
            return JSON;

        }
        catch (Exception e)
        {
            //var JSON = JsonConvert.SerializeObject(e.ToString());
            //return JSON;
            return "";
        }
    }

    [OperationContract]
    public string report_Arrear(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_ArrearList", con);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);

            da.Fill(dt);

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;

        }
        catch (Exception e)
        {
            //var JSON = JsonConvert.SerializeObject(e.ToString());
            //return JSON;
            return "";
        }
    }


    /********** VEHICLE NEW WORK AFTER STRUCTURE CHANGE **********/

    [OperationContract]
    public string GetVehicleType()
    {
        string json = GetSetupDetailByMasterId((int)Constant.HCMSetupMaster.VehicleType, 0);

        return json;
    }

    [OperationContract]
    public string GetManufacturer(int VehicleTypeId)
    {
        string json = GetSetupDetailByMasterId((int)Constant.HCMSetupMaster.Manufacturer, VehicleTypeId);

        return json;
    }

    [OperationContract]
    public string GetVehicleName(int ManufacturerId)
    {
        string json = GetSetupDetailByMasterId((int)Constant.HCMSetupMaster.VehicleName, ManufacturerId);

        return json;
    }

    [OperationContract]
    public string GetVehicleVariant(int VehicleId)
    {
        string json = GetSetupDetailByMasterId((int)Constant.HCMSetupMaster.VehicleVariant, VehicleId);

        return json;
    }

    public string GetSetupDetailByMasterId(int SetupMasterId, int ParentId)
    {
        try
        {

            int? _ParentId = ParentId == 0 ? IntNull : ParentId;



            var List = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.SetupMasterID == SetupMasterId &&
                (_ParentId == null ? true : a.ParentId == _ParentId)
                ).Select(s => new
                {
                    Value = s.ColumnValue,
                    Id = s.SetupDetailID,
                    level2 = s.HCM_Setup_Detail2.ColumnValue,
                    level1 = s.HCM_Setup_Detail2.HCM_Setup_Detail2.ColumnValue,
                    level3 = s.HCM_Setup_Detail2.HCM_Setup_Detail2.HCM_Setup_Detail2.ColumnValue,


                    ParentId = s.ParentId,
                    ParentName = context.HCM_Setup_Detail.FirstOrDefault(x => x.SetupDetailID == s.ParentId).ColumnValue
                }).ToList();
            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string IncreaseRMPercentage(double Percent)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Increase_RM", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@Percentage", SqlDbType.Float).Value = Percent;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = Convert.ToInt32(objBase.UserKey);

            da.Fill(dt);
            var JSON = "1";
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string GetVehicleInfoByVehicleType(int VehicleTypeId)
    {
        try
        {

            var lst = context.HCM_VehicleInformation.Where(a => a.IsActive == true && a.VehicleTypeId == VehicleTypeId)
                .Select(a => new
                {
                    Id = a.VehicleInformationId,
                    Value = a.HCM_Setup_Detail.ColumnValue + " " + a.HCM_Setup_Detail1.ColumnValue + " " + a.HCM_Setup_Detail2.ColumnValue /*+ " " + a.HCM_Setup_Detail3.ColumnValue + " "*/,
                })
                .ToList();


            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }

    }

    [OperationContract]
    public string GetDesignationVehicleMappingNew(int DesignationId, bool Upgrade)
    {
        try
        {
            var lst = context.HCM_VehicleDesignationMapping.Where(x => x.IsActive == true && x.IsUpgradeVehicle == Upgrade && x.DesignationId == DesignationId && x.VehicleInformationId != null)
                .Select(x => new
                {

                    VehicleInfoId = x.VehicleInformationId,
                    VehicleDesignationMappingId = x.VehicleDesignatiionMappingId,
                    Vehicle = x.HCM_VehicleInformation.HCM_Setup_Detail.ColumnValue + " " + x.HCM_VehicleInformation.HCM_Setup_Detail1.ColumnValue + " " + x.HCM_VehicleInformation.HCM_Setup_Detail2.ColumnValue,

                    //x.VehicleDesignatiionMappingId,
                    //x.VehicleId,
                    //x.HCM_Setup_Detail.ColumnValue

                }).ToList();

            var JSON = JsonConvert.SerializeObject(lst);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }

    }

    [OperationContract]
    public string GetCategoryVehicleMappingNew(int CategoryId, bool Upgrade)
    {
        try
        {
            Cls_VehicleInformation Obj_clsVehicleInfo = new Cls_VehicleInformation();
            DataTable dt = new DataTable();
            dt = Obj_clsVehicleInfo.GetVehicleDesignationMapping(CategoryId, Convert.ToInt32(Upgrade)).ResponseDataTable;

            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }

    }

    [OperationContract]
    public string SaveUpdatedDesignationVehiclesMappingNew(string JSon)
    {
        List<DAL.HCM_VehicleDesignationMapping> ResponseDetails = (List<DAL.HCM_VehicleDesignationMapping>)CommonHelper.Deserialize(JSon, typeof(List<DAL.HCM_VehicleDesignationMapping>));
        int DesignationId = 0;
        List<int> lstVehicles = new List<int>();

        int i = 0;
        foreach (DAL.HCM_VehicleDesignationMapping obj in ResponseDetails)
        {
            DesignationId = obj.DesignationId;

            if (i == 0)
            {
                var lst = context.HCM_VehicleDesignationMapping.Where(a => a.IsActive == true && a.DesignationId == DesignationId).ToList();

                if (lst != null && lst.Count > 0)
                {
                    lst.ForEach(a => { a.IsActive = false; a.ModifiedBy = objBase.UserKey; a.ModifiedDate = DateTime.Now; });

                    context.SaveChanges();
                }
            }

            int? VariantId = null;

            var lstVInfo = context.HCM_VehicleInformation.Where(a => a.IsActive == true && a.VehicleInformationId == obj.VehicleInformationId).ToList();

            if (lstVInfo != null && lstVInfo.Count > 0)
            {
                VariantId = lstVInfo[0].VariantId;
            }

            HCM_VehicleDesignationMapping objDesig = new HCM_VehicleDesignationMapping();

            objDesig.DesignationId = obj.DesignationId;
            objDesig.IsUpgradeVehicle = obj.IsUpgradeVehicle;
            objDesig.VehicleInformationId = obj.VehicleInformationId;
            objDesig.VehicleId = Convert.ToInt32(VariantId);
            objDesig.IsActive = true;
            objDesig.CreatedBy = objBase.UserKey;
            objDesig.CreatedDate = DateTime.Now;

            context.HCM_VehicleDesignationMapping.Add(objDesig);
            context.SaveChanges();

            i++;
        }

        return "1";
    }

    [OperationContract]
    public string SaveUpdatedCategoryVehiclesMappingNew(string JSon)
    {
        List<dynamic> ResponseDetails = (List<dynamic>)CommonHelper.Deserialize(JSon, typeof(List<dynamic>));
        Cls_VehicleInformation cls_VehicleInformation = new Cls_VehicleInformation();

        int CategoryID = 0;
        List<int> lstVehicles = new List<int>();

        int i = 0;
        foreach (dynamic obj in ResponseDetails)
        {
            CategoryID = obj.CategoryId;
            int _VehicleInformationId = obj.VehicleInformationId;
            bool IsUpgrade = obj.IsUpgradeVehicle == 0 ? false : true;

            int? VariantId = null;

            var lstVInfo = context.HCM_VehicleInformation.Where(a => a.IsActive == true && a.VehicleInformationId == _VehicleInformationId).ToList();

            if (lstVInfo != null && lstVInfo.Count > 0)
            {
                VariantId = lstVInfo[0].VariantId;
            }


            ResponseHelper ObjResponseHelper = cls_VehicleInformation.ManageVehicleInformationCategoryMapping(CategoryID, Convert.ToInt32(VariantId), IsUpgrade, _VehicleInformationId, i, objBase.UserKey);

            i++;
        }

        return "1";
    }

    [OperationContract]
    public string getAllowances(int CompanyId, string ListOfIds)
    {
        try
        {
            //List<int> lst = ListOfIds.Split(',').Select(int.Parse).ToList();
            //var List = context.HCM_Setup_Allowance.Where(c => c.IsActive == true && c.CompanyId == CompanyId && !lst.Contains(c.AllowanceID))
            //    //.Where(b => b.SpecialTypeId == null || b.SpecialTypeId != (int)Constant.HCMSetupDetail.ProvidentFund)
            //    .Select(a => new
            //    {
            //        Id = a.AllowanceID,
            //        Value = a.AllowanceName,
            //        //Formula = context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null ? "" : context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula,
            //        //IsFormulaExist = context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null || context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == "" ? false : true
            //        Formula = context.HCM_CompanyFormula.Where(b => b.IsActive == true && b.AllowanceID == a.AllowanceID).Count() == 0 ? "" : (context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null ? "" : context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula),
            //        IsFormulaExist = context.HCM_CompanyFormula.Where(b => b.IsActive == true && b.AllowanceID == a.AllowanceID).Count() == 0 ? false : (context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null || context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == "" ? false : true),
            //    })
            //    .OrderBy(c => c.Value).ToList();
            //var JSON = JsonConvert.SerializeObject(List);
            //return JSON;
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeAllowancesCrud", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@AllowanceId", SqlDbType.Int).Value = null;
            da.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = ListOfIds;
            da.SelectCommand.Parameters.Add("@OperationId", SqlDbType.Int).Value = 1;
            da.SelectCommand.Parameters.Add("@Measure", SqlDbType.Decimal).Value = null;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = null;
            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            return JsonConvert.SerializeObject(e.InnerException.ToString());
        }
    }

    [OperationContract]
    public string getOvertimeAllowances(int CompanyId)
    {
        try
        {
            //List<int> lst = ListOfIds.Split(',').Select(int.Parse).ToList();
            //var List = context.HCM_Setup_Allowance.Where(c => c.IsActive == true && c.CompanyId == CompanyId && !lst.Contains(c.AllowanceID))

            string ListOfIds = Convert.ToString((int)Constant.HCM_SpecialTypeId.Overtime) + "," + Convert.ToString((int)Constant.HCM_SpecialTypeId.OvertimeIncentive);
            List<int> lst = ListOfIds.Split(',').Select(int.Parse).ToList();

            var List = context.HCM_Setup_Allowance.AsEnumerable().Where(c => c.IsActive == true && c.CompanyId == CompanyId && lst.Contains(Convert.ToInt32(c.SpecialTypeId)))

                .Select(a => new
                {
                    Id = a.AllowanceID,
                    Value = a.AllowanceName,
                    //Formula = context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null ? "" : context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula,
                    //IsFormulaExist = context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null || context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == "" ? false : true,
                    Formula = context.HCM_CompanyFormula.Where(b => b.IsActive == true && b.AllowanceID == a.AllowanceID).Count() == 0 ? "" : (context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null ? "" : context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula),
                    IsFormulaExist = context.HCM_CompanyFormula.Where(b => b.IsActive == true && b.AllowanceID == a.AllowanceID).Count() == 0 ? false : (context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null || context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == "" ? false : true),
                }).OrderBy(c => c.Value).ToList();
            var JSON = JsonConvert.SerializeObject(List);
            return JSON;
        }
        catch (Exception e)
        {
            return JsonConvert.SerializeObject(e.InnerException.ToString());
        }
    }


    [OperationContract]
    public string getSampleFileFormatUploadType(string upload_type, int company_id)
    {
        ResponseHelper _ObjResponseHelper = new ResponseHelper();
        try
        {
            Constant.FileUploadType _FileUploadType = (Constant.FileUploadType)Enum.Parse(typeof(Constant.FileUploadType), upload_type);


            _ObjResponseHelper = CommonHelper.GetSampleUploadTypeFormat(_FileUploadType, company_id);


            var JSON = JsonConvert.SerializeObject(_ObjResponseHelper);
            return JSON;
        }
        catch (Exception e)
        {
            _ObjResponseHelper.ResponseMessage = e.Message;
            _ObjResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
            _ObjResponseHelper.ResponseData = "";
            return JsonConvert.SerializeObject(_ObjResponseHelper);
        }
    }

    [OperationContract]
    public string report_EobiEmpList(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_EOBIList_V2", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            //da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;

            da.Fill(dt);

            DataTable DT = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                //if (Convert.ToDouble(row["Balance"]) > 0)
                {
                    DT.ImportRow(row);
                }
            }

            var JSON = JsonConvert.SerializeObject(DT);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_SessiContribution(int EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId, int DesignationId, string Firstname, string Lastname, string PayrollMonth)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_SocialSecuritySchemeContribution_V1", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            //da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;

            da.Fill(dt);

            DataTable DT = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                //if (Convert.ToDouble(row["Balance"]) > 0)
                {
                    DT.ImportRow(row);
                }
            }

            var JSON = JsonConvert.SerializeObject(DT);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_HRList(string EmployeeCode, int GroupId, int CompanyId, int LocationId, int BusinessUnitId, int DepartmentId, int CostCenterId, int CategoryId,
        int DesignationId, string Firstname, string Lastname, string PayrollMonth, int TypeId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_HRLIST", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
            da.SelectCommand.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId;
            da.SelectCommand.Parameters.Add("@CostCenterId", SqlDbType.Int).Value = CostCenterId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            da.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = Firstname;
            da.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = Lastname;
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(PayrollMonth);
            da.SelectCommand.Parameters.Add("@Type", SqlDbType.Int).Value = TypeId;

            da.Fill(dt);

            DataTable DT = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                //if (Convert.ToDouble(row["Balance"]) > 0)
                {
                    DT.ImportRow(row);
                }
            }

            var JSON = JsonConvert.SerializeObject(DT);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_HajList(int CompanyId)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_HAJLIST", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

            da.Fill(dt);

            DataTable DT = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                //if (Convert.ToDouble(row["Balance"]) > 0)
                {
                    DT.ImportRow(row);
                }
            }

            var JSON = JsonConvert.SerializeObject(DT);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_MonthlyPfDeduction(int CompanyId, int Year)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);

            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_MonthWisePFDeduction", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@YearOf", SqlDbType.Int).Value = Year;
            da.Fill(dt1);

            da = new SqlDataAdapter("HCM_RPT_MonthWisePFDeduction_Summary", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@YearOf", SqlDbType.Int).Value = Year;
            da.Fill(dt2);

            var JSON = JsonConvert.SerializeObject(dt1) + "#SPLIT#" + JsonConvert.SerializeObject(dt2);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_MonthlyPfLoanDeduction(int CompanyId, int Year)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);

            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_MonthWisePFLoanDeduction", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@YearOf", SqlDbType.Int).Value = Year;
            da.Fill(dt1);

            da = new SqlDataAdapter("HCM_RPT_MonthWisePFLoanDeduction_Summary", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@YearOf", SqlDbType.Int).Value = Year;
            da.Fill(dt2);

            var JSON = JsonConvert.SerializeObject(dt1) + "#SPLIT#" + JsonConvert.SerializeObject(dt2);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string report_IntrestCalculation(int CompanyId, int Year)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_IntrestCalculation", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@YearOf", SqlDbType.Int).Value = Year;

            da.Fill(dt);
            var JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string multimanage_IncTaxForcastAll(int CompanyId, double AdvanceTaxPercent)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Update_AdvanceTaxPercent", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@AdvanceTaxPercent", SqlDbType.Float).Value = AdvanceTaxPercent;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

            da.Fill(dt);
            var JSON = "1";
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string getEmployeeType(int CompanyId)
    {
        var List = context.Setup_EmployeeType.Where(x => x.IsActive == true)
            .OrderBy(x => x.TypeName).Select(s => new
            {
                Value = s.TypeName,
                Id = s.EmployeeTypeId
            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
    public string Test()
    {
        return "Ammar";
    }

    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
    public string Add(int a, int b)
    {
        return (a + b).ToString();
    }

    [OperationContract]
    public string Get_Control_Data_EmployeeSearchFilter(string Type, int GroupId, int CompanyId, int BusinessUnitId, int JobCategoryId,
    int HasEmployeeType, int HasLocation, int HasBusinessUnit, int HasDepartment, int HasCostCenter, int HasSapCostCenter, int HasJobCategory, int HasDesignation)
    {
        try
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter(Type, GroupId, CompanyId, BusinessUnitId, JobCategoryId,
            HasEmployeeType, HasLocation, HasBusinessUnit, HasDepartment, HasCostCenter, HasSapCostCenter, HasJobCategory, HasDesignation, objBase.UserKey);
            var JSON = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                if (Type == "Onload")
                {
                    if (ds.Tables.Count == 3)
                    {
                        JSON = JsonConvert.SerializeObject(ds.Tables[0]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[1]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[2]);
                    }
                }
                else if (Type == "OnChangeGroup")
                {
                    if (ds.Tables.Count == 1)
                    {
                        JSON = JsonConvert.SerializeObject(ds.Tables[0]);
                    }
                }
                else if (Type == "OnChangeCompany")
                {
                    if (ds.Tables.Count == 7)
                    {
                        JSON = JsonConvert.SerializeObject(ds.Tables[0]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[1]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[2]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[3]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[4]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[5]) + "#SPLIT#" + JsonConvert.SerializeObject(ds.Tables[6]);
                    }
                }
                else if (Type == "OnChangeBusinessUnit")
                {
                    if (ds.Tables.Count == 1)
                    {
                        JSON = JsonConvert.SerializeObject(ds.Tables[0]);
                    }
                }
                else if (Type == "OnChangeJobCategory")
                {
                    if (ds.Tables.Count == 1)
                    {
                        JSON = JsonConvert.SerializeObject(ds.Tables[0]);
                    }
                }
            }

            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.InnerException.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string HCM_Validate_PayrollMonth(int CompanyId, string PayrollDate)
    {
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Execute_Status");
        dt1.Columns.Add("msg");
        dt1.Rows.Add("0", "Error occurred while generating the payroll");
        var JSON = JsonConvert.SerializeObject(dt1);
        try
        {
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Validate_PayrollMonth", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@PayrollDate", SqlDbType.DateTime).Value = DateTime.Parse(PayrollDate);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                JSON = JsonConvert.SerializeObject(dt);
                return JSON;
            }
            else
            {
                return JSON;
            }
        }
        catch (Exception e)
        {
            return JSON;
        }
    }

    [OperationContract]
    public string CPPL_Count(string PayrollMonth, int CompanyId)
    {
        try
        {


            DataSet ds = new DataSet();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("RPT_CPPL_HEAD_COUNT", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;

            da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = Convert.ToDateTime(PayrollMonth);
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;



            da.Fill(ds);
            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }

    [OperationContract]
    public string RPT_Payslip_Upload(string PayrollDate, int CompanyId)
    {
        try
        {
            DataTable ds = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_Payslip_Upload", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@PayrollDate", SqlDbType.Date).Value = Convert.ToDateTime(PayrollDate);
            da.Fill(ds);
            var JSON = JsonConvert.SerializeObject(ds);
            return JSON;
        }
        catch (Exception e)
        {
            var JSON = JsonConvert.SerializeObject(e.ToString());
            return JSON;
        }
    }
}

public class IncTaxForcast
{    public int EmployeeId { get; set; }
    public float IncPercent { get; set; }
}