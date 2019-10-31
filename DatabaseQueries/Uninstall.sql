/* Сценарий удаляет сборку из текущей базы данных */

/* Удаляем представление vMainData, использующее функции из сборки */
DROP VIEW dbo.vMainData;
GO

/* удаляем функции, использующие сборку */
DROP FUNCTION dbo.EncodedToLower;
DROP FUNCTION dbo.Decode;
DROP FUNCTION dbo.Encode;
GO

/* удаляем сборку EncodeDecodeAssembly */
DROP ASSEMBLY Sprut;
GO
