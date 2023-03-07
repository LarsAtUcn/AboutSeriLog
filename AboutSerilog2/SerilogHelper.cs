using Serilog.Sinks.MSSqlServer;
using System.Data;

namespace AboutSerilog2 {
    internal class SerilogHelper {

        public static ColumnOptions GetColumnOptions() {
            var logs2ColumnOptions = new ColumnOptions();

            // Adding all the custom columns
            logs2ColumnOptions.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn { DataType = SqlDbType.VarChar, ColumnName = "EventId", DataLength = 64, AllowNull = true},
                new SqlColumn { DataType = SqlDbType.VarChar, ColumnName = "EventType", DataLength= 256, AllowNull = true},
            }; 
            return logs2ColumnOptions;
        }

    }
}
