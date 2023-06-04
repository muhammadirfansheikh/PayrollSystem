using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DAL;

public class DocumentsManager1 : Base
{
    [Serializable]
    public class Document
    {
        public Document() { }// we need this constructor so it is serializable

        public int? DocumenTypeId { get; set; }
        public int? DocumentSubTypeId { get; set; }

        public int DocumentId { get; set; }
        public object TargetId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePathWithFinalSlash { get; set; }
        public string OriginalFileName { get; set; }
        public string Comments { get; set; }
        public DateTime AddedOn { get; set; }

        public string FullPath
        {
            get { return FilePathWithFinalSlash + FileName; }
        }
    }


    public enum DOCUMENTS_TYPE
    {
        EmployeeAttachment,
        EmployeeAcademicAttachment

    }

    private string TableName { get; set; }
    private string ColumnNameDocumentId { get; set; }
    private string CreatedBy { get; set; }
    private string ColumnNameDocumenTypeId { get; set; }
    private string ColumnNameDocumentSubTypeId { get; set; }
    private string ColumnNameTargetId { get; set; }
    private string ColumnNameFileName { get; set; }
    private string ColumnNameFileType { get; set; }
    private string ColumnNameOriginalFileName { get; set; }
    private string ColumnNameComments { get; set; }
    public string FolderWithFinalSlash { get; set; }
    public string Prefix { get; set; }

    private string SpecificColumnNamesSeperatedByCommas { get; set; }

    public DocumentsManager1(DOCUMENTS_TYPE DocumentsType)
    {
        SpecificColumnNamesSeperatedByCommas = "";
        switch (DocumentsType)
        {
            case DOCUMENTS_TYPE.EmployeeAttachment:
                TableName = "HRMS_EmployeeAttachments";
                CreatedBy = "CreatedBy";
                ColumnNameDocumentId = "FileId";
                ColumnNameTargetId = "TargetId";
                //ColumnNameDocumenTypeId = "DocumentTypeId";
                //ColumnNameDocumentSubTypeId = "DocumentSubTypeId";
                ColumnNameFileName = "Filename";
                ColumnNameComments = "Filecomments";
                ColumnNameFileType = "Filetype";
                ColumnNameOriginalFileName = "FileOriginalName";

                FolderWithFinalSlash = "/Attachments/";
                Prefix = "HRMS";
                break;

            case DOCUMENTS_TYPE.EmployeeAcademicAttachment:
                TableName = "HRMS_EmployeeAttachments";
                CreatedBy = "CreatedBy";
                ColumnNameDocumentId = "FileId";
                ColumnNameTargetId = "TargetId";
                //ColumnNameDocumenTypeId = "DocumentTypeId";
                //ColumnNameDocumentSubTypeId = "DocumentSubTypeId";
                ColumnNameFileName = "Filename";
                ColumnNameComments = "Filecomments";
                ColumnNameFileType = "Filetype";
                ColumnNameOriginalFileName = "FileOriginalName";

                FolderWithFinalSlash = "/Attachments/";
                Prefix = "HRMS";
                break;

        }

    }

    public void AddDocument(Document document)
    {
        try
        {
            //int? docTypeId = null;

            //if (document.DocumenTypeId != null)
            //{
            //    // docTypeId = null;
            //}

            ////else if (document.DocumenTypeId == 0)
            ////{
            ////    docTypeId = null;
            ////}

            //int? subDocTypeID = null;

            //if (document.DocumentSubTypeId != null)
            //{
            //    // subDocTypeID = null;
            //}

            //else if (document.DocumentSubTypeId == 0)
            //{
            //    subDocTypeID = null;
            //}

            //else
            {

                string query =
                    //int updatedRecords=DBHelper.insertData(
                    " INSERT INTO " + TableName
                    + " ( "
                        + ColumnNameTargetId
                        + "," + ColumnNameFileName
                    //     + "," + ColumnNameDocumenTypeId
                    //+ "," + ColumnNameDocumentSubTypeId
                        + (string.IsNullOrEmpty(ColumnNameOriginalFileName) ? "" : " , " + ColumnNameOriginalFileName)
                        + (string.IsNullOrEmpty(ColumnNameComments) ? "" : " , " + ColumnNameComments)
                        + (string.IsNullOrEmpty(ColumnNameFileType) ? "" : " , " + ColumnNameFileType)
                    + " ,CreatedBy) "
                    + " VALUES ( "
                        + document.TargetId.ToString()
                        + " , '" + document.FileName + "' "
                    ////+ " , '" + (document.DocumenTypeId ? intnull + "'" : "'" + document.DocumenTypeId + "' ")
                    //    + " , " + docTypeId
                    ////+ " , '" + (document.DocumentSubTypeId ? intnull + "'" : "'" + document.DocumentSubTypeId + "'")
                    //+ " , " + subDocTypeID
                       + (string.IsNullOrEmpty(ColumnNameOriginalFileName) ? "" : " , '" + document.OriginalFileName + " '")
                        + (string.IsNullOrEmpty(ColumnNameComments) ? "" : " , '" + document.Comments + " '")
                        + (string.IsNullOrEmpty(ColumnNameFileType) ? "" : " , '" + document.FileType + " '")
                        + " , " + UserKey
                    + " ) ";

                Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
                // context.ExecuteStoreCommand(query);
                context.Database.ExecuteSqlCommand(query);
            }

        }
        catch (Exception err)
        {
            //DBHelper.logError("DocumentsManager.AddDocument", err.Message);
            throw err;
        }
    }

