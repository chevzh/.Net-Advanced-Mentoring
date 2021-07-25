using RetailEquity.Filters;
using RetailEquity.Task2.Factories;

namespace RetailEquity.Task2
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
                case Bank.DeutscheBank:
                    return new DeutscheBankFilterFactory().CreateFilter();
                default:
                    return new BarclaysFilterFactory().CreateFilter();
            }           
        }
    }
}
