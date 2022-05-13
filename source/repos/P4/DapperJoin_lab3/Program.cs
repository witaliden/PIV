using Dapper;
using System.Data.SqlClient;
using System.Globalization;
using DapperJoin_lab3;

var cstring = @"Data Source=DESKTOP-JJ2EHR8\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";

var sql = "SELECT TerritoryId, TerritoryDescription, t.RegionId, r.RegionDescription FROM Territories t INNER JOIN Region r ON t.RegionId = r.RegionId WHERE t.TerritoryDescription LIKE CONCAT (@letter, '%')";
var conn = new SqlConnection(cstring);

Console.WriteLine("Podaj pierwszą literę regionu: ");
var userLetter = Console.ReadLine()?.Take(1);

var result = conn.Query<Territories, Region, Territories>(sql, (territory, region) =>
{
    territory.RegionDescription = region.RegionDescription;
    return territory;
}, new {letter = userLetter},
    splitOn: "RegionId");

foreach (var item in result)
{
    Console.WriteLine(
        $"{item.TerritoryId} {item.TerritoryDescription} {item.RegionId} {item.RegionDescription} ");
}