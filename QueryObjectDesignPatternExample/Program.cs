using QueryObjectDesignPatternExample;
using QueryObjectDesignPatternExample.Entities;
using System.Data.SqlClient;


#region Sadece Ado.NET Kullanımı
//DALContext context = new();
//SqlDataReader dr = await context.ExecuteQueryAsync("SELECT Name, Fiyat FROM Urunler");
//while (await dr.ReadAsync())
//    Console.WriteLine($"Id : {dr["Id"]}\tName : {dr["Name"]}\tStock : {dr["Stock"]}\tPrice : {dr["Price"]}");
//await context.CloseConnectionAsync();
#endregion
#region Manuel Parametreli Query Object Kullanımı
//Query<Product> query = new("Products");
//query.Add(Criteria<Product>.Contains("Name", "i", QueryLogicalOperator.And));
//query.Add(Criteria<Product>.GreaterThan("Stock", 100));
//string _query = query.GenerateWhereClause();

//Console.WriteLine(_query);

//DALContext context = new();
//SqlDataReader dr = await context.ExecuteQueryAsync(_query);
//while (await dr.ReadAsync())
//    Console.WriteLine($"Id : {dr["Id"]}\tName : {dr["Name"]}\tStock : {dr["Stock"]}\tPrice : {dr["Price"]}");
//await context.CloseConnectionAsync();
#endregion
#region Func Parametreli Query Object Kullanımı
Query<Product> query = new(p => new
{
    p.Id,
    p.Name
}, "Products");
query.Add(Criteria<Product>.GreaterThanOrEqual(p => p.Price, 10, QueryLogicalOperator.Or));
query.Add(Criteria<Product>.Equal(p => p.Name, "Çanta", QueryLogicalOperator.None));
string _query = query.GenerateWhereClause();

Console.WriteLine(_query);

DALContext context = new();
SqlDataReader dr = await context.ExecuteQueryAsync(_query);
while (await dr.ReadAsync())
    Console.WriteLine($"Id : {dr["Id"]}\tName : {dr["Name"]}");
await context.CloseConnectionAsync();
#endregion