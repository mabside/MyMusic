using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Api.Resources;
using MyMusic.Api.Validators;
using MyMusic.Core.Models;
using MyMusic.Core.Services;

namespace MyMusic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistService artistService, IMapper mapper)
        {
            this._artistService = artistService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ArtistResource>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtist();
            var artistResources = _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistResource>>(artists);

            return Ok(artistResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistResource>> GetByArtisitById(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            var artistResource = _mapper.Map<Artist, ArtistResource>(artist);

            return Ok(artistResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<ArtistResource>> CreateArtist([FromBody] SaveArtistResource saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validatorResult = await validator.ValidateAsync(saveArtistResource);

            if(!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);
            
            var artistToCreate = _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);

            var newArtist = await _artistService.CreateArtisit(artistToCreate);

            var artist= await _artistService.GetArtistById(newArtist.Id);

            var artistResource = _mapper.Map<Artist, ArtistResource>(artist);

            return Ok(artistResource);

        }

        [HttpPut("")]
        public async Task<ActionResult<ArtistResource>> UpdateArtist(int id,[FromBody] SaveArtistResource saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validatorResult = await validator.ValidateAsync(saveArtistResource);

            if(!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);
            
            var artistToBeUpdated = await _artistService.GetArtistById(id);

            if(artistToBeUpdated == null)
                return NotFound();

            var artist =  _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);

            await _artistService.UpdateArtist(artistToBeUpdated, artist);


            var updatedArtist = await _artistService.GetArtistById(id);


            var updatedArtistResource = _mapper.Map<Artist, ArtistResource>(updatedArtist);

            return Ok(updatedArtistResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await _artistService.GetArtistById(id);

            await _artistService.DeleteArtist(artist);

            return NoContent();
        }

    }
}
