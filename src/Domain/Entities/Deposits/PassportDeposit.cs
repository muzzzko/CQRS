namespace Domain.Entities.Deposits
{
    using System;

    public class PassportDeposit : Deposit
    {
        public PassportDeposit(string series, string number) 
            : base(DepositTypes.Passport)
        {
            if (string.IsNullOrWhiteSpace(series))
                throw new ArgumentNullException(nameof(series));

            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));

            for (int i = 0; i < series.Length; i++)
                if (series[i] < '0' || series[i] > '9')
                    throw new InvalidOperationException("Series have letter");

            for (int i = 0; i < number.Length; i++)
                if (number[i] < '0' || number[i] > '9')
                    throw new InvalidOperationException("Numer have letter");

            if (series.Length != 4 && number.Length != 6)
                throw new InvalidOperationException("Length is wrong");

            Series = series;
            Number = number;
        }



        public string Series { get; protected set; }

        public string Number { get; protected set; }
    }
}
