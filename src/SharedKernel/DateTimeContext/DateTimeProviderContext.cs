using System.Collections;

namespace SharedKernel.DateTimeContext;

public sealed class DateTimeProviderContext : IDisposable
{
    internal DateTime ContextDateTimeNow { get; set; }
    private static readonly ThreadLocal<Stack> ThreadScopeStack = new(() => new Stack());

    public DateTimeProviderContext(DateTime contextDateTimeNow)
    {
        ContextDateTimeNow = contextDateTimeNow;
        ThreadScopeStack.Value?.Push(this);
    }

    public static DateTimeProviderContext? Current
    {
        get
        {
            if (ThreadScopeStack?.Value is null || ThreadScopeStack.Value.Count == 0)
                return null;
            else
                return ThreadScopeStack.Value.Peek() as DateTimeProviderContext;
        }
    }

    public void Dispose()
    {
        ThreadScopeStack.Value?.Pop();
    }
}