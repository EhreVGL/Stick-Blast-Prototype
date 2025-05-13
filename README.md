# Stick-Blast-Prototype
Unity 2D Mobile Game - Stick Blast Prototype 

# 🎮 Stick Blast – Geliştirme Raporu (4 Günlük Geliştirme Süreci)

## 🌟 Proje Tanımı
**Stick Blast**, oyuncuların önceden tanımlı "I, L, U" gibi şekilleri grid noktaları arasına sürükleyerek kare oluşturmaya çalıştığı mobil tabanlı bir puzzle oyunudur.  
Tamamlanan kareler puan kazandırır, satır veya sütun tamamen dolduğunda temizlenir.  
Oyunda güçlendiriciler, seviye sistemi, görsel animasyonlar ve sade UI yer almaktadır.

---

## 🟨 1. Gün – Temel Oyun Mekaniği ve Grid Sistemi

### 🎯 Hedefler
- Grid ve çubuk yerleştirme sistemini kurmak  
- Kare oluşturma ve skor sistemini kurmak

### 🔨 Yapılanlar
- Unity 2D Mobile Core template’iyle başlandı  
- 6x6 noktalı grid sistemi oluşturuldu  
- İki nokta arasına çubuk yerleştirerek kare oluşturma sistemi geliştirildi  
- Dört kenarı kapalı kare algılandı, skor eklendi  
- Kare highlight efekti eklendi  
- Combo sistemi geliştirildi  
- Score Manager yapısı kuruldu

---

## 🟨 2. Gün – Sürüklenebilir Şekiller ve Görsel Geliştirmeler

### 🎯 Hedefler
- Sürüklenebilir şekiller ile oynanabilirlik  
- Yerleşim doğruluğu ve önizleme sistemi  

### 🔨 Yapılanlar
- "I", "L", "U" gibi şekiller tanımlandı  
- Şekillerin rotasyonları JSON üzerinden organize edildi  
- 3 şekil içeren ShapeSlot sistemi geliştirildi  
- Ghost preview sistemi ile yerleşim önizlemesi  
- Sadece geçerli noktalara yerleşim kısıtı  
- 3 şekil yerleştikten sonra otomatik yenileme  
- Collider optimizasyonu ve sprite renklendirme  
- Grid noktaları bağlıysa aktif renkle değiştirildi

---

## 🟨 3. Gün – İleri Oyun Mekanikleri ve Renk Temaları

### 🎯 Hedefler
- Level sistemi, satır/sütun temizleme  
- Renk temaları ve efektler

### 🔨 Yapılanlar
- 200, 500, 900 puanda level geçişi  
- Grid sıfırlama ve skor koruma  
- Row/Column temizleme kontrolleri  
- “Line Clear” efekti ve animasyonlar  
- Her level’a özel renk temaları  
- Tüm nesneler tema ile yeniden boyandı  
- ScriptableObject ile merkezi tema yönetimi

---

## 🟨 4. Gün – Güçlendiriciler, UI, Menü ve Oyun Sonu

### 🎯 Hedefler
- Güçlendiriciler ile strateji derinliği  
- Menü ve UI ekranları

### 🔨 Yapılanlar
- 4 güçlendirici eklendi:
  - 🎯 **OK:** Satır ve sütunu temizler  
  - 💣 **BOMBA:** 3x3 alanı temizler  
  - ↩ **GERİ AL:** Son şekli geri alır  
  - 🔄 **KARIŞTIR:** Mevcut şekilleri yeniler
- UI üzerinden seçim sistemi  
- Geri alınan hamlede çizgiler ve kareler de silinir  
- Kullanılabilir şekil kalmayınca Game Over kontrolü  
- İstatistikler takip edildi: Skor, şekil sayısı, kare sayısı, satır/sütun temizleme  
- Level geçiş ve Game Over ekranları:
  - Koyu, blurlu arka plan  
  - Ortada istatistikler  
  - Ana Menü ve Devam Et seçenekleri
- Ana Menü sahnesi: Logo ve Start butonu  
- Start → oyun sahnesine geçiş  
- Oyun içi UI’ya “Ana Menüye Dön” butonu eklendi

---

## 🎯 Genel Sonuç

Bu 4 günlük geliştirme süreci sonunda Stick Blast:

✅ Öğrenmesi kolay, stratejik bir puzzle oyununa  
✅ Her seviyesi farklı renk temasına sahip görsel olarak tatmin edici yapıya  
✅ Kombolar, satır/sütun temizlemeler ve efektlerle dolu akıcı oynanışa  
✅ UI, Game Over, seviye geçişleri ve güçlendirici destekli mobil deneyime  

**başarıyla dönüşmüştür.**
