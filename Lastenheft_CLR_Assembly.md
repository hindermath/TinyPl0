# Lastenheft CLR Assembly

# Implementierungsphase: Code-Generierung (`--clr`)
* Integration eines neuen Backends, das statt des PL/0-Zwischencodes direkt CIL (Common Intermediate Language) erzeugt.
* Nutzung von `System.Reflection.Emit` zur Erstellung einer Assembly entweder im Speicher oder als Assenbly-Datei mit der Dateiendung`.dll`.

| Parameter/Switch | Beschreibung                                                                                                                 | 
|:----------------:|------------------------------------------------------------------------------------------------------------------------------|
|   --clr=inmem    | Erzeugt eine ausführbare .Net Assembly im Speicher und führt diese aus.                                                      |
|   `--clr=dll`    | Erzeugt eine ausführbare .Net Assembly-Datei mit der Datei-Endung '.dll', die mit dem Befehl 'dotnet' ausgeführt werden kann |

