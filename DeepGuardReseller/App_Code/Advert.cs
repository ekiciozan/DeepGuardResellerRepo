using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using TermalVadiWebApp.MenuRecursion;

namespace DataModel
{
    public class Advert
    {
        public int id;
        public User user;
        public int galeryId;
        public ImageGallery imageGallery;
        public Image thumbImage;
        public HierarshyElement hierarshyElement;
        public string title;
        public decimal price;
        public string address;
        public DateTime createdDate;
        public string type;
        public string period;
        public string typeOfHome;
        public string duration;
        public string floorLocation;// bulunduğu kat
        public int squareMeters;// metrekare
        public string isFurniture;// esyali mi
        public string numberOfRoom;// oda sayısı
        public string view; // manzara
        public string front; // cephe
        public string description;
        public int viewCount;

        public Advert(){}       
        public static List<Advert> GetMinimalDetailAdverts(string command="select id,gid,uid,hierarchyid,Title,Period,Duration,NumberOfRoom,Price,CreatedDate,Address,ViewCount from adverts",params string[] values)
        {
            List<Advert> adverts = new List<Advert>();
            DataTable dataTable = Sql.Table(command,values);
            foreach (DataRow item in dataTable.Rows)
            {
                adverts.Add(new Advert()
                {
                    id = (int)item["id"],
                    galeryId = (int)item["gid"],
                    thumbImage = GetThumbImage((int)item["gid"]),
                    user = new User((int)item["uid"]),
                    title = (string)item["Title"],
                    typeOfHome = (string)item["TypeId"].ToString(),
                    period = (string)item["Period"],
                    hierarshyElement = new HierarshyElement((int)item["hierarchyid"]),
                    duration = (string)item["Duration"],
                    numberOfRoom = (string)item["NumberOfRoom"],
                    price = (decimal)item["Price"],
                    createdDate = (DateTime)item["CreatedDate"],
                    address = (string)item["Address"],
                    viewCount = (int)item["ViewCount"],
                });
            }
            return adverts;
        }

        public static List<Advert> GetMaximizeDetailAdverts(string command = "select id,gid,uid,hierarchyid,Title,TypeId,SquareMeters,IsFurniture,Period,Duration,FloorLocation,NumberOfRoom,View,Front,Description,Price,CreatedDate,Address from adverts", params string[] values)
        {
            List<Advert> adverts = new List<Advert>();
            DataTable dataTable = Sql.Table(command, values);
            foreach (DataRow item in dataTable.Rows)
            {
                adverts.Add(new Advert()
                {
                    id = (int)item["id"],
                    galeryId = (int)item["gid"],
                    thumbImage = GetThumbImage((int)item["gid"]),

                    imageGallery = new ImageGallery((int)item["gid"]),
                    hierarshyElement = new HierarshyElement((int)item["hierarchyid"]),

                    user = new User((int)item["uid"]),
                    title = (string)item["Title"],
                    period = (string)item["Period"],
                    duration = (string)item["Duration"],
                    typeOfHome = (string)item["TypeId"].ToString(),
                    squareMeters =(int)item["SquareMeters"],
                    isFurniture = (string)item["IsFurniture"].ToString(),
                    floorLocation = (string)item["FloorLocation"].ToString(),
                    view = (string)item["View"].ToString(),
                    front = (string)item["Front"].ToString(),
                    description = (string)item["Description"].ToString(),
                    numberOfRoom = (string)item["NumberOfRoom"],
                    price = (decimal)item["Price"],
                    createdDate = (DateTime)item["CreatedDate"],
                    address = (string)item["Address"],
                });
            }
            return adverts;
        }

        public static Advert GetMaximizeDetailAdverts(string id)
        {
            return GetMaximizeDetailAdverts("select id,gid,uid,hierarchyid,Title,TypeId,SquareMeters,IsFurniture,Period,Duration,FloorLocation,NumberOfRoom,View,Front,Description,Price,CreatedDate,Address from adverts where id=?1", id)[0];
        }

