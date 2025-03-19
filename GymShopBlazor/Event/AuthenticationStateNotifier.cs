namespace GymShopBlazor.Event;
public class AuthenticationStateNotifier
{
    private Action _stateChanged;
    public event Action StateChanged
    {
        add => _stateChanged += value;
        remove => _stateChanged -= value;
    }
    public void NotifyStateChanged() => _stateChanged?.Invoke();
}
