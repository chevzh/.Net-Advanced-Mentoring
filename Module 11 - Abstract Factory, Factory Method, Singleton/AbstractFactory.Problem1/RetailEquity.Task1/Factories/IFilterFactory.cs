using RetailEquity.Filters;

namespace RetailEquity.Task1
{
    public interface IFilterFactory
    {
        public IFilter CreateFilter();
    }
}