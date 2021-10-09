Dossiers cibles:
***
c:\windows\temp
c:\users\user\appdata\local\temp

EF:
***
PM> Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=LogicielNettoyagePC;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

PM> add-migration InitialCreate
PM> update-database –verbose
PM> Remove-Migration

SQL:
***
INSERT INTO versions
(currentversion)
VALUES
('1.0.0.1');

UPDATE versions
SET currentversion = '1.0.0.0'