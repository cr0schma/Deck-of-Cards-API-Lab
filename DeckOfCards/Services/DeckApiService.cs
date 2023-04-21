using DeckOfCards.Models;

namespace DeckOfCards.Services
{
    public class DeckApiService
    {

        private readonly HttpClient _httpClient;
        public DeckApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NewDeck> GetNewDeck()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("new/shuffle/");

            NewDeck result = await response.Content.ReadAsAsync<NewDeck>();

            return result;
        }

        public async Task<DrawCards> DrawCards(string deckId, int cardCount)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{deckId}/draw/?count={cardCount}");

            DrawCards result = await response.Content.ReadAsAsync<DrawCards>();

            return result;
        }

        public async Task<Pile> AddToPile(string deckId, string cardCode)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{deckId}/pile/player1/add/?cards={cardCode}");

            Pile result = await response.Content.ReadAsAsync<Pile>();

            return result;
        }

        public async Task<Pile> ViewPile(string deckId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{deckId}/pile/player1/list/");

            Pile result = await response.Content.ReadAsAsync<Pile>();

            return result;
        }
    }
}
