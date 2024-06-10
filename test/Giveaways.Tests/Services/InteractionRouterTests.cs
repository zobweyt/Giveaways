using Bogus;
using Discord.Interactions;
using Giveaways.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Giveaways.Tests;

/// <summary>
/// Provides unit test cases for <see cref="InteractionRouter"/>.
/// </summary>
public class InteractionRouterTests : TestsBase
{
    [Fact]
    public void InteractionRouter_Binds_CustomID()
    {
        var optionsMock = new Mock<IOptions<InteractionServiceConfig>>();
        optionsMock.Setup(x => x.Value).Returns(new InteractionServiceConfig());

        var moduleMock = new MockedModule();
        var func = moduleMock.InteractionAsync;

        var arg1 = Faker.Lorem.Word();
        var arg2 = Faker.Lorem.Word();

        var routerMock = new Mock<InteractionRouter>(optionsMock.Object);
        var id = routerMock.Object.Bind(func, arg1, arg2);

        var prefix = InteractionRouter.GetCustomIdPrefix(func);

        Assert.Equal($"{prefix}:{arg1}:{arg2}", id);
    }
}
