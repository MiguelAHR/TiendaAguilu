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
    public class PedidoesController : Controller
    {
        private readonly ProyectoContext _context;

        public PedidoesController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Pedido.Include(p => p.Administrador).Include(p => p.Producto).Include(p => p.Proveedor);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Administrador)
                .Include(p => p.Producto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Credencial");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Estado,ProveedorId,ProductoId,AdminId")] Pedido pedido)
        {
            ModelState.Remove("Proveedor");
            ModelState.Remove("Producto");
            ModelState.Remove("Administrador");
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Credencial", pedido.AdminId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", pedido.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre", pedido.ProveedorId);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Credencial", pedido.AdminId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", pedido.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre", pedido.ProveedorId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Estado,ProveedorId,ProductoId,AdminId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["AdminId"] = new SelectList(_context.Administrador, "Id", "Credencial", pedido.AdminId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", pedido.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre", pedido.ProveedorId);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Administrador)
                .Include(p => p.Producto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.Id == id);
        }
    }
}
