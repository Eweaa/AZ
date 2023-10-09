namespace AZ.Domain.Entities;
public class Address
{
    public int Id { get; set; }
    public string? StreetName { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public int BuildingNumber { get; set; }
    public int ApartmentNumber { get; set; }
}
