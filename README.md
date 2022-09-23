# EmailService

В качестве СУБД использовался PostgreSQL, в качестве ORM - Dapper.
Использованный почтовый сервис - gmail.

В файле restore.sql лежит запрос создания таблицы.
После того, как вы развернете бд локально, вставьте connection string в файл appsettings.json
В этом же файле нужно вставить данные почты gmail.