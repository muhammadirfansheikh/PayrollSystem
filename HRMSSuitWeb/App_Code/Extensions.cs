using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Threading;

#region Extension Methods

public static class ExtensionMethods
{
    public static int toInt(this Boolean value)
    {
        return Convert.ToInt32(value);
    }

   

    public static int toInt(this String str)
    {
        int value;
        if (int.TryParse(str, out value))
        {
            return value;
        }
        return 0;
    }

    public static decimal toDecimal(this String str)
    {
        decimal value;
        if (decimal.TryParse(str, out value))
        {
            return value;
        }
        return 0;
    }

    public static bool toBool(this Char charValue)
    {
        return charValue == '1';
    }
    
    public static string ReplaceAngleBrackets(this String str)
    {
        str = str.Replace("<", "(");
        str = str.Replace(">", ")");
        return str;

    }
    public static string ToTitle(this String str)
    {
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;
        string s= textInfo.ToTitleCase(str);
        return s;

    }




    #region DateTime Extension
    public static string toDateTime(this DateTime dt)
    {
        if (dt != null)
        {
            return dt.ToString("MMM dd, yyyy H:mm:ss");
        }
        return "";
    }
    public static string toDate(this DateTime dt)
    {
        if (dt != null)
        {
            if (dt.Date != DateTime.MinValue)
                return dt.ToString("MMM dd, yy");
        }
        return "";
    }
    public static string toDate(this DateTime? dt)
    {
        if (dt != null)
        {
            //if (dt.Date != DateTime.MinValue)
            return string.Format("{0:MMM dd, yyyy}", dt);
        }
        return "";
    }
    public static string toDateDayMonth(this DateTime? dt)
    {
        if (dt != null)
        {
            //if (dt.Date != DateTime.MinValue)
            return string.Format("{0:MMM dd}", dt);
        }
        return "";
    }
    #endregion

    #region String Extensions

    public static string toStringFormat(this DateTime? dt)
    {
        if (dt == null)
        {
            return null;
        }
        return dt.Value.toStringFormat();
    }

    public static string toStringFormat(this DateTime dt)
    {
        return string.Format("{0:MM/dd/yyyy}", dt);
    }

    public static string After(this string str, string afterValue)
    {
        int indexValue = str.LastIndexOf(afterValue);
        if (indexValue < 0)
            return string.Empty;
        indexValue += afterValue.Length;

        if (indexValue >= str.Length)
            return string.Empty;

        return str.Substring(indexValue);
    }

    public static string toIntFormat(this decimal value)
    {
        return Convert.ToInt32(value).ToString();
    }

    public static string ReplaceAll(this string str, Dictionary<string, string> content)
    {
        var stringBuilder = new StringBuilder(str);
        //  return content.Keys.Aggregate(stringBuilder, (current, key) => current.Replace(key, content[key])).ToString();

        foreach (string key in content.Keys)
            stringBuilder.Replace(key, content[key]);

        return stringBuilder.ToString();
    }


    public static string ToStringFormat(this TimeSpan timeSpan, string format)
    {

        if (string.IsNullOrEmpty(format))
            format = "HH:mm:ss";

        return string.Format("{0:" + format + "}", timeSpan);

    }

