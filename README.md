# HackerNews App

This application provides the top posts from Hacker News.

## Getting Started

This is a simple command line application using .net core 2.1.

### Prerequisites

* Visual Studio 2017 version 15.7 or higher.
* .NET Core 2.1 SDK.

### Installing

* Download or clone the repository.
* Launch the .sln file with Visual Studio.
* Install Newtonsoft.Json nuget package by James Newton-King.

In order to run the tests the following packages are required:
* Microsoft.NET.Test.Sdk by Microsoft
* xunit by James Newkirk, Brad Wilson
* xunit.runner.visualstudio by James Newkirk, Brad Wilson

## Run

* In the main project folder (\HackerNews where the .sln file is) hit Shift + Right click.
* Select "Open command window here" and type ```"dotnet restore"``` and hit enter.
* After it is complete type ```"dotnet publish -c Release -r <RID>"```,
where RID is the usual runtime identitfier, e.g. win-x64 or whatever platform you wish to build for (see the catalog [here](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)).
* After that is complete navigate to your <RID> folder by typing ```cd HackerNews\bin\Release\netcoreapp2.1\<RID>```, there you should find the HackerNews.exe file.
* From there you can run the application by typing ```hackernews --post n``` n the number of posts you want to display.
 
## Running the tests

From visual studio by hitting Ctrl+R,A

## Built With
* xUnit so that I was able to write simple tests quickly
* NewtsonSoft.Json to help me serialize and deserialize my objects
* [HackerNews](https://github.com/HackerNews/API) - The API used to retrieve the posts from HackerNews

## Authors

* **Georgios Akrivos** - [GeorgeSamples](https://github.com/GeorgeSamples)

## Licence

This project has an open licence.

## Acknowledgments
* All the brilliant books and online courses I went through.
* All the online content I referenced and the authors that contributed to them.
