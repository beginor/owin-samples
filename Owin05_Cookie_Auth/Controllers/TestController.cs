using System.Web.Http;

namespace Owin05_Cookie_Auth {

    [Authorize]
    public class TestController : ApiController {
        
        public IHttpActionResult Get() {
            var user = User;
            return Ok(new [] {"value 1", "value 2", user.Identity.Name});
        }

    }
}