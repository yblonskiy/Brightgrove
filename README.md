# Brightgrove

1. Brightgrove.Web.App is the main web application where leagues and matches are displayed.

2. Brightgrove.Services.WebApi is the API application.

3. Brightgrove.Web.App has settings in the appsettings.json file:

    "WebApiServiceAgents": {
      "WebApiHost": "https://brightgroveserviceswebapi20241220221247.azurewebsites.net/api/v1/",
      "WebApiTimeout": 600
    }
  
     where WebApiHost is the URL of the Brightgrove.Services.WebApi project.

4. The Brightgrove.Services.ServiceAgent project is used as a wrapper in the web application to send requests to the API application.

5. Brightgrove.Web.App uses Custom Elements to build UI components.

The App and API projects are deployed on Azure:
 - https://brightgrovewebapp20241220220142.azurewebsites.net
 - https://brightgroveserviceswebapi20241220221247.azurewebsites.net (swagger is accessed via swagger/index.html)

6. Performance:

   ![image](https://github.com/user-attachments/assets/8d355b44-9873-4eed-93e9-aab74be310e4)

