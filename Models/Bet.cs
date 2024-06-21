namespace ApiRestRoulette.Models
{
    public class Bet
    {
        public BetType TypeBet { get; set; }
        public decimal BetMoney { get; set; }
    }

    public enum BetType
    {
        NumberColor = 1,
        EvenOdd = 2,
        Color = 3
    }
}
