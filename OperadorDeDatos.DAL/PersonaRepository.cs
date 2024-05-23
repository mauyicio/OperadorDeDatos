using OperadorDeDatos.Model;
using System.Data.SqlClient;
namespace OperadorDeDatos.DAL
{
	public class PersonaRepository
	{
		private readonly string _connectionString;

		public PersonaRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public bool ExisteDocumento(string documentoIdentidad)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand("SELECT COUNT(1) FROM Personas WHERE DocumentoIdentidad = @DocumentoIdentidad", connection))
				{
					command.Parameters.AddWithValue("@DocumentoIdentidad", documentoIdentidad);
					return (int)command.ExecuteScalar() > 0;
				}
			}
		}

		public void RegistrarPersona(Persona persona)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						var command = new SqlCommand(
							"INSERT INTO Personas (DocumentoIdentidad, Nombres, Apellidos, FechaNacimiento) VALUES (@DocumentoIdentidad, @Nombres, @Apellidos, @FechaNacimiento)",
							connection, transaction);
						command.Parameters.AddWithValue("@DocumentoIdentidad", persona.DocumentoIdentidad);
						command.Parameters.AddWithValue("@Nombres", persona.Nombres);
						command.Parameters.AddWithValue("@Apellidos", persona.Apellidos);
						command.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
						command.ExecuteNonQuery();

						RegistrarContactos(connection, transaction, persona);

						transaction.Commit();
					}
					catch
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}

		private void RegistrarContactos(SqlConnection connection, SqlTransaction transaction, Persona persona)
		{
			foreach (var telefono in persona.NumerosTelefonicos)
			{
				var command = new SqlCommand(
					"INSERT INTO Telefonos (DocumentoIdentidad, Telefono) VALUES (@DocumentoIdentidad, @Telefono)",
					connection, transaction);
				command.Parameters.AddWithValue("@DocumentoIdentidad", persona.DocumentoIdentidad);
				command.Parameters.AddWithValue("@Telefono", telefono);
				command.ExecuteNonQuery();
			}

			foreach (var correo in persona.CorreosElectronicos)
			{
				var command = new SqlCommand(
					"INSERT INTO Correos (DocumentoIdentidad, CorreoElectronico) VALUES (@DocumentoIdentidad, @CorreoElectronico)",
					connection, transaction);
				command.Parameters.AddWithValue("@DocumentoIdentidad", persona.DocumentoIdentidad);
				command.Parameters.AddWithValue("@CorreoElectronico", correo);
				command.ExecuteNonQuery();
			}

			foreach (var direccion in persona.DireccionesFisicas)
			{
				var command = new SqlCommand(
					"INSERT INTO Direcciones (DocumentoIdentidad, DireccionFisica) VALUES (@DocumentoIdentidad, @DireccionFisica)",
					connection, transaction);
				command.Parameters.AddWithValue("@DocumentoIdentidad", persona.DocumentoIdentidad);
				command.Parameters.AddWithValue("@DireccionFisica", direccion);
				command.ExecuteNonQuery();
			}
		}
	}
}