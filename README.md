# Receipt Processor Challenge

This is a webservice that fulfills the challenge API. The provided solution is in C# using the ASP.NET Core using .NET 8.0.
The provided solution can run as standalone or in a docker container.

# How To Build

## Download the Sample App
* `git clone https://github.com/dewie102/Receipt-Processor-Challenge.git`

## Run in a Docker Container
* Navidate to the Dockerfile at `Receipt-Processor-Challenge/CSharp/ReceiptProcessorChallenge-CSharp`
* Run the following commands to build and run the app in Docker

```
docker build -t receiptprocessorchallenge-zackr .
docker run -it --rm -p <localport>:8080 --name receiptprocessorchallege-zackr receiptprocessorchallenge-zackr
```

* Go to `http://<host>:<localport>` in a browser to see the Swagger UI, which shows the endpoints and JSON Schemas

# Comments
* I wasn't too certain on if the receipt was to be saved or thrown away. My first attempt was to save it and process it later
but in order to do so with my DBContext I had to change the json schema to include IDs and work with foreign keys. I opted to 
just validate the receipt json, calulate the points and save the points to a given ID.
* I had trouble with how the api endpoint automatically deserialized the body json into a receipt and providing the expected 
`The receipt is invalid` error response, without making the project too complicated and given the time I opted to make a default 
invalid state error response. This works with only accepting the receipts and would be something to handle better later.
* In the try catch for saving the points to the db, I know the Id is always unique but I wanted to catch it anyway for best practices.
* There didn't seem to be a definition of where the Id came from, I decided to just make a GUID and use that. Ideally, we would want to 
do some sort of checksum or md5 to better detect duplicate receipts.
* I decided to do some unit testing but didn't want to go too crazy and I know it's not covering all edge cases.
* This solution should work on either Windows or Linux docker containers but was only tested on Linux docker.
* I kept the swagger UI and made it available in production, normally I wouldn't do that but wanted to provide it for the reviewer and thought
it was kind of cool to have.

# What the future holds:
Given I had more time and was able to talk to the team/ customer I could ask more clarifying questions and make better design decisions.

This is a list of things I fell like I would like to work on but didn't want to complicate the project:

* Add more unit testing and add in integration tests
* Actually save the receipt if that is desired.
* Do a checksum or md5 hash metioned earlier to reduce the chances of duplicates.
* Some how intercept what is received on the endpoint to do the validation/ error handling before sending it to the method.
This way we don't have a generic error message that might not fit all endpoints/ model creation errors.
* Maybe do some better encapulation and abstration or general refactoring.
* I wanted to give a shot at solving this through GO but wanted to submit something. I have never touched GO but see that it is 
statically typed and similar to C so I chose to use C# for my actual submission. I am going to try and add GO into this repo given time.
* I would do further data validation as I only did what was provided in the documentaiton. For example, I would ensure the date and times 
were provided properly: `HH:MM` and `YYYY-MM-DD` as well as other data fields.

# Testing
* I built the app on my Windows machine but used Docker Desktop with Linux containers. Debug and docker build/ run worked correctly.
* I followed the instructions, cloned, docker build and docker run on my linux VM and it also worked without issues.
* I ran my unit tests on all 4 receipts given in the challenge repo.
* I ran postman on the API and verified that I got the expected responses for good and bad data provided to the api.