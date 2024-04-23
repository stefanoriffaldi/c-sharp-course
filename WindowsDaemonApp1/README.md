# Windows Service using BackgroundService (.NET Framework 8)

## Reference:

- [Microsoft Learn: Create Windows Service using BackgroundService](https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service)

## PUNTI DI ATTENZIONE

Come da [reference](#./README.md#reference), il progetto é stato concepito per produrre un singolo file eseguibile, completamente autosufficiente (al netto del .NET Framework).

## Logging su Windows Event

### ! ATTENZIONE ! c'é del troubleshooting

Fermo restando che devo ancora capire come funziona da Dependency Injection in questo contesto, ci sono diverse cose che non mi ispirano della gestione dei log.

1. La presenza dell'istruzione `LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);` a codice; sembra che l'impostazione avvenga da codice, senza la possibilità di configurare il tutto unicamente da file di configurazione. Non mi piace; da approfondire
2. Gli eventi non sembrano funzionare: con le impostazioni e il codice copincollati dall'esempio, il log non viene effettuato con successo e negli eventi di Windows appare il seguente messaggio di log
		Unable to log .NET application events. The source was not found, but some or all event logs could not be searched. To create the source, you need permission to read all event logs to make sure that the new source name is unique. Inaccessible logs: Security, State.

	Ora, non so quanti *"The Joke Service"* ci siano sotto Windows ma non penso sia un problema di univocità del nome, probabilmente bisogna prima creare la ***source*** (mi ricordo che FB mi ha accennato qualcosa del genere, ma non ricordo con precisione cosa). Tuttavia il fatto che questa cosa non venga fatta automaticamente, mi infastidisce non poco.

	Qualche ricerchina:

	- [How to create Windows EventLog source from command line? (stackoverflow)](https://stackoverflow.com/a/1036133/11289119)

	Testo la soluzione `eventcreate` (sono necessari i permessi di amministratore) dopo aver consultato l'help inline

		$ eventcreate /ID 1 /L Application /T Information  /SO "The Joke Service" /D "The Joke Service Event Source Creation"
		
		SUCCESS: An event of type 'Information' was created in the 'Application' log with 'The Joke Service' as the source.

	Lanciando l'eseguibile compilato, funziona e logga correttamente gli eventi; cosa interessante é che logga anche startup e shutdown senza (pare) esplicita gestione via codice, interessante; da approfondire.

## Build (fuori da VS)

	# pulizia directory
	$ rmdir /s/q C:\cs-release
	
	# comando funzionante (vedi sotto)
	$ dotnet publish --property:OutputPath="C:/cs-release/"

### ! ATTENZIONE ! c'é del troubleshooting (pure qui ? che palle ! e che ce dovemo fa ? stacce !)

Durante il process di build con il comando indicato nell'esempio si é sollevato un avvertimento

	$ dotnet publish --output "C:\cs-release"

	warning NETSDK1194: The "--output" option isn't supported when building a solution. Specifying a solution-level output path results in all projects copying outputs to the same directory, which can lead to inconsistent builds. [...\WindowsDaemonApp1.sln]

Anche se non sono un esperto, l'errore mi pare abbastanza parlante: avverte che impostare un unico folder di output a livello di *Solution* ( che per definizione puó contenere piú *Project* ) potrebbe portare a delle sovrapposizioni di file in caso di piú *Project* su una singola *Solution* ( mi vengono in mente i file di configurazione, che si chiamano tutti *appsettings.${ambiente}.json* )

Dei riferimenti per una possibile soluzione:

- [How to fix .NET7 NETSDK1194 error in an upgraded project? (stackoverflow)](https://stackoverflow.com/a/76651344/11289119)
- [Solution-level --output option no longer valid for build-related commands](https://learn.microsoft.com/en-us/dotnet/core/compatibility/sdk/7.0/solution-level-output-no-longer-valid)

Il comando "equivalente" (solo per *Solution Single Project*)

	$ dotnet publish --property:OutputPath="C:\cs-release\"

non funziona granché bene; se il comando senza property crea la sua alberatura con doppio folder

	\bin\Release\net8.0-windows\win-x64\
	\bin\Release\net8.0-windows\win-x64\publish\

**ATTENZIONE**: esplicitando l'OutputPath, utilizzare gli slash `/` **esplicitando quello finale**, altrimenti non funziona correttamente la creazione dell'alberatura; il folder `C:\cs-release\` viene correttamente creato, ma il subfolder `publish` no, crea un `C:\cs-releasefolder`.

	$ dotnet publish --property:OutputPath="C:/cs-release/"
	                  slash come separatore ^ e finale ^

