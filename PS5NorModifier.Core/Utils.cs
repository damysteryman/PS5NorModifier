using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PS5NorModifier.Core;

public static class Utils
{
    public static string HexStringToString(string hexString)
    {
        if (hexString == null || (hexString.Length & 1) == 1)
        {
            throw new ArgumentException();
        }
        var sb = new StringBuilder();
        for (var i = 0; i < hexString.Length; i += 2)
        {
            var hexChar = hexString.Substring(i, 2);
            sb.Append((char)Convert.ToByte(hexChar, 16));
        }
        return sb.ToString();
    }

    /// <summary>
    /// Lauinches a URL in a new window using the default browser...
    /// </summary>
    /// <param name="url">The URL you want to launch</param>
    public static void OpenUrl(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }
}