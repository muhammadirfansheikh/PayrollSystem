using DAL;
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Pages_HCM_Report_Tax_Certificate_New : Base
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

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet _DataForCertificate = GetDataForTaxCertificate();
            DataTable _DataTableForEmployeeDetail = _DataForCertificate.Tables[0];
            DataTable _DataTableForCompany = _DataForCertificate.Tables[1];

            if (_DataTableForEmployeeDetail.Rows.Count > 0)
            {
                GenerateTaxCretificatePDF(_DataTableForEmployeeDetail, _DataTableForCompany);
            }
            else
            {
                Error("Data Not Found");
            }



        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }
    }

    private void GenerateTaxCretificatePDF(DataTable dataTbleEmployeeDetail, DataTable dataTableForCompanyDetail)
    {

        string _Path = ddlCompany.SelectedItem.Text + "_TaxCertificates_" + DateTime.Now.ToString("dd-MM-yyyy") + DateTime.Now.Millisecond.ToString() + DateTime.Now.Second + ".pdf";
        PdfWriter _PdfDocumentFileWriter = new PdfWriter(Server.MapPath("~/Uploads/Temp/" + _Path));

        PdfDocument pdf = new PdfDocument(_PdfDocumentFileWriter);

        PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        document = new Document(pdf).SetFont(font).SetFontSize(8);
        pdf.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A4);

        for (int i = 0; i < dataTbleEmployeeDetail.Rows.Count; i++)
        {

            Paragraph _AddNewLinePdf = new Paragraph(new Text("\n"));
            AreaBreak _PageBreak = new AreaBreak();




            //Paragraph _CertificateHeaderHeading = new Paragraph("Certificate of Collection or Deduction of Income Tax (including salary) Emp# : "+dataTbleEmployeeDetail.Rows[i]["EmployeeCode"].ToString()+"").SetFontSize(12).SetBold().SetTextAlignment(TextAlignment.CENTER);
            //document.Add(_CertificateHeaderHeading);


            iText.Layout.Element.Table _Table = new iText.Layout.Element.Table(2, false);

            Cell _Tbl_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("Certificate of Collection or Deduction of Income Tax (including salary) ").SetFontSize(12)).SetBold().SetTextAlignment(TextAlignment.RIGHT).SetWidth(800).SetBorder(iText.Layout.Borders.Border.NO_BORDER);


            Cell _Tbl_Cell2 = new Cell(0, 0)
           .Add(new Paragraph("Emp# :" + dataTbleEmployeeDetail.Rows[i]["EmployeeCode"].ToString() + "")).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(200).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            _Table.AddCell(_Tbl_Cell1);
            _Table.AddCell(_Tbl_Cell2);

            document.Add(_Table);
            Paragraph _UnderRulePara = new Paragraph("(Under Rule 42)").SetBold().SetTextAlignment(TextAlignment.CENTER);
            document.Add(_UnderRulePara);


            iText.Layout.Element.Table _Table1 = new iText.Layout.Element.Table(4, false);

            Cell _Tbl1_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("S No.")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(333).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl1_Cell2 = new Cell(0, 0)
            .Add(new Paragraph("Oriignal/Duplicate")).SetBold().SetTextAlignment(TextAlignment.CENTER).SetWidth(333).SetBorder(Border.NO_BORDER);

            Cell _Tbl1_Cell3 = new Cell(0, 0)
            .Add(new Paragraph("Date Of Issue")).SetBold().SetTextAlignment(TextAlignment.RIGHT).SetWidth(200).SetBorder(Border.NO_BORDER);

            Cell _Tbl1_Cell4 = new Cell(0, 0)
            .Add(new Paragraph(Convert.ToDateTime(txtIssueDate.Text).ToString("dd/MM/yyyy")).SetUnderline()).SetTextAlignment(TextAlignment.LEFT).SetWidth(133).SetBorder(Border.NO_BORDER);

            _Table1.AddCell(_Tbl1_Cell1);
            _Table1.AddCell(_Tbl1_Cell2);
            _Table1.AddCell(_Tbl1_Cell3);
            _Table1.AddCell(_Tbl1_Cell4);
            document.Add(_Table1);

            document.Add(_AddNewLinePdf);


            iText.Layout.Element.Table _Table2 = new iText.Layout.Element.Table(3, false);

            Cell _Tbl2_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("Certified that the sum of Rupees")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(450).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl2_Cell2 = new Cell(0, 0)
            .Add(new Paragraph(Convert.ToDecimal(dataTbleEmployeeDetail.Rows[i]["IncomeTax"]).ToString("#,##.00"))).SetTextAlignment(TextAlignment.RIGHT).SetWidth(150).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl2_Cell3 = new Cell(0, 0)
            .Add(new Paragraph("(Amount of tax collected/deducted in figures)").SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT).SetWidth(400).SetBorder(Border.NO_BORDER);


            _Table2.AddCell(_Tbl2_Cell1);
            _Table2.AddCell(_Tbl2_Cell2);
            _Table2.AddCell(_Tbl2_Cell3);

            document.Add(_Table2);

            iText.Layout.Element.Table _Table3 = new iText.Layout.Element.Table(3, false);

            Cell _Tbl3_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("")).SetTextAlignment(TextAlignment.LEFT).SetWidth(200).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl3_Cell2 = new Cell(0, 0)
            .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["IncomeTaxInWords"].ToString())).SetTextAlignment(TextAlignment.CENTER).SetWidth(600).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl3_Cell3 = new Cell(0, 0)
            .Add(new Paragraph("(Amount in words)").SetFontSize(7)).SetTextAlignment(TextAlignment.RIGHT).SetWidth(200).SetBorder(Border.NO_BORDER);


            _Table3.AddCell(_Tbl3_Cell1);
            _Table3.AddCell(_Tbl3_Cell2);
            _Table3.AddCell(_Tbl3_Cell3);

            document.Add(_Table3);

            document.Add(_AddNewLinePdf);
            document.Add(_AddNewLinePdf);
            document.Add(_AddNewLinePdf);
            document.Add(_AddNewLinePdf);

            iText.Layout.Element.Table _Table4 = new iText.Layout.Element.Table(2, false);

            Cell _Tbl4_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("on account of income tax has been")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(500).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl4_Cell2 = new Cell(0, 0)
            .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["Name"].ToString())).SetTextAlignment(TextAlignment.LEFT).SetWidth(500).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl4_Cell3 = new Cell(0, 0)
           .Add(new Paragraph("deducted/collected from (Name and Address of")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(500).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl4_Cell4 = new Cell(0, 0)
            .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["Address"].ToString())).SetTextAlignment(TextAlignment.LEFT).SetWidth(500).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl4_Cell5 = new Cell(0, 0)
            .Add(new Paragraph("the person whom tax collected/deducted)")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(500).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl4_Cell6 = new Cell(0, 0)
            .Add(new Paragraph("").SetTextAlignment(TextAlignment.LEFT)).SetWidth(500).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl4_Cell7 = new Cell(0, 0)
            .Add(new Paragraph("")).SetTextAlignment(TextAlignment.LEFT).SetWidth(500).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl4_Cell8 = new Cell(0, 0)
            .Add(new Paragraph("In case of an individual, his/her name in full and in case of an association of persons/ company. name and style of the association of persons/company").SetFontSize(5)).SetTextAlignment(TextAlignment.LEFT).SetWidth(500).SetBorder(Border.NO_BORDER);


            _Table4.AddCell(_Tbl4_Cell1);
            _Table4.AddCell(_Tbl4_Cell2);
            _Table4.AddCell(_Tbl4_Cell3);
            _Table4.AddCell(_Tbl4_Cell4);
            _Table4.AddCell(_Tbl4_Cell5);
            _Table4.AddCell(_Tbl4_Cell6);
            _Table4.AddCell(_Tbl4_Cell7);
            _Table4.AddCell(_Tbl4_Cell8);




            document.Add(_Table4);

            iText.Layout.Element.Table _Table5 = new iText.Layout.Element.Table(3, false);

            Cell _Tbl5_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("having National Tax Number")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell2 = new Cell(0, 0)
            .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["NTN"].ToString()).SetTextAlignment(TextAlignment.LEFT)).SetWidth(450).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl5_Cell3 = new Cell(0, 0)
           .Add(new Paragraph("(if any) and").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(250).SetBorder(Border.NO_BORDER);


            Cell _Tbl5_Cell4 = new Cell(0, 0)
            .Add(new Paragraph("holder of CNIC No.")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell5 = new Cell(0, 0)
            .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["CNIC"].ToString()).SetTextAlignment(TextAlignment.LEFT)).SetWidth(450).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl5_Cell6 = new Cell(0, 0)
           .Add(new Paragraph("(in case of an individual)").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(250).SetBorder(Border.NO_BORDER);

            Cell _Tbl5_Cell7 = new Cell(0, 0)
            .Add(new Paragraph("on")).SetBold().SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell8 = new Cell(0, 0)
            .Add(new Paragraph("Various")).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.CENTER).SetWidth(450).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl5_Cell9 = new Cell(0, 0)
           .Add(new Paragraph("(Date of collection/deduction)").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(250).SetBorder(Border.NO_BORDER);


            Cell _Tbl5_Cell10 = new Cell(0, 0)
          .Add(new Paragraph("Or during the period")).SetBold().SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell11 = new Cell(0, 0)
            .Add(
                new iText.Layout.Element.Table(4, false).AddCell(
                    new Cell(0, 0).Add(
                        new Paragraph("From")).SetBold().SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(200).SetBorder(Border.NO_BORDER)
                        ).AddCell(
                     new Cell(0, 0)
           .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["From"].ToString())).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.CENTER).SetWidth(300).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F))

                    )
                        .AddCell(
                    new Cell(0, 0).Add(
                        new Paragraph("TO")).SetBold().SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(200).SetBorder(Border.NO_BORDER)
                        ).AddCell(
                     new Cell(0, 0)
           .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["To"].ToString())).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.CENTER).SetWidth(300).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F))

                    )
                        ).SetWidth(450).SetBorder(Border.NO_BORDER);


            Cell _Tbl5_Cell12 = new Cell(0, 0)
           .Add(new Paragraph("(period of collection/deduction)").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(250).SetBorder(Border.NO_BORDER);

            Cell _Tbl5_Cell13 = new Cell(0, 0)
           .Add(new Paragraph("under section")).SetBold().SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell14 = new Cell(0, 0)
            .Add(new Paragraph(dataTbleEmployeeDetail.Rows[i]["undersection"].ToString())).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.CENTER).SetWidth(450).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl5_Cell15 = new Cell(0, 0)
           .Add(new Paragraph("(specify the Section of Income Tax Ordinance,2001)").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(250).SetBorder(Border.NO_BORDER);

            Cell _Tbl5_Cell16 = new Cell(0, 0)
            .Add(new Paragraph("on account of")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell17 = new Cell(0, 0)
            .Add(new Paragraph("Salary")).SetTextAlignment(TextAlignment.CENTER).SetWidth(450).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl5_Cell18 = new Cell(0, 0)
           .Add(new Paragraph("(Specify nature)").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(250).SetBorder(Border.NO_BORDER);


            Cell _Tbl5_Cell19 = new Cell(0, 0)
            .Add(new Paragraph("vide")).SetBold().SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell20 = new Cell(0, 0)
            .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.CENTER).SetWidth(450).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl5_Cell21 = new Cell(0, 0)
           .Add(new Paragraph("(particulars of LC, Contract etc.)").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(250).SetBorder(Border.NO_BORDER);

            Cell _Tbl5_Cell22 = new Cell(0, 0)
            .Add(new Paragraph("on the value/amount of Rupee")).SetBold().SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(300).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl5_Cell23 = new Cell(0, 0)
            .Add(new Paragraph(Convert.ToDecimal(dataTbleEmployeeDetail.Rows[i]["AmountOfRupee"]).ToString("#,##.00"))).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.CENTER).SetWidth(537).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl5_Cell24 = new Cell(0, 0)
           .Add(new Paragraph("(Gross amount on which tax collected/deducted in figures)").SetFontSize(7)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetTextAlignment(TextAlignment.LEFT).SetWidth(173).SetBorder(Border.NO_BORDER);


            _Table5.AddCell(_Tbl5_Cell1);
            _Table5.AddCell(_Tbl5_Cell2);
            _Table5.AddCell(_Tbl5_Cell3);
            _Table5.AddCell(_Tbl5_Cell4);
            _Table5.AddCell(_Tbl5_Cell5);
            _Table5.AddCell(_Tbl5_Cell6);
            _Table5.AddCell(_Tbl5_Cell7);
            _Table5.AddCell(_Tbl5_Cell8);
            _Table5.AddCell(_Tbl5_Cell9);
            _Table5.AddCell(_Tbl5_Cell10);
            _Table5.AddCell(_Tbl5_Cell11);
            _Table5.AddCell(_Tbl5_Cell12);
           _Table5.AddCell(_Tbl5_Cell13);
           _Table5.AddCell(_Tbl5_Cell14);
           _Table5.AddCell(_Tbl5_Cell15);
           _Table5.AddCell(_Tbl5_Cell16);
           _Table5.AddCell(_Tbl5_Cell17);
           _Table5.AddCell(_Tbl5_Cell18);
           _Table5.AddCell(_Tbl5_Cell19);
           _Table5.AddCell(_Tbl5_Cell20);
           _Table5.AddCell(_Tbl5_Cell21);
           _Table5.AddCell(_Tbl5_Cell22);
           _Table5.AddCell(_Tbl5_Cell23);
            _Table5.AddCell(_Tbl5_Cell24);

            document.Add(_Table5);

           

            Paragraph _WrittenPara = new Paragraph("This is to further certify that the tax collected/deducted has been deposited in the Federal Government Account as per the following details:").SetBold().SetTextAlignment(TextAlignment.LEFT);
            document.Add(_WrittenPara);

           

            iText.Layout.Element.Table _Table13 = new iText.Layout.Element.Table(9, false);
            Cell _Tbl13_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("Date of Deposit")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetWidth(170).SetBorder(Border.NO_BORDER);


            Cell _Tbl13_Cell2 = new Cell(0, 0)
            .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);


          
            Cell _Tbl13_Cell3 = new Cell(0, 0)
            .Add(new Paragraph("SBp/NBp/treasury")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetWidth(170).SetBorder(Border.NO_BORDER);


            Cell _Tbl13_Cell4 = new Cell(0, 0)
            .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);


            Cell _Tbl13_Cell5 = new Cell(0, 0)
            .Add(new Paragraph("Branch/City")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetWidth(170).SetBorder(Border.NO_BORDER);


            Cell _Tbl13_Cell6 = new Cell(0, 0)
            .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);


            Cell _Tbl13_Cell7 = new Cell(0, 0)
            .Add(new Paragraph("Amount (Rupees)")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetWidth(170).SetBorder(Border.NO_BORDER);


            Cell _Tbl13_Cell8 = new Cell(0, 0)
            .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell9 = new Cell(0, 0)
            .Add(new Paragraph("Challan/ treasury No. / CpR No.")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetWidth(200).SetBorder(Border.NO_BORDER);


            _Table13.AddCell(_Tbl13_Cell1);
            _Table13.AddCell(_Tbl13_Cell2);
            _Table13.AddCell(_Tbl13_Cell3);
            _Table13.AddCell(_Tbl13_Cell4);
            _Table13.AddCell(_Tbl13_Cell5);
            _Table13.AddCell(_Tbl13_Cell6);
            _Table13.AddCell(_Tbl13_Cell7);
            _Table13.AddCell(_Tbl13_Cell8);
            _Table13.AddCell(_Tbl13_Cell9);



            Cell _Tbl13_Cell10 = new Cell(0, 0)
           .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell11 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell12 = new Cell(0, 0)
          .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell13 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell14 = new Cell(0, 0)
         .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell15 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell16 = new Cell(0, 0)
        .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell17 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell18 = new Cell(0, 0)
       .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(200).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            _Table13.AddCell(_Tbl13_Cell10);
            _Table13.AddCell(_Tbl13_Cell11);
            _Table13.AddCell(_Tbl13_Cell12);
            _Table13.AddCell(_Tbl13_Cell13);
            _Table13.AddCell(_Tbl13_Cell14);
            _Table13.AddCell(_Tbl13_Cell15);
            _Table13.AddCell(_Tbl13_Cell16);
            _Table13.AddCell(_Tbl13_Cell17);
            _Table13.AddCell(_Tbl13_Cell18);



            Cell _Tbl13_Cell19 = new Cell(0, 0)
          .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell20 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell21 = new Cell(0, 0)
          .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell22 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell23 = new Cell(0, 0)
         .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell24 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell25 = new Cell(0, 0)
        .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell26 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell27 = new Cell(0, 0)
       .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(200).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            _Table13.AddCell(_Tbl13_Cell19);
            _Table13.AddCell(_Tbl13_Cell20);
            _Table13.AddCell(_Tbl13_Cell21);
            _Table13.AddCell(_Tbl13_Cell22);
            _Table13.AddCell(_Tbl13_Cell23);
            _Table13.AddCell(_Tbl13_Cell24);
            _Table13.AddCell(_Tbl13_Cell25);
            _Table13.AddCell(_Tbl13_Cell26);
            _Table13.AddCell(_Tbl13_Cell27);


            Cell _Tbl13_Cell28 = new Cell(0, 0)
        .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell29 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell30 = new Cell(0, 0)
          .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell31 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell32 = new Cell(0, 0)
         .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell33 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell34 = new Cell(0, 0)
        .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell35 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell36 = new Cell(0, 0)
       .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(200).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            _Table13.AddCell(_Tbl13_Cell28);
            _Table13.AddCell(_Tbl13_Cell29);
            _Table13.AddCell(_Tbl13_Cell30);
            _Table13.AddCell(_Tbl13_Cell31);
            _Table13.AddCell(_Tbl13_Cell32);
            _Table13.AddCell(_Tbl13_Cell33);
            _Table13.AddCell(_Tbl13_Cell34);
            _Table13.AddCell(_Tbl13_Cell35);
            _Table13.AddCell(_Tbl13_Cell36);


            Cell _Tbl13_Cell37 = new Cell(0, 0)
        .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell38 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell39 = new Cell(0, 0)
          .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell40 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell41 = new Cell(0, 0)
         .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell42 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell43 = new Cell(0, 0)
        .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(170).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl13_Cell44 = new Cell(0, 0)
           .Add(new Paragraph("")).SetWidth(30).SetBorder(Border.NO_BORDER);

            Cell _Tbl13_Cell45 = new Cell(0, 0)
       .Add(new Paragraph("")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetHeight(20).SetWidth(200).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            _Table13.AddCell(_Tbl13_Cell37);
            _Table13.AddCell(_Tbl13_Cell38);
            _Table13.AddCell(_Tbl13_Cell39);
            _Table13.AddCell(_Tbl13_Cell40);
            _Table13.AddCell(_Tbl13_Cell41);
            _Table13.AddCell(_Tbl13_Cell42);
            _Table13.AddCell(_Tbl13_Cell43);
            _Table13.AddCell(_Tbl13_Cell44);
            _Table13.AddCell(_Tbl13_Cell45);

            document.Add(_Table13);


            document.Add(_AddNewLinePdf);
            document.Add(_AddNewLinePdf);
            document.Add(_AddNewLinePdf);

            Paragraph _WrittenPara2 = new Paragraph("Company/office etc. collecting/deducting the tax:").SetBold().SetTextAlignment(TextAlignment.LEFT);
            document.Add(_WrittenPara2);


            iText.Layout.Element.Table _Table14 = new iText.Layout.Element.Table(4, false);

            Cell _Tbl14_Cell1 = new Cell(0, 0)
            .Add(new Paragraph("Name")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(200).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell2 = new Cell(0, 0)
            .Add(new Paragraph(dataTableForCompanyDetail.Rows[0]["CompanyName"].ToString()).SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl14_Cell3 = new Cell(0, 0)
            .Add(new Paragraph("Signature")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(100).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell4 = new Cell(0, 0)
           .Add(new Paragraph("").SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));




            Cell _Tbl14_Cell5 = new Cell(0, 0)
            .Add(new Paragraph("Address")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(100).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell6 = new Cell(0, 0)
            .Add(new Paragraph(dataTableForCompanyDetail.Rows[0]["Address"].ToString()).SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl14_Cell7 = new Cell(0, 0)
            .Add(new Paragraph("Name")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(100).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell8 = new Cell(0, 0)
           .Add(new Paragraph(txtIssuedBy.Text).SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));



            Cell _Tbl14_Cell9 = new Cell(0, 0)
           .Add(new Paragraph("NTN (if any)")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(100).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell10 = new Cell(0, 0)
            .Add(new Paragraph(dataTableForCompanyDetail.Rows[0]["CompanyNTN"].ToString()).SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl14_Cell11 = new Cell(0, 0)
            .Add(new Paragraph("Designation")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(100).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell12 = new Cell(0, 0)
           .Add(new Paragraph(txtDesignation.Text).SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));


            Cell _Tbl14_Cell13 = new Cell(0, 0)
           .Add(new Paragraph("Date")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(100).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell14 = new Cell(0, 0)
            .Add(new Paragraph(Convert.ToDateTime(txtIssueDate.Text).ToString("dd/MM/yyyy")).SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));

            Cell _Tbl14_Cell15 = new Cell(0, 0)
            .Add(new Paragraph("Seal")).SetBold().SetTextAlignment(TextAlignment.LEFT).SetWidth(100).SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Cell _Tbl14_Cell16 = new Cell(0, 0)
           .Add(new Paragraph("").SetTextAlignment(TextAlignment.LEFT)).SetWidth(400).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5F));



            _Table14.AddCell(_Tbl14_Cell1);
            _Table14.AddCell(_Tbl14_Cell2);
            _Table14.AddCell(_Tbl14_Cell3);
            _Table14.AddCell(_Tbl14_Cell4);
            _Table14.AddCell(_Tbl14_Cell5);
            _Table14.AddCell(_Tbl14_Cell6);
            _Table14.AddCell(_Tbl14_Cell7);
            _Table14.AddCell(_Tbl14_Cell8);
            _Table14.AddCell(_Tbl14_Cell9);
            _Table14.AddCell(_Tbl14_Cell10);
            _Table14.AddCell(_Tbl14_Cell11);
            _Table14.AddCell(_Tbl14_Cell12);
            _Table14.AddCell(_Tbl14_Cell13);
            _Table14.AddCell(_Tbl14_Cell14);
            _Table14.AddCell(_Tbl14_Cell15);
            _Table14.AddCell(_Tbl14_Cell16);


            document.Add(_Table14);
            ///Page Break  
            document.Add(_PageBreak);
        }
        pdf.RemovePage(pdf.GetNumberOfPages());
        document.Close();




        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + _Path);
        Response.TransmitFile(Server.MapPath("~/Uploads/Temp/" + _Path));
        Response.End();



    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }


    public DataSet GetDataForTaxCertificate()
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("RPT_TaxCertificate", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = Constant.ConnectionTimeout;

        da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime(txtFromDate.Text);
        da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime(txtToDate.Text);
        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Convert.ToInt32(ddlCompany.SelectedValue); ;

        if (!String.IsNullOrEmpty(txtEmployeeCode.Text))
            da.SelectCommand.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = Convert.ToInt32(txtEmployeeCode.Text);




        da.Fill(ds);

        return ds;

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
    }
}