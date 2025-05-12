using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contract;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager; //injection
        private readonly ILoggerService _logger; //injection
        private readonly IMapper _mapper; //injection
        //Managerda birşeyler maplenecekse constructora injection yapılmalı.
        private readonly IDataShaper<BookDto> _shaper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<BookDto> shaper) //Constructor //Dependency Injection
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {   
           
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> 
            GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            if(!bookParameters.ValidPriceRange)
                throw new PriceOutOfRangeBadRequestException();

            var booksWithMetaData = await _manager
                .Book
                .GetAllBooksAsync(bookParameters, trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

            var shapedData = _shaper.ShapeData(booksDto, bookParameters.Fields);
            return (books:shapedData, metaData : booksWithMetaData.MetaData);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id); //return NotFound(); // 404
            return _mapper.Map<BookDto>(book);
        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id,trackChanges);
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookDtoForUpdate, book); //tupple


        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            //check entity
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);           

            //Mapping gerekli 10 alan olsa 10 tane tek tek yazmaya gerek yok.
            //entity.Title = book.Title;
            //entity.Price = book.Price;

            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.Update(entity);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager.Book.GetOneBooksByIdAsync(id, trackChanges);

            if (entity is null)
                throw new BookNotFoundException(id);

            return entity;
        }
    }
}
