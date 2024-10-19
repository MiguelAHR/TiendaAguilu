using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class JefeAlmacensController : Controller
    {
        private readonly ProyectoContext _context;

        public JefeAlmacensController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: JefeAlmacens
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.JefeAlmacen.Include(j => j.Usuario);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: JefeAlmacens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jefeAlmacen = await _context.JefeAlmacen
                .Include(j => j.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jefeAlmacen == null)
            {
                return NotFound();
            }

            return View(jefeAlmacen);
        }

        // GET: JefeAlmacens/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: JefeAlmacens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Credencial")] JefeAlmacen jefeAlmacen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jefeAlmacen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", jefeAlmacen.UsuarioId);
            return View(jefeAlmacen);
        }

        // GET: JefeAlmacens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jefeAlmacen = await _context.JefeAlmacen.FindAsync(id);
            if (jefeAlmacen == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", jefeAlmacen.UsuarioId);
            return View(jefeAlmacen);
        }

        // POST: JefeAlmacens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Credencial")] JefeAlmacen jefeAlmacen)
        {
            if (id != jefeAlmacen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jefeAlmacen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JefeAlmacenExists(jefeAlmacen.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", jefeAlmacen.UsuarioId);
            return View(jefeAlmacen);
        }

        // GET: JefeAlmacens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jefeAlmacen = await _context.JefeAlmacen
                .Include(j => j.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jefeAlmacen == null)
            {
                return NotFound();
            }

            return View(jefeAlmacen);
        }

        // POST: JefeAlmacens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jefeAlmacen = await _context.JefeAlmacen.FindAsync(id);
            if (jefeAlmacen != null)
            {
                _context.JefeAlmacen.Remove(jefeAlmacen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JefeAlmacenExists(int id)
        {
            return _context.JefeAlmacen.Any(e => e.Id == id);
        }
    }
}
