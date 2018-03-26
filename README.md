# CSCTest Project
## Table of Contents
* [Tools](#tools)
* [Running](#running)
* [Documentation](#documentation)
* [Frontend](#frontend)
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
<br> 
<br>  
Finally input in field Value: "Bearer {token}. For example:
![Input token](https://github.com/LytvyniukDima/CSCTest/blob/master/ReadMeImages/InputToken.PNG)
<br>
<br>
After you are successfuly authorized you can use all methods in Web Api.
### Organizations
#### Create
To create new Organization use POST request for route 
/api/organizations. Template of Create Organization model:  
```json
{
  "name": "MyOrganization",
  "code": "MyOrg",
  "type": "Limited partnerships"
}
```
User that create Organization will become owner of this organization and only owner can modify organization: create new countries in the organization, create new businesses in a country inside the organization etc.
Validation rule: Code of organization must be unique.
#### Update
To update Organization use PUT request for route /api/organizations/{id} where parameter id is Id of organization that user want to update. Template of Update Organization model:
```json
{
  "name": "Modified Organization",
  "code": "ModOrg",
  "type": "Limited partnerships"
}
``` 
#### Delete
To delete Organization use DELETE request for route /api/organizations/{id} where parameter id is Id of organization that user want to delete.
#### Get
To get all organizations use GET request for route /api/organizations.  
To get concrete organization use GET request for route /api/organizations/{id} where parameter id is Id of organization that user want to get.
### Countries
#### Create
To create new Country inside an organization use POST request for route /api/organizations/{organizationId}/countries where parameter organizationId is Id of organization where you want create new country. Template of Create Country model:
```json
{
  "name": "Germany",
  "code": "GER"
}
```
Validation rule: code of country must bu unique inside the organization.
#### Update
To update country use PUT request for route /api/countries/{id} where parameter id is Id of country that user want to update. Template of Update Country model: 
```json
{
  "name": "Deutschland",
  "code": "DEU"
}
```
#### Delete
To delete countrty use request DELETE for route /api/countries/{id} where parameter id is Id of country that user want to delete.
#### Get
To get all countries use GET request for route /api/countries.  
To get concrete country use GET request for route /api/countries/{id} where parameter id is Id of country that user want to get.  
To get all countries in concrete organization use GET request for route /api/organizations/{organizationId}/countries where parameter organizationId is Id of organization which countries user want to get.   
### Businesses
### Types of business
I create entity type of business that contains only name of business. Name of business is unique in types of business. So entity business contains only keys on country and type of business. If name of new business that created user isn't exist in types of business, will be created new type of business. 
#### Create
To create new business use POST request for route /api/countries/{countryId}/businesses where parameter countryId is Id of country where user want to create business and with name of business in body. Template to Create business:  
```json
"CEO"
```
Validation rule: Name of business must be unique inside country.
#### Update
User can only update name of type of business. If name of type of business will be updated, it will change name of all business that has reference on this type of business.  
To Update name of type of business use PUT request for route /api/business_types/{id} where id is Id of type of business that user want to update. Template to Update name of type of businness:
```json
"GIS"
```
#### Delete
To delete business use request DELETE for route /api/businesses/{id} where parameter id is Id of business that user want to delete.
#### Get
To get all businesses use GET request for route /api/businesses.    
To get concrete business use GET request for route /api/businesses/{id} where parameter id is Id business of that user want to get.  
To get all businesses in concrete country use GET request for route /api/countries/{countryId}/businesses where parameter countryId is Id of country which businesses user want to get.  
To get all types of businesses use GET request for route /api/business_types.
### Families
#### Types of family
I create entity type of family that contains name of family and reference on type of business of which family depend on. Name of family is unique for type of business. So entity family contains only keys on business and type of family. If name of new family that created user isn't exist in types of families for choosed by user type of business, will be created new type of family. 
#### Create
To create new family use POST request for route /api/businesses/{businessId}/families where parameter businessId is id of business where user want to create family and with name of family in body. Template of Create family:  
```json
"Cloud"
```
Validation rule: Name of family must be unique inside business.
#### Update
User can only update name of type of family. If name of type of family will be updated, it will change name of all families that has reference on this type of family.  
To Update name of type of family use PUT request for route /api/family_types/{id} where id is Id of type of family that user want to update. Template to Update name of type of family:
```json
"Cyber"
```
#### Delete
To delete family use request DELETE for route /api/families/{id} where parameter id is Id of family that user want to delete.
#### Get
To get all families use GET request for route /api/families.  
To get concrete family use GET request for route /api/families/{id} where parameter id is Id of family that user want to get.  
To get family in concrete business use GET request for route /api/businesses/{businessId}/families where parameter businessId is Id of business which families user want to get.   
To get all types of family use GET request for route /api/family_types.

### Offerings
#### Types of offering
I create entity type of offering that contains name of offering and reference on type of family of which offering depend on. Name of offering is unique for type of family. So entity offering contains only keys on family and type of offering. If name of new offering that created user isn't exist in types of offerings for choosed by user type of family, will be created new type of offering. 
#### Create
To create new offering use POST request for route /api/families/{familyId}/offerings where parameter familyId is Id of family where user want to create offering and with name of offering in body. Template of Create offering:  
```json
"Data center"
```
Validation rule: Name of offering must be unique inside family.
#### Update
User can only update name of type of offering. If name of type of offering will be updated, it will change name of all offerings that has reference on this type of offering.  
To Update name of type of offering use PUT request for route /api/offring_types/{id} where id is Id of type of offering that user want to update. Template to Update name of type of  offering:
```json
"Data cloud"
```
#### Delete
To delete offering use request DELETE for route /api/offerings/{id} where parameter id is Id of offering that user want to delete.
#### Get
To get all offerings use GET request for route /api/offerings.  
To get concrete offering use GET request for route /api/offerings/{id} where parameter id is Id of offering that user want to get.  
To get offering in concrete family use GET request for route /api/families/{familyId}/offerings where parameter familyId is Id of family which offerings user want to get.   
To get all types of offering use GET request for route /api/offring_types.  

### Departments
#### Create
To create new department use POST request for route /api/offerings/{offeringId}/departments where parameter offeringId is Id of offering where user want to create department and with name of department in body. Template to Create department:  
```json
"Department 1"
```
Validation rule: Name must of department must be unique inside offering.
#### Update
To Update use PUT request for route /api/departments/{id} where parameter id is Id of department that user want to update. Template to Update department:
```json
"Department 12"
```
#### Delete
To delete department use request DELETE for route /api/departments/{id} where parameter id is Id of department that user want to delete.
#### Get
To get all departments use GET request for route /api/departments.  
To get concrete department use GET request for route /api/departments/{id} where parameter id is Id of department that user want to get.  
To get department in concrete offering use GET request for route /api/offerings/{offeringId}/departments where parameter offeringId is Id of offering which departments user want to get.   
## Frontend
To see the tree you should navigate to port where angular server is listening on your computer. By default angular server is listening on localhost:4200.  
On home page you will see tree like that:  
![Tree](https://github.com/LytvyniukDima/CSCTest/blob/master/ReadMeImages/Tree.PNG)  
