# SimpleCSV
A rudimentary CSV reader with test cases 

This application is broken up into various libraries.

I mostly rely on abstractions, avoiding concretions (Dependency inversion principle) as far as 
possible, making it possible to substitute / test the various bits - also making the relevant
parts available for reuse - e.g. the CSTruter.Parsers Library can be used in other applications.

Perhaps using Ninject for dependency injection?

### CSTruter.Application
The actual application parsing a csv file into two files.

![alt tag](http://cstruter.com/uploads/app.jpg)

### CSTruter.Parsers
Sure I could have used third party CSV parsers, but opted to write my
own as part of an assessment exercize.

Rudimentary but functioning.

I wrote a custom object mapper as well - ideally something that should be
moved out as well as it can be reused, but currently its tightly coupled
with the CSV parser itself.

### CSTruter.Repositories
I used the general repository pattern, this makes it easy to swop out
the data strategy should the developer decide to fetch data from elsewhere
e.g. SQL etc.

### CSTruter.Services
This contains the basic logic for querying the CSV results.

## #CSTruter.TestLibrary
I used NUnit for test cases and NSubstitute to mock out - nuget used for package
management. Testing around some (not all) boundary cases, TestCaseSources used for
more complex data types.

![alt tag](http://cstruter.com/uploads/tdd.jpg)
