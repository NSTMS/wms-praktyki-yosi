# WMS - system zarządzania magazynami

Ten projekt został wykonany w trakcie praktyk w firmie **YOSI s.c.** które odbywały się w okresie od 13.03.2023 do 14.04.2023. 


1. ### Setup Backend
    - otwórz rozwiązanie `wms-praktyki-yosi-api/wms-praktyki-yosi-api.sln`  w edytorze ***Visual Studio***
    - w pliku `appsettings.json` zmień wartości w **"ConnectionStrings"**: "database" na odpowiadające twojemu urządzeniu (potrzebujesz  [SQL Server Management Studio(SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms))
    - w zakładce  na górnym pasku wbierz ***Narzędzia*** >
        ***Menadżer Pakietów Nuget*** > ***Konsola menadżera pakietów***
    - w otwartej konsoli wklej następującą komendę:
        ```nuget
        Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
        Update-Database        
        ```
    - Uruchom projekt
2. ### Setup Frontend
    - Będąc w folderze projektu, przejdź do pliku `src/static/connection.json` i zmień wartość **ip** na wartość na IP swojego komputera - [sprawdź swoje IP](https://whatsmyip.com/)

    - uruchom `Terminal Powershell` w folderze projektu i wpisz następujące komendy:
    ```batch
    cd ./wms-praktyki-yosi-frontend
    npm i
    npm start
    ```

  

