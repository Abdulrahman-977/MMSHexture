using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IIdentityRepository
    {
        Task<TokenViewModel> Login(LoginViewModel login);
        Task<bool> ForgetPasswordAsync(ForgetPasswordViewModel model);
        Task<bool> ResetPassword(ResetPasswordViewModel model);
        //Task<AddUserViewModel>

    }
}
