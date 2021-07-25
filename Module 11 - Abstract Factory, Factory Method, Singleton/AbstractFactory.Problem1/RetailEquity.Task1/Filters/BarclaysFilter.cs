using RetailEquity.Filters;
using RetailEquity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailEquity.Task1.Filters
{
    public class BarclaysFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(x => x.Amount > 50 && x.SubType == TradeSubType.NyOption && x.Type == TradeType.Option);
        }
    }
}
