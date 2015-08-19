using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Refractored.Xam.TTS;

namespace xamarinnycmonkeys.Views
{
    public partial class MonkeyPage : ContentPage
    {
        public MonkeyPage()
        {
            InitializeComponent();

            SpeakButton.Clicked += async (sender, e) => 
            {
                try
                {
                    await SpeakButtonClicked();
                }
                catch(Exception ex)
                {
                    DisplayAlert("Error", "Unable to speak text!", "OK");
                }
            };
        }

        private async Task SpeakButtonClicked()
        {
            CrossTextToSpeech.Current.Speak("hello");
        }
    }
}

