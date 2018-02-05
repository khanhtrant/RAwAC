using Microsoft.AspNetCore.Mvc;
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
            var authors=_libraryRepository.GetAuthors();
            return Ok(authors);
        }
    }
}