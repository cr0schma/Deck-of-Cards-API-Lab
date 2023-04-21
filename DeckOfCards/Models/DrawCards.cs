namespace DeckOfCards.Models
{
    public class DrawCards
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public Card[] cards { get; set; }
        public int remaining { get; set; }

        public Pile pile { get; set; }

        public class Card
        {
            public string code { get; set; }
            public string image { get; set; }
            public Images images { get; set; }
            public string value { get; set; }
            public string suit { get; set; }
            public bool keep { get; set; }
        }

        public class Images
        {
            public string svg { get; set; }
            public string png { get; set; }
        }
    }
}
