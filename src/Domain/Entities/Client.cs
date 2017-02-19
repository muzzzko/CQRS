namespace Domain.Entities
{
    using System;

    public class Client : IEntity
    {
        public Client(string surname, string firstname, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException(nameof(surname));

            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentNullException(nameof(firstname));

            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentNullException(nameof(patronymic));

            Surname = surname;
            FirstName = firstname;
            Patronymic = patronymic;
        }

        public void CrushBike(Bike bike)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));
            bike.Crushed = true;
        }

        public readonly string Surname;

        public readonly string FirstName;

        public readonly string Patronymic;

        public string FullName => $"{Surname} {FirstName} {Patronymic}";
    }
}
