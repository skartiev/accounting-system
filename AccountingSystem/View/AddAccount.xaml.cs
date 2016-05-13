using System.Windows;
using AccountingSystem.ViewModel;
using Microsoft.Practices.Unity;

namespace AccountingSystem.View
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        [Dependency]
        public AddAccountViewModel ViewModel
        {
            set { DataContext = value; }
        }
        public AddAccount()
        {
            InitializeComponent();
        }
    }
}
