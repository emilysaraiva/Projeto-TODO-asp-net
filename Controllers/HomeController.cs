﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoAspNET.Models;
using TodoAspNET.Data;

namespace TodoAspNET.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tarefas = _context.Tarefas.ToList();

            return View(tarefas);
        }
        
        [HttpPost]
        public IActionResult Index([FromBody] Tarefa tarefa)
        {
            tarefa.Status = false;
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return Ok(tarefa);            

        }
        

        public IActionResult CompletaTarefa(int IdTarefa)
        {
            var tarefa = _context.Tarefas.Where(x => x.Id == IdTarefa).First();
            tarefa.Status = !tarefa.Status;
            _context.SaveChanges();

            return Ok(new {id=IdTarefa});
        }

        public IActionResult DeletaTarefa(int IdTarefa)
        {
            var tarefa = _context.Tarefas.Where(x => x.Id == IdTarefa).First();
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return Ok(new {id=IdTarefa});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
