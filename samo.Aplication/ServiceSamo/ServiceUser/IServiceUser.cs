using samo.Aplication.ViewModel.Common;
using samo.Aplication.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace samo.Aplication.ServiceSamo.ServiceUser
{
    public interface IServiceUser
    {
        Task<ApiResult<ResultLogin>> LoginUser(LoginUser request);
        Task<ApiResult<bool>> RegisterUser(RequestUser request);
        Task<ApiResult<UserVm>> GetUserById(string userName);
    }
}
