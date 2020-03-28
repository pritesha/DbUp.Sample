SELECT * INTO 
#SeedCategory
FROM (VALUES
(1, 'Electronics'),
(2, 'Clothings'),
(3, 'Furnitures')) 
AS S([Id], [Title])

SELECT * INTO 
#SeedItem
FROM (VALUES 
(1, 1, 'Macbook', 1200),
(2, 2, 'T-shirt', 12.95),
(3, 3, 'Tall Boy', 375))
AS S([Id], [CategoryId], [Title], [Price])

SET IDENTITY_INSERT [Category] ON

MERGE [Category] AS [target]
USING #SeedCategory AS [source]
ON [target].[Id] = [source].[Id]
WHEN NOT MATCHED THEN
INSERT ([Id], [Title])
VALUES ([source].[Id], [source].[Title]);

SET IDENTITY_INSERT [Category] OFF

SET IDENTITY_INSERT [Item] ON

MERGE [Item] AS [target]
USING #SeedItem AS [source]
ON [target].[Id] = [source].[Id]
WHEN NOT MATCHED THEN
INSERT ([Id], [CategoryId], [Title], [Price])
VALUES ([source].[Id], [source].[CategoryId], [source].[Title], [source].[Price]);

SET IDENTITY_INSERT [Item] OFF