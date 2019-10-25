using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
public class Sql
{
    static public DataTable Table(string sql)
    {
        return new Function().DataTable(sql);
    }

    static public DataTable Table(string sql,params string[] values)
    {
        return new Function().DataTableSafer(sql,values);
    }

    static public string Cell(string sql)
    {
        //try
        //{
            return new Function().DataTable(sql).Rows[0][0].ToString();
        //}
        //catch (Exception ex)
        //{
        //    return ex.Message;
        //}
      
    }

    static public string Cell(string sql,params string[] values)
    {
        //try
        //{
            return new Function().DataTableSafer(sql, values).Rows[0][0].ToString();
        //}
        //catch (Exception ex)
        //{
        //    return ex.Message;
        //}

    }

    static public void ExSql(string sql)
    {
       new Function().ExecuteSqlCommand(sql);
    }

    static public void ExSql(string sql,params string[] values)
    {
        new Function().ExecuteSqlCommandSafer(sql,values);
    }
}
public class Function
{
    //178.18.206.142 yüklerken localhost olarak değiştir.
    public static string hash = "Bu Şifreyi Çözersen Senin A** G**";

    public static string datebase = "db1_termalvadi";
    static public string conString = @"Server=178.18.206.142; Database=" + datebase + "; Uid=db1_Termalvadi19; Pwd=Termalvadi19.; convert zero datetime=True; Allow User Variables=True;";


    ////Eski
    //public static string datebase = "termalvadi";
    //static public string conString = @"Server=localhost; Database=" + datebase+ "; Uid=123456; Pwd=123456.; convert zero datetime=True; Allow User Variables=True;";
    

