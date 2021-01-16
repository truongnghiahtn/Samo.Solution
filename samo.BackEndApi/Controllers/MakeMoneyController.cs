using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using samo.Aplication.ServiceSamo.ServiceMakeMoney;
using samo.Aplication.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samo.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MakeMoneyController : ControllerBase
    {
        private readonly IServiceMakeMoney _ServiceMakeMoney;

        public MakeMoneyController(IServiceMakeMoney ServiceMakeMoney)
        {
            _ServiceMakeMoney = ServiceMakeMoney;
        }

        [HttpGet("All")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {

            var service = await _ServiceMakeMoney.GetAll();
            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegisters([FromBody] RequestCreate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ServiceMakeMoney.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RequestUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ServiceMakeMoney.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int idMakeMoney)
        {
            var result = await _ServiceMakeMoney.Delete(idMakeMoney);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int IdMakeMoney)
        {

            var service = await _ServiceMakeMoney.GetById(IdMakeMoney);
            return Ok(service);
        }

        [HttpGet("idUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdUSer(Guid IdUSer)
        {

            var service = await _ServiceMakeMoney.GetByUser(IdUSer);
            return Ok(service);
        }

        [HttpGet("Chart")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChart(Guid idUser, int month)
        {

            var service = await _ServiceMakeMoney.GetChartByUser(idUser, month);
            return Ok(service);
        }
    }
}
