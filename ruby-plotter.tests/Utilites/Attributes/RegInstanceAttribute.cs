using AutoFixture;
using AutoFixture.Xunit2;
using System.Reflection;
using UnitTests.Utility.Extensions;
using UnitTests.Utility.Kernel;

namespace UnitTests.Utility.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class RegInstanceAttribute : CustomizeAttribute
{
    private readonly string _methodName;
    private readonly object[]? _methodParameters;

    public RegInstanceAttribute(string methodName)
    {
        ArgumentNullException.ThrowIfNull(methodName, nameof(methodName));
        _methodName = methodName;
    }

    public RegInstanceAttribute(string methodInfo, object[]? values)
    {
        _methodName = methodInfo;
        _methodParameters = values;
    }

    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
        var cls = parameter.Member.DeclaringType!;
        var types = AccessorExtensions.GetParametersType(_methodParameters);
        var methodInfo = cls.GetMethodInfo(_methodName, types)!;

        return new RegInstance(methodInfo, _methodParameters, methodInfo.ReturnType);
    }
}
