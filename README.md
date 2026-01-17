# CTF_Project
🕵️‍♂️ CTF Siber Sınav & Kriptoloji Simülasyonu

Bu proje, C# Windows Forms kullanılarak geliştirilmiş, Nesne Yönelimli Programlama (OOP) prensiplerine sadık kalınarak tasarlanmış eğitici bir Capture The Flag (Bayrağı Yakala) simülasyonudur.

Kullanıcıların temel kriptoloji (şifre bilimi) algoritmalarını öğrenmelerini, pratik yapmalarını ve siber güvenlik farkındalığı kazanmalarını amaçlar.

🚀 Proje Hakkında

Uygulama, bir "ajan/hacker" teması üzerine kuruludur. Kullanıcı sisteme kod adıyla giriş yapar ve zorluk seviyesi giderek artan şifreli mesajları çözmeye çalışır.

Temel Özellikler

İki Farklı Oyun Modu:

🎯 Sınav Modu: 15 seviyeden oluşan, puanlı ve süreli görev zinciri.

🛠️ Sandbox (Serbest) Mod: Sorulardan bağımsız, şifreleme araçlarını özgürce kullanabileceğiniz bir laboratuvar ortamı.

Siber İsviçre Çakısı: Uygulamanın sağ panelinde bulunan entegre araç seti sayesinde, kullanıcılar dış kaynaklara (web sitelerine) ihtiyaç duymadan şifreleri çözebilirler.

Lider Tablosu (High Score): JSON tabanlı yerel skor kaydı sistemi ile en yüksek puanları saklar ve gösterir.

Easter Egg (Sürpriz Yumurta): Her doğru cevapta arka planda toplanan harfler, oyun sonunda gizli bir mesaj oluşturur.

🛠️ Kullanılan Teknolojiler ve Mimari

Proje, temiz kod (Clean Code) ve sürdürülebilirlik ilkeleri gözetilerek geliştirilmiştir.

Dil: C# (.NET Framework)

Arayüz: Windows Forms (GDI+) - Tasarımcı (Designer) kullanılmadan, %100 kod tabanlı (Code-Behind) dinamik arayüz oluşturulmuştur.

Veri Yönetimi: JSON (Skor tablosu için yerel dosya yönetimi).

Tasarım Desenleri (Design Patterns)

Projede aşağıdaki yazılım mimarisi prensipleri uygulanmıştır:

Interface Kullanımı: Projedeki tüm soruların standart bir yapıya (Soru Numarası, İpucu, Cevap Kontrolü vb.) sahip olmasını garanti etmek için IQuestion arayüzü tanımlanmıştır. Tüm soru sınıfları bu arayüzü imzalayarak (implement) gerekli özellikleri barındırmayı taahhüt eder.

Inheritance (Miras Alma): Tüm pencereler (LoginForm, QuestionForm, SandboxForm), ortak bir görsel tema sağlayan CyberForm sınıfından türetilmiştir. Bu sayede kod tekrarı önlenmiş ve tek bir noktadan tüm uygulamanın teması yönetilebilir hale gelmiştir.

Polymorphism (Çok Biçimlilik) & Abstraction (Soyutlama): Sorular, soyut QuestionBase sınıfından türetilerek ortak davranışlar (Puan hesaplama vb.) tek bir merkezde toplanmış, ancak her sorunun içeriği (Soru metni, cevap) özelleştirilmiştir.

Factory Pattern (Fabrika Deseni): Bir sonraki seviyeye geçerken hangi soru sınıfının (Question1, Question2 vb.) yükleneceğine karar veren dinamik bir yapı (Factory Method) kullanılmıştır.

Encapsulation (Kapsülleme): Soru verileri ve skor yönetimi, dışarıdan doğrudan müdahaleye kapalı (protected/private set) özellikler (properties) ile korunmuştur.

🔐 Desteklenen Algoritmalar

"Siber İsviçre Çakısı" modülü aşağıdaki şifreleme ve kodlama türlerini destekler:

Kodlamalar: Base64 (Encode/Decode), Hex to String, Binary to String, Octal to String.

Şifreleme (Encryption): Caesar Cipher (Sezar Şifreleme), ROT13, Atbash.

Hashing: MD5 (Hash Oluşturma ve Brute-Force Sözlük Saldırısı Simülasyonu).

Diğer: Morse Code (Mors Alfabesi), Rail Fence (ZigZag), A1Z26 (Sayısal Alfabe), Reverse String (Ters Çevirme).

🏁 Kurulum ve Çalıştırma

Projeyi bilgisayarınıza indirin (Clone veya Download ZIP).

CtfProject.sln dosyasını Visual Studio ile açın.

Start tuşuna basarak projeyi derleyin ve çalıştırın.

Gereksinimler: .NET Framework 4.7.2 veya üzeri yüklü bir Windows işletim sistemi.

👨‍💻 Geliştirici Notu

Bu proje, sadece eğitim amacıyla geliştirilmiştir. Siber güvenliğe yeni başlayanlar ve CTF sınavlarına hazırlananlar için kriptoloji alanını simüle etmesini sağlar. İçerdiği "MD5 Kırma" gibi araçlar tamamen simülasyon amaçlıdır ve sadece eğitim için hazırlanmış basit sözlük listelerini kullanır (dictionary attack). Gerçek dünyadaki kriptografik güvenliği test etmek için kullanılmamalıdır.
