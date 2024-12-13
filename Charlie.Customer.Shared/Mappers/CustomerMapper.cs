using Charlie.Customer.Shared.DTOs;
using Charlie.Customer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlie.Customer.Shared.Mappers
{
    public class CustomerMapper
    {
		public CustomerModel ToCustomerModel(CustomerDTO customerDTO)
		{
			var names = customerDTO.Name.Split(' ', 2); // Split into two parts: FirstName and LastName
			var firstName = names.Length > 0 ? names[0] : string.Empty;
			var lastName = names.Length > 1 ? names[1] : string.Empty;

			return new CustomerModel
			{
				Id = customerDTO.CustomerId, 
				FirstName = firstName,        
				LastName = lastName,          
				Email = customerDTO.Email,    
				PhoneNumber = string.Empty,   
				Address = string.Empty,      
				Name = customerDTO.Name      
			};
		}

		public CustomerDTO ToCustomerDTO(CustomerModel customerModel)
		{
			return new CustomerDTO
			{
				CustomerId = customerModel.Id,  
				Name = $"{customerModel.FirstName} {customerModel.LastName}", 
				Email = customerModel.Email    
			};
		}
	}
}
