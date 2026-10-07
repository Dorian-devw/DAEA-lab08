using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Biblioteca.Web.Models;
using System.Data;

namespace Biblioteca.Web.Repositorios
{
    public class LibroRepositorio
    {
        private readonly string _connectionString;

        public LibroRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Libro>> ObtenerActivosAsync()
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<Libro>(
                "sp_Libros_ListarActivos",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Libro>> BuscarPorTituloAsync(string titulo)
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<Libro>(
                "sp_Libros_BuscarPorTitulo",
                new { Titulo = titulo },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Libro?> ObtenerPorIdAsync(int libroId)
        {
            using var connection = GetConnection();
            return await connection.QueryFirstOrDefaultAsync<Libro>(
                "sp_Libros_ObtenerPorId",
                new { LibroId = libroId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task InsertarAsync(Libro libro)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(
                "sp_Libros_Insertar",
                new { libro.Titulo, libro.ISBN, libro.AutorId, libro.Ejemplares },
                commandType: CommandType.StoredProcedure);
        }

        public async Task ActualizarAsync(Libro libro)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(
                "sp_Libros_Actualizar",
                new { libro.LibroId, libro.Titulo, libro.ISBN, libro.AutorId, libro.Ejemplares },
                commandType: CommandType.StoredProcedure);
        }

        public async Task EliminarAsync(int libroId)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(
                "sp_Libros_Eliminar",
                new { LibroId = libroId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Autor>> ObtenerAutoresActivosAsync()
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<Autor>(
                "sp_Autores_ListarActivos",
                commandType: CommandType.StoredProcedure);
        }
    }
}
