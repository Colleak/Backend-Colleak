De EmployeesControllerTests Controleert of de good en bad flows van de EmployeesController goed verlopen. Hierbij wordt XUnit, Moq, FluentAssertions en AutoFixture gebruikt.

- Voor de Get all endpoint worden er 1 good flow en 1 bad flow getest.<br></br> De good flow moqt een lijst van Employees die de test ook terug verwacht na de uitvoering, als dit goed wordt uitgevoerd dan is deze test geslaagd.<br></br> De bad flow moqt geen data dus er kan ook niks worden meegegeven, hierbij wordt dan ook verwacht dat er geen data wordt gereturned. Als dit goed wordt uitgevoerd dan is deze test geslaagd.  
<br></br>
- Voor de Get by Id endpoint worden er 1 good flow en 2 bad flows getest.<br></br> De good flow moqt 1 Employee die de test ook terug verwacht als je de Id doorgeeft aan de controller. Als dit goed wordt uitgevoerd dan is deze test geslaagd.<br></br> Met de bad flows wordt:
  
  1. Er een Employee gemoqt maar er wordt een onbekend Id gebruikt om de employee op te halen wat resulteerd naar een NotFound response. Als dit goed wordt uitgevoerd dan is deze test geslaagd.
  2. Er een Employee gemoqt maar er wordt een leeg Id gebruikt om de employee op te halen wat resulteerd naar een NotFound response. Als dit goed wordt uitgevoerd dan is deze test geslaagd.
<br></br>
- voor de Post endpoint worden er 1 good en 1 bad flow getest.<br></br> De good flow moqt 1 Employee die terug wordt verwacht als er op basis van de gemoqte body een nieuwe employee wordt aan gemaakt.<br></br> De bad flow moqt 1 Employee waarvan de employeeName null wordt gezet want de naam is een verplicht veld, dus er wordt verwacht dat er een BadRequest response komt. Als dit goed wordt uitgevoerd dan is deze test geslaagd.
<br></br>
- Voor de Update endpoint worden er 1 good en 3 bad flows getest.<br></br> De good flow moqt 1 Employee om te updaten en 1 Employee waar de nieuwe geüpdate data in zit, het verwacht dat er op de eerste Employee de nieuwe data komt te staan van de 2de Employee.<br></br> Met de bad flows wordt:
  
  1. Er geen Id doorgegeven dus het verwacht dat het de Employee niet kan vinden dus een BadRequest response terug krijgt. Als dit goed wordt uitgevoerd dan is deze test geslaagd.
  2. Er wordt een standaard Employee gemoqt maar er wordt geen data doorgegeven om de standaard Employee te updaten, dus er wordt een BadRequest response verwacht. Als dit goed wordt uitgevoerd dan is deze test geslaagd.
  3. Er wordt een standaard Employee gemoqt maar niet alle required keys worden door gegeven om de standaard Employee te updaten, dus er wordt een BadRequest response verwacht. Als dit goed wordt uitgevoerd dan is deze test geslaagd.
<br></br>
- Voor de Delete endpoint worden er 1 good en 2 bad flows getest.<br></br> De good flow moqt een Employee om te deleten, dus als dit goed gebeurt wordt er een Ok response verwacht. Als dit goed wordt uitgevoerd dan is deze test geslaagd.<br></br> Met de bad flows wordt:

  1. Er wordt een standaard Employee gemoqt waar geen data inzit om een Employee te kunnen verwijderen, dus er wordt een BadRequest verwacht. Als dit goed wordt uitgevoerd dan is deze test geslaagd.
  2. Er wordt een standaard Employee gemoqt die geen geldig Id heeft om de Employee te vinden, dus er wordt een BadRequest verwacht. Als dit goed wordt uitgevoerd dan is deze test geslaagd.