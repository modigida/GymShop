# Gym Shop

## Description

Gym Shop is a full-stack e-commerce application inspired by MM Sports. 

<img src="Images/ProductsPage.png" alt="Products page with all products" width="1000" >

The project is build with Blazor WebAssembly for the frontend and ASP.NET for the backend. The application manages customers, accounts, product catalogs, shopping carts, and order processing 
with secure JWT-based authentication. 

<img src="Images/LogIn.png" alt="Login page" width="500" >

Usrs can log in as either administrators or customers. New customers can register directly on the website. Administrators have acecss 
to management features such as editing products and monitoring orders. To test the admin featurs, you can use the following credentials:

**Email:** admin@admin.com

**Password:** Lösenord

<img src="Images/Customers.png" alt="View all customers on admin page" width="1000" >

Data is stored in SQL Server, following the three normal forms to ensure performance and integrity. The backend is structured using the 
Repository Pattern and Unit of Work for clear and scalable architecture. 

## Features

* **Dynamic inventory management** - Products automatically switch to "Out of Stock" when their quantity reaches zero.

<img src="Images/ProductModal.png" alt="Product modal for product details" width="500" >

* **Cart validation** - Customers cannot add more products to the cart than what is available in stock.

<img src="Images/ShoppingCart.png" alt="Shopping cart" width="500" >

<img src="Images/OrderConfirmation.png" alt="Order confirmation" width="500" >

* **JWT-based authentication** - Secure API access with token authentication

<img src="Images/UserInfo.png" alt="Detailed user info" width="500" >

* **Hashed passwords** - Enhanced security for user credentials.
* **Responsive frontend** - Blazor WebAssembly ensures a fast and interactive user experience.

<img src="Images/Icons.png" alt="Navbar icons" width="200" >

## Installation and Usage

1. Clone the repository:
   ```bash  
   git clone https://github.com/modigida/GymShop.git
3. Download the database backup and restore it in SQL Server Management Studio (SSMS).
5. Add your connectionstring and JWT settings in appsettings.default.json


## Technologies

* **Blazor WebAssembly** - Frontend
* **ASP.NET API** - Backend
* **SQL Server** - Database
* **Entity Framework Core** - ORM for database access
* **Repository pattern & Unit of Work** - Design pattern for clear architecture
* **JWT Authentication** - Secure authentication
* **BCrypt** - Password hashing

<img src="Images/Header.png" alt="Logo" width="200" >
