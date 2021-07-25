using RetailEquity.Filters;
using RetailEquity.Task1.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailEquity.Task1
{
    public class FilterFactoryManager: IFilterFactory
    {
        Bank bank;

        public FilterFactoryManager(Bank bank)
        {
            this.bank = bank;
        }

        public IFilter CreateFilter()
        {
            switch (bank)
            {
                case Bank.Barclays:
                    return new BarclaysFilterFactory().CreateFilter();
                case Bank.Bofa:
                    return new BofaFilterFactory().CreateFilter();
                case Bank.Connacord:
                    return new ConnacordFilterFactory().CreateFilter();
                default:
                    return new BarclaysFilterFactory().CreateFilter();
            }           
        }
    }
}
