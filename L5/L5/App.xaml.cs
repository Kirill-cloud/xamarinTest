using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace L5
{
    public partial class App : Application
    {
        private static DataBase dataBase;

        public static DataBase DataBase
        {
            get
            {
                if (dataBase == null)
                {
                    dataBase = new DataBase(Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }
                return dataBase;
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
