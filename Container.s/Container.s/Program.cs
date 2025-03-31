namespace Container.s;

class Program
{
    static void Main(string[] args)
    {
        ContainerShip ship = new ContainerShip(25);
        
        LiquidContainer liquidContainer = new LiquidContainer(10, 5, 200);
        liquidContainer.LoadContainer("milk", 150);
        ship.LoadContainer(liquidContainer);
        
        GasContainer gasContainer = new GasContainer(8, 4, 180);
        gasContainer.LoadContainer("gasoline", 80);
        ship.LoadContainer(gasContainer);
        
        CoolingContainer coolingContainer = new CoolingContainer(12, 6, 220, 5);
        coolingContainer.LoadContainer("Fish", 100);
        ship.LoadContainer(coolingContainer);
        
        ship.UnloadContainer(gasContainer);
        
        ship.PrintBasicInfo();
        
        ship.PrintCargoInfo();
        
    }
}