using System;
using System.Windows.Forms;
using Atm.Presentation;

namespace Atm;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new AtmForm());
    }
}
