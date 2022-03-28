using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreFinal.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CoreFinal
{
    public class ProductController : Controller
    {
        public ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product
        public IActionResult Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Catagory);
            return View(applicationDbContext.ToList());
        }

       
        public IActionResult Create()
        {
            ViewData["ID"] = new SelectList(_context.Catagories, "ID", "Catagory_Name");
            return View();
        }

        
        [HttpPost]
      
        public IActionResult Create( Product product)
        {
            if (ModelState.IsValid)
            {
                string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (product.Image.Length > 0)
                {
                    string filePath = Path.Combine(uploads, product.Image.FileName);
                    using (Stream filestream = new FileStream(filePath, FileMode.Create))
                    {
                         product.Image.CopyTo(filestream);
                        product.ImagePath = product.Image.FileName;

                    }
                   

                }
                _context.Add(product);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID"] = new SelectList(_context.Catagories, "ID", "Catagory_Name", product.CID);
            return View(product);
        }

   
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ID"] = new SelectList(_context.Catagories, "ID", "Catagory_Name", product.CID);
            return View(product);
        }

       
        [HttpPost]
       
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (product.Image.Length > 0)
                {
                    string filePath = Path.Combine(uploads, product.Image.FileName);
                    using (Stream filestream = new FileStream(filePath, FileMode.Create))
                    {
                         product.Image.CopyTo(filestream);
                        product.ImagePath = product.Image.FileName;

                    }


                }
                try
                {
                    _context.Update(product);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ID"] = new SelectList(_context.Catagories, "ID", "Catagory_Name", product.CID);
            return View(product);
        }

    
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Products
                .Include(p => p.Catagory)
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
    
        public IActionResult DeleteConfirmed(int id)
        {
            var product =  _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
