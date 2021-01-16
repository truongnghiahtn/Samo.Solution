using samo.Aplication.ViewModel.Common;
using samo.Data.FE;
using samo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using samo.Aplication.ViewModel.Service;

namespace samo.Aplication.ServiceSamo.ServiceMakeMoney
{
    public class ServiceMakeMoney : IServiceMakeMoney
    {
        private readonly samoDbContext _context;
        public ServiceMakeMoney(samoDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(RequestCreate request)
        {
            var makeMoney = new MakeMoney()
            {
                Name = request.Name,
                Description = request.Description,
                Img = request.Name,
                Status = true,
            };
            _context.MakeMoneys.Add(makeMoney);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
             
        }

        public async Task<ApiResult<bool>> Delete(int idMakeMoney)
        {
       
            var makeMoney = await _context.MakeMoneys.FindAsync(idMakeMoney);
            if (makeMoney == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.MakeMoneys.Remove(makeMoney);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PageResult<ServicesVm>>> GetAll()
        {
            var query = from m in _context.MakeMoneys
                        select new { m };
            int totalrow = await query.CountAsync();

            var data = await query.Select(x => new ServicesVm()
            {
                Id = x.m.Id,
                Name = x.m.Name,
                Description = x.m.Description,
                Img = x.m.Img,
                Status = x.m.Status
            }).ToListAsync();

            var pageResult = new PageResult<ServicesVm>()
            {
                TotalRecords = totalrow,
                Items = data
            };

            return new ApiSuccessResult<PageResult<ServicesVm>>(pageResult);
        }

        public async Task<ApiResult<ServicesVm>> GetById(int idMakeMoney)
        {
            var makeMoney  = await _context.MakeMoneys.FindAsync(idMakeMoney);
            if(makeMoney == null)
            {
                return new ApiErrorResult<ServicesVm>("Không tìm thấy");
            }

            var data = new ServicesVm()
            {
                Id = makeMoney.Id,
                Name = makeMoney.Name,
                Description = makeMoney.Description,
                Img = makeMoney.Img,
                Status = makeMoney.Status
            };

            return new ApiSuccessResult<ServicesVm>(data);
        }

        public async Task<ApiResult<bool>> Update(RequestUpdate request)
        {
            var makeMoney = await _context.MakeMoneys.FindAsync(request.Id);
            if (makeMoney == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }

            makeMoney.Name = request.Name;
            makeMoney.Description = request.Description;
            makeMoney.Img = request.Img;
            makeMoney.Status = request.Status;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }
    }
}
