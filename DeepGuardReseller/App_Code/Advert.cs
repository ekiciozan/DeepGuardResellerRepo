using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

using System.Web.UI;


namespace DataModel
{

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
            profileImage = new Image((int)dataTable.Rows[0]["ProfileImageId"], Image.ImageNullType.ProfileImage);
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
            mailMessage.Body = "<b>Merhaba</b><br>Şifrenizi yenileme için <a href=\"termalvadi.com/ChangePassword?" + Function.Sifrele(this.id.ToString()) + "\">buraya</a> tıklayabilirsiniz.";
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
        public Image(int id, ImageNullType imageNullType)
        {
            DataTable dataTable = Sql.Table("select * from images where id=?1", id.ToString());
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

}