using Cajero.Datos;
using Cajero.Logica;

namespace Cajero.Tests;

public class CentralDBTests
{
    private const string TarjetaValida = "1234567890123456";

    [Test]
    public void ValidarPin_AceptaElPinEncriptado()
    {
        var central = new CentralDB();

        Assert.That(central.ValidarPin(TarjetaValida, Cifrado.EncriptarPin("1234")), Is.True);
    }

    [Test]
    public void ValidarPin_RechazaElPinEnClaro()
    {
        var central = new CentralDB();

        Assert.That(central.ValidarPin(TarjetaValida, "1234"), Is.False);
    }

    [Test]
    public void ValidarPin_RechazaUnaTarjetaDesconocida()
    {
        var central = new CentralDB();

        Assert.That(central.ValidarPin("0000000000000000", Cifrado.EncriptarPin("1234")), Is.False);
    }

    [Test]
    public void AutorizarDebito_AsignaNumeroDeTransaccionConPrefijoTx()
    {
        var central = new CentralDB();

        var respuesta = central.AutorizarDebito(TarjetaValida, 1_000m);

        Assert.That(respuesta.Autorizado, Is.True);
        Assert.That(respuesta.NumeroTransaccion, Does.Match("^TX[0-9]{8}$"));
    }

    [Test]
    public void EjecutarTransferencia_AsignaNumeroDeComprobanteConPrefijoTr()
    {
        var central = new CentralDB();

        var respuesta = central.EjecutarTransferencia(TarjetaValida, "0170099920000012345678", 1_000m);

        Assert.That(respuesta.Ejecutada, Is.True);
        Assert.That(respuesta.NumeroComprobante, Does.Match("^TR[0-9]{8}$"));
    }
}
