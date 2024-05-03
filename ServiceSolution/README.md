# Create Windows Service using BackgroundService

Reference: [Create Windows Service using BackgroundService](https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service)

Di seguito qualche appunto sugli step

## ✅ Create a new project

Al contrario ri quanto riportato nell'esempio, temite in wizard di VisualStudio non sono riuscito a creare un progetto `Worker Service`, non so perché (probabilmente mi manca qualche plugin di VS), tuttavia tramite CLI ci sono riuscito:

- Create una "Blank Solution"
- Aprire un terminal nel folder della Solution
- Eseguire il seguente comando

```.NET CLI
dotnet new worker --name <Project.Name>
```

- Tornare in VS, cliccare con il destro sulla Solution, quindi `Add` -> `Existing Project`, quindi selezionare il file `.csproj` all'interno della directory del progetto creato.

## ✅ Install NuGet package

Installato i pacchetti via CLI per semplicità, ancora mal digerisco la UI di VisualStudio, ma i pacchetti sono stati installati correttamente

## ✅ Update project file

Il progretto era già settato per lavorare con i *nullable reference types*

## ✅ Create the service (fatto)
fatto, custom
## ✅ Rewrite the Worker class
fatto, custom
## ✅ Rewrite the Program class
fatto, custom
## Publish the app

file Project modificato a mano (non sono riuscito a trovare tutti gli equivalenti via UI)
