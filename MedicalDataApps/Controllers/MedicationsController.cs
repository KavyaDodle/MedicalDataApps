using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalDataApps.Models;

namespace MedicalDataApps.Controllers
{
    public class MedicationsController : Controller
    {
        private readonly MedicalDbContext _context;

        public MedicationsController(MedicalDbContext context)
        {
            _context = context;
        }

        // GET: Medications
        public async Task<IActionResult> Index()
        {
            var medicalDbContext = _context.Medication.Include(m => m.Patient);
            return View(await medicalDbContext.ToListAsync());
        }

        // GET: Medications/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicationID == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // GET: Medications/Create
        public IActionResult Create()
        {
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID");
            return View();
        }

        // POST: Medications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicationID,PatientID,MedicationName,PrescribedDate,Dosage,Frequency")] Medication medication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", medication.PatientID);
            return View(medication);
        }

        // GET: Medications/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", medication.PatientID);
            return View(medication);
        }

        // POST: Medications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MedicationID,PatientID,MedicationName,PrescribedDate,Dosage,Frequency")] Medication medication)
        {
            if (id != medication.MedicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationExists(medication.MedicationID))
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
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", medication.PatientID);
            return View(medication);
        }

        // GET: Medications/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicationID == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // POST: Medications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var medication = await _context.Medication.FindAsync(id);
            if (medication != null)
            {
                _context.Medication.Remove(medication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationExists(string id)
        {
            return _context.Medication.Any(e => e.MedicationID == id);
        }
    }
}
