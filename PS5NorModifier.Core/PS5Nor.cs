
using System.Text.Json;

namespace PS5NorModifier.Core;

public class PS5Nor
{
    private static readonly Dictionary<string, int> Offset = new()
    {
        { "Unk_0x000000",   0x000000 },
        { "EthernetMac",    0x1C4020 },
        { "ConsoleEdition", 0x1c7011 },
        { "OffsetTwo",      0x1c7030 },
        { "MoboSerial",     0x1C7200 },
        { "ConsoleSerial",  0x1c7210 },
        { "BoardVariant",   0x1c7226 },
        { "WifiMac",        0x1C73C0 },
    };

    public enum ConsoleEdition : int
    {
        Unknown,
        Disc = 0x2,
        Digital = 0x3,
    }

    public string ConsoleSerial { get; set; } = "";
    public string MoboSerial { get; set; } = "";
    public string WifiMac  { get; set; } = "";
    public string EthernetMac { get; set; } = "";
    public string BoardVariant { get; set; } = "";
    public string Region
    {
        get
        {
            return BoardVariant[^3..] switch
            {
                "00A" or "00B" => "Japan",
                "01A" or "01B" or
                "15A" or "15B" => "US, Canada, (North America)",
                "02A" or "02B" => "Australia / New Zealand, (Oceania)",
                "03A" or "03B" => "United Kingdom / Ireland",
                "04A" or "04B" => "Europe / Middle East / Africa",
                "05A" or "05B" => "South Korea",
                "06A" or "06B" => "Southeast Asia / Hong Kong",
                "07A" or "07B" => "Taiwan",
                "08A" or "08B" => "Russia, Ukraine, India, Central Asia",
                "09A" or "09B" => "Mainland China",
                "11A" or "11B" or
                "14A" or "14B" => "Mexico, Central America, South America",
                "16A" or "16B" => "Europe / Middle East / Africa",
                "18A" or "18B" => "Singapore, Korea, Asia",
                _ => "Unknown Region"
            };
        }
    }
    
    public ConsoleEdition Edition { get; set; }
    public int FileSize { get; private set; }

    public PS5Nor(byte[] data)
    {
        FileSize = data.Length;

        Edition = data[Offset["ConsoleEdition"]] switch
        {
            2 => ConsoleEdition.Disc,
            3 => ConsoleEdition.Digital,
            _ => ConsoleEdition.Unknown,
        };
        MoboSerial = BitConverter.ToString(data, Offset["MoboSerial"], 16);
        ConsoleSerial = BitConverter.ToString(data, Offset["ConsoleSerial"], 17);
        MoboSerial = BitConverter.ToString(data, Offset["MoboSerial"], 17);
        EthernetMac = BitConverter.ToString(data, Offset["EthernetMac"], 6);
        WifiMac = BitConverter.ToString(data, Offset["WifiMac"], 6);
        BoardVariant = BitConverter.ToString(data, Offset["BoardVariant"], 19).Replace("-", null).Replace("FF", null);
    }
}
