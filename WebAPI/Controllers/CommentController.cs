using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private CommentRepository _CommentRepository;

        public CommentController()
        {
            this._CommentRepository = new CommentRepository();
        }
        [HttpGet]
        public CommentCombine GetList(int? id, int page, int fetch)
        {
            return _CommentRepository.GetList(id, page, fetch);
        }


        
        [HttpPost]
        public IActionResult Insert(int id, int Rank, string Des, int AID, int SID)
        {
            var result = this._CommentRepository.insert_comment(id, Rank, Des, AID, SID);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult Insert([FromRoute] int id, IFormFile File)
        {
            var result = this._CommentRepository.insert_pic(id, File);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
