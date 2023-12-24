# Projekaz-ERS-Potrosnja

Evidencija prognozirane i ostvarene potrošnje električne energije
Klijent je kompanija za prenos električne energije. Aplikacija treba da se bavi evidencijom
prognozirane i ostvarene potrošnje električne energije po geografskim oblastima.
Slede korisnički zahtevi.

1. Uvoz podataka o prognoziranoj i ostvarenoj potrošnji
Aplikacija sadrži modul za uvoz podataka o planiranoj i ostvarenoj potrošnji električne 
energije. Uvoz se vrši iz XML fajlova ali je očekivano da postoji mogudnost korišdenja drugih 
formata. Fajlovi se importuju izborom opcije korisničkog interfejsa. Naziv fajla se sastoji od 
tipa fajla i datuma. Između ova dva podatka nalazi se donja crta. Tip fajla može biti “prog” za 
prognoziranu potrošnju i “ostv” za ostvarenu potrošnju. Datum je u obliku yyyy_mm_dd_hh. 
Primeri naziva fajlova su prog_2020_05_07.xml i ostv_2020_05_07.xml. Prvi fajl sadrži 
podatke o prognoziranoj potrošnji, a drugi fajl o ostvarenoj potrošnji, za dan 7. Maj 2020. 
godine.

XML fajl se može odnositi na jedno ili više geografskih područja. Podaci u svakom redu su: 
sat na koji se potrošnja odnosi, iznos potrošnje je u mW/h i šifra geografske oblasti. Za svaku 
geografsku oblast može biti onoliko redova koliko ima sati u danu (24, 23 ili 25). Ako za neku 
geografsku oblast broj sati ne odgovara broju sati u danu za koji se vrši unos, ceo fajl se 
odbacuje kao nevalidan, to se evidentirana u posebnoj audit tabeli. Podaci koje je potrebno 
evidentirati za nevalidan fajl su vreme pokušaja učitavanja, ime fajla, lokacija sa koje je fajl 
učitan i broj redova koje fajl poseduje.

Učitani podaci se upisuju u bazu podataka. Pojedinačno se upisuju svi redovi pročitani iz XML 
fajla, kao satni podaci o potrošnji električne energije za geografsko područje. Posebno se 
prate podaci o prognoziranoj i ostvarenoj potrošnji. Za svaki podatak o potrošnji prati se i 
ime fajla iz kojeg je podatak uvezen, vreme uvoza fajla i lokacija sa koje je fajl učitan.

2. Ispis podataka o ostvarenoj potrošnji
Aplikacija sadrži modul za ispis prognozirane i ostvarene potrošnje. Ispis se vrši na osnovu 
izabranog datuma i geografske oblasti. Za izabrani datum i izabranu geografsku oblast u 
tabelarnom prikazu ispisuje se sat, prognozira potrošnja, ostvarena potrošnja i relativno 
procentualno odstupanje ostvarene prognozirane potrošnje.

Tabelu sa relativnim odstupanjem mogude je eksportovati u CSV fajl. U okviru ove 
funkcionalnosti očekivano je da postoji mogudnost korišdenja drugih formata.

3. Evidencija geografskih područja
Geografsko područje sadrži ime geografskog područja i širinu geografskog područja 
(skradeno ime). Šifra geografskog područja nalazi se u XML fajlovima.
Kroz korisnički interfejs potrebno je da se vrši evidentiranje geografskih područja. Ukoliko se 
prilikom importa fajla pročita geografsko područje sa šifrom koja ne postoji u bazi podataka, 
geografsko područje sa tom šifrom se upisuje u bazu podataka, sa nazivom koji je isti kao 
šifra.

Tehnički i implementacioni zahtevi
1. Aplikacija treba da da bude razvijena poštujudi Agile/Scrum metodologiju razvoja, uz 
upotrebu AzureDevOps platforme.
2. Baza podataka može da bude implementirana kroz neki SUBP (MS SQL Server, Oracle), kroz 
neki od embeded sistema za baze podataka (SQLite, MS Access) ili kroz XML.
3. U okviru projekta potrebno je primeniti SOLID principe i načela čiste arhitekture.
4. Aplikacija treba da bude pokrivena Unit testovima.
