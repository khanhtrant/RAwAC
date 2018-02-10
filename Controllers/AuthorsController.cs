using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTful.Models;
using RESTful.Helpers;
using RESTful.Services;

namespace RESTful.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorsFromDb = _libraryRepository.GetAuthors();
            var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromDb);
            return Ok(authors);
        }

        [HttpGet("{authorId}",Name="GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromDb=_libraryRepository.GetAuthor(authorId);
            var author=Mapper.Map<AuthorDto>(authorFromDb);
            return Ok(author);
        }

        [HttpPost()]
        public IActionResult CreateAuthor([FromBody]AuthorForCreationDto authorForCreation)
        {
            if (authorForCreation==null)
            {
                return BadRequest();
            }

            var author=Mapper.Map<Entities.Author>(authorForCreation);
            _libraryRepository.AddAuthor(author);
            if (!_libraryRepository.Save())
            {
                return StatusCode(500,"Error unexpected from Server");
            }
            var authorForReturn=Mapper.Map<Models.AuthorDto>(author);
            return CreatedAtRoute("GetAuthor",new{
                authorId=authorForReturn.Id
            },authorForReturn);
        }
    }
}