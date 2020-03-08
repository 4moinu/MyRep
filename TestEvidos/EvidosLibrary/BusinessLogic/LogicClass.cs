using EvidosLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidosLibrary
{
    public class LogicClass
    {
        private static DALClass objDALClass;
        static LogicClass()
        {
            objDALClass = new DALClass();
        }


        public bool? RegisterUser(UserClass objUser)
        {
            try
            {
                string encryptedPassword = EncryptFile.EncryptData(objUser.Password);
                objUser.EncryptedPassword = encryptedPassword;
                return objDALClass.RegisterUser(objUser);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }

        public List<UserClass> SearchUser(string emailAddress)
        {
            try
            {
                return objDALClass.SearchUser(emailAddress);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return new List<UserClass>();
            }
        }

        public bool? Login(string emailAddress, string password)
        {
            try
            {
                string encryptedPassword = EncryptFile.EncryptData(password);
                return objDALClass.Login(emailAddress, encryptedPassword);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }

        public bool? UpdateEmailAddress(string oldEmailAddress, string newEmailAddress)
        {
            try
            {
                return objDALClass.UpdateEmailAddress(oldEmailAddress, newEmailAddress);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }

        public bool? DeleteUser(string emailAddress)
        {
            try
            {
                return objDALClass.DeleteUser(emailAddress);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }
    }
}
