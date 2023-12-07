using AbaJohn.Models;
using AbaJohn.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AbaJohn.Services.Itemss
{
    public class ItemRepository : IItem
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public  Item getItemFormItemVM(ItemViewModel itemVm)
        {
            Item item = _mapper.Map<Item>(itemVm);
            item.ID = (int)itemVm.ID;

            return item;
        }
        public List<Item> GetItemsForPrudect(int ProID)
        {
            
            var Items = _context.item.Where(i => i.productID == ProID).Include(c => c.Product).ToList();

          
            return Items;
        }
        public List<Item> Get_all_items()
        {
            return _context.item.ToList();
        }

        public Item Get_item_byid(int? id)
        {
            return _context.item.Include(c=>c.Product).FirstOrDefault(x => x.ID == id);
        }

        public int update_item(Item new_item)
        {
            Item item = _context.item.FirstOrDefault(x => x.ID == new_item.ID);

            item.size = new_item.size;
            item.Color = new_item.Color;
            item.Quantity = new_item.Quantity;



            int update_item = _context.SaveChanges();
            return update_item;

        }
        public int Delete(int id)
        {
            Item item = _context.item.FirstOrDefault(s => s.ID == id);
            _context.item.Remove(item);
            int delete = _context.SaveChanges();
            return delete;
        }

        public bool CheekItemForProduct(int ProductID, int? itemId)
        {
            var productID = Get_item_byid(itemId).productID;
            if (productID == ProductID)
            {
                return true;
            }
            else
                return false;

        
        }

    }
}
