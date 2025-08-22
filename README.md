# Product API - 2. Aşama (Monolith + Caching + JWT)

## 🔹 Açıklama

Bu proje, .NET 8 ile geliştirilmiş **Product CRUD ve Auth Web API** uygulamasıdır.  
Orta düzey **Onion Architecture** ve **CQRS pattern** kullanılarak Controller → Service → Repository → Core yapısı uygulanmıştır.  

- **Auth Servisi:** Kullanıcı kayıt ve login işlemleri (JWT üretimi).  
- **Product Servisi:** Ürün ekleme, güncelleme, silme, listeleme.  
- **Cache:** Redis ile ürün listeleme hızlandırılmıştır, ekleme/güncelleme işleminde cache invalidation yapılmaktadır.  

---

## 🔹 Özellikler

### Auth Servisi
- `POST /api/auth/register` → Kullanıcı kayıt  
- `POST /api/auth/login` → JWT ile login  
- JWT token expiration ve validation ayarları  
- Rol ve isim claim’leri JWT içinde saklanır  

### Product Servisi
- `GET /api/products` → Tüm ürünleri listele (Redis cache ile hızlandırılmış)  
- `GET /api/products/{id}` → Ürün detay  
- `POST /api/products` → Ürün ekle (cache invalidation)  
- `PUT /api/products/{id}` → Ürün güncelle (cache invalidation)  
- `DELETE /api/products/{id}` → Ürün sil (cache invalidation)  

### Diğer Özellikler
- **Katmanlı mimari:** Controller, Service, Repository, Core  
- **CQRS pattern:** Temel Command/Query ayrımı  
- **Dependency Injection** ile servisler yönetiliyor  
- **Global Exception Handling** + **Logging (Serilog / Console)**  
- **Swagger API dokümantasyonu**  
- **Migration yönetimi (EF Core + PostgreSQL)**  

---

## 🔹 Teknolojiler

- .NET 8  
- C#  
- ASP.NET Core Web API  
- Entity Framework Core  
- PostgreSQL  
- Redis Cache  
- JWT Authentication  
- Serilog (Logging)  
- Swagger  

---

## 🔹 Kurulum

Projeyi klonlayın:

```bash
git clone <repo-url>
cd ProductApi
```

Gerekli NuGet paketlerini yükleyin:
```bash
dotnet restore
```

appsettings.json dosyasında veritabanı ve Redis bağlantılarını ayarlayın:

```json

"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=product;Username=postgres;Password=123456",
    "Redis": "localhost:6379"
}
```

Migration oluştur ve database’i güncelle:

```bash

dotnet ef migrations add InitialCreate
dotnet ef database update
```

Uygulamayı çalıştır:

```bash
dotnet run
```

## 🔹 Swagger
Uygulama çalıştığında Swagger UI'ya şu adresten erişebilirsiniz:
https://localhost:{port}/swagger

## 🔹 JWT Authentication
Kullanıcı login olduğunda JWT token üretilir

- Token, request header’da Authorization: Bearer {token} ile gönderilir

- Role ve Name claim’leri ile authorization kontrolü sağlanır

  <img width="1536" height="770" alt="Ekran görüntüsü 2025-08-21 160523" src="https://github.com/user-attachments/assets/da140fee-f926-429b-8b4c-01f70c569b94" />

- Postman login request + JWT response

- API çağrısı sırasında header’da token kullanımı

## 🔹 Redis Caching
- GET /api/products isteği Redis cache üzerinden hızlıca döner

-  POST / PUT / DELETE işlemlerinde cache invalidation uygulanır

- Redis CLI üzerinden cache kontrolü yapılabilir (keys *)

<img width="1482" height="753" alt="Ekran görüntüsü 2025-08-21 160400" src="https://github.com/user-attachments/assets/68e5ce27-02b9-463f-8069-f550868eb7bb" />

- Redis cache’in dolu olduğuna dair örnek

- Cache invalidation sonrası nil dönüşü

## 🔹 Logging (Serilog / Console)
Tüm request/response ve hatalar loglanır

- Serilog ile console ve isteğe bağlı file logging desteklenir

- Log formatı: timestamp, level, message

<img width="1482" height="753" alt="Ekran görüntüsü 2025-08-21 160400" src="https://github.com/user-attachments/assets/1b62f552-c945-475e-87bc-b9b066e51479" />

- GET /api/products çağrısı log örneği

- Exception log örneği

