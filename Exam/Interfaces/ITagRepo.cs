public interface ITagRepo
{
    Task<Tag> Add(Tag Tag);
    Task<List<Tag>> GetAll();
    Task<Tag> GetById(int id);
    Task Update(Tag Tag);
    Task<string> Delete(int id);
}