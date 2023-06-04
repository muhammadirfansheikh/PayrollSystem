using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResponseHelper
/// </summary>
public class ResponseHelper
{
    public ResponseHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string ResponseMessage { get; set; }
    public Constant.ResponseType ResponseMessageType { get; set; }
    public object ResponseData { get; set; }
    public DataTable ResponseDataTable { get; set; }
    public DataSet ResponseDataSet { get; set; }
}