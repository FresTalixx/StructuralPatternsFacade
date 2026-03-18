using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StructuralPatterns;

public interface IKava
{
    string GetDescription();
    double GetPrice();
}

public class Espresso : IKava
{
    public string GetDescription() => "Espresso";

    public double GetPrice() => 35;
}

public class Kava : IKava
{
    public virtual string GetDescription() => "";
    public virtual double GetPrice() => 0;
}

public class KavaDecorator : IKava
{
    protected readonly IKava _kava;
    protected readonly string _description;
    protected readonly double _price;

    public KavaDecorator(IKava kava, string description, double price)
    {
        _kava = kava;
        _description = description;
        _price = price;
    }

    public string GetDescription()
    {
        var description = _kava.GetDescription();

        // Pattern: Milk (x2)
        var pattern = $@"{_description} \(x(\d+)\)";

        var match = Regex.Match(description, pattern);

        if (match.Success)
        {
            int count = int.Parse(match.Groups[1].Value) + 1;

            return Regex.Replace(description, pattern, $"{_description} (x{count})");
            
        }

        if (description.Contains(_description))
        {
            return description.Replace(_description, $"{_description} (x2)");
        }

        return description + " " + _description;
    }

    public double GetPrice()
    {
        return _kava.GetPrice() + _price;
    }
}
