using CommunityToolkit.Mvvm.Messaging;

namespace BestCrush.Services;

public class InitializationStateManager
{
    InitializationState _state = new("Starting...", false);

    public InitializationState GetState() => _state;

    public void UpdateState(string message, bool done = false)
    {
        _state = new InitializationState(message, done);
        WeakReferenceMessenger.Default.Send(_state);
    }

    public void RegisterCallback(Action<InitializationState> callback) => WeakReferenceMessenger.Default.Register<InitializationState>(this, (_, s) => callback(s));
}

public record InitializationState(string Message, bool Done);
