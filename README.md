<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Microsoft_.NET_logo.svg/800px-Microsoft_.NET_logo.svg.png" width="20%"/>

# Backend
The Back end is made by: 
- Lean Meegdes
- Jelle Manders
- Jimmy Schuurmans
- Teun Hurkmans
- Ramon Peeters
- Jaimy Derks
- Art Nooijen
--- 

## Keuze ASP.NET
Wij hebben voor ASP.NET gekozen ondat deze goed functioneert met webapplicaties die veel javascript gebruiken, 
zoals wij in onze front-end doen. Daarnaast is er veel ervaring binnen de groep op het gebied van C#.
Ten slotte wordt er door ons gebruikgemaakt van Azure, een microsoft dienst die goed samenwerkt met ASP.NET.

## Beveiliging gevoelige data
In het begin van ons project hebben wij gebruikgemaakt van user secrets in visual studio. Hiermee kunnen wij de connectionstring
in de applicatie gebruiken zonder dat deze kwetsbaar is voor datalekken. Zo wordt de connectionstring niet opgeslagen in Github.
Een nadeel hiervan was dat de user secrets bij iedereen handmatig aangemaakt en toegevoegd moesten worden.

Uiteindelijk hebben wij onze applicatie gedeployed op Azure, en moesten we een alternatief zoeken voor de user secrets.
Gelukkig heeft Azure hier zijn eigen functionaliteit voor: De Azure keys. Dit is een soortgelijke functie als de user secrets in Visual studio,
Maar alleen binnen Azure zelf. Maar omdat de back-end is gedeployed kan iedereen hier gebruik van maken zonder dat de connectionstring kwetsbaar is.

## Lokaal starten van het programma
Bij he eerste lokale gebruik van de applicatie is het noodzakelijk om de repository te clonen. Belangrijk hierbij is om van de dev-branch een nieuwe branch aan te maken met de naam
van de nieuwe feature of user story als er nieuwe code wordt toegevoegd. Nadat dit is gefetched is handig om deze te builden voordat je de applicatie start.

## End-points 
Onze Services:
- GET/api/Employees
- POST/api/Employees
- GET/api/Employees/{id}
- PUT/api/Employees/{id}
- DELETE/api/Employees/{id}
