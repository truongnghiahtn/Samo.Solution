using samo.Aplication.ViewModel.Common;
using samo.Aplication.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace samo.Aplication.ServiceSamo.ServiceSpend
{
    public interface ISeviceSpend
    {
        Task<ApiResult<PageResult<ServicesVm>>> GetAll();
        Task<ApiResult<bool>> Delete(int idSpend);
        Task<ApiResult<bool>> Update(RequestUpdate request);
        Task<ApiResult<bool>> Create(RequestCreate request);
        Task<ApiResult<ServicesVm>> GetById(int IdSpend);
    }
}
