Main files you should pay attention to:

mailApi/SendSimpleMessageChunk.cs - main backend stuff

Views/Home/Index.cshtml, Views/Home/Send.cshtml, Views/Home/Logs.cshtml - html pages. 
Send for sending a message, 
index for searching logs through a request,
logs for viewing local logs.

JS files:
Scripts/specific

CSS files:
Content/specific

Other important files:
App_Start/BundleConfig.cs - some libraries rely on loading after some other ones.

Controllers - HomeController.cs for overall website,
Event|LogsController.cs - my manual connections.

Views/Shared/_Layots.cshtml - just overall website info.

________________________________________

Right now this API uses my account's info.
To use this API from your account, replace domain and password in ApiPass and ApiDomain in mailApi/SendSimpleMessageChunk.cs. 
When you have a paid account, you will be able to make a custom domain, or even have multiple ones.

To send messages you will need to add recipient's emails to the "verified" list. 
You may not need to do it once you have a paid account.
There is currently only one verified email on my list - it's my own email. Please don't spam it.

________________________________________


Current problems/issues/specificities:

- You can't send actual html code in Send.html - system prevents it. 
However, if you put html content as a string directly in the SendSimpleMessage function in mailApi/SendSimpleMessage.cs, it works fine.

- In Index.html when you click on a searched log to see more information about it, it pulls this information from local logs as in Logs.html, it doesn't do it through a request to mailGun.

- Keyword search (meaning searching message body) is not available through MailGun.
There is, however, subject search.
If you want to implement message-body search, you would have do it with local logs.

- Currently the API creates a folder locally on the computer. 
It creates a folder for logs, + folders for each date. 1 message is 1 JSON file.
Change your path in regPath string in mailApi/SendSimpleMessage.cs