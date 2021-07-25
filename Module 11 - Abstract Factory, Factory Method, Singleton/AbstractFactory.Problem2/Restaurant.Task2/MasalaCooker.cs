using Restaurant.Task2.CookerFactories;
using System;

namespace AbstartFactory
{
    public class MasalaCooker
    {
        private ICooker cooker;

        public MasalaCooker(ICooker cooker)
        {
            this.cooker = cooker;
        }

        public void CookMasala(DateTime currentDate, Country country)
        {
            ICookerFactory cookerFactory = IsSummerDate(currentDate) ? new SummerFactory().CreateFactory(country) : new BasicFactory().CreateFactory(country);            

            this.CookMasala(cookerFactory);
        }

        private void CookMasala(ICookerFactory cookerFactory)
        {
            cookerFactory.CookRice(cooker);
            cookerFactory.CookChicken(cooker);
        }

        private bool IsSummerDate(DateTime currentDate)
        {
            return currentDate.Month >= 6 && currentDate.Month <= 8;
        }
    }
}
