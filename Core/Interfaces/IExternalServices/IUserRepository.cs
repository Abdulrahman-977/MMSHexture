using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IUserRepository
    {
         Task<List<UserDropDownViewModel>> GetAll();
        Task<UserDropDownViewModel> GetById(string UserId);
        Task<UserDropDownViewModel> GetByEmailAsync(string email);
        Task<bool> Delete(string Id);
        Task<ResponseViewModel> Add(AddUserViewModel model);
        Task<ResponseViewModel> UpdateProfile(UpdateUserProfileViewModel model);
        Task<ResponseViewModel> ChangePassword(ChangePasswordViewModel model);
    }
}
