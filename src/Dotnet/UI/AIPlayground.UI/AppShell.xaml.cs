namespace AIPlayground.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute("ChatGptPrompt", typeof(AIPlayground.UI.PresentationLayer.Views.ChatGptPrompt));
        }
    }
}
