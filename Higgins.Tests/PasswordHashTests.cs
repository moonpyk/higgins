using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgins.Core.Lib;
using Xunit;

namespace Higgins.Tests
{
    public class PasswordHashTests
    {
        [Fact]
        public void CreateHashTest()
        {
            var hash = PasswordHash.CreateHash("password");
            var reHash = PasswordHash.CreateHash("password");

            Console.WriteLine(hash);
            Console.WriteLine(reHash);
            //
            Assert.NotEqual(hash, reHash);
        }

        [Fact]
        public void ValidatePasswordTest()
        {
            Assert.True(PasswordHash.ValidatePassword(
                "password",
                "WkFyUGJqSmFUWHFnZzZ4TklETU1odW5lUjJRT1FvTkQzcFZmM1A5QXIwST06MjcxMDptZHhJVDdtYmRGZ3R3bGw0RC9CaTdZWW45Z3BhTzNIckQ0bjYyRmFyOFpJPQ=="
            ));

            Assert.False(PasswordHash.ValidatePassword(
                "password",
                "MTAwMDA6OVJsdfslRzlIekZxdm9xqsdsdnk4cjRtUkJvSndzSjQ6M2sdfsdf3BaamlzbFM3QWpUcS8xUzFVRGpjRVFDOEkz"
            ));
        }
    }
}
