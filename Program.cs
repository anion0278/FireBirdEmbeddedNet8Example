using FirebirdSql.Data.FirebirdClient;

//string connectionString = "User=SYSDBA;Password=masterkey;Database=ExampleData.fdb;ServerType=1;";
FbConnectionStringBuilder conn = new FbConnectionStringBuilder();
// next line basically to ensure I'm using Embedded mode...
conn.ServerType = FbServerType.Embedded;
// --- test connection - in embedded mode, should not have DataSource nor password
if (conn.ServerType == FbServerType.Embedded)
{
    conn.ClientLibrary = "fbclient.dll";
    conn.UserID = "SYSDBA";
    conn.Password = "masterkey";
    conn.Database = "employee.fdb";
}
else
{
    throw new ArgumentException();
}

var con = new FbConnection(conn.ConnectionString);
con.Open();

//string sqlQuery = "SELECT rdb$get_context('SYSTEM', 'ENGINE_VERSION') as version from rdb$database;"; //To check Firebird version
string sqlQuery = "SELECT emp_no, full_name, job_code, job_country FROM employee;";

using (var command = con.CreateCommand())
{
    command.CommandText = sqlQuery;
    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader[i] + "\t");
            }
            Console.WriteLine();
        }
        reader.Close();
    }
}

con.Close();
Console.ReadKey();