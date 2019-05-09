using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyOptionPage : ContentPage
    {
        public CompanyOptionPage()
        {
            InitializeComponent();
        }
        private async void ClickAddProject(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddProjectPage());
        }
        private async void ClickAddWorker(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddWorkerPage());
        }
        private void ClickAddConsultant(object sender, EventArgs args)
        {
            //await Navigation.PushModalAsync(new AddProjectPage());
        }
    }
}