using RetailEquity.Filters;
using RetailEquity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailEquity.Task3.Filters
{
    public class BarclaysEnglandFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(x => x.Amount > 100 && x.Type == TradeType.Future);
        }
    }
}
