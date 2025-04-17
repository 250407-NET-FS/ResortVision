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

-- Which track was purchased the most in 2010?
select
    COUNT(Track.Name) as count,
    Track.Name,
    Invoice.InvoiceDate
from
    InvoiceLine
JOIN 
    Track ON InvoiceLine.TrackId = Track.TrackId
JOIN
    Invoice ON InvoiceLine.InvoiceId = Invoice.InvoiceId
WHERE
    YEAR(Invoice.InvoiceDate) = 2010
GROUP BY
    Track.Name, Invoice.InvoiceDate
ORDER BY
    count DESC;

There are 0 tracks

-- Show the top three best selling artists.

select TOP (3)
    COUNT(Artist.Name) as Sold,
    Artist.Name
FROM
    InvoiceLine
JOIN
    Track ON Track.TrackId = InvoiceLine.TrackId
JOIN
    Artist ON Track.Composer = Artist.Name
Group BY
    Artist.Name
ORDER BY
    Sold DESC;

-- Which customers have the same initials as at least one other customer?

SELECT 
    FirstName,
    LastName
FROM 
    Customer
WHERE 
    CONCAT(LEFT(FirstName, 1), LEFT(LastName, 1)) IN (
        SELECT 
            CONCAT(LEFT(FirstName, 1), LEFT(LastName, 1)) AS Initials
        FROM 
            Customer
        GROUP BY 
            CONCAT(LEFT(FirstName, 1), LEFT(LastName, 1))
        HAVING 
            COUNT(*) > 1
    )
ORDER BY 
    LEFT(FirstName, 1), LEFT(LastName, 1);

-- ADVACED CHALLENGES -- solve these with a mixture of joins, subqueries, CTE, and set operators. -- solve at least one of them in two different ways, and see if the execution -- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?

select *
From (
    SELECT
    Artist.Name,
    COUNT(Album.ArtistId) as Albums
FROM
    Album
RIGHT JOIN
    Artist ON Album.ArtistId = Artist.ArtistId
GROUP BY
    Artist.ArtistId, Artist.Name
) as ArtistAlbumCounts
Where Albums = 0;

CREATE TEMPORARY TABLE ArtistAlbumCounts AS
SELECT
    Artist.Name,
    COUNT(Album.AlbumId) AS Albums
FROM
    Artist
LEFT JOIN Album ON Album.ArtistId = Artist.ArtistId
GROUP BY
    Artist.ArtistId, Artist.Name;

then query from this table the albums

-- 2. which artists did not record any tracks of the Latin genre?

WITH ArtistLatinTrackCounts AS (
    SELECT
        Artist.Name,
        COUNT(CASE WHEN Genre.Name = 'Latin' THEN 1 ELSE NULL END) AS Count
    FROM
        Artist
    LEFT JOIN Track ON Track.Composer = Artist.Name
    LEFT JOIN Genre ON Track.GenreId = Genre.GenreId
    GROUP BY
        Artist.ArtistId, Artist.Name
)
SELECT * 
FROM ArtistLatinTrackCounts AS a
Where Count = 0;

-- 3. which video track has the longest length? (use media type table)

SELECT
    Track.Milliseconds,
    Track.Name
FROM
    Track
JOIN
    MediaType ON Track.MediaTypeId = MediaType.MediaTypeId
WHERE
    MediaType.Name LIKE '%video%'
GROUP BY
    Track.Milliseconds, Track.Name
ORDER BY
    Track.Milliseconds DESC;

-- 4. find the names of the customers who live in the same city as the -- boss employee (the one who reports to nobody)

WITH BossManCity AS(
    SELECT City from Employee
    Where ReportsTo = NULL
)
select e.FirstName, e.City
FROM BossManCity as b
JOIN
    Employee e ON b.City = e.City;

-- 5. how many audio tracks were bought by German customers, and what was -- the total price paid for them?



-- 6. list the names and countries of the customers supported by an employee -- who was hired younger than 35.

-- DML exercises

-- 1. insert two new records into the employee table.

INSERT INTO Employee (
    EmployeeId, FirstName, LastName, Title, ReportsTo, BirthDate, HireDate, Address, 
    City, [State], PostalCode, Phone, Fax, Email
)
VALUES (
    9, 'Mama', 'Joe', 'Henchman', NULL, 
    '1980-01-01',      -- BirthDate
    '1999-09-09',      -- HireDate
    '2423 klajf', 
    'john', 'AR', 
    '27564', 
    '234234234',       -- Phone
    '98275',           -- Fax
    '23905@gmail.com'  -- Email
);

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.