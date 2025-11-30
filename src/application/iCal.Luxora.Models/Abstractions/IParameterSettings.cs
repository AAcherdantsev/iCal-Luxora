using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Models.Abstractions;

/// <summary>
///     Represents a generic parameter setting.
/// </summary>
public interface IParameterSettings
{
    /// <summary>
    ///     Unique identifier for this parameter setting.
    /// </summary>
    int Id { get; }

    /// <summary>
    ///     Localized name of this parameter setting.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Type of this parameter setting.
    /// </summary>
    ParameterType Type { get; }

    /// <summary>
    ///     Default value for this parameter setting.
    /// </summary>
    object? DefaultValueObj { get; }

    /// <summary>
    ///     Current value for this parameter setting.
    /// </summary>
    object? CurrentValueObj { get; set; }

    /// <summary>
    ///     Minimum value for this parameter setting.
    /// </summary>
    object? MinValueObj { get; }

    /// <summary>
    ///     Maximum value for this parameter setting.
    /// </summary>
    object? MaxValueObj { get; }

    /// <summary>
    ///     Type of values this parameter setting supports.
    /// </summary>
    Type ValueType { get; }
}