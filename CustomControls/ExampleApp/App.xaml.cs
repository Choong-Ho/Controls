﻿using ExampleApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ExampleApp
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindowViewModel vm = new MainWindowViewModel();
            MainWindow window = new MainWindow();
            window.DataContext = vm;
            window.Show();

            base.OnStartup(e);
        }

    }
}
