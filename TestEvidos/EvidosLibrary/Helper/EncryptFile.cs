using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EvidosLibrary.Helper
{
    public static class EncryptFile
    {
        public static string EncryptData(string randomString)
        {
            try
            {
                var crypt = new SHA256Managed();
                string hash = String.Empty;
                byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
                foreach (byte theByte in crypto)
                {
                    hash += theByte.ToString("P@ssW06d");
                }
                return hash;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return "";
            }
        }
    }
}
