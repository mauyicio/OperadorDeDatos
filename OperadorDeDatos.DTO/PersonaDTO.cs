using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OperadorDeDatos.DTO
{
	public class PersonaDTO
	{	
		public string DocumentoIdentidad { get; set; }
		public string Nombres { get; set; }
		public string Apellidos { get; set; }		
		public DateTime FechaNacimiento { get; set; }
		public List<string> NumerosTelefonicos { get; set; } = new List<string>();
		public List<string> CorreosElectronicos { get; set; } = new List<string>();
		public List<string> DireccionesFisicas { get; set; } = new List<string>();
	}
}
