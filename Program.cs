using EntityModels.Models;
using Microsoft.EntityFrameworkCore;
using Week3EntityFramework.Dtos;

var context = new IndustryConnectWeek2Context();

//var customer = new Customer
//{
//    DateOfBirth = DateTime.Now.AddYears(-20)
//};


//Console.WriteLine("Please enter the customer firstname?");

//customer.FirstName = Console.ReadLine();

//Console.WriteLine("Please enter the customer lastname?");

//customer.LastName = Console.ReadLine();


//var customers = context.Customers.ToList();

//foreach (Customer c in customers)
//{   
//    Console.WriteLine("Hello I'm " + c.FirstName);
//}

//Console.WriteLine($"Your new customer is {customer.FirstName} {customer.LastName}");

//Console.WriteLine("Do you want to save this customer to the database?");

//var response = Console.ReadLine();

//if (response?.ToLower() == "y")
//{
//    context.Customers.Add(customer);
//    context.SaveChanges();
//}



var sales = context.Sales.Include(c => c.Customer)
    .Include(p => p.Product).ToList();

var salesDto = new List<SaleDto>();

foreach (Sale s in sales)
{
    salesDto.Add(new SaleDto(s));
}



//context.Sales.Add(new Sale
//{
//    ProductId = 1,
//    CustomerId = 1,
//    StoreId = 1,
//    DateSold = DateTime.Now
//});


//context.SaveChanges();




Console.WriteLine("Which customer record would you like to update?");

var response = Convert.ToInt32(Console.ReadLine());

var customer = context.Customers.Include(s => s.Sales)
    .ThenInclude(p => p.Product)
    .FirstOrDefault(c => c.Id == response);


var total = customer.Sales.Select(s => s.Product.Price).Sum();


var customerSales = context.CustomerSales.ToList();

//var totalsales = customer.Sales



//Console.WriteLine($"The customer you have retrieved is {customer?.FirstName} {customer?.LastName}");

//Console.WriteLine($"Would you like to updated the firstname? y/n");

//var updateResponse = Console.ReadLine();

//if (updateResponse?.ToLower() == "y")
//{

//    Console.WriteLine($"Please enter the new name");

//    customer.FirstName = Console.ReadLine();
//    context.Customers.Add(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
//    context.SaveChanges();
//}

//Q1:- Using the linq queries retrieve a list of all customers from the database who don't have sales

var customerswithoutSales = context.Customers
    .Where(context => !context.Sales.Any(s => s.CustomerId == context.CustomerId))
    .ToList();

foreach (var customer in customerswithoutSales)
{
    Console.WriteLine($"{customer.Name} - {customer.CustomerId}");
}

//Q2:- Insert a new customer with a sale record

var newCustomer = new Customer
{
    FirstName = "Krishna Mangrati",
    Sales = new List<Sale>()

};

var newSale = new Sale
{
    SaleDate = DateTime.Now,
    Customer = newCustomer
};

newCustomer.Sales.Add(newSale);

context.Customers.Add(newCustomer);

context.SaveChanges();

Console.WriteLine($"Customer {newCustomer.Name} with Sale ID {newSale.Id} added successfully!");

//Q3:- Add a new store

var newStore = new Store
{
    Name = "Brisbane Store ",
    Location = "45 Garden terrace, New Market "
};

context.Stores.Add(newStore);

context.SaveChanges();

Console.WriteLine($"Store '{newStore.Name}' added sucessfully with Store Id {newStore.Id}");


//Q4:- Find the list of all store that have sales

var storesWithSales = context.Stores
    .Where(store => store.Sales.Any()
    .ToList();

foreach (var store in storesWithSales)

    Console.WriteLine($"Store: {store.Name}, Location: {store.Location} ");
}


Console.ReadLine();









