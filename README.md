Kullanılan Teknolojiler

Bu projede mikroservis mimarisi için birden fazla teknoloji kullanılmıştır. Backend servisleri geliştirmek için .NET8  ve C# tercih edilmiştir. Servisler arası veri taşımak ve yönetmek için DTO (Data Transfer Object) yapıları kullanılmıştır.

Projede servislerin bağımsız ve taşınabilir şekilde çalışabilmesi için Docker ile konteynerleştirme yapılmıştır. Birden fazla servisi birlikte çalıştırmak ve yönetmek için ise Docker Compose kullanılmıştır.

API isteklerini yönlendirmek ve servisler arası iletişimi sağlamak için YARP (Yet Another Reverse Proxy) kullanılmıştır. Tüm servisler REST API standartlarına uygun olarak geliştirilmiştir.

Projeyi hızlıca deploy etmek için bir PowerShell scripti (deploy.ps1) mevcuttur. Bu sayede servisleri tek komutla ayağa kaldırabilirsiniz.
