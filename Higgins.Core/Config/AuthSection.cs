using System.Collections.Generic;
using System.Runtime.Serialization;
using Higgins.Core.Security;

namespace Higgins.Core.Config
{
    [DataContract]
    public class AuthSection
    {
        [DataMember(Name = "enable")]
        public bool Enable
        {
            get;
            set;
        }

        [DataMember(Name = "users")]
        public Dictionary<string, string> Users
        {
            get;
            set;
        }

        [DataMember(Name = "groups")]
        public Dictionary<string, IEnumerable<string>> Groups
        {
            get;
            set;
        }

        public bool VerifyPassword(string username, string password)
        {
            if (Users == null || !Users.ContainsKey(username))
            {
                return false;
            }

            var hash = Users[username];

            if (!string.IsNullOrWhiteSpace(hash))
            {
                return hash.StartsWith("plaintext:") 
                    ? CheckPlainText(hash, password) 
                    : CheckHashed(hash, password);
            }

            return false;
        }

        private static bool CheckPlainText(string hash, string password)
        {
            var arr = hash.Split(':');
            if (arr.Length != 2)
            {
                return arr[1] == password;
            }

            return false;
        }

        private static bool CheckHashed(string hash, string password)
        {
            return PasswordHash.ValidatePassword(password, hash);
        }
    }
}
