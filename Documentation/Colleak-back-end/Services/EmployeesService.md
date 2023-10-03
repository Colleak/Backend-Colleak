De EmployeesService bevat de daadwerkelijke verbinding met de database, hierin wordt de specifieke connection string, database naam, en collection naam gebruikt om data in de database op te halen, toevoegen, aanpassen of te verwijderen.

Als er nieuwe specifieke acties moeten worden toegevoegd dat met de database te maken wordt dat hierin neergezet.
De EmployeesService heeft een interface genaamd IEmployeesService waardoor er gebruik kan worden gemaakt van dependency injection. Dus als de EmployeesService wordt aangepast, moet er hetzelfde gebeuren met de interface.
<br></br>
<br></br>
De methods die de EmployeesService heeft zijn:

- async Task<List<Employee GetEmployeeAsync(): deze method zorgt ervoor dat alle Employees worden opgehaald uit de database
- async Task<Employee?> GetEmployeeAsync(string id): deze method zorgt ervoor dat je een specifieke Employee kan ophalen met de bijbehorende Id. De vraagteken achter Employee zorgt ervoor dat het ook kan zijn dat de Employee niet hoeft te bestaan.
- async Task CreateEmployeeAsync(Employee newEmployee): deze method zorgt ervoor dat er een nieuwe Employee wordt aangemaakt in de database met de meegegeven data.
- async Task UpdateEmployeeAsync(string id, Employee updatedEmployee): deze method zorgt ervoor dat er een Employee kan worden aangepast, de Employee dat moet worden aangepast is te vinden met de Id dat wordt meegegeven en de aanpassingen zitten in de data dat wordt megegeven.
- async Task DeleteEmployeeAsync(string id): deze method zorgt ervoor dat er een Employee kan worden verwijdert, dit gebeurt door de Id dat wordt megegeven.