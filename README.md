# Blazor-and-SqlTableDependency

## Introduction
This repository intends to introduce the use of [SqlTableDependency](https://www.nuget.org/packages/SqlTableDependency) NuGet package together with Blazor.

## How to run?
- Open Microsoft SQL Server Management Studio (SSMS) as administrator and execute the CreateSchema.sql file which will create the TableDependencyDB database.
- Then run the BlazorApp1 or IIS Express profile. Because it uses SSL accepts the test certificate it will create.
- With SSMS create, update, delete products on the Stocks table database and you will see on the browser that the list is always automatically updating.
 