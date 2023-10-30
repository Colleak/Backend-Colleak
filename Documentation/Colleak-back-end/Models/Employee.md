Het Employee-model bevat informatie over de Employee, zodat de data bij de juiste employee kan worden geplaatst op basis van een Id die automatisch wordt aangemaakt door de MongoDB-database.

Als de employee nieuwe variabelen krijgt, moeten deze ook worden gedefineerd, met vermelding of ze null kunnen zijn. anders wordt daar een error gegenereerd als dit wordt uitgevoerd en de variabele null blijft.

Employee bestaat uit:
- string?: Id
- string: EmployeeName
- string?: AllowLocationTracking
- string?: ConnectedRouterName