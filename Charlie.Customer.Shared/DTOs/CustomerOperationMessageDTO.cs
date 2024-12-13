using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlie.Customer.Shared.DTOs
{
	public class CustomerOperationMessageDTO
	{
		public string CorrelationId { get; set; }
		public string Operation { get; set; }
		public CustomerDTO Payload { get; set; }
	}
}
