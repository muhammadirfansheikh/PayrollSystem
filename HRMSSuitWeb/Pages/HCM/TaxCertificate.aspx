<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaxCertificate.aspx.cs" Inherits="Pages_HCM_TaxCertificate" %>

<!DOCTYpE HTML pUBLIC "-//W3C//Dtd HTML 4.01//EN" "http://www.w3.org/tr/html4/strict.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>pdf-html</title>
    <meta name="generator" content="BCL easyConverter SDK 5.0.252">
    <meta name="author" content="Khurram.Shehzad">
    <meta name="title" content="DataWindow">
     <!-- Mainly scripts -->
    <script src="../../js/jquery-2.1.1.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.min.js"></script>
    <style type="text/css">
        body { margin-top: 0px; margin-left: 0px; }
        #id1_1 { border: none; margin: 0px 0px 0px 115px; padding: 0px; border: none; width: 619px; overflow: hidden; }
        #id1_2 { border: none; margin: 0px 0px; padding: 0px; border: none; width: 800px; overflow: hidden; }
        #id1_2 #id1_2_1 { float: left; border: none; margin: 0px 0px 0px 0px; padding: 0px; border: none; width: 600px; overflow: hidden; }
        #id1_2 #id1_2_2 { float: left; border: none; margin: 16px 0px 0px 10px; padding: 0px; border: none; width:190px; overflow: hidden; }
        #id1_3 { border: none; margin: 13px 0px 0px 244px; padding: 0px; border: none; width: 356px; overflow: hidden; }
        #id1_4 { border: none; margin: 7px 0px 0px; padding: 0px; border: none; width: 800px; overflow: hidden; }
        #id1_4 #id1_4_1 { float: left; border: none; margin: 0px 0px 0px 0px; padding: 0px; border: none; width: 200px; overflow: hidden; }
        #id1_4 #id1_4_2 { float: left; border: none; margin: 5px 0px 0px 0px; padding: 0px; border: none; width: 400px; overflow: hidden; }
        #id1_4 #id1_4_3 { float: left; border: none; margin: 3px 0px 0px 10px; padding: 0px; border: none; width: 190px; overflow: hidden; }
        #id1_5 { border: none; margin: 40px 20px 20px; padding: 0px; border: none; width: 760px; overflow: hidden; }
        #id1_6 { border: none; margin: 7px 0px 0px 7px; padding: 0px; border: none; width: 727px; overflow: hidden; }
        #id1_6 #id1_6_1 { float: left; border: none; margin: 2px 0px 0px 0px; padding: 0px; border: none; width: 127px; overflow: hidden; }
        #id1_6 #id1_6_2 { float: left; border: none; margin: 2px 0px 0px 0px; padding: 0px; border: none; width: 195px; overflow: hidden; }
        #id1_6 #id1_6_3 { float: left; border: none; margin: 2px 0px 0px 0px; padding: 0px; border: none; width: 140px; overflow: hidden; }
        #id1_6 #id1_6_4 { float: left; border: none; margin: 0px 0px 0px 0px; padding: 0px; border: none; width: 79px; overflow: hidden; }
        #id1_6 #id1_6_5 { float: left; border: none; margin: 0px 0px 0px 0px; padding: 0px; border: none; width: 186px; overflow: hidden; }
        #id1_7 { border: none; margin: 0px 0px 0px 0px; padding: 0px; border: none; width: 800px; overflow: hidden; }
        #id1_8 { border: none; margin: 9px 0px 0px 547px; padding: 0px; border: none; width: 187px; overflow: hidden; }
        #p1dimg1 { position: absolute; top: 99px; left: 0px; z-index: -1; width: 696px; height: 579px; }
        #p1dimg1 #p1img1 { width: 696px; height: 579px; }
        #p1inl_img1 { position: relative; width: 0px; height: 14px; }
        #p1inl_img2 { position: relative; width: 0px; height: 14px; }
        #p1inl_img3 { position: relative; width: 0px; height: 14px; }
        #p1inl_img4 { position: relative; width: 0px; height: 14px; }
        #p1inl_img5 { position: relative; width: 0px; height: 14px; }
        #p1inl_img6 { position: relative; width: 0px; height: 14px; }
        #p1inl_img7 { position: relative; width: 0px; height: 14px; }
        #p1inl_img8 { position: relative; width: 0px; height: 14px; }
        #p1inl_img9 { position: relative; width: 0px; height: 14px; }
        #p1inl_img10 { position: relative; width: 0px; height: 14px; }
        #p1inl_img11 { position: relative; width: 0px; height: 14px; }
        #p1inl_img12 { position: relative; width: 0px; height: 14px; }
        #p1inl_img13 { position: relative; width: 0px; height: 14px; }
        #p1inl_img14 { position: relative; width: 0px; height: 14px; }
        #p1inl_img15 { position: relative; width: 0px; height: 14px; }
        #p1inl_img16 { position: relative; width: 0px; height: 14px; }
        #p1inl_img17 { position: relative; width: 0px; height: 14px; }
        #p1inl_img18 { position: relative; width: 0px; height: 14px; }
        #p1inl_img19 { position: relative; width: 0px; height: 14px; }
        #p1inl_img20 { position: relative; width: 0px; height: 14px; }
        #p1inl_img21 { position: relative; width: 0px; height: 14px; }
        #p1inl_img22 { position: relative; width: 0px; height: 14px; }
        .ft0 { font: bold 16px 'Arial'; line-height: 19px; }
        .ft1 { font: 1px 'Arial'; line-height: 12px; }
        .ft2 { font: bold 13px 'Arial'; line-height: 12px; }
        .ft3 { font: 11px 'Arial'; line-height: 13px; }
        .ft4 { font: 1px 'Arial'; line-height: 1px; }
        .ft5 { font: bold 13px 'Arial'; line-height: 16px; }
        .ft6 { font: 11px 'Arial'; line-height: 12px; }
        .ft7 { font: 12px 'Arial'; line-height: 14px; }
        .ft8 { font: 1px 'Arial'; line-height: 4px; }
        .ft9 { font: 1px 'Arial'; line-height: 3px; }
        .ft10 { font: 11px 'Arial'; line-height: 16px; }
        .ft11 { font: 1px 'Arial'; line-height: 5px; }
        .ft12 { font: 9px 'Arial'; line-height: 14px; }
        .ft13 { font: 12px 'Arial'; line-height: 15px; }
        .ft14 { font: bold 11px 'Arial'; line-height: 14px; }
        .ft15 { font: bold 11px 'Arial'; line-height: 12px; }
        .ft16 { font: 1px 'Arial'; line-height: 2px; }
        .ft17 { font: 1px 'Arial'; line-height: 11px; }
        .ft18 { font: 1px 'Arial'; line-height: 7px; }
        .ft19 { font: 1px 'Arial'; line-height: 6px; }
        .ft20 { font: bold 9px 'Tahoma'; line-height: 11px; position: relative; bottom: 2px; }
        .ft21 { font: bold 7px 'Tahoma'; line-height: 8px; position: relative; bottom: 2px; }
        .ft22 { font: bold 8px 'Tahoma'; line-height: 10px; position: relative; bottom: 2px; }
        .ft23 { font: bold 9px 'Tahoma'; line-height: 11px; }
        .p0 { text-align: left; margin-top: 0px; margin-bottom: 0px; }
        .p1 { text-align: left; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p2 { text-align: center; margin-top: 0px; margin-bottom: 5px; white-space: nowrap; }
        .p3 { text-align: left; padding-left: 0px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p4 { text-align: center; padding-left: 24px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p5 { text-align: center; padding-right: 0px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; border-bottom: #000000 1px solid; }
        .p6 { text-align: right; padding-right: 42px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p7 { text-align: left; padding-left: 0px; margin-top: 18px; margin-bottom: 0px; }
        .p8 { text-align: left; padding-left: 0px; margin-top: 25px; margin-bottom: 0px; }
        .p9 { text-align: left; margin-top: 5px; margin-bottom: 0px; }
        .p10 { text-align: left; margin-top: 6px; margin-bottom: 0px; }
        .p11 { text-align: center; padding-left:0px; margin-top: 0px; margin-bottom: 0px; border-bottom: #000000 1px solid; }
        .p01 { text-align: center; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; border-bottom: #000000 1px solid; }
        .p12 { text-align: left; padding-left: 20px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p13 { text-align: left; padding-left: 12px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p14 { text-align: left; padding-left: 18px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p15 { text-align: center; padding-left:0px; margin-top: 9px; margin-bottom: 0px; border-bottom: #000000 1px solid; }
        .p16 { text-align: center; padding-left:0px; margin-top: 9px; margin-bottom: 0px; border-bottom: #000000 1px solid; }
        .p17 { text-align: center; padding-left:0px; margin-top: 9px; margin-bottom: 0px; border-bottom: #000000 1px solid; }
        .p18 { text-align: center; padding-left:0px; margin-top: 9px; margin-bottom: 0px; border-bottom: #000000 1px solid; }
        .p19 { text-align: left; margin-top: 8px; margin-bottom: 0px; }
        .p20 { text-align: left; padding-left: 0px; padding-right: 0px; margin-top: 3px; margin-bottom: 0px; text-indent: -2px; }
        .p21 { text-align: left; margin-top: 2px; margin-bottom: 0px; }
        .p22 { text-align: left; padding-right:0px; margin-top: 10px; margin-bottom: 0px; }
        .p23 { text-align: left; padding-left: 2px; margin-top: 0px; margin-bottom: 0px; text-indent: -2px; }
        .p24 { text-align: left; padding-left: 3px; margin-top: 0px; margin-bottom: 0px; }
        .p25 { text-align: left; padding-left: 40px; margin-top: 0px; margin-bottom: 0px; }
        .p26 { text-align: right; padding-right: 18px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p27 { text-align: right; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .p28 { text-align: left; padding-left: 533px; margin-top: 0px; margin-bottom: 0px; }
        .p29 { text-align: left; padding-left: 533px; margin-top: 1px; margin-bottom: 0px; }
        .p30 { text-align: left; padding-left: 1px; margin-top: 0px; margin-bottom: 0px; white-space: nowrap; }
        .td0 { padding: 0px; margin: 0px; width: 37px; vertical-align: bottom; }
        .td1 { padding: 0px; margin: 0px; width: 130px; vertical-align: bottom; }
        .td2 { padding: 0px; margin: 0px; width: 95px; vertical-align: bottom; }
        .td3 { padding: 0px; margin: 0px; width: 139px; vertical-align: bottom; }
        .td4 { padding: 0px; margin: 0px; width: 167px; vertical-align: bottom; }
        .td5 { padding: 0px; margin: 0px; width: 231px; vertical-align: bottom; }
        .td6 { padding: 0px; margin: 0px; width: 3px; vertical-align: bottom; }
        .td7 { border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 136px; vertical-align: bottom; }
        .td8 { padding: 0px; margin: 0px; width: 234px; vertical-align: bottom; }
        .td9 { padding: 0px; margin: 0px; width: 244px; vertical-align: bottom; }
        .td10 { padding: 0px; margin: 0px; width: 100px; vertical-align: bottom; }
        .td11 { padding: 0px; margin: 0px; width: 71px; vertical-align: bottom; }
        .td12 { padding: 0px; margin: 0px; width: 136px; vertical-align: bottom; }
        .td13 { padding: 0px; margin: 0px; width: 31px; vertical-align: bottom; }
        .td14 { border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 111px; vertical-align: bottom; }
        .td15 { padding: 0px; margin: 0px; width: 42px; vertical-align: bottom; }
        .td16 { border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 111px; vertical-align: bottom; }
        .th17 { border-right: #000000 1px solid; border-top: #000000 1px solid; border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 91px; vertical-align: bottom; }
        .th117 { border-left: #000000 1px solid; border-right: #000000 1px solid; border-top: #000000 1px solid; border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 91px; vertical-align: bottom; }
        .td17 { border-left: #000000 1px solid; border-right: #000000 1px solid; border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 91px; vertical-align: bottom; }
        .td18 { border-right: #000000 1px solid; border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 180px; vertical-align: bottom; }
        .td19 { border-right: #000000 1px solid; border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 170px; vertical-align: bottom; }
        .td20 { border-right: #000000 1px solid; border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 94px; vertical-align: bottom; }
        .td21 { border-right: #000000 1px solid; border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 138px; vertical-align: bottom; }
        .td22 { padding: 0px; margin: 0px; width: 0px; vertical-align: bottom; }
        .td23 { padding: 0px; margin: 0px; width: 352px; vertical-align: bottom; }
        .td24 { padding: 0px; margin: 0px; width: 102px; vertical-align: bottom; }
        .td25 {border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 206px; vertical-align: bottom; }
        .td26 { padding: 0px; margin: 0px; width: 76px; vertical-align: bottom; }
        .td27 { border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 276px; vertical-align: bottom; }
        .td28 { border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 206px; vertical-align: bottom; }
        .td29 {border-bottom: #000000 1px solid; padding: 0px; margin: 0px; width: 276px; vertical-align: bottom; }
        .tr0 { height: 12px; }
        .tr1 { height: 21px; }
        .tr2 { height: 27px; }
        .tr3 { height: 4px; }
        .tr4 { height: 3px; }
        .tr5 { height: 18px; }
        .tr6 { height: 14px; }
        .tr7 { height: 16px; }
        .tr8 { height: 5px; }
        .tr9 { height: 15px; }
        .tr10 { height: 26px; }
        .tr11 { height: 19px; }
        .tr12 { height: 2px; }
        .tr13 { height: 20px; }
        .tr14 { height: 13px; }
        .tr15 { height: 33px; }
        .tr16 { height: 17px; }
        .tr17 { height: 11px; }
        .tr18 { height: 25px; }
        .tr19 { height: 6px; }
        .tr20 { height: 7px; }
        .t0 { width:600px; margin-left: 1px; font: 11px 'Arial'; }
        .t1 { margin-top: 34px; font: 11px 'Arial'; }
        .t2 { width: 207px; font: 11px 'Arial'; }
        .t3 { width: 336px; margin-left: 31px; margin-top: 8px; font: 11px 'Arial'; }
        .t4 { width: 678px; font: 11px 'Arial'; }
        .t5 { width: 760px; margin-left: 20px; margin-right: 20px; margin-top:10px; font: bold 11px 'Arial'; }
    </style>
</head>
<body>
    <input style="text-align: right; margin: 0px;" type="button" class="btn btn-info pull-right btnnontprint" value="Export Excel" onclick="excelOutWithouHiddenFields('.tableStaffLoanListing')" />
    <input style="text-align: right; margin: 0px;" type="button" class="btn btn-info pull-right btnnontprint" value="Export PDF" onclick="makePDF()" />
    <input style="text-align: right; margin: 0px;" type="button" class="btn btn-info pull-right btnnontprint" value="Print" onclick="printDiv()" />
    <div id="myDiv"> 
        <table id="tblData" class="tableStaffLoanListing" cellpadding="0" cellspacing="0" style="width:740px; padding: 10px 30px;">
        <tr>
            <td style="text-align:center;">
                <p class="ft0">Certificate of Collection or Deduction of Income Tax (including salary)</p>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">

                            <span class="p2 ft2">(Under rule 42)<br />
                            Original/Duplicate</span>

            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p1 ft3">S.No. <span id="Span1" class="p6 ft6" runat="server"></span></p>
                        </td>
                        <td style="width: 400px; padding: 3px; vertical-align: bottom;">
                           <p>&nbsp;</p>
                        </td>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p1 ft6">Date of Issue. <span id="pDateOfIssue" class="p6 ft6" runat="server"></span></p>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p1 ft6">Certified that the sum of Rupees</p>
                        </td>
                        <td style="width: 400px; padding: 3px; vertical-align: bottom; text-align:center">
                            <p id="pIncomeTax" class="p5 ft7" runat="server"></p>
                        </td>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p7 ft12">(Amount of tax collected/deducted in figures)</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p1 ft6">&nbsp;</p>
                        </td>
                        <td style="width: 400px; padding: 3px; vertical-align: bottom; text-align:center">
                            <p id="pIncomeTaxAlphabets" class="p01 ft10" runat="server"></p>
                        </td>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p8 ft12" style="margin-top: 5px"></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p1 ft6">on account of Income tax has been</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                            <p id="pTaxAccountantName" class="p01 ft6" runat="server"></p>
                        </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p1 ft6">deducted/collected from (Name and address of</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                            <p id="pDeductedAddress" class="p01 ft6" runat="server"></p>
                        </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p1 ft10">the person from whom tax collected/deducted)</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                            <p class="p01 ft4">&nbsp;</p>
                        </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                            <p class="p0 ft12">In case of an individual, his/her name in full and in case of an association of persons/ company. name and style of the association of persons/company</p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p0 ft10">having National Tax Number</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                            <p id="phavingNationalTaxNo" runat="server" class="p11 ft10"></p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p0 ft12">(if any) and</p>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p9 ft10">holder of CNIC No.</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                            <p id="pHolderCINC" runat="server" class="p11 ft10"></p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p19 ft12">(in case of an individual)</p>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                             <p class="p10 ft10">on</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                             <p id="pOn" runat="server" class="p15 ft10"></p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                           <p class="p19 ft12">(Date of collection/deduction)</p>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: middle;">
                            <p class="p10 ft10">Or during the period</p>
                           
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: middle;">
                            <table cellpadding="0" cellspacing="0" class="t3">
                                <tbody><tr>
                                    <td class="tr9 td13">
                                        <p class="p1 ft6">From</p>
                                    </td>
                                    <td class="tr6 td14" style="text-align:left">
                                        <p id="pFromDate" runat="server" class="p12 ft6"></p>
                                    </td>
                                    <td class="tr9 td15">
                                        <p class="p13 ft6">To</p>
                                    </td>
                                    <td class="tr6 td16" style="text-align:left">
                                        <p id="pToDate" runat="server" class="p14 ft6"></p>
                                    </td>
                                </tr>
                            </tbody></table>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: middle;">
                            <p class="p19 ft12">(period of collection/deduction)</p>

                         </td>
                    </tr>
                        <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                             <p class="p10 ft10">under section</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                             <p id="pUnderSection" runat="server" class="p15 ft10"></p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                           <p class="p19 ft12">(specify the Section of Income Tax Ordinance,2001)</p>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p10 ft10">on account of</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                           <p id="pOnAccountOf" runat="server" class="p15 ft10"></p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p21 ft12">(Specify nature)</p>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p10 ft10">vide</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom; text-align:center">
                            <p runat="server" id="pVide" class="p15 ft10">&nbsp;</p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p10 ft12">(particulars of LC, Contract etc.)</p>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p9 ft10">on the value/amount of Rupee</p>
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;text-align:center">
                            <p id="pAmountInRupee" runat="server" class="p17 ft13"></p>
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            <p class="p22 ft12">(Gross amount on which tax collected/deducted in figures)</p>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                         <td style="width: 400px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                         <td style="width: 200px; padding: 3px; vertical-align: bottom;">
                            &nbsp;
                         </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
               <p class="p23 ft3" style="padding:8px">This is to further certify that the tax collected/deducted has been deposited in the Federal Government Account as per the following details:</p>
            </td>
        </tr>
        <tr>
            <td>
            <table cellpadding="0" cellspacing="0" class="t4" style="width:100%">
                <tr>
                    <th class="tr6 th117 th17">Date of Deposit</th>
                    <th class="tr6 th17">SBp/NBp/ treasury</th>
                    <th class="tr6 th17">Branch/City</th>
                    <th class="tr6 th17">Amount<br />(Rupees)</th>
                    <th class="tr6 th17">Challan<br />/ treasury No. / CpR No.</th>
                </tr>
                <tr>
                    <td class="tr6 td17">
                        <p class="p26 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td18">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td19">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td20">
                        <p class="p27 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td21">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td class="tr6 td17">
                        <p class="p26 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td18">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td19">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td20">
                        <p class="p27 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td21">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td class="tr6 td17">
                        <p class="p26 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td18">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td19">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td20">
                        <p class="p27 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td21">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td class="tr6 td17">
                        <p class="p26 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td18">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td19">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td20">
                        <p class="p27 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td21">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td class="tr6 td17">
                        <p class="p26 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td18">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td19">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td20">
                        <p class="p27 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td21">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td class="tr6 td17">
                        <p class="p26 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td18">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td19">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td20">
                        <p class="p27 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td21">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td class="tr6 td17">
                        <p class="p26 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td18">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td19">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr6 td20">
                        <p class="p27 ft6">&nbsp;</p>
                    </td>
                    <td class="tr6 td21">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
            </table>
            <p class="p28 ft10">&nbsp;</p>
            <table style="width:100%" cellpadding="0" cellspacing="0" class="t5">
                <tr>
                    <td colspan="4" class="tr6 td23" style="width:800px">
                        <p class="p1 ft6">Company/office etc. collecting/deducting the tax:</p>
                    </td>
                </tr>
                <tr>
                   
                    <td class="tr2 td26" style="width:100px">
                        <p class="p1 ft15">Name.</p>
                    </td>
                    <td class="tr10 td27" style="width:400px;text-align:left">
                        <p id="pCompanyName" runat="server" class="p1 ft6"></p>
                    </td>
                    <td class="tr2 td24"style="width:100px">
                        <p class="p12 ft15">Signature</p>
                    </td>
                    <td class="tr10 td28" style="width:200px;text-align:left">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                </tr>
                <tr>
                    
                    <td class="tr7 td26"style="width:100px">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr9 td27"style="width:400px">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td class="tr7 td24"style="width:100px">
                        <p class="p12 ft15">Name</p>
                    </td>
                    <td class="tr7 td25" style="width:200px;text-align:left">
                        <p id="pCustomerName" runat="server" class="p1 ft6"></p>
                    </td>
                </tr>
                <tr>
                    
                    <td class="tr11 td26"style="width:100px">
                        <p class="p1 ft15">Address.</p>
                    </td>
                    <td  class="tr5 td27" style="width:400px;text-align:left">
                        <p id="pCompanyAddress" runat="server" class="p1 ft6"></p>
                    </td>
                    <td class="tr3 td24"style="width:100px">
                        <p class="p1 ft8">&nbsp;</p>
                    </td>
                    <td class="tr4 td28"style="width:200px">
                        <p class="p1 ft9">&nbsp;</p>
                    </td>
                </tr>

                <tr>
                    
                    <td class="tr4 td26"style="width:100px">
                        <p class="p1 ft9">&nbsp;</p>
                    </td>
                    <td class="tr9 td27" style="width:400px">
                        <p class="p1 ft4">&nbsp;</p>
                    </td>
                    <td  class="tr13 td24"style="width:100px">
                        <p class="p12 ft15">Designation</p>
                    </td>
                    <td class="tr12 td28"style="width:200px">
                        <p id="pDesignation" runat="server" class="p1 ft6"></p>
                    </td>
                </tr>
                <tr>
                   
                    <td  class="tr15 td26"style="width:100px">
                        <p class="p1 ft15">NTN (if any)</p>
                    </td>
                <td  class="tr11 td29"style="width:400px">
                        <p id="pCompanyNTN" runat="server" class="p1 ft6"></p>
                    </td>
                     <td class="tr1 td24"style="width:100px">
                        <p class="p12 ft15">Seal</p>
                    </td>
                    <td class="tr3 td28"style="width:200px">
                        <p class="p1 ft8">&nbsp;</p>
                    </td>
                </tr>
       
                <tr>
                   
                    <td class="tr18 td26"style="width:100px">
                        <p class="p1 ft15">Date.</p>
                    </td>
                 <td  class="tr13 td29"style="width:400px;text-align:left">
                        <p id="pDateOFIssueSecond" runat="server" class="p1 ft6"></p>
                    </td>
                    <td class="tr8 td15"style="width:100px">
                        <p class="p1 ft11">&nbsp;</p>
                    </td>
                       <td class="tr3 td28"style="width:200px">
                        <p class="p1 ft8">&nbsp;</p>
                    </td>
                </tr>
     
             
            </table>
        </td>
    </tr>
    </table>

    </div>
   
    <script src="../../js/Page_JS/Constant.js"></script>
     <script src="../../js/jquery.table2excel.js"></script>
    <script>
        $(document).ready(function () {
            printDiv();

        })
        function printDiv() {

            $(".btnnontprint").css("display", "none");
            window.print();

            setTimeout(function () { $(".btnnontprint").css("display", "inline-block"); }, 1000);

            //$('body').html(originalContent);



        }
        function makePDF() {

            var quotes = document.getElementById('myDiv');

            html2canvas(quotes, {
                scale: 2,
                onrendered: function (canvas) {

                    //! MAKE YOUR PDF
                    var pdf = new jsPDF('p', 'pt', 'letter');

                    for (var i = 0; i <= quotes.clientHeight / 980; i++) {
                        //! This is all just html2canvas stuff
                        var srcImg = canvas;
                        var sX = 0;
                        var sY = 980 * i; // start 980 pixels down for every new page
                        var sWidth = 900;
                        var sHeight = 980;
                        var dX = 0;
                        var dY = 0;
                        var dWidth = 900;
                        var dHeight = 980;

                        window.onePageCanvas = document.createElement("canvas");
                        onePageCanvas.setAttribute('width', 900);
                        onePageCanvas.setAttribute('height', 980);
                        var ctx = onePageCanvas.getContext('2d');



                        // details on this usage of this function: 
                        // https://developer.mozilla.org/en-US/docs/Web/API/Canvas_API/Tutorial/Using_images#Slicing
                        ctx.drawImage(srcImg, sX, sY, sWidth, sHeight, dX, dY, dWidth, dHeight);

                        // document.body.appendChild(canvas);
                        var canvasDataURL = onePageCanvas.toDataURL("image/png", 1.0);

                        var width = onePageCanvas.width;
                        var height = onePageCanvas.clientHeight;

                        //! If we're on anything other than the first page,
                        // add another page
                        if (i > 0) {
                            pdf.addPage(612, 791); //8.5" x 11" in pts (in*72)
                        }
                        //! now we declare that we're working on that page
                        pdf.setPage(i + 1);
                        //! now we add content to that page!
                        pdf.addImage(canvasDataURL, 'PNG', 20, 40, (width * .62), (height * .62));

                    }
                    //! after the for loop is finished running, we save the pdf.
                    pdf.save('TaxCertificate.pdf');
                }
            });
        }



        function exportTableToExcel(tableID, filename = '') {
            var downloadLink;
            var dataType = 'application/vnd.ms-excel';
            var tableSelect = document.getElementById(tableID);
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

            // Specify file name
            filename = filename ? filename + '.xls' : 'excel_data.xls';

            // Create download link element
            downloadLink = document.createElement("a");

            document.body.appendChild(downloadLink);

            if (navigator.msSaveOrOpenBlob) {
                var blob = new Blob(['\ufeff', tableHTML], {
                    type: dataType
                });
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                // Create a link to the file
                downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

                // Setting the file name
                downloadLink.download = filename;

                //triggering the function
                downloadLink.click();
            }
        }
    </script>
</body>
</html>

