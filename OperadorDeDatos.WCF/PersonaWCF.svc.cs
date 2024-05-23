using OperadorDeDatos.BLL;
using OperadorDeDatos.DTO;
using OperadorDeDatos.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OperadorDeDatos.WCF
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PersonaWCF" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select PersonaWCF.svc or PersonaWCF.svc.cs at the Solution Explorer and start debugging.
	public class PersonaWCF : IPersonaWCF
	{
		private readonly PersonaService _personaService;

		public PersonaWCF()
		{
			string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
			_personaService = new PersonaService(connectionString);
		}

		public void RegistrarPersona(PersonaDTO personaDto)
		{
			try
			{
				var persona = new Persona
				{
					DocumentoIdentidad = personaDto.DocumentoIdentidad,
					Nombres = personaDto.Nombres,
					Apellidos = personaDto.Apellidos,
					FechaNacimiento = personaDto.FechaNacimiento,
					CorreosElectronicos = personaDto.CorreosElectronicos,
					DireccionesFisicas = personaDto.DireccionesFisicas,
					NumerosTelefonicos = personaDto.NumerosTelefonicos
				};

				_personaService.RegistrarPersona(persona);
			}
			catch (ArgumentException ex)
			{
				// Manejar excepciones de validación y lanzar FaultException
				throw new FaultException<ServiceFault>(new ServiceFault(ex.Message, string.Empty), new FaultReason(ex.Message));
			}
			catch (SqlException ex)
			{
				// Manejar excepciones de base de datos y lanzar FaultException
				throw new FaultException<ServiceFault>(new ServiceFault("Se produjo un error en la base de datos..", ex.StackTrace), new FaultReason("Error BaseDeDatos"));
			}
			catch (Exception ex)
			{
				// Manejar excepciones generales y lanzar FaultException
				throw new FaultException<ServiceFault>(new ServiceFault("ocurrió un error inesperado.", ex.StackTrace), new FaultReason("Error inesperado"));
			}
		}
	}
}
