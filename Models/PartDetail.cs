namespace Engage.Keyloop.Api.Models;

public class PartDetail : Part
{
    public ListPrice ListPrice { get; set; }

    public int UnitOfSale { get; set; }
}

public class ListPrice
{
    public decimal NetValue { get; set; }
    public decimal GrossValue { get; set; }

    public decimal TaxValue { get; set; }

    public decimal TaxRate { get; set; }
    public string CurrencyCode { get; set; }
}