# Lesson 12 - Entity Framework Core

ASP.NET Core MVC .NET 9 lab with CRUD for Category, Product (image upload), Banner, and StudentManager (class, student, subject, marks).

## SQL Server / SSMS

Connect in SSMS to `TQDATHIHI\SQLEXPRESS` with Windows Authentication. The same server is configured in `appsettings.json`.

The Code First migrations have already been applied to these databases:

- `NetCoreCRUD`: `Category`, `Product`, `Banner`
- `StudentManager`: `StdClass`, `Student`, `Subjects`, `Marks`

If you need to recreate the schema, run `SQL/CreateDatabases.sql` in SSMS, then run `SQL/NetCoreCRUD.sql` with `NetCoreCRUD` selected and `SQL/StudentManager.sql` with `StudentManager` selected. Use the scripts only for empty databases.

## Run

```powershell
dotnet run --project TqdLesson12/TqdLesson12.csproj
```

Pages: `/Categories`, `/Products`, `/Banners`, and `/StudentManager`.
