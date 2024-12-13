using Charlie.Customer.API;
using Charlie.Customer.DataAccess.Repositories;
using Charlie.Customer.Shared.DTOs;
using Charlie.Customer.Shared.Mappers;
using Charlie.Customer.Shared.Models;
using System.Text.Json;

namespace Charlie.Customer.RMQ
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly RabbitMqClient _rabbitMqClient;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public Worker(ILogger<Worker> logger, RabbitMqClient rabbitMqClient, IServiceScopeFactory serviceScopeFactory)
		{
			_logger = logger;
			_rabbitMqClient = rabbitMqClient;
			_serviceScopeFactory = serviceScopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Worker is subscribing to customer.operations queue...");

			await _rabbitMqClient.SubscribeAsync("customer.operations", async message =>
			{
				try
				{
					_logger.LogInformation($"Worker received message: {message}");

					
					var operation = JsonSerializer.Deserialize<CustomerOperationMessageDTO>(message);

					// Check if operation is not null and has the expected properties
					if (operation != null)
					{
						string correlationId = operation.CorrelationId;
						string operationType = operation.Operation;
						var customer = operation.Payload;

						_logger.LogInformation($"Processing operation for CorrelationId: {correlationId}");

						using (var scope = _serviceScopeFactory.CreateScope())
						{
							var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository<CustomerModel>>();  // Resolve repository
							var customerMapper = scope.ServiceProvider.GetRequiredService<CustomerMapper>();  // Resolve mapper

							if (operationType == "Create")
							{
								var customerModel = customerMapper.ToCustomerModel(customer);
								await customerRepository.AddAsync(customerModel);

								var response = new CustomerResponseDTO
								{
									CorrelationId = correlationId,
									Status = "Processed",
									Message = "Customer created successfully"
								};
								await _rabbitMqClient.PublishAsync("customer.responses", response);
								_logger.LogInformation($"Response sent for CorrelationId: {response.CorrelationId}");
							}
							else if (operationType == "Read")
							{

								var existingCustomer = await customerRepository.GetByIdAsync(customer.CustomerId);

								CustomerDTO customerDTO = null;
								string status = "Failed";
								string responseMessage = "Customer not found";

								if(existingCustomer != null)
								{
									customerDTO = customerMapper.ToCustomerDTO(existingCustomer);
									status = "Processed";
									message = "Customer retrieved successfully";
								}

								var response = new CustomerResponseDTO
								{
									CorrelationId = correlationId,
									Status = "Processed",
									Message = responseMessage,
									Payload = customerDTO
								};
								await _rabbitMqClient.PublishAsync("customer.responses", response);
								_logger.LogInformation($"Response sent for CorrelationId: {response.CorrelationId}");
							}
							else
							{
								var response = new CustomerResponseDTO
								{
									CorrelationId = correlationId,
									Status = "Failed",
									Message = "Unknown operation type"
								};
								await _rabbitMqClient.PublishAsync("customer.responses", response);
								_logger.LogWarning($"Unknown operation type for CorrelationId: {correlationId}");
							}
						}
					}
					else
					{
						_logger.LogWarning("Received an invalid request message (null request).");
					}
				}
				catch (Exception ex)
				{
					_logger.LogError($"Error processing message: {ex.Message}");
				}
			}, stoppingToken);
		}
	}
}


//protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//{
//	_logger.LogInformation("Worker is subscribing to customer.requests queue...");

//	while (!stoppingToken.IsCancellationRequested)
//	{
//		if (_logger.IsEnabled(LogLevel.Information))
//		{
//			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
//		}
//		await Task.Delay(1000, stoppingToken);
//	}
//}