using Cajero.Logica;

namespace Cajero.Tests;

public class CifradoTests
{
    [Test]
    public void EncriptarPin_Devuelve64CaracteresHexadecimales()
    {
        var pinEncriptado = Cifrado.EncriptarPin("1234");

        Assert.That(pinEncriptado, Has.Length.EqualTo(64));
        Assert.That(pinEncriptado, Does.Match("^[0-9A-F]{64}$"));
    }

    [Test]
    public void EncriptarPin_EsDeterministaYNoDevuelveElPinEnClaro()
    {
        Assert.That(Cifrado.EncriptarPin("1234"), Is.EqualTo(Cifrado.EncriptarPin("1234")));
        Assert.That(Cifrado.EncriptarPin("1234"), Is.Not.EqualTo("1234"));
        Assert.That(Cifrado.EncriptarPin("1234"), Is.Not.EqualTo(Cifrado.EncriptarPin("1235")));
    }
}
