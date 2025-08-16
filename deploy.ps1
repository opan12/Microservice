
$projects = @(
   "C:\Users\Casper\source\repos\Microservice\user\user.api\Microservice.user.api\Microservice.user.api.csproj"
"C:\Users\Casper\source\repos\Microservice\basvuru\basvuru.api\Microservice.basvuru.api\Microservice.basvuru.api.csproj" 
"C:\Users\Casper\source\repos\Microservice\Notification\Microservice.notification.api.csproj"
)

foreach ($proj in $projects) {
    $publishPath = Join-Path -Path (Split-Path $proj -Parent) -ChildPath "publish"
    
    Write-Host "Publishing $proj to $publishPath"
    
    dotnet publish $proj -c Release -o $publishPath
}

Write-Host "Starting docker-compose services..."
docker-compose up -d
