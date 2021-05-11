using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myBooks.ActionResults;
using myBooks.Data.Models;
using myBooks.Data.Services;
using myBooks.Data.ViewModels;
using myBooks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublisherService _publisherService;
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublisherService publisherService, ILogger<PublishersController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        // Normal GET ALL Method
        //[HttpGet("get-all-publishers")]
        //public IActionResult GetAllPublishers()
        //{
        //    try
        //    {
        //        var _result = _publisherService.GetAllPublishers();
        //        return Ok(_result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("We could not load the publishers");
        //    }
            
        //}


        // Sorting Feature Only with GET ALL Method
        //[HttpGet("get-all-publishers")]
        //public IActionResult GetAllPublishers(string sortBy)
        //{
        //    try
        //    {
        //        var _result = _publisherService.GetAllPublishers(sortBy);
        //        return Ok(_result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("We could not load the publishers");
        //    }
        //}


        // Filtering & Sorting Feature for GET ALL Method
        //[HttpGet("get-all-publishers")]
        //public IActionResult GetAllPublishers(string sortBy, string searchString)
        //{
        //    try
        //    {
        //        var _result = _publisherService.GetAllPublishers(sortBy, searchString);
        //        return Ok(_result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("We could not load the publishers");
        //    }
        //}

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            //throw new Exception("This is an exception from get all new publishers"); // Forced  Exception for testing logs

            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers()");
                var _result = _publisherService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest("We could not load the publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-books-with-author/{id}")]

        public IActionResult GetPublisherData(int id)
        {
            var _response = _publisherService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpGet("get-publisher-by-id/{id}")]

        public IActionResult GetPublisherById(int id)
        {
            var _response = _publisherService.GetPublisherById(id);

            if (_response != null)
            {

                // CustomActionResult (instead of IActionResult

                //var _responseObj = new CustomActionResultVM()
                //{
                //    Publisher = _response
                //};

                //return new CustomActionResult(_responseObj);

                return Ok(_response);
            }
            else
            {
                // CustomActionResult (instead of IActionResult

                //var _responseObj = new CustomActionResultVM()
                //{
                //    Exception = new Exception("This is coming from publisher controller custom action result")
                //};

                //return new CustomActionResult(_responseObj);

                return NotFound();
            }
        }

        [HttpDelete("delete-publisher-by-id/{id}")] 
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
