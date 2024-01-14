Pelny plik dokumentacji jest dostępny w fitwallet\doc. Jego treść jest przeklejona też tutaj, ale nie radzimy spojrzeć w powyższą ścieżkę. 

Instrukcja uruchomienia aplikacji:



Dane logowania, na których pracowaliśmy:
login
isybycnsmam
hasło
zaq1@WSX
Polecamy korzystać z tego użytkownika, tam jest dodane dużo danych i można dobrze sprawdzić działanie aplikacji.


Link do projektu w Figmie:
https://www.figma.com/file/5joPPq5aS1SpgkHBdT1U4O/FitWallet?type=design&node-id=0%3A1&mode=design&t=22rFORFBOSesHvKO-1



Dokumentacja:

FITWALLET

CEL
Główną funkcją FitWallet będzie umożliwienie użytkownikowi kontrolowania swoich finansów za pomocą bieżącego notowania wpływów oraz wydatków poprzez zapisywanie ich, z możliwością dodawania komentarzy, filtrowania pozycji. Wydatki będą podzielone na kategorie oraz do wszelkich aktywności finansowych dopisywane będą firmy lub osoby, które są z nimi powiązane. Oprócz tego, użytkownik będzie mógł zobaczyć wykresy sporządzone na podstawie jego ruchów na koncie (np. jaki procent stanowią wydatki w poszczególnych sklepach lub na poszczególne kategorie). Dodatkowo, system będzie wspierał tagi przypisane do transakcji, które użytkownik dowolnie będzie mógł modyfikować, a dzięki nim będzie mu łatwiej je analizować.

FUNKCJONALNOŚĆ

KONTA UŻYTKOWNIKÓW
Użytkownicy serwisu będą musieli się zarejestrować, by w pełni korzystać z serwisu. Podczas procesu rejestracji użytkownik będzie musiał podać imię, login, hasło, email oraz wypełnić captchę. Hasła zostaną następnie zaszyfrowane. Każda rejestracja użytkownika musi zostać zaakceptowana przez administratora. Niezalogowany użytkownik strony nie ma dostępu do funkcji strony, widzi jedynie stronę reklamową z opisem usług oraz formularzem rejestracji. 
Administrator będzie mógł zarządzać użytkownikami (akceptować ich lub odrzucać, usuwać konta) poprzez wbudowany panel. Ze względu na prywatność, nikt oprócz właściciela danych nie ma dostępu do żadnych treści (transakcje, stany kont itd.). Admin widzi tylko ogólny opis profilu (dane podane przy rejestracji z wyłączeniem hasła).

PORTFELE
Użytkownicy na swoich kontach będą mogli tworzyć portfele, czyli rozdzielać swoje fundusze na poszczególne podkonta. Przy tworzeniu portfela użytkownik będzie musiał podać kwotę, którą dysponuje na start oraz nazwać go (na przykład "PKO", +150 zł). Portfel będzie zawierał wszystkie transakcje przypisane do niego, a jego stan będzie przeliczany przy użyciu transakcji wprowadzanych przez użytkownika, zarówno dodatnich, jak i ujemnych (na przykład -50 zł z portfela "PKO"). 

TRANSAKCJE
Transakcja jest kontenerem, co ułatwi zarządzanie swoimi zakupami oraz samymi wpisami w aplikacji. Korzystając z takiego rozwiązania możemy edytować poszczególny element z paragonu stanowiącego całą transakcję, przy jednoczesnym zachowaniu rozdzielenia wielu transakcji jednego dnia. Jednocześnie możemy również dla wielu elementów zmienić naraz poszczególne informacje, np. datę czy sklep, co jest dużym usprawnieniem w porównaniu do konkurencyjnych rozwiązań. 
Przykładowo, robiąc zakupy w sklepie odzieżowym, do jednej transakcji możemy dodać buty za 200 zł oraz koszulkę za 50 zł. Jeśli po czasie użytkownik zwróci koszulkę do sklepu i otrzyma zwrot pieniędzy, z wpisanej wcześniej transakcji usuwa wyłącznie jedną pozycję, zamiast edytować całą transakcję i/lub inne pozycje. Dzięki podziałowi na transakcje możemy wybrać, jaki poziom szczegółów chcemy zobaczyć- od ogólnego spisu transakcji, do detali konkretnych pozycji w transakcji. 
Przy dodawaniu transakcji użytkownik będzie musiał uzupełnić do którego portfela będzie przypisana, wprowadzić datę, cel/źródło oraz opcjonalnie opis. Musimy dodać co najmniej jedną pozycję do transakcji, aby została utworzona. Każda pozycja musi zawierać nazwę, kategorię i kwotę. 
Kwoty transakcji będą liczone na podstawie sumy kwot ich pozycji. Użytkownik wprowadza kwoty ze znakami – lub + w zależności od tego, czy jest to przychód czy wydatek. 

