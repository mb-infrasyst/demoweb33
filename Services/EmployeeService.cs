using DemoWeb33.Models;
using System.Data.SqlClient;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
namespace DemoWeb33.Services
{

    // This service will interact with our Employee data in the SQL database
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection GetConnection()
        {
            var client = new SecretClient(new Uri("https://demoweb33-kv.vault.azure.net/"), new DefaultAzureCredential());

            KeyVaultSecret secret = client.GetSecret("demoweb33-secret-conn-01");
            string connectionString = secret.Value;

            return new SqlConnection(connectionString);
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> _employee_lst = new List<Employee>();
            string _statement = "SELECT Name, Position, Status, Department, ZipCode from Employees";
            SqlConnection _connection = GetConnection();

            _connection.Open();

            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Employee _employee = new Employee()
                    {
                        Name = _reader.GetString(0),
                        Position = _reader.GetString(1),
                        Status = _reader.GetString(2),
                        Department = _reader.GetString(3),
                        ZipCode = _reader.GetString(4)
                    };

                    _employee_lst.Add(_employee);
                }
            }
            _connection.Close();
            return _employee_lst;
        }

    }
}

