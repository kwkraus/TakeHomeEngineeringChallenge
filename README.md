# Take Home Engineering Challenge
This project is my attempt to complete the following challenge
https://github.com/seushermsft/Take-Home-Engineering-Challenge

## How it works
This solution is implemented as a .NET Core CLI with several other supporting projects.

![alt text](https://github.com/kwkraus/TakeHomeEngineeringChallenge/blob/master/images/SolutionExplorerView.png "Solution Explorer View")

Data Access is accomplished using Entity Framework Core backended with SQL Server (LocalDb)

### Import Data command
The *.csv files that contain the data must be inserted into the LocalDb database.  These files can contain many millions of rows of data.  Because of this volume, the application requires a `--Count` switch to limit the number of rows imported for a particular file.  The following commands are used to accomplish this.


`dotnet IWannago.dll import --Type Yellow --Count 1000`

`dotnet IWannago.dll import --Type Green --Count 1000`

`dotnet IWannago.dll import --Type ForHireVehicle --Count 1000`


### InATaxi command

`dotnet IWannago.dll inataxi --From 0 --To 44 --In Yellow -Date 1/1/2018`

`dotnet IWannago.dll inataxi --From 0 --To 44 --In Green -Date 1/1/2018`

`dotnet IWannago.dll inataxi --From 0 --To 44 --In ForHireVehicle -Date 1/24/2018`

## How it runs
In order to run the application locally, you need to download and unzip the [Test Data](https://sqlvakjnqkwpjkvio2.blob.core.windows.net/takehomeengineeringchallenge/tripdata.zip) into a folder called DataFiles in your Documents folder. (e.g c:\users\kkraus.NORTHAMERICA\Documents\DataFiles\\*.csv)

[![Build Status](https://dev.azure.com/kkraus/Take%20Home%20Engineering%20Challenge/_apis/build/status/kwkraus.TakeHomeEngineeringChallenge?branchName=master)](https://dev.azure.com/kkraus/Take%20Home%20Engineering%20Challenge/_build/latest?definitionId=19&branchName=master)
