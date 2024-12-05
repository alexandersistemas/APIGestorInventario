-- Crear la base de datos SOGS
CREATE DATABASE GestorInventario
GO


-- Usar la base de datos SOGS
USE GestorInventario
GO

-- Crear una tabla para menu
CREATE TABLE [dbo].[Menu](
	[idMenu] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [varchar](50) NULL,
	[Icono] [varchar](50) NULL,
	[Url] [varchar](80) NULL
);

GO

--Tabla Base de productos con sus caracteristicas
CREATE TABLE Productos (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- Tabla para Administrar las entradas al inventario, con cantidades y fechas de caducidad.
CREATE TABLE EntradasInventario (
    IdEntrada INT IDENTITY(1,1) PRIMARY KEY,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    FechaCaducidad DATE NOT NULL,
    FechaEntrada DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);
GO

--Tabla para Rastrear las salidas del inventario, asociándolas a entradas específicas.
CREATE TABLE SalidasInventario (
    IdSalida INT IDENTITY(1,1) PRIMARY KEY,
    IdProducto INT NOT NULL,
    IdEntrada INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    FechaSalida DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
    FOREIGN KEY (IdEntrada) REFERENCES EntradasInventario(IdEntrada)
);
GO

-- Vista para calcular en tiempo real el estado del inventario basado en fechas de caducidad y cantidades disponibles.
CREATE VIEW EstadoInventario AS
SELECT 
    P.IdProducto,
    P.Nombre,
    P.Descripcion,
    EI.FechaCaducidad,
    SUM(EI.Cantidad) - ISNULL(SUM(SI.Cantidad), 0) AS CantidadDisponible,
    CASE 
        WHEN EI.FechaCaducidad < GETDATE() THEN 'Vencido'
        WHEN EI.FechaCaducidad BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE()) THEN 'Por vencer'
        ELSE 'Vigente'
    END AS Estado
FROM 
    Productos P
LEFT JOIN EntradasInventario EI ON P.IdProducto = EI.IdProducto
LEFT JOIN SalidasInventario SI ON EI.IdEntrada = SI.IdEntrada
GROUP BY 
    P.IdProducto, P.Nombre, P.Descripcion, EI.FechaCaducidad;


--Ingresos de prueba
INSERT INTO Productos (Nombre, Descripcion) VALUES ('Disco Duro', 'Disco duro de 2 teras');
INSERT INTO EntradasInventario (IdProducto, Cantidad, FechaCaducidad) VALUES (1, 50, '2024-12-31');
INSERT INTO SalidasInventario (IdProducto, IdEntrada, Cantidad) VALUES (1, 1, 10);


