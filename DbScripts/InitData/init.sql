
ALTER TABLE Questions 
ENABLE CHANGE_TRACKING  
WITH (TRACK_COLUMNS_UPDATED = ON)  


INSERT INTO [dbo].[PersonRoles]([Type]) VALUES('RND'), ('Support'),('Customer')
GO

INSERT INTO [dbo].[People]([Name],[RoleId],[Password]) VALUES
('JohnDoe',1, '123456'), 
('DavidShon',1, '123456'),
('KamaGreze', 1, '123456'),
('GraceTim', 2, '123456'),
('RivasRaul', 2, '123456'),
('SamBush',3, '123456'),
('MillsShone',3, '123456'),
('SupportTest1', 2, '123456'),
('SupportTest2', 2, '123456'),
('SupportTest3', 2, '123456'),
('SupportTest4', 2, '123456'),
('SupportTest5', 2, '123456'),
('SupportTest6', 2, '123456'),
('SupportTest7', 2, '123456'),
('SupportTest8', 2, '123456')
GO

INSERT INTO [dbo].[Families]([Name]) VALUES('Performance Engineering'), ('Manufacturing and Supply Chain'), ('Asset Performance Management'), ('Artificial Intelligence of Things Hub(AIoT)')
GO

INSERT INTO [dbo].[Products]([FamilyId],[Name],[Supportor_Id]) VALUES
(1,'Aspen HYSYS',5),
(2,'Aspen APC',4),
(1,'Aspen Capital Cost Estimator',8) ,
(1,'Aspen Basic Engineering',9),
(2,'Aspen GDOT',10),
(3,'Aspen Mtell',11),
(3,'Aspen ProMV',12),
(4,'Aspen Mtell',13),
(4,'Aspen ProMV',14)
GO





