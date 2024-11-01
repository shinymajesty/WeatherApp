public class LocationState
{
    private LocationOption? _currentLocation;
    public event Action? OnLocationChange;

    public LocationOption? CurrentLocation
    {
        get => _currentLocation;
        set
        {
            _currentLocation = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnLocationChange?.Invoke();
}