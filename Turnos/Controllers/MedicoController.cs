using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;
using Microsoft.Extensions.Logging;

namespace Turnos.Controllers
{
    public class MedicoController : Controller
    {
        private readonly TurnosContext _context;

        public MedicoController(TurnosContext context)
        {
            _context = context;
        }

        // GET: Medico
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medico.ToListAsync());
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

             var medico = await _context.Medico
            .Include(m => m.MedicoEspecialidad)
            .ThenInclude(e => e.Especialidad)
            .FirstOrDefaultAsync(m => m.MedicoID == id);
            
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medico/Create
        public IActionResult Create()
        {
            ViewData["ListaEspecialidades"] = new SelectList(_context.Especialidad, "EspecialidadID", "Descripcion");
            return View();
        }

        // POST: Medico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoID,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")] Medico medico, int[] EspecialidadID)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                
                await _context.SaveChangesAsync();
                
                if (EspecialidadID != null && EspecialidadID.Any())
                {
                    foreach (var especialidadID in EspecialidadID)
                    {
                        var medicoEspecialidad = new MedicoEspecialidad();
                        medicoEspecialidad.MedicoID = medico.MedicoID;
                        medicoEspecialidad.EspecialidadID = especialidadID;

                        _context.Add(medicoEspecialidad);
                    }
                    await _context.SaveChangesAsync();
                }
               
           

                return RedirectToAction(nameof(Index));
            }

            return View(medico);
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicoID,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")] Medico medico)
        {
            if (id != medico.MedicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.MedicoID))
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
            return View(medico);
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.MedicoID == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
            {
                _context.Medico.Remove(medico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.MedicoID == id);
        }
    }
}
