﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_TechStore.Models.DB;

namespace Sistema_TechStore.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DbTechStoreContext _context;

        public ProductosController(DbTechStoreContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var dbTechStoreContext = _context.Productos.Include(p => p.IdCategoriaProductoNavigation).Include(p => p.IdModeloNavigation).Include(p => p.IdTipoProductoNavigation);
            return View(await dbTechStoreContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaProductoNavigation)
                .Include(p => p.IdModeloNavigation)
                .Include(p => p.IdTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdCategoriaProducto", "IdCategoriaProducto");
            ViewData["IdModelo"] = new SelectList(_context.Modelos, "IdModelo", "IdModelo");
            ViewData["IdTipoProducto"] = new SelectList(_context.TiposProductos, "IdTipoProducto", "IdTipoProducto");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,NombreProducto,DescripcionProducto,IdCategoriaProducto,IdTipoProducto,IdModelo,PrecioCompra,PrecioVenta,CantidadStock,Estado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdCategoriaProducto", "IdCategoriaProducto", producto.IdCategoriaProducto);
            ViewData["IdModelo"] = new SelectList(_context.Modelos, "IdModelo", "IdModelo", producto.IdModelo);
            ViewData["IdTipoProducto"] = new SelectList(_context.TiposProductos, "IdTipoProducto", "IdTipoProducto", producto.IdTipoProducto);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdCategoriaProducto", "IdCategoriaProducto", producto.IdCategoriaProducto);
            ViewData["IdModelo"] = new SelectList(_context.Modelos, "IdModelo", "IdModelo", producto.IdModelo);
            ViewData["IdTipoProducto"] = new SelectList(_context.TiposProductos, "IdTipoProducto", "IdTipoProducto", producto.IdTipoProducto);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdProducto,NombreProducto,DescripcionProducto,IdCategoriaProducto,IdTipoProducto,IdModelo,PrecioCompra,PrecioVenta,CantidadStock,Estado")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewData["IdCategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdCategoriaProducto", "IdCategoriaProducto", producto.IdCategoriaProducto);
            ViewData["IdModelo"] = new SelectList(_context.Modelos, "IdModelo", "IdModelo", producto.IdModelo);
            ViewData["IdTipoProducto"] = new SelectList(_context.TiposProductos, "IdTipoProducto", "IdTipoProducto", producto.IdTipoProducto);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaProductoNavigation)
                .Include(p => p.IdModeloNavigation)
                .Include(p => p.IdTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(string id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
