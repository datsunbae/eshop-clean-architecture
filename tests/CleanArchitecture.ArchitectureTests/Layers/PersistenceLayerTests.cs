using CleanArchitecture.ArchitectureTests.Constants;
using FluentAssertions;
using NetArchTest.Rules;

namespace CleanArchitecture.ArchitectureTests.Layers;

public class PersistenceLayerTests
{
    [Fact]
    public void PersistenceLayer_ShouldNotHaveDependencyOnSomeLayer()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;
        string[] layers =
        [
            Namespace.Infrastructure,
            Namespace.Identity,
            Namespace.Presentation,
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
