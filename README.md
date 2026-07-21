# Evenex
Evenex - Etkinlik Oluşturma ve Bilet Alma Uygulaması

## Sistem Mimarisi

Bu proje, sistemin ölçeklenebilir, test edilebilir ve dış framework'lere veya veritabanlarına bağımlı olmamasını sağlamak için **Clean Architecture** (Temiz Mimari) prensiplerini takip etmektedir.

## Katmanların Detayları

### 1. İstemci Uygulaması (Angular)
*   **Rol:** Frontend kullanıcı arayüzü.
*   **Sorumluluk:** Kullanıcı girdilerini alır, Web API'ye HTTP istekleri yapar ve verileri ekrana çizer. Veritabanı veya iş kuralları hakkında hiçbir doğrudan bilgisi yoktur.

### 2. Sunum Katmanı (Presentation Layer - `Evenex.Api`)
*   **Rol:** Backend için giriş noktası.
*   **Sorumluluk:** RESTful Controller'ları barındırır. Angular istemcisinden gelen HTTP isteklerini alır, Application katmanına yönlendirir ve uygun HTTP durum kodlarıyla JSON yanıtları döner. 
*   **Kural:** Burada hiçbir iş kuralı (business logic) veya veritabanı sorgusu (LINQ) yazılmasına izin verilmez.

