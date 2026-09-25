using Pagination.Demo.Views.Forms;

namespace Pagination.Demo;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        LoadEnvironmentFile();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }

    private static void LoadEnvironmentFile()
    {
        var path = Path.Combine(AppContext.BaseDirectory, ".env");
        if (!File.Exists(path)) return;

        foreach (var line in File.ReadLines(path))
        {
            var entry = line.Trim();
            if (entry.Length == 0 || entry.StartsWith('#')) continue;

            var separator = entry.IndexOf('=');
            if (separator <= 0) continue;

            var name = entry[..separator].Trim();
            var value = entry[(separator + 1)..].Trim().Trim('"');
            Environment.SetEnvironmentVariable(name, value);
        }
    }
}
