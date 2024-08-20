# MyGameStat

## Description:
MyGameStat is a full-stack web application designed to help users catalog, organize, and manage their video game collections. The platform allows users to add, edit, and view detailed information about their games, track their progress, and analyze their gaming habits. MyGameStat will offer a user-friendly interface that integrates with external gaming databases for fetching game details, providing a comprehensive and engaging experience for gamers.

## User Stories:
### User Authentication
- Users can create an account to personalize their game collection.
- Users can log in to access their game collection and settings.
- Users can log out to secure their account.

### Game Collection Management
- Users can manually add games to their collection with details like title, platform, and release date.
- Users can search a game database (e.g., IGDB, RAWG) and add games to their collection.
- Users can update game details in their collection.
- Users can delete games they no longer want to track.

### Game Details
- Users can view detailed information about each game, including cover art, genre, and ratings.
- Users can mark games as “Completed,” “In Progress,” or “Backlog.”
- Users can write personal reviews or notes for each game.

### Search & Filter
- Users can search their collection by title, genre, platform, or status.
- Users can filter their collection by platform, release date, or status.

### Wishlist
- Users can add games to a wishlist for future purchases or play.

### Statistics & Analytics
- Users can see the percentage of completed games.
- Users can view the distribution of games by platform.
- Users can analyze their most collected genres.

## Running the application:
### Backend
The backend application is a .NET Core code-first application with MS SQL Server as the database server. In order to run the backend application data migration is required.
- To run the backend navigate to `src/Web/API` then run the command:
  - `dotnet run --launch-profile "https"`.
  - View the swagger page at `https://localhost:7094/swagger/index.html`
- For data migration navigate to `src/Web/API` then run the commands:
  - `dotnet ef migrations add <migration name here>`
  - `dotnet ef database update`
  - Note:
    - The MS SQL Server should be running.
    - The connection string in `src/Web/API/appsettings.json` (`src/Web/API/appsettings.Development.json` for dev environment) should be valid.
    - For fresh migrations, delete the `Migrations` folder in `src/Web/API`.
    - For fresh database updates, nuke the database using SQL below.

## Nuking a SQL Server Database (Database name is MyGameStat in example):
```sql
USE [master]
GO
ALTER DATABASE [MyGameStat] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
DROP DATABASE [MyGameStat]
GO
```

The UserController is for user resources not related to authentication (identity) such as accessing a user's collection of games.
