<%@ Page Language="C#" %>

<% var kullaniciid = Request.Cookies["id"].Value;%>





<%if (Request.QueryString["p"] == "confirm")
    {
        //new Function().ExecuteSqlCommand($"Delete from gorevler where id ='" +eventId+"'");
        new Function().DataTable("UPDATE users SET isConfirm = 1 where id='" + Request.QueryString["id"]  + "';"); 
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>
<%if (Request.QueryString["p"] == "notconfirm")
    {
        //new Function().ExecuteSqlCommand($"Delete from gorevler where id ='" +eventId+"'");
        new Function().DataTable("UPDATE users SET isConfirm = 0 where id='" + Request.QueryString["id"]  + "';"); 
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>

<%--<%else if (Request.QueryString["p"] == "deletegorev")
    {
        new Function().ExecuteSqlCommand($"Delete from {Request.QueryString["table"]} where id ={Request.QueryString["id"]}");
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
        // new Function().DataTable("update kullanicilar set bildirim =0 where id="  + Request.QueryString["id"] + "'");
    }
%>

<%else if (Request.QueryString["p"] == "deleteizin")
    {
        new Function().ExecuteSqlCommand($"Delete from izinler where id ={Request.QueryString["Id"]}");
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>

<%else if (Request.QueryString["p"] == "redizintalep")
    {
        new Function().ExecuteSqlCommand($"UPDATE izinler SET iStatü =  'Reddedildi.' where id= " + Request.QueryString["Id"]);
        new Function().ExecuteSqlCommand($"UPDATE izinler SET iAtayanKisi = '{adSoy}' where id= " + Request.QueryString["Id"]);
        //new Function().ExecuteSqlCommand($"Delete from izintalepler where id ={Request.QueryString["Id"]}");
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>

<%else if (Request.QueryString["p"] == "deleteizintalep")
    {
        new Function().ExecuteSqlCommand($"Delete from izinler where id ={Request.QueryString["Id"]}");
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>

<%else if (Request.QueryString["p"] == "deleteekip")
    {
        new Function().ExecuteSqlCommand($"Delete from bkm_ekipler where id ={Request.QueryString["Id"]}");
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>

<%else if (Request.QueryString["p"] == "deletepoz")
    {
        new Function().ExecuteSqlCommand($"Delete from bkm_pozisyonlar where id ={Request.QueryString["Id"]}");
        new Function().DataTable($"UPDATE kullanicilar SET pozisyon = 'Tanimsiz.' where id={Request.QueryString["id"]}"); 
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>

<%else if (Request.QueryString["p"] == "delgorev")
    {
        string eventId = Request.Form["id"];
        //new Function().ExecuteSqlCommand($"Delete from gorevler where id ='" +eventId+"'");
        new Function().DataTable("UPDATE gorevler SET teslimDurumu = 'Henüz Edilmedi.' where id=" + eventId + ";"); 
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>

<%else if (Request.QueryString["p"] == "logout")
    {
        HttpCookie aCookie;
        string cookieName;
        int limit = Request.Cookies.Count;
        for (int i = 0; i < limit; i++)
        {
            cookieName = Request.Cookies[i].Name;
            aCookie = new HttpCookie(cookieName);
            aCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(aCookie);
        }
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
    else if (Request.QueryString["p"] == "saveeventdate")
    {
        string eventTitle = Request.Form["title"];
        string eventStartTime = Request.Form["starttime"];
        string eventEndTime = Request.Form["endtime"];
        string eventId = Request.Form["id"];
        new Function().ExecuteSqlCommand($"UPDATE gorevler SET basTarihi ='{eventStartTime}',bitTarihi='{eventEndTime}' where id =" + eventId + ";");
        new Function().DataTable("UPDATE gorevler SET teslimDurumu = 'Teslim Edildi.' where id=" + eventId + ";"); // 0 olark update diyor
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }%>

<%else if (Request.QueryString["p"] == "geribildirim")
    {
        new Function().DataTable("update geri_bildirim set geriBildirimDurumu = '1' where id="  + Request.QueryString["id"] + "");
        //new Function().ExecuteSqlCommand($"Delete from izinler where id ={Request.QueryString["Id"]}");
        Response.Write("<body onload=\"window.location=document.referrer;\"></body>");
    }
%>--%>