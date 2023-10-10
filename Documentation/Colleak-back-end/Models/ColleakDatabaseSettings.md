De ColleakDatabaseSettings-model bestaat om de informatie die wordt opgehaald hierin te zetten. Dit bevat onder andere de Connectionstring en de database naam met daaronder de verschillende collection namen.

Als er nieuwe collection worden aangemaakt moeten deze ook worden gedefinieerd in de ColleakDatabaseSettings-model, zodat ze kunnen worden opgehaald.

ColleakDatabaseSettings bestaat uit:
- string: ConnectionString
- string: DatabaseName
- string: EmployeeCollectionName