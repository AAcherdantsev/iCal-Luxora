using System.Collections.Frozen;
using iCal.Luxora.Models.Abstractions;
using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Models.Parameters;

/// <summary>
///     Represents an effect.
/// </summary>
public class Effect
{
    /// <summary>
    ///     Unique identifier for this effect.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    ///     Name of this effect.
    /// </summary>
    public required string Name { get; set; } // Should be dictionary for localization?

    /// <summary>
    ///     Whether this effect is a favorite effect or not.
    /// </summary>
    public bool IsFavorite { get; set; } = false;

    /// <summary>
    ///     Features supported by this effect.
    /// </summary>
    public IReadOnlySet<EffectFeature> EffectFeatures { get; set; } = new HashSet<EffectFeature>().ToFrozenSet();

    /// <summary>
    ///     Parameter settings supported by this effect.
    /// </summary>
    public IReadOnlySet<IParameterSettings> ParameterSettings { get; set; } =
        new HashSet<IParameterSettings>().ToFrozenSet();
}