    public static DateTime FirstDayOfMonth(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    public static DateTime LastDayOfMonth(this DateTime dateTime)
    {
        DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
        return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
    }
    public static int LastDay(this DateTime dateTime)
    {
        int lastDay = 31;
        switch (dateTime.Month)
        {
            case 2:
                {
                    lastDay = dateTime.Year % 4 == 0 ? 29 : 28;
                    break;
                }
            case 4:
            case 6:
            case 9:
            case 11:
                {
                    lastDay = 30;
                    break;
                }
        }
        return lastDay;
    }

    #endregion

    #region Json Extensions

    public static string toJson(this Object obj)
    {
        var serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
        //using (var ms = new MemoryStream())
        //{
        //    var serializer = new DataContractJsonSerializer(obj.GetType());
        //    serializer.WriteObject(ms, obj);
        //    return Encoding.UTF8.GetString(ms.ToArray());
        //}
    }

    public static string toJson(this Object obj, int recursionDepth)
    {
        var serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
        return serializer.Serialize(obj);
    }

    private static JProperty[] ConvertRowToJPropertyArray(DataRow dr)
    {
        JProperty[] o = new JProperty[dr.Table.Columns.Count];
        for (int i = 0; i < dr.Table.Columns.Count; i++)
        {
            o[i] = new JProperty(dr.Table.Columns[i].ColumnName, dr[dr.Table.Columns[i]]);
        }
        return o;
    }

    public static string toJson(this DataSet ds)
    {
        JObject trans =
          new JObject(
            new JProperty("data",
              new JArray(
                from r in ds.Tables[0].AsEnumerable()
                select new JObject(
                    ConvertRowToJPropertyArray(r)
                  ))));
        return trans.ToString();
    }



    public static string toJson(this DataTable dataTable)
    {

        string[] StrDc = new string[dataTable.Columns.Count];

        string HeadStr = string.Empty;
        for (int i = 0; i < dataTable.Columns.Count; i++)
        {

            StrDc[i] = dataTable.Columns[i].Caption;
            HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";

        }

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
        StringBuilder Sb = new StringBuilder();

        Sb.Append("{\"" + dataTable.TableName + "\" : [");
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {

            string TempStr = HeadStr;

            Sb.Append("{");
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {

                TempStr = TempStr.Replace(dataTable.Columns[j] + j.ToString() + "¾", dataTable.Rows[i][j].ToString());

            }
            Sb.Append(TempStr + "},");

        }
        Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

        Sb.Append("]}");
        return Sb.ToString();

    }
    #endregion
}

#endregion

#region DataReader

public static class DataReaderExtensions
{
    public static bool IsDBNull(this IDataReader dataReader, string columnName)
    {
        return dataReader[columnName] == DBNull.Value;
    }

    public static bool GetBoolean(this IDataReader dataReader, string columnName)
    {
        if (dataReader.IsDBNull(columnName))
        {
            return false;
        }
        Object value = dataReader[columnName];

        if (value.GetType() == typeof(Boolean))
        {
            return (bool)value;
        }

        bool boolValue;

        if (bool.TryParse(value.ToString(), out boolValue))
            return boolValue;

        return Convert.ToBoolean(value);
        //   throw new ArrayTypeMismatchException(string.Format("Can not convert to value={2} to Boolean type from {0} for column {1}", value.GetType().ToString(),columnName,value.ToString()));
    }

    public static int? GetInt(this IDataReader dataReader, string columnName)
    {
        if (dataReader.IsDBNull(columnName))
        {
            return null;
        }
        Object value = dataReader[columnName];

        if (value.GetType() == typeof(Int32))
        {
            return (int?)value;
        }

        int intValue;

        if (int.TryParse(value.ToString(), out intValue))
            return intValue;

        return Convert.ToInt32(value);
        //   throw new ArrayTypeMismatchException(string.Format("Can not convert to value={2} to Boolean type from {0} for column {1}", value.GetType().ToString(),columnName,value.ToString()));
    }


    public static TimeSpan? GetTimeSpan(this IDataReader dataReader, string columnName)
    {
        if (dataReader.IsDBNull(columnName))
        {
            return null;
        }
        Object value = dataReader[columnName];

        if (value.GetType() == typeof(TimeSpan))
        {
            return (TimeSpan?)value;
        }


        TimeSpan timeSpanValue;

        if (TimeSpan.TryParse(value.ToString(), out timeSpanValue))
            return (TimeSpan?)timeSpanValue;

        return null;

    }



