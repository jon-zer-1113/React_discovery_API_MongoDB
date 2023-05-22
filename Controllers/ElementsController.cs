using ReactMongoDB_API_01.Models;
using ReactMongoDB_API_01.Services;
using Microsoft.AspNetCore.Mvc;

namespace ReactMongoDB_API_01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElementsController : ControllerBase
    {
        private readonly ElementsService _elementsService;

        // Injection de dépendances pour obtenir une instance de ElementsService.
        public ElementsController(ElementsService elementsService) =>
            _elementsService = elementsService;

        // Endpoint pour récupérer tous les éléments de la liste.
        [HttpGet]
        public async Task<List<Element>> Get() =>
        await _elementsService.GetAsync();

        // Récupérer un élément de la liste par son ID.
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Element>> Get(string id)
        {
            var element = await _elementsService.GetAsync(id);

            if (element is null)
            {
                return NotFound();
            }

            return element;
        }

        // Créer un nouvel élément dans la liste.
        [HttpPost]
        public async Task<IActionResult> Post(Element newElement)
        {
            await _elementsService.CreateAsync(newElement);

            return CreatedAtAction(nameof(Get), new { id = newElement.Id }, newElement);
        }

        // Mettre à jour un élément existant dans la liste par son ID.
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Element updatedElement)
        {
            var element = await _elementsService.GetAsync(id);

            if (element is null)
            {
                return NotFound();
            }

            updatedElement.Id = element.Id;

            await _elementsService.UpdateAsync(id, updatedElement);

            return NoContent();
        }

        // Supprimer un élément de la liste par son ID.
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var element = await _elementsService.GetAsync(id);

            if (element is null)
            {
                return NotFound();
            }

            await _elementsService.RemoveAsync(id);

            return NoContent();
        }

    }
}
