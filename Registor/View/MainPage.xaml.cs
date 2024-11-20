using Registor.ViewModel;

namespace Registor.View;

public partial class MainPage : ContentPage
{
    public MainPage(CryptoModuleViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CryptoModuleViewModel viewModel)
        {
            viewModel.GetCryptoModulesCommand.Execute(null);
        }
    }
}
