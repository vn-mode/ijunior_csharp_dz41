using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        Deck deck = new Deck();
        Player player = new Player("Игрок");

        bool continueDrawing = true;
        string yesAnswer = "да";

        while (continueDrawing)
        {
            Console.WriteLine("Хотите взять карту? (Да/Нет)");
            string answer = Console.ReadLine();

            if (answer.ToLower() == yesAnswer)
            {
                player.DrawFrom(deck);
            }
            else
            {
                continueDrawing = false;
            }
        }

        player.ShowCards();

        Console.ReadLine();
    }
}

public class Card
{
    public string Suit { get; private set; }
    public string Rank { get; private set; }

    public Card(string suit, string rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} {Suit}";
    }
}

public class Deck
{
    private List<Card> cards;
    private static Random random = new Random();

    public Deck()
    {
        cards = new List<Card>();
        Initialize();
        Shuffle();
    }

    public Card Draw()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("Колода пуста");
        }

        Card card = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);

        return card;
    }

    private void Initialize()
    {
        string[] suits = { "Черви", "Бубны", "Трефы", "Пики" };
        string[] ranks =
        {
            "Двойка", "Тройка", "Четверка", "Пятерка", "Шестерка",
            "Семерка", "Восьмерка", "Девятка", "Десятка",
            "Валет", "Дама", "Король", "Туз"
        };

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    private void Shuffle()
    {
        int n = cards.Count;

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }
}

public class Player
{
    private List<Card> hand;

    public string Name { get; private set; }

    public Player(string name)
    {
        Name = name;
        hand = new List<Card>();
    }

    public void DrawFrom(Deck deck)
    {
        Card drawnCard = deck.Draw();
        hand.Add(drawnCard);

        Console.WriteLine($"{Name} взял карту: {drawnCard}");
    }

    public void ShowCards()
    {
        Console.WriteLine($"Карты в руке у {Name}:");

        foreach (Card card in hand)
        {
            Console.WriteLine(card.ToString());
        }
    }
}
