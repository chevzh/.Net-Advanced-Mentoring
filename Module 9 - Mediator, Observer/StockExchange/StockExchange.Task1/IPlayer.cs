using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchange.Task1
{
    public interface IPlayer
    {
        Dictionary<string, int> SellOffers { get; }
        Dictionary<string, int> BuyOffers { get; }

        public bool SellOffer(string stockName, int numberOfShares);
        public bool BuyOffer(string stockName, int numberOfShares);
    }
}
