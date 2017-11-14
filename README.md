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

## Usage

### General usage
GET: api/entity - get all entities  
GET: api/entity/5 - get entity with ID 5 

POST: api/entity - post (create) an entity, with the entity properties in the request body  
PUT: api/entity/5 - put (update) an entity with ID 5, with the entity properties in the request body  
DELETE: api/entity/5 - delete an entity with ID 5  

### Addresses
GET: api/Addresses  
GET: api/Addresses/5  
GET: api/Addresses/5/trainingsessions  

PUT: api/Addresses/5  
POST: api/Addresses  
DELETE: api/Addresses/5  

### Books
GET: api/Books  
GET: api/Books/5  
GET: api/Books/5/trainings  

PUT: api/Books/5  
POST: api/Books  
DELETE: api/Books/5  

### Certificates
GET: api/Certificates  
GET: api/Certificates/5  
GET: api/certificates/5/users  
GET: api/Certificates/5/training  

PUT: api/Certificates/5  
POST: api/Certificates  
DELETE: api/Certificates/5  

### Employees
GET: api/Employees  
GET: api/Employees/5    
GET: api/employees/5/manages  

### Faqs
GET: api/Faqs  
GET: api/Faqs/5  
GET: api/Faqs/5/trainings  

PUT: api/Faqs/5  
POST: api/Faqs  
DELETE: api/Faqs/5  

### Followingtrainings
GET: api/Followingtrainings  
PUT: api/Followingtrainings/5  
POST: api/Followingtrainings  
DELETE: api/Followingtrainings/5  

### Surveys
GET: api/Surveys  
GET: api/Surveys/5  
GET: api/Surveys/5/questions  
GET: api/Surveys/5/trainings  

PUT: api/Surveys/5  
POST: api/Surveys  
DELETE: api/Surveys/5  

### Surveyanswers
GET: api/Surveyanswers  
GET: api/Surveyanswers/5  
GET: api/Surveyanswers/5/question  
GET: api/Surveyanswers/5/user  

PUT: api/Surveyanswers/5  
POST: api/Surveyanswers  
DELETE: api/Surveyanswers/5  

### Surveyquestion
GET: api/Surveyquestions  
GET: api/Surveyquestions/5  
GET: api/Surveyquestions/5/survey  
GET: api/Surveyquestions/5/answers  

PUT: api/Surveyquestions/5  
POST: api/Surveyquestions  
DELETE: api/Surveyquestions/5  

### Teachers
GET: api/Teachers  
GET: api/Teachers/5  
api/Teachers/5/sessions  

PUT: api/Teachers/5 
POST: api/Teachers  
DELETE: api/Teachers/5  

### Trainingfaqs
GET: api/Trainingfaqs  
POST: api/Trainingfaqs  
DELETE: api/Trainingfaqs/5  

### Traininginfos
GET: api/Traininginfos  
GET: api/Traininginfos/5  
GET: api/Traininginfos/5/faqs  
GET: api/Traininginfos/5/certificates  
GET: api/Traininginfos/5/sessions  
GET: api/Traininginfos/5/books  

PUT: api/Traininginfos/5  
POST: api/Traininginfos  
DELETE: api/Traininginfos/5  

### Trainingsbooks
GET: api/Trainingsbooks  
GET: api/Trainingsbooks/5  

PUT: api/Trainingsbooks/5  
POST: api/Trainingsbooks  
DELETE: api/Trainingsbooks/5  

### Trainingsessions
GET: api/Trainingsessions  
GET: api/Trainingsessions/5  
GET: api/trainingsessions/5/users  
GET: api/trainingsessions/5/traininginfo  
GET: api/trainingsessions/5/teacher  

PUT: api/Trainingsessions/5  
POST: api/Trainingsessions  
DELETE: api/Trainingsessions/5  

### Trainingsurveys
GET: api/Trainingsurveys  
GET: api/Trainingsurveys/5  

PUT: api/Trainingsurveys/5  
POST: api/Trainingsurveys  
DELETE: api/Trainingsurveys/5  

### Users
GET: api/Users  
GET: api/Users/5  
GET: api/users/5/certificates  
GET: api/users/5/trainings  
GET: api/users/5/surveyanswers  

PUT: api/Users/5  
POST: api/Users  
DELETE: api/Users/5  

### Usercertificates
GET: api/Usercertificates  

PUT: api/Usercertificates/5  
POST: api/Usercertificates  
DELETE: api/Usercertificates/5
