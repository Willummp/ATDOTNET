using CarGallery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace CarGallery.Controllers
{
    [Authorize]
    public class CarroController : Controller
    {
        private readonly CarGalleryContext _context;

        public CarroController(CarGalleryContext context)
        {
            _context = context;
        }

        // Action para exibir o formulário de criação de um novo carro
        public IActionResult Create()
        {
            var fabricantes = _context.Fabricantes.ToList();
            ViewBag.fabricantes = fabricantes;
            return View();
        }

        // Action para processar o formulário de criação de um novo carro
        [HttpPost]
        public IActionResult Create([FromForm] Carro carro, [Bind("idFabricante")] int idFabricante)
        {
            var fabricante = _context.Fabricantes.FirstOrDefault(x => x.Id == idFabricante);
            if (fabricante == null)
                throw new Exception("Fabricante não encontrado");

            var image = Request.Form.Files.GetFile("imageFile");

            if (image != null && !image.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("image_error", "Extensão de arquivo não permitida");

                var fabricantes = _context.Fabricantes.ToList();
                ViewBag.fabricantes = fabricantes;

                return View();
            }

            var fileName = image.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens_carros", image.FileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(stream);
                stream.Flush();
            }

            carro.Imagem = $"/imagens_carros/{image.FileName}";
            fabricante.Carros.Add(carro);

            _context.Fabricantes.Update(fabricante);
            _context.SaveChanges();

            return RedirectToAction("Index", "Fabricante");
        }

        // Action para exibir os detalhes de um carro específico
        [AllowAnonymous] // Permite que usuários não autenticados acessem esta action
        [HttpGet("/Carro/Detail/{id}")] // Define a rota personalizada para a action Detail
        public IActionResult Detail(int id)
        {
            var carro = _context.Carros.Find(id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }
    }
}
