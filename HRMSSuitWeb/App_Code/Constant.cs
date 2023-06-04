using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Constant
/// </summary>
public class Constant
{
    public const int DefaultCompanyCode = 0;
    public Constant()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public const string DateFormat2 = "dd/MM/yyyy";
    public const string DateFormat2 = "MM/dd/yyyy";
    public const string DateFormat4 = "yyyy/MM/dd";
    public const string DateFormat3 = "MM/dd/yyyy";
    public const string DateFormat = "MMM dd,yyyy";
    public const string DateFormat1 = "dd-MMM-yyyy ddd";
    public const string DateTimeFormat1 = "dd-MMM-yyyy ddd HH:mm";
    public const string DateFormatWithDay = "ddd, MMM dd, yyyy";
    public const string IntDateFormat = "yyyyMMdd";
    public const string HRKhi = "hr@sybrid.com";
    public const string HRISB = "HRTeam-ISB@sybridts.com";
    public const string DateFormatMDY = "dd-MMM-yyyy";
    public const int ConnectionTimeout = 36000;
    public const string SecurityKey = "SecurityKey_HCM_PAYROLL";

    //public enum HREmail
    //{
    //    HRKHI = "hr@sybrid.com",
    //    HRISB="HRTeam-ISB@sybridts.com"

    //}
    public enum enumAccessFunction
    {

    }

    public enum Page
    {

    }

    public enum Role
    {
        Admin = 5,
        Incharge = 6,
        Employee = 7,
        SuperAdmin = 8,
        HOD = 0,
        SuperEmployee = 38,
        HCMSuperAdmin = 39,
    }

    public enum Application
    {
        TMS = 1,
        HRMS = 2

    }

    public enum EmailTemplates
    {
        ForgotPassword = 1,
        NewRegistration = 2,
        EmployeeProfileLink = 3
    }

    public enum CostCenterLevel
    {
        Country = 1,
        City = 2,
        Branch = 3
    } 

    public enum Sap_staging_Table
    {
        HRMS_Setup_BloodGroup = 1,
        HRMS_Setup_EducationType = 2,
        HRMS_Setup_Gender = 3,
        HRMS_Setup_MartialStatus = 4,
        TS_Setup_BusinessUnit = 5,
        Setup_Category = 6,
        Setup_City = 7,
        Setup_CostCenter = 8,
        Setup_Department = 9,
        Setup_EmployeeType = 10,
        Setup_Location = 11,
        Setup_Province = 12,
        HCM_SETUP_SapCostCenter = 13
    }

    public enum Department
    {
        HumanResources = 9039582,
        HumanResourcesTS = 14975486,
        Finance = 0
    }

    public enum EmployeeType
    {
        //Permanent = 1,
        //Contractual = 2,
        //PartTime = 3

        Permanent = 1,
        Contractual = 2,
        Probation = 3,
        ContractFT = 4,
        ContractPT = 5,
        Internee = 6,
        Newhiring = 7
    }

    public enum EducationStatus
    {
        Complete = 1,
        pendingIncompleteclosed = 2,
        Continue = 3,
        Leaving = 4
    }

    //public enum leavechangescode
    //{
    //    cr = cr,
    //    ye = ye
    //}

