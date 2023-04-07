using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using AutoMapper;
using HotelListing.API.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Exceptions;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {       
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
        {           
            this._hotelsRepository = hotelsRepository;
            this._mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelModel>>> GetHotels()
        {
            if (_hotelsRepository == null)
            {
                throw new NotFoundException(nameof(GetHotels));
            }
            var hotels = await _hotelsRepository.GetAllAsync();
            return Ok(_mapper.Map<List<HotelModel>>(hotels));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelModel>> GetHotel(int id)
        {
            if (_hotelsRepository == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
            }
            var hotel = await _hotelsRepository.GetAsync(id);
            
            if(hotel == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
            }
            return Ok(_mapper.Map<HotelModel>(hotel));
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelModel hotelModel)
        {
            if (id != hotelModel.Id)
            {
                throw new BadRequestException(nameof(PutHotel), id, "Invalid Record ID");
            }
            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
                //return NotFound();
            }

            _mapper.Map(hotelModel, hotel);

            try
            {
                await _hotelsRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    throw new NotFoundException(nameof(GetHotel), id);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelModel createHotelModel)
        {
          if (_hotelsRepository == null)
          {
              return Problem("Entity set 'HotelListingDbContext.Hotels'  is null.");
          }
            var hotel = _mapper.Map<Hotel>(createHotelModel);
            await _hotelsRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {           
            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
            }

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
