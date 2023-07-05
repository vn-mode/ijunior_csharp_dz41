using System;
using System.Collections.Generic;

public enum Suit
{
    Hearts, Diamonds, Clubs, Spades
}

public enum Rank
{
    Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
}

public class Card
{
    public Suit CardSuit { get; private set; }
    public Rank CardRank { get; private set; }

    public Card(Suit cardSuit, Rank cardRank)
    {
        CardSuit = cardSuit;
        CardRank = cardRank;
    }

    public override string ToString()
    {
        string[] russianSuits = { "Черви", "Бубны", "Трефы", "Пики" };
        string[] russianRanks =
        {
            "Двойка", "Тройка", "Четверка", "Пятерка", "Шестерка",
            "Семерка", "Восьмерка", "Девятка", "Десятка",
            "Валет", "Дама", "Король", "Туз"
        };

        return $"{russianRanks[(int)CardRank - 2]} {russianSuits[(int)CardSuit]}";
    }
}

public class Deck
{
    private List<Card> Cards;

    public Deck()
    {
        Cards = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Cards.Add(new Card(suit, rank));
            }
        }

        var rng = new Random();
        int n = Cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = Cards[k];
            Cards[k] = Cards[n];
            Cards[n] = value;
        }
    }

    public Card DrawCard()
    {
        if (Cards.Count == 0)
        {
            throw new InvalidOperationException("Колода пуста");
        }

        Card card = Cards[Cards.Count - 1];
        Cards.RemoveAt(Cards.Count - 1);

        return card;
    }
}

public class Player
{
    public string PlayerName { get; private set; }
    public List<Card> PlayerHand { get; private set; }

    public Player(string playerName)
    {
        PlayerName = playerName;
        PlayerHand = new List<Card>();
    }

    public void DrawCardFromDeck(Deck deck)
    {
        Card drawnCard = deck.DrawCard();
        PlayerHand.Add(drawnCard);

        Console.WriteLine($"{PlayerName} взял карту: {drawnCard}");
    }

    public void ShowPlayerHand()
    {
        Console.WriteLine($"Карты в руке у {PlayerName}:");
        foreach (Card card in PlayerHand)
        {
            Console.WriteLine(card.ToString());
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Deck deck = new Deck();
        Player player = new Player("Игрок");

        for (int i = 0; i < 5; i++)
        {
            player.DrawCardFromDeck(deck);
        }

        player.ShowPlayerHand();

        Console.ReadLine();
    }
}
