using FirebirdSql.Data.FirebirdClient;

string connectionString = "User=SYSDBA;Password=masterkey;Database=ExampleData.fdb;ServerType=1;";
var con = new FbConnection(connectionString);
con.Open();

string sqlQuery = "SELECT * FROM \"Items\"";

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