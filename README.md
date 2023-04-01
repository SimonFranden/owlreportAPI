# owlreportAPI

This is the Swagger API that controlls the backend of https://github.com/SimonFranden/owlreport

This Api is configured to be the backbone of a timereporting software for diffrent projects with it's own internal Database in SQL Lite.

AuthController : is resposible for login requests and all other things that requires authorization,
via usernames and other credentials + secrect key that is generated (to keep track of users more easily and their level of access).

ProjectController : Responsible for the Fetching and Sorting of projects from the Database to the webpage.

TimeReportController : Responsible for Fetching and posting the Timereports that are made on the webpage to the Database.

UserController : Responsible for all the diffrent things a User can do on the website.
I:E create a new project , Editing Existing projects , Add/remove users to Projects, Checking what projects "Current User" is a part of.
