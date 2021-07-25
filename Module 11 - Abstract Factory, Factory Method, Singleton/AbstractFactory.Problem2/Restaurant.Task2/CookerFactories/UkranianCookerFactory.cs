using AbstartFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Task2.CookerFactories
{
    public class UkranianCookerFactory : ICookerFactory
    {
        public void CookRice(ICooker cooker)
        {
            cooker.FryRice(500, Level.Strong);
            cooker.SaltRice(Level.Strong);
            cooker.PepperRice(Level.Low);
        }

        public void CookChicken(ICooker cooker)
        {
            cooker.FryChicken(300, Level.Medium);
            cooker.SaltChicken(Level.Medium);
            cooker.PepperChicken(Level.Low);
        }
    }
}
