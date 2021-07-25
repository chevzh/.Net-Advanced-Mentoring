using RetailEquity.Filters;

namespace RetailEquity.Task2
{
    public interface IFilterFactory
    {
        public IFilter CreateFilter();
    }
}