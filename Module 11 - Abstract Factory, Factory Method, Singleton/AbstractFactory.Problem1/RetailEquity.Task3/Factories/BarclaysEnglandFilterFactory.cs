using RetailEquity.Filters;
using RetailEquity.Task3.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailEquity.Task3.Factories
{
    public class BarclaysEnglandFilterFactory : IFilterFactory
    {
        public IFilter CreateFilter()
        {
            return new BarclaysEnglandFilter();
        }
    }
}
