/* Пример кода для создания индексированного представления на основе таблицы MainData. 
   Таблица содержит с закодированные данные (Фамилия, Имя и Отчество) 
   В представлении они выводятся в раскодированном виде */

/* Создаем представление с привязкой к схеме (иначе не сможем создать индексы) */
CREATE VIEW dbo.vMainData
WITH SCHEMABINDING
AS
SELECT ID, dbo.Decode(MD.LastName) AS [LastName], dbo.Decode(MD.FirstName) AS [FirstName], dbo.Decode(MD.SecName) AS [SecName]
FROM dbo.MainData AS MD
GO

/* Создаем индексы */
CREATE UNIQUE CLUSTERED INDEX PK_vMainData
   ON [dbo].[vMainData] ([ID]);
GO
CREATE NONCLUSTERED INDEX [IX_vMainData_PIN_LastName] ON [dbo].[vMainData] ([ID] ASC)
INCLUDE ([LastName])
GO
CREATE NONCLUSTERED INDEX [IX_vMainData_PIN_FirstName] ON [dbo].[vMainData] ([ID] ASC)
INCLUDE ([FirstName])
GO
CREATE NONCLUSTERED INDEX [IX_vMainData_PIN_SurName] ON [dbo].[vMainData] ([ID] ASC)
INCLUDE ([SurName])
GO
