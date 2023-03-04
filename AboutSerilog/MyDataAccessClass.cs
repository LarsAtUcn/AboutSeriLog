using Serilog;
using System.Data.SqlClient;

namespace AboutSerilog {
    public class MyDataAccessClass {

        private readonly ILogger _logger;

        public MyDataAccessClass(ILogger logger) {
            _logger = logger;
        }

        public void DoSomething() {
            string connectionString = "Server=localhost; Database=Company; Integrated Security=true; Encrypt=False";
            // Create command
            string queryString = "select fname,minit,lname from employee";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    SqlCommand execSqlStatement = new SqlCommand(queryString, connection);
                    SqlDataReader foundRecords = execSqlStatement.ExecuteReader();
                    if (foundRecords.HasRows) {
                        bool isNotNull;
                        string fNam, mIni, lNam;
                        Console.WriteLine("Employees:");
                        while (foundRecords.Read()) {
                            fNam = foundRecords.GetString(foundRecords.GetOrdinal("fname"));
                            isNotNull = !foundRecords.IsDBNull(foundRecords.GetOrdinal("minit"));
                            mIni = (isNotNull) ? foundRecords.GetString(foundRecords.GetOrdinal("minit")) : "";
                            //mIni = foundRecords.GetString(foundRecords.GetOrdinal("minit"));
                            lNam = foundRecords.GetString(foundRecords.GetOrdinal("lname"));
                            Console.WriteLine($"Employee: {fNam} {mIni} {lNam}");
                        }
                        _logger.ForContext("EventId", "5")
                            .ForContext("EventType", "Database success")
                            .Information("Successfully read from database.");
                    } else {
                        _logger.ForContext("EventId", "6")
                            .ForContext("EventType", "Database success")
                            .Information("No data found.");
                    }

                }
                catch (Exception ex) {
                    _logger.ForContext("EventId", "7")
                        .ForContext("EventType", "Database error")
                        .Error(ex, "Error performing database access");
                }
            }
            ((IDisposable)_logger).Dispose();
        }
    }
}
