using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Xappy.Content.Scenarios;
using Xappy.Domain.Conversational;

namespace Xappy.Scenarios
{
    public partial class ConversationPage : ContentPage
    {
        public ConversationPage()
        {
            InitializeComponent();

            var conversationService = new ConversationPageViewModel(new ConversationMockService());
            BindingContext = conversationService;
            conversationService.InitAsync();
        }
    }
}
