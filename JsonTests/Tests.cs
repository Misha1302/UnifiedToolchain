using Jsons;

namespace JsonTests;

public class Tests
{
    private readonly dynamic _complexJson =
        new Json(""" { "v1": true, "v2": "some string", "v3": [ { "v4": 123 } ] } """);

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var j = (dynamic)new Json(_complexJson);
        j.v1 = !(bool)j.v1;
        Assert.AreNotEqual(j, _complexJson);
    }

    [Test]
    public void Test2()
    {
        var j = (dynamic)new Json(_complexJson);

        j.v1 = !(bool)j.v1;
        j.newField = "new value";
        j.v3[0].v4 = (int)j.v3[0].v4 + 123;

        var left = (string)j.ToString();
        var right = """{"v1":false,"v2":"some string","v3":[{"v4":246}],"newField":"new value"}""";
        Assert.That(left.Equals(right, StringComparison.InvariantCultureIgnoreCase), Is.True);
    }
}