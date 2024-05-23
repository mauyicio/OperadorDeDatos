using OperadorDeDatos.DAL;
using OperadorDeDatos.Model;
using System;
using System.Text.RegularExpressions;

namespace OperadorDeDatos.BLL
{
	public class PersonaService
	{
		private readonly PersonaRepository _repository;

		public PersonaService(string connectionString)
		{
			_repository = new PersonaRepository(connectionString);
		}

		public void RegistrarPersona(Persona persona)
		{
			if (_repository.ExisteDocumento(persona.DocumentoIdentidad))
			{
				throw new ArgumentException("El documento de identidad ya está registrado.");
			}

			ValidarPersona(persona);

			_repository.RegistrarPersona(persona);
		}

		public void ValidarPersona(Persona persona)
		{
			if (string.IsNullOrWhiteSpace(persona.DocumentoIdentidad) ||
				string.IsNullOrWhiteSpace(persona.Nombres) ||
				string.IsNullOrWhiteSpace(persona.Apellidos) ||
				persona.FechaNacimiento == default)
			{
				throw new ArgumentException("Los campos DocumentoIdentidad, Nombres, Apellidos y Fecha de Nacimiento son obligatorios.");
			}

			if (!Regex.IsMatch(persona.DocumentoIdentidad, @"^[a-zA-Z0-9]+$"))
			{
				throw new ArgumentException("El Documento de Identidad solo acepta valores alfanuméricos.");
			}

			if (!Regex.IsMatch(persona.Nombres, @"^[a-zA-Z\s]+$") ||
				!Regex.IsMatch(persona.Apellidos, @"^[a-zA-Z\s]+$"))
			{
				throw new ArgumentException("Los Nombres y Apellidos solo pueden contener caracteres del alfabeto latino y no pueden contener valores numéricos.");
			}

			if (persona.CorreosElectronicos.Count > 2 ||
				persona.DireccionesFisicas.Count > 2 ||
				persona.NumerosTelefonicos.Count > 2)
			{
				throw new ArgumentException("Máximo se pueden registrar 2 números telefónicos, 2 correos electrónicos y 2 direcciones físicas por persona.");
			}

			if (persona.CorreosElectronicos.Count == 0 && persona.DireccionesFisicas.Count == 0)
			{
				throw new ArgumentException("Debe registrar al menos una información de contacto: dirección de correo electrónico ó una dirección física.");
			}

			// Otras validaciones específicas si es necesario.
		}
	}
}
