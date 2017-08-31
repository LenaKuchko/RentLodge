# Rent Lodge

####  _August 31st, 2017_

#### By _**Olena Kuchko**_

## Description
Rent Lodge is a web application that allows a user to find and reserve an apartment for vacation. As a guest user can leave the review and put a rating for apartment. Also user as a property owner is able to offer his/her own house or apartment for rent. 
This application uses Asp.NET to demonstrate a basic, relational database using Microsoft SQL Server Management Studio.

## Specifications
 |Behavior| Input (User Action/Selection)| Output (Program Action)|
 |---|---|---|
 | User is able to view all available apartments for chosen location and dates | "USA", "Portland", "8/31/2017", "9/2/2017" | App displays all available apartments for Portland location from 8/31/2017 to 9/2/2017 |
 | User is able to view details of chosen appartments | Click on detail button | App displays the information about chosen apartment |
 | Authorized User is able to make a reservation | Click on rent button | App displays the information about reservation (dates, price, additional info) |
| Authorized User is able to view reservation history | Click on "My reservation" in menu | App displays all reservation that have been made by user |
 | Authorized User is able to leave a review and put a rating for an apartment | Fill up form for review and click on radiobutton for rating | App displays information about chosen apartment with all reviews and ratings |
 | Authorized User is able to create apartment for rent | Fill up form | App adds new apartment to database |
 |  Authorized User is able to view list of all apartments that belongs to her/him | Click "My apartment" in menu | App displays all apartments that belongs to user |
 | Authorized User is able to edit an apartment | Ex. change price | App displays apartment info with changes, adds changes to database  |
 | Authorized User is able to dalete an apartment | Click delete button | App deletes an apartment from database  |
 | Authorized User is able to add photos to her/his apartment  | Upload photo for chosen apartment | App adds photo to database and displays it |


## Setup/Installation Requirements
Visual Studio 2015
SQL Server Management Studio
Geocoder API Key (you can get it here for free https://developers.google.com/maps/documentation/geocoding/get-api-key and add it to EnvironmentVariables)
GeocoderKey = "YOUR_KEY"


#### _**Replicating/Editing this Project**_

* Clone this repository https://github.com/LenaKuchko/RentLodge.
* Using PowerShell navigate to the folder, where you clone project.
* Open project with Microsoft Visual Studio 2015.
* Using terminal, navigate to this folder C:\PATH_WHERE_YOU_CLONE_PROJECT\RentLodge\src\RentLodge>    
* Then run this command: dotnet ef database update Initial  


## Technologies Used

 ASP.NET Core, C#, _HTML, CSS, Materialize, Ajax, JavaScript, SSMS, Microsoft Visual Studio 2015

### License

 MIT

 Copyright (c) 2017 ** Olena Kuchko **