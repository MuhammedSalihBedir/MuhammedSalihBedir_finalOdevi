# 🏨 Otel Yönetim Sistemi

Bu proje, nesne yönelimli programlama prensiplerini kullanarak geliştirilmiş kapsamlı bir otel yönetim sistemidir. Sistem, aşağıdaki OOP özelliklerini içermektedir:

## 📋 Nesne Yönelimli Tasarım Özellikleri

1. **Soyutlama (Abstraction)**: 
   - `IBookingService` arayüzü, rezervasyon işlemlerini `AddBooking`, `DeleteBooking`, `UpdateBooking` gibi yüksek seviyeli metodlarla soyutlamıştır
   - `IRoom` arayüzü, farklı oda tiplerini ortak bir arayüz üzerinden yönetmeyi sağlar
   - `HotelManager` sınıfı, karmaşık işlemleri basit metodlar arkasında soyutlayarak kullanımı kolaylaştırır

2. **Kapsülleme (Encapsulation)**: 
   - `BookingService` sınıfında `private readonly List<Booking> bookings` ve `private readonly List<IRoom> rooms` alanları
   - `Booking` sınıfında property'ler (`RoomNumber`, `Customer`, `BookingDate`)
   - Veri doğrulama işlemleri `HotelUI` sınıfında `ReadValidatedInput`, `nameChecked`, `phoneChecked` metodlarıyla kapsüllenmiştir

3. **Kalıtım (Inheritance)**: 
   - `Person` sınıfından türetilen `Customer` ve `Employee` sınıfları
   - `Room` temel sınıfından türetilen `SingleRoom`, `DoubleRoom` ve `Suite` sınıfları
   - Her oda tipi kendi özelliklerini korurken temel oda özelliklerini miras alır

4. **Çok Biçimlilik (Polymorphism)**: 
   - `IRoom` arayüzünü uygulayan farklı oda tipleri (`SingleRoom`, `DoubleRoom`, `Suite`)
   - `BookingService` sınıfının `IBookingService` arayüzünü uygulaması
   - Farklı oda tiplerinin aynı `HotelManager` metodları üzerinden yönetilebilmesi

5. **SOLID Prensipleri**: 
   - Tek Sorumluluk (SRP): Her sınıf belirli bir göreve odaklanmıştır (örn: `CsvHelper` sadece dosya işlemleri, `ConsoleHelper` sadece konsol çıktıları)
   - Açık/Kapalı (OCP): Yeni oda tipleri `Room` sınıfından türetilerek sistem değiştirilmeden genişletilebilir
   - Arayüz Ayrımı (ISP): `IRoom` ve `IBookingService` gibi özelleşmiş arayüzler
   - Bağımlılığın Ters Çevrilmesi (DIP): `BookingService` sınıfı `IRoom` arayüzüne bağımlıdır, somut sınıflara değil

## 🔧 Teknik Özellikler

- Veri Kalıcılığı:
  - CSV dosya sistemi ile veri yönetimi (`CsvHelper` sınıfı)
  - Rezervasyon ve çalışan verilerinin otomatik kaydedilmesi

- Kullanıcı Arayüzü:
  - Modüler konsol arayüzü (`HotelUI` sınıfı)
  - Renkli konsol çıktıları ve hata mesajları (`ConsoleHelper` sınıfı)
  - Kullanıcı dostu navigasyon ve geri bildirimler

- Sistem Özellikleri:
  - Farklı oda tipleri (Tek Kişilik, Çift Kişilik, Suit)
  - Kapsamlı rezervasyon yönetimi
  - Çalışan yönetimi ve yetkilendirme
  - Gerçek zamanlı oda durumu takibi

## 💡 Geliştirici Notu
Bu sistem, modern OOP yaklaşımlarını kullanarak, bakımı kolay ve genişletilebilir bir yapı sunmaktadır. Modüler yapısı sayesinde yeni özellikler kolayca eklenebilir.
