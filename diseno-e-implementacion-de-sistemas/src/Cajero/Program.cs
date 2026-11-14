using System;
using System.Windows.Forms;
using Cajero.Presentacion;

namespace Cajero;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormCajero());
    }
}
