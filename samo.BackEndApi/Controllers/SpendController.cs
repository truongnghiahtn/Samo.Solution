using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using samo.Aplication.ServiceSamo.ServiceSpend;
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
    public class SpendController : ControllerBase
    {
        private readonly ISeviceSpend _serviceSpend;

        public SpendController(ISeviceSpend serviceSpend)
        {
            _serviceSpend = serviceSpend;
        }

        [HttpGet("All")]
     
        public async Task<IActionResult> GetAll()
        {

            var service = await _serviceSpend.GetAll();
            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestCreate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceSpend.Create(request);
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

            var result = await _serviceSpend.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int IdSpend)
        {
            var result = await _serviceSpend.Delete(IdSpend);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int IdSpend)
        {

            var service = await _serviceSpend.GetById(IdSpend);
            return Ok(service);
        }
    }
}
