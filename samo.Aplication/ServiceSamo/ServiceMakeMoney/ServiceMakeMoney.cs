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

        public async Task<ApiResult<PageResult<ListMakeMoney<MakeMoneyVm>>>> GetByUser(Guid idUser)
        {
            var query = from rmm in _context.RegisterMakeMoneys
                        join mm in _context.MakeMoneys on rmm.IdMakeMoney equals mm.Id
                        where rmm.IdUser == idUser
                        select new { rmm, mm };
            var type = query.Select(x => new {name=x.mm.Name,img=x.mm.Img }).Distinct().ToList();

            var data = type.Select(x => new ListMakeMoney<MakeMoneyVm>()
            {
                Type = x.name,
                Img = x.img,
                Data = query.Where(y => y.mm.Name == x.name).Select(n => new MakeMoneyVm()
                {
                    Id = n.rmm.Id,
                    Description = n.rmm.Description,
                    Money = n.rmm.Money,
                    DateCreate = n.rmm.DateCreate.ToString("dd-MM-yyyy")
                }).ToList()
            }).ToList();

            var result = new PageResult<ListMakeMoney<MakeMoneyVm>>()
            {
                Items = data,
                TotalRecords = data.Count()

            };

            return new ApiSuccessResult<PageResult<ListMakeMoney<MakeMoneyVm>>>(result);
        }

        public async  Task<ApiResult<PageResult<ChartVm>>> GetChartByUser(Guid idUser, int Month)
        {
            var query = from rmm in _context.RegisterMakeMoneys
                        join mm in _context.MakeMoneys on rmm.IdMakeMoney equals mm.Id
                        where rmm.IdUser == idUser && rmm.DateCreate.Month == Month
                        select new {mm,rmm};
            var data = query.ToList().GroupBy(l => l.mm.Name)
                .Select(x => new
                {
                   name = x.First().mm.Name,
                    value = x.Sum(c => c.rmm.Money)
                }).ToList();

            decimal Sum = data.Sum(x => x.value);

            var result = data.Select(x => new ChartVm()
            {
                Name = x.name,
                Value = x.value,
                Percent = (double)(x.value / Sum)*100
            }).ToList();
            var result1 = new PageResult<ChartVm>()
            {
                Items = result,
                TotalRecords = data.Count()
            };

            return new ApiSuccessResult<PageResult<ChartVm>>(result1);
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
