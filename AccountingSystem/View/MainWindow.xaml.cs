using System.Windows;
using AccountingSystem.ViewModel;
using Microsoft.Practices.Unity;

namespace AccountingSystem.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public MainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
