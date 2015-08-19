using System;

using Xamarin.Forms;

using xamarinnycmonkeys.Models;
using xamarinnycmonkeys.ViewModels;

namespace xamarinnycmonkeys.Views
{
    public class MonkeyListPage : ContentPage
    {
        MonkeyListViewModel _viewModel = new MonkeyListViewModel();

        public MonkeyListPage()
        {
            var spinner = new ActivityIndicator();
            spinner.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            spinner.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
            spinner.Color = Color.Blue;

            var list = new ListView();
            list.SetBinding(ListView.ItemsSourceProperty, "MonkeyList");

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(ImageCell.TextProperty, "Name");
            cell.SetBinding(ImageCell.DetailProperty, "Location");
            cell.SetBinding(ImageCell.ImageSourceProperty, "Image");

            list.ItemTemplate = cell;

            //listView
            // --> ItemTemplate
            // ----> DataTemplate
            // -------> Cell

            var getMonkeys = new Button
            {
                Text = "Get Monkeys"
            };

            getMonkeys.Clicked += async (sender, e) => 
            {
                try
                {
                    await _viewModel.GetMonkeysAsync();
                }
                catch
                {
                    DisplayAlert("Error", "No Monkeys Found :(", "OK");
                }
            };

            list.ItemTapped += async (sender, e) =>
            {
                var detail = new MonkeyPage();
                detail.BindingContext = e.Item;
                await Navigation.PushAsync(detail);

                list.SelectedItem = null;
            };

            Content = new StackLayout
            { 
                Children =
                {
                    spinner, list, getMonkeys
                }
            };

            BindingContext = _viewModel;
        }
    }
}


