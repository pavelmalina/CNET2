namespace Model
{
    public interface IGreetable
    {
        string Name { get; }

        string SayHello();
    }

    public class Client : IGreetable
    {
        public string Name { get; set; }

        public string SayHello() => $"Hello {Name}";
    }

    public class VipClient : IGreetable
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public string SayHello() => $"Hello VIP: {Name} ({Status})";
    }
}
