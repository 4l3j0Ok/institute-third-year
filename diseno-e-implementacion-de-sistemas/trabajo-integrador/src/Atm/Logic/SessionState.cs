namespace Atm.Logic;

public enum SessionState
{
    Started,
    Authenticating,
    Validating,
    Operating,
    Closing,
    Closed,
}
