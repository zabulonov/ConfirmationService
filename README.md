# About
Confirmation email service. This is a pet project, which is a web API for confirming email when registering in various services.
![image_2024-01-22_17-25-42](https://github.com/zabulonov/ConfirmationService/assets/83907630/b0773d5e-afce-4e57-9189-227fc4e846c3)

The main users of this service are other services that want to use a ready-made boxed solution to confirm the email of their clients.

# Projects in Solution

***ConfirmationService.Host*** is the main web API project, the application is launched from it, stores controllers, launch settings, authentication settings, etc.

***ConfirmationService.BusinessLogic*** stores models used in APIs and business logic services. Business logic services are used in controllers to process application logic.

***ConfirmationService.Core*** stores models of entities used to interact with the database.

***ConfirmationService.Infrastructure*** this project contains everything related to external services, Entity Framework settings, connection to the database and Mailkit Smtp.

***ConfirmationService.Tests*** - unit test project for testing the application.
