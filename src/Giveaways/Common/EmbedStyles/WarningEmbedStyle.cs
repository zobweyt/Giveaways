using Discord;

namespace Giveaways;

/// <summary>
/// Represents the style of a warning embed.
/// </summary>
public class WarningEmbedStyle : EmbedStyle
{
    /// <inheritdoc/>
    public override string Name => "Caution!";

    /// <inheritdoc/>
    public override string IconUrl => Icons.Exclamation;

    /// <inheritdoc/>
    public override Color Color => Colors.Warning;
}
