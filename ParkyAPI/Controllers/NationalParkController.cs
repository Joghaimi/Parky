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
    public class NationalParkController : ControllerBase
    {
        private INationalParkRepository _npReosetory;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository npReosetory, IMapper mapper)
        {
            _npReosetory = npReosetory;
            _mapper = mapper;

        }
        [HttpGet]
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
        [HttpGet("{id:int}", Name = "GetNationalParks")]
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
        [HttpPost]
        public IActionResult createNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            // Filter Data
            if (!ModelState.IsValid || nationalParkDto == null)
                return BadRequest();
            if (_npReosetory.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Name existed");
                return StatusCode(404, ModelState);
            }
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            bool isSaved = _npReosetory.CreateNationalPark(nationalPark);
            if (!isSaved)
            {
                ModelState.AddModelError("", "Not Saved , Some Error occures");
                return StatusCode(500, ModelState);
            }
            return Ok(nationalPark);
            //return CreatedAtRoute("GetNationalParks", new { id = nationalPark.Id },nationalPark);
        }


        [HttpPatch("{id:int}", Name = "updateNationalPark")]
        public IActionResult updateNationalPark(int id, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || id != nationalParkDto.Id)
            {
                return BadRequest();
            }
            NationalPark nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npReosetory.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "Can't Update it");
                return StatusCode(404, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteNationlPark")]
        public IActionResult delateNAtionalPark(int id)
        {
            if (!_npReosetory.NationalParkExists(id))
            {
                return NotFound();
            }
            var nationlPark = _npReosetory.GetNationalPark(id);
            if (!_npReosetory.DeleteNationalPark(nationlPark))
            {
                ModelState.AddModelError("", "Can't Delete it");
                return StatusCode(500, ModelState);
            }
            return NoContent();


        }

    }
}