KATEGORIE
Po rejestracji konta zostaną utworzone „standardowe kategorie” produktów, np. ubrania, jedzenie, opłaty stałe. Użytkownik będzie miał możliwość edytowania, dodawania i usuwania kategorii (również tych domyślnych), do których chce przypisywać pozycje z transakcji. Każda kategoria ma obrazek, który można wybrać z tych proponowanych lub wgrać samodzielnie. 
Kategorie mają system hierarchiczny i to od użytkownika zależy, jak bardzo je rozbuduje, przykładowo: może używać tylko etykiety „zachcianki” lub stworzyć drzewko kategorii:
![Drzewko kategorii](src\frontend\src\assets\categorytree.png)

STATYSTYKI
Jeśli użytkownik wyrazi taką chęć, będzie mógł w ten sposób łatwo grupować swoje zakupy i widzieć dzięki temu konkretniejsze statystyki swoich wydatków. Początkowo Dashboard będzie zawierał kilka domyślnych statystyk, np. wykres kołowy zawierający odzwierciedlenie procentowego udziału wydatków na poszczególne kategorie. Użytkownik otrzyma również możliwość tworzenia własnych wykresów ustalonych za pomocą filtrów. 
Sposób implementacji statystyk musimy jeszcze ustalić ze względu na komplikacje wynikające ze swobody użytkownika w tworzeniu własnych wykresów, w związku z tym ten temat zostanie rozwinięty w dalszych wersjach dokumentacji.

BAZA DANYCH
![Schemat bazy danych](src\frontend\src\assets\database.png)

ARCHITEKTURA

Projekt będzie realizowany w architekturze klient-serwer, gdzie serwer oparty jest o ASP.NET z wykorzystaniem Entity Framework do obsługi bazy danych. Interfejs użytkownika będzie oparty o Angular. Projekt będzie rozwijany z użyciem Gita i przesyłany na publiczne repozytorium na platformie GitHub. 

Narzędzia i technologie:

Backend: ASP.NET, C#, Entity Framework
Testy backend: NUnit, NSubstitute 
Frontend: Angular, HTML, CSS

MOŻLIWE ROZSZERZENIA PROJEKTU

W przypadku oficjalnego wydania aplikacji, dobrą ścieżką dla jej rozwoju mogłoby być połączenie aplikacji z bankiem użytkownika, dzięki czemu transakcje importowałyby się automatycznie. Jest to jednocześnie opcja, która wyróżniłaby aplikację wśród innych, konkurencyjnych produktów i mogłaby pozwolić na zwiększenie popularności, ale również to zagrożenie złej sławy czy wątpliwości ze strony użytkowników, zmniejszenie ich zaufania względem niej. Pewnego rodzaju szansą na sukces takiej funkcjonalności mogłoby być zaimplementowanie całości aplikacji jako dodatek do aplikacji banku,  jednakże jesteśmy prawie pewni, że co niektórzy doszukiwaliby się w tym spisku i inwigilacji. 

Innym pomysłem na rozwój, już nieco bardziej przyziemnym i możliwym do spełnienia, jest dodanie do aplikacji również skanera paragonów, aby używać jej również jako archiwum, zamiast zbierać dowody zakupu ręcznie. Ten ruch pozwoliłby również na porównywanie cen produktów spożywczych w dość prosty dla użytkownika sposób, a deweloperom na dodanie kolejnej funkcji- sugestii przepisów z wykorzystaniem posiadanych produktów, lub pozwolenie na wpisywanie do aplikacji posiłków i, co za tym idzie, liczenie kalorii. Wizja posiadania aplikacji łączącej w sobie wszystkie te cechy jest bardzo atrakcyjna, ale jednocześnie wymaga dużego nakładu pracy oraz dostępu do rozwiniętych już baz danych aplikacji pokroju Fitatu. 



