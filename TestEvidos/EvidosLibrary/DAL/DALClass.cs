using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EvidosLibrary.Helper;
using System.IO;

namespace EvidosLibrary
{
    public class DALClass
    {
        public bool? Login(string emailAddress, string encryptedPassword)
        {
            try
            {
                bool registeredUser = false;
                var users = JsonConvert.DeserializeObject<ParentClass>(File.ReadAllText(@"DataFile.json"));// Reading it from a file from local, can change this to reading from NoSQL in network
                registeredUser = users.Data.Any(m => m.EmailAddress == emailAddress && m.EncryptedPassword == encryptedPassword);
                return registeredUser;
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
                var objUsers = new List<UserClass>();
                var users = JsonConvert.DeserializeObject<ParentClass>(File.ReadAllText(@"DataFile.json"));// Reading it from a file from local, can change this to reading from NoSQL in network
                objUsers = users.Data.Where(m => emailAddress.Contains(m.EmailAddress)).ToList();
                return objUsers;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return new List<UserClass>();
            }
        }

        public bool? RegisterUser(UserClass objUser)
        {
            try
            {
                var users = JsonConvert.DeserializeObject<ParentClass>(File.ReadAllText(@"DataFile.json"));// Reading it from a file from local, can change this to reading from NoSQL in network
                if (!users.Data.Select(m => m.EmailAddress).Contains(objUser.EmailAddress))
                {
                    users.Data.Add(objUser);
                    var convertedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                    File.WriteAllText(@"DataFile.json", convertedJson);
                    return true;
                }
                else
                {
                    return false;
                }
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
                var users = JsonConvert.DeserializeObject<ParentClass>(File.ReadAllText(@"DataFile.json"));// Reading it from a file from local, can change this to reading from NoSQL in network
                var userData = users.Data.FirstOrDefault(m => m.EmailAddress == oldEmailAddress);
                if (userData != null && userData.IsVerified)
                {
                    if (!users.Data.Any(m => m.EmailAddress == newEmailAddress))
                    {
                        userData.EmailAddress = newEmailAddress;
                        var convertedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        File.WriteAllText(@"DataFile.json", convertedJson);
                        return true;
                    }
                    else return false;
                }
                else
                {
                    return false;
                }
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
                var users = JsonConvert.DeserializeObject<ParentClass>(File.ReadAllText(@"DataFile.json"));// Reading it from a file from local, can change this to reading from NoSQL in network
                var userData = users.Data.FirstOrDefault(m => m.EmailAddress == emailAddress && m.RegiteredDate.AddDays(30) < DateTime.Now);
                if (userData != null)
                {
                    users.Data.Remove(userData);
                    var convertedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                    File.WriteAllText(@"DataFile.json", convertedJson);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return null;
            }
        }

    }
}
