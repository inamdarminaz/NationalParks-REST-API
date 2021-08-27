using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.DTOs;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParks : ControllerBase
    {
        private INationalParkRepository _repo;
        private IMapper _mapper;

        public NationalParks(INationalParkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// Get list of all National Parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllNationalParks()
        {
            var objList = _repo.GetNationalParks();
            var objListDTO = new List<NationalParkDTO>(); //new empty list of dto 
            foreach (var obj in objList)
            {
                objListDTO.Add(_mapper.Map<NationalParkDTO>(obj)); // .Map<Destination>(source);
            }

            return Ok(objListDTO);
        }

        /// <summary>
        /// Get individual National Park
        /// </summary>
        /// <param name="id">The id of the National Park </param>
        /// <returns></returns>
        [HttpGet("{id:int}",Name ="GetNationalPark")]
        public IActionResult GetNationalPark(int id)
        {
            var obj = _repo.GetNationalPark(id);

            if (obj == null)
                return NotFound();
            else
            {
                var objDTO = _mapper.Map<NationalParkDTO>(obj);
                return Ok(objDTO);
            }
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDTO dto)
        {
            if(dto==null)
                return BadRequest(ModelState);

            if(_repo.NationalParkExists(dto.Name))
            {
                ModelState.AddModelError("", "National Park Exists.");
                return StatusCode(404, ModelState);
            }

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState); // no need if using data annotations

            var np = _mapper.Map<NationalPark>(dto);
            if (!_repo.CreateNationalParkExists(np))
            {
                ModelState.AddModelError("", "Something went wrong.");
                return StatusCode(505, ModelState);
            }
            return CreatedAtRoute("GetNationalPark",new { id=np.Id},np);
        }

        [HttpPatch("{id:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int id, [FromBody]NationalParkDTO dto)
        {
            if (dto == null || id != dto.Id)
                return BadRequest();
            NationalPark np = _mapper.Map<NationalPark>(dto);
            if(!_repo.UpdateNationalParkExists(np))
            {
                ModelState.AddModelError("", "Something went wrong while patching");
                return StatusCode(505, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int id)
        {
            if (!_repo.NationalParkExists(id))
                return NotFound();
            var np = _repo.GetNationalPark(id);
            if (!_repo.DeleteNationalParkExists(np))
            {
                ModelState.AddModelError("", "Something went wrong while patching");
                return StatusCode(505, ModelState);
            }
            return NoContent();
        }
    }
}
