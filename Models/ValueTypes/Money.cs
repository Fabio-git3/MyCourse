using System;
using MyCourse.Models.Enums;

namespace MyCourse.Models.ValueTypes
{
    public class Money
    {
        public Money(): this(Currency.EUR, 0.00m){

        }

       public Money(Currency currency,decimal amount) {
           Amount= amount;
           Currency=currency;
       }
        private decimal amount=0;

        public decimal Amount{
            get{
                return amount;
            }
            set{
                if (value<0){
                    throw new InvalidOperationException("non è possibile inserire un valore negativo");
                }
                amount=value;
            }

        }

        public Currency Currency{
            get;set;
        }

        public override bool Equals(object obj)
        {
            return obj is Money money &&
                   amount == money.amount &&
                   Amount == money.Amount &&
                   Currency == money.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( Amount, Currency);
        }

        public override string ToString() {
            return $"{Currency}{Amount:#.00}";
        }

    }
}