        public static Image GetThumbImage(int galeriId)
        {
            DataTable dataTable = Sql.Table("select id from images where gid="+galeriId+" limit 1");
            if (dataTable.Rows.Count ==0)
            {
                return Image.GetNullImage();
            }
            else
            {
                return new Image((int)dataTable.Rows[0][0],Image.ImageNullType.NoneImage);
            }
            
        }
        public void AddViewCount()
        {
            Sql.ExSql("UPDATE adverts SET ViewCount = ViewCount+1 WHERE id = ?1",id.ToString());
        }

    }
    public class User
    {
        public int id;
        public string userName;
        public string email;
        public string password;
        public string phone;
        public Image profileImage;
        public string name;
        public string surname;
        public DateTime signUpDate;
        public User(int id)
        {
            DataTable dataTable = Sql.Table("select * from users where id=?1", id.ToString());
            this.id = (int)dataTable.Rows[0]["id"];
            this.userName = (string)dataTable.Rows[0]["UserName"];
            this.email = (string)dataTable.Rows[0]["Email"];
            this.password = (string)dataTable.Rows[0]["Password"];
            this.phone = (string)dataTable.Rows[0]["Phone"];
            profileImage = new Image((int)dataTable.Rows[0]["ProfileImageId"],Image.ImageNullType.ProfileImage);
            this.name = (string)dataTable.Rows[0]["Name"];
            this.surname = (string)dataTable.Rows[0]["Surname"];
            this.signUpDate = (DateTime)dataTable.Rows[0]["SignUpDate"];

        }
        public static User GetUserWithMailOrUserName(string mailOrUsername)
        {
            DataTable dataTable = Sql.Table("select * from users where UserName=?1 or Email=?1", mailOrUsername);
            if (dataTable.Rows.Count == 0) return null;
            User user = new User()
            {
                id = (int)dataTable.Rows[0]["id"],
                userName = (string)dataTable.Rows[0]["UserName"],
                email = (string)dataTable.Rows[0]["Email"],
                password = (string)dataTable.Rows[0]["Password"],
                phone = (string)dataTable.Rows[0]["Phone"],
                profileImage = new Image((int)dataTable.Rows[0]["ProfileImageId"], Image.ImageNullType.ProfileImage),
                name = (string)dataTable.Rows[0]["Name"],
                surname = (string)dataTable.Rows[0]["Surname"],
                signUpDate = (DateTime)dataTable.Rows[0]["SignUpDate"],
            };
            return user;

        }
        public User() { }
        public void SendPasswordMissingMail()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.termalvadi.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("noreply@termalvadi.com", "Noreply123456");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage("noreply@termalvadi.com", this.email);
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Şifre Yenileme Maili | termalvadi.com";
            mailMessage.Body = "<b>Merhaba</b><br>Şifrenizi yenileme için <a href=\"termalvadi.com/ChangePassword?"+Function.Sifrele(this.id.ToString())+"\">buraya</a> tıklayabilirsiniz.";
            smtpClient.Send(mailMessage);
        }

    }
    public class ImageGallery
    {
        public List<Image> images;
        public ImageGallery(int id)
        {
            images = new List<Image>();
            DataTable dataTable = Sql.Table("select * from images where gid=?1", id.ToString());
            foreach (DataRow item in dataTable.Rows)
            {
                Image image = new Image();
                image.id = (int)item["id"];
                image.galeryId = (int)item["gid"];
                image.userId = (int)item["uid"];
                image.thumbUrl = (string)item["ThumbUrl"];
                image.originalUrl = (string)item["OriginalUrl"];
                image.title = (string)item["Title"];
                image.createdDate = (DateTime)item["CreatedDate"];
                images.Add(image);
            }
        }
    }
    public class Image
    {
        public enum ImageNullType
        {
            ProfileImage,
            NoneImage
        }
        public int id;
        public int galeryId;
        public int userId;
        public string thumbUrl;
        public string originalUrl;
        public string title;
        public DateTime createdDate;
        public Image() { }
        public static Image GetNullImage()
        {
            return new Image
            {
                originalUrl = @"images/noimage.png",
                thumbUrl = @"images/noimage.png",
                title = "Resim Yok"
            };
        }
        public Image(int id,ImageNullType imageNullType)
        {
            DataTable dataTable = Sql.Table("select * from images where id=?1",id.ToString());
            if (dataTable.Rows.Count == 0)
            {
                if (imageNullType == ImageNullType.NoneImage)
                {
                    originalUrl = @"images/noimage.png";
                    thumbUrl = @"images/noimage.png";
                    title = "Resim Yok";
                }
                else if (imageNullType == ImageNullType.ProfileImage)
                {
                    originalUrl = @"images/nulluser.jpg";
                    thumbUrl = @"images/nulluser.jpg";
                    title = "Resim Yok";
                }
            }
            else
            {
                this.id = (int)dataTable.Rows[0]["id"];
                this.galeryId = (int)dataTable.Rows[0]["gid"];
                this.userId = (int)dataTable.Rows[0]["uid"];
                //this.thumbUrl = (string)dataTable.Rows[0]["ThumbUrl"];
                //Şimdilik
                this.thumbUrl = (string)dataTable.Rows[0]["OriginalUrl"];
                //Şimdilik
                this.originalUrl = (string)dataTable.Rows[0]["OriginalUrl"];
                this.title = (string)dataTable.Rows[0]["Title"];
                this.createdDate = (DateTime)dataTable.Rows[0]["CreatedDate"];
            }
        }
    }
    public class HierarshyElement
    {
        public int id;
        public int parentid;
        public string title;
        public HierarshyElement() { }
        public HierarshyElement(int id)
        {
            DataTable dataTable = Sql.Table("select * from hierarchy where id=?1", id.ToString());
            this.id = id;
            parentid = (int)dataTable.Rows[0]["parentid"];
            title = (string)dataTable.Rows[0]["Title"];
        }
        public HierarshyElement GetParent {
            get
            {
                return new HierarshyElement(parentid);
            }
        }
        public List<HierarshyElement> GetCategories()
        {
            List<HierarshyElement> hierarshies = new List<HierarshyElement>();
            DataTable dataTable = Sql.Table("select * from hierarchy where parentid=?1", parentid.ToString());
            foreach (DataRow item in dataTable.Rows)
            {
                hierarshies.Add(
                    new HierarshyElement()
                    {
                        id = (int)item["id"],
                        parentid = (int)item["parentid"],
                        title = (string)item["Title"]
                    });
            }
            return hierarshies;

        }
        public static List<HierarshyElement> GetCategories(int parentid)
        {
            List<HierarshyElement> hierarshies = new List<HierarshyElement>();
            DataTable dataTable = Sql.Table("select * from hierarchy where parentid=?1", parentid.ToString());
            foreach (DataRow item in dataTable.Rows)
            {
                hierarshies.Add(
                    new HierarshyElement() {
                        id = (int)item["id"],
                        parentid = (int)item["parentid"],
                        title = (string)item["Title"]
                    });              
            }
            return hierarshies;
         
        }
        public static string GetCategoryList(NameValueCollection querystring,string id)
        {
            string returned = "";
            string topName = "";
            if (id == "0")
            {
                topName = "Kategoriler";
                returned = "<div class=\"card\" style=\"\">" +
                           "<div class=\"card-header\">" +
                             topName +
                           "</div>";
            }
            else
            {
                DataTable d = Sql.Table("select id,parentid,Title from hierarchy where id=?1", id);
                topName = d.Rows[0]["Title"].ToString();

                returned = "<div class=\"card\" style=\"\">" +
                           "<div style=\"cursor:pointer;\"  onclick=\"window.location.href = '" + Function.RebuildQueryString(querystring, "category", d.Rows[0]["parentid"].ToString()) + "'\" class=\"card-header\"> <i class=\"fa fa-arrow-left\"></i> " +
                             topName +
                           "</div>";
            }


            DataTable dataTable = Sql.Table("select id,parentid,Title from hierarchy where parentid=?1", id);
            if (dataTable.Rows.Count != 0)
            {
                if (dataTable.Rows.Count == 1)
                {
                    if(Sql.Cell("select count(id) from hierarchy where parentid=?1", dataTable.Rows[0]["id"].ToString()) == "0")
                    {
                        returned += "<ul class=\"list-group list-group-flush\">";
                        foreach (DataRow item in dataTable.Rows)
                        {
                            returned += "<li  class=\"list-group-item\" >" + item["Title"] + "<span style=\"float:right; font-size: 21px; font-weight: bold;\" ></span></li>";
                        }
                        returned += "</ul>";
                        returned += "</div>";
                    }
                    else
                    {
                        returned += "<ul class=\"list-group list-group-flush\">";
                        foreach (DataRow item in dataTable.Rows)
                        {
                            returned += "<li style=\"cursor:pointer;\" class=\"list-group-item\" onclick=\"window.location.href = '" + Function.RebuildQueryString(querystring, "category", item["id"].ToString()) + "'\">" + item["Title"] + "<span style=\"float:right; font-size: 21px; font-weight: bold;\" class=\"fa fa-angle-double-right\"></span></li>";
                        }
                        returned += "</ul>";
                        returned += "</div>";
                    }
                }
                else
                {
                    returned += "<ul class=\"list-group list-group-flush\">";
                    foreach (DataRow item in dataTable.Rows)
                    {
                        returned += "<li style=\"cursor:pointer;\" class=\"list-group-item\" onclick=\"window.location.href = '" + Function.RebuildQueryString(querystring, "category", item["id"].ToString()) + "'\">" + item["Title"] + "<span style=\"float:right; font-size: 21px; font-weight: bold;\" class=\"fa fa-angle-double-right\"></span></li>";
                    }
                    returned += "</ul>";
                    returned += "</div>";
                }
                //returned += "<ul class=\"list-group list-group-flush\">";
                //foreach (DataRow item in dataTable.Rows)
                //{
                //    returned += "<li style=\"cursor:pointer;\" class=\"list-group-item\" onclick=\"window.location.href = '"+Function.RebuildQueryString(querystring, "category", item["id"].ToString()) +"'\">" + item["Title"] + "<span style=\"float:right; font-size: 21px; font-weight: bold;\" class=\"fa fa-angle-double-right\"></span></li>";
                //}
                //returned += "</ul>";
                //returned += "</div>";

            }
            else
            {
                returned += "<ul class=\"list-group list-group-flush\">";
                returned += "<li style=\"cursor:pointer;\" class=\"list-group-item\">"+
                                  //"<div style=\"text-align: center;\" id=\"selectedCategoryOkay\">" +
                                  //"<i class=\" fa fa-check \" style=\"font-size: 10rem;color: #00ad17;\"></i>" +
                                  //"<br>" +
                                  //"<div>Kategori Seçimi Tamamlandı.</div>" +
                                  //"<script>SetSelectedCategory('" + id + "');</script>" +
                                  //"<script> $(\"#selectedCategoryOkay\").on(\"click\", function () {" +
                                  //"$(\"#smartwizard\").smartWizard(\"next\");" +
                                  //"return true; });</script>" +
                                  //"</div>" +
                             "</li>";
                returned += "</ul>";
                returned += "</div>";
            }

            return returned;
        }
    }
    public class Types
    {

    }   
}