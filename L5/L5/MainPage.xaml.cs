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
        Person validPersonFromFields
        {
            get
            {
                int score;
                if (!string.IsNullOrWhiteSpace(nameEntry.Text) &&
                    !string.IsNullOrWhiteSpace(suranameEntry.Text) &&
                    Int32.TryParse(scoreEntry.Text, out score))
                {
                    var x = new Person
                    {
                        Name = nameEntry.Text,
                        Surname = suranameEntry.Text,
                        Patronimic = patronimicEntry.Text,
                        Score = score,
                    };
                    return x;
                }

                return null;
            }
        }

        private List<Person> people = new List<Person>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await UpdateViewState();
        }

        private async void AddPerson(object sender, EventArgs e)
        {
            Person newPerson = validPersonFromFields;

            if (newPerson != null)
            {
                nameEntry.Text = string.Empty;
                suranameEntry.Text = string.Empty;
                patronimicEntry.Text = string.Empty;
                scoreEntry.Text = string.Empty;

                await App.DataBase.AddPersonAsync(newPerson);
                await UpdateViewState();
            }

        }

        private async void ResolveCommand(object sender, EventArgs e)
        {
            var command = (sender as Button).Command;
            if (command != null)
            {
                command.Execute(null);
            }
            await UpdateViewState();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var id = (int)(sender as Button).CommandParameter;

            var updatedPerson = validPersonFromFields;
            if (updatedPerson != null)
            {
                updatedPerson.Id = id;
                var updatingPerson = people[people.IndexOf(people.Find(p => p.Id == id))] = updatedPerson;

                updatedPerson.UpdateMe.Execute(updatingPerson);
                await UpdateViewState();
            }
        }

        private async Task UpdateViewState()
        {
            people = await App.DataBase.GetPeopleAsync();
            personsColectionView.ItemsSource = people;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            personsColectionView.ItemsSource = await App.DataBase.GetFiltredPeopleAsync();
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            personsColectionView.ItemsSource = await App.DataBase.GetLinqFiltredPeopleAsync();
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            var editindPerson = people.Find(p => p.Id == (int)(sender as Button).CommandParameter);
            nameEntry.Text = editindPerson.Name;
            suranameEntry.Text = editindPerson.Surname;
            patronimicEntry.Text = editindPerson.Patronimic;
            scoreEntry.Text = editindPerson.Score.ToString();
        }
    }
}
