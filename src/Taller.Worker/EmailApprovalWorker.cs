using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Taller.Application.Emails;

public sealed class EmailApprovalWorker : BackgroundService
{
    private readonly ILogger<EmailApprovalWorker> _logger;

    public EmailApprovalWorker(ILogger<EmailApprovalWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker IMAP iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            // TODO: implementar conexión IMAP real con MailKit.
            var emailBodyEjemplo = "APROBADO #123";
            var parsed = ApprovalEmailParser.Parse(emailBodyEjemplo);
            if (parsed is not null)
            {
                _logger.LogInformation("Procesado email: Estado={Estado}, PresupuestoId={PresupuestoId}", parsed.Value.estado, parsed.Value.presupuestoId);
                // TODO: actualizar presupuesto y registrar auditoría.
            }

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }
}
