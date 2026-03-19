using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StructuralPatterns;

public interface IMenuElement
{
    string Name { get; }
    double Price { get; }
    void Display(IPrinter printer, int indent = 0);
    public void AddChild(IMenuElement child);
    public List<IMenuElement> Children { get; }
}

public class AbstractMenuElement : IMenuElement
{
    public string Name { get; set; }
    public double Price { get; set; }
    public List<IMenuElement> Children { get; set; } = new List<IMenuElement>();
    public AbstractMenuElement(string name, double price)
    {
        Name = name;
        Price = price;
    }
    public void AddChild(IMenuElement child)
    {
        Children.Add(child);
    }
    public void Display(IPrinter printer, int indent = 0)
    {
        printer.Print(this, indent);
    }
}

public class Dish : AbstractMenuElement
{
    public Dish(string name, double price) : base(name, price)
    {
        Price = price;
    }
}

public class Section : AbstractMenuElement
{
    public Section(string name) : base(name, 0)
    {
        Price = 0; // Sections don't have a price, only dishes do
    }
}

public class DiscountOffer : AbstractMenuElement
{
    public bool IsSection;
    public DiscountOffer(string name, double discountPercentage, bool isSection) : base(name, 0)
    {
        Name = name;
        if (isSection)
            { Price = 0; }
        else
            { Price = Price - (Price * discountPercentage / 100); }
        IsSection = isSection;
    }
}

