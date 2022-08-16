using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanche;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                lanche = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
               /* if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanche = _lancheRepository.Lanches
                        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                        .OrderBy(l => l.Nome);
                }
                else
                {
                    lanche = _lancheRepository.Lanches
                        .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                        .OrderBy(l => l.Nome);
                }*/
               lanche= _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(c=>c.Nome);
                categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanche,
                CategoriaAtual = categoriaAtual,
            };
            return View (lanchesListViewModel);
        }
        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault
                (l => l.LancheId == lancheId);
            return View (lanche);
        }
        public IActionResult Search( string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches= _lancheRepository.Lanches
                    .Where(p=>p.Nome.ToLower().Contains(searchString.ToLower()));
                if (lanches.Any())

                    categoriaAtual = "Lanches";
                else
                    categoriaAtual = "Nenhum lanche foi encontrado";

            }
            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
                {
                    Lanches = lanches,
                    CategoriaAtual = categoriaAtual,
                });
        }
    }
}
