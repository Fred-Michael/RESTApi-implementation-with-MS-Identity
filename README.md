# REST API implementation

This is a user-centric application that implements a REST API architecture using Microsoft Identity package.

The main logic is contained within the `Controllers` file inside the `Week10Correction` folder


## The endpoints:

### Request

`GET /allUsers/{page}`
**You pass in** the page number you want to view records. **You get** a paginated response of all users in the database

**Successful Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json
{
    "firstName": "XXXX",
    "lastName": "XXXX",
    "department": "XXXX",
    "dateOfEmployment": "2020-09-27T00:03:13.3191018",
    "gender": "XXXX"
},
{
    "firstName": "XXXX",
    "lastName": "XXXX",
    "department": "XXXX",
    "dateOfEmployment": "2020-10-27T00:03:13.3191018",
    "gender": "XXXX"
}
```

`POST /register`
**You pass in** your credentials as a new user. **You get** success or failure status code indicating if your registration was successful

**Request:**
```json
POST /login HTTP/1.1
Accept: application/json
Content-Type: application/json
{
    "UserName" : "me@eg.com",
    "Email" : "me@eg.com",
    "FirstName" : "myFirstName",
    "LastName" : "myLastName",
    "Department" : "myDepartment",
    "Gender" : "myGender" 
}
```

**Successful Response:**
```
HTTP/1.1 200 OK("Registration was successful");
```

**Failed Response:**
```
HTTP/1.1 400 BadRequest();
```

`POST /login`
**You pass in** your login credentials as an existing user. **You get** your details with your API token for further access to the restricted endpoint

**Request:**
```json
POST /login HTTP/1.1
Accept: application/json
Content-Type: application/json
{
    "Email" : "me@eg.com",
    "Password" : `a six-digit password with at least an uppercase, a lowercase, symbol and numbers`
}
```

**Successful Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json
{
    "firstName": "XXXX",
    "lastName": "XXXX",
    "email": "XXXX@eg.com",
    "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjYjQ5MjdmMS0xY2Y1LTRmYzgtYTViMC1mYTM2ZmM1ZDFhZGEiLCJ1bmlxdWVfbmFtZSI6IlRlc3QiLCJuYmYiOjE2MDI0MTkzMzgsImV4cCI6MTYwMjY3ODUzOCwiaWF0IjoxNjAyNDE5MzM4fQ.OQsrbcv41R37zQW_r7MF2HMUMUJxGYUmECyououDs8pCx3QzrDVX9FD3mAFtMZfz9PLkgHMTEu0VA43e49tKHB",
    "department": "XXXX",
    "gender": XXXX,
    "dateEmployed": "XXXXXXXXXXXXXXXXX"
}
```

**Failed Response:**
```
HTTP/1.1 400 BadRequest();
```

`POST /getUserDetails`
`This route is protected and for one to access it, he/she must be logged in and use the generated token as an authentication to access this route`

**You pass in** your credentials. **You get** your full details only accessible by you (except you give your login details to another user 🙃)

**Request:**
```json
POST /login HTTP/1.1
Accept: application/json
Content-Type: application/json
{
    "FirstName":"XXXX",
    "LastName":"XXXX",
    "Email":"XXXX@eg.com"
}
```

**Successful Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json
{
    "firstName": "XXXX",
    "lastName": "XXXX",
    "department": "XXXX",
    "dateOfEmployment": "2020-10-11T13:28:35.4996379",
    "gender": "XXXX"
}
```

**Failed Response:**
```
HTTP/1.1 401 Unauthorized();
```
