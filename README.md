# Dataservice
Service to access and modify data

## Goal
This dataservice exposes multiple REST endpoints, through which you can read (GET), add (POST), update (PUT) and remove (DELETE)
data from our database.

You can do this by performing basic rest requests, like GET <servername>/api/addresses , which will fetch all databases.
Add an identifier to this URL, for example GET <servername>/api/addresses/1 , and you'll get only the entity associated with that ID.

## Running the dataservice locally
To run the dataservice locally, you must first [install the .net core 2 sdk](https://blogs.msdn.microsoft.com/benjaminperkins/2017/09/20/how-to-install-net-core-2-0/).
Then run the dataservice in Visual Studio. A browser window opens.
Take note of the base URL, this is the URL you must use to access the api.
