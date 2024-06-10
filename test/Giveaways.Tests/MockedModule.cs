using System;
using System.Threading.Tasks;
using Discord.Interactions;

namespace Giveaways.Tests;

/// <summary>
/// Represents a module for unit testing.
/// </summary>
internal sealed class MockedModule : ModuleBase
{
    [ComponentInteraction("interaction:*:*")]
    public Task InteractionAsync(string arg1, string arg2) => throw new NotImplementedException();
}
