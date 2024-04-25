# MCS-HR
A simple app to manage employee data 

Version .net 8.0
Type ASP.NET MVC Application

How to start?
 first dowload the folder 📂 or clone it using git
 then inside the folder find appsetting.json file an change database connection 'MCSHR' with you target db.
 --it must be Microsoft sql server database
 you can open the solution in side visual studio or you favorite ide 😀 and run the project and start editing if you like 

 if you want to just run the app you can go to bin -> release -> .net8.0
 you can use the content directly in the IIS and access it

 when you start the app the first screen is the employee list screen 
 you will find 2 button in the top of the screen ucan use them to add employees using add new form or you could upload a tab delimited file

 under the 2 button there is a employees list table to display employee data with 2 button on the end of each row to edit and delete employee data with 2 fields to filter data by the employment type and overtime hour rate

 in employee data there is a column for each employee overtime hour value calculated as follow:
o	In case of employee (Monthly) = Monthly salary /160

o	In case of worker (Hourly) = daily salary * 3 /16

o	In case of Freelancer = Hour salary * 1.5
the column get calculated on data retrieval

improvement to do:

need employee list screen to be paged as if it had a lage set of data the sceen will load in longer time also need to add search for the other data fields
the forms for add, edit, and delete would be better and easy for the user experience to be in popup screen 



 
 
