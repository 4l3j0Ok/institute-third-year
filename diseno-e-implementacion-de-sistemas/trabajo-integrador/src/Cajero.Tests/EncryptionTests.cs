using Cajero.Logic;

namespace Cajero.Tests;

public class EncryptionTests
{
    [Test]
    public void EncryptPin_Returns64HexadecimalCharacters()
    {
        var encryptedPin = Encryption.EncryptPin("1234");

        Assert.That(encryptedPin, Has.Length.EqualTo(64));
        Assert.That(encryptedPin, Does.Match("^[0-9A-F]{64}$"));
    }

    [Test]
    public void EncryptPin_IsDeterministicAndDoesNotReturnTheClearPin()
    {
        Assert.That(Encryption.EncryptPin("1234"), Is.EqualTo(Encryption.EncryptPin("1234")));
        Assert.That(Encryption.EncryptPin("1234"), Is.Not.EqualTo("1234"));
        Assert.That(Encryption.EncryptPin("1234"), Is.Not.EqualTo(Encryption.EncryptPin("1235")));
    }
}
