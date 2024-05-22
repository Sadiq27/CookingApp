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
