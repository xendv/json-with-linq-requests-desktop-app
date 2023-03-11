using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ТСПО_1_JSON
{
    /// <summary>
    /// Логика взаимодействия для Task2_Window.xaml
    /// </summary>
    public partial class Task2_Window : Window
    {

        private readonly JsonUtils jsonUtils;
        public ObservableCollection<KeyValuePair<string, string>> Indicators { get; set; }
        public ObservableCollection<KeyValuePair<string, string>> UniversalCompetences { get; set; }
        public ObservableCollection<KeyValuePair<string, string>> CommonCompetences { get; set; }

        public KeyValuePair<string, string> SelectedUniversal { get; set; }
        public KeyValuePair<string, string> SelectedCommon { get; set; }

        public Task2_Window()
        {
            InitializeComponent();
        }

        public Task2_Window(JsonUtils jsonUtils)
        {
            InitializeComponent();
            this.jsonUtils = jsonUtils;

            UniversalCompetences = jsonUtils.GetUniversalCompetences();
            CommonCompetences = jsonUtils.GetCommonCompetences();
            SelectedUniversal = UniversalCompetences[0];
            SelectedCommon = CommonCompetences[0];

            foreach (var comp in UniversalCompetences)
            {
                UniversalCompetenceComboBox.Items.Add(comp);
            }
            UniversalCompetenceComboBox.SelectedItem = SelectedUniversal;

            foreach (var comp in CommonCompetences)
            {
                CommonCompetenceComboBox.Items.Add(comp);
            }
            CommonCompetenceComboBox.SelectedItem = SelectedCommon;

            Indicators = new ObservableCollection<KeyValuePair<string, string>>();
            IndicatorsDataGrid.ItemsSource = Indicators;
            ShowDialog();
        }

        private void ButtonUniversal_Click(object sender, RoutedEventArgs e)
        {
            var data = jsonUtils.GetIndicatorsByCompenensy(
                ((KeyValuePair<string, string>)UniversalCompetenceComboBox.SelectedItem).Key,
                "universalCompetencyRows"
                );
            FillTable(data);
        }

        private void ButtonCommon_Click(object sender, RoutedEventArgs e)
        {
            var data = jsonUtils.GetIndicatorsByCompenensy(
                ((KeyValuePair<string, string>)CommonCompetenceComboBox.SelectedItem).Key,
                "commonCompetencyRows"
                );
            FillTable(data);
        }

        private void FillTable(Indicators data)
        {
            Indicators.Clear();
            Indicators.Add(new KeyValuePair<string, string>(data.code, data.title));
            Indicators.Add(new KeyValuePair<string, string>(data.znatTitle, data.znat));
            Indicators.Add(new KeyValuePair<string, string>(data.umetTitle, data.umet));
            Indicators.Add(new KeyValuePair<string, string>(data.vladetTitle, data.vladet));
            IndicatorsDataGrid.ItemsSource = Indicators;
        }
    }
}
