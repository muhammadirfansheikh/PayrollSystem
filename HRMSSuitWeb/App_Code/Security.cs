using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DAL;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{
    public Security()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public const int PasswordExpiration_Days = 90;
    public const int TotalWrongPassword_Attemp = 6;
    public static string DefaultPassword = "Hcm@_098";
    public const string SecurityKey = "SecurityKey_HCM_HRMS";
    public const string EmailFooter = "HCM Team";
    public const int ResetPasswordEmailLink_Expiry_Mints = 180;
    public const int FileSizeInMB = 4;
    public static void DisableAccount(int UserId)
    {
        Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
        Setup_User user = context.Setup_User.FirstOrDefault(x => x.User_Code == UserId);
        if (user != null)
        {
            //user.IsEnabled = false;
            context.SaveChanges();
        }
    }
    public static string Get_Random()
    {
        var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var resultToken = new string(Enumerable.Repeat(allChar, 8).Select(token => token[random.Next(token.Length)]).ToArray());
        Guid objGuid = Guid.NewGuid();
        return (objGuid + resultToken.ToString());
    }
    public static DataTable ResetPasswordEmailLink(string LoginId)
    {
        DataTable dtReturn = new DataTable();
        dtReturn.Columns.Add("Status");
        dtReturn.Columns.Add("Message");
        dtReturn.Rows.Add(0, "Something went wrong. Please contact to HR.");
        try
        {
            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            Setup_User user = context.Setup_User.FirstOrDefault(x => x.Is_Active == true && x.Login_ID == LoginId);
            if (user != null)
            {
                string Token = Get_Random();
                if (Token != "")
                { 
                    if (user.Email_Address != "")
                    {
                        string Encrypted = Encrypt(SecurityKey, "Token=" + Token);
                        string Reset_Password_Link = System.Configuration.ConfigurationManager.AppSettings["Root"] + "/ResetPassword.aspx?" + Encrypted;
                        if (Reset_Password_Link != "")
                        {
                            string EmailMsg = "";
                            EmailMsg += "Dear " + user.Full_Name + ",<br/><br/>";
                            EmailMsg += "Please click on the link to <a href = " + Reset_Password_Link + "> reset Password </a>.<br/><br/>";
                            EmailMsg += "Note that the password will not change unless you click on the given link to create a new one. This link will expire after " + ResetPasswordEmailLink_Expiry_Mints / 60 + " hours.<br/><br/>";
                            EmailMsg += "If your link has expired, please create a new request.<br/><br/>";
                            EmailMsg += "Regards,<br/><br/> " + EmailFooter + "";
                            Email.SendMail(user.Email_Address, "Reset Password Request", EmailMsg, ""); 
                            user.ForgetPassword_Token = Token;
                            user.ForgetPassword_Token_CreatedDatetime = DateTime.Now;
                            context.SaveChanges();
                            dtReturn.Rows[0]["Status"] = "1";
                            dtReturn.Rows[0]["Message"] = "Reset password link has been sent successfully at " + user.Email_Address + "."; 
                        }
                        else
                        {
                            dtReturn.Rows[0]["Status"] = "0";
                            dtReturn.Rows[0]["Message"] = "Unable to generate a link. Please try again later";
                        }
                    }
                    else
                    {
                        dtReturn.Rows[0]["Status"] = "0";
                        dtReturn.Rows[0]["Message"] = "Email address not found. Please contact to HR.";
                    }
                }
                else
                {
                    dtReturn.Rows[0]["Status"] = "0";
                    dtReturn.Rows[0]["Message"] = "Something went wrong. Please contact to HR.";
                }
            }
            else
            {
                dtReturn.Rows[0]["Status"] = "0";
                dtReturn.Rows[0]["Message"] = "Invalid Login-Id";
            }
        }
        catch
        {
            dtReturn.Rows[0]["Status"] = "0";
            dtReturn.Rows[0]["Message"] = "Something went wrong. Please contact to HR.";
        }
        return dtReturn;
    }
    public static bool CheckPasswordExpiration(int UserId)
    {
        bool IsPasswordExpired = false;
        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {
            var obj = context.UserPasswordHistories.Where(u => u.UserId == UserId && u.IsActive == true).OrderByDescending(a => a.ID).ToList();
            if (obj != null && obj.Count > 0)
            {
                double PasswordLimit = Convert.ToDouble(Security.PasswordExpiration_Days);
                DateTime ExpiryDate = (DateTime)obj[0].CreatedDate;
                ExpiryDate = ExpiryDate.AddDays(PasswordLimit);
                if (ExpiryDate.Date <= DateTime.Today)
                {
                    IsPasswordExpired = true;
                }
            }
            else
            {
                IsPasswordExpired = true;
            }
        }
        return IsPasswordExpired;
    }

    public static void Reset_Password_Request_Token(int UserId)
    {
        Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
        Setup_User user = context.Setup_User.FirstOrDefault(x => x.User_Code == UserId);
        if (user != null)
        {
            user.ForgetPassword_Token = null;
            user.ForgetPassword_Token_CreatedDatetime = null;
            context.SaveChanges();
        }
    }
    public static void SendEmailOnPasswordChange(int UserId, bool IsPasswordChangeRequest)
    {
        try
        {
            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            Setup_User user = context.Setup_User.FirstOrDefault(x => x.User_Code == UserId);
            if (user != null)
            {
                string EmailMsg = "";
                EmailMsg += "Dear " + user.Full_Name + ",<br/><br/>";
                if (IsPasswordChangeRequest == true)
                {
                    EmailMsg += "Your password has been changes successfully.";
                }
                else
                {
                    EmailMsg += "Your profile has been updated.";
                }
                EmailMsg += "<br/><br/>Regards,<br/> " + EmailFooter + "";
                Email.SendMail(user.Setup_Employee.OfficeEmailAddress, "Change Password", EmailMsg, "");
            }
        }
        catch
        {
        }
    }
    public static string DecryptById(string EncryptedData, string Id)
    {
        string DecryptedQueryString = Decrypt(SecurityKey, EncryptedData);

        DecryptedQueryString = DecryptedQueryString.After(Id + "=");
        int EndIndex = DecryptedQueryString.IndexOf("&");

        if (EndIndex == -1)
        {
            return DecryptedQueryString;
        }
        else
        {
            return DecryptedQueryString.Substring(0, EndIndex);
        }
    }
    public static string Encrypt(string key, string data)
    {
        string encData = null;
        byte[][] keys = GetHashKeys(key);

        try
        {
            encData = EncryptStringToBytes_Aes(data, keys[0], keys[1]);
        }
        catch (CryptographicException) { }
        catch (ArgumentNullException) { }

        return encData;
    }
    public static string Decrypt(string key, string data)
    {
        string decData = null;
        byte[][] keys = GetHashKeys(key);

        try
        {
            decData = DecryptStringFromBytes_Aes(data, keys[0], keys[1]);
        }
        catch (CryptographicException) { }
        catch (ArgumentNullException) { }

        return decData;
    }
    private static byte[][] GetHashKeys(string key)
    {
        byte[][] result = new byte[2][];
        Encoding enc = Encoding.UTF8;

        SHA256 sha2 = new SHA256CryptoServiceProvider();
        byte[] rawKey = enc.GetBytes(key);
        byte[] rawIV = enc.GetBytes(key);

        byte[] hashKey = sha2.ComputeHash(rawKey);
        byte[] hashIV = sha2.ComputeHash(rawIV);

        Array.Resize(ref hashIV, 16);

        result[0] = hashKey;
        result[1] = hashIV;

        return result;
    }
    private static string EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;
        using (AesManaged aesAlg = new AesManaged())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt =
                        new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encrypted);
    }
    private static string DecryptStringFromBytes_Aes(string cipherTextString, byte[] Key, byte[] IV)
    {
        byte[] cipherText = Convert.FromBase64String(cipherTextString);

        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt =
                        new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }
    public static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    public static bool Validate_EmailId(string _EmailId)
    {
        bool Status = false;
        var r = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
        if (r.IsMatch(_EmailId) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_Name(string Name)
    {
        bool Status = false;
        var r = new Regex(@"^[a-zA-Z ]+$");
        if (r.IsMatch(Name) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_contactno(string contactno)
    {
        bool Status = false;
        var r = new Regex(@"^[\d]{4}-[\d]{7}$");
        if (r.IsMatch(contactno) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_cnic(string cnic)
    {
        bool Status = false;
        var r = new Regex(@"^[\d]{5}-[\d]{7}-[\d]{1}$");
        if (r.IsMatch(cnic) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_alphanumeric(string alphanumeric)
    {
        bool Status = false;
        var r = new Regex(@"^[a-zA-Z0-9]+$");
        if (r.IsMatch(alphanumeric) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_number(string number)
    {
        bool Status = false;
        var r = new Regex(@"^\[0-9]+");
        if (r.IsMatch(number) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_decimalnumber(string decimalnumber)
    {
        bool Status = false;
        var r = new Regex(@"^(\d*\.)?\d+$");
        if (r.IsMatch(decimalnumber) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_Password(string Password)
    {
        bool Status = false;
        var r = new Regex(@"^(?!.*[ ]{2})(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,128}$");
        if (r.IsMatch(Password) == true)
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_File_Extension(string File_Extension)
    {
        bool Status = false;
        File_Extension = File_Extension.ToLower();
        if (
            File_Extension == ".png" ||
            File_Extension == ".jpg" ||
            File_Extension == ".jpeg" ||
            File_Extension == ".pdf" ||
            File_Extension == ".docx" ||
            File_Extension == ".xls" ||
            File_Extension == ".xlsx" ||
            File_Extension == ".vsd" ||
            File_Extension == ".txt"
            )
        {
            Status = true;
        }
        return Status;
    }
    public static bool Validate_Date(string date)
    {
        bool Status = false;
        try
        {
            DateTime dt = Convert.ToDateTime(date);
            Status = true;
        }
        catch
        {

        }
        return Status;
    }
    public static string Generate_Password(string value)
    {
        string pwd = "";
        string Encrypted = Encrypt(SecurityKey, value);
        pwd = ComputeSha256Hash(Encrypted);
        return pwd;
    }
}