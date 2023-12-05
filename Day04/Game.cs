using System.Diagnostics;

namespace Day04;

public class Game
{
    public IEnumerable<Card> Cards { get; set; }
    public IEnumerable<Card> Result { get; private set; }

    public Game(IEnumerable<string> lines)
    {
        var list = new List<Card>();
        foreach (var line in lines)
        {
            list.Add(new Card(line));
        }
        Cards = list;
        Result = CardWinnings(Cards);
    }

    private IEnumerable<Card> CardWinnings(IEnumerable<Card> cards)
    {
        var res = new List<Card>();
        for (int c = 0; c < cards.Count(); c++)
        {
            var card = cards.ElementAt(c);
            var next = card.Winners.Count(w => card.Picks.Contains(w));
            var wonCards = new List<Card>();
            for (int i = 1; i <= next; i++)
            {
                if (card.Id + i <= Cards.Count())
                {
                    wonCards.Add(Cards.ElementAt(card.Id - 1 + i));
                }
            }
            if (wonCards.Any())
            {
                res.AddRange(wonCards);
                var addtionalCards = CardWinnings(wonCards);
                res.AddRange(addtionalCards);
            }
        }
        return res;
    }

    public static IEnumerable<Card> CardWinnings2(IEnumerable<Card> cards)
    {
        for (int c = 0; c < cards.Count(); c++)
        {
            var card = cards.ElementAt(c);
            var next = card.Winners.Count(w => card.Picks.Contains(w));
            for (int i = 1; i <= next; i++)
            {
                if (card.Id + i <= cards.Count())
                {
                    var wonCard = cards.ElementAt(c + i);
                    wonCard.Amount += card.Amount;
                }
            }
        }
        return cards;
    }
}
