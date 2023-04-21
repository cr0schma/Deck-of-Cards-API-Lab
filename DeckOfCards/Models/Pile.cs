namespace DeckOfCards.Models
{
    public class Pile
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public string remaining { get; set; }
        public Piles piles { get; set; }
        
        public class Piles
        {
            public Player1 player1 { get; set; }
        }

        public class Player1
        {
            public string remaining { get; set; }
            public Card[] cards { get; set; }
        }

        public class Card
        {
            public string image { get; set; }
            public string value { get; set; }
            public string suit { get; set; }
            public string code { get; set; }
        }

    }
}
