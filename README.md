# Cartel-Search-Products  

## Table of Contents
- [Overview :open_book:](#overview-open_book)
- [Documentation :sparkles:](#documentation-sparkles)
- [Take a Look :mag_right:](#take-a-look-mag_right)
- [AI :robot:](#ai-robot)
- [Setup and Installation :desktop_computer:](#setup-and-installation-desktop_computer)
- [Scenario :scroll:](#scenario-scroll)

## Overview :open_book:
*Cartel* is a versatile B2B platform designed for everyone! This web application is built using ASP .NET CORE MVC Framework and follows a robust 3-tier architecture:  
  
  **1) Presentation Layer:** aka Views using CSHTML.  
    
  **2) Application Layer:** Developed using C#.  
    
  **3) Data Layer:** Powered by a MySQL database.  
    
The platform offers a wide range of features and functionalities, which will be detailed further using our use case diagram. Some of the core capabilities include browsing products from other companies, purchasing them, and selling your own products to other businesses.  

## Documentation :sparkles:
### Idea Description :heavy_dollar_sign:  
The basic idea of this project ***Cartel*** is the following. A B2B marketplace where any business can find the products they need from various suppliers.  
This way the business can compare prices between suppliers and the suppliers can sell to many customers through our platform.  

### Use Case :infinity:  
The use case implemented in this project is the ***Search Products*** use case.  
This use case has the following features:
  1) **Browse Products and Pagination**  
  2) **Search Products using search bar (Search by Name, Supplier, Description, Category ...)**  
  3) **Apply filters to the products (Sort by Price, Sort by Rating)**
  4) **See products of a specific category (Dropdown list)**  
  5) **Choose a product to see details**  
  6) **See reviews of the product and post review**  
  7) **See Related products**  
  8) **Add Product to Cart**  
  9) **Remove Product from Cart**    
  10) **Login, Register and Logout**

### Future Improvements :soon:  
  1) Mobile Version: **Add Review**
  2) Mobile Version: **Join Cartel**
  3) Combine **Filters** and **Search Result** 

## Take a Look :mag_right:
**You can check the project live [here](http://ism.dmst.aueb.gr/ismgroup58)**  

## AI :robot:
**You can find all the ChatGPT prompts that helped me complete this project [here](https://github.com/gregalexan/Cartel-Search-Products/blob/master/AI.md)**  
Note: Free Version, like everyone.    

## Setup and Installation :desktop_computer:

### Prerequisites :clipboard:
- **Visual Studio**
- **Access to the ***Connection String*****

### Installing & Running :gear:
1) Clone the repository
```
git clone git@github.com:gregalexan/Cartel-Search-Products.git
cd Cartel-Search-Products
```
2) Edit appsetings.json
```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        "Default": "ADD CONNECTION STRING HERE"
    }
}
```
3) Run the application
```
dotnet run
```
The app is now running on http://localhost:5244

## Scenario :scroll:
In order to test the web application, you can execute the following scenario.

