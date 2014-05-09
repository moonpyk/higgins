using System.Collections.Generic;
using Higgins.Core.Config;
using Higgins.Core.Lib;
using Xunit;

namespace Higgins.Tests.Lib
{
    public class DotObjectNotationHelperTests
    {
        [Fact]
        public void ParseTest()
        {
            var parsed = DotObjectNotationHelper.Parse("test.plop.moonpyk.zgrunt");

            Assert.Null(parsed.Value);
            Assert.Equal(parsed.PathElements, new[] { "test", "plop", "moonpyk", "zgrunt" });

            var parsedTwo = DotObjectNotationHelper.Parse("test.plop.Hello.World.Coucou=Hello World");

            Assert.Equal(parsedTwo.Value, "Hello World");
            Assert.Equal(parsedTwo.PathElements, new[] { "test", "plop", "Hello", "World", "Coucou" });

        }

        [Fact]
        public void ApplyTest()
        {
            var myComplexObj = new Configuration()
            {
                Auth = new AuthSection { Enable = false },
                Higgins = new HigginsSection(),
                Projects = new Dictionary<string, Project>()
            };

            {
                var parsed = DotObjectNotationHelper.Parse("Auth.Enable=true");
                Assert.True(DotObjectNotationHelper.Apply(parsed, myComplexObj));
                Assert.True(myComplexObj.Auth.Enable);
            }

            myComplexObj.Auth.Enable = false;
            Assert.False(myComplexObj.Auth.Enable);

            {
                var parsed = DotObjectNotationHelper.Parse("auth.enable=true");
                Assert.True(DotObjectNotationHelper.Apply(parsed, myComplexObj));
                Assert.True(myComplexObj.Auth.Enable);
            }
        }
    }
}
