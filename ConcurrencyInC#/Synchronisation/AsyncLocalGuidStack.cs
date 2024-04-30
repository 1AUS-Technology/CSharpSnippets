using System.Collections.Immutable;

namespace ConcurrencyInC_.Synchronisation;

internal sealed class AsyncLocalGuidStack
{
    private readonly AsyncLocal<ImmutableStack<Guid>> _operationIds = new();

    public AsyncLocalGuidStack()
    {
        _operationIds.Value = ImmutableStack<Guid>.Empty;
    }

    private ImmutableStack<Guid> CurrentStack => _operationIds.Value ?? ImmutableStack<Guid>.Empty;

    public IDisposable Push(Guid value)
    {
        _operationIds.Value = CurrentStack.Push(value);
        return new PopWhenDisposed(this);
    }

    private void Pop()
    {
        // immutable stack so return a new instance after every operation
        ImmutableStack<Guid> newValue = CurrentStack.Pop();
       _operationIds.Value = newValue;
    }

    public Guid PeekCurrentValue => CurrentStack.Peek();

    private sealed class PopWhenDisposed : IDisposable
    {
        private AsyncLocalGuidStack _stack;

        public PopWhenDisposed(AsyncLocalGuidStack stack) =>
            _stack = stack;

        public void Dispose()
        {
            _stack?.Pop();
            _stack = null;
        }
    }

    public void PrintStack()
    {
        foreach (var guid in CurrentStack)
        {
            Console.WriteLine("Guid values in the current stack: " + guid);
        }
    }
}