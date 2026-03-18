using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralPatterns;

public interface IKava
{
    string GetDescription();
    double GetPrice();
}

public class Kava : IKava
{
    public virtual string GetDescription() => "";
    public virtual double GetPrice() => 0;
}

public class CoffeeDecorator : IKava
{
    protected readonly IKava _kava;
    protected readonly string _description;
    protected readonly double _price;

    public CoffeeDecorator(IKava kava, string description, double price)
    {
        _kava = kava;
        _description = description;
        _price = price;
    }

    public string GetDescription()
    {
        return _kava.GetDescription() + " " + _description;
    }

    public double GetPrice()
    {
        return _kava.GetPrice() + _price;
    }
}
