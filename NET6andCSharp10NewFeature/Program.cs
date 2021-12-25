
using NET6andCSharp10NewFeature;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");

//global using - global using static System.Console; - global using Env = System.Environment; <ImplicitUsings>enable</ImplicitUsings>

//global using static System.Console;
//global using Env = System.Environment;

//File scoped namespace declarations    


//CallerArgumentExpression attribute diagnostics
Validate(10);

static void Validate(int condition, [CallerArgumentExpression("condition")] string? message = null)
{
    if (condition == 10)
    {
        try
        {
            throw new InvalidOperationException($"Argument failed validation: condition value = {message}");
        }
        catch (Exception ex)
        {

           Console.WriteLine(ex.ToString());
        }
      
    }
}

//Extended property patterns - Patternler C#7 ile beraber geldi. 10 versiyonunda gelen gelişme - Patterns is eşitliği ile atama
//    if (e is MethodCallExpression { Method.Name: "MethodName" })
//    if (e is MethodCallExpression { Method: { Name: "MethodName" } })

object greeting = "Hello, World!";
if (greeting is string message)
{
    Console.WriteLine(message.ToLower());  // output: hello, world!
}

int? xNullable = 7;
int y = 23;
object yBoxed = y;
if (xNullable is int a && yBoxed is int b)
{
    Console.WriteLine(a + b);  // output: 30
}

Developer obj = new Developer() ;
obj.FirstName = "Birkan";
Manager objManager = new Manager() ;
objManager.FirstName = "Cengiz";
objManager.YearOfBirth = 1980;
obj.Manager = objManager;

if (obj is Developer { Manager: { FirstName: "Cengiz" } } developerWithManager)
{
    Console.WriteLine(developerWithManager.FirstName);
}
if (obj is Developer { Manager.FirstName: "Cengiz" } developerWithCSharp10)
{
    Console.WriteLine(developerWithCSharp10.FirstName); 
}

if (obj is Developer { Manager: { FirstName: { Length: 6 }, YearOfBirth: 1980 } } developer)
{
    Console.WriteLine(developer.FirstName);
}
if (obj is Developer { Manager.FirstName.Length: 6, Manager.YearOfBirth: 1980 } devCSharp10)
{
    Console.WriteLine(devCSharp10.FirstName);
}



//record - value-based equality record is referance type but value type behavior C#9 ile geldi

Student student = new Student { FirstName = "Birkan", LastName = "Dev", ID = 111 };

Student similarStudent = new Student { FirstName = "Birkan", LastName = "Dev", ID = 222 };
Console.WriteLine(student == similarStudent); //false, since ID's are different
Student similarStudent2 = new Student { FirstName = "Birkan", LastName = "Dev", ID = 222 };
Console.WriteLine(student == similarStudent2); //true, value-based behavior

//Positional syntax for property definition
Person person = new("Birkan", "Dev");
//person.FirstName = "ad"; recordlarda default verilen proplar immutable dır
Console.WriteLine(person);

//struct record ilk değer atamasının zorunlu tutulduğu tip C#10 ile geldi

//Lambdas with natural types
Func<string, int> parse = (string s) => int.Parse(s);
var naturalParse = (string s) => int.Parse(s);

var read = Console.Read; // Just one overload; Func<int> inferred
//var write = Console.Write; // ERROR: Multiple overloads, can't choose
var writeCorrect = (string value)  => Console.Write(value);

//var choose = (bool b) => b ? 1 : "two"; // ERROR: Can't infer return type
var chooseTrue = object (bool b) => b ? 1 : "two"; // Func<bool, object>

var parseAttr =[Example(1)] (string s) => int.Parse(s);

//Available in C# 9.0 and later, a with expression produces a copy of its operand with the specified properties and fields modified, c#10 has some improvment 
//with sadece record type'lar da kullanılabılır.
var person2 = person with { LastName = "Test" };

Console.WriteLine(person == person2);

var person3= person with { LastName = "Dev" };
Console.WriteLine(person == person3);

//hata alır
//Worker worker = new Worker() { FirstName = "Birkan", LastName = "Dev"};
//var worker2 = worker with { FirstName = "Cengiz" };

//class vs struct vs record

//struct paramatrelerını de tteker teker ımmutable yapmamak ıcın record struct geldi.

