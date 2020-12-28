using samo.Aplication.ViewModel.Common;
using samo.Aplication.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace samo.Aplication.ServiceSamo.ServiceMakeMoney
{
    public interface IServiceMakeMoney
    {
        Task<ApiResult<PageResult<ServicesVm>>> GetAll();
        Task<ApiResult<bool>> Delete(int idMakeMoney);
        Task<ApiResult<bool>> Update(RequestUpdate request);
        Task<ApiResult<bool>> Create(RequestCreate  request);
        Task<ApiResult<ServicesVm>> GetById(int IdMakeMoney);

    }
}
