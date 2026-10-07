USE BibliotecaDB;
GO

-- Libros: listado activos, búsqueda título, obtener id, insertar, actualizar, eliminar lógico
CREATE OR ALTER PROCEDURE sp_Libros_ListarActivos
AS
BEGIN
    SELECT L.LibroId, L.Titulo, L.ISBN, L.AutorId, L.Ejemplares, L.Activo, A.Nombre AS AutorNombre
    FROM Libros L
    INNER JOIN Autores A ON L.AutorId = A.AutorId
    WHERE L.Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_Libros_BuscarPorTitulo
    @Titulo NVARCHAR(200)
AS
BEGIN
    SELECT L.LibroId, L.Titulo, L.ISBN, L.AutorId, L.Ejemplares, L.Activo, A.Nombre AS AutorNombre
    FROM Libros L
    INNER JOIN Autores A ON L.AutorId = A.AutorId
    WHERE L.Activo = 1 AND L.Titulo LIKE '%' + @Titulo + '%';
END
GO

CREATE OR ALTER PROCEDURE sp_Libros_ObtenerPorId
    @LibroId INT
AS
BEGIN
    SELECT L.LibroId, L.Titulo, L.ISBN, L.AutorId, L.Ejemplares, L.Activo, A.Nombre AS AutorNombre
    FROM Libros L
    INNER JOIN Autores A ON L.AutorId = A.AutorId
    WHERE L.LibroId = @LibroId;
END
GO

CREATE OR ALTER PROCEDURE sp_Libros_Insertar
    @Titulo NVARCHAR(200),
    @ISBN NVARCHAR(50),
    @AutorId INT,
    @Ejemplares INT
AS
BEGIN
    INSERT INTO Libros (Titulo, ISBN, AutorId, Ejemplares, Activo)
    VALUES (@Titulo, @ISBN, @AutorId, @Ejemplares, 1);
END
GO

CREATE OR ALTER PROCEDURE sp_Libros_Actualizar
    @LibroId INT,
    @Titulo NVARCHAR(200),
    @ISBN NVARCHAR(50),
    @AutorId INT,
    @Ejemplares INT
AS
BEGIN
    UPDATE Libros
    SET Titulo = @Titulo,
        ISBN = @ISBN,
        AutorId = @AutorId,
        Ejemplares = @Ejemplares
    WHERE LibroId = @LibroId;
END
GO

CREATE OR ALTER PROCEDURE sp_Libros_Eliminar
    @LibroId INT
AS
BEGIN
    UPDATE Libros
    SET Activo = 0
    WHERE LibroId = @LibroId;
END
GO

-- Socios: listado activos, insertar
CREATE OR ALTER PROCEDURE sp_Socios_ListarActivos
AS
BEGIN
    SELECT SocioId, DNI, Nombre, Email, Activo
    FROM Socios
    WHERE Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_Socios_Insertar
    @DNI NVARCHAR(20),
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Socios (DNI, Nombre, Email, Activo)
    VALUES (@DNI, @Nombre, @Email, 1);
END
GO

-- Autores: listado activos
CREATE OR ALTER PROCEDURE sp_Autores_ListarActivos
AS
BEGIN
    SELECT AutorId, Nombre, Nacionalidad, Activo
    FROM Autores
    WHERE Activo = 1;
END
GO

-- Reporte Prestamos
CREATE OR ALTER PROCEDURE sp_Prestamos_Reporte
    @Desde DATETIME,
    @Hasta DATETIME
AS
BEGIN
    SELECT 
        S.Nombre AS SocioNombre,
        L.Titulo AS LibroTitulo,
        P.FechaLimite,
        P.Estado
    FROM Prestamos P
    INNER JOIN Socios S ON P.SocioId = S.SocioId
    INNER JOIN DetallePrestamo DP ON P.PrestamoId = DP.PrestamoId
    INNER JOIN Libros L ON DP.LibroId = L.LibroId
    WHERE P.FechaPrestamo >= @Desde AND P.FechaPrestamo <= @Hasta;
END
GO
