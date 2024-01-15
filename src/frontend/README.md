# Pelny plik dokumentacji jest dostępny w fitwallet\doc. Jego treść jest przeklejona też tutaj, ale nie radzimy spojrzeć w powyższą ścieżkę. 

# Instrukcja uruchomienia aplikacji:
<p>Zainstaluj nodejs 20.11.0 LTS.
Upewnij się, że razem z tym instalujesz npm.
Zainstaluj ASP.NET Core Runtime 8.0.1</p>

# Dane logowania, na których pracowaliśmy:
<p>login
isybycnsmam
hasło
zaq1@WSX
Polecamy korzystać z tego użytkownika, tam jest dodane dużo danych i można dobrze sprawdzić działanie aplikacji.</p>


# Link do projektu w Figmie:
https://www.figma.com/file/5joPPq5aS1SpgkHBdT1U4O/FitWallet?type=design&node-id=0%3A1&mode=design&t=22rFORFBOSesHvKO-1



# Dokumentacja:

## FITWALLET

# CEL
<p>Główną funkcją FitWallet będzie umożliwienie użytkownikowi kontrolowania swoich finansów za pomocą bieżącego notowania wpływów oraz wydatków poprzez zapisywanie ich, z możliwością dodawania komentarzy, filtrowania pozycji. Wydatki będą podzielone na kategorie oraz do wszelkich aktywności finansowych dopisywane będą firmy lub osoby, które są z nimi powiązane. Oprócz tego, użytkownik będzie mógł zobaczyć wykresy sporządzone na podstawie jego ruchów na koncie (np. jaki procent stanowią wydatki w poszczególnych sklepach lub na poszczególne kategorie). Dodatkowo, system będzie wspierał tagi przypisane do transakcji, które użytkownik dowolnie będzie mógł modyfikować, a dzięki nim będzie mu łatwiej je analizować.</p>

# FUNKCJONALNOŚĆ

# KONTA UŻYTKOWNIKÓW
<p>Użytkownicy serwisu będą musieli się zarejestrować, by w pełni korzystać z serwisu. Podczas procesu rejestracji użytkownik będzie musiał podać imię, login, hasło i email. Hasła zostaną następnie zaszyfrowane. Niezalogowany użytkownik strony nie ma dostępu do funkcji strony, widzi jedynie stronę informacyjną na temat aplikacji, logowanie, rejestrowanie oraz kontakt. <br>
Ze względu na prywatność, nikt oprócz właściciela danych nie ma dostępu do żadnych treści (transakcje, stany kont itd.). Admin widzi tylko ogólny opis profilu (dane podane przy rejestracji z wyłączeniem hasła).
</p>

