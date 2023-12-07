using AbaJohn.Models;
using AbaJohn.Services.Itemss;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace AbaJohn.Controllers
{
    public class ItemController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;
        private readonly IItem itemRepository;

        public ItemController(IProductRepository _productRepository, IcategoeryRepository _categoeryRepository, IItem _item)
        {

            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
            itemRepository = _item;
        }
        [HttpGet]
        public IActionResult ShowItemsForProdcut(int ProductID)
        {
            if (ProductID == null|| ProductID==0)
            {
                ProductID = (int)TempData["ProductID"];
            }
            // check if the product form sellerProduct List or not 
            var username = User.Identity?.Name;
            var result = productRepository.CheeckProductForSeller(ProductID, username);
            var items = itemRepository.GetItemsForPrudect(ProductID);
    
            if (result)
            {
                int totalQuantity=0;
                foreach (var item in items)
                {
                    totalQuantity += item.Quantity;
                }
                ViewBag.TotalQuantity = totalQuantity;
                ItemViewModel Items = new ItemViewModel()
                {
                    // get product by image 
                    Product = productRepository.get_product_byid(ProductID),
                    Colors = Colors_and_Sizes.getcolors(),
                    Sizes = Colors_and_Sizes.getSizes(),

                    productID = ProductID,
                    Items= items,
                    ReturnUrl= "ShowItemsForProdcut"

                };
                return View(Items);
            }else
            {
                TempData["massege"] = " متحاولش تاني كفايا وفر وقتك ";
                return RedirectToAction("ShowProductSeller", "product");
            }
  
        }
        [HttpGet]
        public IActionResult AddItemToProduct(int product_id)
        {
            ViewBag.Message = "";
            // check if the product form sellerProduct List or not 
            var username = User.Identity?.Name;
            var result = productRepository.CheeckProductForSeller(product_id, username);
            if (result)
            {
                ItemViewModel ProductWithItems = new ItemViewModel()
                {
                    // get product by image 
                    Product = productRepository.get_product_byid(product_id),
                    Colors = Colors_and_Sizes.getcolors(),
                    Sizes = Colors_and_Sizes.getSizes(),
                   
                    productID = product_id,
                    ReturnUrl= "AddItemToProduct"

                };
                return View(ProductWithItems);
            }
            else
            {
                  TempData["massege"] = "لم نفسك عيييب عيب ";
                return RedirectToAction("ShowProductSeller", "product");
            }
               


        }
        //_________________________________________________________
        [HttpPost]
        public IActionResult AddItemToProduct(ItemViewModel Item)
        {
            var username = User.Identity?.Name;
            var result = productRepository.CheeckProductForSeller(Item.productID, username);
            if(result)
            {
                productRepository.AddItemToProduct(Item);
                var item = itemRepository.GetItemsForPrudect(Item.productID);
             
                ViewBag.Message = "Item Successfully Added";
                ItemViewModel ProductWithItems = new ItemViewModel()
                {
                    // get product by image 
                    Product = productRepository.get_product_byid(Item.productID),
                    Colors = Colors_and_Sizes.getcolors(),
                    Sizes = Colors_and_Sizes.getSizes(),
                    productID = Item.productID,
                    Items = item,
                    ReturnUrl = Item.ReturnUrl

                };
                return View(Item.ReturnUrl,ProductWithItems);
            }
            else
            {
                TempData["massege"] = " العب غيرها يابن النصابه";
                return RedirectToAction("ShowProductSeller", "product");
            }
         

        }
       
        public IActionResult Edit_item(int ID,int ProductID)
        {
            TempData["massege"] = "لم نفسك عيييب عيب ";
            var username = User.Identity?.Name;

            var resultforproduct = productRepository.CheeckProductForSeller(ProductID, username);
            if (resultforproduct)
            {
                // cheek items for product 
                var reusltForItem = itemRepository.CheekItemForProduct(ProductID, ID);
                if(reusltForItem)
                {
                    var item = itemRepository.Get_item_byid(ID);
                    ItemViewModel model = new ItemViewModel
                    {
                        Colors = Colors_and_Sizes.getcolors(),
                        Sizes = Colors_and_Sizes.getSizes(),
                        productID = item.productID,
                        ID = item.ID,
                        Quantity = item.Quantity,
                        size =     item.size,
                        Color =    item.Color,
                    };
                 
                    return View(model);
                }else
                    return RedirectToAction("ShowProductSeller", "product");
            }
            else
            {
               
                return RedirectToAction("ShowProductSeller", "product");
            }
          
        }
        [HttpPost]
        public IActionResult Edit_item(ItemViewModel NewItem)
        {
            TempData["massege"] = "لم نفسك عيييب عيب ";
            var username = User.Identity?.Name;

            var resultforproduct = productRepository.CheeckProductForSeller(NewItem.productID, username);
            if (resultforproduct)
            {
                // cheek items for product 
                var reusltForItem = itemRepository.CheekItemForProduct(NewItem.productID, NewItem.ID);
                if (reusltForItem)
                {
                    var item = itemRepository.getItemFormItemVM(NewItem);
                    itemRepository.update_item(item);
                    TempData["ProductID"] = NewItem.productID;
                    return RedirectToAction("ShowItemsForProdcut");
                }
                else
                    return RedirectToAction("ShowProductSeller", "product");
            }
            else
            {

                return RedirectToAction("ShowProductSeller", "product");
            }
 
        }


        public IActionResult DeleteItem(int ItemId ,int ProductID)
        {
            try
            {
                itemRepository.Delete(ItemId);
                TempData["ProductID"] = ProductID;
                return RedirectToAction("ShowItemsForProdcut");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);

                return RedirectToAction("Index", "Home");
            }

           
        }
    }
}