   // static public string conString = @"Server=localhost; Database=" + datebase+ "; Uid=root; Pwd=; convert zero datetime=True; Allow User Variables=True;";
    /// <summary>
    /// Güvenliksiz yere yazıyosan kesinlikle kullanma!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="komut"></param>
    /// <returns></returns>
    public DataTable DataTable(string komut)
    {
       // DataTableSafer("INSERT INTO table_name (column1, column2, column3) VALUES(?1, ?2, ?3); ","asd","bsd","csd");
        //try
        //{
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand(komut, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt.TableName = "table";
                            return dt;
                        }
                    }
                }
            }
        //}catch(Exception e)
        //{
        //    ExecuteSqlCommand("INSERT INTO `hata` (`id`, `tarih`, `kimde`, `islem`, `hatailetisi`,ayrinti) VALUES (NULL, CURRENT_TIMESTAMP, '', 'Çekilen Veriden Tablo Oluştururken!', '"+e.Message+ "', '" + e.ToString() + "');");
        //    return new System.Data.DataTable();
        //}

    }
    public DataTable DataTableSafer(string komut,params string[] values)
    {
        using (MySqlConnection con = new MySqlConnection(conString))
        {
            using (MySqlCommand cmd = new MySqlCommand(komut, con))
            {
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.Parameters.AddWithValue("?"+(i+1), values[i]);
                }
                
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        dt.TableName = "table";
                        return dt;
                    }
                }
            }
        }
    }
    public static bool IsLogin(string userNameOrEmail,string password)
    {
        using (MySqlConnection con = new MySqlConnection(conString))
        {
            using (MySqlCommand cmd = new MySqlCommand("select * from users where (UserName=?username or Email=?username) and Password=?pass", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("?username", userNameOrEmail);
                cmd.Parameters.AddWithValue("?pass", password);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                         sda.Fill(dt);
                        dt.TableName = "table";
                        if (dt.Rows.Count == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }
    }
    public static void SendPasswordMissingMail(string userMail)
    {
        


        SmtpClient smtpClient = new SmtpClient("smtp.termalvadi.com", 587);

        smtpClient.Credentials = new System.Net.NetworkCredential("noreply@termalvadi.com", "Noreply123456");
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

        MailMessage mailMessage = new MailMessage("noreply@termalvadi.com", "yusuf.kirenci@daimia.com");
       
        mailMessage.Subject = "esfg";
        mailMessage.Body = "dfsdf";
        smtpClient.Send(mailMessage);
    }
    public static string TranslateText(string input, string languagePair)
    {
        string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
        WebClient webClient = new WebClient();
        webClient.Encoding = System.Text.Encoding.UTF8;
        string result = webClient.DownloadString(url);
        result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
        result = result.Substring(result.IndexOf(">") + 1);
        result = result.Substring(0, result.IndexOf("</span>"));
        return result.Trim();
    }
    public static string FirstCharUpper(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "";
        }
        else
        {
            return value[0].ToString().ToUpper() + value.Remove(0, 1).Substring(0, value.Length - 1);
        }
    }
    public static string GeoCodeService(string konum)
    {
        try
        {
            string address = konum;
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false&key=AIzaSyCHaMbxb8eqbWqtmEtDfgR-WJ4b9WRetcE", Uri.EscapeDataString(address));
            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());
            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");
            return lat.Value.ToString() + "," + lng.Value.ToString();
        }
        catch (Exception)
        {
            return "41.3606393,33.731558";
        }

    }
    public static string GetIPAddress()
    {
        try
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        catch
        {
            return "000.000.000.000";
        }
    }
    public static string DataTableToJSONWithStringBuilder(DataTable table)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            JSONString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        }
        return JSONString.ToString();
    }
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
    public static string GetPrettyDate(DateTime d)
    {
        // 1.
        // Get time span elapsed since the date.
        TimeSpan s = DateTime.Now.Subtract(d);

        // 2.
        // Get total number of days elapsed.
        int dayDiff = (int)s.TotalDays;

        // 3.
        // Get total number of seconds elapsed.
        int secDiff = (int)s.TotalSeconds;

        // 4.
        // Don't allow out of range values.
        if (dayDiff < 0 || dayDiff >= 31)
        {
            return null;
        }

        // 5.
        // Handle same-day times.
        if (dayDiff == 0)
        {
            // A.
            // Less than one minute ago.
            if (secDiff < 60)
            {
                return "Şimdilerde";
            }
            // B.
            // Less than 2 minutes ago.
            if (secDiff < 120)
            {
                return "Bu Dakikalarda";
            }
            // C.
            // Less than one hour ago.
            if (secDiff < 3600)
            {
                return string.Format("{0} Dakika Önce",
                    Math.Floor((double)secDiff / 60));
            }
            // D.
            // Less than 2 hours ago.
            if (secDiff < 7200)
            {
                return "Bu Saatlerde";
            }
            // E.
            // Less than one day ago.
            if (secDiff < 86400)
            {
                return string.Format("{0} Saat Önce",
                    Math.Floor((double)secDiff / 3600));
            }
        }
        // 6.
        // Handle previous days.
        if (dayDiff == 1)
        {
            return "Dün";
        }
        if (dayDiff < 7)
        {
            return string.Format("{0} Gün Önce",
                dayDiff);
        }
        if (dayDiff < 31)
        {
            return string.Format("{0} Hafta Önce",
                Math.Ceiling((double)dayDiff / 7));
        }
        if (dayDiff >= 31)
        {
            return string.Format(d.ToLongDateString(),
                Math.Ceiling((double)dayDiff / 7));
        }
        return null;
    }
    static public DataTable ExcelToDS(string Path)
    {
        //Provider =Microsoft.ACE.OLEDB.12.0; Data Source =c:\myFolder\myExcel2007file.xlsx; Extended Properties ="Excel 12.0 Xml;HDR=YES"
        string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Path+"; Extended Properties =\"Excel 12.0 Xml; HDR = YES;IMEX=1;\"";
        //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0; " +"Data Source = "+ Path +"; "+"Extended Properties = Excel 12.0; ";
        OleDbConnection conn = new OleDbConnection(strConn);
        conn.Open();
        System.Data.DataTable 
            dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string strExcel = "";
        OleDbDataAdapter myCommand = null;
        DataSet ds = null;
        strExcel = "select * from ["+ dt.Rows[0]["TABLE_NAME"] + "]";
        myCommand = new OleDbDataAdapter(strExcel, strConn);
        ds = new DataSet();
        myCommand.Fill(ds);
        conn.Close();
        return ds.Tables[0];
    }
    public static void AddError(Exception exception)
    {
        Sql.ExSql("INSERT INTO `errors` (`id`, `uid`, `message`, `ex`, `serialized`) VALUES (NULL, '', ?1, ?2, ?3);", exception.Message,exception.ToString(),Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }
    public static string GetUserDesktop(string Uid)
    {
        string returned = "";
        System.Data.DataTable dataTable = Sql.Table("select desktop from desktops where uid=" + Uid);
        if (dataTable.Rows.Count == 0)
        {
            returned = "<div class=\"col-md-12\">                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-3 ui-sortable\"></div>                        <div class=\"column col-md-3 ui-sortable\"></div>                        <div class=\"column col-md-3 ui-sortable\"></div>                        <div class=\"column col-md-3 ui-sortable\"></div>                    </div>                     <div class=\"col-md-12\">                        <div class=\"column col-md-4 ui-sortable\"></div>                        <div class=\"column col-md-4 ui-sortable\"></div>                        <div class=\"column col-md-4 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-6 ui-sortable\"></div>                        <div class=\"column col-md-6 ui-sortable\"></div>                    </div>                     <div class=\"col-md-12\">                        <div class=\"column col-md-8 ui-sortable\"></div>                        <div class=\"column col-md-4 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-5 ui-sortable\"></div>                        <div class=\"column col-md-7 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-12 ui-sortable\"></div>                    </div>";
        }
        else if (string.IsNullOrWhiteSpace(dataTable.Rows[0][0].ToString()))
        {
            returned = "<div class=\"col-md-12\">                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                        <div class=\"column col-md-2 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-3 ui-sortable\"></div>                        <div class=\"column col-md-3 ui-sortable\"></div>                        <div class=\"column col-md-3 ui-sortable\"></div>                        <div class=\"column col-md-3 ui-sortable\"></div>                    </div>                     <div class=\"col-md-12\">                        <div class=\"column col-md-4 ui-sortable\"></div>                        <div class=\"column col-md-4 ui-sortable\"></div>                        <div class=\"column col-md-4 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-6 ui-sortable\"></div>                        <div class=\"column col-md-6 ui-sortable\"></div>                    </div>                     <div class=\"col-md-12\">                        <div class=\"column col-md-8 ui-sortable\"></div>                        <div class=\"column col-md-4 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-5 ui-sortable\"></div>                        <div class=\"column col-md-7 ui-sortable\"></div>                    </div>                    <div class=\"col-md-12\">                        <div class=\"column col-md-12 ui-sortable\"></div>                    </div>";
        }
        else
        {
            returned = dataTable.Rows[0][0].ToString();
        }
        return returned;
    }
    public static string RebuildQueryString(NameValueCollection nameValue,string name,string value)
    {
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        for (int i = 0; i < nameValue.Keys.Count; i++)
        {
            keyValuePairs.Add(nameValue.Keys[i],nameValue[i]);
        }
        if (!keyValuePairs.ContainsKey(name))
        {
            keyValuePairs.Add(name,value);
        }
        else
        {
            keyValuePairs[name]=value;
        }
        string temp = "?";
        for (int i = 0; i < keyValuePairs.Count; i++)
        {
            temp += keyValuePairs.ElementAt(i).Key + "=" + keyValuePairs.ElementAt(i).Value;
            if (keyValuePairs.Count != i+1)
            {
                temp += "&";
            }
        }
        return temp;
    }
    public static string RebuildQueryString(NameValueCollection nameValue, string name, string value,string deletingName,string deletingName2)
    {
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        for (int i = 0; i < nameValue.Keys.Count; i++)
        {
            if (nameValue.Keys[i] == deletingName) continue;if (nameValue.Keys[i] == deletingName2) continue;
            keyValuePairs.Add(nameValue.Keys[i], nameValue[i]);
        }
        //if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
        //{
            if (!keyValuePairs.ContainsKey(name))
            {
                keyValuePairs.Add(name, value);
            }
            else
            {
                keyValuePairs[name] = value;
            }
        //}

        string temp = "?";
        for (int i = 0; i < keyValuePairs.Count; i++)
        {
            temp += keyValuePairs.ElementAt(i).Key + "=" + keyValuePairs.ElementAt(i).Value;
            if (keyValuePairs.Count != i + 1)
            {
                temp += "&";
            }
        }
        return temp;
    }
    public static string RebuildQueryString(NameValueCollection nameValue, string name, string value,string deletingName)
    {
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        for (int i = 0; i < nameValue.Keys.Count; i++)
        {
            if (nameValue.Keys[i] == deletingName) continue;
            keyValuePairs.Add(nameValue.Keys[i], nameValue[i]);
        }
        //if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
        //{
            if (!keyValuePairs.ContainsKey(name))
            {
                keyValuePairs.Add(name, value);
            }
            else
            {
                keyValuePairs[name] = value;
            }
        //}

        string temp = "?";
        for (int i = 0; i < keyValuePairs.Count; i++)
        {
            temp += keyValuePairs.ElementAt(i).Key + "=" + keyValuePairs.ElementAt(i).Value;
            if (keyValuePairs.Count != i + 1)
            {
                temp += "&";
            }
        }
        return temp;
    }
    /// <summary>
    /// Güvenliksiz yere yazıyosan kesinlikle kullanma!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="komut"></param>
    /// <returns></returns>
    public void InsertUpdateIntoTable(string tabloAdi,Dictionary<string,string> dictionary)
    {
        string JoinedKeys = string.Join(",", dictionary.Keys);
        string joinedValues = string.Join(",", dictionary.Values);
        string sqlcmd = "INSERT INTO `" + tabloAdi+ "`(" + JoinedKeys + ") VALUES (" + joinedValues + ");";
        ExecuteSqlCommand(sqlcmd);
    }
    /// <summary>
    /// Güvenliksiz yere yazıyosan kesinlikle kullanma!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="komut"></param>
    /// <returns></returns>
    public string InsertUpdateIntoTable(string tabloAdi, Dictionary<string, string> dictionary,bool returnid)
    {
        string JoinedKeys = string.Join(",", dictionary.Keys);
        string joinedValues = string.Join(",", dictionary.Values);
        string sqlcmd = "INSERT INTO `" + tabloAdi + "`(" + JoinedKeys + ") VALUES (" + joinedValues + ");";
        return ExecuteSqlCommand(sqlcmd,returnid:true);
    }
    public static void CreateEIF(string TableName, string ConnectedTable)
    {
        string sqlcmd = ("CREATE TABLE IF NOT EXISTS `" + TableName + "_"+ConnectedTable+ "_eif` ( `id` INT NOT NULL AUTO_INCREMENT , `ParentObjectId` INT NOT NULL, `ChildObjectId` INT NOT NULL,   PRIMARY KEY (`id`)) ENGINE = InnoDB;");
        if (new Function().DataTable("select TableName from eifobject where tablename='" + TableName + "' and ConnectedTable='" + ConnectedTable + "'").Rows.Count == 0)
        {
            new Function().ExecuteSqlCommand("insert into eifobject (TableName,ConnectedTable) values ('" + TableName + "','" + ConnectedTable + "');");
        }
        else
        {
            new Function().ExecuteSqlCommand("update eifobject set tablename='" + TableName + "',ConnectedTable='" + ConnectedTable + "' where tablename='" + TableName + "'");
        }
        new Function().ExecuteSqlCommand(sqlcmd);
    }
    public static void CreateClassTable(string TableName, string TableTitle, string Description)
    {
        string sqlcmd=("CREATE TABLE IF NOT EXISTS `"+TableName+ "` ( `id` INT NOT NULL AUTO_INCREMENT, `old` INT NOT NULL , `old_id` INT NOT NULL ,`last_user_id` INT NOT NULL  , `deleted` INT NOT NULL ,`flowchart` TEXT NULL,`lock_by` INT NOT NULL,`created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,`changed` TIMESTAMP on update CURRENT_TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,   PRIMARY KEY (`id`)) ENGINE = InnoDB;");
        if(new Function().DataTable("select TableName from itemtable where tablename='"+TableName+"'").Rows.Count == 0)
        {
            new Function().ExecuteSqlCommand("insert into itemtable (TableName,TableTitle,Description) values ('" + TableName + "','" + TableTitle + "','" + Description + "');");
        }
        else
        {
            new Function().ExecuteSqlCommand("update itemtable set tablename='"+TableName+"',tabletitle='"+TableTitle+"',description='"+Description+"'  where tablename='"+TableName+"'");
        }
        new Function().ExecuteSqlCommand(sqlcmd);
    }
    public static string ChangeLegalChars(string val)
    {
        val = val.Trim();
        val = val.Replace("ç", "c").Replace("Ç", "C").Replace("ş", "s").Replace("Ş", "S").Replace("ö", "o").Replace("Ö", "O").Replace("Ü", "U").Replace("ü", "u").Replace("ı", "i").Replace("İ", "I").Replace("ğ", "g").Replace("Ğ", "G");
        string charSet = "abcdefghijklmnoprstuvyzwxABCDEFGHJKLMNOPRSTUVYZWX";
        string returned = "";
        foreach (var valItem in val)
        {
            char? tmpChr = null;
            foreach (var charItem in charSet)
            {
                if (valItem==charItem)
                {
                    tmpChr = valItem;
                    break;
                }
            }
            
            returned += tmpChr!=null?tmpChr.ToString():"";
        }
        return returned;
    }
    public static string CreateInputWithColumn(string data,string dataType,string columnTitle,string columnName,string description,bool innerUpload=false)
    {
        string returning = "";
             if (dataType == "Text")
        {
            returning = "<div class=\"form-group\">" +
                              "<label for=\"" + columnName + "_textbox\">" + columnTitle + "</label>" +
                              "<input type=\"text\" value=\"" + data + "\" name=\"" + columnName + "\" class=\"form-control\" id=\"" + columnName + "_textbox\" placeholder=\"Enter " + columnTitle + "\">" +
                              "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>" +
                        "</div>";
        }
        else if (dataType == "Integer")
        {
            returning = "<div class=\"form-group\">" +
                              "<label for=\"" + columnName + "_textbox\">" + columnTitle + "</label>" +
                              "<input type=\"number\" value=\"" + data + "\" name=\"" + columnName + "\" class=\"form-control\" id=\"" + columnName + "_textbox\" placeholder=\"Enter " + columnTitle + "\">" +
                              "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>" +
                        "</div>";
        }
        else if (dataType == "Decimal")
        {
            returning = "<div class=\"form-group\">" +
                              "<label for=\"" + columnName + "_textbox\">" + columnTitle + "</label>" +
                              "<input type=\"number\" value=\"" + data + "\" name=\"" + columnName + "\" class=\"form-control\" id=\"" + columnName + "_textbox\" placeholder=\"Enter " + columnTitle + "\">" +
                              "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>" +
                        "</div>";
        }
        else if (dataType == "Bool")
        {
            returning = "<div class=\"form-check\">" +
                        "<label class=\"form-check-label\">" +
                            "<input type=\"hidden\" id=\"" + columnName + "_hidden_chkbx\" value=\"" + (string.IsNullOrWhiteSpace(data)?"0":data) + "\" name=\"" + columnName + "\" >" +
                            "<input id=\"" + columnName + "_chkbx\" class=\"form-check-input\" onchange=\"CheckBoxSetValueFuncTo" + columnName + "();\" type=\"checkbox\" " + (data == "1" ? "checked" : "") + " >" +
                            "<span class=\"form-check-sign\">"+columnTitle+"</span>" +
                        "</label>" +
                         "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>"+
                    "</div>" +
                    "<script>" +
                    "function CheckBoxSetValueFuncTo" + columnName + "() {" +
                    "  var checkBox = document.getElementById(\"" + columnName + "_chkbx\");" +
                    "  var text = document.getElementById(\"" + columnName + "_hidden_chkbx\");" +
                    "  if (checkBox.checked == true){" +
                    "    text.value = \"1\";" +
                    "  } else { " +
                    "    text.value = \"0\"; " +
                    "  }" +
                    "}" +
                    "</script>";
        }
        else if (dataType == "File")
        {
            //?i=upload data buraya yönlenmeli
            returning = "<div class=\"form-group\">" +
                "<label for=\""+columnName+"\" ><b>"+columnTitle+"</b>  "+ "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>" + "</label>" +
                "<label for=\""+columnName+"\" class=\"col-md-12 btn btn-light fileinput-button\">" +
                "<i class=\"glyphicon glyphicon-plus\">Select File<span id=\"" + columnName + "_ending_returneddata_vieving\"><i> <a href=\""+data+"\" target=\"blank\" >"+Path.GetFileName(data)+"</a></i></span></i>" +
                "<input  id=\"" + columnName + "\" style=\"width: 100%;opacity:0;\" type=\"file\" name=\"files\">" +
                "</label>" +
                "<div id=\"" + columnName + "_progress_bar\" class=\"progress\">" +
                "<div class=\"progress-bar progress-bar-success\"></div>" +
                "</div>" +
                "</div>" +
                "<input type=\"hidden\" id=\"" + columnName + "_hidden_value\" name=\"" + columnName + "\" value=\""+data+"\" />" +
                "<script>" +
                "$(document).ready(function () {" +
                "'use strict';" +
                "var url = '"+(!innerUpload? "Function.aspx" : "")+"?i=upload';" +
                "$('#" + columnName + "').fileupload({" +
                "url: url," +
                "dataType: 'json'," +
                "done: function (e, data) {" +
                "$.each(data.result.files, function (index, file) {" +
                "if (file.error) {" +
                "$('#" + columnName + "_ending_returneddata_vieving').text(file.error);" +
                "} else {" +
                "$('#" + columnName + "_ending_returneddata_vieving').html('<a href=\"' + file.url + '\" target=\"blank\" >Uploaded File : ' + file.name + '</a>');" +
                "$('#" + columnName + "_hidden_value').val(file.url);" +
                "}" +
                "});" +
                "}," +
                "progressall: function (e, data) {" +
                "var progress = parseInt(data.loaded / data.total * 100, 10);" +
                "$('#" + columnName + "_progress_bar .progress-bar').css(" +
                "'width'," +
                "progress + '%'" +
                ");" +
                "}" +
                "}).prop('disabled', !$.support.fileInput)" +
                ".on('fileuploadadd', function (e, data) {" +
                "$('#" + columnName + "_progress_bar .progress-bar').css(" +
                "'width'," +
                "0 + '%'" +
                ");" +
                "$('#" + columnName + "_hidden_value').val('');" +
                "$('#" + columnName + "_ending_returneddata_vieving').html('No File Selected');" +
                "})" +
                ".parent().addClass($.support.fileInput ? undefined : 'disabled');" +
                "});" +
                "</script>";
        }
        else if (dataType == "Date")
        {
            returning = "<div class=\"form-group\">" +
                              "<label for=\"" + columnName + "_textbox\">" + columnTitle + "</label>" +
                              "<input type=\"text\" value=\"" + (string.IsNullOrWhiteSpace(data)?DateTime.Now.ToString("yyyy-MM-dd") :Convert.ToDateTime(data).ToString("yyyy-MM-dd")) + "\" name=\"" + columnName + "\" class=\"form-control\" id=\"" + columnName + "_textbox_picker\" placeholder=\"Enter " + columnTitle + "\">" +
                              "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>" +
                        "</div>"+
                           " <script type=\"text/javascript\">" +
                                "$(function () {" +
                                        "jQuery('#" + columnName + "_textbox_picker').datetimepicker({" +
                                            "timepicker: false,"+
                                            "format: 'Y-m-d'"+
                                        "});"+
                                "});" +
                            "</script>";
        }
        else if (dataType == "DateTime")
        {
            returning = "<div class=\"form-group\">" +
                              "<label for=\"" + columnName + "_textbox\">" + columnTitle + "</label>" +
                              "<input type=\"text\" value=\"" + (string.IsNullOrWhiteSpace(data) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm") : Convert.ToDateTime(data).ToString("yyyy-MM-dd HH:mm")) + "\" name=\"" + columnName + "\" class=\"form-control\" id=\"" + columnName + "_textbox_picker\" placeholder=\"Enter " + columnTitle + "\">" +
                              "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>" +
                        "</div>"+
                           "<script type=\"text/javascript\">" +
                                "$(function () {" +
                                        "jQuery('#" + columnName + "_textbox_picker').datetimepicker({" +
                                            "timepicker: true," +
                                            "format: 'Y-m-d H:i'" +
                                        "});" +
                                "});" +
                            "</script>";
        }
        else
        {
            returning = "<div class=\"form-group\">" +
                            "<label for=\"" + columnName + "_textbox_viewed\">" + columnTitle+"</label>" +
                                "<div class=\"input-group mb-3\">" +
                                    "<div class=\"input-group-prepend\">" +
                                        "<span class=\"input-group-text\" id=\"" + columnName + "_add_on\">" + dataType+".</span>" +
                                    "</div>" +
                                "<input type=\"hidden\" value=\""+data+"\" name=\""+ columnName + "\" id=\"" + columnName + "_hidden_value\" />" +
                                "<input type=\"text\" onclick=\"" + columnName + "GetSelectionModal()\" value=\"" +(string.IsNullOrWhiteSpace(data)?"Click To Select An Item":"")+/* IdFromMainColumnValue(dataType,data))+*/"\" class=\"form-control\" id=\"" + columnName + "_textbox_viewed\" aria-describedby=\"" + columnName + "_add_on\" readonly>" +
                                "</div>" +
                             "<small id=\"" + columnName + "_Description\" class=\"form-text text-muted\">" + description + "</small>" +
                        "</div>"+
                        "<div class=\"modal fade\" id=\"" + columnName + "SelectionMsgBxModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"exampleModalLabel\" aria-hidden=\"true\">" +
                        "<div class=\"modal-dialog modal-lg bd-example-modal-lg\" role=\"document\">" +
                        "<div class=\"modal-content\">" +
                        "<div class=\"modal-header\">" +
                        "<h5 class=\"modal-title\"><span class=\"btn btn-circle btn-success\"><span class=\" fa fa-info\"></span></span>  Select A " + dataType + " Item</h5>" +
                        "<button class=\"close\" type=\"button\" data-dismiss=\"modal\" aria-label=\"Close\">" +
                        "<span aria-hidden=\"true\">×</span>" +
                        "</button>" +
                        "</div>" +
                        "<div class=\"modal-body\" id=\"" + columnName + "SelectionMsgBx\">" +
                        "</div>" +
                        "<div class=\"modal-footer\">" +
                        "<button class=\"btn btn-danger\" type=\"button\" data-dismiss=\"modal\">Cancel</button>" +
                        "<button class=\"btn btn-info\" data-dismiss=\"modal\" type=\"button\" onclick=\"" + columnName + "SelectItem(document.getElementById('" + columnName + "selectingiframe').contentWindow.selectedId);\" >Select</button>" +
                        "</div>" +
                        "</div>" +
                        "</div>" +
                        "</div>"+
                        "<script>" +
                        " function " + columnName + "GetSelectionModal() {" +
                            "$('#" + columnName + "SelectionMsgBx').empty().append('<iframe src=\"TableViewer.aspx?onlyUseSelecting=1&tableName="+ Function.Sifrele(dataType)+ "\" id=\"" + columnName + "selectingiframe\" style=\"width:100%;height:700px;border:none;\" ></iframe>');" +
                           " $('#" + columnName + "SelectionMsgBxModal').modal('show');" +
                        "}"+
                        "function " + columnName + "SelectItem(id) {" +
                            "$('#" + columnName + "_hidden_value').val(id);" +
                           "var posting = $.post('?i=IdFromMainColumnValue&tableName="+Function.Sifrele(dataType)+"&id='+id,null);" +
                           "posting.done(function (data) {" +
                           "$('#" + columnName + "_textbox_viewed').val(data)" +
                           "});" +
                        "}" +
                        "</script>";
        }
        return returning;
    }
    /// <summary>
    /// Güvenliksiz yere yazıyosan kesinlikle kullanma!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="komut"></param>
    /// <returns></returns>
    public void InsertUpdateIntoTable(string tabloAdi,  Dictionary<string, string> dictionary, Dictionary<string, string> where)
    {

        List<string> listid = new List<string>();
        foreach (var item in where.Keys)
        {
            listid.Add(item + "=" + where[item]);
        }
        string JoinedValuesid = string.Join(" AND ", listid);

        List<string> list = new List<string>();
        foreach (var item in dictionary.Keys)
        {
            list.Add(item+"="+dictionary[item]);
        }
        string JoinedValues = string.Join(",", list);

        string sqlcmd = "UPDATE " + tabloAdi + " SET " + JoinedValues + " WHERE " + JoinedValuesid + " ;";
        ExecuteSqlCommand(sqlcmd);
    }
    /// <summary>
    /// Güvenliksiz yere yazıyosan kesinlikle kullanma!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="komut"></param>
    /// <returns></returns>
    public void ExecuteSqlCommand(string komut)
    {
        MySqlConnection mySqlConnection = new MySqlConnection(conString);
        MySqlCommand myCommand = new MySqlCommand(komut, mySqlConnection);
       
        myCommand.Connection.Open();
        myCommand.ExecuteNonQuery();
        mySqlConnection.Close();
    }
    public void ExecuteSqlCommandSafer(string komut, params string[] values)
    {
        MySqlConnection mySqlConnection = new MySqlConnection(conString);
        MySqlCommand myCommand = new MySqlCommand(komut, mySqlConnection);
        for (int i = 0; i < values.Length; i++)
        {
            myCommand.Parameters.AddWithValue("?" + (i + 1), values[i]);
        }
        myCommand.Connection.Open();
        myCommand.ExecuteNonQuery();
        mySqlConnection.Close();
    }
    /// <summary>
    /// Güvenliksiz yere yazıyosan kesinlikle kullanma!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="komut"></param>
    /// <returns></returns>
    public string ExecuteSqlCommand(string komut, bool returnid)
    {
        if (returnid)
        {
            return new Function().DataTable(komut + " select LAST_INSERT_ID();").Rows[0][0].ToString();
        }
        else
        {
            new Function().ExecuteSqlCommand(komut);
            return "";
        }
    }
    public static string GetImageFilePath(string FileName)
    {
        return HttpContext.Current.Server.MapPath("~/Upload/" + FileName).ToString();
    }
    public static string GetFileUrl(string FileName)
    {
        return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Upload/" + FileName;
    }
    public static string CreateRandomNamedFolder()
    {
        string tmpName = CreateFileName("");
        if (!Directory.Exists(tmpName))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/"+tmpName));
        }
        return tmpName;
    }
    /// <summary>
    /// /
    /// </summary>
    /// <param name="folderName">"~/Upload/"</param>
    /// <returns></returns>
    public static string CreateRandomNamedFolder(string folderName)
    {
        string tmpName = CreateFileName("");
        if (!Directory.Exists(tmpName))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderName + tmpName));
        }
        return tmpName;
    }
    public static void CopyStream(Stream stream, string destPath)
    {
        using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(fileStream);
        }
    }
    public static Bitmap ResizeImage(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }
    public static string UploadFile(HttpPostedFile file)
    {
        if (file != null && file.ContentLength > 0)
        {
            string extention = Path.GetExtension(file.FileName);
            string filename = Function.CreateFileName("-File" + extention);
            string fullfilename = Function.GetImageFilePath(filename);
            file.SaveAs(fullfilename);
            return GetFileUrl(filename);
        }
        return "";
    }
    public static string UploadFileReturnJson(HttpPostedFile file)
    {
        string json = "";
        try
        {
            if (file != null && file.ContentLength > 0)
            {
                string folderName =CreateRandomNamedFolder();
                string extention = Path.GetExtension(file.FileName);
                string filename = file.FileName;
                string mimeType = file.ContentType;
                int fileSize = file.ContentLength;
                string fullfilename = Function.GetImageFilePath(folderName + "/"+filename);
                file.SaveAs(fullfilename);
                string fileUrl = GetFileUrl(folderName + "/" + filename);
                json +=
                    "{ " +
                    "fieldname: 'file'," +
                    " originalname: '" + filename + "'," +
                    //"  encoding: '7bit'," +
                    " mimetype: '" + mimeType + "'," +
                    "  destination: 'Upload/'," +
                    " filename: '" + fullfilename + "'," +
                    "  path: '" + fileUrl + "', " +
                    " size: " + fileSize + "" +
                    " }";
            }
            else
            {
                json = ""; json += "{ \"files\":[{";
                json += "\"error\":\"The File Null Or Empty.\"";
                json += "}]}";
            }
        }
        catch (Exception ex)
        {
            json = ""; json += "{ \"files\":[{";
            json += "\"error\":\""+ex.Message+".\"";
            json += "}]}";
        }
        return json;
    }
    public static string UploadImage(string Image)
    {
      string s = SaveImage(Image);
      return new Function().DataTable("INSERT INTO `fotograflar` (`id`, `fotograf`) VALUES (NULL, '" + s + "'); select LAST_INSERT_ID();").Rows[0][0].ToString();
    }
    public static string UploadVideo(string video)
    {
        string s = SaveVideo(video);
        return new Function().DataTable("INSERT INTO `fotograflar` (`id`, `fotograf`) VALUES (NULL, '" + s + "'); select LAST_INSERT_ID();").Rows[0][0].ToString();
    }
    public static string SaveImage(string base64String)
    {



        string filename = CreateFileName("-Photo.png");
        string fullfilename = GetImageFilePath(filename);
        //GC.Collect();
        string base64 = base64String.Split(',')[1];
        byte[] imageBytes = Convert.FromBase64String(base64);
        File.WriteAllBytes(fullfilename, imageBytes);
        //string testfullpath = "C:\\" + filename;
        //Base64ToImage(base64String).Save(testfullpath);

        //File.WriteAllText(fullfilename, "testesdteywryhadsfhgadfhasdjhasdjsfgj");
        //File.WriteAllBytes(fullfilename,ImageToByteArray(Base64ToImage(base64String)));
        return GetFileUrl(filename);

    }
    public static string SaveVideo(string base64String)
    {
        string filename = Function.CreateFileName("-Video.mp4");
        string fullfilename = Function.GetImageFilePath(filename);
        //GC.Collect();
        string base64 = base64String.Split(',')[1];
        byte[] imageBytes = Convert.FromBase64String(base64);
        File.WriteAllBytes(fullfilename, imageBytes);
        //string testfullpath = "C:\\" + filename;
        //Base64ToImage(base64String).Save(testfullpath);

        //File.WriteAllText(fullfilename, "testesdteywryhadsfhgadfhasdjhasdjsfgj");
        //File.WriteAllBytes(fullfilename,ImageToByteArray(Base64ToImage(base64String)));
        return GetFileUrl(filename);

    }
    public static string CreateFileName(string dosyauzantisi)
    {
        string temp = "";
        string chara = "abcdefghijklmnoprstuvyzABCDEFGHIJKLMNOPRSTUVYZ123456789";
        Random r = new Random();
        for (int i = 0; i < 25; i++)
        {
            temp += chara[r.Next(0, chara.Length - 1)];
        }
        temp += DateTime.Now.ToString("ddMMyyyyhhmmss");
        temp += new Random().Next(1000, 9999);
        temp += dosyauzantisi;
        return temp;
    }
    public static string CreateSearchSqlStringBuilderAllColumns(string tableName,string searchKeyword)
    {
        DataTable dataTable = Sql.Table("select * from "+tableName+" where id=-1");
        List<string> vs = new List<string>();
        foreach (DataColumn item in dataTable.Columns)
        {
            vs.Add(" `" + item.ToString() + "` LIKE '%"+searchKeyword+"%' ");
        }
        string JoinedKeys = string.Join(" OR ", vs);
        string end = "Select * from " + tableName +" where "+ JoinedKeys;
        return end;
    }
    public static string UrlDecrypt(string url)
    {
        string a = url;
        a = a.Replace(" ", "+");
        int mod4 = a.Length % 4;
        if (mod4 > 0)
        {
            a += new string('=', 4 - mod4);
        }
        return a;
    }
    public static string Encrypt(string sifre)
    {
        byte[] data = UTF8Encoding.UTF8.GetBytes(sifre);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(results, 0, results.Length);
            }
        }
    }
    public static string Decrypt(string SifrelenmisDeger)
    {
        byte[] data = Convert.FromBase64String(SifrelenmisDeger);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = tripDes.CreateDecryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                return UTF8Encoding.UTF8.GetString(results);
            }
        }
    }
    public static string Sifrele(string value){
        return Encrypt(value);
    }
    public static string SifreyiCoz(string value)
    {
        return Function.Decrypt(Function.UrlDecrypt(value));
    }
    public static bool MailKontrol(string mail)
    {
        try
        {
             System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(mail);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }/// <summary>
    /// IsNumeric
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsNumeric(string text)
    {
        foreach (var item in text)
        {
            if (!char.IsNumber(item))
            {
                return false;
            }
          
        }
        return true;
    }

    /// <summary>
    /// SignUpTermalVadi
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="phone"></param>
    /// <param name="name"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="againpassword"></param>
    public static void SignUpTermalVadi(string mail ,string phone,string name,string username, string password, string againpassword)
    {
        if (string.IsNullOrWhiteSpace(mail)) throw new Exception("Email Boş geçilemez");
        if (string.IsNullOrWhiteSpace(phone)) throw new Exception("Telefon Boş geçilemez");
        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Ad Soyad Boş geçilemez");
        if (string.IsNullOrWhiteSpace(username)) throw new Exception("Kullanıcı Adı Boş geçilemez");
        if (string.IsNullOrWhiteSpace(password)) throw new Exception("Şifre Boş geçilemez");
        if (new Function().DataTable("Select UserName from users where username ='" + username + "'").Rows.Count != 0) throw new Exception("Girilen Kullanıcı Adı Kullanılmıştır!");
        if (new Function().DataTable("Select Email from users where Email ='" + mail + "'").Rows.Count != 0) throw new Exception("Girilen Email Kullanılmıştır!");
        if (new Function().DataTable("Select Phone from users where Phone ='" + phone+ "'").Rows.Count != 0) throw new Exception("Girilen Telefon Kullanılmıştır!");
        if (password.Length < 6) throw new Exception("Şifre 6 karakterden küçük olamaz");
        if (password != againpassword) throw new Exception("Girilen Şifreler Uyuşmamaktadır.");
        if (!MailKontrol(mail)) throw new Exception("Lütfen doğru Bir Email Giriniz.");
        //if (!IsNumeric(phone)) throw new Exception("Lütfen Geçerli Bir Telefon Giriniz.");

        string phoneWithOutMask = phone.Replace("(", "").Replace(")","").Replace(" ","").Replace("-","");
        var surnameSplit = "";
        var nameSplit = "";

        var nameSplited = name.Split(' ');
        if(nameSplited.Length == 1)
        {
            nameSplit = nameSplited[0];
        }
        else
        {
            nameSplit = nameSplited[0];
            surnameSplit = nameSplited[1];
        }

       

        MySqlConnection mySqlConnection = new MySqlConnection(conString);
        MySqlCommand myCommand = new MySqlCommand($"insert into users (UserName,Email,Password,Phone,Name,Surname) values (?username1,?email1,?password1,?phone1,?nameSplit1,?surnameSplit1)", mySqlConnection);
        myCommand.Parameters.AddWithValue("?username1", username);
        myCommand.Parameters.AddWithValue("?email1", mail);
        myCommand.Parameters.AddWithValue("?password1", password);
        myCommand.Parameters.AddWithValue("?phone1", phoneWithOutMask);
        myCommand.Parameters.AddWithValue("?nameSplit1", nameSplit);
        myCommand.Parameters.AddWithValue("?surnameSplit1", surnameSplit);
        myCommand.Connection.Open();
        myCommand.ExecuteNonQuery();
        mySqlConnection.Close();
    }
    public static string RastgeleYazi()
    {
        string[] abc = new string[] { "Doğa ile iç içe huzur içinde bir tatile ne derseniz.", "Kaplıcaların doğal yapısı ve içinde bulunan minerallerin etkisi, şifa kaynağı !", "Romatizmal hastalıklar, kas ve iskelet sistemi hastalıkları ,deri hastalıkları tedavilerine katkı...", "Her şey kötüye gittiğinde kendine bir tatil ısmarla. – Betty Williams", "Senede bir defa daha önce hiç görmediğin bir yere git. – Dalai Lama", "Bilmediğin bir yere gitmek, bilmediğin bir yönünü keşfetmektir. Martin Burber ", "Stresten uzaklaşmak, hayatı yaşamak için tatile başlayın.", "Tatil varsa ben de varım...", "En iyi termal otellerde bir tatile hazır mısınız?", "Her bütçeye uygun en iyi konaklama seçenekleri.." };
        return abc[new Random().Next(0, abc.Length-1)];
    }

}
