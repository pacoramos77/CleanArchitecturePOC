using SharedKernel.DateTimeContext;

namespace Core.Tests;

public class TestBaseFixture
{
    protected DateTime DateTimeMock { get; }
    private readonly DateTimeProviderContext _dateTimeProviderContext;

    public TestBaseFixture(DateTime? dateTimeMock = default)
    {
        DateTimeMock = dateTimeMock ?? DateTime.Parse("2022-12-31T22:45:00Z").ToUniversalTime();
        _dateTimeProviderContext = new DateTimeProviderContext(DateTimeMock);
    }

    internal void Dispose()
    {
        _dateTimeProviderContext.Dispose();
    }
}
