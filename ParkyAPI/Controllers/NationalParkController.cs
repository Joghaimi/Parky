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
    public class NationalParkController : ControllerBase
    {
        private INationalParkRepository _npReosetory;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository npReosetory, IMapper mapper)
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

        /// <summary>
        /// Get National Park By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetNationalParks")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200,Type=typeof(NationalParkDto))]
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
        /// <summary>
        /// Create New National Park
        /// </summary>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
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
        /// <summary>
        /// Update Existing National Park
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
    
        [HttpPatch("{id:int}", Name = "updateNationalPark")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
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
        /// <summary>
        /// Delete National Park
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    
        [HttpDelete("{id:int}", Name = "DeleteNationlPark")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
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
