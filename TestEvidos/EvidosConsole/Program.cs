using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidosLibrary;
using EvidosLibrary.Helper;
using System.Text.RegularExpressions;
namespace EvidosConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UserSelection();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public static void UserSelection()
        {
            try
            {
                Console.WriteLine("Welcome to User Directory !!");
                Console.WriteLine("Please selct any of the options to continue : ");
                Console.WriteLine("1. Create user.");
                Console.WriteLine("2. Search an user by email address.");
                Console.WriteLine("3. Delete a user.");
                Console.WriteLine("4. Update a user's Email address.");
                Console.WriteLine("0. Exit this console.");
                int userInput = Convert.ToInt16(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        RegisterUser();
                        break;
                    case 2:
                        SearchUser();
                        break;
                    case 3:
                        DeleteUser();
                        break;
                    case 4:
                        UpdateEmailAddress();
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public static void RegisterUser()
        {
            try
            {
                Console.WriteLine("Enter email address of the user.");
                UserClass objUser = new UserClass();
                objUser.EmailAddress = Console.ReadLine();
                ValidateEmailAddress(objUser.EmailAddress);
                Console.WriteLine("Enter password for the account.");
                objUser.Password = Console.ReadLine();
                Console.WriteLine("Enter registration date for the account.");
                objUser.RegiteredDate = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("User verification done. 'Y' for Yes and 'N' for No !");
                string verificationResult = Console.ReadLine();
                objUser.IsVerified = (verificationResult == "Y" || verificationResult == "y") ? true : false;

                bool? response = ControllerClass.RegisterUser(objUser);
                if (response != null && response.Value)
                    Console.WriteLine("Email Registration successfull. An email has been sent to your email address for verification.");
                else if (response != null && !response.Value)
                    Console.WriteLine("A user account with same email address already exists. Please try again !!");
                else
                    Console.WriteLine("An error occured. Please try again !!");

                Console.WriteLine("Please select '1' to go to Main menu, '0' to exit");
                int userEntry = Convert.ToInt16(Console.ReadLine());
                if (userEntry == 1)
                    UserSelection();
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public static void SearchUser()
        {
            try
            {
                Console.WriteLine("Enter email address of the user to search.");
                string emailAddress = Console.ReadLine();
                var result = ControllerClass.SearchUser(emailAddress);
                if (result != null && result.Count > 0)
                {
                    Console.WriteLine("(" + result.Count + ")" + " Data found !!");
                    int count = 1;
                    foreach (var item in result)
                    {
                        Console.WriteLine("Data No : " + count + " !!");
                        Console.WriteLine("Email Address : " + item.EmailAddress);
                        Console.WriteLine("Registered On : " + item.RegiteredDate);
                        Console.WriteLine("Verication status : " + ((item.IsVerified) ? "Verified" : "Un-Verified"));
                    }
                }
                else
                {
                    Console.WriteLine("No data found for the searched email.");
                }
                Console.WriteLine("Please select '1' to go to Main menu, '0' to exit");
                int userEntry = Convert.ToInt16(Console.ReadLine());
                if (userEntry == 1)
                    UserSelection();
            }
            catch (Exception ex)
            {
            }
        }

        public static void DeleteUser()
        {
            try
            {
                Console.WriteLine("Enter email address of the user to delete.");
                string deleteEmailAddress = Console.ReadLine();
                var deleteResult = ControllerClass.DeleteUser(deleteEmailAddress);

                if (deleteResult != null && deleteResult.Value)
                    Console.WriteLine("User was deleted successfully");
                else if (deleteResult != null && !deleteResult.Value)
                    Console.WriteLine("No data found Or User deletion failed, since accounts with registration date more than 30 days from current date can be deleted. Please try again !!");
                else
                    Console.WriteLine("An error occured. Please try again !!");

                Console.WriteLine("Please select '1' to go to Main menu, '0' to exit");
                int userEntry = Convert.ToInt16(Console.ReadLine());
                if (userEntry == 1)
                    UserSelection();
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public static void UpdateEmailAddress()
        {
            try
            {
                Console.WriteLine("Enter current email address of the user to update.");
                string oldEmailAddress = Console.ReadLine();
                Console.WriteLine("Enter new email address of the user to update with.");
                string newEmailAddress = Console.ReadLine();

                var updateResult = ControllerClass.UpdateEmailAddress(oldEmailAddress, newEmailAddress);
                if (updateResult != null && updateResult.Value)
                    Console.WriteLine("Email address updaed successfully");
                else if (updateResult != null && !updateResult.Value)
                    Console.WriteLine("User couldnot be found Or current email address is not yet verified Or email address is being used by other user. Please try again !!");
                else
                    Console.WriteLine("An error occured. Please try again !!");

                Console.WriteLine("Please select '1' to go to Main menu, '0' to exit");
                int userEntry = Convert.ToInt16(Console.ReadLine());
                if (userEntry == 1)
                    UserSelection();
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public static bool ValidateEmailAddress(string emailAddress)
        {
            try
            {
                bool emailAddressValidation = Regex.IsMatch(emailAddress,
                     @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                     RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

                if (!emailAddressValidation)
                {
                    Console.WriteLine("Entered string is not in correct format. Please try again !!");
                    Console.WriteLine("Please select '1' to go to Main menu, '0' to exit");
                    int userEntry = Convert.ToInt16(Console.ReadLine());
                    if (userEntry == 1)
                        UserSelection();
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {           
                ErrorLogger.LogError(ex);
                return false;
            }
        }

    }
}

