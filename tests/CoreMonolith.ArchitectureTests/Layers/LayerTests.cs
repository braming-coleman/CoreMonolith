using FluentAssertions;
using NetArchTest.Rules;

namespace CoreMonolith.ArchitectureTests.Layers;

public class LayerTests : BaseTest
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOnApplication()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn("Application")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(WebAppAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(WebAppAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(WebAppAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
