using AbaJohn.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AbaJohn.ViewModel
{
    public class ProductListVM_Paging
    {
        public List<productViewModel>? products { get; set; }
        public int CurrentPage { get; set; }

        public int NoOfRecordPerPage = 2;
        
        public int NoOfPage()
        { 
            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products?.Count) / Convert.ToDouble(NoOfRecordPerPage)));
        }
        public  int NoOfRecordToSkip()
        { 
            return (CurrentPage - 1) * NoOfRecordPerPage; 
        }

        public List<productViewModel>? CalculateData()
        {
            int NoOfRecordToSkip = (CurrentPage - 1) * NoOfRecordPerPage;

            return products?.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
        }
        public string? ProductGender { get; set; }
    }
}
