using SharedKernel.GuidContext;

namespace System;

public static class GuidProvider
{
    public static Guid NewGuid() =>
        GuidProviderContext.Current == null
            ? Guid.NewGuid()
            : GuidProviderContext.Current.ContextGuid;

    public static Guid Empty => Guid.Empty;
}
