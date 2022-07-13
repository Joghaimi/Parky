using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "NationalPark")]
    public class NationalParkV2Controller : ControllerBase
    {
        private INationalParkRepository _npReosetory;
        private readonly IMapper _mapper;
        public NationalParkV2Controller(INationalParkRepository npReosetory, IMapper mapper)
        {
            _npReosetory = npReosetory;
            _mapper = mapper;

        }
        /// <summary>
        /// Get All National Parks 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks()
        {

            var objList = _npReosetory.GetNationalParks();
            var objDtos = new List<NationalParkDto>();
            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<NationalParkDto>(obj));
            }
            return Ok(objDtos);
        }
    }
}
