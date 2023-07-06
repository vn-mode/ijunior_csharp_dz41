using System;
using System.Collections.Generic;

public class Program
{
    private static void Main(string[] args)
    {
        Deck deck = new Deck();
        Player player = new Player("Игрок");

        bool continueDrawing = true;
        while (continueDrawing)
        {
            Console.WriteLine("Хотите взять карту? (Да/Нет)");
            string answer = Console.ReadLine();

            if (answer.ToLower() == "да")
            {
                player.DrawCard(deck);
            }
            else
            {
                continueDrawing = false;
            }
        }

        player.ShowHand();

        Console.ReadLine();
    }
}

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public class Card
{
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
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

        return $"{russianRanks[(int)Rank - 2]} {russianSuits[(int)Suit]}";
    }
}

public class Deck
{
    private List<Card> _cards;

    public Deck()
    {
        _cards = new List<Card>();
        InitializeDeck();
        ShuffleDeck();
    }

    private void InitializeDeck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }

    private void ShuffleDeck()
    {
        var rng = new Random();
        int deckSize = _cards.Count;
        while (deckSize > 1)
        {
            deckSize--;
            int randomIndex = rng.Next(deckSize + 1);
            Card tempCard = _cards[randomIndex];
            _cards[randomIndex] = _cards[deckSize];
            _cards[deckSize] = tempCard;
        }
    }

    public Card DrawCard()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("Колода пуста");
        }

        Card card = _cards[_cards.Count - 1];
        _cards.RemoveAt(_cards.Count - 1);

        return card;
    }
}

public class Player
{
    public string Name { get; private set; }
    private List<Card> _hand;

    public Player(string name)
    {
        Name = name;
        _hand = new List<Card>();
    }

    public void DrawCard(Deck deck)
    {
        Card drawnCard = deck.DrawCard();
        _hand.Add(drawnCard);

        Console.WriteLine($"{Name} взял карту: {drawnCard}");
    }

    public void ShowHand()
    {
        Console.WriteLine($"Карты в руке у {Name}:");
        foreach (Card card in _hand)
        {
            Console.WriteLine(card.ToString());
        }
    }
}
