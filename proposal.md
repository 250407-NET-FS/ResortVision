Name: ResortVision

Theme: Resorts API
    -API for Resort customer tracking

Summary: This application handles the resort bookings for customers across multiple resorts.

Models:
    Resort:
        -Name
        -Email
        -Members
        -price
        -perks
        -Bookings
    
    Customer:
        -ResortMemberships
        -fName
        -lName
        -email
        -balance
        -Bookings
    
    -Booking:
        -Customer
        -Resort
        -DateTime
        -Cost
    

Many to many: Resorts can have many customers and customers can visit many resorts

Features:
    -Create Customer

    -Create Resort

    -Customer can book Resort

    -Customer can add to balance

    -Customer can get booking history

    -Customer can get resortsVisited

    -Resorts can retrieve Customers

    -Resorts can retrieve Bookings

    -can get Resort perks to be displayed

    -facilitate transactions between customers and Resorts

    

