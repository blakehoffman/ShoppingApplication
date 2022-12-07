--INSERT ROLES
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
	VALUES (NEWID(), 'customer', 'CUSTOMER', NULL)

INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
	VALUES (NEWID(), 'Admin', 'ADMIN', NULL)

--INSERT ADMIN
INSERT INTO Administrators (Email) VALUES ('emailhere@test.com')