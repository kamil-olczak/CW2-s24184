using System.ComponentModel;
using System.Text.Json;

namespace Container.s;

public class Container
{
    private int Id { get; set; }
    public int Mass { get; set; } = 0;
    public int Height { get; }
    public int Depth { get; }
    public int MaxLoad { get; }
    public string? SerialNumber { get; private set; }
    public static List<Container> Containers { get; set; } = new List<Container>();
    

    public Container(int height, int depth, int maxLoad)
    {
        Height = height;
        Depth = depth;
        MaxLoad = maxLoad;
        Containers.Add(this);
        Id = Containers.Count;
    }
    
    protected void GenerateSerialNumber(string type)
    {
        SerialNumber = $"KON-{type}-{Id}";
    }

    public int LoadContainer(int load, double maxLoadAllowed)
    {
        try
        {
            if ((Mass += load) > maxLoadAllowed )
            {
                throw new OverfillException("Container will be overloaded. Cannot load container.");
            }
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e);
            Mass -= load;
            return 1;
        }
        return 0;
    }

    public int EmptyContainer(int massToEmpty , double minLoadAllowed)
    {
        if ((Mass -= massToEmpty) < minLoadAllowed)
        {
            Mass += massToEmpty;
            return 1;
        }
        return 0;
    }
    
    public override string ToString()
    {
        return "Base Container";
    }
}












public class LiquidContainer : Container , IHazzardNotfier
{   
    public string? Contains { get; set; }
    
    public LiquidContainer(int height, int depth, int maxLoad) : base(height, depth, maxLoad)
    {
        GenerateSerialNumber("L");
    }

    public void LoadContainer(string cargo, int load)
    {
        if (Contains == null)
        {
            Contains = cargo;
        }

        if (Contains == cargo)
        {
            if (cargo == "milk")
            {
                var maxLoadAllowed = MaxLoad * 0.9;
                base.LoadContainer(load, maxLoadAllowed);
            }

            if (cargo == "gasoline")
            {
                var maxLoadAllowed = MaxLoad * 0.5;
                if (base.LoadContainer(load, maxLoadAllowed) != 0)
                {
                    HazzardNotfier(
                        $"Warning, hazardous activity!!! Container contains hazardous material, it cannot be loaded over 50% of max load. Container: {SerialNumber}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Container already contains {cargo}. Cannot load container.");
        }
        
    }

    public void EmptyContainer(int massToEmpty)
    {
        var minLoadAllowed = 1; 
        base.EmptyContainer(massToEmpty, minLoadAllowed);
        Contains = null;
    }
    
    public void HazzardNotfier(string message)
    {
        Console.WriteLine(message);
    }
    
    public override string ToString()
    {
        return SerialNumber ?? "null";
    }
    
}










public class GasContainer : Container, IHazzardNotfier
{
    public string? Contains { get; set; }
    
    
    public GasContainer(int height, int depth, int maxLoad) : base(height, depth, maxLoad)
    {
        GenerateSerialNumber("G");
    }
    
    public void LoadContainer(string cargo, int load)
    {
        if (Contains == null)
        {
            Contains = cargo;
        }


        if (Contains == cargo)
        {
            var maxloadAllowed = MaxLoad * 0.9;
            base.LoadContainer(load, maxloadAllowed);
        }
        else
        {
            Console.WriteLine($"Container already contains {cargo}. Cannot load container.");
        }
        
    }

    public void EmptyContainer(int massToEmpty)
    {
        var minLoadAllowed = MaxLoad * 0.05;
        if (base.EmptyContainer(massToEmpty, minLoadAllowed) == 1)
        {
            HazzardNotfier($"Warning, hazardous activity!!! Container contains gas, it cannot be emptied below 5% of its max load. Container: {SerialNumber}");
        }
    }
    
    public void HazzardNotfier(string message)
    {
        Console.WriteLine(message);
    }
    
    public override string ToString()
    {
        return SerialNumber ?? "null";
    }
}










public class CoolingContainer : Container
{
    public string? Contains { get; set; }
    public int? ContainerTemp { get; set; }
    public Dictionary<string, int> ProductTemp { get; } = new Dictionary<string, int>()
    {
        { "Bananas", 13 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7 },
        { "Sausages", 5 },
        { "Butter", 20 },
        { "Eggs", 19 }
    };
    public CoolingContainer(int height, int depth, int maxLoad, int containerTemp) : base(height, depth, maxLoad)
    {
        ContainerTemp = containerTemp;
        GenerateSerialNumber("C");
    }
    
    public void LoadContainer(string cargo, int load)
    {
        if (Contains == null)
        {
            if (ContainerTemp < ProductTemp[cargo])
            {
                Console.WriteLine($"Cannot load this {cargo}, container temperature is lower than {ProductTemp[cargo]}." );
            }
            Contains = cargo;
        }

        if (Contains == cargo)
        {
            var maxloadAllowed = MaxLoad * 0.9;
            base.LoadContainer(load, maxloadAllowed);
        }
        else
        {
            Console.WriteLine($"Container already contains {cargo}. Cannot load container.");
        }
    }
    
    public override string ToString()
    {
        return SerialNumber ?? "null";
    }
}
