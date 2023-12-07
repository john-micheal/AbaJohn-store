using AbaJohn.Models;

using AbaJohn.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AbaJohn
{
    public class ProductRepository : IProductRepository
    {
       private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;

      

        public ProductRepository(ApplicationDbContext _context, IMapper mapper)
        {
            context = _context;
            _mapper = mapper;
        }
        public string getseller_id(string SellerName) { 
 

            return context.Users.FirstOrDefault(x => x.UserName == SellerName)?.Id;
        }
        //CURD
        public List<Product> get_all_product()
        {
            return context.products.Include(x=>x.category).Include(i=>i.images).ToList();
         
        }
        public List<Product> GetSellerProducts(string SellerName)
        {
            var SellerID = context.Users.FirstOrDefault(x => x.UserName == SellerName)?.Id;
            return context.products.Where(c => c.SellerID == SellerID).Include(c => c.category).Include(i => i.images).ToList();
        }
        public List<productViewModel> GetProductsByGender(string GenderName)
        {
           var productlst = context.products
                .Where(p => p.prodeuctGender.ToLower() == GenderName.ToLower())
                .Include(I => I.images).Include(c => c.category)
                .ToList();

            var data = _mapper.Map<List<productViewModel>>(productlst);
           
            return data;
        }
        public List<productViewModel> ProductsFilter(string? GenderName, string? Category, double? MinPrice, double? MaxPrice, string Color, string size)
        {
            if (Color == null) { Color = ""; }
            if (size == null) { size = ""; }
            if (Category == null) { Category = ""; }
            if (GenderName == null) { GenderName = ""; }


            var productlst = context.products.Include(I => I.images).Include(c => c.category).Include(i => i.Items)
                .Where(p => (p.price >= MinPrice && p.price <= MaxPrice) || (MinPrice == 0 && MaxPrice == 0) || (MinPrice == null && MaxPrice == null))
                .Where(G => (!string.IsNullOrEmpty(G.prodeuctGender) && G.prodeuctGender.ToLower() == GenderName.ToLower()) || string.IsNullOrEmpty(GenderName))
                .Where(p => (!string.IsNullOrEmpty(p.category.Name) && p.category.Name.ToLower() == Category.ToLower() ) || string.IsNullOrEmpty(Category))
                .Where(p => p.Items.Any(c=> !string.IsNullOrEmpty(c.size)&& c.size.ToLower() == size.ToLower()) || string.IsNullOrEmpty(size))
                .Where(p => p.Items.Any(c=> !string.IsNullOrEmpty(c.Color)&& c.Color == Color) || string.IsNullOrEmpty(Color))
                .ToList();
            var DataAfterFilter = _mapper.Map<List<productViewModel>>(productlst);

            return DataAfterFilter;
        }

        public productViewModel get_product_byid(int id)
        {
            productViewModel product_vw = new productViewModel();
            Product product = context.products.FirstOrDefault(x => x.ID == id);

            ProductImage product_images = context.productImages.FirstOrDefault(c => c.Product_id == id);
            if(product != null && product_images != null)
            {
                product_vw.Name = product.Name;
                product_vw.price = product.price;
               
                product_vw.Code = product.Code;
                product_vw.title = product.title;
                product_vw.Description = product.Description;
                product_vw.prodeuctGender = product.prodeuctGender;
                product_vw.category_id = product.CategoryID;
                product_vw.BaseImg = product_images.BaseImg;
                product_vw.Img1 = product_images.Img1;
                product_vw.Img2 = product_images.Img2;
                product_vw.Img3 = product_images.Img3;
                return product_vw;
            }



            return null;

        }
        public int AddItemToProduct(ItemViewModel ProductItems)
        {
            var item = _mapper.Map<Item>(ProductItems);
            context.item.Add(item);
            return context.SaveChanges();
        }

        public bool CheeckProductForSeller(int ProductID, string SellerName)
        {
            var productList = GetSellerProducts(SellerName).Select(x=>x.ID);
           if(productList.Contains(ProductID))
                 return true;
           else return false;
        }
        public string GenerateUniqueImageName()
        {
            // Get the current date and time
            DateTime now = DateTime.Now;

            // Generate a unique name using a combination of timestamp and random number
            string uniqueName = $"{now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";

            // Return the unique name
            return uniqueName;
        }

        public int update(int id, productViewModel Newproduct)
        {
            Product Old_product = context.products.FirstOrDefault(s => s.ID == id);
            ProductImage new_product_imag = context.productImages.FirstOrDefault(s => s.Id == id);

            string baseImgFileName = "";
            string img1FileName = "";
            string img2FileName = "";
            string img3FileName = "";

            Old_product.Name = Newproduct.Name;
            Old_product.price = Newproduct.price;
            Old_product.Code = Newproduct.Code;
            Old_product.title = Newproduct.title;
            Old_product.Description = Newproduct.Description;
            Old_product.CategoryID = Newproduct.category_id;
            Old_product.prodeuctGender = Newproduct.prodeuctGender;

            context.SaveChanges();



            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/productImg");

                // Create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (Newproduct.BaseFileImg != null)
                {
                    // Generate a unique file name for BaseImg
                    baseImgFileName = GenerateUniqueImageName() + Path.GetExtension(Newproduct.BaseFileImg.FileName);
                    string baseImgFilePath = Path.Combine(path, baseImgFileName);

                    using (var stream = new FileStream(baseImgFilePath, FileMode.Create))
                    {
                        Newproduct.BaseFileImg.CopyTo(stream);
                    }
                    string deletepath = path + "/" + Newproduct.BaseImg;
                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.BaseImg = baseImgFileName;
                }

                if (Newproduct.Img1File != null)
                {
                    // Generate a unique file name for Img1
                    img1FileName = GenerateUniqueImageName() + Path.GetExtension(Newproduct.Img1File.FileName);
                    string img1FilePath = Path.Combine(path, img1FileName);

                    using (var stream = new FileStream(img1FilePath, FileMode.Create))
                    {
                        Newproduct.Img1File.CopyTo(stream);
                    }
                    string deletepath = path + "/" + Newproduct.Img1;
                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.Img1 = img1FileName;
                }

                if (Newproduct.Img2File != null)
                {
                    // Generate a unique file name for Img2
                    img2FileName = GenerateUniqueImageName() + Path.GetExtension(Newproduct.Img2File.FileName);
                    string img2FilePath = Path.Combine(path, img2FileName);

                    using (var stream = new FileStream(img2FilePath, FileMode.Create))
                    {
                        Newproduct.Img2File.CopyTo(stream);
                    }

                    string deletepath = path + "/" + Newproduct.Img2;

                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.Img2 = img2FileName;
                }

                if (Newproduct.Img3File != null)
                {
                    // Generate a unique file name for Img3
                    img3FileName = GenerateUniqueImageName() + Path.GetExtension(Newproduct.Img3File.FileName);
                    string img3FilePath = Path.Combine(path, img3FileName);

                    using (var stream = new FileStream(img3FilePath, FileMode.Create))
                    {
                        Newproduct.Img3File.CopyTo(stream);
                    }
                    string deletepath = path + "/" + Newproduct.Img3;
                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.Img3 = img3FileName;
                }


                int updateCount = context.SaveChanges();
                return updateCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                // Handle the exception or log the error here
            }
            return 0;
        }
        public int create(productViewModel new_product_view)
        {
            string baseImgFileName  = "";
            string img1FileName     = "";
            string img2FileName     = "";
            string img3FileName     = "";
            Product new_product = new Product();
            ProductImage productImage = new ProductImage();
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/productImg");

                // Create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (new_product_view.BaseFileImg != null)
                {
                    // Generate a unique file name for BaseImg
                    baseImgFileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.BaseFileImg.FileName);
                    string baseImgFilePath = Path.Combine(path, baseImgFileName);

                    using (var stream = new FileStream(baseImgFilePath, FileMode.Create))
                    {
                        new_product_view.BaseFileImg.CopyTo(stream);
                    }
                    productImage.BaseImg = baseImgFileName;
                }

                if (new_product_view.Img1File != null)
                {
                    // Generate a unique file name for Img1
                    img1FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img1File.FileName);
                    string img1FilePath = Path.Combine(path, img1FileName);

                    using (var stream = new FileStream(img1FilePath, FileMode.Create))
                    {
                        new_product_view.Img1File.CopyTo(stream);
                    }
                    productImage.Img1 = img1FileName;
                }

                if (new_product_view.Img2File != null)
                {
                    // Generate a unique file name for Img2
                    img2FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img2File.FileName);
                    string img2FilePath = Path.Combine(path, img2FileName);

                    using (var stream = new FileStream(img2FilePath, FileMode.Create))
                    {
                        new_product_view.Img2File.CopyTo(stream);
                    }
                    productImage.Img2 = img2FileName;
                }

                if (new_product_view.Img3File != null)
                {
                    // Generate a unique file name for Img3
                    img3FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img3File.FileName);
                    string img3FilePath = Path.Combine(path, img3FileName);

                    using (var stream = new FileStream(img3FilePath, FileMode.Create))
                    {
                        new_product_view.Img3File.CopyTo(stream);
                    }
                    productImage.Img3 = img3FileName;
                }

                new_product.Name           = new_product_view.Name;
                new_product.price          = new_product_view.price;
                new_product.Code           = new_product_view.Code;
                new_product.title          = new_product_view.title;
                new_product.Description    = new_product_view.Description;
                new_product.prodeuctGender = new_product_view.prodeuctGender;
                new_product.CategoryID     = new_product_view.category_id;
                new_product.SellerID = new_product_view.seller_id;

                context.products.Add(new_product);

                context.SaveChanges();

                var pro_id = new_product.ID;
                productImage.Product_id = pro_id;





                context.productImages.Add(productImage);

                int product = context.SaveChanges();
                return product;

                // Commit the transaction if everything succeeds
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            return 0;
        }

        public int Delete(int id)
        {
            Product product = context.products.FirstOrDefault(s => s.ID == id);
            ProductImage product_imag = context.productImages.FirstOrDefault(s => s.Product_id == id);
            var items = context.item.Where(s => s.productID == id).ToList();
            
            
            context.Remove(product);
            context.Remove(product_imag);
            foreach (var item in items)
            {
                context.Remove(item);
            }
           

            int delete = context.SaveChanges();
            return delete;
        }

  
    }
}