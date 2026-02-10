using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AIPlayground.UI.Domain.Models;
using AIPlayground.UI.PresentationLayer.Resources;
using SimpleCqrs;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace AIPlayground.UI.PresentationLayer.ViewModels
{
    public partial class ChatGptPromptViewModel(ISimpleMediator _mediator, ILogger<ChatGptPromptViewModel>? _logger) : ObservableObject
    {
        public string Question
        {
            get => field = string.Empty;
            set => SetProperty(ref field, value);
        }

        public string Response
        {
            get => field = string.Empty;
            set => SetProperty(ref field, value);
        }

        public bool IsBusy
        {
            get => field;
            set => SetProperty(ref field, value);
        }

        public string ErrorMessage
        {
            get => field = string.Empty;
            set => SetProperty(ref field, value);
        }

        public ICommand SubmitCommand => new AsyncRelayCommand(SubmitQuestionAsync);

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
                var result = await _mediator.GetQueryAsync(query);

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