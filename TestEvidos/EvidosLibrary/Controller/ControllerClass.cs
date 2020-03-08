using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidosLibrary.Helper;

namespace EvidosLibrary
{
    public static class ControllerClass
    {
        private static LogicClass objLogicClass;
        static ControllerClass()
        {
            objLogicClass = new LogicClass();
        }

        /// <summary>
        /// Method to create a user.
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public static bool? RegisterUser(UserClass objUser)
        {
            try
            {
                return objLogicClass.RegisterUser(objUser);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Method to Search for an user
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static List<UserClass> SearchUser(string emailAddress)
        {
            try
            {
                return objLogicClass.SearchUser(emailAddress);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return new List<UserClass>();
            }
        }

        /// <summary>
        /// Method to authenticate user.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool? Login(string emailAddress, string password)
        {
            try
            {
                return objLogicClass.Login(emailAddress, password);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }

        public static bool? UpdateEmailAddress(string oldEmailAddress, string newEmailAddress)
        {
            try
            {
                return objLogicClass.UpdateEmailAddress(oldEmailAddress, newEmailAddress);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool? DeleteUser(string emailAddress)
        {
            try
            {
                return objLogicClass.DeleteUser(emailAddress);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }
    }
}
