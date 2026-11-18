namespace Cajero.Logic;

public enum EstadoSesion
{
    Iniciada,
    Autenticando,
    Validando,
    Operando,
    Cerrando,
    Cerrada,
}
