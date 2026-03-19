using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StructuralPatterns;


public interface IPrinter
{
    public void Print(IMenuElement menuElement, int indent = 0);
}

public class TextPrinter : IPrinter
{
    public void Print(IMenuElement menuElement, int indent = 0)
    {
        if (menuElement.Price > 0)
        { Console.WriteLine(new string(' ', indent) + menuElement.Name + " " + menuElement.Price); }
        else
        { Console.WriteLine(new string(' ', indent) + menuElement.Name); }

        foreach (var child in menuElement.Children)
        {
            child.Display(this, indent + 2);
        }
    }
}

public class HTMLprinter : IPrinter
{
    public void Print(IMenuElement menuElement, int indent = 0)
    {
        Console.WriteLine("<html><body>");
        PrintMenuElement(menuElement, 0);
        Console.WriteLine("</body></html>");
    }
    private void PrintMenuElement(IMenuElement menuElement, int indent)
    {
        string indentStr = new string(' ', indent * 2);
        if (menuElement.Price > 0)
        {
            Console.WriteLine($"{indentStr}<p>{menuElement.Name} - {menuElement.Price}</p>");
        }
        else
        {
            Console.WriteLine($"{indentStr}<h2>{menuElement.Name}</h2>");
        }
        foreach (var child in menuElement.Children)
        {
            PrintMenuElement(child, indent + 1);
        }
    }
}


public class JsonPrinter : IPrinter
{
    public void Print(IMenuElement menuElement, int indent = 0)
    {
        Console.WriteLine("{");
        PrintMenuElement(menuElement, 1);
        Console.WriteLine("}");
    }

    private void PrintMenuElement(IMenuElement menuElement, int indent)
    {
        string indentStr = new string(' ', indent * 2);

        if (menuElement.Price > 0)
        {
            Console.WriteLine($"{indentStr}{{\"Name\":{menuElement.Name}," +
                $"\"Price\":{menuElement.Price}}},");

        }
        else
        {
            Console.WriteLine($"{indentStr}\"{menuElement.Name}\":[");

            foreach (var child in menuElement.Children)
            {
                PrintMenuElement(child, indent + 1);
            }

            Console.WriteLine($"{indentStr}]");
        }
        
    }
}