    public static decimal? GetDecimal(this IDataReader dataReader, string columnName)
    {
        if (dataReader.IsDBNull(columnName))
        {
            return null;
        }
        Object value = dataReader[columnName];

        if (value.GetType() == typeof(Decimal))
        {
            return (decimal?)value;
        }

        decimal decimalValue;

        if (decimal.TryParse(value.ToString(), out decimalValue))
            return decimalValue;

        return Convert.ToDecimal(value);
        //   throw new ArrayTypeMismatchException(string.Format("Can not convert to value={2} to Boolean type from {0} for column {1}", value.GetType().ToString(),columnName,value.ToString()));
    }

    public static DateTime? GetDateTime(this IDataReader dataReader, string columnName)
    {
        if (dataReader.IsDBNull(columnName))
        {
            return null;
        }
        Object value = dataReader[columnName];

        if (value.GetType() == typeof(DateTime))
        {
            return (DateTime?)value;
        }

        DateTime dateTimeValue;

        if (DateTime.TryParse(value.ToString(), out dateTimeValue))
            return dateTimeValue;

        return Convert.ToDateTime(value);
        //   throw new ArrayTypeMismatchException(string.Format("Can not convert to value={2} to Boolean type from {0} for column {1}", value.GetType().ToString(),columnName,value.ToString()));
    }

    public static string GetString(this IDataReader dataReader, string columnName)
    {
        if (dataReader.IsDBNull(columnName))
        {
            return null;
        }
        return (string)dataReader[columnName];
    }

    public static ArrayList GetKeyValueRows(this IDataReader reader)
    {
        var dataRows = new ArrayList();

        while (reader.Read())
        {
            KeyValuePair<string, string> singleRow = reader.GetKeyValueRow(); //GetKeyValueRow(reader);
            dataRows.Add(singleRow);
        }

        return dataRows;
    }

    public static KeyValuePair<string, string> GetKeyValueRow(this IDataReader reader)
    {
        String key = reader[0].ToString();
        String value = reader[1].ToString();
        return new KeyValuePair<string, string>(key, value) { };

    }

    public static Dictionary<string, string> GetDictionaryRows(this IDataReader reader)
    {
        var values = new Dictionary<string, string>();

        while (reader.Read())
        {
            values.Add(reader[0].ToString(), reader[1].ToString());
        }

        return values;
    }

    public static List<KeyValuePair<string, string>> GetListRows(this IDataReader reader)
    {
        var values = new List<KeyValuePair<string, string>>();

        while (reader.Read())
        {
            KeyValuePair<string, string> singleRow = reader.GetKeyValueRow();
            values.Add(singleRow);
        }

        return values;
    }
}

#endregion

#region UI

public static class DropDownExtension
{
    public static void AddItemsOfRange(this ListControl lstList, int min, int max, string prefix, string suffix)
    {
        if (max < min)
        {
            for (int i = min; i >= max; i--)
            {
                string value = i.ToString();

                lstList.Items.Add(new ListItem(prefix + value + suffix, value));
            }
        }
        else
        {
            for (int i = min; i <= max; i++)
            {
                string value = i.ToString();

                lstList.Items.Add(new ListItem(prefix + value + suffix, value));
            }
        }
    }

    public static void Populate(this ListControl lstList, String[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            lstList.Items.Add(new ListItem(values[i]));
        }
    }

    public static void Populate(this ListControl lstList, String[] values, string prefix, string suffix)
    {
        for (int i = 0; i < values.Length; i++)
        {
            string value = values[i];

            lstList.Items.Add(new ListItem(prefix + value + suffix, value));
        }
    }

    public static void Populate(this ListControl lstList, ArrayList values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            lstList.Items.Add(new ListItem(values[i].ToString()));
        }
    }

    public static Hashtable GetEnumForBind(Type enumeration)
    {
        string[] names = Enum.GetNames(enumeration);
        Array values = Enum.GetValues(enumeration);
        var ht = new Hashtable();
        for (int i = 0; i < names.Length; i++)
        {
            ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
        }
        return ht;
    }

    public static void DataBind(this ListControl lstList, Type type)
    {
        Hashtable ht = GetEnumForBind(type);
        lstList.DataSource = ht;
        lstList.DataTextField = "value";
        lstList.DataValueField = "key";

        lstList.DataBind();
    }



