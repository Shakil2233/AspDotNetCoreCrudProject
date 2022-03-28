using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreFinal.Models;

namespace CoreFinal
{
    public class CatagoryController : Controller
    {
        public ApplicationDbContext _context;

        public CatagoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CatagoryViewModel catagoryViewModel = new CatagoryViewModel();
            catagoryViewModel.CatagorieVM = _context.Catagories.AsEnumerable();
            return View(catagoryViewModel.CatagorieVM);
        }

       
      
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
      
        public IActionResult Create(Catagory catagory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catagory);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(catagory);
        }

 
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catagory =  _context.Catagories.Find(id);
            if (catagory == null)
            {
                return NotFound();
            }
            return View(catagory);
        }

        
        [HttpPost]
    
        public IActionResult Edit(int id, Catagory catagory)
        {
            if (id != catagory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catagory);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatagoryExists(catagory.ID))
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
            return View(catagory);
        }

       
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catagory =  _context.Catagories
                .FirstOrDefault(m => m.ID == id);
            if (catagory == null)
            {
                return NotFound();
            }

            return View(catagory);
        }

      
        [HttpPost, ActionName("Delete")]
      
        public IActionResult DeleteConfirmed(int id)
        {
            var catagory = _context.Catagories.Find(id);
            _context.Catagories.Remove(catagory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CatagoryExists(int id)
        {
            return _context.Catagories.Any(e => e.ID == id);
        }
    }
}
