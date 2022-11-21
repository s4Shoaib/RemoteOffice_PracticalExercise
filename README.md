**Mention the challenges you may have faced**

  The application is build in Visual Studio 2019 using technologies as Vue.js, Bootstrap(Frontend), ASP.NET MVC Core(Backend).

  I have applied almost all concepts such as using 
    
    Vue Components, 
    Bundling of Javascipt and CSS,
    MVC, 
    API, 
    Unit Testing, 
    Middleware Authentication,
    Global Exception Handler, 
    Dependency Injection, 
    Coding Standards etc

**Share guideline to run the system in a different/specific virtual environment (if there's any)**
  
  Please follow the below steps to run the application locally
  
  1) Download the source code at your local path : Let say local path = "C:\Projects\RemoteOffice_PracticalExercise\"
  2) You will need to install the npm packages to run the application hence follow below steps
  
      -> Open command promt (cmd)
      
      -> Navigate to the Web project using command "cd C:\Projects\RemoteOffice_PracticalExercise\PracticalExercise_RO.Web" and press Enter
      
      -> Now run command "npm install webpack --save-dev" . This will install all the required npm packages
      
      -> Now run command "npm run wbp" . This command will compile all the Vue components, Javascipt files and CSS
      
  3) Clean and Build the project in Visual Studio
  4) Set the "PracticalExercise_RO.Web" as the Startup project
  5) Run the project by clicking F5, you will get to see the Practitioners Appointment on the homepage

**Below API's are exposed which can be using directly in other projects when this project is run**

  1)For each practitioner, displaying cost and revenue per month within the given date range
  
    http://localhost:56621/api/Appointment/Get?fromDate=2019-12-01&toDate=2020-02-29
    
    Request method: Get

    Please find the sample output in the below image
    
    ![image](https://user-images.githubusercontent.com/7437749/202501038-5bc05944-25db-4025-87fc-e8f140081a61.png)
    
  2)Appointment data list in the UI for current practitioner within the selected date range.
  
    http://localhost:56621/api/Appointment/Search/
    
    Request Method : Post
    
    Sample payload to be passed : {"practitioner_Id":8,"fromDate":"2019-12-01","ToDate":"2020-02-20"}
 
    Please find the sample output in the below image
    
    ![image](https://user-images.githubusercontent.com/7437749/202502836-ef3bf2b6-8497-4ed8-a490-cc927cd96a2d.png)

**Mention your findings from the technical exercise (exceptions)**

  I believe this test is a good approach to know/test one's skills. Mostly all the concepts can be implemented while developing this test.

**Mention if you have fully completed/partially completed the task**

  I have fully completed the task following all the below com

**I will be attaching a complete screen recording video showing the running of the project in the Remote Office email chain.**
