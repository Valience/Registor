using Registor.View;

namespace Registor;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(FormPage), typeof(FormPage));
    }
}
