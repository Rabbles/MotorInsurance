using Autofac;
using System.Windows;
using VehicleInsurance.Factories;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.ViewModel;

namespace VehicleInsurance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var calculateFactory = new CalculateRulesFactory();
            var declineFactory = new DeclineRulesFactory();
            var vm = new MainWindowViewModel(calculateFactory, declineFactory);
            Application.Current.MainWindow = new MainWindow(vm);
            Application.Current.MainWindow.Show();

            //var builder = new ContainerBuilder();

            //builder.RegisterType<Driver>().As<IDriver>();
            //builder.RegisterType<CalculateRulesFactory>().As<ICalculateFactory>();

            //builder.Build();
        }
    }
}