    public static void DataBind(this Dictionary<string, ArrayList> result, string[] keys, DropDownList[] dropDownLists)
    {
        if (result != null && result.Count == dropDownLists.Length)
        {
            for (int i = 0; i < dropDownLists.Length; i++)
            {
                if (dropDownLists[i] != null && result.ContainsKey(keys[i]))
                    dropDownLists[i].DataBind(result[keys[i]], null);

            }
        }
    }

    public static void DataBind(this ListControl lstList, ArrayList arrayList)
    {
        lstList.DataBind(arrayList, null);
    }

    public static void DataBind(this ListControl lstList, Dictionary<string, string> data)
    {
        lstList.DataBind(data, null);
    }

    public static void DataBind(this ListControl lstList, Dictionary<string, string> data, string defaultValue)
    {
        lstList.DataSource = data;
        lstList.DataTextField = "Value";
        lstList.DataValueField = "Key";
        lstList.DataBind();
        lstList.addDefaultValue(defaultValue);
    }


    public static void DataBind(this ListControl lstList, ArrayList arrayList, string defaultValue)
    {
        lstList.DataSource = arrayList;
        lstList.DataTextField = "Value";
        lstList.DataValueField = "Key";
        lstList.DataBind();

        lstList.addDefaultValue(defaultValue);
    }

    public static void addDefaultValue(this ListControl lstList, string defaultValue)
    {
        if (defaultValue == null)
            lstList.Items.Insert(0, new ListItem("--Select--", ""));
        else
        {
            ListItem listItem = lstList.Items.FindByValue(defaultValue);
            if (listItem == null)
            {
                lstList.Items.Insert(0, defaultValue);
            }
            else
            {
                listItem.Selected = true;
            }
        }
    }


    public static void SelectText(this ListControl lstList, string text)
    {
        if (lstList.Items.Count == 0)
            return;

        ListItem item = lstList.Items.FindByText(text);
        if (item != null)
        {
            lstList.ClearSelection();
            item.Selected = true;
        }
    }

    public static void SelectValue(this ListControl lstList, string value)
    {
        if (lstList.Items.Count == 0)
            return;

        ListItem item = lstList.Items.FindByValue(value);
        if (item != null)
        {
            lstList.ClearSelection();
            item.Selected = true;
        }
    }


    public static void Sort(this DropDownList dropDownList)
    {
        //create a ListItem array the size of the items
        //in your DropDownList
        if (dropDownList.Items.Count == 0) return;
        string selectedValue = dropDownList.SelectedValue;
        var textList = new ArrayList();
        var valueList = new ArrayList();

        foreach (ListItem li in dropDownList.Items)
            textList.Add(li.Text);

        textList.Sort();

        foreach (object item in textList)
        {
            string value = dropDownList.Items.FindByText(item.ToString()).Value;
            valueList.Add(value);
        }
        dropDownList.Items.Clear();

        for (int i = 0; i < textList.Count; i++)
        {
            var objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
            dropDownList.Items.Add(objItem);
        }

        if (!string.IsNullOrEmpty(selectedValue)) dropDownList.SelectValue(selectedValue);
    }
}

public static class UIExtension
{
    public static void BindValidators<T>(this T ctrl, ControlCollection collection) where T : Control
    {
        string sendEmailValidationGroup = ctrl.UniqueID;
        collection.Cast<Control>().Where(c => c is BaseValidator).ToList().ForEach(
            v => (v as BaseValidator).ValidationGroup = sendEmailValidationGroup);
        (ctrl as IButtonControl).ValidationGroup = sendEmailValidationGroup;
    }
}

#region Web Extensions

public static class WebExtensions
{
    public static string UserIP(this HttpRequest request)
    {
        return string.IsNullOrEmpty(request.ServerVariables["HTTP_X_FORWARDED_FOR"])
                   ? request.ServerVariables["REMOTE_ADDR"]
                   : request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    }
}

#endregion

#endregion