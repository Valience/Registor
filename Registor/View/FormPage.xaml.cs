using Registor.ViewModel;

namespace Registor.View;

public partial class FormPage : ContentPage
{
	public FormPage(FormPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}