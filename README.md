# Rise Phone Directory

Telefon rehberi uygulamasý için geliþtirilmiþ **Backend** projesi. Projede **CRUD** iþlemlere ek olarak bir adet **Worker Servis** üzerine yerleþtirilmiþ raporlama aracý bulunmaktadýr.

## Baþlarken

Repo'yu yerel bilgisayarýnýza indirerek Visual Studio ile çalýþtýrabilirsiniz. 

### Önkoþullar

Bilgisayarýnýz **PostgreSql** sunucusu kurlu olmalýdýr. Eðer kurulu deðilse aþaðýda bulunan adresten iþletim sisteminize uygun olan seçeneði seçerek kurulum iþlemini tamamlayýn. Ayrýca uzak baðlantýda gerçekleþtirebilirsiniz. Uzak baðlantý gerçekleþtireceðiniz durumlarda bu adamý ***atlayýn***.

```
https://www.postgresql.org/download/
```

### Kurulum

Repoyu klonlamak için lütfen aþaðýdaki kodu kullanýn;

```
git clone https://github.com/doganbas/rise-phone-directory.git
```

Klonlama iþlemi tamamlandýktan sonra gerekli paketleri yüklemek için aþaðýdaki kodu kullanabilirsiniz.

```
cd .\Rise.PhoneDirectory\
dotnet restore
```

Baðlantý bilgilerini deðiþtirmek için ***appsettings.json*** dosyasý içinde bulunan *"NpgSql"* bilgilerini güncellemeniz gerekmektedir. Ayrýca *RabbitMQ* üzerinden yeni bir ***AMQP*** url oluþturup *"ReportService"* deðerini güncellemelisiniz. 

## Çalýþtýrma

Projeyi canlý test etmek için lütfen Startup ayarlarýnda çoklu baþlangýç seçeneðini iþaretleyin ve aþaðýdaki iki projeyi baþlangýç konumuna getirin.

![enter image description here](https://i.ibb.co/s3vTb8N/startup.png)

## Geliþtirilirken Kullanýlanlar

* .Net Core 6.0
* Web API
* Worker Service
* Entity Freamework
* PostgreSQL
* RabbitMQ
* Autofac
* AutoMapper
* FluentValidation
* EPPlus
* Swagger
* xUnitTest
* Moq

## Versiyonlar

Mevcut sürümler için [bu repodaki etiketlere](https://github.com/doganbas/rise-phone-directory/tags) bakýn.

## Oluþturan

* **Serkan DOÐANBAÞ** - *Initial work* - [Profilim](https://github.com/doganbas)

## Lisans

Bu proje MIT Lisansý kapsamýnda lisanslanmýþtýr - ayrýntýlar için [LICENSE.md](LICENSE.md) dosyasýna bakýn

## Teþekkürler

* Projeyi inceleyen kullananlar.