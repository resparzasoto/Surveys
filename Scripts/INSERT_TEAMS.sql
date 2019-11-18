DELETE FROM Surveys.dbo.Teams
GO

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 1, 'Alianza Lima', '#0000FF', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\alianzalima.png', SINGLE_BLOB) AS IMAGE

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 2, 'América', '#FFFF35', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\america.png', SINGLE_BLOB) AS IMAGE

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 3, 'Boca Juniors', '#0000FF', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\bocajuniors.png', SINGLE_BLOB) AS IMAGE

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 4, 'Caracas FC', '#7C0029', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\caracasfc.png', SINGLE_BLOB) AS IMAGE

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 5, 'Colo-Colo', '#0000FF', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\colocolo.png', SINGLE_BLOB) AS IMAGE

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 6, 'Peñarol', '#FFFF35', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\penarol.png', SINGLE_BLOB) AS IMAGE

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 7, 'Real Madrid', '#E612E3', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\realmadrid.png', SINGLE_BLOB) AS IMAGE

INSERT INTO Surveys.dbo.Teams(Id, Name, Color, Logo)
SELECT 8, 'Saprissa', '#7C0029', BulkColumn
FROM OPENROWSET (BULK 'C:\Xamarin\Surveys\Imagenes\saprissa.png', SINGLE_BLOB) AS IMAGE
GO