SET WDBX="C:\WDBXEditor\WDBX Editor.exe"

FOR %%A IN (%*) DO %WDBX% -sqlload -f %%A -b 12340 -m 2 -c "Server=127.0.0.1;Database=database;Uid=trinity;Pwd=trinity;"