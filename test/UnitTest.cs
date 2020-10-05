using System;
using Xunit;
using System.Text.RegularExpressions;

namespace SecretHubTest
{
    public class TestSuite
    {
        [Fact]
        public void TestReadSuccess()
        {
            var client = new SecretHub.Client();
            SecretHub.SecretVersion secret = client.Read("secrethub/xgo/dotnet/test/test-secret:3");
            Assert.Equal(new Guid("529beaaf-9934-432f-a6b0-c5cb7e847458"), secret.SecretVersionID);
            Assert.Equal(new Guid("c37ec233-e168-436d-8b06-48c52aa22d5e"), secret.Secret.SecretID);
            Assert.Equal(3, secret.Version);
            Assert.Equal("super_secret_value", secret.Data);
            Assert.Equal(DateTime.Parse("10/1/2020 2:22:08 PM",
                          System.Globalization.CultureInfo.InvariantCulture), secret.CreatedAt);
            Assert.Equal(DateTime.Parse("10/1/2020 10:49:33 AM",
                          System.Globalization.CultureInfo.InvariantCulture), secret.Secret.CreatedAt);
            Assert.Equal("ok", secret.Status);
        }

        [Fact]
        public void TestReadFail()
        {
            var client = new SecretHub.Client();
            Regex expectedErrorRegex = new Regex(@"^.*\(server\.secret_not_found\) $");
            var ex = Assert.Throws<ApplicationException>(() => client.Read("secrethub/xgo/dotnet/test/not-this-one"));
            Assert.True(expectedErrorRegex.IsMatch(ex.Message), "error should end in the (server.secret_not_found) error code");
        }

        [Fact]
        public void TestReadStringSuccess()
        {
            var client = new SecretHub.Client();
            string secret = client.ReadString("secrethub/xgo/dotnet/test/test-secret");
            Assert.Equal("super_secret_value", secret);
        }

        [Fact]
        public void TestReadStringFail()
        {
            var client = new SecretHub.Client();
            Regex expectedErrorRegex = new Regex(@"^.*\(server\.secret_not_found\) $");
            var ex = Assert.Throws<ApplicationException>(() => client.ReadString("secrethub/xgo/dotnet/test/not-this-one"));
            Assert.True(expectedErrorRegex.IsMatch(ex.Message), "error should end in the (server.secret_not_found) error code");
        }

        [Fact]
        public void TestResolveSuccess() {
            var client = new SecretHub.Client();
            Assert.Equal("super_secret_value", client.Resolve("secrethub://secrethub/xgo/dotnet/test/test-secret"));
        }

        [Theory]
        [InlineData("super_secret_value", "TEST")]
        [InlineData("aaaa", "OTHER_TEST")]
        [InlineData("this=has=three=equals", "TEST_MORE_EQUALS")]
        public void TestResolveEnvSuccess(string secretValue, string envVarName) {
            var client = new SecretHub.Client();
            System.Collections.Generic.IDictionary<string,string> res = client.ResolveEnv();
            Assert.Equal(secretValue, res[envVarName]);
        }

        [Theory]
        [InlineData("secrethub/xgo/dotnet/test/test-secret", true)]
        [InlineData("secrethub/xgo/dotnet/test/not-this-one", false)]
        public void TestExists(string path, bool expectedTestResult) {
            var client = new SecretHub.Client();
            Assert.Equal(expectedTestResult, client.Exists(path));
        }

        [Fact]
        public void TestExistsException()
        {
            var client = new SecretHub.Client();
            Regex expectedErrorRegex = new Regex(@"^.*\(api\.invalid_secret_path\) $");
            var ex = Assert.Throws<ApplicationException>(() => client.Exists("not-a-path"));
            Assert.True(expectedErrorRegex.IsMatch(ex.Message), "error should end in the (api.invalid_secret_path) error code");
        }

        [Fact]
        public void TestWriteSuccess() {
            var client = new SecretHub.Client();
            client.Write("secrethub/xgo/dotnet/test/new-secret", "new_secret_value");
            String secret = client.ReadString("secrethub/xgo/dotnet/test/new-secret");
            Assert.Equal("new_secret_value", secret);
            client.Remove("secrethub/xgo/dotnet/test/new-secret");
        }

        [Fact]
        public void TestRemoveSuccess() {
            var client = new SecretHub.Client();
            client.Write("secrethub/xgo/dotnet/test/delete-secret", "delete_secret_value");
            Assert.True(client.Exists("secrethub/xgo/dotnet/test/delete-secret"));
            client.Remove("secrethub/xgo/dotnet/test/delete-secret");
            Assert.False(client.Exists("secrethub/xgo/dotnet/test/delete-secret"));
        }

        [Fact]
        public void TestRemoveFail() {
            var client = new SecretHub.Client();
            Regex expectedErrorRegex = new Regex(@"^.*\(server\.secret_not_found\) $");
            var ex = Assert.Throws<ApplicationException>(() => client.Remove("secrethub/xgo/dotnet/test/not-this-one"));
            Assert.True(expectedErrorRegex.IsMatch(ex.Message), "error should end in the (server.secret_not_found) error code");
        }
    }
}
