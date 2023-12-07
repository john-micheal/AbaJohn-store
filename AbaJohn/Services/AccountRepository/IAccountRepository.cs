using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace AbaJohn.Services.AccountRepository
{
    public interface IAccountRepository
    {
        List<IdentityRole> get_all_roles();
       Task<int> create_business_account(userViewModel userViewModel);
    }
}