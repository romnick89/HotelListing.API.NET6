﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Core.Contracts;
using AutoMapper;
using HotelListing.API.Core.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Models;

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
        [HttpGet("GetAllHotels")]
        public async Task<ActionResult<IEnumerable<HotelModel>>> GetHotels()
        {
            /*if (_hotelsRepository == null)
            {
                throw new NotFoundException(nameof(GetHotels));
            }*/
            var hotels = await _hotelsRepository.GetAllAsync<HotelModel>();
            return Ok(hotels);
        }

        // GET: api/Hotels?StartIndex=0&PageSize=3&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<QueryPaged<HotelModel>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            if (_hotelsRepository == null)
            {
                throw new NotFoundException(nameof(GetHotels));
            }
            var pagedHotelsResult = await _hotelsRepository.GetAllAsync<HotelModel>(queryParameters);
            //return Ok(_mapper.Map<List<HotelModel>>(hotels));
            return Ok(pagedHotelsResult);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelModel>> GetHotel(int id)
        {
            /*if (_hotelsRepository == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
            }*/
            var hotel = await _hotelsRepository.GetAsync<HotelModel>(id);
            
            /*if(hotel == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
            }*/
            return Ok(hotel);
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
            /*if (hotel == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
                //return NotFound();
            }

            _mapper.Map(hotelModel, hotel);*/

            try
            {
                await _hotelsRepository.UpdateAsync(id, hotel);
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
          /*if (_hotelsRepository == null)
          {
              return Problem("Entity set 'HotelListingDbContext.Hotels'  is null.");
          }*/
            //var hotel = _mapper.Map<Hotel>(createHotelModel);
            var hotel = await _hotelsRepository.AddAsync<CreateHotelModel, HotelModel>(createHotelModel);

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {           
            //var hotel = await _hotelsRepository.GetAsync(id);
            /*if (hotel == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
            }*/

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
