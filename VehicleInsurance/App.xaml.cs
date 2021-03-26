using Autofac;
using System.Windows;
using VehicleInsurance.Model;

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
            var builder = new ContainerBuilder();

            builder.RegisterType<Driver>().As<IDriver>();

            builder.Build();
        }
    }
}
