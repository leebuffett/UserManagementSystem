using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace Assignment.Models
{
 
    static public class DBManage
    {
        private static Database1Entities2 db = new Database1Entities2();
        public static string GetLoginInfo(string username, string password, string sessionId)
        {
            var s = db.GetUserInfo(username, CalculateSHA256(password),sessionId);
            var item = s.FirstOrDefault();

            if (item == "Success")
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return item;
        }

        public static void CreateUser(User user)
        {
            db.CreateUser(user.Username,user.EmployeeId,user.Email, user.FullName, CalculateSHA256(user.Password),user.JoinDate,user.PositionId,user.TeamId,user.Security,user.StatusId);
        }

        public static void EditUser(User user)
        {
            db.EditUser(user.Username, user.Email, user.FullName, CalculateSHA256(user.Password), user.JoinDate, user.PositionId, user.TeamId, user.Security, user.StatusId);
        }

        public static void DeleteUser(User user)
        {
            db.DeleteUser(user.Id);
        }

        public static void Logout(string username)
        {
            db.Logout(username);
        }

        public static bool CompareSession(string username, string sessionId)
        {
            var IsLogIn = db.Users.Where(x => x.Username == username && x.LoginStatus == "true" && x.SessionId != sessionId).AsEnumerable();

            if (IsLogIn.Any())
            {
                FormsAuthentication.SignOut();
                return false;
            }
            else
            {
                return true;
            }
        }

        private static string CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                builder.Append(hashValue[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}