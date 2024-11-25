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
    public class AuxiliarAlmacensController : Controller
    {
        private readonly ProyectoContext _context;

        public AuxiliarAlmacensController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: AuxiliarAlmacens
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.AuxiliarAlmacen.Include(a => a.Usuario);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: AuxiliarAlmacens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auxiliarAlmacen = await _context.AuxiliarAlmacen
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auxiliarAlmacen == null)
            {
                return NotFound();
            }

            return View(auxiliarAlmacen);
        }

        // GET: AuxiliarAlmacens/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: AuxiliarAlmacens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Credencial")] AuxiliarAlmacen auxiliarAlmacen)
        {
            ModelState.Remove("Usuario");
            if (ModelState.IsValid)
            {
                _context.Add(auxiliarAlmacen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", auxiliarAlmacen.UsuarioId);
            return View(auxiliarAlmacen);
        }

        // GET: AuxiliarAlmacens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auxiliarAlmacen = await _context.AuxiliarAlmacen.FindAsync(id);
            if (auxiliarAlmacen == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", auxiliarAlmacen.UsuarioId);
            return View(auxiliarAlmacen);
        }

        // POST: AuxiliarAlmacens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Credencial")] AuxiliarAlmacen auxiliarAlmacen)
        {
            if (id != auxiliarAlmacen.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Usuario");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auxiliarAlmacen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuxiliarAlmacenExists(auxiliarAlmacen.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", auxiliarAlmacen.UsuarioId);
            return View(auxiliarAlmacen);
        }

        // GET: AuxiliarAlmacens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auxiliarAlmacen = await _context.AuxiliarAlmacen
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auxiliarAlmacen == null)
            {
                return NotFound();
            }

            return View(auxiliarAlmacen);
        }

        // POST: AuxiliarAlmacens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auxiliarAlmacen = await _context.AuxiliarAlmacen.FindAsync(id);
            if (auxiliarAlmacen != null)
            {
                _context.AuxiliarAlmacen.Remove(auxiliarAlmacen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuxiliarAlmacenExists(int id)
        {
            return _context.AuxiliarAlmacen.Any(e => e.Id == id);
        }
    }
}
