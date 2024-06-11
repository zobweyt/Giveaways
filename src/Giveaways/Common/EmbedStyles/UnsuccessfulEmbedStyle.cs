using Discord;

namespace Giveaways;

/// <summary>
/// Represents the style of an unsuccessful embed.
/// </summary>
public class UnsuccessfulEmbedStyle : EmbedStyle
{
    public override string Name => "Woops!";
    public override string IconUrl => Icons.Cross;
    public override Color Color => Colors.Danger;
}
