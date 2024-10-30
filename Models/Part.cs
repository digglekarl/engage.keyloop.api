namespace Engage.Keyloop.Api.Models;

public class Part
{
    public string PartId { get; set; }
    public int PartCode { get; set; }

    public string BrandCode { get; set; }
    public string Description { get; set; }

    public AlternativePart? AlternativePart { get; set; }
}

public class AlternativePart
{
    public Part Parts { get; set; }
}