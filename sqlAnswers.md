-- On the Chinook DB, practice writing queries with the following exercises

-- BASIC CHALLENGES -- List all customers (full name, customer id, and country) who are not in the USA SELECT FirstName + ' ' + LastName AS 'Full Name', customerid, country FROM Customer WHERE country != 'USA'; -- List all customers from Brazil

SELECT FirstName + ' ' + LastName AS 'Full Name', customerid, country FROM Customer WHERE country != 'USA';

SELECT FirstName + ' ' + LastName AS 'Full Name', customerid, country FROM Customer WHERE country = 'Brazil';



-- List all sales agents

SELECT * FROM Employee Where Title = 'Sales Support Agent';

-- Retrieve a list of all countries in billing addresses on invoices

SELECT Distinct BillingCountry From Invoice;

-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?

SELECT * From Invoice Where YEAR(InvoiceDate) = 2009;
# = 0

-- (challenge: find the invoice count sales total for every year using one query)

SELECT YEAR(InvoiceDate) as year, COUNT(InvoiceId) as count, SUM(Total) as total From Invoice Group By YEAR(InvoiceDate);

-- how many line items were there for invoice #37
SELECT Count(CustomerId) From Invoice Where CustomerId = 37;
7

-- how many invoices per country? BillingCountry # of invoices -
SELECT BillingCountry, Count(BillingCountry) as country From Invoice Group By BillingCountry;

-- Retrieve the total sales per country, ordered by the highest total sales first.

SELECT BillingCountry, SUM(Total) as country_total
 From 
    Invoice 
 Group By 
    BillingCountry 
Order By 
    country_total DESC;

-- JOINS CHALLENGES -- Every Album by Artist

SELECT Album.Title, Artist.Name
From Artist
INNER JOIN Album
ON Album.ArtistId = Artist.ArtistId;

-- All songs of the rock genre

SELECT Track.Name, Genre.Name
From Track
INNER JOIN Genre
ON Track.GenreId = Genre.GenreId
Where Genre.Name = 'Rock';

-- Show all invoices of customers from brazil (mailing address not billing)

SELECT * FROM Invoice
INNER JOIN Customer
ON Invoice.CustomerId = Customer.CustomerId
Where Customer.Country = 'Brazil';

-- Show all invoices together with the name of the sales agent for each one Select i.InvoiceId, i.InvoiceDate, c.CustomerId, c.FirstName + ' ' + c.LastName 'Customer', e.FirstName + ' ' + e.LastName 'Sales Agent' From Invoice i JOIN Customer c ON i.CustomerId = c.CustomerId JOIN Employee e ON c.SupportRepId = e.EmployeeId

-- Which sales agent made the most sales in 2009?

0

-- How many customers are assigned to each sales agent?

Select COUNT(c.CustomerId), e.FirstName + ' ' + e.LastName 'Sales Agent' From Invoice i JOIN Customer c ON i.CustomerId = c.CustomerId JOIN Employee e ON c.SupportRepId = e.EmployeeId
GROUP BY e.FirstName, e.LastName;

-- Which track was purchased the most ing 20010?

-- Show the top three best selling artists.

-- Which customers have the same initials as at least one other customer?

-- ADVACED CHALLENGES -- solve these with a mixture of joins, subqueries, CTE, and set operators. -- solve at least one of them in two different ways, and see if the execution -- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?

-- 2. which artists did not record any tracks of the Latin genre?

-- 3. which video track has the longest length? (use media type table)

-- 4. find the names of the customers who live in the same city as the -- boss employee (the one who reports to nobody)

-- 5. how many audio tracks were bought by German customers, and what was -- the total price paid for them?

-- 6. list the names and countries of the customers supported by an employee -- who was hired younger than 35.

-- DML exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.