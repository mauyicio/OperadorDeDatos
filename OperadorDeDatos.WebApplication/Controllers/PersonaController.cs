using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using OperadorDeDatoss.WebApplication.Models;
using OperadorDeDatoss.WebApplication.PersonaServiceClient;

namespace OperadorDeDatoss.WebApplication.Controllers
{
	public class PersonaController : Controller
	{
		// GET: Persona
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult RegistrarPersona(PersonaModel model)
		{
			try
			{
				using (var client = new PersonaWCF())
				{
					var persona = PersonaMap(model);
					client.RegistrarPersona(persona);
				}

				ViewBag.Message = "Persona registrada con éxito.";
			}
			catch (Exception ex)
			{
				ViewBag.Message = $"Error: {ex.Message}";
			}

			return View("~/Views/Persona/Index.cshtml", model);
		}

		private PersonaDTO PersonaMap(PersonaModel model)
		{
			return new PersonaDTO
			{
				DocumentoIdentidad = model.DocumentoIdentidad,
				Nombres = model.Nombres,
				Apellidos = model.Apellidos,
				FechaNacimiento = model.FechaNacimiento,
				FechaNacimientoSpecified = true,
				CorreosElectronicos = model.CorreosElectronicos.ToArray(),
				DireccionesFisicas = model.DireccionesFisicas.ToArray(),
				NumerosTelefonicos = model.NumerosTelefonicos.ToArray()
			};
		}
	}
}