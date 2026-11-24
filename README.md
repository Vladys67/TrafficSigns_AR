# TrafficSigns_AR — Aplicație Educațională de Realitate Augmentată pentru Indicatoare Rutiere

**Autor:** Vladislav Cernov  
**Specialitatea:** 0211.4 Designul Jocurilor — Universitatea de Stat din Moldova (USM)  
**Versiune înghețată (Git Tag):** `individual-final`  
**Commit Hash:** `15f47b8`  
**Fișier Build Android (APK semnat):** `Cernov_Vladislav_IndividualFinal_v1.0.apk`

## 1. Descrierea proiectului
**TrafficSigns_AR** este o aplicație mobilă Android de Realitate Augmentată (AR) dezvoltată în **Unity 6.3 LTS (6000.3.27f1)** cu **Vuforia Engine**, destinată învățării interactive a 7 indicatoare rutiere (*STOP*, *Cedează trecerea*, *Drum cu prioritate*, *Accesul interzis*, *Limitare de viteză 50 km/h*, *Parcare*, *Trecere de pietoni*).

### Funcționalități principale
* **Scanare AR (Image Target):** Detectarea imaginii țintă (`TrafficSignsTarget.jpg`) și afișarea unui carusel interactiv în centrul ecranului.
* **Interacțiune tactilă & Zoom (Pinch):** Rotirea caruselului, zoom cu două degete (`PinchScale.cs`) și selectarea unui indicator (`SignTouchManager.cs`).
* **Panou informativ + Narațiune Audio:** Afișarea descrierii indicatorului (`InfoPanelManager.cs`) și redarea explicației vocale.
* **Recuperare la pierderea tracking-ului:** Mesaj de ghidare (`TrackingUIManager.cs`) la ieșirea din cadru și reluare automată.
* **Mod Studiază & Mod Quiz:** Parcurgere liberă (`StudyModeManager.cs`) și evaluare interactivă cu scor final (`QuizManager.cs`).

## 2. Cerințe de sistem și mediu de dezvoltare
* **Engine:** Unity `6000.3.27f1` (Universal Render Pipeline — Mobile Renderer)
* **SDK AR:** Vuforia Engine (Image Target Recognition)
* **Platformă țintă:** Android (API Level 24+ / testat pe Samsung Galaxy A35 `SM-A356B`)

## 3. Instrucțiuni de instalare și rulare
1. **Android (APK):** Transferă `Cernov_Vladislav_IndividualFinal_v1.0.apk` pe telefon, instalează-l, acordă permisiunea pentru **Cameră** și îndreaptă camera spre `Assets/Images/TrafficSignsTarget.jpg`.
2. **Unity Editor:** Rulează `git checkout individual-final`, deschide folderul în Unity Hub (`6000.3.27f1`) și încarcă scena `Assets/Scenes/TrafficSigns_AR.unity`.
