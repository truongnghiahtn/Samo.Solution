using samo.Aplication.ViewModel.Common;
using samo.Data.FE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using samo.Aplication.ViewModel.Service;
using samo.Data.Entities;

namespace samo.Aplication.ServiceSamo.ServiceSpend
{
    public class ServiceSpend : ISeviceSpend
    {
        private readonly samoDbContext _context;
        public ServiceSpend(samoDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(RequestCreate request)
        {
            var spend = new Spend()
            {
                Name = request.Name,
                Description = request.Description,
                Img = request.Img,
                Status = true,
            };

            _context.Spends.Add(spend);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();

        }

        public async Task<ApiResult<bool>> Delete(int idSpend)
        {
            var spend = await _context.Spends.FindAsync(idSpend);
            if(spend==null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }

            _context.Spends.Remove(spend);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PageResult<ServicesVm>>> GetAll()
        {
            var query = from sp in _context.Spends
                        select new { sp };

            int toltalRow = await query.CountAsync();

            var data = await query.Select(x => new ServicesVm()
            {
                Id = x.sp.Id,
                Name = x.sp.Name,
                Description = x.sp.Description,
                Img = x.sp.Img,
                Status = x.sp.Status
            }).ToListAsync();

            var pageResult = new PageResult<ServicesVm>()
            {
                Items = data,
                TotalRecords = toltalRow
            };
            return new ApiSuccessResult<PageResult<ServicesVm>>(pageResult);
        }

        public async Task<ApiResult<ServicesVm>> GetById(int IdSpend)
        {
            var spend = await _context.Spends.FindAsync(IdSpend);
            if (spend == null)
            {
                return new ApiErrorResult<ServicesVm>("Không tìm thấy");
            }
            var data = new ServicesVm()
            {
                Id = spend.Id,
                Name = spend.Name,
                Description = spend.Description,
                Img = spend.Img,
                Status = spend.Status
            };

            return new ApiSuccessResult<ServicesVm>(data);
        }

        public async Task<ApiResult<PageResult<ListSpend<SpendVm>>>> GetByUser(Guid idUser)
        {
            var query = from rs in _context.RegisterSpends
                                join s in _context.Spends on rs.IdSpend equals s.Id
                                where rs.IdUser == idUser
                                select new { rs, s };
            var type= query.Select(x => new {name=x.s.Name,img=x.s.Img }).Distinct().ToList();

            var data = type.Select(x => new ListSpend<SpendVm>()
            {
                Type = x.name,
                Img = x.img,
                Data = query.Where(y => y.s.Name == x.name).Select(n => new SpendVm()
                {
                    Id = n.rs.Id,
                    Description = n.rs.Description,
                    Money = n.rs.Money,
                    DateCreate = n.rs.DateCreate.ToString("dd-MM-yyyy")
                }).ToList()
            }).ToList();

            var result = new PageResult<ListSpend<SpendVm>>()
            {
                Items = data,
                TotalRecords = data.Count()
            };

            return new ApiSuccessResult<PageResult<ListSpend<SpendVm>>>(result);
        }

        public async Task<ApiResult<PageResult<ChartVm>>> GetChartByUser(Guid idUser, int Month)
        {
            var query = from rs in _context.RegisterSpends
                        join s in _context.Spends on rs.IdSpend equals s.Id
                        where rs.IdUser == idUser && rs.DateCreate.Month==Month   
                        select new { name = s.Name, value = rs.Money };
            var data = query.ToList().GroupBy(l => l.name)
                 .Select(x => new
                 {
                     name = x.First().name,
                     value = x.Sum(c => c.value)
                 }).ToList();

            decimal Sum= data.Sum(x=>x.value);

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
            var spend = await _context.Spends.FindAsync(request.Id);
            if(spend==null)
            {
                return new ApiErrorResult<bool>("Khong tim thay");
            }
            spend.Name = request.Name;
            spend.Description = request.Description;
            spend.Img = request.Img;
            spend.Status = request.Status;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }
    }
}
