namespace ConsoleApp
{
    using System;
    using App;
    using Autofac;
    using Domain.Repositories;
    using Domain.Services;
    using Domain.Entities;
    using Domain.Entities.Deposits;
    using Domain.Entities.CQRS;
    using LayerCQRS.Command;
    using LayerCQRS.CommandContext;
    using System.Reflection;
    using LayerCQRS.Query;
    using LayerCQRS.Criterion;
    using System.Collections.Generic;


    public class Program
    { 
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .SingleInstance();

            containerBuilder
                .RegisterType<BikeNameVerifier>()
                .As<IBikeNameVerifier>();

            containerBuilder
                .RegisterType<BikeService>()
                .As<IBikeService>();

            containerBuilder
                .RegisterType<DepositCalculator>()
                .As<IDepositCalculator>();

            containerBuilder
                .RegisterType<RentService>()
                .As<IRentService>();

            containerBuilder
                .RegisterType<DepositService>()
                .As<IDepositService>();

            // ---------------- Command -----------------------

            containerBuilder
                .RegisterType<AddNewBikeCommand>()
                .As<ICommand<AddNewBikeCommandContext>>();

            containerBuilder
                .RegisterType<AddNewRentPointCommand>()
                .As<ICommand<AddNewRentPointCommandContext>>();

            containerBuilder
             .RegisterType<AddBikeToRentPointCommand>()
             .As<ICommand<AddBikeToRentPointCommandContext>>();

            containerBuilder
            .RegisterType<TakeBikeCommand>()
            .As<ICommand<TakeBikeCommandContext>>();

            containerBuilder
                .RegisterType<ReturnBikeCommand>()
                .As<ICommand<ReturnBikeCommandContext>>();

            //----------------------Query------------------------

            containerBuilder
                .RegisterType<GetAllRentPoint>()
                .As<IQuery<GetAllEntitiesCriterion, IEnumerable<RentPoint>>>();

            containerBuilder
              .RegisterType<GetAllBikes>()
              .As<IQuery<GetAllEntitiesCriterion, IEnumerable<Bike>>>();

            containerBuilder
              .RegisterType<GetAllRents>()
              .As<IQuery<GetAllEntitiesCriterion, IEnumerable<Rent>>>();

            //----------------------------------------------------



            containerBuilder
                .RegisterTypedFactory<ICommandFactory>();

            containerBuilder
                .RegisterTypedFactory<IQueryFactory>();    
                         
                

            containerBuilder.RegisterType<App>();

            IContainer container = containerBuilder.Build();

          
            
            App app = container.Resolve<App>();
            

           
            Employee employee = new Employee("Иванов", "Иван", "Иванович");
            Employee employee1 = new Employee("Мазеина", "Надежда", "Николаевна");
           // Client client = new Client("Петров", "Петр", "Петрович");
            Client client1 = new Client("Петров", "Сергей", "Валерьевич");
            app.AddRentPoint(employee, 100);
            app.AddRentPoint(employee1, 6000);

            app.AddBike("Кама", 50);
            app.AddBikeToRentPoint("Кама", employee);
            app.AddBike("Фишер", 100);
            app.AddBikeToRentPoint("Фишер", employee);
           

            IEnumerable<RentPoint> lr = app.GetAllRentPoint();
            // app.MakeResevation("Кама", client);
            // app.TakeBike("Кама", client, new MoneyDeposit(5000));
            //// app.CrushBike("Кама", client);
            // app.ReturnBike("Кама", employee1);
            app.TakeBike("Кама", client1, new MoneyDeposit(5000));
            IEnumerable<Rent> rr = app.GetAllRents();
            app.ReturnBike("Кама", employee);
            IEnumerable<Bike> br = app.GetAllBikes();


            container.Dispose();
        }
    }
}
