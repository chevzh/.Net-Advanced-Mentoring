using AbstartFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Task2.CookerFactories
{
    public class SummerUkranianCookerFactory : ICookerFactory
    {
        public void CookRice(ICooker cooker)
        {
            cooker.FryRice(150, Level.Medium);
            cooker.SaltRice(Level.Low);
        }

        public void CookChicken(ICooker cooker)
        {
            cooker.FryChicken(200, Level.Medium);
            cooker.SaltChicken(Level.Low);
        }
    }
}
