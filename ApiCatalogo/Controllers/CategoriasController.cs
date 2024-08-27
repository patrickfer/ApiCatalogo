using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger) 
        {
            _context = context; 
            _logger = logger;   
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {

            _logger.LogInformation("=================GET api/categorias/produtos ======================");
             return _context.Categorias.Include(p=> p.Produtos).Where(c=> c.CategoriaId <= 5).Take(10).AsNoTracking().ToList();  
        }


        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task <ActionResult<IEnumerable<Categoria>>> Get()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();         
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult Get(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não localizada...");
            }

            _context.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
