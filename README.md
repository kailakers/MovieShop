## Introduction and scope of the project

The goal of this course is to build a real-world web application using .NET technologies and Angular. To be more specific we are going to use ASP.NET Core version 3, Entity Framework Core version 3 and Angular 8 as our main technologies. Along with these technologies we are going to use HTML, CSS and Bootstrap 4 for making a relatively good-looking and responsive web app. For our database we are going to use SQL Server development version, or you can also use SQL Server Express.

Before we get started, I would like to show you guys the web application we are going to build. Here is the app that acts a Movie Store where registered customers you can buy movies and add movies to their favorites list. This web app has lots of real-world functionality like Server-side pagination, searching, JWT Authentication for security, Role based Authorization where we restrict access to some pages by roles. During the course we are also going to learn some good design principles, coding standards etc. Finally, the data we are going to use for this course comes from TMDB website which has excellent API to get lots of information about movies. SO, I would like to thank them.

## Prerequisites

As I said in the introduction video, we are going to build the real-world application I am expecting certain level of knowledge from you. 
Very First thing is you should have good knowledge of C#, at least 3-6 months of working experience and understand some of the core C# concepts such as Interfaces, Extension methods, Collections and some LINQ.

Good understanding of REST concepts and HTTP protocol including different HTTP verbs such as GET, POST, PUT and DELETE.
Previous working knowledge of either ASP.NET MVC or ASP.NET Web API would be good, but I am expecting you to be expert in either of those. At least understand what Model is, what is Controller is etc.

We will be using Entity Framework Core 3.0 in this project, but I am not expecting any knowledge on that, but decent knowledge of Entity Framework 6 would be required. But we will go through all the steps from beginning to create the database using EF. 

Finally coming to front-end having beginner knowledge in HTML, CSS and JavaScript is enough. Bootstrap knowledge is helpful but not required. But I am expecting you to have used Angular before for at least 6 weeks. So, you know the concept of Components, Services, Directives etc. Even if you don’t know those things, I Definitely think you can follow this course as we will be creating or using all of those.

## Installing the Required Software

If you are on Windows, please go ahead and download the required software from the document I provided. We are going to use Visual Studio 2019 for our ASP.NET Core API Development and Visual Studio Code which is  a lightweight IDE for our Angular development. If you are on macOS then you can use Visual Studio Code for both API and Angular development. But for SQL Server on mac you need to have Docker installed and have SQL Server run on it. I have provided the instructions on how to install it. 

We are going to use Postman to test our API and SQL Management Studio for general database development like checking whether tables have been properly created, running SQL scripts to create initial data etc. If you are on macOS then Visual Studio Code has an extension for connecting to SQL Server which I have provided in the document.

Here are the links for required software

  * <https://visualstudio.microsoft.com/>
  * <https://code.visualstudio.com/>
  * <https://www.getpostman.com/>
  * <https://www.microsoft.com/en-us/sql-server/sql-server-downloads>
  
[![Build Status](https://dev.azure.com/Reddy57/MovieShop/_apis/build/status/Reddy57.MovieShop?branchName=master)](https://dev.azure.com/Reddy57/MovieShop/_build/latest?definitionId=4&branchName=master)
