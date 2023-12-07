using AbaJohn.Models;
using AbaJohn;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AbaJohn
{
    public class categoeryRepository : IcategoeryRepository
    {
        private readonly ApplicationDbContext context;

        public categoeryRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        //CURD
        public List<Category> get_all()
        {
            return context.categories.ToList();
        }
        public Category get_category_id(int id)
        {
            return context.categories.FirstOrDefault(s => s.Id == id);
        }

        public int create(categoeryViewModel new_category_view)
        {
            Category new_category = new Category();
            new_category.Name = new_category_view.Name;
            new_category.Description = new_category_view.Description;
            new_category.type = new_category_view.type;

            context.Add(new_category);
            int create = context.SaveChanges();
            return create;
        }

        public int update(int id, Category old_category)
        {
            Category new_category = context.categories.FirstOrDefault(s => s.Id == id);

            new_category.Name = old_category.Name;
            new_category.Description = old_category.Description;
            new_category.type = old_category.type;

            int update = context.SaveChanges();
            return update;

        }

        public int Delete(int id)
        {
            Category category = context.categories.FirstOrDefault(s => s.Id == id);
            context.categories.Remove(category);
            int delete = context.SaveChanges();
            return delete;
        }


    }
}