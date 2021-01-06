namespace TC.Core.JwtAuthServer.Controllers
{
    using System.Web.Http;

    using TC.Core.JwtAuthServer.Models;
    using TC.Core.JwtAuthServer.Entities;

    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        // End point in our Authorization server which allow registering new Resource servers (Audiences)
        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AudienceStore store = new AudienceStore();
            Audience newAudience = store.AddAudience(audienceModel.Name);

            return this.Ok<Audience>(newAudience);
        }
    }
}