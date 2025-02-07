# Cartel-Search-Products  

## Table of Contents
- [Overview :open_book:](#overview-open_book)
- [Documentation :sparkles:](#documentation-sparkles)
- [Take a Look :mag_right:](#take-a-look-mag_right)
- [AI :robot:](#ai-robot)
- [Setup and Installation :desktop_computer:](#setup-and-installation-desktop_computer)
- [Scenario :scroll:](#scenario-scroll)
- [License :balance_scale:](#license-balance_scale)

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
Please execute this in *desktop* mode.  
  
| A/A  | Page   | Data Entry   | Expected Result |
| ---- | ------ | ------------ | --------------- | 
| 1    | Home   | Type ```chair``` into the search bar and press enter | Shows all the products that have in their title or description the word: ***chair*** |
| 2    | Home   | Type ```tree``` into the search bar and press enter | Shows the message: ***No products found matching: tree*** |
| 3    | Home   | Click the dropdown list ***Categories*** and choose **Bathroom** | Shows all the products with category: **Bathroom** |
| 4    | Home   | Click the dropdown list ***Default Sorting*** and change it to **Sort by Rating** | See all the products sorted based on the rating (5 stars first) |
| 5    | Home   | Select again the ***Default Sorting*** and then click on the product **Luxe Loom Towels 100% Cotton** | Redirect to Details page |
| 6    | Details | Click on **Add your own review** | Redirect to Join Cartel with error message ***You have to log in to post a review*** |
| 7    | Join    | Click ***Register*** and add the following information: Company Name: ```cartelsearchproducts```  Email: ```info@cartelsearchproducts.com```  Company SSN: ```123456123```   Username: ```cartelsearchproducts``` Password: ```abc```  then click **Register** | Shows error message: ***There are errors in the register form*** |
| 8    | Join    | Click ***Register*** again and add the same information, this time Password: ```123456789``` | Redirect to the home page |
| 9    | Home    | Click again the **Luxe Loom Towels 100% Cotton** and then **Add your own review**. Then select 5 stars (because the product is excellent) and type a message if you like | Redirect to the Details page of the product. |
| 10   | Details | Click ***View More*** next to **Reviews** | Redirect to **Product Reviews** and the review you have posted is in there. |
| 11   | Reviews | Click the ***Cartel Logo*** in the Top Left Corner, select the product **Luxe Loom Towels 100% Cotton** and now click **Add To Cart** | Shows success message: ***Product: Luxe Loom Towels 100% Cotton added to cart successfully!*** |
| 12   | Details | Click the **Cart** icon | Redirect to the **Cart** page, with the product in there |
| 13   | Cart    | Click ***Remove*** | Redirect to Details with success message: ***Product: Luxe Loom Towels 100% Cotton deleted successfully!*** |
| 14   | Details | Click **Log out** | You are logged out and redirected to the home page |
| 15   | Home    | Click **Join Cartel**, type Username: ```cartelsearchproducts``` and Password: ```123456789``` | Redirect to the home page, logged in |

## License :balance_scale:
The project is licensed under the terms of ***Apache License version 2.0***  
