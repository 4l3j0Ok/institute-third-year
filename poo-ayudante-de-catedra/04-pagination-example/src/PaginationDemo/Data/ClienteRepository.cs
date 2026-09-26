using Microsoft.Data.SqlClient;
using Pagination.Demo.Models;

namespace Pagination.Demo.Data;

public sealed class ClienteRepository(string connectionString)
{
    public void Initialize()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
            IF OBJECT_ID(N'Clientes', N'U') IS NULL
            BEGIN
                CREATE TABLE Clientes (
                    Id BIGINT IDENTITY(1, 1) PRIMARY KEY,
                    Name NVARCHAR(200) NOT NULL,
                    Email NVARCHAR(320) NOT NULL,
                    City NVARCHAR(100) NOT NULL
                );
            END;
            """;
        command.ExecuteNonQuery();
        if (IsSeedEnabled()) Seed(connection);
    }

    public int Count()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Clientes";
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public IReadOnlyList<Cliente> GetPage(int page, int pageSize)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Email, City FROM Clientes ORDER BY Id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
        command.Parameters.AddWithValue("@pageSize", pageSize);
        command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
        using var reader = command.ExecuteReader();
        var clientes = new List<Cliente>();
        while (reader.Read()) clientes.Add(new Cliente(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
        return clientes;
    }

    private static bool IsSeedEnabled() => bool.TryParse(Environment.GetEnvironmentVariable("PAGINATION_DEMO_SEED"), out var enabled) && enabled;

    private static void Seed(SqlConnection connection)
    {
        using var count = connection.CreateCommand();
        count.CommandText = "SELECT COUNT(*) FROM Clientes";
        if (Convert.ToInt32(count.ExecuteScalar()) > 0) return;

        using var transaction = connection.BeginTransaction();
        for (var i = 1; i <= 123; i++)
        {
            using var insert = connection.CreateCommand();
            insert.Transaction = transaction;
            insert.CommandText = "INSERT INTO Clientes(Name, Email, City) VALUES (@name, @email, @city)";
            insert.Parameters.AddWithValue("@name", $"Cliente {i:000}");
            insert.Parameters.AddWithValue("@email", $"cliente{i:000}@ejemplo.com");
            insert.Parameters.AddWithValue("@city", new[] { "Buenos Aires", "La Plata", "Córdoba", "Rosario" }[i % 4]);
            insert.ExecuteNonQuery();
        }
        transaction.Commit();
    }
}
