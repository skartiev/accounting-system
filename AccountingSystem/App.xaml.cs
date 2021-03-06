﻿using System.Windows;
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
            var window = Container.Resolve<MainWindow>();
            window.Show();
        }
    }

   
}
