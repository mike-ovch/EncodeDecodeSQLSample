This project was created as an example of using SQL CLR to extend the functionality of MS SQL Server

# Solution Structure

The solution consists of three projects:

* **EncodeDecodeLibrary** - Contains the library code, that allow you to encode, 
    decode strings, and translate encoded strings to lowercase.
* **EncodeDecodeLibraryTests** - Contains library tests
* **DatabaseQueries** - 
Contains SQL queries: 
   * adding the assembly and functions from the library to the current database
   * creating an indexed view using these functions
   * queries to delete views, functions and assemblies from current database

# Security note
Using such encoding and decoding methods is not a secure way to store important data.
This code is provided as a case study to understand how to add and use your own functions
in a Microsoft SQL Server database.

# Comment Language Information
Comments on the code are written in Russian, 
as the project was originally developed for the Russian-speaking team. 
I plan to translate the comments into English later.