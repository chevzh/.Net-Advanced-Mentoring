using RetailEquity.Filters;
using RetailEquity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailEquity.Task3.Filters
{
    public class ConnacordFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(x => x.Type == TradeType.Future && x.Amount > 10 && x.Amount < 40);
        }
    }
}
