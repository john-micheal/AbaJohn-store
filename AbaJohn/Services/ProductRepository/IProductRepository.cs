using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn
{
    public interface IProductRepository
    {
        int create(productViewModel new_product_view);
        int Delete(int id);
        List<Product> get_all_product();
        List<Product> GetSellerProducts(string UserName);
         string getseller_id(string SellerName);
        int update(int id, productViewModel product);
        productViewModel get_product_byid(int id);

        int AddItemToProduct(ItemViewModel item);
        List<productViewModel> GetProductsByGender(string? GenderName);
        List<productViewModel> ProductsFilter(string? GenderName , string? Category, double? MinPrice, double? MaxPrice,string Color, string size);
        bool CheeckProductForSeller(int ProductID , string SellerName);

    }
}