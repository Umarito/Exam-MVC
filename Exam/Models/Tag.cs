using System.ComponentModel.DataAnnotations;

public class Tag
{
    public int Id{get;set;}
    [Required]
    public string Name{get;set;}=null!;
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
    public List<Post> Posts{get;set;}=new();
}