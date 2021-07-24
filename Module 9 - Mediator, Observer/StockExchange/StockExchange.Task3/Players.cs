﻿using System;
using System.Collections.Generic;

namespace StockExchange.Task3
{
    public class Players
    {
        public Blossomers blossomers;
        public RedSocks redSocks;
        public RossSocks rossSocks;

        public Blossomers Blossomers
        {
            get
            {
                return blossomers;
            }
            set
            {
                blossomers = value;
                agents.Add(blossomers);
            }
        }
        public RedSocks RedSocks
        {
            get
            {
                return redSocks;
            }
            set
            {
                redSocks = value;
                agents.Add(redSocks);
            }
        }
        public RossSocks RossSocks
        {
            get
            {
                return rossSocks;
            }
            set
            {
                rossSocks = value;
                agents.Add(rossSocks);
            }
        }

        HashSet<Player> agents = new HashSet<Player>();

        public Players()
        {
        }

        public bool SellOffer(string stockName, int numberOfShares, Player player)
        {
            bool result = false;

            foreach (Player agent in agents)
            {
                result = agent.SellOffer(stockName, numberOfShares, player);

                if (result)
                {
                    break;
                }
            }

            return result;
        }

        public bool BuyOffer(string stockName, int numberOfShares, Player player)
        {
            bool result = false;

            foreach (Player agent in agents)
            {
                result = agent.BuyOffer(stockName, numberOfShares, player);

                if (result)
                {
                    break;
                }
            }

            return result;
        }
    }
}
