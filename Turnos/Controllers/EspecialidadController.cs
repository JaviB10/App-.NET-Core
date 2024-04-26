using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class EspecialidadController : Controller
    {

        private readonly TurnosContext _context;

        public EspecialidadController(TurnosContext context)
        {
            _context = context;
        }

        public IActionResult Index() 
        {
            return View(_context.Especialidad.ToList());
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidad = _context.Especialidad.Find(id);

            if (especialidad == null)
            {
                return NotFound();
            }

            return View(especialidad);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del Edit de vista
        public IActionResult Edit(int id, [Bind("EspecialidadID,Descripcion")] Especialidad especialidad)
        {
            if (id != especialidad.EspecialidadID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(especialidad);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(especialidad);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidad = _context.Especialidad.FirstOrDefault(e => e.EspecialidadID == id);

            if (especialidad == null)
            {
                return NotFound();
            }

            return View(especialidad);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var especialidad = _context.Especialidad.Find(id);

            if (especialidad == null)
            {
                return NotFound();
            }
            _context.Especialidad.Remove(especialidad);
            
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}