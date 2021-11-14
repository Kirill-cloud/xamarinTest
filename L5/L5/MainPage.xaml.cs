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
            int score;

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && 
                !string.IsNullOrWhiteSpace(suranameEntry.Text) && 
                Int32.TryParse(scoreEntry.Text, out score))
            {
                await App.DataBase.AddPersonAsync(new Person
                {
                    Name = nameEntry.Text,
                    Surname = suranameEntry.Text,
                    Patronimic = patronimicEntry.Text,
                    Score = score,
                });

                nameEntry.Text = string.Empty;

                personsColectionView.ItemsSource = await
                    App.DataBase.GetPeopleAsync();
            }
        }
    }
}
