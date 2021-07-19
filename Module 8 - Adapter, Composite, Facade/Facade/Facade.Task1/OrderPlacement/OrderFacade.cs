using System;

namespace Facade.Task1.OrderPlacement
{
    public class OrderFacade
    {
        InvoiceSystem invoiceSystem;
        PaymentSystem paymentSystem;
        ProductCatalog productCatalog;

        public OrderFacade(InvoiceSystem invoiceSystem, PaymentSystem paymentSystem, ProductCatalog productCatalog)
        {
            this.invoiceSystem = invoiceSystem;
            this.paymentSystem = paymentSystem;
            this.productCatalog = productCatalog;
        }

        public void PlaceOrder(string productId, int quantity, string email)
        {
            Product product = productCatalog.GetProductDetails(productId);

            Payment payment = new Payment()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = quantity,
                TotalPrice = product.Price
            };

            Invoice invoice = new Invoice() { ProductId = product.Id, CustomerEmail = email, ProductName = product.Name, Quantity = quantity, TotalPrice = payment.TotalPrice };

            paymentSystem.MakePayment(payment);
            invoiceSystem.SendInvoice(invoice);
        }
    }
}
