using DeckOfCards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DeckOfCards.Services;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DeckOfCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DeckApiService _deckApiService;
        //private int cardsToDraw = 0;

        public HomeController(ILogger<HomeController> logger, DeckApiService deckApiService)
        {
            _logger = logger;
            _deckApiService = deckApiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                NewDeck newDeckResult = await _deckApiService.GetNewDeck();
                DrawCards result = await _deckApiService.DrawCards(newDeckResult.deck_id, 5);
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                Debug.WriteLine(ex);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Draw(DrawCards drawCards)
        {
            try
            {
                for (int i = 0; i < drawCards.cards.Count(); i++)
                {
                    if (drawCards.cards[i].keep == true)
                    {
                        //Thread.Sleep(5000); < - This was used before I used await, otherwise it was basically DDOS'ing the API by trying to add too fast
                        await _deckApiService.AddToPile(drawCards.deck_id, drawCards.cards[i].code);
                    }
                    /*if (drawCards.cards[i].keep == false)
                    {
                        cardsToDraw = ++i;
                    }*/
                }
                int cardsToDraw = (drawCards.cards.Where(item => item.keep == false)).Count();
                DrawCards result = await _deckApiService.DrawCards(drawCards.deck_id, cardsToDraw);
                if (cardsToDraw < 5)
                {
                    Pile viewPile = await _deckApiService.ViewPile(drawCards.deck_id);
                    result.pile = viewPile;
                }
                ModelState.Clear(); // This is what was needed to make the checkboxes not stay checked when brought over to the view
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                Debug.WriteLine(ex);
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}