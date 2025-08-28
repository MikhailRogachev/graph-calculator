using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace UnitTests.Utility.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
internal class AutoMockAttribute : AutoDataAttribute
{
    public AutoMockAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}

