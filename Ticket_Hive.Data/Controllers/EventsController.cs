using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.Data.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventsController : ControllerBase
	{
		private readonly IEventModelRepo _repository;
		private static List<EventModel> ? events = new();
			
		
		public EventsController(IEventModelRepo repository)
		{
			_repository = repository;
		}

		// GET api/events
		[HttpGet]
		public async Task<List<EventModel>>Get()
		{
			events =  await _repository.GetAllEventsAsync();

			return events;
		}

	}
}
