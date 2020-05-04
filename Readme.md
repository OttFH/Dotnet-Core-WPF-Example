#.NET Core
Ist eine der Standard Frameworks von C# und hebt sich von seinem Vorgänger .NET Framework vor allem durch Cross-Platform-Support und Open-Source Ansatz hervor. Wenn es um die Enwicklung von Desktop-Anwendung oder Web-Services (z.B. Rest-APIs) mit C# geht, ist es die erste Wahl.

Wie bei den meisten anderen Standard Frameworks von C# wie Xamerin, UWP, usw. ist der meiste Code quasi identisch (es existieren die gleichen Klassen, Methoden, usw.) zum .NET Framework und man als Entwickler hat keine Probleme beim Umsteig.


 ##Getting started
 - [Offizelles Getting Started](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro)
 1. Download aktuellste [SDK-Version](https://dotnet.microsoft.com/download/dotnet-core/3.1) (3.1.3)
 2. Installiere SDK
 3. Erstelle eines Projekts:
     * **Visual Studio**: Neues Projekt erstellen -> WPF-App (.NET Core) C# auswählen -> Projektname und Speicherort eingeben -> Fertig
     * **[Command Line](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new)**: ``dotnet new wpf -n <name>`` (erstellt das Projekt in einem Subordner)
 4. Ausführen:
     * **Visual Studio**: Auf den grünen Pfeil klicken oder F5
     * **[Command Line](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run)**: ``dotnet run``
 
 
 ##Inhalt
Dieses Projekt ist ein Demobeispiel einer WPF-Anwendung in .NET Core. Es werden unter anderem Folgendes bespielhaft gezeigt:
+ Window lifecycle
+ DataBinding
+ Asynchrone Programmierung (async/await)
+ Web requests
+ JSON parsing
+ File access