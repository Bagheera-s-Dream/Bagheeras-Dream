namespace Bagheeras.Dream.Web.Areas.Administration.Controllers
{
    using Bagheeras.Dream.Common;
    using Bagheeras.Dream.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
