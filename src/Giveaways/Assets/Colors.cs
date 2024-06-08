using Discord;

namespace Giveaways;

/// <summary>
/// Represents a constant set of predefined <see cref="Color"/> values.
/// </summary>
public static class Colors
{
    /// <summary>
    /// The color used to indicate an informative state.
    /// </summary>
    public static readonly Color Primary = new(88, 101, 242);

    /// <summary>
    /// The color used to indicate an informative state.
    /// </summary>
    public static readonly Color Secondary = new(77, 80, 87);

    /// <summary>
    /// The color used to depict an emotion of positivity.
    /// </summary>
    public static readonly Color Success = new(119, 178, 85);

    /// <summary>
    /// The color used to indicate caution.
    /// </summary>
    public static readonly Color Warning = new(255, 204, 77);

    /// <summary>
    /// The color used to depict an emotion of negativity.
    /// </summary>
    public static readonly Color Danger = new(221, 46, 68);

    /// <summary>
    /// The color used to attract attention.
    /// </summary>
    public static readonly Color Fuchsia = new(235, 69, 158);
}
