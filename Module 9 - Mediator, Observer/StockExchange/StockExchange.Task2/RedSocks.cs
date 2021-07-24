using System;
using System.Collections.Generic;

namespace StockExchange.Task2
{
    public class RedSocks: Player
    {
        Dictionary<string, int> sellOffers = new Dictionary<string, int>();
        Dictionary<string, int> buyOffers = new Dictionary<string, int>();

        public RedSocks(Players player) : base(player)
        {
            SellOffers = sellOffers;
            BuyOffers = buyOffers;
        }
    }
}
