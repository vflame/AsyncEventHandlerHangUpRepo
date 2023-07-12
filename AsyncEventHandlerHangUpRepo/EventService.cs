using Microsoft.VisualStudio.Threading;

namespace AsyncEventHandlerHangUpRepo;

public class EventService : BackgroundService
{
    public event AsyncEventHandler<object>? EventHappened;

    public async Task FireEvent()
    {
        try
        {
            await (EventHappened?.InvokeAsync(this, null) ?? Task.CompletedTask);
        }
        catch (Exception ex)
        {
        }
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                await FireEvent();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

