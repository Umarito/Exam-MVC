public interface IPostRepo
{
    Task<Post> Add(Post Post);
    Task<List<Post>> GetAll();
    Task<Post> GetById(int id);
    Task Update(Post Post);
    Task<string> Delete(int id);
}