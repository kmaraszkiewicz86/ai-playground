using AIPlayground.UI.PresentationLayer.ViewModels;

namespace AIPlayground.UI.PresentationLayer.Views;

public partial class ChatGptPrompt : ContentPage
{
	public ChatGptPrompt(ChatGptPromptViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}