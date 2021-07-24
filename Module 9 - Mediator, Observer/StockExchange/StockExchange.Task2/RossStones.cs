using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchange.Task2
{
    public class RossStones: Player
    {
        Dictionary<string, int> sellOffers = new Dictionary<string, int>();
        Dictionary<string, int> buyOffers = new Dictionary<string, int>();

        public RossStones(Players player) : base(player)
        {
            SellOffers = sellOffers;
            BuyOffers = buyOffers;
        }
    }
}
