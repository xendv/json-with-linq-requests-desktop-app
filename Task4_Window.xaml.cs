using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ТСПО_1_JSON
{
    /// <summary>
    /// Логика взаимодействия для Task4_Window.xaml
    /// </summary>
    public partial class Task4_Window : Window
    {
        private readonly JsonUtils jsonUtils;
        public ObservableCollection<Discipline> Disciplines { get; set; }

        public Task4_Window(JsonUtils jsonUtils)
        {
            InitializeComponent();
            this.jsonUtils = jsonUtils;

            Disciplines = new ObservableCollection<Discipline>();
            DisciplineDataGrid.ItemsSource = Disciplines;
            ShowDialog();
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            Disciplines = jsonUtils.GetDisciplines(((ComboBoxItem)TermsComboBox.SelectedItem).Content.ToString());
            DisciplineDataGrid.ItemsSource = Disciplines;
        }
    }
}
