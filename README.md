# Rise Phone Directory

Telefon rehberi uygulamas� i�in geli�tirilmi� **Backend** projesi. Projede **CRUD** i�lemlere ek olarak bir adet **Worker Servis** �zerine yerle�tirilmi� raporlama arac� bulunmaktad�r.

## Ba�larken

Repo'yu yerel bilgisayar�n�za indirerek Visual Studio ile �al��t�rabilirsiniz. 

### �nko�ullar

Bilgisayar�n�z **PostgreSql** sunucusu kurlu olmal�d�r. E�er kurulu de�ilse a�a��da bulunan adresten i�letim sisteminize uygun olan se�ene�i se�erek kurulum i�lemini tamamlay�n. Ayr�ca uzak ba�lant�da ger�ekle�tirebilirsiniz. Uzak ba�lant� ger�ekle�tirece�iniz durumlarda bu adam� ***atlay�n***.

```
https://www.postgresql.org/download/
```

### Kurulum

Repoyu klonlamak i�in l�tfen a�a��daki kodu kullan�n;

```
git clone https://github.com/doganbas/rise-phone-directory.git
```

Klonlama i�lemi tamamland�ktan sonra gerekli paketleri y�klemek i�in a�a��daki kodu kullanabilirsiniz.

```
cd .\Rise.PhoneDirectory\
dotnet restore
```

Ba�lant� bilgilerini de�i�tirmek i�in ***appsettings.json*** dosyas� i�inde bulunan *"NpgSql"* bilgilerini g�ncellemeniz gerekmektedir. Ayr�ca *RabbitMQ* �zerinden yeni bir ***AMQP*** url olu�turup *"ReportService"* de�erini g�ncellemelisiniz. 

## �al��t�rma

Projeyi canl� test etmek i�in l�tfen Startup ayarlar�nda �oklu ba�lang�� se�ene�ini i�aretleyin ve a�a��daki iki projeyi ba�lang�� konumuna getirin.

![enter image description here](https://i.ibb.co/s3vTb8N/startup.png)

## Geli�tirilirken Kullan�lanlar

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

Mevcut s�r�mler i�in [bu repodaki etiketlere](https://github.com/doganbas/rise-phone-directory/tags) bak�n.

## Olu�turan

* **Serkan DO�ANBA�** - *Initial work* - [Profilim](https://github.com/doganbas)

## Lisans

Bu proje MIT Lisans� kapsam�nda lisanslanm��t�r - ayr�nt�lar i�in [LICENSE.md](LICENSE.md) dosyas�na bak�n

## Te�ekk�rler

* Projeyi inceleyen kullananlar.