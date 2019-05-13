using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Timesheet_Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            //var a = Application.Current.Properties["IdUser"];
            //var b = Application.Current.Properties["Token"];

            if (String.IsNullOrEmpty(Application.Current.Properties["IdUser"].ToString()) && String.IsNullOrEmpty(Application.Current.Properties["Token"].ToString()))
            {
                MainPage = new Login();
            }
            else
            {
                MainPage = new MainPage();
            }

            InitializeComponent();
            Initialize();
        }
        
        private async void Initialize()
        {
            await Foo<object>();
        }

        private async Task<TResponse> Foo<TResponse>()
    where TResponse : class
        {
            await Task.Delay(500);
            return null;
        }
        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
