/// <summary>
/// This interface represents the object that is to observe an observable
/// </summary>
public interface IObserver
{
    /// <summary>
    /// Called by the Observable when notify the observers
    /// </summary>
    public abstract void UpdateObservers(string message = null);
}

