namespace AZ.Domain.Entities;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int Discount { get; set; }
    public string? Description { get; set; }
    public string? ImgPath { get; set; }
    public decimal Rating { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public int? SellerId { get; set; }
    public Seller? Seller { get; set; }
}
