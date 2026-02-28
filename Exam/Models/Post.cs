using System.ComponentModel.DataAnnotations;

public class Post
{
    public int Id{get;set;}
    [Required][MaxLength(100)]
    public string Title{get;set;}=null!;
    [MaxLength(500)]
    public string? Text{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
    public List<Tag> Tags{get;set;}=new();
}