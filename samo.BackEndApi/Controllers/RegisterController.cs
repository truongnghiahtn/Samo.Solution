using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using samo.Aplication.ServiceSamo.ServiceRegister;
using samo.Aplication.ViewModel.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samo.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterController : ControllerBase
    {
        private readonly IServiceRegisters _serviceRegister;

        public RegisterController(IServiceRegisters serviceRegister)
        {
            _serviceRegister = serviceRegister;
        }
        [HttpGet("All")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAll(Guid idUser)
        {
            var data = await _serviceRegister.GetRegister(idUser);
            return Ok(data);
        }
        [HttpGet("Id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int idService, string type)
        {
            var data = await _serviceRegister.GetById(idService, type);
            return Ok(data);
        }

  

        [HttpPost]
        public async Task<IActionResult> CreateRegisters([FromBody] RequestRegisterCreate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceRegister.CreateRegister(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRegisters([FromBody] RequestRegisterUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceRegister.UpdateRegister(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRegisters(int id, string type)
        {
            var result = await _serviceRegister.Delete(id,type);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("Chart")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChart(Guid idUser, int month)
        {

            var service = await _serviceRegister.GetChartByUser(idUser, month);
            return Ok(service);
        }
    }
   
}
