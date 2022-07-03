using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParkController : ControllerBase
    {
        private INationalParkRepository _npReosetory;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository npReosetory , IMapper mapper)
        {
            _npReosetory = npReosetory;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult GetNationalParks() {

            var objList = _npReosetory.GetNationalParks();
            var objDtos = new List<NationalParkDto>();
            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<NationalParkDto>(obj));
            }
            return Ok(objDtos);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetNationalParks(int id)
        {

            var obj = _npReosetory.GetNationalPark(id);
            if (obj != null)
            {
                var objDto = _mapper.Map<NationalParkDto>(obj);
                return Ok(objDto);
            }
            return NotFound();


        }

    }
}
