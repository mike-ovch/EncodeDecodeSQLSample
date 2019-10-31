/* Сценарий для подключения библиотеки в качестве сборки к MS SQL Server 2008 с возможностью использовать функции из нее */

/* Создаем в текущей базе сборку EncodeDecodeAssembly */
CREATE ASSEMBLY EncodeDecodeAssembly
FROM 'C:\Program Files\Microsoft SQL Server\MSSQL\Assembly\EncodeDecodeLibrary.dll'
WITH PERMISSION_SET = SAFE;
GO

/* Создаем в текущей базе функции, использующие код из сборки */
CREATE FUNCTION [dbo].EncodedToLower(@Input nvarchar(150))
RETURNS nvarchar(150)
                 /* Имя_сборки.[Пространство_имен.Класс].Метод */
AS EXTERNAL NAME EncodeDecodeAssembly.[EncodeDecodeLibrary.Library].EncodedToLower;
GO

CREATE FUNCTION [dbo].Decode(@Input nvarchar(150))
RETURNS nvarchar(150)
AS EXTERNAL NAME EncodeDecodeAssembly.[EncodeDecodeLibrary.Library].Decode;
GO

CREATE FUNCTION [dbo].Encode(@Input nvarchar(150))
RETURNS nvarchar(150)
AS EXTERNAL NAME EncodeDecodeAssembly.[EncodeDecodeLibrary.Library].Encode;
GO

/* Теперь можно в запросах использовать функции в виде dbo.Имя_Функции(параметр) */