using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReceitaMx.Data;
using ReceitaMx.Models;
using ReceitaMx.Requests;

namespace ReceitaMx.Controllers
{
    [Route("receitas/[action]")]
    public class ReceitaController : Controller
    {
        private readonly AppDbContext _context;

        public ReceitaController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Receitas.Include(r => r.Categoria);
            return View(await appDbContext.ToListAsync());
        }

  
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receitas == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .Include(r => r.Categoria)
                .FirstOrDefaultAsync(m => m.ReceitaId == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        public IActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "CategoriaId", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrateReceitaRequest request)
        {
            if (ModelState.IsValid)
            {
                var receita = new Receita()
                {
                    Title = request.Title,
                    Description = request.Description,
                    CategoriaId = request.CategoriaId,
                    Guide = request.Guide,
                    SendDate = DateTime.Now
                };
                _context.Add(receita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", request.CategoriaId);
            return View();
        }

    
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Receitas == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas.FirstOrDefaultAsync(x => x.ReceitaId == id);
            if (receita == null)
            {
                return NotFound();
            }
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "CategoriaId", "Title", receita.CategoriaId);
            return View(new EditReceitaRequest()
            {
                ReceitaId = id,
                Title = receita.Title,
                Description = receita.Description,
                Guide = receita.Guide,
                CategoriaId = receita.CategoriaId
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditReceitaRequest request)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var receita = await _context.Receitas.FirstOrDefaultAsync(x => x.ReceitaId == request.ReceitaId);
                    receita.CategoriaId = request.CategoriaId;
                    receita.Title = request.Title;
                    receita.Description = request.Description;
                    receita.Guide = request.Guide;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceitaExists(request.ReceitaId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", request.CategoriaId);
            return View(new EditReceitaRequest()
            {
                ReceitaId = request.ReceitaId,
                Title = request.Title,
                Description = request.Description,
                Guide = request.Guide,
                CategoriaId = request.CategoriaId
            });
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receitas == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .Include(r => r.Categoria)
                .FirstOrDefaultAsync(m => m.ReceitaId == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receitas == null)
            {
                return Problem("Entity set 'AppDbContext.Receitas'  is null.");
            }

            var receita = await _context.Receitas.FirstOrDefaultAsync(x => x.ReceitaId == id);
            if (receita != null)
            {
                _context.Receitas.Remove(receita);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceitaExists(int id)
        {
          return (_context.Receitas?.Any(e => e.ReceitaId == id)).GetValueOrDefault();
        }
    }
}
