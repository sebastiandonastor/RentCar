using Autofac;
using Persistence.Generic;
using RentCar.DAL.SQL;
using RentCar.Persistence.Interfaces;
using RentCar.UI.ViewModel;

namespace RentCar.UI.Startup
{
    public class Bootstraper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<RentCarContext>().AsSelf();

            return builder.Build();
        }
    }
}
