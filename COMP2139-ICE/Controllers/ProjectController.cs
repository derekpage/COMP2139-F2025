using System.Diagnostics;
using COMP2139_ICE.Data;
using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Controllers;

public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        /*
        var projects = new List<Project>()
        {
            new Project { ProjectId = 1, Name = "Project 1", Description = "First Project" }
        };
        */
        var projects = _context.Projects.ToList();
        return View(projects);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Project project)
    {
        if (ModelState.IsValid)
        {
            project.StartDate = ToUtc(project.StartDate);
            project.EndDate = ToUtc(project.EndDate);
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(project);
    }

    private DateTime ToUtc(DateTime input)
    {
        if (input.Kind == DateTimeKind.Utc) return input;
        if (input.Kind == DateTimeKind.Unspecified) return DateTime.SpecifyKind(input, DateTimeKind.Utc);
        return input.ToUniversalTime();
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var project = _context.Projects.Find(id);
        if (project == null) return NotFound(); // Returns a 404 error if the project doesn't exist.
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ProjectId,Name,Description")] Project project)
    {
        if (id != project.ProjectId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Projects.Update(project);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.ProjectId)) return NotFound();

                throw;
            }

            return RedirectToAction("Index");
        }

        return View(project);
    }

    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.ProjectId == id);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        /*
        var project = new Project{ ProjectId = id, Name = "Project " + id, Description = "Details of Project " + id };
        */
        var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpPost]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var project = _context.Projects.Find(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return NotFound();
    }
}