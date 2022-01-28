
# RecipeLib

.NET6.0 Webapp for storing, adding and managing culinary recipes 

&nbsp;

## Features

- Xunit tested
- Api + Cookie authorization and authentication
- SqlDb
- Responsive design


## Default accounts


### Password for any default user 
```
password: zaq1@WSX
```

### Administrator accounts (same password as default)
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

Start the server

```bash
  dotnet run or dotnet watch
```

&nbsp;



