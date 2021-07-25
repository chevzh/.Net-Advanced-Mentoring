using AbstartFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Task1.CookerFactories
{
    public class IndianCookerFactory: ICookerFactory
    {
        public void CookRice(ICooker cooker)
        {
            cooker.FryRice(200, Level.Strong);
            cooker.SaltRice(Level.Strong);
            cooker.PepperRice(Level.Strong);
        }

        public void CookChicken(ICooker cooker)
        {
            cooker.FryChicken(100, Level.Strong);
            cooker.SaltChicken(Level.Strong);
            cooker.PepperChicken(Level.Strong);
        }
    }
}
