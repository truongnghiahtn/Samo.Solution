using samo.Aplication.ViewModel.Common;
using samo.Aplication.ViewModel.Register;
using samo.Data.FE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using samo.Data.Entities;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace samo.Aplication.ServiceSamo.ServiceRegister
{
    public class ServiceRegisters : IServiceRegisters
    {
        private readonly samoDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ServiceRegisters(samoDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }
        public async Task<ApiResult<bool>> CreateRegister(RequestRegisterCreate request)
        {
            if((request.type != "chiphi" && request.type != "thunhap")||request.Money<=0)
            {
                return new ApiErrorResult<bool>("Đăng ký không thành công");
            }
            var user = await _userManager.FindByIdAsync((request.IdUser).ToString());
            if(user==null)
            {
                return new ApiErrorResult<bool>("Không tồn tại người dùng này ");
            }

            if (request.type=="chiphi")
            {
                var service = await _context.Spends.FindAsync(request.IdService);
                if(service==null)
                {
                    return new ApiErrorResult<bool>("Không tồn tại dịch vụ này dùng này ");
                }    
                var registerSpend = new RegisterSpend()
                {
                    IdUser = request.IdUser,
                    IdSpend = request.IdService,
                    Money = request.Money,
                    Description = request.Description,
                    DateCreate = DateTime.UtcNow.AddHours(7)
                };
                user.AccountBalance -= request.Money;
                _context.RegisterSpends.Add(registerSpend);
            }
            if (request.type == "thunhap")
            {
                var service = await _context.MakeMoneys.FindAsync(request.IdService);
                if (service == null)
                {
                    return new ApiErrorResult<bool>("Không tồn tại dịch vụ này dùng này ");
                }
                var registerMakeMoney = new RegisterMakeMoney()
                {
                    IdUser = request.IdUser,
                    IdMakeMoney = request.IdService,
                    Money = request.Money,
                    Description = request.Description,
                    DateCreate = DateTime.UtcNow.AddHours(7)
                };
                user.AccountBalance += request.Money;
                _context.RegisterMakeMoneys.Add(registerMakeMoney);
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int idService, string type)
        {
            if (type != "chiphi" && type != "thunhap")
            {
                return new ApiErrorResult<bool>("Xóa không thành công");
            }
            if (type == "chiphi")
            {
                var data = await _context.RegisterSpends.FindAsync(idService);
                if (data == null)
                {
                    return new ApiErrorResult<bool>("không thành công");
                }
                var idUser = data.IdUser;
                var money = data.Money;
                var user = await _userManager.FindByIdAsync(idUser.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>("Không tồn tại người dùng này ");
                }
                user.AccountBalance += money;

                _context.RegisterSpends.Remove(data);
            }

            if (type == "thunhap")
            {
                var data = await _context.RegisterMakeMoneys.FindAsync(idService);
                if (data == null)
                {
                    return new ApiErrorResult<bool>("không thành công");
                }
                var idUser = data.IdUser;
                var money = data.Money;
                var user = await _userManager.FindByIdAsync(idUser.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>("Không tồn tại người dùng này ");
                }
                user.AccountBalance -= money;
                _context.RegisterMakeMoneys.Remove(data);
            }

            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<RegisterDetailVm>> GetById(int idService, string type)
        {
            if (type != "chiphi" && type != "thunhap")
            {
                return new ApiErrorResult<RegisterDetailVm>("Không tìm thấy");
            }
            if (type == "chiphi")
            {
                var registerSpend = await _context.RegisterSpends.FindAsync(idService);
                var Spend = await _context.Spends.FindAsync(registerSpend.IdSpend);
                if (registerSpend == null)
                {
                    return new ApiErrorResult<RegisterDetailVm>("không Tìm thấy");
                }

                var data = new RegisterDetailVm()
                {
                    Id = registerSpend.Id,
                    Name = Spend.Name,
                    Description = registerSpend.Description,
                    Img = Spend.Img,
                    Money = registerSpend.Money,
                    Type = Spend.Description,
                    DateCreate = registerSpend.DateCreate,
                    DateCreateToString = registerSpend.DateCreate.ToString(),
                };

                return new ApiSuccessResult<RegisterDetailVm>(data);

            }
            var registerMakeMoney = await _context.RegisterMakeMoneys.FindAsync(idService);
            var makeMoney = await _context.MakeMoneys.FindAsync(registerMakeMoney.IdMakeMoney);
            if (registerMakeMoney == null)
            {
                return new ApiErrorResult<RegisterDetailVm>("không Tìm thấy");
            }

            var registerDetailVm = new RegisterDetailVm()
            {
                Id = registerMakeMoney.Id,
                Name = makeMoney.Name,
                Description = registerMakeMoney.Description,
                Img = makeMoney.Img,
                Money = registerMakeMoney.Money,
                Type = makeMoney.Description,
                DateCreate = registerMakeMoney.DateCreate,
                DateCreateToString = registerMakeMoney.DateCreate.ToString(),
            };

            return new ApiSuccessResult<RegisterDetailVm>(registerDetailVm);
        }

        public async Task<ApiResult<PageResult<RegisterByDate<RegisterVm>>>> GetRegister(Guid idUser)
        {

            //Tien hanh lay du lieu tu database

            var registerMakeMoney = from rmm in _context.RegisterMakeMoneys
                                    join mm in _context.MakeMoneys on rmm.IdMakeMoney equals mm.Id
                                    where rmm.IdUser == idUser
                                    select new RegisterVm()
                                    {
                                        Id = rmm.Id,
                                        Name = mm.Name,
                                        Img = mm.Img,
                                        Money = rmm.Money,
                                        Type = mm.Description,
                                        DateCreate = rmm.DateCreate,
                                        DateCreateToString = rmm.DateCreate.ToString(),
                                    };

            var registerSpend = from rs in _context.RegisterSpends
                                join s in _context.Spends on rs.IdSpend equals s.Id
                                where rs.IdUser == idUser
                                select new RegisterVm()
                                {
                                    Id = rs.Id,
                                    Name = s.Name,
                                    Img = s.Img,
                                    Money = rs.Money,
                                    Type = s.Description,
                                    DateCreate = rs.DateCreate,
                                    DateCreateToString = rs.DateCreate.ToString(),
                                };

            List<RegisterVm> myList = new List<RegisterVm>();
            myList.AddRange(registerMakeMoney);
            myList.AddRange(registerSpend);

            var date = myList.Select(x => x.DateCreate.Date).Distinct();

            if (myList.Count == 0)
            {
                return new ApiErrorResult<PageResult<RegisterByDate<RegisterVm>>>("Khong co thong tin ");
            }
            var data =  date.Select(x => new RegisterByDate<RegisterVm>()
            {
                DateCreate = x.Date,
                Data = myList.Where(y => y.DateCreate > x.Date && y.DateCreate <= x.Date.AddDays(1)).Select(n => new RegisterVm()
                {
                    Id = n.Id,
                    Name = n.Name,
                    Img = n.Img,
                    Money = n.Money,
                    Type = n.Type,
                    DateCreate = n.DateCreate,
                    DateCreateToString = n.DateCreateToString,
                }).ToList()
            }).ToList();

            var updatedata = new PageResult<RegisterByDate<RegisterVm>>()
            {
                Items = data,
                TotalRecords = data.Count(),
            };

            return new ApiSuccessResult<PageResult<RegisterByDate<RegisterVm>>>(updatedata);
        }

  

        public async Task<ApiResult<bool>> UpdateRegister(RequestRegisterUpdate request)
        {
            if ((request.type != "chiphi" && request.type != "thunhap")||request.Money<=0)
            {
                return new ApiErrorResult<bool>("Chỉnh sữa  không thành công");
            }

            //Tien hanh update
            if (request.type == "chiphi")
            {

                var registerSpend = await _context.RegisterSpends.FindAsync(request.Id);
                if (registerSpend == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy nội dung");
                }
                var Service = await _context.Spends.FindAsync(request.IdService);
                if(Service==null)
                {
                    return new ApiErrorResult<bool>("Khong tim thay dich vu nay");
                }
                var money = registerSpend.Money;    
                registerSpend.Money = request.Money;
                registerSpend.Description = request.Description;
                registerSpend.IdSpend = request.IdService;

                var idUser = registerSpend.IdUser;
                
                var user = await _userManager.FindByIdAsync(idUser.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>("Không tồn tại người dùng này ");
                }
                user.AccountBalance = (user.AccountBalance + money) - request.Money;
            }
            if (request.type == "thunhap")
            {
                var registerMakeMoney = await _context.RegisterMakeMoneys.FindAsync(request.Id);
                if (registerMakeMoney == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy nội dung");
                }
                var Service = await _context.MakeMoneys.FindAsync(request.IdService);
                if (Service == null)
                {
                    return new ApiErrorResult<bool>("Khong tim thay dich vu nay");
                }
                var money = registerMakeMoney.Money;
                registerMakeMoney.Money = request.Money;
                registerMakeMoney.Description = request.Description;
                registerMakeMoney.IdMakeMoney = request.IdService;

                var idUser = registerMakeMoney.IdUser;
                
                var user = await _userManager.FindByIdAsync(idUser.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>("Không tồn tại người dùng này ");
                }
                user.AccountBalance = (user.AccountBalance - money) + request.Money;
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();

        }
    }
}
