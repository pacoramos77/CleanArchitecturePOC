using System.Collections;

namespace SharedKernel.GuidContext;

public sealed class GuidProviderContext : IDisposable
{
    internal Guid ContextGuid { get; set; }
    private static readonly ThreadLocal<Stack> ThreadScopeStack = new(() => new Stack());

    public GuidProviderContext(Guid contextGuidNow)
    {
        ContextGuid = contextGuidNow;
        ThreadScopeStack.Value?.Push(this);
    }

    public static GuidProviderContext? Current
    {
        get
        {
            if (ThreadScopeStack?.Value is null || ThreadScopeStack.Value.Count == 0)
                return null;
            else
                return ThreadScopeStack.Value.Peek() as GuidProviderContext;
        }
    }

    public void Dispose()
    {
        ThreadScopeStack.Value?.Pop();
    }
}
