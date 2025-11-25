using iCal.Luxora.Models.Abstractions;
using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Models.Parameters;

/// <summary>
/// Represents a generic parameter setting with support for type-specific default value,
/// current value, minimum, and maximum value definitions. It implements the
/// <see cref="IParameterSettings"/> interface and provides typed access
/// to parameter configuration properties.
/// </summary>
/// <typeparam name="T">
/// The type of values this parameter setting supports, defining the type of
/// the default value, current value, minimum value, and maximum value.
/// </typeparam>
public class ParameterSettings<T> : IParameterSettings
{
    /// <inheritdoc/>
    public required int Id { get; set; }
    
    /// <inheritdoc/>
    public required string Name { get; set; } // Should be dictionary for localization? 
    
    /// <inheritdoc/>
    public required ParameterType Type { get; set; }

    /// <summary>
    /// Default value for this parameter setting.
    /// </summary>
    public required T DefaultValue { get; set; }
    
    /// <summary>
    /// Current value for this parameter setting.
    /// </summary>
    public required T CurrentValue { get; set; }
    
    /// <summary>
    /// Minimum value for this parameter setting.
    /// </summary>
    public T? MinValue { get; set; }
    
    /// <summary>
    /// Maximum value for this parameter setting.
    /// </summary>
    public T? MaxValue { get; set; }
    
    /// <inheritdoc/>
    object? IParameterSettings.DefaultValueObj => DefaultValue;
    
    /// <inheritdoc/>
    object? IParameterSettings.CurrentValueObj
    {
        get => CurrentValue;
        set => CurrentValue = (T)value!;
    }
    
    /// <inheritdoc/>
    object? IParameterSettings.MinValueObj => MinValue;
    
    /// <inheritdoc/>
    object? IParameterSettings.MaxValueObj => MaxValue;

    /// <inheritdoc/>
    public Type ValueType => typeof(T);
}