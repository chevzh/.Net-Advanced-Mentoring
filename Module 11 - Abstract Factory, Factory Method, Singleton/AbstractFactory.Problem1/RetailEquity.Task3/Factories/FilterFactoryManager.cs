using RetailEquity.Filters;
using RetailEquity.Task3.Factories;

namespace RetailEquity.Task3
{
    public class FilterFactoryManager: IFilterFactory
    {
        Bank bank;
        Country country;

        public FilterFactoryManager(Bank bank, Country country)
        {
            this.bank = bank;
            this.country = country;
        }

        public IFilter CreateFilter()
        {
            switch (bank)
            {
                case Bank.Barclays:
                    if(country == Country.England)
                    {
                        return new BarclaysEnglandFilterFactory().CreateFilter();
                    }
                    return new BarclaysUSAFilterFactory().CreateFilter();
                case Bank.Bofa:
                    return new BofaFilterFactory().CreateFilter();
                case Bank.Connacord:
                    return new ConnacordFilterFactory().CreateFilter();
                case Bank.DeutscheBank:
                    return new DeutscheBankFilterFactory().CreateFilter();
                default:
                    return null;
            }           
        }
    }
}
