<p align="center">
  <img src="design/megasoft-260x190.png" />
</p>

# Instalando la aplicación

1. Clone este repositorio (si utilizar Sql Server 2008 clona la branch de sqlserver2008)
2. Descargue [Visual Studio 2022 ](https://visualstudio.microsoft.com/vs/) y [Microsoft SQL Server Management Studio](https://aka.ms/ssmsfullsetup)
3. Ingrese al software **Sql Server Configuration Manager** e inicie el servicio **SQL Server (MSSQLSERVER)**
4. Habra **Microsoft SQL Server Management Studio**
5. Cree una base de datos llamada **AdMegasoft**
6. Habrá el proyecto que ha descargado con Visual Studio
7. En el buscador de Visual Studio escriba **Package Manager Console**
8. Dentro de la consola ejecute el siguientes comandos para crear las tablas necesarias en la base de datos: <br/><br/>
`dotnet tool install --global dotnet-ef` <br/><br/>
`dotnet ef migrations add Initial --verbose --project "src/Infrastructure" --startup-project "src/Web"` <br/><br/>
`dotnet ef database update --verbose --project "src/Infrastructure" --startup-project "src/Web"`  

9. Le recomendamos descargar algunos de los siguientes software para trabajar con GIT: <br/>[GitHub Desktop](https://desktop.github.com/) - [TortoiseGit](https://tortoisegit.org/) - [GitKraken](https://www.gitkraken.com/)


10. Por ultimo ejecutar la siguiente consulta en la base de datos:

```


USE [AdMegasoft]
GO

-- USER
INSERT INTO [dbo].[Users]
           ([Name]
           ,[Password]
           ,[Enabled])
     VALUES
           ('admin'
           ,'admin'
           ,1)
GO

-- ROL
USE [AdMegasoft]
GO

INSERT INTO [dbo].[Roles]
           ([Name]
           ,[Description])
     VALUES
           ('Admin'
           ,'Administrador del sistema')
GO

-- Groups

USE [AdMegasoft]
GO

INSERT INTO [dbo].[PermissionGroups]
           ([Name])
     VALUES
           ('Usuarios') 
GO

INSERT INTO [dbo].[PermissionGroups]
           ([Name])
     VALUES
           ('Roles')
GO

INSERT INTO [dbo].[PermissionGroups]
           ([Name])
     VALUES
           ('Permisos')
GO

INSERT INTO [dbo].[PermissionGroups]
           ([Name])
     VALUES
           ('Empresas')
GO

-- Permisos

USE [AdMegasoft]
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (1 -- Usuarios
           ,'Permisos.Usuarios.Ver'
           ,'Puede ver el listado de usuarios')
GO


INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (1 -- Usuarios
           ,'Permisos.Usuarios.Crear'
           ,'Puede crear usuarios')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (1 -- Usuarios
           ,'Permisos.Usuarios.Editar'
           ,'Puede editar usuarios')
GO


INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (2 -- Roles
           ,'Permisos.Roles.Ver'
           ,'Puede ver los roles')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (2 -- Roles
           ,'Permisos.Roles.Crear'
           ,'Puede crear roles')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (2 -- Roles
           ,'Permisos.Roles.Editar'
           ,'Puede editar roles')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (2 -- Roles
           ,'Permisos.Roles.Eliminar'
           ,'Puede eliminar roles')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (3 -- Permisos
           ,'Permisos.RolPermisos.Ver'
           ,'Puede ver los permisos')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (3 -- Permisos
           ,'Permisos.RolPermisos.Editar'
           ,'Puede editar los permisos')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (4 -- Empresas
           ,'Permisos.Empresas.Ver'
           ,'Puede ver el listado de empresas')
GO


INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (4 -- Empresas
           ,'Permisos.Empresas.Crear'
           ,'Puede crear empresas')
GO

INSERT INTO [dbo].[Permissions]
           ([PermissionGroupId]
           ,[Name]
           ,[Description])
     VALUES
           (4 -- Empresas
           ,'Permisos.Empresas.Editar'
           ,'Puede editar las empresas existentes')
GO


-- Permisos del rol admin

INSERT INTO [dbo].[RolePermissions]
           ([RoleId]
           ,[PermissionId])
     VALUES
           (1,1),
           (1,2),
           (1,3),
           (1,4),
           (1,5),
           (1,6),
           (1,7),
           (1,8),
           (1,9),
           (1,10),
           (1,11),
           (1,12)
GO

-- Rol asignado al usuario admin

INSERT INTO [dbo].[UserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (1
           ,1)
GO

```
