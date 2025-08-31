# Pratik – Dependency Injection

Bu örnekte **Ogretmen (IOgretmen)** ve **ClassRoom** sınıfları arasında **constructor injection** kullanılarak bağımlılık yönetimi gösterilir. Senin kurduğun akış, `Program.cs` içinde öğretmeni oluşturup sınıfa enjekte ederek **manuel DI** yaklaşımını kullanır.

---

## Amaç

* Sınıflar arası sıkı bağımlılığı azaltmak
* Arayüz (interface) üzerinden programlayarak test edilebilir ve esnek bir yapı kurmak

---

## Proje Yapısı (öneri)

```
DiPratik/
 ├─ DiPratik.csproj
 ├─ Program.cs              // Uygulama girişi – MANUEL DI kullanılır
 └─ Models/
     ├─ IOgretmen.cs       // Base interface
     ├─ Ogretmen.cs        // IOgretmen implementasyonu
     └─ ClassRoom.cs       // IOgretmen bağımlılığını constructor ile alır
```

---

## Gereksinimler

* .NET SDK **8.0+**

Kontrol için:

```bash
dotnet --version
```

---

## Kurulum & Çalıştırma

1. Konsol uygulaması oluştur (oluşturduysan geç):

```bash
dotnet new console -n DiPratik
```

2. `Models` klasörünü ekle ve aşağıdaki 3 dosyayı yerleştir:

* `IOgretmen.cs`: `GetInfo()` içeren arayüz
* `Ogretmen.cs`: Ad-soyad alanları olan somut sınıf
* `ClassRoom.cs`: `IOgretmen` bağımlılığını **constructor** üzerinden alan sınıf

3. **Program.cs** (MANUEL DI — senin kullandığın örnek):

```csharp
using System;
using DiPratik.Models;

var teacher   = new Ogretmen("Ayşe", "Yılmaz");
var classRoom = new ClassRoom(teacher);

Console.WriteLine(classRoom.GetTeacherInfo()); // Ayşe Yılmaz
```

4. Derle & çalıştır:

```bash
dotnet build

dotnet run
```

### Beklenen çıktı

```
Ayşe Yılmaz
```

---

## Kısa Teknik Açıklama

* **IOgretmen**: `FirstName`, `LastName` ve `GetInfo()` sözleşmesini tanımlar.
* **Ogretmen**: `IOgretmen`’i uygular; `GetInfo()` ad ve soyadı birleştirir.
* **ClassRoom**: Öğretmeni `IOgretmen` türü üzerinden **constructor** parametresiyle alır (Dependency Injection). `GetTeacherInfo()` içerden `Teacher.GetInfo()`’yu çağırır.
* **Program.cs**: Somut `Ogretmen` örneği oluşturup `ClassRoom`’a verir (**manuel DI**).

> Not: Arayüz kullanımı sayesinde `Ogretmen` yerine testlerde sahte (fake/mock) bir sınıf enjekte edilebilir.

---

## Test Edilebilirlik – Hızlı İpucu

Aynı arayüzü uygulayan bir test sınıfı oluşturup sınıfa vererek davranışı izole edebilirsin:

```csharp
public class FakeOgretmen : IOgretmen
{
    public string FirstName => "Test";
    public string LastName  => "Teacher";
    public string GetInfo() => "Test Teacher";
}
// var classRoom = new ClassRoom(new FakeOgretmen());
```

---

## Opsiyonel: .NET DI Container ile Kullanım

Manuel bağlama yerine bir IoC konteyner tercih edersen:

```bash
dotnet add package Microsoft.Extensions.DependencyInjection
```

```csharp
using Microsoft.Extensions.DependencyInjection;
using DiPratik.Models;

var services = new ServiceCollection();
services.AddTransient<IOgretmen>(_ => new Ogretmen("Ayşe", "Yılmaz"));
services.AddTransient<ClassRoom>();

var provider = services.BuildServiceProvider();
var classRoom = provider.GetRequiredService<ClassRoom>();
Console.WriteLine(classRoom.GetTeacherInfo());
```

Bu yaklaşımda bağımlılık çözümleme işini container üstlenir; sınıflar arası bağlar daha da gevşer.

---

## Özet

* **Constructor Injection** uygulandı.
* **Interface tabanlı** tasarım sayesinde esneklik ve test edilebilirlik sağlandı.
* Manuel DI ile hızlı başlangıç; istenirse .NET’in yerleşik DI container’ı ile genişletilebilir.
