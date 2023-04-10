using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Core.Models.Country;
using AutoMapper;
using HotelListing.API.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Repository;
using HotelListing.API.Core.Models;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;
        //private readonly ILogger<CountriesController> _logger;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository/*, ILogger<CountriesController> logger*/)
        {           
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
            //this._logger = logger;
        }

        // GET: api/Countries
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetCountryModel>>> GetCountries()
        {
            if (_countriesRepository == null)
            {
                throw new NotFoundException(nameof(GetCountries));
            }
            var countries =  await _countriesRepository.GetAllAsync<CountryModel>();
            //var records = _mapper.Map<List<GetCountryModel>>(countries);
            return Ok(countries);
        }

        // GET: api/Countries/?StartIndex=0&PageSize=15&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<QueryPaged<GetCountryModel>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            if (_countriesRepository == null)
            {
                throw new NotFoundException(nameof(GetCountries));
            }
            var pagedCountriesResult = await _countriesRepository.GetAllAsync<GetCountryModel>(queryParameters);
            return Ok(pagedCountriesResult);
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryModel>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            /*if (country == null)
            {
                //logging in a controller level
                *//*_logger.LogWarning($"Record foung in {nameof(GetCountry)} with Id: {id}. ");*//*

                //Global Exception Handler
                throw new NotFoundException(nameof(GetCountry), id);
                //return NotFound();
            }*/

            //var countryModel = _mapper.Map<CountryModel>(country);

            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryModel updateCountry)
        {
            if (id != updateCountry.Id)
            {
                //return BadRequest("Invalid Record ID");
                throw new BadRequestException(nameof(PutCountry), id,"Invalid Record ID");
            }

            /*var country = await _countriesRepository.GetAsync(id);
            if(country == null)
            {
                throw new NotFoundException(nameof(GetCountries), id);
            }
            _mapper.Map(updateCountry, country);*/

            try
            {
                await _countriesRepository.UpdateAsync(id, updateCountry);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    throw new NotFoundException(nameof(GetCountries), id);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryModel createCountry)
        {
            
            //var country = _mapper.Map<Country>(createCountry);

            var country = await _countriesRepository.AddAsync<CreateCountryModel, GetCountryModel>(createCountry);

            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            /*var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountries), id);
            }*/

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
