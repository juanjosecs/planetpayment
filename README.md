# Integration with Planet Payment API

This implementation use the Planet Payment API with credit cards in next workflows:

- Token registration
- Charge
- Reverse
- Refund


## Configuration

### Credentials
Currently, the Planet Payment test credentials are configured. However, you can put your credentials in app.config

```
planetPayment.userId
planetPayment.password
planetPayment.entityId
```

### Enviroment
To change the environment that you are going to test, you can update a value of app.config.

```
planetPayment.url
```

and put the URL: 

```
- Test: https://test.planetpaymentgateway.com/
- Live: https://planetpaymentgateway.com/
```

### Test Credit Card

|Brand| Number | CVV | Expiry Date |
|-|-|-|-|
|VISA|4200000000000000 <br /> 4711100000000000 |any 3 digits|any date after today|
|MASTER|5454545454545454 <br /> 5212345678901234 |any 3 digits|any date after today|
|AMEX|377777777777770 <br />375987000000005 |any 3 digits|any date after today|

For more information about the data testing, [click here](https://docs.planetpaymentgateway.com/reference/parameters#test-accounts) 

## Logging
All parameters (sending and receiving) are saved on a text file.

- **File name**: planet_payment_trace.log
- **Folder**: Debug\logs 

**Note**: A text file will be created daily with format planet_payment_trace.yyyy-MM-dd.log

## Built With

- [RestSharp](http://restsharp.org/) - To communicate with Planet Payment API 
- [Log4net](https://logging.apache.org/log4net/) - To save logs


## Reference

- [Planet Payment Documentation](https://docs.planetpaymentgateway.com)

## Author
- **Juan José Chiroque** - [LinkedIn](https://www.linkedin.com/in/jjchiroque)
