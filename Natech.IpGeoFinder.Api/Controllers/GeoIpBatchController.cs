using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Natech.IpGeoFinder.DAL.Repositories;
using Natech.IpGeoFinder.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Natech.IpGeoFinder.DAL.Interfaces;
using Natech.IpGeoFinder.DAL.DataTypes;
using Natech.IpGeoFinder.Api.Utilities;
using Microsoft.AspNetCore.Routing;


namespace Natech.IpGeoFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoIpBatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGeoIpRepository _repository;
        private readonly IGeoIpDbRepository _repositoryDB;
        private readonly LinkGenerator _linkGenerator;

        public GeoIpBatchController(IMapper mapper, IGeoIpRepository repository, IGeoIpDbRepository repositoryDB, LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryDB = repositoryDB;
            _linkGenerator = linkGenerator;

        }

        [HttpPost()]
        public async Task<IActionResult> PostBatch(BatchIpsModel iPs)
        {
            try
            {
                BatchProcessor br = new BatchProcessor(_mapper, _repository, _repositoryDB);
                Guid batchid = await br.ProccessIps(iPs);



                var link = _linkGenerator.GetPathByAction(
                          HttpContext,
                          "Get",
                          values: new { id = batchid });

                var fullLink = $" { HttpContext.Request.Scheme }://{ HttpContext.Request.Host}{link}";

                return Created(link, fullLink);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                BatchProcessor br = new BatchProcessor(_mapper, _repository, _repositoryDB);
                return Ok(br.ProgressReport(id));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

    }
}
