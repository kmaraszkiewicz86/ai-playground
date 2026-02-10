using AIPlayground.UI.PresentationLayer.ViewModels;

namespace AIPlayground.UI.PresentationLayer.Views;

public partial class AIDemoList : ContentPage
{
	public AIDemoList(AIDemoListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}