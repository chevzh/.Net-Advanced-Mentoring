using System.Collections.Generic;

namespace StockExchange.Task1
{
    public class Players
    {
        public RedSocks RedSocks { get; set; }
        public Blossomers Blossomers { get; set; }

        public Players()
        { 
        }

        public Players(RedSocks redSocks, Blossomers blossomers)
        {
            RedSocks = redSocks;
            Blossomers = blossomers;
        }

        public bool SellOffer(string stockName, int numberOfShares, IPlayer player)
        {
            if(player == Blossomers)
            {
                return RedSocks.SellOffer(stockName, numberOfShares, player);
            }

            if (player == RedSocks)
            {
                return Blossomers.SellOffer(stockName, numberOfShares, player);
            }

            return false;
        }

        public bool BuyOffer(string stockName, int numberOfShares, IPlayer player)
        {
            if (player == Blossomers)
            {
                return RedSocks.BuyOffer(stockName, numberOfShares, player);
            }

            if (player == RedSocks)
            {
                return Blossomers.BuyOffer(stockName, numberOfShares, player);
            }

            return false;
        }
    }
}
