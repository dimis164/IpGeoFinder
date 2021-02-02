using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Natech.IpGeoFinder.Api.BackgroundServices;
using Natech.IpGeoFinder.Api.Model;
using Natech.IpGeoFinder.Api.Utilities;
using Natech.IpGeoFinder.DAL.Interfaces;
using System;
using System.Threading.Tasks;

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
        private readonly BatchProcessingChannel _batchProcessingChannel;

        public GeoIpBatchController(IMapper mapper, IGeoIpRepository repository, IGeoIpDbRepository repositoryDB, LinkGenerator linkGenerator, BatchProcessingChannel batchProcessingChannel)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryDB = repositoryDB;
            _linkGenerator = linkGenerator;
            _batchProcessingChannel = batchProcessingChannel;

        }

        [HttpPost()]
        public async Task<IActionResult> PostBatch(BatchIpsModel iPs)
        {
            try
            {
                BatchProcessor br = new BatchProcessor(_mapper, _repository, _repositoryDB);
                Guid batchid = await br.ProccessIps(iPs);

                var batchAdded = await _batchProcessingChannel.AddBatchAsync(batchid);

                if (batchAdded)
                {
                    var link = _linkGenerator.GetPathByAction(
                         HttpContext,
                         "Get",
                         values: new { id = batchid });

                    var fullLink = $" { HttpContext.Request.Scheme }://{ HttpContext.Request.Host}{link}";

                    return Created(link, fullLink);
                }

                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
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
