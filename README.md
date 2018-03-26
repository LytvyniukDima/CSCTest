# CSCTest Project
## Table of Contents
* [Tools](#tools)
* [Running](#running)
* [Documentation](#documentation)
## Tools
### .Net Core
To run WebApi project you need [.Net Core SDK](https://www.microsoft.com/net/download/windows/build). 
### Angular Cli
To run frontend project project you need [angular-cli](https://www.npmjs.com/package/angular-cli#installation).

## Running
### Start WebApi project
To start WebApi project navigate to folder CSCTest/CSCTest.Api and run command:
 ```bash
    dotnet run
```
### Start frontend project
If port where listening WebApi project isn't 5000, change port in variable "CSCTestUrl" in the file CSCTest/CSCTest.Client/csc-test-client/src/environments/evironment.ts.
To start frontend project navigate to folder CSCTest/CSCTest.Client/csc-test-client and run commands:
 ```bash
    npm install
    ng s
```
## Documentation
To see Web API documentation using Swagger navigate to localhost:{Web API port}/swagger/ in your browser. 

- [Account](#account)
- [Organizations](#organizations)
- [Countries](#countries)
- [Businesses](#businesses)
- [Families](#families)
- [Offerings](#offerings)
- [Departments](#departments)

### Account
All GET methods are accessible	without	authentication, for others mehtods User mest be authenticated.
#### Registration
To register use POST request to route /api/account/registration.  
Template of registration model:
```json
{
  "name": "Michael",
  "surname": "Jackson",
  "email": "michael@gmail.com",
  "address": "Indiana",
  "password": "password"
}
```
#### Authorization
After registration you need to authorize. Firstly you need to get jwt by POST request /api/account/token.  
Template of LogIn model:
```json
{
  "email": "michael@gmail.com",
  "password": "password"
}
```
If request is successful, you will have respond like this:
```json
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiY3VzdG9tZXIiLCJuYmYiOjE1MjIwNDUwMjAsImV4cCI6MTUyMjA0ODYyMCwiaXNzIjoiQ1NDVGVzdCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8ifQ.ktnDaZmje3fP9dU5jmjAf-aTMHTJ83ERkFKZF8q_EBU"
}
```
After that you need to copy only token without quotes. For our example:
```text
  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiY3VzdG9tZXIiLCJuYmYiOjE1MjIwNDUwMjAsImV4cCI6MTUyMjA0ODYyMCwiaXNzIjoiQ1NDVGVzdCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8ifQ.ktnDaZmje3fP9dU5jmjAf-aTMHTJ83ERkFKZF8q_EBU
```
After that click on button Authorize:
![Authorize button](https://github.com/LytvyniukDima/CSCTest/blob/master/ReadMeImages/AuthorizeButton.PNG)  
Finally input in field Value: "Bearer {token}. For example:
![Input token](https://github.com/LytvyniukDima/CSCTest/blob/master/ReadMeImages/InputToken.PNG)

### Organizations
### Countries
### Businesses
### Families
### Offerings
### Departments
