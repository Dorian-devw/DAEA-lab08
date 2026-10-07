using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Biblioteca.Web.Models;
using System.Data;

namespace Biblioteca.Web.Repositorios
{
    public class SocioRepositorio
    {
        private readonly string _connectionString;

        public SocioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Socio>> ObtenerActivosAsync()
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<Socio>(
                "sp_Socios_ListarActivos",
                commandType: CommandType.StoredProcedure);
        }

        public async Task InsertarAsync(Socio socio)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(
                "sp_Socios_Insertar",
                new { socio.DNI, socio.Nombre, socio.Email },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PrestamoReporte>> ReportePrestamosAsync(DateTime desde, DateTime hasta)
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<PrestamoReporte>(
                "sp_Prestamos_Reporte",
                new { Desde = desde, Hasta = hasta },
                commandType: CommandType.StoredProcedure);
        }
    }
}
