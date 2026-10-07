# DAEA Laboratorio 08 - Biblioteca MVC con Dapper

Este repositorio contiene la resolución del Laboratorio 08 del curso de Desarrollo de Aplicaciones Empresariales Avanzadas (DAEA). Consiste en una aplicación web ASP.NET Core MVC para la gestión de una biblioteca, utilizando ADO.NET (con Dapper) y Procedimientos Almacenados en SQL Server.

## Tecnologías Utilizadas

* **Framework:** ASP.NET Core MVC (.NET 10)
* **Base de Datos:** SQL Server
* **ORM:** Dapper (Micro-ORM) para el mapeo de datos asíncrono
* **Frontend:** Razor Views (`.cshtml`), HTML5, Bootstrap 5 (con personalización premium UI) y CSS3
* **Arquitectura:** Modelo-Vista-Controlador (MVC) y Patrón Repositorio

## Características Implementadas

1. **Gestión de Libros (CRUD Completo):**
   * Listado de libros activos con búsqueda por título.
   * Creación, edición, visualización de detalles y eliminación lógica de libros (marcando `Activo = 0`).
   * Validación de campos con `DataAnnotations` (ej. ISBN, stock de ejemplares).
2. **Gestión de Socios:**
   * Listado de socios registrados.
   * Registro de nuevos socios con validación de DNI único (captura de excepción SQL).
3. **Reporte de Préstamos:**
   * Visualización de un reporte que cruza información de Préstamos, Detalles, Libros y Socios.
   * Filtros por rango de fechas (Desde - Hasta) enviados mediante petición `GET`.
4. **Diseño y UI:**
   * Implementación del patrón PRG (Post/Redirect/Get) y uso de `TempData` para alertas de éxito.
   * Uso de vistas parciales (`_LibroRow.cshtml`) para reutilización de código.
   * Diseño moderno y responsivo con Bootstrap 5, animaciones CSS personalizadas y paleta de colores crema suave.

## Instalación y Configuración

### 1. Base de Datos
1. Abre **SQL Server Management Studio (SSMS)**.
2. Abre y ejecuta el script de creación inicial `BibliotecaDB_Setup.sql` que se encuentra en la carpeta raíz. Esto creará la base de datos `BibliotecaDB`, las tablas y algunos datos de prueba.
3. Abre y ejecuta el script `StoredProcedures.sql` (ubicado dentro de la carpeta `Biblioteca.web`) para compilar todos los procedimientos almacenados necesarios para el CRUD y los reportes.

### 2. Configuración de la Aplicación
1. Clona este repositorio o descárgalo a tu equipo local:
   ```bash
   git clone https://github.com/Dorian-devw/DAEA-lab08.git
   ```
2. Navega al directorio del proyecto web:
   ```bash
   cd Biblioteca.web
   ```
3. Abre el archivo `appsettings.json` y verifica la cadena de conexión bajo `ConnectionStrings:DefaultConnection`. Asegúrate de que el `Data Source` (nombre de servidor) corresponda a tu instancia local de SQL Server. Ejemplo:
   ```json
   "Server=localhost\\SQLEXPRESS;Database=BibliotecaDB;Trusted_Connection=True;Encrypt=False;"
   ```

### 3. Ejecución
1. Restaura los paquetes NuGet (Dapper y SqlClient):
   ```bash
   dotnet restore
   ```
2. Compila y ejecuta el proyecto:
   ```bash
   dotnet run
   ```
3. Abre tu navegador en la URL indicada en la consola (por ejemplo, `http://localhost:5168`).

## Convenciones de Desarrollo
Este proyecto fue versionado siguiendo las prácticas de **Trunk Based Development** y **Conventional Commits** simples en español.
