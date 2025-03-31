namespace Container.s

{
    public class ContainerShip(int maxSpeed)
    {
        public List<Container> Containers { get; set; } = new List<Container>();
        public int MaxSpeed { get; set; } = maxSpeed;
        public int CargoMass { get; set; } = 0;

        
        public void LoadContainer(Container container)
        {
            Containers.Add(container);

            Console.WriteLine($"Załadowano kontener {container} na statek.");
        }

        public void UnloadContainer(Container container)
        {
            if (Containers.Remove(container))
            {
                Console.WriteLine($"Rozładowano kontener {container} ze statku");
            }
            else
            {
                Console.WriteLine("Nie znaleziono kontenera na statku.");
            }
        }
        
        public void PrintBasicInfo()
        {
            Console.WriteLine("\nInformacje o statku:");
            Console.WriteLine($"Maksymalna prędkość: {MaxSpeed} węzłów");
            Console.WriteLine($"Liczba kontenerów: {Containers.Count}");
        }
        
        public void PrintCargoInfo()
        {
            Console.WriteLine("\nInformacje o kontenerach:");
            Console.WriteLine("Numery seryjne kontenerow na statku:");
            foreach (var container in Containers)
            {
                Console.WriteLine(container.ToString());
            }
        }
    }
}