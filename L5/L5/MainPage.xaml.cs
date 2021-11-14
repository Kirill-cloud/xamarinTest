using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace L5
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            personsColectionView.ItemsSource = await
                App.DataBase.GetPeopleAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.DataBase.AddPersonAsync(new Person
                {
                    Name = nameEntry.Text,
                    Completed = completedCheckBox.IsChecked
                });

                nameEntry.Text = string.Empty;
                completedCheckBox.IsChecked = false;

                personsColectionView.ItemsSource = await
                    App.DataBase.GetPeopleAsync();
            }
        }
    }
}
