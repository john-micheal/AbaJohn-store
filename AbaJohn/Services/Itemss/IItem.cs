using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn.Services.Itemss
{
    public interface IItem
    {
        Item getItemFormItemVM(ItemViewModel itemVm);
        List<Item> GetItemsForPrudect(int ProID);
        List<Item> Get_all_items();
        bool CheekItemForProduct(int ProductID,int? itemId);
        Item Get_item_byid(int? id);
        int update_item( Item new_item);
        int Delete(int id);
    }
}
