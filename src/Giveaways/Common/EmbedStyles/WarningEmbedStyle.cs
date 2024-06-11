using Discord;

namespace Giveaways;

/// <summary>
/// Represents the style of a warning embed.
/// </summary>
public class WarningEmbedStyle : EmbedStyle
{
    public override string Name => "Caution!";
    public override string IconUrl => Icons.Exclamation;
    public override Color Color => Colors.Warning;
}
