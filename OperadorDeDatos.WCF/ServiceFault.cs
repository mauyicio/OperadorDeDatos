using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OperadorDeDatos.WCF
{
	[DataContract]
	public class ServiceFault
	{
		[DataMember]
		public string ErrorMessage { get; set; }

		[DataMember]
		public string ErrorDetails { get; set; }

		public ServiceFault(string message, string details)
		{
			ErrorMessage = message;
			ErrorDetails = details;
		}
	}
}