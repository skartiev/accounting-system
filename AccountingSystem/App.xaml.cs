using System.Windows;
using AccountingSystem.Domain.Abstract;
using AccountingSystem.Domain.Concrete;
using AccountingSystem.Domain.Entities;
using AccountingSystem.View;
using Microsoft.Practices.Unity;

namespace AccountingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnityContainer Container { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Container = new UnityContainer();
            Container.RegisterType<User>();
            Container.RegisterType<IUserRepository, UsersRepository>();
            
            var window = Container.Resolve<MainWindow>();
            window.Show();
        }
    }

   
}