    public void RemoveDocument(int DocumentId)
    {
        try
        {
            //int updatedRecords = DBHelper.insertData(
            //string query =
            //    " DELETE FROM " + TableName
            //    + " WHERE " + ColumnNameDocumentId + " = " + DocumentId;
            DateTime dt = DateTime.Now;
            string query =
             " Update  " + TableName +
             " SET IsActive=0 , " +
                 " ModifiedBy = " + UserKey +
                 " , ModifiedDate = '" + dt +
              "' WHERE " + ColumnNameDocumentId + " = " + DocumentId;


            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            //  context.ExecuteStoreCommand(query);
            context.Database.ExecuteSqlCommand(query);
            //);
        }
        catch (Exception err)
        {
            // DBHelper.logError("DocumentsManager.RemoveDocument(int)", err.Message);
            throw err;
        }
    }

    public DataTable GetDocuments(object TargetId)
    {
        try
        {
            string query =
                //return DBHelper.getDataTable(
               " SELECT "
                   + ColumnNameDocumentId + " as DocumentId "
                    + " , " + CreatedBy + " as CreatedBy "
                   + " , " + ColumnNameTargetId + " as TargetId "
                   + " , " + ColumnNameFileName + " as FileName "

                   + " , " + (string.IsNullOrEmpty(ColumnNameDocumenTypeId) ? "NULL" : ColumnNameDocumenTypeId)
                     + " , " + (string.IsNullOrEmpty(ColumnNameDocumentSubTypeId) ? "NULL" : ColumnNameDocumentSubTypeId)

                   + " , " + (string.IsNullOrEmpty(ColumnNameOriginalFileName) ? " 'Document' " : ColumnNameOriginalFileName)
                           + " as OriginalFileName "
                   + " , " + (string.IsNullOrEmpty(ColumnNameFileType) ? " NULL " : ColumnNameFileType)
                           + " as FileType "

                   // there are some migrated tables that save all the path in the ColumnNameFileName
                // we have to cover those cases:

                   + " , CASE "
                           + " WHEN " + ColumnNameFileName + " LIKE '~%' THEN " + ColumnNameFileName
                           + " ELSE '" + FolderWithFinalSlash + "' + " + ColumnNameFileName
                       + " END as FullPath "
                   + " , " + (string.IsNullOrEmpty(ColumnNameComments) ? " NULL " : ColumnNameComments)
                           + " as Comments "
               + " FROM " + TableName
               + " WHERE ISACTIVE=1 and " + ColumnNameTargetId + "=" + TargetId
               + " and DocumentTypeId is null and DocumentSubTypeId is null";
            //  );

            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            DataTable DT = context.DynamicQuerySelect(query).ToList().ToDataTable(); // context.DynamicQuerySelect(query).ToList().ToDataTable();

            //List<DynamicQuerySelect_Result> obj = context.DynamicQuerySelect(query).;



            return DT;
        }
        catch (Exception err)
        {
            //DBHelper.logError("DocumentsManager.GetDocuments", err.Message);
            return null;
        }
    }

    public string ComposeFileName(object TargetId, string OriginalFileName)
    {
        Guid id = Guid.NewGuid();
        string rnd = id.ToString().ToUpper().Replace('_', '2').Replace('-', '1').Substring(0, 20);

        return Prefix
            + SIMUtils.FilesAndStreams.PrepareToBeFileName(TargetId.ToString())
            + "_"
            + rnd //+ new Random().Next()
            + SIMUtils.FilesAndStreams.GetFileNameExtension(OriginalFileName);
    }
}