namespace RecipeLib.Models
{
    public interface ICRUDContactRepository
    {
        Contact FindById(int id);
        Contact Add(Contact contact);

        void Delete(int id);

        Contact Update(Contact contact);

        List<Contact> FindAll();

    }
}