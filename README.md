# Brightgrove

1. Brightgrove.Web.App is the main web application where leagues and matches are displayed.

2. Brightgrove.Services.WebApi is the API application.

3. rightgrove.Web.App has settings in the appsettings.json file:

  "WebApiServiceAgents": {
    "WebApiHost": "https://localhost:44379/api/v1/",
    "WebApiTimeout": 600
  }
  
  where WebApiHost is the URL of the Brightgrove.Services.WebApi project.

4. The Brightgrove.Services.ServiceAgent project is used as a wrapper in Web App to send requests to the API application.
