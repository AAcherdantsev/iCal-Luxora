using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Application.UI.Common.Devices;

public partial class SavedDeviceCard : ContentView
{
    public List<Feature> Features { get; set; } = new List<Feature>()
    {
        Feature.Bluetooth,
        Feature.WiFi,
        Feature.Music,
        
        Feature.Games,
        Feature.SdCard,
        Feature.Microphone,
        
        Feature.Paint,
        Feature.Battery,
        Feature.AutoBrightness,
    };
    
    public SavedDeviceCard()
    {
        InitializeComponent();
    }
}