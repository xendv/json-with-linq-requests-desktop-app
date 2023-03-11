using System.Windows;

namespace ТСПО_1_JSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Content = new MainPage(this);
        }

        private void ButtonTask1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
