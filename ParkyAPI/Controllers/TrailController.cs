using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Trails")]
    public class TailController : ControllerBase
    {
        private ITrailsRepository _ITReosetory;
        private readonly IMapper _mapper;
        public TailController(ITrailsRepository ITReosetory, IMapper mapper)
        {
            _ITReosetory = ITReosetory;
            _mapper = mapper;

        }



        /// <summary>
        /// Get All GetTrails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        public IActionResult GetTrails()
        {

            var objList = _ITReosetory.GetTrails();
            var objDtos = new List<TrailDto>();
            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<TrailDto>(obj));
            }
            return Ok(objDtos);
        }

        /// <summary>
        /// Get Trails By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTrails")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        public IActionResult GetTrails(int id)
        {
            var obj = _ITReosetory.GetTrails(id);
            if (obj != null)
            {
                var objDto = _mapper.Map<TrailDto>(obj);
                return Ok(objDto);
            }
            return NotFound();
        }
        /// <summary>
        /// Create New Trail
        /// </summary>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        public IActionResult createTrail([FromBody] TrailDto TrailDtoDto)
        {
            // Filter Data
            if (!ModelState.IsValid || TrailDtoDto == null)
                return BadRequest();
            if (_ITReosetory.TrailsExists(TrailDtoDto.Name))
            {
                ModelState.AddModelError("", "National Park Name existed");
                return StatusCode(404, ModelState);
            }
            var trailObj = _mapper.Map<Trail>(TrailDtoDto);
            bool isSaved = _ITReosetory.CreateTrails(trailObj);
            if (!isSaved)
            {
                ModelState.AddModelError("", "Not Saved , Some Error occures");
                return StatusCode(500, ModelState);
            }
            return Ok(trailObj);
        }
        /// <summary>
        /// Update Existing Trail Park
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trailDto"></param>
        /// <returns></returns>

        [HttpPatch("{id:int}", Name = "UpdateTrails")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult UpdateTrails(int id, [FromBody] TrailDto trailDto)
        {
            if (trailDto == null || id != trailDto.id)
            {
                return BadRequest();
            }
            Trail trail = _mapper.Map<Trail>(trailDto);
            if (!_ITReosetory.UpdateTrails(trail))
            {
                ModelState.AddModelError("", "Can't Update it");
                return StatusCode(404, ModelState);
            }
            return NoContent();
        }
        /// <summary>
        /// Delete Trail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id:int}", Name = "delateTrail")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        public IActionResult delateTrail(int id)
        {
            if (!_ITReosetory.TrailsExists(id))
            {
                return NotFound();
            }
            var trail = _ITReosetory.GetTrails(id);
            if (!_ITReosetory.DeleteTrails(trail))
            {
                ModelState.AddModelError("", "Can't Delete it");
                return StatusCode(500, ModelState);
            }
            return NoContent();


        }

    }
}
