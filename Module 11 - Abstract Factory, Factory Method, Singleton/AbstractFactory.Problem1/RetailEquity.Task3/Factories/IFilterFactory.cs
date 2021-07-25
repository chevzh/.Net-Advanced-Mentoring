using RetailEquity.Filters;

namespace RetailEquity.Task3
{
    public interface IFilterFactory
    {
        public IFilter CreateFilter();
    }
}