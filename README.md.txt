# session_state
Instructions on running the session API:

Step-1:Install visual studio with ASP.NET framwork installed if required/

Step-2:Install postman for sending API requests.

Step-3:Open the project solution on Visual studio.
http://localhost:49761/

Step-4:Add an absolute path in Methods/Shopping_Cart_Handler.cs class in line 24 and 36 of the json file in "Data/ShoppingCart.json"
Note:This step is critical to run the API successfully,failing to comply with this step may result in failure to run the API.

in line 24:
var dataString = File.ReadAllText(@"Your absolute path");

in line 36:
File.WriteAllText(@"D:\Books\3-2\Software Design and Architechture\Microservice\Session\Data\ShoppingCart.json", dataString);

Step-5:Click on IIS Express.It should open a browser with the url: "http://localhost:49761/"
If it doesn't start on the same url than choose the one that appears on the browser header.

Step-6:Open postman to check all the tasks with appropriate endpoints and HTTP request type.

1.Send POST request with http://localhost:49761/cart/add/{youritemname}
  a.if this is the first request,either send no "session-id" in the header of the request or keep the header blank.The server will create a session-id 
  which can be seen in the response header section with field "session-id".
  b.Copy the session-id to the request header to keep the session alive.
For the next subsequent tasks,you must preserve the session_id in the header.

2.Send POST request with http://localhost:49761/cart/add/{youritemname}
  a.If this is a new item,it will successfully add it to the list of items in cart and send back a response.
  b.If this an item already added,it will simply increment it.

3.Send DELETE request with http://localhost:49761/cart/decrement/{youritemname}
  a.it will decrement the item from cart if it exists.

4.Send DELETE request with http://localhost:49761/cart/remove/{youritemname}
 a.it will remove the item from cart if it exists.

5.Send GET request with http://localhost:49761/cart
 a.If  either no valid session-id was passed or the cart is empty,it will return a successful response with no item.
 b.Otherwise it will return all of the items with thier respective amount.



