using RetailEquity.Filters;
using RetailEquity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailEquity.Task3.Filters
{
    public class DeutscheBankFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(x => x.Type == TradeType.Option && x.SubType == TradeSubType.NewOption && x.Amount > 90 && x.Amount < 120);
        }
    }
}
