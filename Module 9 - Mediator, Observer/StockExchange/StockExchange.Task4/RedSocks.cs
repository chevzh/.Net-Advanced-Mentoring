﻿using System;
using System.Collections.Generic;

namespace StockExchange.Task4
{
    public class RedSocks: Player
    {
        Dictionary<string, int> sellOffers = new Dictionary<string, int>();
        Dictionary<string, int> buyOffers = new Dictionary<string, int>();

        int soldShares = 0;
        int boughtShares = 0;

        public int SoldShares => soldShares;
        public int BoughtShares => boughtShares;

        public RedSocks(Players player) : base(player)
        {
            SellOffers = sellOffers;
            BuyOffers = buyOffers;
        }

        public override void Update(string dealType, int numberOfShares)
        {
            if (dealType == "Sell")
            {
                this.boughtShares += numberOfShares;
            }

            if (dealType == "Buy")
            {
                this.soldShares += numberOfShares;
            }
        }
    }
}
