create database CookingDb;

use CookingDb;

create table Recipes (
    Id int primary key identity,
    Name nvarchar(100) not null,
    Category nvarchar(50),
    Ingredients nvarchar(max),
    Instructions nvarchar(max) not null
);

create table Categories (
    Id int primary key identity(1,1),
    Name nvarchar(100) not null
);

create table RequestLogs (
    Id int primary key identity(1,1),
    Url nvarchar(2048),
    RequestBody nvarchar(max),
    ResponseBody nvarchar(max),
    CreationDate datetime2,
    EndDate datetime2,
    StatusCode int,
    HttpMethod nvarchar(10)
);