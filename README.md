# LuxRecruitment
Zadanie rekrutacyjne
## 🚀 Uruchamianie aplikacji
Po sklonowaniu repozytorium najlepiej zrobić restore zależności:
~~~bash
dotnet restore
~~~
Aby zobaczyć swaggera oraz główny projekt należy ustawić Multiple startup Projects:
- **LuxRecruitment.Web**
- **LuxRecruitment.WebAPI**

## Dane do zalogowania się:
- **Login: admin**
- **Hasło: admin123**

## 📋 Główne funkcjonalności
- **Logowanie z wykorzystaniem JWT** – zapewnia bezpieczeństwo oraz autoryzację użytkowników.
- **Prezentacja kursów walut** – dane pobierane z API NBP i wyświetlane w tabeli z paginacją i wyszukiwaniem.
- **Oddzielenie warstw aplikacji** – zastosowanie Clean Architecture dla przejrzystości i łatwego rozwoju projektu.
- **Nowoczesny interfejs użytkownika** – wykorzystanie Bootstrap, dostosowanego stylowania CSS oraz responsywnego designu.

## 🛠 Technologie użyte w projekcie
- **Backend:** .NET 8 (ASP.NET Core Web API, MVC)
- **Frontend:** Razor Pages, Bootstrap
- **Testy:** xUnit, Moq
- **Pozostałe:** Serilog (logowanie), JWT (autoryzacja)

## ℹ️ Ważne informacje
Z racji, że projekt jest **zadaniem rekrutacyjnym**:
- Pewne funkcjonalności zostały uproszczone lub nie są w pełni rozwinięte.
- Skupiłem się na tym, aby struktura i kod były zgodne z najlepszymi praktykami oraz "gotowe do dalszej rozbudowy w zespole".

Projekt pokazuje, jak mogę podejść do budowy większego systemu w sposób przemyślany i skalowalny. 😊

---
