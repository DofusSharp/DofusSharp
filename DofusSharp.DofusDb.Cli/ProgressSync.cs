namespace DofusSharp.DofusDb.Cli;

class ProgressSync<T>(Action<T> callback) : IProgress<T>
{
    public void Report(T value) => callback.Invoke(value);

    public ProgressSync<T> Derive(Func<T, T> func) => new(x => callback(func(x)));
    public ProgressSync<TDerived> Derive<TDerived>(Func<TDerived, T> func) => new(x => callback(func(x)));
}
