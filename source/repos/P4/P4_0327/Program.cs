using Dapper;
using P4_0327;
using System.Data.SqlClient;

var cstring = @"Data Source=DESKTOP-703RFM4\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
using var conn = new SqlConnection(cstring);

conn.Open();
int newNum = Convert.ToInt16(Console.ReadLine());
string? newDesc = Console.ReadLine().Trim();
string insertString = "INSERT into dbo.Region (RegionID, RegionDescription) VALUES ('" + newNum + "','" + newDesc + "' )";
SqlCommand insert1 = new(insertString, conn);
insert1.ExecuteNonQuery();

var regions = conn.Query<Region>("Select * FROM Region");

foreach (var item in regions)
{
    Console.WriteLine($"{item.RegionID}");
}

conn.Close();