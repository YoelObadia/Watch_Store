This is a project in c# where we created a store of watches.
For this, we used the layers model: DAL, BL, PL.

The DAL contains the data and a module for an default initialization of the entities and the CRUD methods.
There are 3 entities in the DAL: Products(watches), Orders and OrderItems.
We use interfaces for to implement the CRUD methods.
There is also a Factory and Singleton design patterns that allow us to use just one reference of the DAl layer from the BL layer.

The BL layer contains all the logic of the project and allow us the veracity of the data.
We use also interfaces for to implement the CRUD methods, Factory and Singleton.
There are new entities like Cart, ProductForList, OrderForList, ProductItems which are logicales entities.

The PL layer display all the data on windows with an automatic update on every action.
The administrator can update or add a product, update the status of the order, delete an order delivered or check the order tracking.
The client can check the list of items in the shop and creating a cart by adding products. The client can after check the cart and update the quantity of the product he wants to modify. He can after this finishing the cart that will be sent in the list of orders in the screen of the administrator.

We also created a DalXml layer that is the new DAL layer and use the data in xml files. This allow us to continue where we stopped the program last time and not to begin every time from the default initialization.

There is also a simulator. We create a thread as a background worker of the PL and takes care of automatically updating order statuses from oldest to newest.

Credits: Yoël Obadia & Shimon Cohen (Github: ShimonCohen2001)
