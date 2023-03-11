using System.Windows;
using System.Windows.Controls;

namespace ТСПО_1_JSON
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainWindow mainWin;
        private readonly JsonUtils jsonUtils = new JsonUtils();

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(MainWindow win)
        {
            InitializeComponent();
            this.mainWin = win;
        }

        public void ButtonTask1_Click(object sender, RoutedEventArgs e)
        {
            _ = new Task1_Window(jsonUtils)
            {
                Owner = mainWin
            };
        }

        public void ButtonTask2_Click(object sender, RoutedEventArgs e)
        {
            _ = new Task2_Window(jsonUtils)
            {
                Owner = mainWin
            };
        }

        private void ButtonTask3_Click(object sender, RoutedEventArgs e)
        {
            _ = new Task3_Window(jsonUtils)
            {
                Owner = mainWin
            };
        }

        private void ButtonTask4_Click(object sender, RoutedEventArgs e)
        {
            _ = new Task4_Window(jsonUtils)
            {
                Owner = mainWin
            };
        }

        private void ButtonTask5_Click(object sender, RoutedEventArgs e)
        {
            _ = new Task5_Window(jsonUtils)
            {
                Owner = mainWin
            };
        }
    }
}
