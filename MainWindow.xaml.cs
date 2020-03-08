using RegistroDetails.UI.Registros;
using System.Windows;


namespace RegistroDetails
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            rPersonas pers = new rPersonas();
            pers.Show();
        }
    }
}
