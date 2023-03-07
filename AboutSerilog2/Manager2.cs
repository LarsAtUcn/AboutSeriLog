using Serilog;
using Serilog.Sinks.MSSqlServer;
using AboutSerilog2;
using System.Configuration;

// Configure Serilog
var sinkOptions = new MSSqlServerSinkOptions() {
    TableName = "Logs2",
    AutoCreateSqlTable = true
};
string connectionString = ConfigurationManager.ConnectionStrings["ProdConnection"].ConnectionString;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.MSSqlServer(connectionString, sinkOptions, null, null,
        Serilog.Events.LogEventLevel.Information,
        columnOptions: SerilogHelper.GetColumnOptions())
    .CreateLogger();

MyDataAccessClass2 mdaClass = new MyDataAccessClass2(Log.Logger);
mdaClass.DoSomething();

/*
var sinkOptions = new MSSqlServerSinkOptions()
            {
                TableName = "Your table name",
                AutoCreateSqlTable = true
            };
            var connectionString = "Your connection string";
            
            logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .WriteTo.MSSqlServer(connectionString, sinkOptions,null, null,
            Serilog.Events.LogEventLevel.Information, columnOptions:     GetColumnOptions())
           .CreateLogger();
*/