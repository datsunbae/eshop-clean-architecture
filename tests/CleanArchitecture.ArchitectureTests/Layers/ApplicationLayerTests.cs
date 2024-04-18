using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.ArchitectureTests.Constants;
using FluentAssertions;
using FluentValidation;
using NetArchTest.Rules;

namespace CleanArchitecture.ArchitectureTests.Layers;

public class ApplicationLayerTests
{
    [Fact]
    public void Application_ShouldNotHaveDependencyOnInfrastructure()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;
        string[] layers =
        [
            Namespace.Infrastructure,
            Namespace.Presentation,
            Namespace.Identity
        ];

        // Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(layers)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandler_ShouldHaveNameEndingWithCommandHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandler_ShouldHaveNameEndingWith_QueryHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Acr
        TestResult result = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Validator_ShouldHaveNameEndingWithValidator()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(assembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