## 🔹 Commit ve Versiyonlama
- Geliştirme süreci boyunca günlük commit’ler yapılmıştır.
Branch: test/v1.0.0

# Product API - 3. Aşama (Microservices + API Gateway + CI)
## 🔹 Açıklama
Monolitik yapıda geliştirilen Product Web API uygulaması, mikroservis mimarisine dönüştürüldü.Login API, User API ve Product API olmak üzere üç ayrı servise bölündü.Servisler arası iletişim, merkezi yetkilendirme ve ölçeklenebilirlik için API Gateway (YARP), RabbitMQ ve Docker Compose kullanıldı.Süreç, temel Continuous Integration (CI) pipeline'ı ile otomatize edildi.

## 🔹 Özellikler ve Gelişmeler
**1. Mimari Dönüşüm**

- Login API → Kullanıcı giriş & JWT üretimi

- User API → Kullanıcı CRUD işlemleri

- Product API → Ürün CRUD & Redis Cache işlemleri

- API Gateway (YARP) → Merkezi yetkilendirme, rota yönetimi

**2. Merkezi Kimlik Doğrulama**

- Tüm kimlik doğrulama & yetkilendirme işlemleri API Gateway katmanına taşındı.

- Mikroservisler kendi içinde JWT doğrulaması yapmıyor, bu işlem Gateway tarafından yönetiliyor.

- Daha hafif servisler & merkezi güvenlik mantığı sağlandı.

**3. Servisler Arası İletişim**

- Mikroservisler arasındaki mesajlaşma için RabbitMQ eklendi.

- Olay tabanlı iletişim (event-driven architecture) yaklaşımı destekleniyor.

**4. Containerization & Orkestrasyon**

- Tüm servisler Docker üzerinde containerize edildi.

- Docker Compose ile servisler (API’ler, Gateway, RabbitMQ, Redis, PostgreSQL) tek komutla ayağa kaldırılabiliyor.

**5. CI/CD Süreçleri**

- GitHub Actions ile CI pipeline kuruldu.

- Her push ve pull request’te:

- Kod derleniyor

- Unit test’ler çalıştırılıyor

- Docker imajları build ediliyor

(Planlanan) CD aşaması ile otomatik dağıtım yapılacak.

## 🔹 Tamamlananlar
- ✅ Monolitik yapının mikroservislere ayrılması
- ✅ Login, User ve Product servislerinin oluşturulması
- ✅ API Gateway kurulumu & rota tanımları
- ✅ JWT yetkilendirmesinin Gateway’e taşınması
- ✅ Redis cache entegrasyonu
- ✅ RabbitMQ entegrasyonu
- ✅ Docker & Docker Compose entegrasyonu
- ✅ GitHub Actions CI pipeline (.github/workflows/ci-pipeline.yml)
<img width="1573" height="891" alt="Ekran görüntüsü 2025-08-22 192744" src="https://github.com/user-attachments/assets/59e680f8-7c9c-4365-bd1e-c8ee9c622f7a" />

## 🔹 Eksikler ve Gelecek Planı
- **Sürekli Dağıtım (CD)**: Şu an sadece CI aşaması tamamlandı. Gelecekte, derlenen ve test edilen uygulamaların otomatik olarak bir sunucuya veya bulut ortamına (Azure, AWS) dağıtılması için bir CD pipeline'ı eklenmeli.

- **Servisler Arası İletişim**: Mikroservisler arasında daha gelişmiş ve güvenli bir iletişim mekanizması (örneğin RabbitMQ veya gRPC) kurulabilir. Şu an HTTP üzerinden haberleşiyorlar.

- **Hizmet Keşfi (Service Discovery)**: Servislerin adreslerini manuel olarak appsettings.json'da tutmak yerine, Consul veya Eureka gibi bir hizmet keşif mekanizması entegre edilerek adres yönetimi dinamik hale getirilebilir.

- **Circuit Breaker**: Servislerden biri çöktüğünde diğer servisleri korumak için Polly gibi kütüphanelerle Circuit Breaker (devre kesici) pattern'i uygulanabilir.

- **Hata Toleransı ve İstek Tekrarlama**: Ağ hatalarına karşı istekleri otomatik olarak tekrarlayacak politikalar eklenmeli.

- **Oturum Yönetimi**: RefreshToken mekanizması Redis gibi harici bir cache'e taşınarak daha güvenli hale getirilebilir.
