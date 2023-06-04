//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class HCM_Loan_Master_20220224
    {
        public int LoanMasterId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int LoanTypeId { get; set; }
        public int EmployeeId { get; set; }
        public double LoanAmount { get; set; }
        public double InstallmentAmount { get; set; }
        public Nullable<double> LoanAmountWithInterest { get; set; }
        public Nullable<double> LoanBalance { get; set; }
        public System.DateTime SanctionDate { get; set; }
        public System.DateTime SettlementDate { get; set; }
        public Nullable<int> InterestId { get; set; }
        public Nullable<double> InterestRate { get; set; }
        public Nullable<double> InterestAmount { get; set; }
        public bool IsHold { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsSettled { get; set; }
        public string Reason { get; set; }
        public string Comments { get; set; }
        public Nullable<double> CurrentMonthInstallment { get; set; }
        public Nullable<System.DateTime> CurrentMonthInstallmentTillDate { get; set; }
        public Nullable<System.DateTime> LoanGivenDate { get; set; }
    }
}
