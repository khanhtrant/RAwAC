using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTful.Models;
using RESTful.Services;

namespace RESTful.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public BooksController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetBooksForAuthor(Guid authorId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var booksFromDb = _libraryRepository.GetBooksForAuthor(authorId);
            var booksForAuthor = Mapper.Map<IEnumerable<BookDto>>(booksFromDb);
            return Ok(booksForAuthor);
        }

        [HttpGet("{id}",Name="GetBookForAuthor")]
        public IActionResult GetBookForAuthor(Guid authorId, Guid id)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookFromDb = _libraryRepository.GetBookForAuthor(authorId, id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            var bookForAuthor = Mapper.Map<BookDto>(bookFromDb);
            return Ok(bookForAuthor);
        }

        [HttpPost()]
        public IActionResult CreateBookForAuthor(Guid authorId,[FromBody] BookForCreationDto book)
        {
            if (book==null)
            {
                return BadRequest();
            }

            var bookForDb=Mapper.Map<Entities.Book>(book);
            _libraryRepository.AddBookForAuthor(authorId,bookForDb);
            if (!_libraryRepository.Save())
            {
                return StatusCode(500,"Unexpected from server");
            }

            var bookCreateToReturn=Mapper.Map<Models.BookDto>(bookForDb);

            return CreatedAtRoute("GetBookForAuthor",new{
                authorId=authorId,
                id=bookCreateToReturn.Id
            },bookCreateToReturn);
        }
    }
}