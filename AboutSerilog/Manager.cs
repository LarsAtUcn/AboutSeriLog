using AboutSerilog;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

// Configure Serilog
#pragma warning disable CS0618 // Type or member is obsolete
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.MSSqlServer(
        connectionString: "Server=localhost; Database=Company; Integrated Security=true; Encrypt=False",
        tableName: "Logs",
        autoCreateSqlTable: true,
        columnOptions: new ColumnOptions {
            AdditionalDataColumns = new Collection<DataColumn> {
                new DataColumn{DataType = typeof(string), ColumnName = "EventId"},
                new DataColumn{DataType = typeof(string), ColumnName = "EventType" }
            }
        })
    .CreateLogger();
#pragma warning restore CS0618 // Type or member is obsolete

MyDataAccessClass mdaClass = new MyDataAccessClass(Log.Logger);
mdaClass.DoSomething();

//// Configure Serilog
//string? connectionStringLog = "";
//string? tableLog = "Logs1";
//if (!string.IsNullOrWhiteSpace(connectionStringLog) && !string.IsNullOrWhiteSpace(tableLog)) {
//    Log.Logger = new LoggerConfiguration()
//        .WriteTo.MSSqlServer(connectionStringLog, tableLog)
//        .CreateLogger();
//}



///* Read from database */
//// Setup connection
//string connectionStringCompany = @"Server=localhost; Database=Company; Integrated Security=true; Encrypt=False";
////connectionStringCompany = @"Server=localhost; Database=NotSoCoolShop; Integrated Security=true";
//SqlConnection connection = new SqlConnection(connectionStringCompany);

//// Create command
//string queryString = "select fname, minit, lname from employee";
//SqlCommand execSqlStatement = new SqlCommand(queryString, connection);

//// Execute database access
//try {
//    connection.Open();
//    SqlDataReader foundRecords = execSqlStatement.ExecuteReader();
//    if (foundRecords.HasRows) {
//        string fNam, mIni, lNam;
//        Console.WriteLine("Employees:");
//        while (foundRecords.Read()) {
//            fNam = foundRecords.GetString(foundRecords.GetOrdinal("fname"));
//            mIni = foundRecords.GetString(foundRecords.GetOrdinal("minit"));
//            lNam = foundRecords.GetString(foundRecords.GetOrdinal("lname"));
//            Console.WriteLine($"Employee: {fNam} {mIni} {lNam}");
//        }
//    } else {
//        Console.WriteLine("No Employees found!");
//    }
//} catch(SqlNullValueException snvExc) {
//    ProcessException(snvExc.Source + " - Manager", snvExc.GetType().ToString(), snvExc.Message);
//} catch (InvalidOperationException invalidOpExc) {
//    ProcessException(invalidOpExc.Source + " - Manager", invalidOpExc.GetType().ToString(), invalidOpExc.Message);
//} catch (SqlException sExc) {
//    ProcessException(sExc.Source + " - Manager", sExc.GetType().ToString(), sExc.Message);
//} catch (Exception exc) {
//    ProcessException(exc.Source, exc.GetType().ToString(), exc.Message);
//}

//// Release resources
//connection.Close();       // SqlConnection: Close and Dispose are functionally equivalent
//execSqlStatement.Dispose();


//void ProcessException(String src, string excType, string excMessage) {
//    Console.WriteLine(src);
//    Console.WriteLine(excType);
//    Console.WriteLine(excMessage);
//}