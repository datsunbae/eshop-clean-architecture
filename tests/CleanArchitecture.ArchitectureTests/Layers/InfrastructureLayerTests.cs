using CleanArchitecture.ArchitectureTests.Constants;
using FluentAssertions;
using NetArchTest.Rules;

namespace CleanArchitecture.ArchitectureTests.Layers;

public class InfrastructureLayerTests
{
    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOnSomeLayer()
    {
        // Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;
        string[] layers =
        [
            Namespace.Presentation,
            Namespace.Persistence,
            Namespace.Infrastructure
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
}
