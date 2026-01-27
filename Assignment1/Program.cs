using System.Runtime.Intrinsics.Arm;

namespace Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Computer c1 = new Computer("HP","Omnibook",211223,1600);
            Computer c2 = new Computer("sam","123",1234343,10000);
            Console.WriteLine(c1);

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