### 3. Uygulama Katmanı (Application Layer - `Evenex.Application`)
*   **Rol:** Orkestratör (Use Case'ler / Kullanım Senaryoları).
*   **Sorumluluk:** Uygulamanın ne yapacağını yönetir. Komutları (commands) ve sorguları (queries) işler, uygulamaya özel kuralları uygular ve repository arayüzleri (interface) aracılığıyla Domain entity'leri ile etkileşime girer.

### 4. Çekirdek Katmanı (Domain Layer - `Evenex.Domain`)
*   **Rol:** Bağımlılığı olmayan değişmeyen kısımları tutan katman.
*   **Sorumluluk:** Kurumsal iş mantığını, Entity'leri (`Seat`, `Event`, `Reservation`), Enum'ları ve Repository Arayüzlerini (örneğin, `ISeatRepository`) barındırır. 
*   **Kural:** Bu katman tamamen izoledir ve çözüm (solution) içindeki diğer hiçbir projeye **sıfır bağımlılığı** vardır.

### 5. Altyapı Katmanı (Infrastructure Layer - `Evenex.Infrastructure`)
*   **Rol:** Teknik implementasyon.
*   **Sorumluluk:** EF Core `DbContext`'i, veritabanı migration'larını ve Domain katmanında tanımlanan repository arayüzlerinin (interface) somut (concrete) kodlarını barındırır. 
*   **Kural:** Özel eşzamanlılık (concurrency) kontrollerinin (`RowVersion` hatalarını yakalamak gibi) ve ham SQL çevirilerinin yapıldığı yer burasıdır.


## Teknoloji Yığını (Tech Stack)

Bu proje, güçlü ve ölçeklenebilir bir temel oluşturmak için modern kurumsal standartlara uygun olarak ve **YAGNI (You Aren't Gonna Need It)** prensibi göz önünde bulundurularak geliştirilmiştir:

*   **Backend Framework:** .NET (C#)
*   **Mimari Yaklaşım:** Clean Architecture (Temiz Mimari)
*   **Tasarım Deseni:** Kullanım Senaryosuna Özel (Specific) Repository Pattern & Unit of Work
*   **Veritabanı:** MSSQL
*   **ORM:** Entity Framework Core (Code-First Yaklaşımı)
*   **Frontend:** Angular
*   **Kimlik Doğrulama & Güvenlik:** JWT (JSON Web Token)


## Mimari Kararlar ve Veri Yönetimi

### Neden Spesifik Repository Pattern?
Bu projede, veri erişimi için standart bir Generic Repository (`Repository<T>`) yerine **Spesifik Repository Pattern** (ör. `ISeatRepository`, `IEventSectionRepository`) tercih edilmiştir. Biletleme sistemleri doğası gereği karmaşık iş kuralları içerir. Bu seçimin temel nedenleri şunlardır:

* **Karmaşık Sorguların Yönetimi:** "Sadece yayında olan etkinliklerin, durumu müsait olan VIP koltuklarını getir" gibi katmanlı LINQ sorgularının Application katmanını kirletmesini engeller. Bu sorgular spesifik repository'ler içinde kapsüllenir (encapsulation).
* **Esneklik ve Sınırların Korunması:** Her entity'nin ihtiyaç duyduğu veritabanı operasyonu farklıdır. İhtiyaç duyulmayan metotların (örneğin bir `Event` kaydının fiziksel olarak silinmesi) dışarıya açılması engellenir.
* **Hata Yönetimi (Exception Handling):** Bilet satışındaki eşzamanlılık (concurrency) çakışmaları gibi özel hatalar, doğrudan ilgili repository içinde yakalanıp işlenebilir.

### EF Core Code-First ve Eşzamanlılık (Concurrency) Yönetimi
Veritabanı tasarımı ve yönetimi için **Entity Framework Core Code-First** yaklaşımı benimsenmiştir. Bu sayede C# sınıflarımız (Domain Entities) sistemin Tek Doğru Kaynağı (Single Source of Truth) olarak kalır ve veritabanı şeması Migration'lar aracılığıyla koddan üretilir.

Yüksek trafikli bir bilet satış platformunda en büyük zorluklardan biri **Eşzamanlılık (Concurrency)** problemidir. İki kullanıcının saniyenin onda biri farkla aynı koltuğu satın almaya çalışması durumunda çifte satışı (double-booking) önlemek için şu mekanizma kurulmuştur:

* **Optimistic Concurrency (İyimser Eşzamanlılık):** Entity'lerimizde (ör. `Seat` ve `EventSection`) bir `RowVersion` alanı bulunur.
* **Nasıl Çalışır?** Bir koltuk veritabanından okunduğunda, o anki `RowVersion` damgası da hafızaya alınır. Güncelleme işlemi sırasında, EF Core arka planda bu damganın değişip değişmediğini kontrol eder. Eğer kullanıcı koltuğu rezerve edene kadar başka biri o koltuğu almışsa, veritabanındaki `RowVersion` değişmiş olacaktır.
* **Sonuç:** EF Core işlemi durdurur ve bir `DbUpdateConcurrencyException` fırlatır. Altyapı katmanımız bu hatayı yakalayarak kullanıcıya "Bu koltuk başka biri tarafından rezerve edildi." şeklinde temiz bir uyarı dönülmesini sağlar.



## Veri Akış Şeması

Aşağıdaki şema, Angular istemcisinden (client) başlayıp uygulama katmanlarımızdan geçerek SQL Server veritabanına inen standart bir isteğin (örneğin; koltuk rezervasyonu) akışını göstermektedir.

```mermaid
sequenceDiagram
    autonumber
    actor User as Angular İstemcisi (Web)
    participant API as Web API (Sunum)
    participant App as Application Katmanı
    participant Domain as Domain Katmanı
    participant Infra as Infrastructure Katmanı
    participant DB as SQL Server (EF Core)

    User->>API: HTTP POST /api/reservations {seatId, eventId}
    API->>App: Command/DTO Gönderir
    Note over App: Kuralları ve yetkileri doğrular
    App->>Domain: Domain Entity'lerini Çeker
    Note over Domain: İş kurallarını çalıştırır<br/>(örn: MarkAsReserved)
    App->>Infra: ISeatRepository.Update() Çağırır
    Infra->>DB: EF Core SQL'e çevirir
    Note over DB: RowVersion Kontrolü (Concurrency)
    DB-->>Infra: Başarılı veya Hata (Exception) Döner
    Infra-->>App: Domain Sonucunu Döner
    App-->>API: Application Yanıtını Döner
    API-->>User: HTTP 200 OK veya 409 Conflict