# Take Home Engineering Challenge
This project is my attempt to complete the following challenge
https://github.com/seushermsft/Take-Home-Engineering-Challenge

## How it works
The Iwannago solution is implemented as a .NET Core CLI that uses a subset of the New York Taxi and Limousine Commission taxi trip data set.  The data originates from 3 different .csv files provided by the NY TLC, For Hire Vehicle data (FHV), Yellow taxi data, and Green taxi data.

Data Access is accomplished using Entity Framework Core backended with SQL Server (LocalDb).  The Repository pattern was followed that utilizes a .NET implementation of the [Specification Pattern](https://www.martinfowler.com/apsupp/spec.pdf) for creating a centralized abstraction for search criteria.

The solution implements .NET Core's implementation of Dependency Injection and Logging Framework using the NLog provider.

There are two main commands for this CLI:

- __Import Command__ :  Because this application is meant to be "self-contained", it has an import command that allows the user to import a predfined number trip rows from each dataset.  The goal of this method is to import as much data as is necessary to return a relevant result set.  __NOTE:__ due to time constraints the import process is time consuming.  It is recommended to import only 5000 rows or less from each dataset.

- __InATaxi Command__ : This command is the main command for defining filter criteria and executing a search against the data.



### Import command
Before the InATaxi command can be executed, you need to import the data into your SQL LocalDb.  The *.csv files that contain the data are provided in the link below.  These files can contain many millions of rows of data.  Because of this volume, the application requires a `--Count` switch to limit the number of rows imported for a particular file.  The following commands are used to accomplish this.




`dotnet IWannago.dll import --Type Yellow --Count 1000`

`dotnet IWannago.dll import --Type Green --Count 1000`

`dotnet IWannago.dll import --Type ForHireVehicle --Count 1000`


### InATaxi command
Once the test data has been imported into the database, the InATaxi command can be used to query taxi trip statistics for an individual day `--On`, for a specific taxi type `--In`, from a pickup location `--From` and a dropoff location `--To`

`dotnet IWannago.dll inataxi --From 0 --To 44 --In Yellow --On 1/1/2018`

`dotnet IWannago.dll inataxi --From 0 --To 44 --In Green --On 1/1/2018`

`dotnet IWannago.dll inataxi --From 0 --To 44 --In ForHireVehicle --On 1/24/2018`

## How it runs
In order to run the application locally, you need to download and unzip the [Test Data](https://sqlvakjnqkwpjkvio2.blob.core.windows.net/takehomeengineeringchallenge/tripdata.zip) into a folder called DataFiles in your Documents folder. (e.g c:\users\kkraus.NORTHAMERICA\Documents\DataFiles\\*.csv).

Because this solution utilizes Entity Framework Core Code First implementation, it is required to apply EF migrations to create the database schema.

Run the following command to create the database in your LocalDb store.

`dotnet ef database update --project Iwannago.Api`

__NOTE:__ the Iwannago.Api project had grand aspirations that were not to be.  But it still does have a purpose.  the EF migrations work very well on ASP.NET Core projects, so this is the target project that will be used to run the EF migrations.

Once the database has successfully been created, run the Import commands, outlined above, to start the data import and begin searching for data.


## Project Status

[![Build Status](https://dev.azure.com/kkraus/Take%20Home%20Engineering%20Challenge/_apis/build/status/kwkraus.TakeHomeEngineeringChallenge?branchName=master)](https://dev.azure.com/kkraus/Take%20Home%20Engineering%20Challenge/_build/latest?definitionId=19&branchName=master)
