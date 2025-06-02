# Butunleme_Sınavı_Konsol_Tabanlı_Basit_RPG_Oyunu
# 🛡️ Basit RPG Oyunu (C# - OOP Projesi)

## 🎮 Proje Açıklaması

Bu C# ile geliştirilen basit RPG oyununda oyuncu, karşısına çıkan düşmanlara karşı savaşır. Oyuncu normal saldırılar yapabilir, mana harcayarak büyü saldırıları gerçekleştirebilir ve seviye atlayarak güçlenir.

---

## 🧱 Sınıf Yapısı ve OOP Açıklamaları

**OOP Kavramları Kullanımı:**

- ✅ Kalıtım (Inheritance): `Oyuncu` ve `Dusman` sınıfları `Karakter` sınıfından türetilmiştir.
- ✅ Polimorfizm (Polymorphism): `Vur()` metodu her sınıfta farklı çalışır (`override`).
- ✅ Kapsülleme (Encapsulation): Karakter verileri sınıf içerisinde kontrol edilir.
- ✅ Arayüz (Interface): `IOzelHareket` arayüzü, özel büyü saldırısını tanımlar.

**Sınıf Yapısı:**

- `Karakter`: Temel sınıf. Can, mana, vurma gibi temel özellikleri taşır.
- `Oyuncu`: Oyuncunun yönettiği karakter. XP ve Seviye gibi ekstra sistemlere sahiptir.
- `Dusman`: `Zombi`, `Goblin`, `Ejderha` sınıflarının ortak atasıdır.
- `Oyun`: Tüm akışı yönetir.

---

## 💻 Örnek Ekran Çıktısı
*** RPG ARENASI ***
İsminizi girin: TUNCAY
Hoş geldin TUNCAY!
Yeni düşman: Zombi (XP: 15)


>>> TUNCAY <<<
Can: 100/100
Mana: 30/30
Güç: 15


>>> Zombi <<<
Can: 50/50
Mana: 10/10
Güç: 10

1) Saldır
2) Büyü Saldırısı
3) Mana Yenile
4) Saldırı Geçmişi
5) Durum
Seçim: 2
TUNCAY özel saldırı yaptı! (41 hasar)
Zombi 41 hasar aldı! Kalan can: 9
--- Düşman hamlesi ---
Zombi mana yeniledi. Yeni mana: 10
Devam için Enter...

## 📂 Kaynak Dosyalar

Tüm `.cs` dosyaları `src/` klasörü içinde yer almaktadır:

- `Karakter.cs`
- `Oyuncu.cs`
- `Dusman.cs` (Zombi, Goblin, Ejderha dahil)
- `Oyun.cs`

---

## 🧭 Sınıf Diyagramı

![Sınıf Diyagramı]![Sınıf Diyagramı](https://github.com/user-attachments/assets/4466f606-9b82-40bf-880e-abf0bdd83299)


---

## 🚀 Nasıl Çalıştırılır?

1. Visual Studio 2022'de çözümü açın.
2. `Program.cs` dosyasındaki `Main` metodunu çalıştırın.
3. Seçimleri konsol üzerinden yaparak oyunu oynayın.

---

## 👤 Geliştirici

- **Ad Soyad**: Tuncay KAYALAR
- **GitHub**: https://github.com/TuncayKayalar
