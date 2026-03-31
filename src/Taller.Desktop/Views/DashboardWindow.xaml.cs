using System.Windows;

namespace Taller.Desktop.Views;

public partial class DashboardWindow : Window
{
    public DashboardWindow(string token)
    {
        InitializeComponent();
        SummaryTextBlock.Text = "Dashboard inicial listo. Próximas vistas: Kanban, Presupuestos, Siniestros y Detalle de Orden." +
                                "\n\nJWT recibido (truncado): " + token[..Math.Min(30, token.Length)] + "...";
    }
}
