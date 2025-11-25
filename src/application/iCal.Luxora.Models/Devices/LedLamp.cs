using System.Drawing;
using iCal.Luxora.Models.Enums;
using iCal.Luxora.Models.Parameters;

namespace iCal.Luxora.Models.Devices;

/// <summary>
/// Represents a LED lamp.
/// </summary>
public class LedLamp
{
    /// <summary>
    /// Unique identifier for this LED lamp.
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Firmware version of this LED lamp.
    /// </summary>
    public required Version FirmwareVersion { get; set; }
    
    /// <summary>
    /// Whether this LED lamp is enabled or not.
    /// </summary>
    public required bool IsEnabled { get; set; }
    
    /// <summary>
    /// Name of this LED lamp.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Specifies the size of led matrix.
    /// </summary>
    public required Size Size { get; set; }
    
    /// <summary>
    /// Features supported by this LED lamp.
    /// </summary>
    public IReadOnlySet<Features> Features { get; set; }
    
    /// <summary>
    /// Effects supported by this LED lamp.
    /// </summary>
    public IReadOnlySet<Effect> Effects { get; set; }
    
    /// <summary>
    /// Currently active effect.
    /// </summary>
    public int CurrentEffectId { get; set; }
}