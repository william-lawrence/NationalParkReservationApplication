# NationalParkReservationApplication

## Introduction
The National Park Reservation Application was a mini capstone project that I created at Tech Elevator, the boot camp that I attended.  

The primary user of the application is a park employee, and they are making the reservation for a guest.



## Requirements  
- The user needs to be able to view all the available parks in the system sorted alphabetically by name.
    - A park includes an id, name, location, established date, area, annual visitor count, and description.
- A user needs to be able to select the park that the guest is visiting and see a list of all the campgrounds available in that park.
    - A campground includes an id, name, open month, closing month, and a daily fee.
- A user needs to be able to selects a campground and search for a date of ability so that they can make the reservation for the guest. 
    - A reservation search only requires the desired campground, a start date, and an end date.
    - A campsite is unavailable if any part of their preferred date range overlaps with an existing reservation.
    - If no campsites are available, indicate to the user that there are no available sites and ask them if they would like to enter in an alternate date range.
- A user of the system needs to select a campsite that is open during the selected time frame and make a reservation for that site.
    - A reservation requires a name to reserve under, a start date, and an end date.
    - A reservation requires a name to reserve under, a start date, and an end date.
- The system must use a database to store park information and reservation data. 
