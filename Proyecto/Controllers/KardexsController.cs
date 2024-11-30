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
    public class KardexsController : Controller
    {
        private readonly ProyectoContext _context;

        public KardexsController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Kardexs
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Kardex.Include(k => k.Inventario).Include(k => k.JefeAlmacen).Include(k => k.Producto).Include(k => k.TipoMovimiento);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Kardexs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kardex = await _context.Kardex
                .Include(k => k.Inventario)
                .Include(k => k.JefeAlmacen)
                .Include(k => k.Producto)
                .Include(k => k.TipoMovimiento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kardex == null)
            {
                return NotFound();
            }

            return View(kardex);
        }

        // GET: Kardexs/Create
        public IActionResult Create()
        {
            ViewData["InventarioId"] = new SelectList(_context.Inventario, "Id", "Id");
            ViewData["jefeAlmacenId"] = new SelectList(_context.JefeAlmacen, "Id", "Credencial");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["TipoMovId"] = new SelectList(_context.TipoMovimiento, "Id", "Nombre");
            return View();
        }

        // POST: Kardexs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaMovimiento,TipoMovId,InventarioId,ProductoId,jefeAlmacenId")] Kardex kardex)
        {
            ModelState.Remove("TipoMovimiento");
            ModelState.Remove("Inventario");
            ModelState.Remove("Producto");
            ModelState.Remove("JefeAlmacen");

            if (ModelState.IsValid)
            {
                _context.Add(kardex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventarioId"] = new SelectList(_context.Inventario, "Id", "Id", kardex.InventarioId);
            ViewData["jefeAlmacenId"] = new SelectList(_context.JefeAlmacen, "Id", "Credencial", kardex.jefeAlmacenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", kardex.ProductoId);
            ViewData["TipoMovId"] = new SelectList(_context.TipoMovimiento, "Id", "Nombre", kardex.TipoMovId);
            return View(kardex);
        }

        // GET: Kardexs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kardex = await _context.Kardex.FindAsync(id);
            if (kardex == null)
            {
                return NotFound();
            }
            ViewData["InventarioId"] = new SelectList(_context.Inventario, "Id", "Id", kardex.InventarioId);
            ViewData["jefeAlmacenId"] = new SelectList(_context.JefeAlmacen, "Id", "Credencial", kardex.jefeAlmacenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", kardex.ProductoId);
            ViewData["TipoMovId"] = new SelectList(_context.TipoMovimiento, "Id", "Nombre", kardex.TipoMovId);
            return View(kardex);
        }

        // POST: Kardexs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaMovimiento,TipoMovId,InventarioId,ProductoId,jefeAlmacenId")] Kardex kardex)
        {
            if (id != kardex.Id)
            {
                return NotFound();
            }

            ModelState.Remove("TipoMovimiento");
            ModelState.Remove("Inventario");
            ModelState.Remove("Producto");
            ModelState.Remove("JefeAlmacen");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kardex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KardexExists(kardex.Id))
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
            ViewData["InventarioId"] = new SelectList(_context.Inventario, "Id", "Id", kardex.InventarioId);
            ViewData["jefeAlmacenId"] = new SelectList(_context.JefeAlmacen, "Id", "Credencial", kardex.jefeAlmacenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", kardex.ProductoId);
            ViewData["TipoMovId"] = new SelectList(_context.TipoMovimiento, "Id", "Nombre", kardex.TipoMovId);
            return View(kardex);
        }

        //empieza 
        public async Task<IActionResult> ProductoMasVendido()
        {
            var productoMasVendido = await _context.Kardex
                .GroupBy(k => k.ProductoId)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    TotalVentas = g.Count()
                })
                .OrderByDescending(g => g.TotalVentas)
                .FirstOrDefaultAsync();

            if (productoMasVendido != null)
            {
                var producto = await _context.Productos.FindAsync(productoMasVendido.ProductoId);
                ViewBag.Producto = producto?.Nombre;
                ViewBag.Total = productoMasVendido.TotalVentas;
            }
            else
            {
                ViewBag.Producto = null;
                ViewBag.Total = 0;
            }

            return View();
        }
        //menos vendido 
        public async Task<IActionResult> ProductoMenosVendido()
        {
            var productoMenosVendido = await _context.Kardex
                .GroupBy(k => k.ProductoId)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    TotalVentas = g.Count()
                })
                .OrderBy(g => g.TotalVentas)
                .FirstOrDefaultAsync();

            if (productoMenosVendido != null)
            {
                var producto = await _context.Productos.FindAsync(productoMenosVendido.ProductoId);
                ViewBag.Producto = producto?.Nombre;
                ViewBag.Total = productoMenosVendido.TotalVentas;
            }
            else
            {
                ViewBag.Producto = null;
                ViewBag.Total = 0;
            }

            return View();
        }


        //termina




        // GET: Kardexs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kardex = await _context.Kardex
                .Include(k => k.Inventario)
                .Include(k => k.JefeAlmacen)
                .Include(k => k.Producto)
                .Include(k => k.TipoMovimiento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kardex == null)
            {
                return NotFound();
            }

            return View(kardex);
        }

        // POST: Kardexs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kardex = await _context.Kardex.FindAsync(id);
            if (kardex != null)
            {
                _context.Kardex.Remove(kardex);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KardexExists(int id)
        {
            return _context.Kardex.Any(e => e.Id == id);
        }
    }
}
