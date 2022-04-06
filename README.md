
# RecipeLib

.NET6.0 Webapp for storing, adding and managing culinary recipes 

&nbsp;

## Features

- Cookie authorization and authentication
- SqlDb or mssqllocaldb
- Responsive design


## Database connection

### Connection string location [AppDbContext.cs](https://github.com/net-runner/recipelib/blob/main/RecipeLib.App/Data/AppDbContext.cs)

For custom database connection edit one following strings and recompile the app.

```cs
string localConnection
string sqlConnection

...

optionsBuilder.UseSqlServer(localConnection);
```
## Default accounts


### Password for any default user 
```
password: zaq1@WSX
```

### Administrator account (same password as default)
```
username: Adam
```
&nbsp;


## API Reference

#### Get all recipes items

```http
  GET /api/
```


#### Get specific recipe by id

```http
  GET /api/${id}
```

#### Get recipes by CategoryId

```http
  GET /api/category/${id}
```

#### Get recipes by AuthorId

```http
  GET /api/author/${id}
```

&nbsp;



## Run Locally

Clone the project

```bash
  git clone https://github.com/net-runner/recipelib
```

Go to the project directory or test directory

```bash
  cd RecipeLib.App 
```

Install dependencies

```bash
  dotnet build
```

Update database

```bash
  dotnet ef database update
```

Start the server

```bash
  dotnet run or dotnet watch
```

&nbsp;




## Screenshots

![Home page](https://github.com/net-runner/recipelib/blob/main/RecipeLib.App/screenshots/home-page.PNG)

![Home page filtered](https://github.com/net-runner/recipelib/blob/main/RecipeLib.App/screenshots/home-page-filtered.PNG)

![Recipe details](https://github.com/net-runner/recipelib/blob/main/RecipeLib.App/screenshots/recipe-details-page.PNG)

![User list](https://github.com/net-runner/recipelib/blob/main/RecipeLib.App/screenshots/user-list-page.PNG)
