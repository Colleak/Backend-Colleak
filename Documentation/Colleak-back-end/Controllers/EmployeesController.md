De EmployeesController bevat de endpoints waarbij de CRUD acties kunnen worden uitgevoerd voor de Employees.

De standaard localhost URL is: https://localhost:7025/
De standaard cloud URl is: https://colleak-back-end.azurewebsites.net/

Achter de standaard URl kunnen verschillende endpoints worden geplaats met de verschillende CRUD(Create, Read, Update, Delete) aanvragen.
De verschillende endpoints zijn:

| Endpoint | Actie |
| ---------------- | --------------- |
| {GET}/api/Employees | vraag alle Employees op |
| {GET}/api/Employees/locationtrackedEmployees | vraag alle Employees op waar de AllowLocationTracking variabele op true staat |
| {GET}/api/Employees/trackedAndUntrackedEmployees | vraag alle Employees op die hebben geantwoord op de vraag of ze willen worden getracked, hierbij zitten de Employees die dat hebben geaccepteerd en geweigert |
| {GET}/api/Employees/connectedtodevice | vraag alle Employees op die geconnect zijn met een device op het netwerk |
| {GET}/api/Employees/Getrouterinfo | vraag alle alle router data op, dit is voor test en debug doeleinden |
| {GET}/api/Employees/{Id} | Vraag naar een specifieke Employee met een Id |
| {POST}/api/Employees | Maak een nieuwe Employee aan met de meegegeven data |
| {PUT}/api/Employees/{Id} | Pas een Employee aan met de meegegeven data |
| {PUT}/api/Employees/{Id}/ip | Pas een Employee IP adres aan |
| {PUT}/api/Employees/{Id}/updateMac | Pas een Employee mac adres aan met het gekoppelde apparaat op het netwerk |
| {PUT}/api/Employees/{Id}/updaterecentDevice | Pas een Employee recente connected router naam aan |
| {DELETE}/api/Employees{Id} | Verwijder een Employee met de meegegeven Id |

Als een endpoint wordt aangeroepen met de juiste data, dan wordt de service opdracht gegeven om deze data in de database op te halen, aan te maken, aanpassen of te verwijderen. Dit gebeurt dan in de Employee collection in de MongoDB-database van deze organisatie.