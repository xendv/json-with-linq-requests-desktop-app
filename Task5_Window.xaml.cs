using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ТСПО_1_JSON
{
    /// <summary>
    /// Логика взаимодействия для Task5_Window.xaml
    /// </summary>
    public partial class Task5_Window : Window
    {
        private readonly JsonUtils jsonUtils;
        public ObservableCollection<Schedule> ScheduleInfos { get; set; }

        public Task5_Window(JsonUtils jsonUtils)
        {
            InitializeComponent();
            this.jsonUtils = jsonUtils;
            ScheduleDataGrid.ItemsSource = ScheduleInfos;
            ShowDialog();
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            ScheduleInfos = jsonUtils.GetSchedule(((ComboBoxItem)CoursesComboBox.SelectedItem).Content.ToString());
            ScheduleDataGrid.ItemsSource = ScheduleInfos;
        }
    }
}
