# Take Home Engineering Challenge
This project is my attempt to complete the following challenge
https://github.com/seushermsft/Take-Home-Engineering-Challenge

## How it works
This solution is implemented as a .NET Core CLI with several other supporting projects.

![alt text](https://github.com/kwkraus/TakeHomeEngineeringChallenge/blob/master/images/SolutionExplorerView.png "Solution Explorer View")

Data Access is accomplished using Entity Framework Core backended with SQL Server (LocalDb)

`dotnet IWannago.dll inataxi --From 0 --To 44 --In Yellow -Date 1/1/2018`

`dotnet IWannago.dll inataxi --From 0 --To 44 --In Green -Date 1/1/2018`

`dotnet IWannago.dll inataxi --From 0 --To 44 --In ForHireVehicle -Date 1/24/2018`

## How it runs
In order to run the application locally, you need to download and unzip the [Test Data](https://sqlvakjnqkwpjkvio2.blob.core.windows.net/takehomeengineeringchallenge/tripdata.zip) into a folder called DataFiles in your Documents folder. (e.g c:\users\kkraus.NORTHAMERICA\Documents\DataFiles\\*.csv)

[![Build Status](https://dev.azure.com/kkraus/Take%20Home%20Engineering%20Challenge/_apis/build/status/kwkraus.TakeHomeEngineeringChallenge?branchName=master)](https://dev.azure.com/kkraus/Take%20Home%20Engineering%20Challenge/_build/latest?definitionId=19&branchName=master)
