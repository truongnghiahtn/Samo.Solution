﻿using samo.Aplication.ViewModel.Common;
using samo.Aplication.ViewModel.Register;
using samo.Aplication.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace samo.Aplication.ServiceSamo.ServiceRegister
{
    public interface IServiceRegisters
    {
        Task<ApiResult<bool>> CreateRegister(RequestRegisterCreate request);
        Task<ApiResult<bool>> UpdateRegister(RequestRegisterUpdate request);

        Task<ApiResult<bool>> Delete(int id, string type);

        Task<ApiResult<PageResult<RegisterByDate<RegisterVm>>>> GetRegister(Guid idUser);

        Task<ApiResult<RegisterDetailVm>> GetById(int idService, string type);

        Task<ApiResult<PageResult<ChartVm>>> GetChartByUser(Guid idUser, int Month);

    }
}
