Dit is een automatisch generated workflow door Azure.

De workflow "main_colleak-back-end" zorgt ervoor dat er een build gemaakt wordt van de repository op de main branch. Nadat de build goed is gemaakt wordt de build gedeployed naar Azure die verbonden is met de repository.

Wat de verschillende stappen doen wordt hieronder uitgelegd.
- **build**: De workflow job genaamd "build" draait op de laatste windows omgeving terwijl het de stappen binnen de job uitvoerd. Het doel van deze job is om een build te maken van de repository.
  
  - **actions/checkout@v2**: deze stap zorgt ervoor dat de workflow de root van de repository pakt waar het de volgende stappen kan uitvoeren.
  - **actions/setup-dotnet@v1**: deze stap maakt een .NET Core omgeving om de build in te maken. Dit gebuert in .NET versie 6.
  - **dotnet build --configuration Release**: deze command maakt een build van de repository waarbij de build configuration op "Release" wordt gezet. Release configuratie betekend dat de code geoptimaliseerd wordt for prestaties.
  - **dotnet publish -c Release -o ${{env.DOTNET_ROOT}}/myapp**: de command bestaat uit meerdere delen die apart worden uitgelegd.

    1. **dotnet publish**: dit betekend dat het de build aan het klaar maken voor deployment waarbij het alle code compiled, alle dependencies bij elkaar haalt die de deployment nodig heeft, en maakt een output folder dat alles bevat om de applicatie te runnen.
    2. **-c Release**: dit maakt duidelijk welke build configuratie er moet worden gebruikt. Hier zegt het dat de Release configuratie moet worden gebruikt die in de vorige stap is gebruikt.
    3. **-o ${{env.DOTNET_ROOT}}/myapp**: dit deel van de command zegt waar de output folder van de gepubliceerde applicatie wordt neergezet. Het maakt hier duidelijk dat het in de lokale .NET SDK locatie een subfolder wordt aan gemaakt met de naam "myapp".
   
   - **actions/upload-artifact@v2**: gegenereerde bestanden die in de huidige job zijn gemaakt en upload ze naar een plek om ze in een andere job te kunnen gebruiken. In deze workflow maakt het dudielijk dat de bestanden worden geupload naar .net-app vanuit de folder die in de vorige stap is gemaakt.
  
- **deploy**: De workflow job genaamd "deploy" draait op de laatste windows omgeving terwijl het de stappen binnen de job uitvoerd, de job heeft ook de eis dat de "build" job goed is uitgevoerd. De environment waarin de build wordt gedeployed is "Production". Het doel van deze job is om de build van de vorige job te deployen naar Azure.
  
  - 