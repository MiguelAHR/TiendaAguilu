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
    public class RegistroPagosController : Controller
    {
        private readonly ProyectoContext _context;

        public RegistroPagosController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: RegistroPagos
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.RegistroPago.Include(r => r.Administrador).Include(r => r.Pedido).Include(r => r.TipoPago);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: RegistroPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroPago = await _context.RegistroPago
                .Include(r => r.Administrador)
                .Include(r => r.Pedido)
                .Include(r => r.TipoPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroPago == null)
            {
                return NotFound();
            }

            return View(registroPago);
        }

        // GET: RegistroPagos/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Id");
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id");
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "Id", "tipo");
            return View();
        }

        // POST: RegistroPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Detalle,Estado,TipoPagoId,AdminId,PedidoId")] RegistroPago registroPago)
        {
            ModelState.Remove("TipoPago");
            ModelState.Remove("Administrador");
            ModelState.Remove("Pedido");

            if (ModelState.IsValid)
            {
                _context.Add(registroPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Id", registroPago.AdminId);
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id", registroPago.PedidoId);
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "Id", "tipo", registroPago.TipoPagoId);
            return View(registroPago);
        }

        // GET: RegistroPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroPago = await _context.RegistroPago.FindAsync(id);
            if (registroPago == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Id", registroPago.AdminId);
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id", registroPago.PedidoId);
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "Id", "tipo", registroPago.TipoPagoId);
            return View(registroPago);
        }

        // POST: RegistroPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Detalle,Estado,TipoPagoId,AdminId,PedidoId")] RegistroPago registroPago)
        {
            if (id != registroPago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroPagoExists(registroPago.Id))
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
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Id", registroPago.AdminId);
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id", registroPago.PedidoId);
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "Id", "tipo", registroPago.TipoPagoId);
            return View(registroPago);
        }

        // GET: RegistroPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroPago = await _context.RegistroPago
                .Include(r => r.Administrador)
                .Include(r => r.Pedido)
                .Include(r => r.TipoPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroPago == null)
            {
                return NotFound();
            }

            return View(registroPago);
        }

        // POST: RegistroPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroPago = await _context.RegistroPago.FindAsync(id);
            if (registroPago != null)
            {
                _context.RegistroPago.Remove(registroPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroPagoExists(int id)
        {
            return _context.RegistroPago.Any(e => e.Id == id);
        }
    }
}
