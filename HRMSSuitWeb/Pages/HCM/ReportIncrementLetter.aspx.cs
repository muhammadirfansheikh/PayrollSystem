using DAL;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Pages_HCM_ReportIncrementLetter : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    Base baseclass = new Base();
    Document document;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }

    private void BindDropDown()
    {
        ddlGroup.Enabled = false;
        var LstGroup = context.Setup_Group.Where(x => x.IsActive == true).OrderBy(x => x.GroupName).Select(s => new
        {
            GroupName = s.GroupName,
            GroupId = s.GroupId
        }).ToList();
        CommonHelper.BindDropDown(ddlGroup, LstGroup, "GroupName", "GroupId", LstGroup.Count == 1 ? false : true, false);
        ddlGroup_SelectedIndexChanged(null, null);
        ddlCompany_SelectedIndexChanged(null, null);
    }


    private void BindReportingCostCenter()
    {
        int CompanyId = 0;

        CompanyId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
        var _ReportingCostCenter = context.HRMS_Setup_ReportingCostCenter.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.ReportingCostCenter).Select(x => new { Id = x.ReportingCostCenterId, Value = x.ReportingCostCenter + "_" + x.ReportingCostCenterCode }).ToList();
        CommonHelper.BindDropDown(ddlReportingCostCenter, _ReportingCostCenter, "Value", "Id", _ReportingCostCenter.Count == 1 ? false : true, false);


    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {


        BindReportingCostCenter();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
        var LstCompany = context.Setup_EmployeeCompanyMapping.Where(a => a.IsActive == true && a.EmployeeId == UserKey && a.Setup_Company.IsActive == true && a.Setup_Company.GroupId == GroupId)
            .Select(a => new
            {
                CompanyName = a.Setup_Company.CompanyName,
                CompanyId = a.Setup_Company.CompanyId
            }).OrderBy(b => b.CompanyName).ToList();

        CommonHelper.BindDropDown(ddlCompany, LstCompany, "CompanyName", "CompanyId", false, false);

        /// btnExport_Click(null, null);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            int _EmployeeCode = 0;

            if (!String.IsNullOrEmpty(txtEmployeeCode.Text))
                _EmployeeCode = Convert.ToInt32(txtEmployeeCode.Text);

            GenerateReport(Convert.ToInt32(ddlCompany.SelectedValue), _EmployeeCode, txtFromDate.Text, Convert.ToInt32(ddlReportingCostCenter.SelectedValue));
        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    private void GeneratePDF(List<EmployeeModel> EmployeeModelList, List<EmployeeSalaryModel> EmployeeSalaryModelList, int FileNumber = 0)
    {
        string _Path = ddlReportingCostCenter.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy") + DateTime.Now.Millisecond.ToString() + DateTime.Now.Second + ".pdf";
        PdfWriter _PdfDocumentFileWriter = new PdfWriter(Server.MapPath("~/Uploads/Temp/" + _Path));
        PdfDocument pdf = new PdfDocument(_PdfDocumentFileWriter);
        PdfFont font = PdfFontFactory.CreateFont(StandardFonts.COURIER);
        document = new Document(pdf).SetFont(font).SetFontSize(10);

        for (int i = 0; i < EmployeeModelList.Count; i++)
        {
            EmployeeModel _EmployeeData = EmployeeModelList[i];

            List<EmployeeSalaryModel> _FilterEmployeeSalarData = EmployeeSalaryModelList.Where(x => x.EmployeeCode == _EmployeeData.EmployeeCode).ToList();

            Paragraph newline = new Paragraph(new Text("\n"));
           
            document.Add(newline);
            document.Add(newline);
            document.Add(newline);
            document.Add(newline);
            document.Add(newline);
            //document.Add(newline);
            //document.Add(newline);
            // Header
            //Paragraph header = new Paragraph("Confidential")
            //   .SetTextAlignment(TextAlignment.RIGHT).SetBorderBottom(new DashedBorder(ColorConstants.BLACK, 1))    ;

            //// New line

            ////document.Add(newline);
            //document.Add(header);


            iText.Layout.Element.Table _TableHeader = new iText.Layout.Element.Table(2, false);
            Cell _CellHeader1 = new Cell(1, 1)
               .Add(new Paragraph("")).SetWidth(370).SetBorder(Border.NO_BORDER);
            Cell _CellHeader2 = new Cell(1, 1)
               .Add(new Paragraph("CONFIDENTIAL")).SetTextAlignment(TextAlignment.LEFT)
               .SetWidth(50).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.GRAY, 1));

            _TableHeader.AddCell(_CellHeader1);
            _TableHeader.AddCell(_CellHeader2);

            document.Add(_TableHeader);
            int EmployeeMasterPaddingTop = -5;

            iText.Layout.Element.Table _TableEmployeData1 = new iText.Layout.Element.Table(2, false);

            Cell _CellEmployeeName = new Cell(1, 1).SetPaddingLeft(95)
               .Add(new Paragraph(_EmployeeData.Name)).SetWidth(200).SetPaddingTop(10)
              // .SetBorder(new DashedBorder(ColorConstants.BLACK, 2));
              .SetBorder(Border.NO_BORDER);

            Cell _CellEmployeeCode = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER)
             .Add(new Paragraph("[  " + _EmployeeData.EmployeeCode.ToString() + "  ]")).SetWidth(100).SetPaddingTop(10)
              // .SetBorder(new DashedBorder(ColorConstants.BLACK, 2));
              .SetBorder(Border.NO_BORDER);

            Cell CellEmpty = new Cell(1, 2)
               .SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("")).SetBorder(Border.NO_BORDER);
            Cell CellEmptyval = new Cell(1, 2)
              .SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("")).SetBorder(Border.NO_BORDER);

            _TableEmployeData1.AddCell(_CellEmployeeName);
            _TableEmployeData1.AddCell(_CellEmployeeCode);
            _TableEmployeData1.AddCell(CellEmpty);
            _TableEmployeData1.AddCell(CellEmptyval);
            _TableEmployeData1.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
            document.Add(_TableEmployeData1);

            //Cell CellEmpty = new Cell(1, 2)
            //   .SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("")).SetBorder(Border.NO_BORDER);
            //Cell CellEmptyval = new Cell(1, 2)
            //  .SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("")).SetBorder(Border.NO_BORDER);


            iText.Layout.Element.Table _TableEmployeData = new iText.Layout.Element.Table(2, false);
            Cell _CellDesignation = new Cell(1, 1).SetWidth(200).SetPaddingLeft(95)
               .Add(new Paragraph("DESIGNATION"))
              //.SetBorder(new DashedBorder(ColorConstants.BLACK, 2));
              .SetBorder(Border.NO_BORDER);

            Cell _CellDesignationValue = new Cell(1, 1)
                 .Add(new Paragraph(": " + _EmployeeData.DesignationName))
                .SetTextAlignment(TextAlignment.LEFT).SetWidth(350).SetMarginRight(90)
              // .SetBorder(new DashedBorder(ColorConstants.BLACK, 2));
              .SetBorder(Border.NO_BORDER);


            Cell _CellLocation = new Cell(1, 1)
               .Add(new Paragraph("LOCATION"))
               .SetPaddingTop(EmployeeMasterPaddingTop).SetWidth(20).SetPaddingLeft(95)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellLocationValue = new Cell(1, 1)
               .Add(new Paragraph(": " + _EmployeeData.LocationName))
               .SetPaddingTop(EmployeeMasterPaddingTop)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetWidth(350).SetMarginRight(90)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellSapCostCenter = new Cell(1, 1)
               .Add(new Paragraph("SAP COST CENTER"))
               .SetPaddingTop(EmployeeMasterPaddingTop).SetWidth(20).SetPaddingLeft(95)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellSapCostCenterValue = new Cell(1, 1)
               .Add(new Paragraph(": " + _EmployeeData.SapCostCenter))
               .SetPaddingTop(EmployeeMasterPaddingTop).SetTextAlignment(TextAlignment.LEFT)
               .SetWidth(350).SetMarginRight(90)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellDateOfJoining = new Cell(1, 1)
               .Add(new Paragraph("DATE OF JOINING"))
                .SetPaddingTop(EmployeeMasterPaddingTop).SetWidth(20).SetPaddingLeft(95)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellDateOfJoiningValue = new Cell(1, 1)
               .Add(new Paragraph(": " + _EmployeeData.DateOfJoining))
               .SetPaddingTop(EmployeeMasterPaddingTop).SetTextAlignment(TextAlignment.LEFT)
               .SetWidth(350).SetMarginRight(90)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellIncrementDate = new Cell(1, 1)
               .Add(new Paragraph("DATE OF NEW INCREMENT"))
               .SetPaddingTop(EmployeeMasterPaddingTop).SetWidth(100).SetPaddingLeft(94)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellIncrementDateValue = new Cell(1, 1)
               .Add(new Paragraph(": " + _EmployeeData.DateOfNewIncrement))
               .SetPaddingTop(EmployeeMasterPaddingTop).SetTextAlignment(TextAlignment.LEFT)
               .SetWidth(560).SetMarginRight(220)
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            //_TableEmployeData.AddCell(_CellEmployeeName);
            //_TableEmployeData.AddCell(_CellEmployeeCode);
            //_TableEmployeData.AddCell(CellEmpty);
            //_TableEmployeData.AddCell(CellEmptyval);
            _TableEmployeData.AddCell(_CellDesignation);
            _TableEmployeData.AddCell(_CellDesignationValue);
            _TableEmployeData.AddCell(_CellLocation);
            _TableEmployeData.AddCell(_CellLocationValue);
            _TableEmployeData.AddCell(_CellSapCostCenter);
            _TableEmployeData.AddCell(_CellSapCostCenterValue);
            _TableEmployeData.AddCell(_CellDateOfJoining);
            _TableEmployeData.AddCell(_CellDateOfJoiningValue);
            _TableEmployeData.AddCell(_CellIncrementDate);
            _TableEmployeData.AddCell(_CellIncrementDateValue);

            _TableEmployeData.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
            document.Add(_TableEmployeData);

            //document.Add(newline);



            // Add paragraph1
            Paragraph _DescriptionOnePara = new Paragraph(_EmployeeData.DescriptionOne);
            _DescriptionOnePara.SetWidth(399);
            _DescriptionOnePara.SetMarginLeft(94);
            _DescriptionOnePara.SetPaddingTop(5);
            _DescriptionOnePara.SetFontSize(10.5F);
            _DescriptionOnePara.SetFixedLeading(12);
            document.Add(_DescriptionOnePara);

            int EmptyCellWidth = 20;
            int CellWidth = 30;
            int HeaderWidth = 10;


            iText.Layout.Element.Table _TableEmployeeAllownceData = new iText.Layout.Element.Table(7, false);

            Cell _CellEmpAllow = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.RIGHT).Add(new Paragraph(""))//.SetWidth(HeaderWidth)
            .SetBorder(Border.NO_BORDER);
            _TableEmployeeAllownceData.AddCell(_CellEmpAllow);

            Cell _CellEmpty0 = new Cell(1, 1)
              .Add(new Paragraph("")).SetWidth(EmptyCellWidth)
              .SetBorder(Border.NO_BORDER);
            _TableEmployeeAllownceData.AddCell(_CellEmpty0);

            Cell _CellEmpNewSalary = new Cell(1, 1)
                
                .SetWidth(HeaderWidth)
                .SetTextAlignment(TextAlignment.RIGHT)
               .Add(new Paragraph("New Salary Rupees"))
               .SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.GRAY, 1));
            _TableEmployeeAllownceData.AddCell(_CellEmpNewSalary);

            Cell _CellEmpty = new Cell(1, 1)
              .Add(new Paragraph("")).SetWidth(EmptyCellWidth)
              .SetBorder(Border.NO_BORDER);
            _TableEmployeeAllownceData.AddCell(_CellEmpty);

            Cell _CellEmpCurrentSalary = new Cell(1, 1)
              .SetWidth(HeaderWidth)
             .SetTextAlignment(TextAlignment.RIGHT)
               .Add(new Paragraph("Current Salary Rupees"))
               .SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.GRAY, 1));
            _TableEmployeeAllownceData.AddCell(_CellEmpCurrentSalary);

            Cell _CellEmpty1 = new Cell(1, 1)
                .SetWidth(EmptyCellWidth)
       .Add(new Paragraph(""))
       .SetBorder(Border.NO_BORDER);
            _TableEmployeeAllownceData.AddCell(_CellEmpty1);


            Cell _CellEmpChangeSalary = new Cell(1, 1)
               .SetWidth(HeaderWidth).SetTextAlignment(TextAlignment.RIGHT)
               .Add(new Paragraph("Change Salary Rupees"))
               .SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.GRAY, 1));
            _TableEmployeeAllownceData.AddCell(_CellEmpChangeSalary);


            for (int j = 0; j < _FilterEmployeeSalarData.Count; j++)
            {

                if (_FilterEmployeeSalarData[j].Allowances == "Gross Salary per month" || _FilterEmployeeSalarData[j].Allowances == "Total Pay per month")
                {
                    Cell _CellEmpAllownceName = new Cell(1, 1)
             .SetPaddingLeft(95).SetPaddingTop(1)//.SetWidth(CellWidth)//.SetMarginLeft(400).SetWidth(400)
             .Add(new Paragraph(_FilterEmployeeSalarData[j].Allowances))
             .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpAllownceName);
                }
                else
                {
                    Cell _CellEmpAllownceName = new Cell(1, 1)
            .SetPaddingLeft(95).SetPaddingBottom(-3)//.SetWidth(CellWidth)//.SetMarginLeft(400).SetWidth(400)
            .Add(new Paragraph(_FilterEmployeeSalarData[j].Allowances))
            .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpAllownceName);
                }

                if (_FilterEmployeeSalarData[j].Allowances == "Gross Salary per month" || _FilterEmployeeSalarData[j].Allowances == "Total Pay per month")
                {

                    Cell _CellEmpty6 = new Cell(1, 1)
            .SetPaddingLeft(0).SetWidth(EmptyCellWidth).SetTextAlignment(TextAlignment.RIGHT)
            .Add(new Paragraph(""))
           .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpty6);

                    Cell _CellEmpNewSalaryRupeesValue = new Cell(1, 1)
                      .SetPaddingLeft(0).SetWidth(CellWidth).SetPaddingBottom(4).SetPaddingTop(4)
                    .SetTextAlignment(TextAlignment.RIGHT)
                     .Add(new Paragraph(_FilterEmployeeSalarData[j].NewSalaryRupees.ToString()))
                     
                    .SetBorder(Border.NO_BORDER)//.SetBorderBottom(new DashedBorder(ColorConstants.BLACK, 1))
                    .SetBorderTop(new SolidBorder(ColorConstants.GRAY, 1));
                    _TableEmployeeAllownceData.AddCell(_FilterEmployeeSalarData[j].Allowances == "Total Pay per month" ? _CellEmpNewSalaryRupeesValue.SetBorderBottom(new SolidBorder(ColorConstants.GRAY, 1)) : _CellEmpNewSalaryRupeesValue);
                    


                    Cell _CellEmpty2 = new Cell(1, 1)
            .SetPaddingLeft(0).SetWidth(EmptyCellWidth).SetTextAlignment(TextAlignment.RIGHT)
            .Add(new Paragraph(""))
           .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpty2);


                    Cell _CellEmpCurrentSalaryRupeesValue = new Cell(1, 1)
                       .SetPaddingLeft(0).SetMarginLeft(2).SetWidth(CellWidth).SetPaddingBottom(4).SetPaddingTop(4)
                       .SetTextAlignment(TextAlignment.RIGHT)
                       .Add(new Paragraph(_FilterEmployeeSalarData[j].CurrentSalaryRupees.ToString()))
                  
                       .SetBorder(Border.NO_BORDER)//.SetBorderBottom(new DashedBorder(ColorConstants.BLACK, 1))
                       .SetBorderTop(new SolidBorder(ColorConstants.GRAY, 1));
                    _TableEmployeeAllownceData.AddCell(_FilterEmployeeSalarData[j].Allowances == "Total Pay per month" ? _CellEmpCurrentSalaryRupeesValue.SetBorderBottom(new SolidBorder(ColorConstants.GRAY, 1)) : _CellEmpCurrentSalaryRupeesValue);
                    //_TableEmployeeAllownceData.AddCell(_CellEmpCurrentSalaryRupeesValue);

                    Cell _CellEmpty3 = new Cell(1, 1)
                .SetPaddingLeft(0).SetWidth(EmptyCellWidth).SetTextAlignment(TextAlignment.RIGHT)
            .SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(""))
            .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpty3);

                    Cell _CellEmpChangeRupeesValue = new Cell(1, 1)
                         .SetPaddingLeft(0).SetMarginLeft(2).SetWidth(CellWidth).SetPaddingBottom(4).SetPaddingTop(4)
                         .SetTextAlignment(TextAlignment.RIGHT)
                       .Add(new Paragraph(_FilterEmployeeSalarData[j].ChangeRupees.ToString()))
                       .SetBorder(Border.NO_BORDER)//.SetBorderBottom(new DashedBorder(ColorConstants.BLACK, 1))
                       .SetBorderTop(new SolidBorder(ColorConstants.GRAY, 1));
                    _TableEmployeeAllownceData.AddCell(_FilterEmployeeSalarData[j].Allowances == "Total Pay per month" ? _CellEmpChangeRupeesValue.SetBorderBottom(new SolidBorder(ColorConstants.GRAY, 1)) : _CellEmpChangeRupeesValue);
                   // _TableEmployeeAllownceData.AddCell(_CellEmpChangeRupeesValue);

                }
                else
                {

                    Cell _CellEmpty6 = new Cell(1, 1)
            .SetPaddingLeft(0).SetWidth(EmptyCellWidth).SetTextAlignment(TextAlignment.RIGHT)
            .Add(new Paragraph(""))
           .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpty6);

                    Cell _CellEmpNewSalaryRupeesValue = new Cell(1, 1)
                 .SetPaddingLeft(0).SetWidth(CellWidth).SetPaddingBottom(-1)
                 .SetTextAlignment(TextAlignment.RIGHT)
                  .Add(new Paragraph(_FilterEmployeeSalarData[j].NewSalaryRupees.ToString()))
                  
                  .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpNewSalaryRupeesValue);

                    Cell _CellEmpty2 = new Cell(1, 1)
                  .SetPaddingLeft(0).SetWidth(EmptyCellWidth).SetTextAlignment(TextAlignment.RIGHT)
                  .Add(new Paragraph(""))
                  .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpty2);

                    Cell _CellEmpCurrentSalaryRupeesValue = new Cell(1, 1)
                        .SetPaddingLeft(0).SetWidth(CellWidth).SetPaddingBottom(-1)
                       .SetTextAlignment(TextAlignment.RIGHT)
                       .Add(new Paragraph(_FilterEmployeeSalarData[j].CurrentSalaryRupees.ToString()))
                       
                       .SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpCurrentSalaryRupeesValue);

                    Cell _CellEmpty5 = new Cell(1, 1)
                    .SetPaddingLeft(0).SetWidth(EmptyCellWidth).SetTextAlignment(TextAlignment.RIGHT)
                .Add(new Paragraph("")).SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpty5);

                    //        //    Cell _CellEmpty6 = new Cell(1, 1)
                    //        //    .SetMarginLeft(1).SetWidth(EmptyCellWidth)//.SetTextAlignment(TextAlignment.RIGHT)
                    //        //.Add(new Paragraph("")).SetBorder(Border.NO_BORDER);
                    //        //    _TableEmployeeAllownceData.AddCell(_CellEmpty6);

                    Cell _CellEmpChangeRupeesValue = new Cell(1, 1)
                            .SetPaddingLeft(0).SetWidth(CellWidth).SetPaddingBottom(-1)
                       .SetTextAlignment(TextAlignment.RIGHT)
                       .Add(new Paragraph(_FilterEmployeeSalarData[j].ChangeRupees.ToString())).SetBorder(Border.NO_BORDER);
                    _TableEmployeeAllownceData.AddCell(_CellEmpChangeRupeesValue);
                }

                }


            ////    _TableEmployeeAllownceData.SetWidth(UnitValue.CreatePercentValue(100));
            //  //  _TableEmployeeAllownceData.SetWidth(300);

                document.Add(_TableEmployeeAllownceData.SetMarginTop(7));
            document.Add(newline);



            // Add paragraph1
            int multiParagraphwidth = 399;
            int multiParagraphMarginLeft = 95;

            if (!String.IsNullOrEmpty(_EmployeeData.DescriptionTwo))
            {

                Paragraph _SecoandPara = new Paragraph(_EmployeeData.DescriptionTwo);
                _SecoandPara.SetWidth(multiParagraphwidth);
                _SecoandPara.SetMarginLeft(multiParagraphMarginLeft);
                document.Add(_SecoandPara);
                //document.Add(newline);
            }

            if (!String.IsNullOrEmpty(_EmployeeData.DescriptionThree))
            {
                Paragraph _ThirdPara = new Paragraph(_EmployeeData.DescriptionThree);
                _ThirdPara.SetWidth(multiParagraphwidth);
                _ThirdPara.SetMarginLeft(multiParagraphMarginLeft);
                document.Add(_ThirdPara);
                //document.Add(newline);
            }

            if (!String.IsNullOrEmpty(_EmployeeData.DescriptionFour))
            {
                Paragraph _FourthPara = new Paragraph(_EmployeeData.DescriptionFour);
                _FourthPara.SetWidth(multiParagraphwidth);
                _FourthPara.SetMarginLeft(multiParagraphMarginLeft);
                document.Add(_FourthPara);
                // document.Add(newline);
            }

            if (!String.IsNullOrEmpty(_EmployeeData.DescriptionFive))
            {
                Paragraph _FivePara = new Paragraph(_EmployeeData.DescriptionFive);
                _FivePara.SetWidth(multiParagraphwidth);
                _FivePara.SetMarginLeft(multiParagraphMarginLeft);
                document.Add(_FivePara);
                // document.Add(newline);
            }

            document.Add(newline);

            iText.Layout.Element.Table _TableFooterData = new iText.Layout.Element.Table(2, false);

            Cell _CellFoterKarachi = new Cell(1, 1)
             .SetPaddingLeft(100)
             .Add(new Paragraph("KARACHI")).SetWidth(30)
             .SetBorder(Border.NO_BORDER);



            Cell _CellFooterManagerName = new Cell(1, 1)
                .SetPaddingLeft(100).SetWidth(250).SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(txtIssuedBy.Text))
            .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _CellFooterCurrentDate = new Cell(1, 1)
            .SetPaddingLeft(100).SetPaddingTop(-5)
            .Add(new Paragraph(" Dated:" + DateTime.Now.ToString("dd/MM/yyyy") + "")).SetWidth(30)
            .SetBorder(Border.NO_BORDER);


            Cell _CellFooterManagerDesc = new Cell(1, 1)
                .SetPaddingLeft(100).SetWidth(250).SetTextAlignment(TextAlignment.CENTER).SetPaddingTop(-5)
               .Add(new Paragraph(txtDesignation.Text))
               .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            _TableFooterData.AddCell(_CellFoterKarachi);
            _TableFooterData.AddCell(_CellFooterManagerName);
            _TableFooterData.AddCell(_CellFooterCurrentDate);
            _TableFooterData.AddCell(_CellFooterManagerDesc);
            //_TableFooterData.SetBorder(new SolidBorder(ColorConstants.BLACK, 1));

            document.Add(_TableFooterData);

            //// Page numbers
            //int n = pdf.GetNumberOfPages();
            //for (int i = 1; i <= n; i++)
            //{
            //    document.ShowTextAligned(new Paragraph(String
            //       .Format("page" + i + " of " + n)),
            //       559, 806, i, TextAlignment.RIGHT,
            //       VerticalAlignment.TOP, 0);
            //}

            // Creating an Area Break          
            AreaBreak aB = new AreaBreak();

            // Adding area break to the PDF       
            document.Add(aB);

            /// _HtmlData += GetHtmlData(_EmployeeData, _FilterEmployeeSalarData);
        }
        pdf.RemovePage(pdf.GetNumberOfPages());
        document.Close();




        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + _Path);
        Response.TransmitFile(Server.MapPath("~/Uploads/Temp/" + _Path));
        Response.End();
        //_Html = _Html.Replace("{SheetData}", _HtmlData);

        //if (!String.IsNullOrEmpty(_Html))
        //{



        //}
        //else
        //{
        //    Error("PDF Not Generated Data Not Found.");
        //}

    }


    private void GenerateReport(int CompanyID, int EmployeeCode, string IncrementLetterDate, int ReportingCostCenterID)
    {
        DataSet _DsIncrementLetterData = GetIncrementLetterData(CompanyID, EmployeeCode, IncrementLetterDate, ReportingCostCenterID);

        if (_DsIncrementLetterData.Tables.Count > 0)
        {
            System.Data.DataTable _DTEmployeeData = _DsIncrementLetterData.Tables[0];
            System.Data.DataTable _DTEmployeeSalaryData = _DsIncrementLetterData.Tables[1];


            if (_DTEmployeeData.Rows.Count > 0)
            {
                List<EmployeeModel> _ListEmployee = _DTEmployeeData.AsEnumerable().Select(dtRow => new EmployeeModel()
                {
                    EmployeeCode = Convert.ToInt32(dtRow["EmployeeCode"]),
                    Name = Convert.ToString(dtRow["Name"]),
                    DesignationName = Convert.ToString(dtRow["DesignationName"]),
                    LocationName = Convert.ToString(dtRow["LocationName"]),
                    SapCostCenter = Convert.ToString(dtRow["SapCostCenter"]),
                    DateOfJoining = Convert.ToString(dtRow["DateOfJoining"]),
                    DateOfNewIncrement = Convert.ToString(dtRow["DateOfNewIncrement"]),
                    DescriptionOne = Convert.ToString(dtRow["DescriptionOne"]),
                    DescriptionTwo = Convert.ToString(dtRow["DescriptionTwo"]),
                    DescriptionThree = Convert.ToString(dtRow["DescriptionThree"]),
                    DescriptionFour = Convert.ToString(dtRow["DescriptionFour"]),
                    DescriptionFive = Convert.ToString(dtRow["DescriptionFive"])
                }).OrderBy(x => x.EmployeeCode).ToList();


                List<EmployeeSalaryModel> _ListEmployeeSalary = _DTEmployeeSalaryData.AsEnumerable().Select(dtRow => new EmployeeSalaryModel()
                {
                    EmployeeCode = Convert.ToInt32(dtRow["EmployeeCode"]),
                    Allowances = Convert.ToString(dtRow["Allowances"]),
                    NewSalaryRupees = Convert.ToString(dtRow["NewSalaryRupees"]),
                    CurrentSalaryRupees = Convert.ToString(dtRow["CurrentSalaryRupees"]),
                    ChangeRupees = Convert.ToString(dtRow["ChangeRupees"]),
                }).ToList();


                //if (_ListEmployee.Count > 7)
                //{
                //    int _EmployeeCode = 0;
                //    for (int i = 0; i < _ListEmployee.Count; i++)
                //    {
                //        List<EmployeeModel> _Data = _ListEmployee.OrderByDescending(x => x.EmployeeCode).Take(7).Skip(i * 7).ToList();

                //        GeneratePDF(_Data, _ListEmployeeSalary, i);

                //    }
                //}
                //else
                //{
                //    GeneratePDF(_ListEmployee, _ListEmployeeSalary);
                //}
                GeneratePDF(_ListEmployee, _ListEmployeeSalary);


            }
            else
            {
                Error("Employee Not Found For Generate Increment Letter.");
            }

        }
        else
        {
            Error("Tables Not Found For Generate Report.");
        }

    }
    private DataSet GetIncrementLetterData(int CompanyID, int EmployeeCode, string IncrementLetterDate, int ReportingCostCenterID)
    {

        DataSet ds = new DataSet();

        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        //SqlDataAdapter da = new SqlDataAdapter("HCM_EmployeeAllowances", con);
        SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_IncrementLetter", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = Constant.ConnectionTimeout;
        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyID;
        if (EmployeeCode != 0)
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
        else
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = null;

        da.SelectCommand.Parameters.Add("@Month", SqlDbType.Date).Value = DateTime.Parse(IncrementLetterDate);
        if (ReportingCostCenterID != 0)
            da.SelectCommand.Parameters.Add("@ReportingCostCenterId", SqlDbType.Int).Value = ReportingCostCenterID;
        else
            da.SelectCommand.Parameters.Add("@ReportingCostCenterId", SqlDbType.Int).Value = null;

        da.Fill(ds);

        return ds;

    }



    protected class EmployeeModel
    {

        public int EmployeeCode { get; set; }
        public string Name { get; set; }
        public string DesignationName { get; set; }
        public string LocationName { get; set; }
        public string SapCostCenter { get; set; }
        public string DateOfJoining { get; set; }
        public string DateOfNewIncrement { get; set; }
        public string DescriptionOne { get; set; }
        public string DescriptionTwo { get; set; }
        public string DescriptionThree { get; set; }
        public string DescriptionFour { get; set; }
        public string DescriptionFive { get; set; }
    }


    protected class EmployeeSalaryModel
    {

        public int EmployeeCode { get; set; }
        public string Allowances { get; set; }
        public string NewSalaryRupees { get; set; }
        public string CurrentSalaryRupees { get; set; }
        public string ChangeRupees { get; set; }

    }
}