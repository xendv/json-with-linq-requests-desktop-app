using System.Collections.Generic;
using System.Windows;

namespace ТСПО_1_JSON
{
    /// <summary>
    /// Логика взаимодействия для Task1_Window.xaml
    /// </summary>
    public partial class Task1_Window : Window
    {
        public List<KeyValuePair<string, string>> professionalStandards { get; set; } 
        public Task1_Window()
        {
            InitializeComponent();
        }

        public Task1_Window(JsonUtils jsonUtils)
        {
            InitializeComponent();
            professionalStandards = new List<KeyValuePair<string, string>>();
            var content = jsonUtils.ReadFileContent();
            foreach (var pair in content)
            {
                professionalStandards.Add(pair);
            }
            ProfessionalStandardsListBox.ItemsSource = professionalStandards;
            this.ShowDialog();
        }
    }
}
