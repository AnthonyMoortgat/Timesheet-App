using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Log;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectInfo : ContentPage
	{
        ObservableCollection<LogDto> logsCollection = new ObservableCollection<LogDto>();
        public ObservableCollection<LogDto> LogsCollection { get { return logsCollection; } }
        string idUser = Application.Current.Properties["IdUser"].ToString();
        private Dictionary<int, string> logsWithKey = new Dictionary<int, string>();
        UserServices userServices = new UserServices();
        List<LogDto> logs = new List<LogDto>();

        public ProjectInfo(int ProjectId)
        {
            InitializeComponent();
            Start(ProjectId);
            LogProjectList.ItemsSource = LogsCollection;
        }

        private async void Start(int id)
        {
            List<LogDto> tempLogs = await userServices.GetAllUserLogsAsync(int.Parse(idUser));
            foreach (var log in tempLogs)
            {
                if (log.ProjectID == id)
                {
                    logsCollection.Add(log);
                }
            } 
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (LogDto)e.SelectedItem;
                LogProjectList.SelectedItem = null;
                await Navigation.PushModalAsync(new MainPage2(item.ID));                
            }             
        }

        private void GoBack(object sender, EventArgs e)
        {
            var page = new MainPage();
            page.CurrentPage = page.Children[1];
            Application.Current.MainPage = page;           
        }
    }
}