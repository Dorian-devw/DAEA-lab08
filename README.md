# DAEA Laboratorio 08 - Biblioteca MVC con Dapper

Este repositorio contiene la resolución del Laboratorio 08 del curso de Desarrollo de Aplicaciones Empresariales Avanzadas (DAEA). Consiste en una aplicación web robusta construida con **ASP.NET Core MVC** para la gestión de un sistema de biblioteca, utilizando **ADO.NET (con Dapper)** para un acceso a datos de alto rendimiento y **Procedimientos Almacenados** en SQL Server para toda la lógica de transacciones.

## Arquitectura y Tecnologías

El proyecto sigue el patrón de diseño Modelo-Vista-Controlador (MVC) apoyado por el Patrón Repositorio para lograr una correcta separación de responsabilidades:
* **Framework:** ASP.NET Core MVC (.NET 10)
* **Base de Datos:** Microsoft SQL Server
* **ORM:** Dapper (Micro-ORM) para ejecutar procedimientos almacenados de forma asíncrona.
* **Frontend:** Vistas fuertemente tipadas en Razor (`.cshtml`), HTML5, Bootstrap 5 (con estilos personalizados y animaciones interactivas) y CSS3.
* **Inyección de Dependencias:** Registro de repositorios `Scoped` en `Program.cs`.

## Especificaciones y Requerimientos Cumplidos

### 1. Base de Datos y Procedimientos Almacenados
Se partió de la base de datos `BibliotecaDB` y se crearon rutinas específicas para:
- **Libros:** Búsqueda por título, listado completo con `INNER JOIN` hacia `Autores`, obtención por ID, inserción, actualización, y **eliminación lógica** (actualizando el campo `Activo = 0` sin utilizar sentencias `DELETE` físicas).
- **Socios y Autores:** Procedimientos para listado de registros activos e inserción segura.
- **Reportes:** Procedimiento almacenado que cruza las tablas `Prestamos`, `DetallePrestamo`, `Libros` y `Socios` para filtrar mediante un rango de fechas.

### 2. Capa de Modelos (Models)
Se crearon las clases `Libro`, `Socio`, `Autor` y `PrestamoReporte`. A estas clases se les aplicó atributos de validación robustos (`Data Annotations`):
- `[Required]` para campos obligatorios.
- `[StringLength]` para restringir tamaño de texto en BD.
- `[Range]` para delimitar el número de ejemplares disponibles.
- `[EmailAddress]` para la validación de correos de socios.

### 3. Capa de Repositorios (Data Access)
En la carpeta `Repositorios` se diseñaron las clases `LibroRepositorio` y `SocioRepositorio`.
- Todas las ejecuciones hacia la base de datos usan `QueryAsync` y `ExecuteAsync`.
- Se especifica estrictamente el uso de `CommandType.StoredProcedure`.
- Los repositorios abstraen todo el código SQL para que los controladores se mantengan limpios sin acceder directamente a los datos.

### 4. Capa de Controladores (Controllers)
- **LibrosController:** Operaciones CRUD completas. Utiliza el patrón PRG (*Post/Redirect/Get*), manejando validaciones con `ModelState.IsValid` y guardando alertas flash con `TempData`.
- **SociosController:** Listado y creación, integrando la captura de excepciones SQL (`SqlException`) para evitar que el sistema falle cuando se intente registrar un DNI duplicado, mostrando en su lugar un mensaje en el formulario usando `ModelState.AddModelError`.
- **PrestamosController:** Renderiza el reporte basado en filtros de fechas proporcionados por la URL (petición GET), persistiendo el estado en la vista mediante `ViewData`.

### 5. Interfaz de Usuario y Vistas (Views)
- Vistas construidas usando Bootstrap 5 con un diseño enriquecido: barra de navegación oscura, tarjetas flotantes con bordes redondeados y un fondo de pantalla cálido en color crema para mejorar la estética.
- Animaciones CSS personalizadas (Efecto *Fade-In-Up*) para lograr transiciones fluidas de los componentes de las tablas, formularios y tarjetas informativas.
- Refactorización de código HTML mediante el uso de **Vistas Parciales (Partial Views)**, destacando `_LibroRow.cshtml` para renderizar asíncronamente las filas de las tablas.

---

## Guía de Instalación y Ejecución

Sigue estos pasos para levantar el proyecto localmente y probar todas sus funcionalidades.

### 1. Configuración de la Base de Datos
1. Abre **SQL Server Management Studio (SSMS)** u otra herramienta de gestión de SQL Server.
2. Abre y ejecuta el script inicial `BibliotecaDB_Setup.sql` que se encuentra en la carpeta raíz del repositorio. Este script recreará la base de datos `BibliotecaDB`, diseñará el esquema completo y poblará las tablas con información semilla (Mock data) de prueba.
3. Dirígete a la carpeta del proyecto web (`Biblioteca.web`) y abre el script `StoredProcedures.sql`. Ejecútalo sobre la base de datos `BibliotecaDB`. Esto instalará todos los procedimientos almacenados necesarios para ejecutar el CRUD de libros, socios y la generación de reportes.

### 2. Configuración de la Conexión a la Base de Datos
1. Clona el repositorio o descárgalo en tu computadora y ábrelo en tu IDE preferido (por ejemplo, Visual Studio o Visual Studio Code).
2. Localiza el archivo `appsettings.json` en el proyecto web.
3. Modifica la propiedad `DefaultConnection` para que su valor apunte a tu instancia local de base de datos.
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR_AQUI\\SQLEXPRESS;Database=BibliotecaDB;Trusted_Connection=True;Encrypt=False;"
   }
   ```

### 3. Ejecutar la Aplicación
1. Abre tu consola de comandos o terminal integrada en el directorio del proyecto (específicamente la ruta `.../Biblioteca.web`).
2. Restaura los paquetes NuGet y dependencias del sistema requeridas:
   ```bash
   dotnet restore
   ```
3. Compila e inicia la aplicación web en modo desarrollo:
   ```bash
   dotnet run
   ```
4. Navega en tu explorador hacia la ruta que indique la consola (usualmente `http://localhost:5000` o `https://localhost:5001`) para visualizar la plataforma de gestión interactiva de la biblioteca.
