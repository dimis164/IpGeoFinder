using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Natech.IpGeoFinder.Api.Model;
using Natech.IpGeoFinder.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoIpController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGeoIpRepository _repository;

        public GeoIpController(IMapper mapper, IGeoIpRepository repository)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet("{iP}")]
        public async Task<IActionResult> Get(string iP)
        {

            try
            {
                var result = await _repository.GetGeo(iP);
                GeoIpModel geoIp = _mapper.Map<GeoIpModel>(result);

                return Ok(geoIp);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fetch data faild");

            }


        }

    }
}
