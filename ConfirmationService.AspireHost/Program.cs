using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ConfirmationService_Host>("webapi");

builder.Build().Run();