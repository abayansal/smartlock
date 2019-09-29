# Smartlock

This is a very simple API build with .NET CORE 2.2. In order to build and run it locally please make sure you have .NET CORE 2.2 installed.

# Build

In addition to build it locally with your Visual Studio instance, you can simply call the build powershell script.
This will build the solution and run the tests.
```
PS C:\workspace\smart-lock> .\build.ps1
```

# Technologies

Some other technology decisions I made was:

 - MongoDB: For DB demonstration purposes, easy to setup, scales well, for the purposes of this simple assignment a relational DB could also be an option too.
 - Mediatr: For in-proc messaging, as the details can be seen here [https://github.com/jbogard/MediatR/wiki] Normally we could've used a real distributed message broker like RabbitMQ or NServiceBus, but for the demostration of the publish/subscribe mechanis and command dispatching, Mediatr is a big help.
 - ASP.NET Web API: Defacto web framework in .NET world, a complete framework with a lot of extensibility and documentation.
 - Docker: Easy to install other dependant tools such as DB servers, also can be utilized in production environments.
 - Cake: A perfect DSL for building .NET applications, great for CI integrations, has a lot of integration for many different tasks.
 
# Design

The application has the following endpoints.

 - /token/authenticate : This is the first endpoint that needs to be hit. The client APIs should retrieve a JWT token to be able to access other endpoints. The purpose of this endpoint is first authenticate the client application whether it is a mobile app or a backend system for a office automation.
 - /user/create : We can simply add new users to our system.
 - /gate/create : We can simply add new gates to our system.
 - /gate/grant-access : This is the endpoint where we give a user permission to unlock a particular gate. 
 - /gate/unlock : This is the endpoint when our user tries to unlock a gate. If a user is granted access for a particular gate with the previous endpoint, then this endpoint will allow user to unlock the gate, otherwise gate won't be opened.

 
