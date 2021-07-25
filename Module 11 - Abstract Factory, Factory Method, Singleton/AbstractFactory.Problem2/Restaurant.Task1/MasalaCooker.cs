using Restaurant.Task1.CookerFactories;
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

        public void CookMasala(Country country)
        {
            ICookerFactory cookerFactory;

            switch (country)
            {
                case Country.India:
                    cookerFactory = new IndianCookerFactory();
                    break;
                case Country.Ukraine:
                    cookerFactory = new UkranianCookerFactory();
                    break;
                case Country.England:
                    cookerFactory = new EnglandCookerFactory();
                    break;
                default:
                    cookerFactory = new IndianCookerFactory();
                    break;
            }

            this.CookMasala(cookerFactory);
        }

        private void CookMasala(ICookerFactory cookerFactory)
        {
            cookerFactory.CookRice(cooker);
            cookerFactory.CookChicken(cooker);
        }
    }
}