    public enum TMSStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3

    }

    public enum EmailLinkTIme
    {
        TImeHour = 72
    }
    public enum TMSDayTypes
    {

        Working = 1,
        Alternate = 2,
        Holiday = 3,
        Leave = 4,
        Off = 5,
        NotApplicable = 10
    }
    public enum TMSLeaveTypes
    {
        Sick = 1,
        Annual = 2,
        Casual = 3,
        Maternity = 4,
        Default = 5,
        WithoutPay = 6,
        Official = 7
    }
    public enum LeaveChangesCode
    {
        YE = 1,
        CR = 2,
        MC = 3
    }

    public static class Urls
    {
        public static string RootInSybrid { get { return Convert.ToString(WebConfigurationManager.AppSettings["RootInSybrid"]); } }

    }

    public enum TMSIds
    {

        Received = 1,
        NotReceived = 2,
        NotIssued = 3,
        Active = 4,
        Deleted = 5,
        NA = 6,
        Yes = 7,
        No = 8,
        Suspended = 9
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum ValueType
    {
        Url_Expiry_Days = 1,
        Wrong_Login_Attempts = 2
    }
    public enum OperationType
    {
        INSERT = 1,
        UPDATE = 2,
        DELETE = 3
    }

    public enum ResponseType
    {
        SUCCESS = 1,
        WARNING = 2,
        ERROR = 3
    }
    public enum Location
    {
        Khi = 1,
        Isb = 2,
        Uae = 3
    }

    public enum RequisitionSetupMaster
    {
        ExperienceYear = 10,
        JobPlacementCategory = 11,
        HiringReason = 12,
        SalaryRange = 13,
        MediaCategory = 1,
        WorkflowDecision = 20,
        YES_No = 27,
    }

    public enum RequisitionSetupDetail
    {
        Approved = 93,
        Rejected = 94,
        Negotiate = 95,
        Yes = 117,
        No = 118,
        ApprovedAfterNegotiate = 131,
        RejectAfterNegotiate = 132,
    }

    public enum Workflow
    {
        RequisitionWorkflow = 1,
        NewHiringWorkflow = 2,
        Probation = 3,
        Separation = 4,
        PfLoan = 5,
        MedReimbursment = 6,
    }

    public enum WorkflowStepRequisition
    {
        InitiateRequest = 1,
        HODApproval = 2,
        HRApproval = 3,
        FinanceApproval = 6,
        CEOApproval = 8,
        Terminate = 4,
        JobPosting = 11,
        HODAgreeDisagree = 7,
        HODAfterQADecision = 10,
        QADecision = 9
    }


    public enum WorkflowStepNewHiring
    {
        HRStarter = 21,
        HODStarter = 22,
        AdminStarter = 23,
        ITStarter = 24,
        DeptAcknowledgementAdminStarter = 25,
        DeptAcknowledgementITStarter = 31,
        NadraVerisys = 17,
        CeoApprovalRequired = 16,
        TrainingDecision = 39,
        Terminate = 38,
        HRSentOfferLetter = 18,
        TelephoneReferenceCheck = 65,
        EMPLOYMENTREFERENCECHECK = 66,
        PERSONALREFERENCECHECK = 67,
    }

    public enum WorkflowStepSeperation
    {
        InitiateSeparation = 40,
        HRReviewSeparation = 41,
        ManagerApprovalSeparation = 42,
        HRApprovalSeparation = 43,
        TerminateWorkflow = 44,

        PayrollSeparationTask = 45,
        AdminSeparationTask = 46,
        HRExitInterview = 47,
        InitiatorExitInterview = 55,

        HRRCA = 48,
        ManagerRCA = 49,
        ITLeaver = 50,
        Finance = 51,
        //HODReview = 52,

        ITSubmitITLeaver = 53,
        HRClearenceTask = 54,
        HODClearenceTask = 52,
        //  InitiaterExitInterview=55
    }

    public enum WorkflowStepReimbursement
    {
        InitiateMedicalReimbursement = 60,
        HRReceiveDocuments = 61,
        HRApprovalMedicalReimbursement = 62,
        PersonAcknowledgement = 63,
        Terminate = 64
    }

    public enum WorkflowStepPfLoan
    {
        InitiatePfLoan = 56,
        HodApproval = 57,
        Payroll = 58,
        Terminate = 59,
    }

    public enum SeperationCriteriaMaster
    {
        ITLeaver = 1,
        Admin = 2,
        Payroll = 3,
        HR = 4,
        Finance = 5,
        HOD = 6,
        PfInitiateLoan = 7,
        PfHodForm = 8,
        PfPayrollForm = 9,
        HODStarterCriteria = 16,
        TelephoneReferenceCheckForm = 13,
        EmploymentReferenceCheckForm = 14,
        PersonalReferenceCheckForm = 15,
    }

    public enum CriteriaDetail
    {
        LoanAmount = 26,
        RecomendedLoanAmount = 30,

    }

    public enum ValueTypeControl
    {
        Textbox = 1,
        RadioButton = 2,
        Dropdown = 3,
        DatePicker = 4,
        TextboxSimple = 5,
    }
    public enum SeperationStatus
    {
        Yes = 7,
        No = 8,
        Suspended = 9
    }

    public enum WorkflowStatus
    {
        New = 1,
        InProgress = 2,
        Completed = 3,
    }

    public enum WorkflowTaskStatus
    {
        Pending = 1,
        Completed = 2,
    }

    public enum WorkflowMemberCategory
    {
        Management = 1,
        Member = 2
    }

    public enum SetupMaster
    {
        FileStatus = 5,
        FileType = 6,
        InterviewType = 14,
        InterviewSteps = 16,
        EvaluationStatus = 9,
        ApplicantSkillStatus = 3,
        CandidateStatus = 4,
        InterviewStatus = 7,
        InterviewEvaluation = 8,
        EmployeeType = 18,
        HRStarterCriteria = 16,
        ProbationJobBehviour = 17,
        EmployeeWeaknessStrengthRecommendation = 19,
        Training = 22,
        ClaimType = 23,
        ClaimFor = 24
    }

    public enum Training
    {
        Complete = 102,
        Extend = 103,
        Fail = 104,
    }

    public enum CandidateEmployeeType
    {
        Perminent = 86,
        Contractual = 87,
        Training = 88,
    }

    public enum CandidateStatus
    {
        ShortListed = 13,
        InterviewScheduled = 14,
        Hire = 17,
        //  JobOffered = 18,
        Finalize = 101,
        SendOfferLetter = 18,
        OfferAccept = 80,
        OfferReject = 81,
        Rejected = 15,
        ConsiderForOtherPosition = 16,
        OnHold = 82,
        NextInterviewRequired = 58,
        CEOApprovalRequired = 83,
        CEOApproved = 84,
        CEOReject = 85,
        OnTraining = 89,
        BlackList = 100
    }


    public enum FileUploadType
    {
        Overtime = 1,
        AbsentLog = 2,
        Separation = 3,
        ContractRenewal = 4,
        Allowance = 5,
        BankAccount = 6,
        Increment = 7,
        NewEmployee = 8,
        EmployeeEducationDetail = 9,
        IncrementLetter = 10,
        ConfirmationLetter = 11,
        LeaveEncashment = 12,
        GeneralData = 13
    }


    public enum FileUploadTypeName
    {
        Sample_Format_Overtime = 1,
        Sample_Format_AbsentLog = 2,
        Sample_Format_Separation = 3,
        Sample_Format_ContractRenewal = 4,
        Sample_Format_Allowance = 5,
        Sample_Format_BankAccount = 6,
        Sample_Format_Increment = 7,
        Sample_Format_NewEmployee = 8,
        Sample_Format_EmployeeEducationDetail = 9,
        Sample_Format_NewEmployeeDataColgate = 10,
        Sample_Format_Increment_Letter = 11,
        Sample_Format_Confirmation_Letter = 12,
        Sample_Format_Leaves = 13,
        Sample_Format_MedicalInsurance = 14
    }
    public enum FileStatus
    {
        Received = 19,
        Viewed = 20,
        CallForInterview = 59,
        ShortList = 60,
        Reject = 61,
        FileType = 6
    }
    public enum FileType
    {
        InterviewLetter = 21,
        OfferLetter = 22

    }

    public enum SetupDetail
    {
        Received = 19,
        Viewed = 20,
        Supervisor = 127,
        Peer = 128

    }

    public enum Nationality
    {
        Pakistani = 1,


    }

    public enum WorkflowStepProbation
    {
        HODConfirmProbation = 28,
        HRConfirmProbation = 37,
        Terminate = 36,
    }

    public enum TerminationType
    {
        Resigned = 1,
        Layoff = 2,
        Dissmiss = 3
    }

    //*************************** HCM ***************************


    public enum HCMAllowance
    {
        VehicleDeduction = 24
    }
    public enum HCMSetupMaster
    {
        FormulaElement = 2,
        FormulaCategory = 5,
        Unit = 4,
        LoanType = 6,
        Manufacturer = 11,
        SalaryStandard = 18,
        Dynamic = 17,
        Loan_Type = 6,
        Arrear_Type = 13,
        InterestYearSlabs = 34,
        NoOfLoansAllow = 20,
        InterestSetting = 24,
        SpecialType = 37,
        NoticePeriodType = 42,
        GroupInsuranceType = 41,
        LoanInterestRate = 32,
        VehicleType = 10,
        VehicleName = 12,
        VehicleVariant = 23,
    }

    public enum HCMSetupDetail
    {
        BasicSalary = 11,
        GrossSalary = 12,
        CompoundInterest = 77,
        ProvidentFund = 105,
        VehiclePayment = 134,
        CarAllowance = 1172,
        RepairMaintenance = 124,
        FuelAmount = 1160 
    }

    public enum FormulaCategory
    {
        Allowance = 15,
        Salary = 16,
    }

    public enum FormulaElement
    {
        Allowance = 1,
        Deduction = 2,
        Constant = 3,
        Operator = 4,
        Salary = 10,
        AbsentFlexi = 106,
    }


    public enum HCM_Allowance
    {
        OvertimeAllowance = 25
    }

    public enum HCM_Arrear
    {
        Salary = 30
    }

    public enum HCM_ChangeRequestStatus
    {
        Requested = 1,
        Approved = 2,
        Rejected = 3
    }


    public enum HCM_Salary_Standards
    {
        BasicSalary = 43,
        GrossSalary = 44
    }

    public enum EmployeeTypeId
    {
        Contractual = 16,
        Contract_No_Bonus = 19,
        Contract_Bonus_Only = 20,
        Contract_PF_Only = 23,
    }

    public enum HCM_SpecialTypeId
    {
        PerformanceBonus = 1177,
        Overtime = 134,
        OvertimeIncentive = 1179,
    }
}