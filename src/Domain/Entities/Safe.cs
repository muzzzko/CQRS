namespace Domain.Entities
{
    using Domain.Entities.Deposits;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class Safe : IEntity
    {
        protected readonly IList<PassportDeposit> _passportDeposits = new List<PassportDeposit>();
        public IEnumerable<PassportDeposit> PassportDeposits => _passportDeposits.AsEnumerable();

        public void putPassport(PassportDeposit passportDeposit)
        {
            _passportDeposits.Add(passportDeposit);
        }

        public void returnPassport(PassportDeposit passportDeposit)
        {
            PassportDeposit currentPassportDeposit = _passportDeposits
                .SingleOrDefault(x =>
                    x.Number == passportDeposit.Number &&
                    x.Series == passportDeposit.Series);

            if (passportDeposit == null)
                throw new InvalidOperationException("No such passport");

            _passportDeposits.Remove(currentPassportDeposit);
        }

    }
}
