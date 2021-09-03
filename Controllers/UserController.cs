namespace LODSInterviewProject.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using LODSInterviewProject.Models;
    using LODSInterviewProject.Services;

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService cosmosDbService)
        {
            _userService = cosmosDbService;
        }

        /*[ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllAsync("SELECT * FROM c"));
        }
        */

        [ActionName("Index")]
        public async Task<IActionResult> Index([Bind("Id")] LODSInterviewProject.Models.Organization item)
        {
            //Organization item = (Organization)ViewData["Organization"];
            System.Console.WriteLine(item.Id);
            ViewData["Organization"] = item;
            return View(await _userService.GetAllAsync(String.Format("SELECT * FROM c WHERE c.organizationid = '{0}'", item.Id)));
        }
        

        [ActionName("Create")]
        public IActionResult Create([Bind("Id")] LODSInterviewProject.Models.Organization item)
        {
            ViewData["Organization"] = item;
            User user = new User
            {
                OrganizationId = item.Id,
                //OrganizationId = ((Organization)ViewData["Organization"]).Id,
                FirstName = "TestFN",
                LastName = "TestLN"
            };

            return View(user);
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateAsync([Bind("FirstName,LastName,Email,Completed")] LODSInterviewProject.Models.User item)
        public async Task<ActionResult> CreateAsync([Bind("OrganizationId,FirstName,LastName,Email,Completed")] LODSInterviewProject.Models.User item)
        {
            if (ModelState.IsValid)
            {
                //ViewData["Organization"] = item;
                item.Id = Guid.NewGuid().ToString();
                //item.OrganizationId = ((Organization)ViewData["Organization"]).Id;
                await _userService.AddAsync(item);
                return RedirectToAction("Index", new { id = item.OrganizationId });
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("AddUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUserAsync([Bind("OrganizationId,Completed")] LODSInterviewProject.Models.User item)
        {
            if (ModelState.IsValid)
            { 
                item.OrganizationId = ((Organization)ViewData["Organization"]).Id;
                item.Id = Guid.NewGuid().ToString();
                await _userService.AddAsync(item);               
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id, OrganizationId, FirstName, LastName, Email, Completed")] LODSInterviewProject.Models.User item)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateAsync(item.Id, item);
                return RedirectToAction("Index", new { id = item.OrganizationId });
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            LODSInterviewProject.Models.User item = await _userService.GetAsync(id);
            ViewData["User"] = item;

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            LODSInterviewProject.Models.User item = await _userService.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            ViewData["User"] = item;
            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id, OrganizationId, FirstName, LastName, Email, Completed")] LODSInterviewProject.Models.User item)
        {
            await _userService.DeleteAsync(item.Id);
            return RedirectToAction("Index", new { id = item.OrganizationId });
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            LODSInterviewProject.Models.User item = await _userService.GetAsync(id);
            ViewData["User"] = item;

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
    }
}
