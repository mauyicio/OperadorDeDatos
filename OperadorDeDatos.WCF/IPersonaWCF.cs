using OperadorDeDatos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OperadorDeDatos.WCF
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPersonaWCF" in both code and config file together.
	[ServiceContract]
	public interface IPersonaWCF
	{
		[OperationContract]
		[FaultContract(typeof(ServiceFault))]
		void RegistrarPersona(PersonaDTO personaDto);
	}
}
