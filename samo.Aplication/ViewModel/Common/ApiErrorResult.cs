using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.Common
{
    public class ApiErrorResult<T>:ApiResult<T>
    {
        public ApiErrorResult()
        {
            Message = "Err";
        }
        public ApiErrorResult(string mess)
        {
            Message = mess;
            IsSuccessed = false;
        }

    }
}
