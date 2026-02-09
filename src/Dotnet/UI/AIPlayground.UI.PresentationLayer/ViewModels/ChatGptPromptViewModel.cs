using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AIPlayground.UI.Domain.Models;
using AIPlayground.UI.PresentationLayer.Resources;
using SimpleCqrs;
using System.Threading.Tasks;

namespace AIPlayground.UI.PresentationLayer.ViewModels
{
    public partial class ChatGptPromptViewModel : ObservableObject
    {
        private readonly IAsyncQueryDispatcher _queryDispatcher;

        [ObservableProperty]
        private string _question = string.Empty;

        [ObservableProperty]
        private string _response = string.Empty;

        [ObservableProperty]
        private bool _isBusy = false;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public ChatGptPromptViewModel(IAsyncQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [RelayCommand]
        private async Task SubmitQuestionAsync()
        {
            if (string.IsNullOrWhiteSpace(Question))
            {
                ErrorMessage = Translation.ErrorEmptyQuestion;
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;
            Response = string.Empty;

            try
            {
                var query = new GetChatGptAnswerQuery { Prompt = Question };
                var result = await _queryDispatcher.DispatchAsync(query);

                if (result.IsSuccess)
                {
                    Response = result.Value;
                }
                else
                {
                    ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Message));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
