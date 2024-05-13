public interface IListener
{
    EventManager EventManager { get; }
    void OnEnable();
    void OnDisable();
}