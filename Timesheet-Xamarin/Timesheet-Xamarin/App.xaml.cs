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
            InitializeComponent();
            Initialize();
            MainPage = new Login();
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
