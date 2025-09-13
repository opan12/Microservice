Bu projede mikroservis mimarisi için birden fazla teknoloji kullanılmıştır. Backend servisleri geliştirmek için .NET 8 ve C# tercih edilmiştir. Servisler arası veri taşımak ve yönetmek için DTO (Data Transfer Object) yapıları kullanılmıştır.

Projede servislerin bağımsız ve taşınabilir şekilde çalışabilmesi için Docker kullanılmıştır. Tüm servisler ve bağımlılıkları, docker-compose.yml dosyası ile yönetilmekte ve tek komutla çalıştırılabilmektedir.

API isteklerini yönlendirmek ve servisler arası iletişimi sağlamak için YARP (Yet Another Reverse Proxy) kullanılmıştır. Tüm servisler REST API standartlarına uygun olarak geliştirilmiştir.
