using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.Arm;

namespace Assignment1
{
    internal class Program
    {
        private const string password = "password";

        private static Computer[] inventory = Array.Empty<Computer>();
        private static int currentCount = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my computer store: ");
            Console.WriteLine("Enter maximum number of computers: ");
            int maxComputers = int.Parse(Console.ReadLine());

            inventory = new Computer[maxComputers];
            currentCount = 0;

            int choice;

            do {
                DisplayMenu();
                choice = GetMenuChoice();

                switch (choice)
                { 
                    case 1:
                        Option1();
                        break;
                    case 2:
                        Option2();
                        break;
                    case 3:
                        Option3();
                        break;
                    case 4:
                        Option4();
                        break;
                    default:
                        break;
                }
            
            } while (choice != 5);


            //Computer c1 = new Computer("HP","Omnibook",211223,1600);
            //Computer c2 = new Computer("sam","123",1234343,10000);
            //Console.WriteLine(c1);

        }

        public static void DisplayMenu() 
        {
            Console.WriteLine("Main Menu: ");
            Console.WriteLine("1. Enter new computer");
            Console.WriteLine("2. Update computer");
            Console.WriteLine("3. Find computer by brand");
            Console.WriteLine("4. Find computer cheaper than a price");
            Console.WriteLine("5. Exit ");

        }

        public static int GetMenuChoice()
        {
            int choice;
            do
            {
                Console.Write("Enter your Choice[1-5]: ");
                choice = int.Parse(Console.ReadLine());
            } while (choice < 1 || choice > 5);

            return choice;
        }

        public static bool VerifyPassword()
        {
            int MxAttempts = 3;

            for (int i = 1; i <= MxAttempts; i++)
            {
                Console.Write("Enter your password: ");
                string? input = Console.ReadLine();

                if (input == password)
                {
                    Console.WriteLine("Loading...");
                    return true;
                }
                
                Console.WriteLine("Incorrect password.\nTry Again");
            }

            return false;
        }


        public static void Option1()
        {
            bool ok = VerifyPassword();
            if (!ok)
            {return;}


            int available = inventory.Length - currentCount;
            if (available == 0)
            {
                Console.WriteLine("Inventory is full. Cannot add more computers.");
                return;
            }

            Console.Write($"How many computers would you like to enter (max {available}): ");
            if (!int.TryParse(Console.ReadLine(), out int computers) || computers <= 0)
            {
                Console.WriteLine("Invalid number. Returning to main menu.");
                return;
            }

            if (computers > available)
            {
                Console.WriteLine($"Only {available} slot(s) available. Will add {available} computer(s).");
                computers = available;
            }

            for (int i = 0; i < computers; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Entering computer {currentCount + 1} of {inventory.Length}:");

                Console.Write("Brand: ");
                string brand = Console.ReadLine();

                Console.Write("Model: ");
                string model = Console.ReadLine();

                Console.Write("Serial number: ");
                long serialNumber = 0;
                long.TryParse(Console.ReadLine(), out serialNumber);

                Console.Write("Price: ");
                double price = 0;
                double.TryParse(Console.ReadLine(), out price);

                inventory[currentCount++] = new Computer(brand, model, serialNumber, price);
                Console.WriteLine("Computer added.");
            }

            Console.WriteLine($"{computers} computer(s) added. {inventory.Length - currentCount} slot(s) remaining.");
        }

        public static void Option2()
        {
            bool ok = VerifyPassword();
            if (!ok)
            { return; }

            

            if (currentCount == 0)
            {
                Console.WriteLine("No computers in inventory to update.");
                return;
            }

            while (true)
            {
            ask1: Console.Write($"Enter computer index to update [0 - {inventory.Length - 1}]: ");
                if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 0 || idx >= inventory.Length)
                {
                    Console.WriteLine("Invalid index.");
                    goto ask1;
                }

                if (inventory[idx] == null)
                {
                    Console.WriteLine("No computer at that index.");
                    goto ask1;
                }

               
                Computer comp = inventory[idx];
                bool done = false;
                while (!done)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Computer #{idx}");
                    Console.WriteLine($"Brand: {comp.GetBrand()}");
                    Console.WriteLine($"Model: {comp.GetModel()}");
                    Console.WriteLine($"SN: {comp.GetSerialNumber()}");
                    Console.WriteLine($"Price: {comp.GetPrice()}");
                    Console.WriteLine();
                    Console.WriteLine("What information would you like to change?");
                    Console.WriteLine("1. brand");
                    Console.WriteLine("2. model");
                    Console.WriteLine("3. SN");
                    Console.WriteLine("4. price");
                    Console.WriteLine("5. Quit");
                    Console.Write("Enter your choice [1-5]: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5)
                    {
                        Console.WriteLine("Invalid choice. Enter 1-5.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.Write("New brand: ");
                            comp.SetBrand(Console.ReadLine());
                            break;
                        case 2:
                            Console.Write("New model: ");
                            comp.SetModel(Console.ReadLine());
                            break;
                        case 3:
                            Console.Write("New serial number: ");
                            if (long.TryParse(Console.ReadLine(), out long sn)) comp.SetSerialNumber(sn);
                            else Console.WriteLine("Invalid serial number.");
                            break;
                        case 4:
                            Console.Write("New price: ");
                            if (double.TryParse(Console.ReadLine(), out double pr) && pr >= 0) comp.SetPrice(pr);
                            else Console.WriteLine("Invalid price.");
                            break;
                        case 5:
                            done = true;
                            break;
                    }
                }
                return;
            }
        }
        public static void Option3()
        {
            Console.Write("Enter brand to search for: ");
            string brandQuery = Console.ReadLine();

            int found = 0;
            for (int i = 0; i < currentCount; i++)
            {
                var c = inventory[i];
                if (c == null) continue;
                if (string.Equals(c.GetBrand(), brandQuery))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Computer #{i}");
                    Console.WriteLine(c.ToString());
                    found++;
                }
            }

            if (found == 0)
                Console.WriteLine($"No computers found with brand {brandQuery}.");
           
        }
        public static void Option4() 
        {
            Console.Write("Enter price: ");
            double.TryParse(Console.ReadLine(), out double limit);

            int found = 0;
            for (int i = 0; i < currentCount; i++)
            {
                var c = inventory[i];
                if (c == null) continue;
                if (c.GetPrice() < limit)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Computer #{i}");
                    Console.WriteLine(c.ToString());
                    found++;
                }
            }

            if (found == 0)
                Console.WriteLine($"No computers found cheaper than {limit}.");
            
        }
    }

    public class Computer 
    {
        private string brand;
        private string model;
        private long serialnumber;
        private double price;

        private static int NumberOfCreatedComputer = 0;

        public Computer(string brand, string model, long serialnumber, double price)
        {
            this.brand = brand;
            this.model = model;
            this.serialnumber = serialnumber;
            this.price = price;
        }

        //getters
        public string GetBrand() { return brand; }
        public string GetModel() { return model; }
        public long GetSerialNumber() { return serialnumber; }
        public double GetPrice() { return price; }

        //setters
        public void SetBrand(string br)
        { 
            brand = br;
        }
        public void SetModel(string model)
        {
            this.model = model;
        }
        public void SetSerialNumber(long serialnumber) 
        {
            this.serialnumber = serialnumber;
        }
        public void SetPrice(double price) 
        {
            this.price = price;
        }

        public override string? ToString()
        {
            return $"Brand: {brand}\nModel: {model}\nSN: {serialnumber}\nPrice: {price}";
        }

        public static int findNumberOFCreatedComputer()
        { return NumberOfCreatedComputer; }
    }
}