# PORTFELE
<p>Użytkownicy na swoich kontach będą mogli tworzyć portfele, czyli rozdzielać swoje fundusze na poszczególne podkonta. Przy tworzeniu portfela użytkownik będzie musiał  nazwać go (na przykład "PKO”) oraz opcjonalnie wprowadzić opis. Portfel będzie zawierał wszystkie transakcje przypisane do niego, a jego stan będzie przeliczany przy użyciu transakcji wprowadzanych przez użytkownika, zarówno dodatnich, jak i ujemnych (na przykład -50 zł z portfela "PKO"). Zalecamy dodanie jakichś środków do portfela przed rozpoczęciem korzystania.</p>

# TRANSAKCJE
<p>Transakcja jest kontenerem, co ułatwi zarządzanie swoimi zakupami oraz samymi wpisami w aplikacji. Korzystając z takiego rozwiązania możemy edytować poszczególny element z paragonu stanowiącego całą transakcję, przy jednoczesnym zachowaniu rozdzielenia wielu transakcji jednego dnia. Jednocześnie możemy również dla wielu elementów zmienić naraz poszczególne informacje, np. datę czy sklep, co jest dużym usprawnieniem w porównaniu do konkurencyjnych rozwiązań. <br>
Przykładowo, robiąc zakupy w sklepie odzieżowym, do jednej transakcji możemy dodać buty za 200 zł oraz koszulkę za 50 zł. Jeśli po czasie użytkownik zwróci koszulkę do sklepu i otrzyma zwrot pieniędzy, z wpisanej wcześniej transakcji usuwa wyłącznie jedną pozycję, zamiast edytować całą transakcję i/lub inne pozycje. Dzięki podziałowi na transakcje możemy wybrać, jaki poziom szczegółów chcemy zobaczyć- od ogólnego spisu transakcji, do detali konkretnych pozycji w transakcji. <br>
Przy dodawaniu transakcji użytkownik będzie musiał uzupełnić do którego portfela będzie przypisana, opcjonalny opis, „kto?” oraz datę. „Kto?” to inna nazwa na źródło dochodu lub cel wydatku- np. „właściciel mieszkania”, „pracodawca”, „Biedronka”, „Lidl”.  Wartość transakcji jest sumą jej pozycji.<br>
Musimy dodać co najmniej jedną pozycję do transakcji, aby została utworzona. Każda pozycja musi zawierać nazwę, wartość (+/-), ilość sztuk, kategorię i opcjonalny opis. 
Kwoty transakcji będą liczone na podstawie sumy kwot ich pozycji. Użytkownik wprowadza kwoty ze znakami – lub + w zależności od tego, czy jest to przychód czy wydatek. 
 </p>

# KATEGORIE
<p>Po rejestracji konta należy utworzyć kategorie, które będą przypisywane do elementów transakcji. Mieliśmy w planach dodanie domyślnych kategorii, natomiast plany spotkały się z rzeczywistością. Użytkownik będzie miał możliwość edytowania koloru kategorii i usuwania kategorii, do których chce przypisywać pozycje z transakcji. <br>
Kategorie mają system hierarchiczny i to od użytkownika zależy, jak bardzo je rozbuduje, przykładowo: może używać tylko etykiety „zachcianki” lub stworzyć drzewko kategorii:
![Drzewko kategorii](src/frontend/src/assets/categorytree.png)</p>

# STATYSTYKI
<p>Kiedyś, jeśli użytkownik wyrazi taką chęć, będzie mógł w ten sposób łatwo grupować swoje zakupy i widzieć dzięki temu konkretniejsze statystyki swoich wydatków.  Użytkownik otrzyma również możliwość tworzenia własnych wykresów ustalonych za pomocą filtrów.<br>
 Na ten moment Dashboard będzie zawierał kilka domyślnych statystyk, np. wykres kołowy zawierający odzwierciedlenie procentowego udziału wydatków na poszczególne kategorie czy podstawowe statystyki sum dochodów, wydatków. <br>
Sposób implementacji statystyk musimy jeszcze ustalić ze względu na komplikacje wynikające ze swobody użytkownika w tworzeniu własnych wykresów, w związku z tym ten temat zostanie rozwinięty w dalszych wersjach aplikacji. Na razie wyglądają biednie.
</p>

# BAZA DANYCH
![Schemat bazy danych](src/frontend/src/assets/database.png)

# ARCHITEKTURA

<p>Projekt będzie realizowany w architekturze klient-serwer, gdzie serwer oparty jest o ASP.NET z wykorzystaniem Entity Framework do obsługi bazy danych. Interfejs użytkownika będzie oparty o Angular. Projekt będzie rozwijany z użyciem Gita i przesyłany na publiczne repozytorium na platformie GitHub. 
<br>
Narzędzia i technologie:
<br>
Backend: ASP.NET, C#, Entity Framework<br>
Testy backend: NUnit, NSubstitute <br>
Frontend: Angular, HTML, CSS</p>

# MOŻLIWE ROZSZERZENIA PROJEKTU

<p>W przypadku oficjalnego wydania aplikacji, dobrą ścieżką dla jej rozwoju mogłoby być połączenie aplikacji z bankiem użytkownika, dzięki czemu transakcje importowałyby się automatycznie. Jest to jednocześnie opcja, która wyróżniłaby aplikację wśród innych, konkurencyjnych produktów i mogłaby pozwolić na zwiększenie popularności, ale również to zagrożenie złej sławy czy wątpliwości ze strony użytkowników, zmniejszenie ich zaufania względem niej. Pewnego rodzaju szansą na sukces takiej funkcjonalności mogłoby być zaimplementowanie całości aplikacji jako dodatek do aplikacji banku,  jednakże jesteśmy prawie pewni, że co niektórzy doszukiwaliby się w tym spisku i inwigilacji. 

Innym pomysłem na rozwój, już nieco bardziej przyziemnym i możliwym do spełnienia, jest dodanie do aplikacji również skanera paragonów, aby używać jej również jako archiwum, zamiast zbierać dowody zakupu ręcznie. Ten ruch pozwoliłby również na porównywanie cen produktów spożywczych w dość prosty dla użytkownika sposób, a deweloperom na dodanie kolejnej funkcji- sugestii przepisów z wykorzystaniem posiadanych produktów, lub pozwolenie na wpisywanie do aplikacji posiłków i, co za tym idzie, liczenie kalorii. Wizja posiadania aplikacji łączącej w sobie wszystkie te cechy jest bardzo atrakcyjna, ale jednocześnie wymaga dużego nakładu pracy oraz dostępu do rozwiniętych już baz danych aplikacji pokroju Fitatu. </p>



