using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineShopping.Controllers
{
    public class OTPController : ApiController
    {
        private DbonlineshoppingEntities db = new DbonlineshoppingEntities();
        public async Task<int> GetOtp(string email)
        {
            var user = db.UserTables.Where(w => w.Email == email).FirstOrDefault();
            if (user != null)
            {
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                OTP otp = new OTP();
                otp.UserID = user.UserID;
                otp.OTP1 = r;
                db.OTPs.Add(otp);

                SmtpClient smtp = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("oeasyshopping@gmail.com");
                mailMessage.To.Add(email);
                mailMessage.Subject = "Forgot Password";
                mailMessage.Body = "Dear Customer...Your OTP is " + r;
                await Task.Run(() => smtp.SendAsync(mailMessage, null));
                return r;
            }
            else
                return 0;
            
        }

        [HttpGet]
        public async Task ResendOtp(string email, int otp)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("dotnettraining2011@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "Forgot Password";
            mailMessage.Body = "Dear User,Your OTP is " + otp;
            await Task.Run(() => smtp.SendAsync(mailMessage, null));
        }
    }
}

