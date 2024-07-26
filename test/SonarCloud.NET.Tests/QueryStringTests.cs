using SonarCloud.NET.Helpers;
using Xunit.Sdk;

namespace SonarCloud.NET.Tests;
public class QueryStringTests
{
    public class ToNameValueCollection
    {
        public class TestObject
        {
            public string Value { get; set; } = Guid.NewGuid().ToString();
            public string EmptyValue { get; set; } = string.Empty;
            public string? NullValue { get;set; }

            [QueryString("required")]
            public required string Required { get; set; }
            [QueryString("nullable")]
            public string? Nullable { get; set; }

        }

        [Fact]
        public void Should_add_only_properties_withvalues_marked_with_attribute()
        {

            var request = new TestObject() {
                Required = Guid.NewGuid().ToString()
            };
            var result = QueryString.ToNameValueCollection(request);
            result.Keys.Should().BeEquivalentTo(["required"]);
            result["required"].Should().Be(request.Required.ToString());
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("bla", "", "required")]
        [InlineData("bla", null, "required")]
        [InlineData("bla", "test", "required,nullable")]
        public void Should_add_only_properties_withvalues(
            string required, string? nullable, string keys)
        {
            var request = new TestObject()
            {
                Required = required,
                Nullable = nullable,
            };
            var result = QueryString.ToNameValueCollection(request);
            result.Keys.Should().BeEquivalentTo(keys.Split(',', StringSplitOptions.RemoveEmptyEntries));            
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("bla", "", "?required=bla")]
        [InlineData("bla", null, "?required=bla")]
        [InlineData("bla", "test", "?required=bla&nullable=test")]
        [InlineData("bla test", "test", "?required=bla+test&nullable=test")]
        public void Should_be_create_querystring(
            string required, string? nullable, string querystring)
        {
            var request = new TestObject()
            {
                Required = required,
                Nullable = nullable,
            };
            var query = QueryString.AsQueryString(request);
            query.Should().BeEquivalentTo(querystring);
        }
    }
}
