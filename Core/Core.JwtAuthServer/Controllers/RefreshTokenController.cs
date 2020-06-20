using System.Web.Http;
using TC.Core.JwtAuthServer.Models;

namespace TC.Core.JwtAuthServer.Controllers
{
    public class RefreshTokensController : ApiController
    {
        private AudienceStore repo = null;

        public RefreshTokensController()
        {
            this.repo = new AudienceStore();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}