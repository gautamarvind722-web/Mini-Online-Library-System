using Mini_Backed.Models;
using System;

public class OverdueCheckService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceScopeFactory _scopeFactory;

    public OverdueCheckService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Run every 1 hour
        _timer = new Timer(UpdateOverdueBooks, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private void UpdateOverdueBooks(object state)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MiniOnlineLibraryContext>();

        var now = DateTime.Now;
        var overdueBooks = context.BorrowTransactions
            .Where(bt => bt.Status == "Borrowed" && bt.BorrowDate.AddDays(2) < now)
            .ToList();

        overdueBooks.ForEach(bt => bt.Status = "Overdue");
        context.SaveChanges();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}
