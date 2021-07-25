using RetailEquity.Filters;
using RetailEquity.Task1.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailEquity.Task1.Factories
{
    public class BofaFilterFactory : IFilterFactory
    {
        public IFilter CreateFilter()
        {
            return new BofaFilter();
        }
    }
}
