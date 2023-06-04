using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_VehicleInformation
/// </summary>
public class Cls_VehicleInformation
{
    Sybrid_DatabaseEntities ObjContext = new Sybrid_DatabaseEntities();
    ResponseHelper ObjResponseHelper = new ResponseHelper();
    public ResponseHelper ManageVehicleInformationCategoryMapping(int CategoryID, int VehicleId, bool IsUpgradeVehicle, int VehicleInformationID, int LoopingIndex, int UserID)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ObjContext.Database.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_HCM_Vehicle_Information_Manage", con))
                {
                    con.Open();
                    cmd.CommandTimeout = int.MaxValue;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    cmd.Parameters.Add("@CatgoryID", SqlDbType.Int).Value = CategoryID;
                    cmd.Parameters.Add("@VehicleId", SqlDbType.Int).Value = VehicleId;
                    cmd.Parameters.Add("@IsUpgradeVehicle", SqlDbType.Bit).Value = IsUpgradeVehicle;
                    cmd.Parameters.Add("@VehicleInformationId", SqlDbType.Int).Value = VehicleInformationID;
                    cmd.Parameters.Add("@loopingIndex", SqlDbType.Int).Value = LoopingIndex;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;


                    DataTable dataTable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                    con.Close();



                    ObjResponseHelper.ResponseMessage = dataTable.Rows[0]["ResponseMessage"].ToString();
                    ObjResponseHelper.ResponseMessageType = (Constant.ResponseType)1;
                    ObjResponseHelper.ResponseDataTable = dataTable;

                }

            }
        }
        catch (Exception ex)
        {
            ObjResponseHelper.ResponseMessage = ex.Message;
            ObjResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
            ObjResponseHelper.ResponseData = null;

        }
        return ObjResponseHelper;


    }


    public ResponseHelper GetCategoryWiseVehicleList(int GroupId, int CompanyId, int CategoryId, string CategoryName)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ObjContext.Database.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("HCM_GetCategoryWiseVehicle", con))
                {
                    con.Open();
                    cmd.CommandTimeout = int.MaxValue;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                    cmd.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
                    cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = CategoryName;



                    DataTable dataTable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                    con.Close();



                    ObjResponseHelper.ResponseMessage = "Data Found";
                    ObjResponseHelper.ResponseMessageType = (Constant.ResponseType)1;
                    ObjResponseHelper.ResponseDataTable = dataTable;

                }

            }
        }
        catch (Exception ex)
        {
            ObjResponseHelper.ResponseMessage = ex.Message;
            ObjResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
            ObjResponseHelper.ResponseData = null;

        }
        return ObjResponseHelper;


    }


    public ResponseHelper GetVehicleDesignationMapping(int CategoryID, int IsUpgrade)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ObjContext.Database.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("HCM_GetHCM_VehicleDesignationMapping", con))
                {
                    con.Open();
                    cmd.CommandTimeout = int.MaxValue;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryID;
                    cmd.Parameters.Add("@Upgrade", SqlDbType.Int).Value = IsUpgrade;
                   

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                    con.Close();



                    ObjResponseHelper.ResponseMessage = "Data Found";
                    ObjResponseHelper.ResponseMessageType = (Constant.ResponseType)1;
                    ObjResponseHelper.ResponseDataTable = dataTable;

                }

            }
        }
        catch (Exception ex)
        {
            ObjResponseHelper.ResponseMessage = ex.Message;
            ObjResponseHelper.ResponseMessageType = Constant.ResponseType.ERROR;
            ObjResponseHelper.ResponseData = null;

        }
        return ObjResponseHelper;


    }
}