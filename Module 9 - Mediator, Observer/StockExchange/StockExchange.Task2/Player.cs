using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchange.Task2
{
    public abstract class Player
    {
        Players Mediator { get; }

        public string Id { get; }
        public Dictionary<string, int> SellOffers { get; set; }
        public Dictionary<string, int> BuyOffers { get; set; }

        public Player(Players player)
        {
            Mediator = player;
            Id = Guid.NewGuid().ToString();
        }

        public bool SellOffer(string stockName, int numberOfShares)
        {
            return Mediator.SellOffer(stockName, numberOfShares, this);
        }

        public bool SellOffer(string stockName, int numberOfShares, Player player)
        {
            if (this != player && Id != player.Id)
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

                SellOffers.Add(stockName, numberOfShares);
            }

            return false;
        }

        public bool BuyOffer(string stockName, int numberOfShares)
        {
            return Mediator.BuyOffer(stockName, numberOfShares, this);
        }

        public bool BuyOffer(string stockName, int numberOfShares, Player player)
        {
            if (this != player && Id != player.Id)
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

                BuyOffers.Add(stockName, numberOfShares);
            }

            return false;
        }
    }
}
