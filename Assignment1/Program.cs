namespace Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Computer c1 = new Computer("sam","123",1234343,10000);
        }
    }

    public class Computer 
    {
        private string brand;
        private string model;
        private long serialnumber;
        private double price;

        public Computer(string br, string m, long sn, double pr)
        {
            br = brand;
            m = model;
            sn = serialnumber;
            pr = price;
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
    }
}
