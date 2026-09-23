using Microsoft.Data.SqlClient;
using SkiaPagination.Demo.Models;

namespace SkiaPagination.Demo.Data;

public sealed class CustomerRepository(string connectionString)
{
    public void Initialize()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
            IF OBJECT_ID(N'dbo.Customers', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Customers (
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
        command.CommandText = "SELECT COUNT(*) FROM dbo.Customers";
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public IReadOnlyList<Customer> GetPage(int page, int pageSize)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Email, City FROM dbo.Customers ORDER BY Id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
        command.Parameters.AddWithValue("@pageSize", pageSize);
        command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
        using var reader = command.ExecuteReader();
        var customers = new List<Customer>();
        while (reader.Read()) customers.Add(new Customer(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
        return customers;
    }

    /// <summary>Alta: inserta un cliente nuevo y devuelve el Id generado.</summary>
    public long Insert(Customer customer)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO dbo.Customers(Name, Email, City)
            OUTPUT INSERTED.Id
            VALUES (@name, @email, @city);
            """;
        command.Parameters.AddWithValue("@name", customer.Name);
        command.Parameters.AddWithValue("@email", customer.Email);
        command.Parameters.AddWithValue("@city", customer.City);
        return Convert.ToInt64(command.ExecuteScalar());
    }

    /// <summary>Modificación: actualiza los datos de un cliente existente.</summary>
    public void Update(Customer customer)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE dbo.Customers SET Name = @name, Email = @email, City = @city WHERE Id = @id";
        command.Parameters.AddWithValue("@name", customer.Name);
        command.Parameters.AddWithValue("@email", customer.Email);
        command.Parameters.AddWithValue("@city", customer.City);
        command.Parameters.AddWithValue("@id", customer.Id);
        command.ExecuteNonQuery();
    }

    /// <summary>Baja: elimina un cliente por Id.</summary>
    public void Delete(long id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM dbo.Customers WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    private static bool IsSeedEnabled() => bool.TryParse(Environment.GetEnvironmentVariable("PAGINATION_DEMO_SEED"), out var enabled) && enabled;

    private static void Seed(SqlConnection connection)
    {
        using var count = connection.CreateCommand();
        count.CommandText = "SELECT COUNT(*) FROM dbo.Customers";
        if (Convert.ToInt32(count.ExecuteScalar()) > 0) return;

        using var transaction = connection.BeginTransaction();
        for (var i = 1; i <= 123; i++)
        {
            using var insert = connection.CreateCommand();
            insert.Transaction = transaction;
            insert.CommandText = "INSERT INTO dbo.Customers(Name, Email, City) VALUES (@name, @email, @city)";
            insert.Parameters.AddWithValue("@name", $"Cliente {i:000}");
            insert.Parameters.AddWithValue("@email", $"cliente{i:000}@ejemplo.com");
            insert.Parameters.AddWithValue("@city", new[] { "Buenos Aires", "La Plata", "Córdoba", "Rosario" }[i % 4]);
            insert.ExecuteNonQuery();
        }
        transaction.Commit();
    }
}
