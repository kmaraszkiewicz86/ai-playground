using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleCqrs;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AIPlayground.UI.PresentationLayer.Models;

namespace AIPlayground.UI.PresentationLayer.ViewModels
{
    public partial class AIDemoListViewModel : ObservableObject
    {
        public ObservableCollection<ViewPageMetaData> ViewPages { get; }

        public ICommand NavigateToPageCommand => new AsyncRelayCommand<string>(NavigateToPageAsync!);

        public AIDemoListViewModel(ISimpleMediator mediator, ILoggerFactory loggerFactory)
        {
            ViewPages = [
                new ViewPageMetaData { Title = "ChatGPT Prompt", Route = "ChatGptPrompt" }
            ];
        }

        private async Task NavigateToPageAsync(string route)
        {
            if (!string.IsNullOrEmpty(route))
            {
                await Shell.Current.GoToAsync(route);
            }
        }
    }

    
}
