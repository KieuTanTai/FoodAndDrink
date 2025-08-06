namespace ProjectShop.Server.Core.Interfaces.IServices.Transaction.Invoice
{
    public interface IDiscountCodeForInvoiceService<T, TKeys> : IBaseLinkingDataService<T, TKeys> where T : class where TKeys : class
    {
    }
}
