using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTful.Helpers;
using RESTful.Models;
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
    }
}