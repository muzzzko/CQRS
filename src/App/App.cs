namespace App
{
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Entities.Deposits;
    using Domain.Entities.CQRS;
    using LayerCQRS.CommandContext;
    using LayerCQRS.Criterion;


    public class App
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IQueryFactory _queryFactory;


        public App(
            ICommandFactory commandFactory,
            IQueryFactory queryFactory)
        {
            _commandFactory = commandFactory;
            _queryFactory = queryFactory;
        }



        public void AddBike(string name, decimal hourCost)
        {
            _commandFactory.Create<AddNewBikeCommandContext>().Execute(
                new AddNewBikeCommandContext()
                {
                    HourCost = hourCost,
                    Name = name
                });
        }

        public void AddBikeToRentPoint(string bikeName, Employee employee)
        {
            _commandFactory.Create<AddBikeToRentPointCommandContext>().Execute(
                new AddBikeToRentPointCommandContext
                {
                    BikeName = bikeName,
                    Employee = employee
                });
        }

        public void AddRentPoint(Employee employee, decimal money)
        {
            _commandFactory.Create<AddNewRentPointCommandContext>()
                .Execute(new AddNewRentPointCommandContext
                {
                    Employee = employee,
                    Money = money
                });
        }

        public void TakeBike(string bikeName, Client client, Deposit deposit)
        {
            _commandFactory.Create<TakeBikeCommandContext>().Execute
                (new TakeBikeCommandContext
                {
                    BikeName = bikeName,
                    Client = client,
                    Deposit = deposit
                });
        }

        public void ReturnBike(string bikeName, Employee employee)
        {
            _commandFactory.Create<ReturnBikeCommandContext>().Execute(
                new ReturnBikeCommandContext
                {
                    BikeName = bikeName,
                    Employee = employee
                });
        }


        public IEnumerable<RentPoint> GetAllRentPoint()
        {
            return _queryFactory.Create<GetAllEntitiesCriterion, IEnumerable<RentPoint>>()
                .Ask(new GetAllEntitiesCriterion());
        }

        public IEnumerable<Bike> GetAllBikes()
        {
            return _queryFactory.Create<GetAllEntitiesCriterion, IEnumerable<Bike>>().Ask(
                new GetAllEntitiesCriterion());
        }

        public IEnumerable<Rent> GetAllRents()
        {
            return _queryFactory.Create<GetAllEntitiesCriterion, IEnumerable<Rent>>().Ask(
                new GetAllEntitiesCriterion());
        }

        //public void AddEmployee(string surname, string firstname, string patronymic)
        //{
        //    Employee employee = new Employee(surname, firstname, patronymic);
        //    _employeeRepository.Add(employee);
        //}

        //public IEnumerable<Employee> GetEmployees()
        //{
        //    return _employeeRepository.All();
        //}

        //public void AddClient(string surname, string firstname, string patronymic)
        //{
        //    Client client = new Client(surname, firstname, patronymic);
        //    _clientRepository.Add(client);
        //}

        //public IEnumerable<Client> GetClients()
        //{
        //    return _clientRepository.All();
        //}


        //public IEnumerable<RentPoint> GetRents()
        //{
        //    return _rentPointRepository.All();
        //}

        

        //public void CrushBike(string bikeName,Client client)
        //{
        //    client.CrushBike(_bikeRepository.All().SingleOrDefault(x => x.Name == bikeName));
        //}

        //public void MakeResevation(string bikeName, Client client)
        //{
        //    _bikeService.MakeReservation(bikeName, client);
        //}

       
    }
}
