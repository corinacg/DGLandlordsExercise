# DGLandlordsExercise

### Running instructions
For security reasons the connection string is not filled in.
Fill the following placeholders in the DgCodeTestEntities connection string in LandlordPropertiesManager\LandlordPropertiesManager\App.config :
- {{server_Name}} -> fill with server name
- {{initial_Catalog}} -> fill with database name
- {{user_Id}} -> fill with user is
- {{password}} -> fill with password

### Todos
 - Write Tests
 - Improve UI
 - Split Repository into a separate assembly, remove EntityFramework reference from the WPF project
 - Make Repository more generic
 - Add pagination to landlord list (don't bring all the data from the database)
 - Improve navigation between MainWindow and landlord list/add&edit property views
 - more

