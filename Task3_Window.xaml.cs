using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ТСПО_1_JSON
{
    /// <summary>
    /// Логика взаимодействия для Task3_Window.xaml
    /// </summary>
    public partial class Task3_Window : Window
    {
        private readonly JsonUtils jsonUtils;
        public ObservableCollection<KeyValuePair<string, string>> Info { get; set; }
        public ObservableCollection<KeyValuePair<string, string>> Disciplines { get; set; }

        public KeyValuePair<string, string> SelectedDiscipline { get; set; }

        public Task3_Window()
        {
            InitializeComponent();
        }

        public Task3_Window(JsonUtils jsonUtils)
        {
            InitializeComponent();
            this.jsonUtils = jsonUtils;

            Disciplines = jsonUtils.GetDisciplines();
            SelectedDiscipline = Disciplines[0];

            foreach (var discipline in Disciplines)
            {
                DisciplineComboBox.Items.Add(discipline);
            }
            DisciplineComboBox.SelectedItem = SelectedDiscipline;

            Info = new ObservableCollection<KeyValuePair<string, string>>();
            DisciplineDataGrid.ItemsSource = Info;
            this.ShowDialog();
        }


        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            var data = jsonUtils.GetDisciplineInfo(((KeyValuePair<string, string>)DisciplineComboBox.SelectedItem).Key);
            Info.Clear();
            Info.Add(new KeyValuePair<string, string>(data.Index, data.Title));
            Info.Add(new KeyValuePair<string, string>("Цель", data.description));
            Info.Add(new KeyValuePair<string, string>("Компетенции", data.competences));
            Info.Add(new KeyValuePair<string, string>("З.Е.", data.unitsCost));
            Info.Add(new KeyValuePair<string, string>("Семестры", Terms(data.terms)));
            DisciplineDataGrid.ItemsSource = Info;
        }

        private string Terms(List<bool> terms)
        {
            var result = "";
            terms.ForEach((term) => result += term ? '+' : '-');
            return result;
        }

    }
}
