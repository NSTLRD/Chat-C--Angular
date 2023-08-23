# Chat-C#-API---Angular
small functionality of the chat application

# Online Chat Application
This is an online chat application with Angular, ASP.NET Core, SignalR, and SqlServer following the principles of Clean Architecture. It has the following functionalities </br> 
 * As a new user you can register with your email address, first name, and last name </br>
 * Registered users can log in with their email address. </br>
 * LoggedIn user has a dashboard from where he can see list users and he can chat with anyone from the list. Chat always happen between two users. </br>
 * User can see the chat history.</br>
 * User can delete his chat history in one of two ways. He or she can delete chat data for himself/herself or everyone.</br>
 * Application has the sign out functionality.</br></br>

## Technologies

* ASP.NET Core 7.1
* Entity Framework Core 7.1
* Angular 9
* Signal R
* Sql Server
* Autofac
* Moq
* XUnit
* xunit.runner.visualstudio
* FluentAssertions
* Bootstrap

## Development Environment Ready

1. Install Latest [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. Install [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
3. Install the latest .Net 7
4. Install the latest [Node.js LTS](https://nodejs.org/en/)
5. Run `npm install -g @angular/cli` to install the latest version of Angular CLI


## Run Front-End Application (Angular 9)

1. Navigate to the workspace folder, such as `simple-chat-ui`.
2. Open the terminal window
3. Run `npm install` to install all dependencies used in the application.
4. Run `npm start` to run the chat application in the browser.
5. Browse `http://localhost:4200` to view the chat app in browser

##Video about the functionality of the project.
   [screen-capture (2).webm](https://github.com/NSTLRD/Chat-C--Angular/assets/103397605/ab52c17a-459c-42e4-92f0-c0067b0f7177)

Note: Please walk through this `simple-chat/simple chat development environment preparation guidelines.docx` to learn how to prepare your environment and to run this online chat application in details.

## Database Configuration

The Application uses data-store in SQL Server.

Update the **SimpleChatConnectionString** connection string within **simple-chat-api/src/Ehasan.SimpleChat.API/appsettings.json**, so that application can point to a valid SQL Server instance. 

```json
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "SimpleChatConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=Chat;Trusted_Connection=True;TrustServerCertificate=True"
  },
```

When you run the **update-database** command, the migrations will be applied and the database will be automatically created.

## Application Architecture

### Domain (Sdiaz.SimpleChat.Core)

This will contain all entities, enums, exceptions, interfaces, types, and logic specific to the domain layer.

### Application (Sdiaz.SimpleChat.Core)

This layer contains all application logic. It is dependent on the domain layer but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a message service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, SMTP, and so on. These classes should be based on interfaces defined within the application layer.


### RestFull API (Sdiaz.SimpleChat.API)

This layer is a restful API based on .Net Core 7.1. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Program.cs* should reference Infrastructure.

### Front-End (Angular 9)

Front-end is a single-page application based on angular 9. This only communicates with a restful API layer to store or retrieve data.

