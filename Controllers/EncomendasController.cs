using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeEncomendas.Models;

namespace SistemaDeEncomendas.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly Context _context;

        public EncomendasController(Context context)
        {
            _context = context;
        }


        // GET: Encomendas
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var context = _context.Encomendas.Include(e => e.Clientes);
            return View(await context.ToListAsync());
        }

        // GET: Encomendas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encomendas == null)
            {
                return NotFound();
            }

            var encomendas = await _context.Encomendas
                .Include(e => e.Clientes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encomendas == null)
            {
                return NotFound();
            }

            return View(encomendas);
        }

        // GET: Encomendas/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ClientesId"] = new SelectList(_context.Clientes, "Id", "Nome");
            return View();
        }

        // POST: Encomendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,FormaPagamento,Status,Valor,ClientesId")] Encomendas encomendas)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Encomendas.Add(encomendas);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //personalizado
            _context.Encomendas.Add(encomendas);
            await _context.SaveChangesAsync();
            ViewData["ClientesId"] = new SelectList(_context.Clientes, "Id", "Nome", encomendas.ClientesId);
            return RedirectToAction(nameof(Index));
            //return View(encomendas);
        }

        // GET: Encomendas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Encomendas == null)
            {
                return NotFound();
            }

            var encomendas = await _context.Encomendas.FindAsync(id);

            if (encomendas == null)
            {
                return NotFound();
            }

            ViewData["ClientesId"] = new SelectList(_context.Clientes, "Id", "Nome", encomendas.ClientesId);
            return View(encomendas);
        }

        // POST: Encomendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,FormaPagamento,Status,Valor,ClientesId")] Encomendas encomendas)
        {

            if (id != encomendas.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                _context.Encomendas.Update(encomendas);
                await _context.SaveChangesAsync();

                ViewData["ClientesId"] = new SelectList(_context.Clientes, "Id", "Nome", encomendas.ClientesId);
                //return View(encomendas);

                return RedirectToAction(nameof(Index));
            }
            //catch (DbUpdateConcurrencyException)
            catch (Exception ex)
            {
                if (!EncomendasExists(encomendas.Id))
                {
                    return NotFound();
                }
                else
                {
                    //throw;
                    return NotFound(ex.Message);
                }
            }
            //}              
        }

        // GET: Encomendas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Encomendas == null)
            {
                return NotFound();
            }

            var encomendas = await _context.Encomendas
                .Include(e => e.Clientes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encomendas == null)
            {
                return NotFound();
            }
            return View(encomendas);
        }

        // POST: Encomendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Encomendas == null)
            {
                return Problem("Entity set 'Context.Encomendas'  is null.");
            }
            var encomendas = await _context.Encomendas.FindAsync(id);
            if (encomendas != null)
            {
                _context.Encomendas.Remove(encomendas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncomendasExists(int id)
        {
            return _context.Encomendas.Any(e => e.Id == id);
        }
    }
}