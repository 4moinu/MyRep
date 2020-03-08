using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EvidosLibrary.Helper
{
    public class UserClass
    {
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("encryptedPassword")]
        public string EncryptedPassword { get; set; }

        [JsonProperty("registeredDate")]
        public DateTime RegiteredDate { get; set; }

        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }

    public class ParentClass
    {
        [JsonProperty("data")]
        public List<UserClass> Data { get; set; }
    }
}
