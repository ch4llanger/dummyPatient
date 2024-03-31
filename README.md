
## Gereksinimler

- RabbitMQ 3.13.0
- PostgreSQL 16.1
- Kubernetes 1.29
- .NET 6 Runtime / SDK
- TCP 5001, 5002, 5003, 5004, 5005 port'larının kullanımda olmaması

## API

### Patient
- Hasta yönetimi
- Hasta geçmişi görüntülemek için Appointment servisini dinler, hastanın randevuları görüntülenebilir.

### Appointment

- Randevu yönetimi
- Hasta, departmanı ve doktorunu seçerek randevu alır, alınan randevular Notification servisine kullanıcıya bilgi gönerilmesi için gönderilir.

### Doctor

- Departman ve doktor yönetimi.
- Oluşturulan departman ve doktorların event aracılığıyla Appointment ve Discussion servisine gönderilmesi.

### Question
- Doktora soru sorma ve doktor tarafından cevaplanabilme
- Patient ve Departmant servislerini dinleyerek hastaların doğru doktor/departman seçimi yapmasını sağlar. Doktor sorulara cevap verebilir.

### Notification
- Diğer servisleri dinleyerek console yazdırır .
- Gönderilen notifikasyonların takibi için notifikasyonlar kaydedilir.

## Kurulum

- Servislerde bulunan **Dockerfile** dosyaları kullanılarak Dockerize işlemleri tamamlanır. Daha sonra Kubernetes development'ı için **Development.yaml** ve **Service.yaml** kullanılır.
- Daha önceden kurulu bulunan RabbitMQ ve  Sqlite bağlantıları için her servisin içinde bulunan appsettings.json dosyası düzenlenir. RabbitMQ için ``` "RabbitMQ": {
        "Username": "hakan.tutar",
        "Password": "123456",
        "ConnectionAddress": "localhost"
    } ```, Sqlite için ```    "ConnectionStrings": {
        "API": "server=localhost;username=hakan.tutar;password=password;database=PatientDatabase"
    }, ``` alanları düzenlenir.
- Uygulama ayağa kaldırılır, migration ALINIR ve Database ve tablolar otomatik olarak oluşturulur.
- Kullanıma hazır!

