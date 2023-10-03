De EmployeesController bevat de endpoints waarbij de CRUD acties kunnen worden uitgevoerd voor de Employees.

De standaard URl is: https://colleak-back-end.azurewebsites.net/

Achter de standaard URl kunnen verschillende endpoints worden geplaats met de verschillende CRUD(Create, Read, Update, Delete) aanvragen.
De verschillende endpoints zijn:

| Endpoint | Actie |
| ---------------- | --------------- |
| {GET}/api/Employees | vraag alle Employees op |
| {GET}/api/Employees/{Id} | Vraag naar een specifieke Employee met een Id |
| {POST}/api/Employees | Maak een nieuwe Employee aan met de meegegeven data |
| {PUT}/api/Employees/{Id} | Pas een Employee aan met de meegegeven data |
| {DELETE}/api/Employees{Id} | Verwijder een Employee met de meegegeven Id |

Als een endpoint wordt aangeroepen met de juiste data, dan wordt de service opdracht gegeven om deze data in de database op te halen, aan te maken, aanpassen of te verwijderen. Dit gebeurt dan in de Employee collection in de MongoDB-database van deze organisatie.