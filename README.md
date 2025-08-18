# Product API - 1. Aşama (Monolith)

## 🔹 Açıklama

Bu proje, .NET 8 ile geliştirilmiş basit bir Product CRUD Web API uygulamasıdır.  
Katmanlı mimari kullanılarak Controller → Service → Repository yapısı uygulanmıştır.  
Entity Framework Core ile MSSQL veritabanı bağlantısı sağlanmıştır.  
Swagger dokümantasyonu ile API endpointleri test edilebilir.

## 🔹 Özellikler

**Product CRUD işlemleri:**

- `GET /api/products` → Tüm ürünleri listele  
- `GET /api/products/{id}` → Ürün detay  
- `POST /api/products` → Ürün ekle  
- `DELETE /api/products/{id}` → Ürün sil  

**Diğer Özellikler:**

- Katmanlı mimari: Controller, Service, Repository  
- EF Core migration yönetimi  
- Swagger API dokümantasyonu  
- Dependency Injection  
- Global Exception Handling  

## 🔹 Teknolojiler

- .NET 8 
- C#  
- ASP.NET Core Web API  
- Entity Framework Core  
- MSSQL  
- Swagger  

## 🔹 Kurulum

Projeyi klonlayın:

```bash
git clone <repo-url>
cd ProductApi
````

Gerekli NuGet paketlerini yükleyin:

```bash
dotnet restore
````
Veritabanı bağlantısını appsettings.json dosyasında ayarlayın:

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;initial Catalog=ProductDb;integrated Security=true;TrustServerCertificate=true"
}
````
Migration oluştur ve database’i güncelle:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
````
Uygulamayı çalıştır:

````bash
dotnet run
````

##  🔹 Swagger

Uygulama çalıştığında Swagger UI'ya şu adresten erişebilirsiniz:
https://localhost:{port}/swagger

##  🔹 Commit ve Versiyonlama

Geliştirme sürecinde tek commit yapılmıştır.
Branch: phase1-monolith

##  🔹 Test

API endpointlerini Postman veya Swagger üzerinden test edebilirsiniz.
