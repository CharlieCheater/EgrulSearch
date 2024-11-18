// See https://aka.ms/new-console-template for more information
using InternationalPayments.Egrul;

Console.WriteLine("Hello, World!");
EgrulService service = new EgrulService();
Console.WriteLine((await service.GetEgrulInformation("7719468312"))[0].FullName);
Console.ReadKey();
