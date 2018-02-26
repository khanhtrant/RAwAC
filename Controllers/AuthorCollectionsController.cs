using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTful.Models;
using RESTful.Services;

namespace RESTful.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorCollectionsController:Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorCollectionsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository=libraryRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody]IEnumerable<AuthorForCreationDto>authorCollection)
        {
            if (authorCollection==null)
            {
                return BadRequest();
            }

            var authorEntities=Mapper.Map<IEnumerable<Entities.Author>>(authorCollection);
            foreach (var author in authorEntities)
            {
                _libraryRepository.AddAuthor(author);
            }

            if (!_libraryRepository.Save())
            {
                return StatusCode(500,"Unexpected error from server");
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorCollection(IEnumerable<Guid> ids)
        {
            
        }
    }
}