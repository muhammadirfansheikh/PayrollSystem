﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DAL;


public partial class Controls_Shared_DocumentsControl : System.Web.UI.UserControl
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    private const string TEMP_FOLDER = "/temp/";

    public bool ShowHeader
    {
        get { return trHeader.Visible; }
        set { trHeader.Visible = value; }
    }


    public int Limits = 0;

    public int? limit
    {
        get
        {

            ////if (decimal.TryParse(txt.Text, out aux)) return aux;
            return Limits = DBHelper.getInteger(limit);

        }
        set
        {
            if (value == null) Limits = 0;
            else Limits = DBHelper.getInteger(value.Value);// ("n" + Deciamls);
        }
    }



    public DocumentsManager1.DOCUMENTS_TYPE? DocumentsType
    {
        get
        {
            try { return (DocumentsManager1.DOCUMENTS_TYPE)ViewState["WFType1"]; }
            catch (Exception err) { return null; }
        }
        set { ViewState["WFType1"] = value; }
    }

    DocumentsManager1 _manager;
    /// <summary>
    /// Returns null if no DocumentsType is set
    /// </summary>
    private DocumentsManager1 manager
    {
        get
        {
            if (_manager == null && DocumentsType != null)
                _manager = new DocumentsManager1(DocumentsType.Value);
            return _manager;
        }
    }

    public bool ReadOnly
    {
        get { return !tblUploadControls.Visible; }
        set
        {
            tblUploadControls.Visible
                = grdDocumentsNEW.Columns[4].Visible
                = !value;
        }
    }

    /// <summary>
    /// If TargetId is null we store the uploaded files in the temporary folder
    /// </summary>
    public object TargetId
    {
        get { return ViewState["TargetId1"]; }
        //        set { ViewState["TargetId"] = value; }
    }

    public bool TemporaryMode
    {
        get { return TargetId == null; }
    }

    private List<DocumentsManager1.Document> documents
    {
        get
        {
            if (ViewState["documentsNew"] == null)
                ViewState["documentsNew"] = new List<DocumentsManager1.Document>();
            return (List<DocumentsManager1.Document>)ViewState["documentsNew"];
        }
    }

    public string Header
    {
        get { return lblHeader.Text; }
        set { lblHeader.Text = value; }
    }

    public int FilesCount
    {
        get { return grdDocumentsNEW.Rows.Count; }
    }

    public void ResetDocument()
    {
        grdDocumentsNEW.SelectedIndex = -1;
        grdDocumentsNEW.DataSource = null;
        grdDocumentsNEW.DataBind();
        ViewState["TargetId1"] = null;
        tblUploadControls.Visible = true;
    }








    private string composeTemporaryFileName(string extension)
    {
        Guid id = Guid.NewGuid();
        string rnd = id.ToString().ToUpper().Replace('_', '2').Replace('-', '1').Substring(0, 20);

        return ""
                + (manager == null ? "" : manager.Prefix)
                + rnd //+ new Random().Next()
                + extension;
    }

    public void SetToTemporaryCopingDocuments()
    {
        if (TemporaryMode) return;



        DataTable documentsTable = manager.GetDocuments(TargetId);
        foreach (DataRow documentRow in documentsTable.Rows)
        {
            string fileName = documentRow["FileName"].ToString();
            string extension = SIMUtils.FilesAndStreams.GetFileNameExtension(fileName);
            string newTempFileName = composeTemporaryFileName(extension);
            System.IO.File.Copy(
                Server.MapPath(manager.FolderWithFinalSlash + fileName)
                , Server.MapPath(TEMP_FOLDER + newTempFileName));

            DocumentsManager1.Document document = new DocumentsManager1.Document();

            document.OriginalFileName = documentRow["OriginalFileName"].ToString();
            document.FileType = documentRow["FileType"].ToString();
            document.FileName = newTempFileName;
            document.Comments = documentRow["Comments"].ToString();
            document.FilePathWithFinalSlash = TEMP_FOLDER;
            //document.AddedOn = (DateTime)documentRow["CreatedOn"];


            this.documents.Add(document);

        }

        ReadOnly = false;
        ViewState["TargetId1"] = null;

        grdDocumentsNEW.DataSource = documents;
        grdDocumentsNEW.DataBind();
    }

    public void SetTargetIdAndShowAsReadOnly(object TargetId)
    {
        ViewState["TargetId1"] = TargetId;

        ReadOnly = true;

        grdDocumentsNEW.DataSource = manager.GetDocuments(TargetId);
        grdDocumentsNEW.DataBind();
    }

    public void SetTargetIdAndShowInEditMode(object TargetId)
    {
        ViewState["TargetId1"] = TargetId;
        Base bs = new Base();
        ReadOnly = false;

        grdDocumentsNEW.DataSource = manager.GetDocuments(TargetId);
        grdDocumentsNEW.DataBind();

    }

    public void SetTargetIdAndMoveTemporaryFilesToDestinationFolder(object TargetId)
    {
        if (!TemporaryMode) throw new Exception("We are not in temporary mode anymore.");

        ViewState["TargetId1"] = TargetId;

        foreach (DocumentsManager1.Document document in documents)
        {
            string newFileName = manager.ComposeFileName(TargetId, document.OriginalFileName);
            // and now we move the file to the right destination:
            System.IO.File.Move(
                Server.MapPath(document.FullPath)
                , Server.MapPath(manager.FolderWithFinalSlash + newFileName));

            document.FileName = newFileName;
            document.TargetId = TargetId;
            manager.AddDocument(document);
        }
        ViewState["documentsNew"] = null;
    }

    public void Clear()
    {
        ViewState["documentsNew"] = null;
        grdDocumentsNEW.DataSource = documents;
        grdDocumentsNEW.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.grdDocumentsNEW);
    }
    protected bool ValidateFile(string FileName)
    {
        int errflag = 1;
        FileName = FileName.ToLower();
        if (FileName.IndexOf(".gif") != -1)
        {
            errflag = 0;
        }
        if (FileName.IndexOf(".jpg") != -1)
        {
            errflag = 0;
        }
        if (FileName.IndexOf(".doc") != -1)
        {
            errflag = 0;
        }
        if (FileName.IndexOf(".pdf") != -1)
        {
            errflag = 0;
        }
        if (FileName.IndexOf(".xls") != -1)
        {
            errflag = 0;
        }
        if (FileName.IndexOf(".xlsx") != -1)
        {
            errflag = 0;
        }
        if (FileName.IndexOf(".docx") != -1)
        {
            errflag = 0;
        }

        if (FileName.IndexOf(".rar") != -1)
        {
            errflag = 0;
        }

        if (FileName.IndexOf(".zip") != -1)
        {
            errflag = 0;
        }

        if (FileName.IndexOf(".txt") != -1)
        {
            errflag = 0;
        }

        if (errflag == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void showMessageBox(string message,string type)
    {
 
    }
    protected void btnUpload1_Click(object sender, EventArgs e)
    {
        //try
        //{
        DocumentsManager1.Document document = new DocumentsManager1.Document();

        if (!FileUpload11.HasFile)
        {
            showMessageBox("A file must be selected.", "error");
            return;
        }


        if ((Limits == grdDocumentsNEW.Rows.Count) && (grdDocumentsNEW.Rows.Count > 0))
        {

            showMessageBox("Upload limit exceeded. Only " + Limits + " files can be uploaded.", "validation");
            return;
        }

        document.OriginalFileName = FileUpload11.FileName;

        //only .docx, .doc, .xlsx, .xls, .pdf, .gif, .jpg, .zip
        if (ValidateFile(document.OriginalFileName) == false)
        {
            showMessageBox("Invalid File Format - Acceptable formats: .doc, .xls, .docx, .xlsx, .pdf, .gif, .jpg, .zip, .rar, .txt", "error");
            return;
        }

        if (FileUpload11.PostedFile.ContentLength > 30720000)     // 5 MB = 5120000, 30 MB = 30720000
        {
            showMessageBox("File size too big - Maximum acceptable file size: 30 MB", "error");
            return;
        }



        string extension = SIMUtils.FilesAndStreams.GetFileNameExtension(FileUpload11.FileName);
        document.FileName = composeTemporaryFileName(extension);
        string fileUrl;

        if (TemporaryMode)
            fileUrl = TEMP_FOLDER;
        else
            fileUrl = manager.FolderWithFinalSlash;

        fileUrl += document.FileName;
        FileUpload11.SaveAs(Server.MapPath(fileUrl));

        document.FileType = SIMUtils.FilesAndStreams.GetFileType(document.OriginalFileName);
        document.Comments = txtComments1.Text.Trim();


        /*UAE work 9-9-2015*/
        int? intnull = null;
        //document.DocumenTypeId = Convert.ToInt32(Session["DocumentTypeID"]) == null ? intnull : Convert.ToInt32(Session["DocumentTypeID"]);
        //document.DocumentSubTypeId = Convert.ToInt32(Session["DocumentSubTypeID"]) == null ? intnull : Convert.ToInt32(Session["DocumentSubTypeID"]);

        if (TemporaryMode)
        {
            // we add the file in the local datatable
            document.FilePathWithFinalSlash = "/temp/";

            documents.Add(document);
            grdDocumentsNEW.DataSource = documents;
        }
        else
        {
       
            document.TargetId = TargetId;
            manager.AddDocument(document);
            grdDocumentsNEW.DataSource = manager.GetDocuments(TargetId);
            grdDocumentsNEW.DataBind();

        }

        showMessageBox("The attached file has been uploaded successfully.","success");

        //.executeQuery("INSERT INTO PI_tblWebReqAttachments(req_id,attach_name,attach_type) values(" + reqid + ",'Req" + (reqid) + "_" + fname + "','" + checktype(fname) + "')");
        //ProcManager.insertRequisitionAttachment("", requisitionId, fname, "");

        //'}
        hdnFileCount.Value = "1";
        txtComments1.Text = string.Empty;
        //}
        //catch (Exception ex)
        //{
        //    DBHelper.logError(this, "btnUpload1_Click:" + ex.Message);
        //    showMessageBox("Error uploading the file.", "error");
        //}
    }

    public void AutoUploadDocument(byte[] bytes, string fileName)
    {
        try
        {
            DocumentsManager1.Document document = new DocumentsManager1.Document();
            string extension = SIMUtils.FilesAndStreams.GetFileNameExtension(fileName);
            document.FileName = composeTemporaryFileName(extension);
            document.OriginalFileName = fileName;

            string fileUrl;

            if (TemporaryMode)
                fileUrl = TEMP_FOLDER;
            else
                fileUrl = manager.FolderWithFinalSlash;

            fileUrl += document.FileName;

            using (FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath(fileUrl), FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            document.FileType = SIMUtils.FilesAndStreams.GetFileType(document.OriginalFileName);

            if (TemporaryMode)
            {
                // we add the file in the local datatable
                document.FilePathWithFinalSlash = "/temp/";

                documents.Add(document);
                grdDocumentsNEW.DataSource = documents;
            }
            else
            {
                //We add the file in the right table
                document.TargetId = TargetId;
                manager.AddDocument(document);
                grdDocumentsNEW.DataSource = manager.GetDocuments(TargetId);
            }

            grdDocumentsNEW.DataBind();
            hdnFileCount.Value = "1";
        }
        catch (Exception ex)
        {
            DBHelper.logError(this, "AutoUploadDocument:" + ex.Message);
            showMessageBox("Error automatically uploading the file.", "error");
        }
    }



    protected void grdDocumentsNEW_DataBound(object sender, EventArgs e)
    {
        grdDocumentsNEW.Visible = grdDocumentsNEW.Rows.Count > 0;
    }
    protected void grdDocumentsNEW_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    public void down()
    {

    }

    protected void grdDocumentsNEW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {


            try
            {

                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                int selIndex = row.RowIndex;

                string documentToRemovePath;
                if (TemporaryMode)
                {


                    DocumentsManager1.Document document = documents[selIndex];
                    documentToRemovePath = document.FullPath;

                    //    removed from the temporary data table
                    documents.Remove(document);
                    grdDocumentsNEW.DataSource = documents;
                    grdDocumentsNEW.DataBind();
                }
                else
                {
                    documentToRemovePath = grdDocumentsNEW.DataKeys[selIndex]["FullPath"].ToString();
                    int documentId = (int)grdDocumentsNEW.DataKeys[selIndex]["DocumentId"];

                    //    removed from storage:
                    manager.RemoveDocument(documentId);
                    grdDocumentsNEW.DataSource = manager.GetDocuments(TargetId);
                    grdDocumentsNEW.DataBind();
                }

                //  and we remove the actual file:
                System.IO.File.Delete(Server.MapPath(documentToRemovePath));
                grdDocumentsNEW.SelectedIndex = -1;

                hdnFileCount.Value = this.FilesCount > 0 ? "1" : "0";
            }
            catch (Exception err)
            {
                DBHelper.logError(this, "grdDocumentsNEW_RowCommand (remove document): " + err.Message);
                showMessageBox("Error removing document.", "error");
            }
        }


        if (e.CommandName == "Download")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

            int selIndex = row.RowIndex;

            HiddenField field = (HiddenField)grdDocumentsNEW.Rows[selIndex].FindControl("ValueHiddenField");
            // Response.Redirect(field.Value);

            //// string strFileName = field.Value.Remove(0, 6);


            // Get the physical Path of the file(test.doc)
            string filepath = Server.MapPath(field.Value);

            // Create New instance of FileInfo class to get the properties of the file being downloaded
            FileInfo file = new FileInfo(filepath);

            // Checking if file exists
            if (file.Exists)
            {

                // Clear the content of the response
                Response.ClearContent();

                // LINE1: Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

                // Add the file size into the response header
                Response.AddHeader("Content-Length", file.Length.ToString());

                // Set the ContentType
                Response.ContentType = ReturnExtension(file.Extension.ToLower());


                // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
                Response.WriteFile(file.FullName);

                // End the response

                Response.End();
                //  Response.Redirect(Request.RawUrl);
            }

        }
        //if (e.CommandName == "View")
        //{
        //    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

        //    int selIndex = row.RowIndex;

        //    HiddenField field = (HiddenField)grdDocuments.Rows[selIndex].FindControl("ValueHiddenField");
        //    Response.Redirect(field.Value);


        //}
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
    protected void grdDocumentsNEW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdDocumentsNEW_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //HRMSEntities context = new HRMSEntities();
        //int EmployeeId = Convert.ToInt32(Session["EmployeeID"]);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //HiddenField hfsubdoctype = (HiddenField)e.Row.FindControl("hfsubdoctype");
            //HiddenField hfdoctypeid = (HiddenField)e.Row.FindControl("hfdoctypeid");


            ImageButton del = (ImageButton)e.Row.FindControl("DeleteButton");
            HiddenField TargetID = (HiddenField)e.Row.FindControl("hftargetID");


            //if (hfdoctypeid.Value != "")
            //{
            //    int docID = Convert.ToInt32(hfdoctypeid.Value);
            //    HRMSDAL.HRMS_Setup_DocumentType doctype = context.HRMS_Setup_DocumentType.FirstOrDefault(a => a.DocumentTypeId == docID && a.IsActive == true);

            //    Label lbldoc = (Label)e.Row.FindControl("lbldoctype");
            //    lbldoc.Text = doctype.DocumentType;

            //}
            //if (hfsubdoctype.Value != "")
            //{
            //    int SubDocID = Convert.ToInt32(hfsubdoctype.Value);
            //    HRMSDAL.HRMS_Setup_DocumentSubType Subdoctype = context.HRMS_Setup_DocumentSubType.FirstOrDefault(a => a.DocumentSubTypeId == SubDocID && a.IsActive == true);

            //    Label lblSubdoctype = (Label)e.Row.FindControl("lblSubdoctype");
            //    lblSubdoctype.Text = Subdoctype.DocumentSubType;
            //}

            //ImageButton del = (ImageButton)e.Row.FindControl("DeleteButton");
            //HiddenField TargetID = (HiddenField)e.Row.FindControl("hftargetID");

            //Label lbldoc = (Label)e.Row.FindControl("hfdoctypeid");
            // HiddenField hfsubdoctypeid = (HiddenField)e.Row.FindControl("hfsubdoctypeid");
            //lbldoc.Text = doctype.DocumentType;
            
            Base objBase = new Base();

            Int32 EmpID = Convert.ToInt32(Session["EmpId"]);

            if (objBase.IsEmployee && EmpID == Convert.ToInt32(TargetID.Value))
            {
                del.Visible = false;

                grdDocumentsNEW.Columns[7].Visible = false;
            }
            else if (objBase.IsAdmin || objBase.IsSuperAdmin)
            {
                del.Visible = true;

                grdDocumentsNEW.Columns[7].Visible = true;
            }
            else
            {
                del.Visible = false;

                grdDocumentsNEW.Columns[7].Visible = false;
            }
        }
    }

    //protected void grdUAEDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
    //{



    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        HiddenField hfsubdoctype = (HiddenField)e.Row.FindControl("hfsubdoctype");
    //        HiddenField hfdoctypeid = (HiddenField)e.Row.FindControl("hfdoctypeid");


    //        ImageButton del = (ImageButton)e.Row.FindControl("DeleteButton");
    //        HiddenField TargetID = (HiddenField)e.Row.FindControl("hftargetID");


    //        if (hfdoctypeid.Value != "")
    //        {
    //            int docID = Convert.ToInt32(hfdoctypeid.Value);
    //            HRMSDAL.HRMS_Setup_DocumentType doctype = context.HRMS_Setup_DocumentType.FirstOrDefault(a => a.DocumentTypeId == docID && a.IsActive == true);

    //            Label lbldoc = (Label)e.Row.FindControl("lbldoctype");
    //            lbldoc.Text = doctype.DocumentType;

    //        }
    //        if (hfsubdoctype.Value != "")
    //        {
    //            int SubDocID = Convert.ToInt32(hfsubdoctype.Value);
    //            HRMSDAL.HRMS_Setup_DocumentSubType Subdoctype = context.HRMS_Setup_DocumentSubType.FirstOrDefault(a => a.DocumentSubTypeId == SubDocID && a.IsActive == true);

    //            Label lblSubdoctype = (Label)e.Row.FindControl("lblSubdoctype");
    //            lblSubdoctype.Text = Subdoctype.DocumentSubType;
    //        }



    //        Int32 EmpID = Convert.ToInt32(Session["EmpId"]);

    //        if (Convert.ToString(Session["RoleCode"]) == "7" && EmpID == Convert.ToInt32(TargetID.Value))
    //        {
    //            del.Visible = true;
    //        }
    //        else if ((Convert.ToString(Session["RoleCode"]) == "5" || Convert.ToString(Session["RoleCode"]) == "8"))
    //        {
    //            del.Visible = true;
    //        }

    //        else
    //        {
    //            del.Visible = false;
    //        }
    //    }
    //}

    //protected void grdUAEDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Delete")
    //    {


    //        try
    //        {

    //            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

    //            int selIndex = row.RowIndex;

    //            string documentToRemovePath;
    //            if (TemporaryMode)
    //            {


    //                DocumentsManager1.Document document = documents[selIndex];
    //                documentToRemovePath = document.FullPath;

    //                //    removed from the temporary data table
    //                documents.Remove(document);
    //                grdUAEDocuments.DataSource = documents;
    //                grdUAEDocuments.DataBind();
    //            }
    //            else
    //            {
    //                documentToRemovePath = grdUAEDocuments.DataKeys[selIndex]["FullPath"].ToString();
    //                int documentId = (int)grdUAEDocuments.DataKeys[selIndex]["DocumentId"];

    //                //    removed from storage:
    //                manager.RemoveDocument(documentId);
    //                grdUAEDocuments.DataSource = manager.GetDocuments(TargetId);
    //                grdUAEDocuments.DataBind();
    //            }

    //            //  and we remove the actual file:
    //            System.IO.File.Delete(Server.MapPath(documentToRemovePath));
    //            grdUAEDocuments.SelectedIndex = -1;

    //            hdnFileCount.Value = this.FilesCount > 0 ? "1" : "0";
    //        }
    //        catch (Exception err)
    //        {
    //            DBHelper.logError(this, "grdDocuments_RowCommand (remove document): " + err.Message);
    //            showMessageBox("Error removing document.", "error");
    //        }
    //    }


    //    if (e.CommandName == "Download")
    //    {
    //        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

    //        int selIndex = row.RowIndex;

    //        HiddenField field = (HiddenField)grdUAEDocuments.Rows[selIndex].FindControl("ValueHiddenField");
    //        // Response.Redirect(field.Value);

    //        //// string strFileName = field.Value.Remove(0, 6);


    //        // Get the physical Path of the file(test.doc)
    //        string filepath = Server.MapPath(field.Value);

    //        // Create New instance of FileInfo class to get the properties of the file being downloaded
    //        FileInfo file = new FileInfo(filepath);

    //        // Checking if file exists
    //        if (file.Exists)
    //        {
    //            try
    //            {


    //                // Clear the content of the response
    //                Response.ClearContent();

    //                // LINE1: Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
    //                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

    //                // Add the file size into the response header
    //                Response.AddHeader("Content-Length", file.Length.ToString());

    //                // Set the ContentType
    //                Response.ContentType = ReturnExtension(file.Extension.ToLower());


    //                // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
    //                Response.WriteFile(file.FullName);

    //                // End the response

    //                Response.End();
    //            }
    //            catch (Exception ex)
    //            {


    //            }
    //            //  Response.Redirect(Request.RawUrl);
    //        }

    //    }
    //    //if (e.CommandName == "View")
    //    //{
    //    //    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

    //    //    int selIndex = row.RowIndex;

    //    //    HiddenField field = (HiddenField)grdDocuments.Rows[selIndex].FindControl("ValueHiddenField");
    //    //    Response.Redirect(field.Value);


    //    //}
    //}
}