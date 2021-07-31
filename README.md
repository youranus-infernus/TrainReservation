
# TrainReservation

<h3>An application for booking seats on the train </h3>

To reserve a seat, you need to choose a suitable route <br/> 
You can use the search, select the desired stations and day. <br/>
If the two stations indicated in the search are visited on a certain route in chronological order, then the route is suitable. <br/>
The time and station![CRUD Trip](https://user-images.githubusercontent.com/74061165/127734550-8ce1c1dd-00eb-4a62-b67c-d72ad907d7c4.gif)
 of departure and arrival changes according to the selected stations <br/>

Then all the railcars of this train appear on the screen with seats in the form of checkboxes. <br/>
Previously reserved seats will not be available for selection <br/>
After choosing the right seats, you need to enter the first and last name of the passenger who will use this place, and also choose a discount <br/>

Then a summary page will appear with a description of the route and a list of reserved seats. <br/> 
After confirming the reservation, the reserved seats can be seen in the My Tickets tab <br/>


![Boocking Seat](https://user-images.githubusercontent.com/74061165/127734060-35a02eb4-c3d9-40a2-b5cd-7088d09a1bbe.gif)


<br/>
The application also allows the administrator to create, modify, and delete trains, stations and trips. <br/>
In the case of stations, everything looks as simple as possible. <br/>
<br/>

![CreateStation](https://user-images.githubusercontent.com/74061165/127347650-a5d90573-f05d-451d-bc0e-4b5abf969f77.gif)

When creating a train, you need to enter only its name <br/>
Then you can create cars on this train and seats in the created car. <br/>
All this can also be changed and deleted <br/>
<br/>

![CRUD Train](https://user-images.githubusercontent.com/74061165/127734296-6c488ac4-1239-48ae-b1af-8b7b019519d1.gif)

To create a new route, the admin must select a train <br/>
Then the administrator adds a visit to certain stations at a certain time <br/>
The earliest visit becomes the departure point of the train, and the latest visit becomes the destination <br/>

![CRUD Trip](https://user-images.githubusercontent.com/74061165/127734568-382f7ff7-c3e3-4260-b8fc-58659ebcd538.gif)


The admin's email and password are located in /Initializers/RoleInitializer.cs
