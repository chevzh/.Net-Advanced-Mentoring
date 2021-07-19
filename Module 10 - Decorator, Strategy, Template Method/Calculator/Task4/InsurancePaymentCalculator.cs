using System;
using System.Collections.Generic;

namespace Calculator.Task4
{
    public class InsurancePaymentCalculator : ICalculator
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;

        public InsurancePaymentCalculator(
            ICurrencyService currencyService,
            ITripRepository tripRepository)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
        }

        public decimal CalculatePayment(string touristName)
        {
            var tripDetails = tripRepository.LoadTrip(touristName);
            var rate = currencyService.LoadCurrencyRate();

            return Constants.A * rate * tripDetails.FlyCost + Constants.B * rate * tripDetails.AccomodationCost + Constants.C * rate * tripDetails.ExcursionCost;
        }

        public class RoundingCalculatorDecorator : ICalculator
        {
            ICalculator calculator;

            public RoundingCalculatorDecorator(ICalculator calculator)
            {
                this.calculator = calculator;
            }

            public decimal CalculatePayment(string touristName)
            {
                return Decimal.Round(calculator.CalculatePayment(touristName));
            }
        }

        public class LoggingCalculatorDecorator : ICalculator
        {
            ICalculator calculator;
            ILogger logger;

            public LoggingCalculatorDecorator(ICalculator calculator, ILogger logger)
            {
                this.calculator = calculator;
                this.logger = logger;
            }

            public decimal CalculatePayment(string touristName)
            {
                logger.Log("Start");
                decimal payment = calculator.CalculatePayment(touristName);
                logger.Log("End");

                return payment;
            }
        }

        public class CachedPaymentDecorator : ICalculator
        {
            ICalculator calculator;
            static Dictionary<string, decimal> cache = new Dictionary<string, decimal>();

            public CachedPaymentDecorator(ICalculator calculator)
            {
                this.calculator = calculator;
            }

            public decimal CalculatePayment(string touristName)
            {
                decimal payment;

                if (cache.TryGetValue(touristName, out payment))
                {
                    return payment;
                }
                else
                {
                    payment = calculator.CalculatePayment(touristName);
                    cache.Add(touristName, payment);

                    return payment;
                }
            }
        }
    }
}
