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
	public partial class ProjectInfo : ContentPage
	{
        //List<Log> logs;
        //Project project;
        public ProjectInfo(int id)
        {
            InitializeComponent();
            //logs = new List<Log>();
            //logs.Add(new Log() { LogId = 1, ProjectId = 2, StartTime = DateTime.Now.AddHours(-4), EndTime = DateTime.Now, Description = "Test Log 1" });
            //logs.Add(new Log() { LogId = 2, ProjectId = 3, StartTime = DateTime.Now.AddHours(-50), EndTime = DateTime.Now, Description = "Test Log 2" });
            //logs.Add(new Log() { LogId = 3, ProjectId = 1, StartTime = DateTime.Now.AddHours(-7), EndTime = DateTime.Now, Description = "Test Log 3" });
            //logs.Add(new Log() { LogId = 4, ProjectId = 3, StartTime = DateTime.Now.AddHours(-10), EndTime = DateTime.Now, Description = "Test Log 4" });
            //logs.Add(new Log() { LogId = 5, ProjectId = 4, StartTime = DateTime.Now.AddHours(-666), EndTime = DateTime.Now, Description = "Test Log 5" });

            //gridLayout.RowDefinitions.Add(new RowDefinition());
            InitializeGrid();

            //logs opvragen met ingelogde userId + project id
            //this.project = GetProjectById(id);

        }

        private void InitializeGrid()
        {
            //int rowIndex = 2;
            //foreach (var log in logs)
            //{
            //    var description = new Label { Text = log.Description, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, FontSize = 10 };
            //    var start = new Label { Text = log.StartTime.ToString("hh:mm:ss"), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, FontSize = 10 };
            //    var end = new Label { Text = log.EndTime.ToString("hh:mm:ss"), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, FontSize = 10 };
            //    var hours = log.EndTime - log.StartTime;
            //    var total = new Label { Text = hours.ToString(), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, FontSize = 10 };

            //    gridLayout.Children.Add(description, 0, rowIndex);
            //    gridLayout.Children.Add(start, 1, rowIndex);
            //    gridLayout.Children.Add(end, 2, rowIndex);
            //    gridLayout.Children.Add(total, 3, rowIndex);
            //    rowIndex++;
            //}

        }

        private void GoBack(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}