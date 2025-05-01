using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shark_Tech.DAL;

namespace Shark_Tech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
