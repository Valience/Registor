using Registor.Model;
using Registor.ViewModel;

namespace Registor.View;

public partial class FormPage : ContentPage
{
	public FormPage(FormPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void PortUnfocused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is string port)
        {
            var viewModel = (FormPageViewModel)BindingContext;
            int index = viewModel.CryptoModule.Ports.IndexOf(port);
            if (index >= 0)
            {
                viewModel.CryptoModule.Ports[index] = entry.Text;
            }
        }
    }
}