using RetailEquity.Filters;
using RetailEquity.Task2.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailEquity.Task2.Factories
{
    public class DeutscheBankFilterFactory : IFilterFactory
    {
        public IFilter CreateFilter()
        {
            return new DeutscheBankFilter();
        }
    }
}
