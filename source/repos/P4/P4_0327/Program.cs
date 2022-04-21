using Dapper;
using P4_0327;
using System.Data.SqlClient;

var cstring = @"Data Source=DESKTOP-JJ2EHR8\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
using var conn = new SqlConnection(cstring);

conn.Open();
int newNum = Convert.ToInt16(Console.ReadLine());
var newDesc = Console.ReadLine()?.Trim();
var insertString = "INSERT into dbo.Region (RegionID, RegionDescription) VALUES (@newNum, @newDesc)";
SqlCommand insert1 = new(insertString, conn);
insert1.Parameters.AddWithValue("@newNum", newNum);
insert1.Parameters.AddWithValue("@newDesc", newDesc);
int result = insert1.ExecuteNonQuery();

// Check Error
if(result < 0)
    Console.WriteLine("Error inserting data into Database!");

var regions = conn.Query<Region>("Select * FROM Region");

foreach (var item in regions)
{
    Console.WriteLine($"{item.RegionID}");
}

conn.Close();