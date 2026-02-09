using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AIPlayground.UI.Domain.Models;
using AIPlayground.UI.PresentationLayer.Resources;
using SimpleCqrs;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AIPlayground.UI.PresentationLayer.ViewModels
{
    public partial class ChatGptPromptViewModel : ObservableObject
    {
        private readonly IAsyncQueryDispatcher _queryDispatcher;
        private readonly ILogger<ChatGptPromptViewModel>? _logger;

        [ObservableProperty]
        private string _question = string.Empty;

        [ObservableProperty]
        private string _response = string.Empty;

        [ObservableProperty]
        private bool _isBusy = false;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public ChatGptPromptViewModel(IAsyncQueryDispatcher queryDispatcher, ILogger<ChatGptPromptViewModel>? logger = null)
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
            _logger = logger;
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
                    var errorDetails = string.Join(", ", result.Errors.Select(e => e.Message));
                    _logger?.LogError("Failed to get ChatGPT response: {ErrorDetails}", errorDetails);
                    ErrorMessage = Translation.ErrorProcessingRequest;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception while processing ChatGPT query");
                ErrorMessage = Translation.ErrorProcessingRequest;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
