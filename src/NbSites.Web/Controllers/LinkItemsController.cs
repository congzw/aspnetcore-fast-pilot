﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NbSites.Web.Libs.Data;
using NbSites.Web.Libs.Domain;

namespace NbSites.Web.Controllers
{
    public class LinkItemsController : Controller
    {
        private readonly LinksDbContext _context;

        public LinkItemsController(LinksDbContext context)
        {
            _context = context;
        }

        // GET: LinkItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.LinkItems.ToListAsync());
        }

        // GET: LinkItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkItem = await _context.LinkItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkItem == null)
            {
                return NotFound();
            }

            return View(linkItem);
        }

        // GET: LinkItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LinkItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Href,Description,Sort,OffLine")] LinkItem linkItem)
        {
            if (ModelState.IsValid)
            {
                linkItem.Id = Guid.NewGuid();
                _context.Add(linkItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linkItem);
        }

        // GET: LinkItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkItem = await _context.LinkItems.FindAsync(id);
            if (linkItem == null)
            {
                return NotFound();
            }
            return View(linkItem);
        }

        // POST: LinkItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Href,Description,Sort,OffLine")] LinkItem linkItem)
        {
            if (id != linkItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linkItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkItemExists(linkItem.Id))
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
            return View(linkItem);
        }

        // GET: LinkItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkItem = await _context.LinkItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkItem == null)
            {
                return NotFound();
            }

            return View(linkItem);
        }

        // POST: LinkItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var linkItem = await _context.LinkItems.FindAsync(id);
            _context.LinkItems.Remove(linkItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkItemExists(Guid id)
        {
            return _context.LinkItems.Any(e => e.Id == id);
        }
    }
}
