using CleanArchitecture.ArchitectureTests.Constants;
using FluentAssertions;
using NetArchTest.Rules;

namespace CleanArchitecture.ArchitectureTests.Layers;

public sealed class DomainLayerTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOnAnyLayer()
    {
        // Arrange
        var assembly = Domain.AssemblyReference.Assembly;
        string[] layers =
        [
            //Namespace.Application,
            Namespace.Infrastructure,
            Namespace.Presentation,
            Namespace.Persistence
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
