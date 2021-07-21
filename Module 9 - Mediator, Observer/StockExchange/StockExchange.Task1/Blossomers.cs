using System;
using System.Collections.Generic;

namespace StockExchange.Task1
{
    public class Blossomers: IPlayer
    {
        Players player;

        Dictionary<string, int> sellOffers = new Dictionary<string, int>();
        Dictionary<string, int> buyOffers = new Dictionary<string, int>();

        Dictionary<string, int> IPlayer.SellOffers => sellOffers;
        Dictionary<string, int> IPlayer.BuyOffers => buyOffers;

        public Blossomers(Players player)
        {
            this.player = player;
        }

        public bool SellOffer(string stockName, int numberOfShares)
        {
            return player.SellOffer(stockName, numberOfShares, this);
        }

        public bool SellOffer(string stockName, int numberOfShares, IPlayer player)
        {
            if (this != player)
            {
                int offerNumber;

                if (player.BuyOffers.TryGetValue(stockName, out offerNumber))
                {
                    if (offerNumber == numberOfShares)
                    {
                        player.BuyOffers.Remove(stockName);

                        return true;
                    }
                }

                sellOffers.Add(stockName, numberOfShares);
            }

            return false;
        }

        public bool BuyOffer(string stockName, int numberOfShares)
        {
            return player.BuyOffer(stockName, numberOfShares, this);
        }

        public bool BuyOffer(string stockName, int numberOfShares, IPlayer player)
        {
            if (this != player)
            {
                int offerNumber;

                if (player.SellOffers.TryGetValue(stockName, out offerNumber))
                {
                    if (offerNumber == numberOfShares)
                    {
                        player.SellOffers.Remove(stockName);

                        return true;
                    }
                }

                buyOffers.Add(stockName, numberOfShares);
            }

            return false;
        }
    }
}
