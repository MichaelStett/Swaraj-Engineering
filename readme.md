1. Framework:
ASP.Net + Blazor
2. Interface adapters
Controllers, Cron Jobs
	Validator -> Validates incoming data (syntactic validation)
	Presenters -> format response
	SingalR?
3. Application Logic:
Services
	Validators -> is authorized? (semantic validation)
	GET 
	POST, DELETE and PUT -> enques inputs
4. Entity Logic
Repository classes -> Retreaving and writing data from/to database
Adapter classes -> providing acess to external apis
5. Data layer
Models, Structs, ORMs

