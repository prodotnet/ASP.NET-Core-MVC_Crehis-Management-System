using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Data;
using MVC_Management.Models;
using MVC_Management.ViewModal;
using System.IO;




namespace MVC_Management.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RegistrationsController(IMapper mapper,ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        // GET: Registrations
        public async Task<IActionResult> Index(ClientsViewModal AllClientsList)
        {

            var registration = await _context.Registration
              .FirstOrDefaultAsync(m => m.Phone == AllClientsList.Phone);
            if (registration == null)
            {
                ViewBag.AmountDue =0;
            }
            else
            {
                ViewBag.AmountDue =  registration.Amount_due;
            }




            AllClientsList.Allclients = await _context.Registration.ToListAsync();


            if (!string.IsNullOrEmpty(AllClientsList.Phone))
            {
                AllClientsList.Allclients = AllClientsList.Allclients
                    .Where(x => x.Phone == AllClientsList.Phone).ToList();
            }


            return View(AllClientsList);
        }



        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }


     
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registration registration, IFormFile Picture)
        {


            /**
             var fileName = Picture.Name  + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + Picture.FileName;
             var path = _configuration["FileSettings:UploadFolder"]!;
             var filepath = Path.Combine(path, fileName);
             var stream = new FileStream(filepath, FileMode.Create);

             await Picture.CopyToAsync(stream);
             registration.Photo = fileName;
           **/


            var Registration = await _context.Registration
              .FirstOrDefaultAsync(m => m.ID == registration.ID);



            if (Registration == null)
            {
                
                registration.Amount_due = Convert.ToDecimal(0);

                _context.Add(registration);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "You Have SuccessfulRegistered This Client";
                return RedirectToAction(nameof(Index));


            }
            else 
            {

                TempData["ErrorMessage"] = "The ID Number You Entered Is already  Exist in The System";


            }


            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,Surname,Phone,Address,Al_Phone,Gender,Amount_due")] Registration registration)
        {
            if (id != registration.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var registration = await _context.Registration.FindAsync(id);
            if (registration != null)
            {
                _context.Registration.Remove(registration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(string id)
        {
            return _context.Registration.Any(e => e.ID == id);
        }
    }
}
