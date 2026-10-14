# ✅ Todo API — ASP.NET Core 9

A minimal RESTful **Todo** API built with **ASP.NET Core (.NET 9)** — create, read, update and delete todo items through a controller-based Web API.

<p>
  <img src="https://img.shields.io/badge/.NET%209-512BD4?style=flat&logo=dotnet&logoColor=white">
  <img src="https://img.shields.io/badge/C%23-239120?style=flat&logo=csharp&logoColor=white">
</p>

## Run

```bash
cd TodoApi
dotnet run
```

Try the endpoints with the included `TodoApi.http` file (VS Code REST Client / Visual Studio).

## Endpoints

| Method | Route | Description |
|---|---|---|
| GET | `/todos` | List todos |
| GET | `/todos/{id}` | Get one todo |
| POST | `/todos` | Create a todo |
| PUT | `/todos/{id}` | Update a todo |
| DELETE | `/todos/{id}` | Delete a todo |
