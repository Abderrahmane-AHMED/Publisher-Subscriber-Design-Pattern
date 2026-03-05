
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

class PublisherSubscriberDesignPattern 
{
    #region   Event 1 Temperature Change 
    public class TemperaturChangeEventArguments  : EventArgs
    {
        public double OldTemperature { get; }
        public double NewTemperature { get; }
        public double Differance { get; }

        public TemperaturChangeEventArguments(double oldtemperature, double newTemperature)
        {
            OldTemperature = oldtemperature;
            NewTemperature = newTemperature;
            Differance = newTemperature - oldtemperature;

        }
    }
    public class Thermostat
    {
        public event EventHandler<TemperaturChangeEventArguments> TemperaturChangeEvent;

        public double OldTemperature;
        public double CurrentTemperature;

        public void SetTemperature(double newTemperature)
        {


            if (newTemperature != CurrentTemperature)
            {
                OldTemperature = CurrentTemperature;
                CurrentTemperature = newTemperature;
                OnTemperatureChange(OldTemperature, CurrentTemperature);
            }
        }
        private void OnTemperatureChange(double oldTemperature, double currentTemperature)
        {
            OnTemperatureChange(new TemperaturChangeEventArguments(oldTemperature, currentTemperature));
        }

        protected virtual void OnTemperatureChange(TemperaturChangeEventArguments args)
        {
            TemperaturChangeEvent?.Invoke(this, args);
        }
    }
    public class Display
    {
        public void Subscribe(Thermostat thermostat)
        {
            thermostat.TemperaturChangeEvent += HandleTemperatureChange;
        }
        public void HandleTemperatureChange(Object sender, TemperaturChangeEventArguments args)
        {

            Console.WriteLine("\t\t---- Temperature Change : ---- \n");
            Console.WriteLine($"\t\tTemperature Change From {args.OldTemperature}°C");
            Console.WriteLine($"\t\tTemperature Change To {args.NewTemperature}°C");
            Console.WriteLine($"\t\tTemperature Differance To  {args.Differance}°C");
        }
    }
    #endregion


    #region Event 2  News Publisher Subscriber
    public class NewsArticle  
    {
        public string Title { get; }
        public string Content { get; }

        public NewsArticle(string title ,string content)
        {
            Title = title;
            Content = content;
        }
    }
    public class NewsPublisher
    {
        public event EventHandler<NewsArticle> PublisherNews;


        public void NewNewsPublisher(string titel , string content)
        {
            var article = new NewsArticle(titel , content );
            NewNewsPublisher(article);
        }

        public virtual void NewNewsPublisher(NewsArticle article) 
        { 
            PublisherNews?.Invoke(this, article);
        }
    }
    public class NewsSubscriber
    {
        public string Name { get; }

        public NewsSubscriber(string name)
        {
            Name = name;    
        }
        public void Subscribe(NewsPublisher publisher)
        {
            publisher.PublisherNews += HandleNewNews;
        }

        public void UnSubscribe(NewsPublisher publisher)
        {
            publisher.PublisherNews -= HandleNewNews;
        }

        public void HandleNewNews(object sender , NewsArticle article)
        {
               Console.WriteLine($"{Name} Recevied A New Article :\n");
               Console.WriteLine($"News subscriber: {article.Title}");
               Console.WriteLine($"Content : {article.Content}");
                Console.WriteLine();
        }
    }

    #endregion

    #region  Event 3 New Order 
    public class OrderEventArguments   : EventArgs
    {
        public int OrderId { get; }
        public string ClientEmail { get; }
        public int OrderTotalPrice { get; }
        public string ClientAddress {  get; }
        public string OrderDetails {  get; }

        public OrderEventArguments(int orderId ,string clientEmail, int orderTotalprice, string clientaddress , string orederDetails)
        {
            OrderId = orderId;
            ClientEmail = clientEmail;
            OrderTotalPrice = orderTotalprice;
            ClientAddress = clientaddress;
            OrderDetails = orederDetails;
        }
    }
    public class Order
    {
        public event EventHandler<OrderEventArguments> OnOrderCreated;


        public void Create(int orderId, string clientEmail, int orderTotalprice, string clientaddress, string orederDetails)
        {
            Console.WriteLine("New Order Created , New Will Notify Eveyone By Raising The Event.\n");

            if (OnOrderCreated != null)
            {
                OnOrderCreated(this , new OrderEventArguments(orderId , clientEmail, orderTotalprice, clientaddress, orederDetails));
            }
        }
    

    }
    public class EmailService
    {
        public void SubScribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }
        public void UnSubScribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }
        public void HandleNewOrder(Object sender , OrderEventArguments args)
        {
            Console.WriteLine("--------------Email Service--------------");
            Console.WriteLine("Email Service Object Received A New Order Event");
            Console.WriteLine($"Order Id : {args.OrderId}");
            Console.WriteLine($"Order Details : {args.OrderDetails}");
            Console.WriteLine($"Client Address: {args.ClientAddress}");
            Console.WriteLine($"Order Total Price : {args.OrderTotalPrice}");
            Console.WriteLine($"Client Email : {args.ClientEmail}");
            Console.WriteLine("-------------- Send Email --------------");
            Console.WriteLine("\n----------------------------------------------\n");
        }
    }
    public class SMSService
    {
        public void SubScribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }
        public void UnSubScribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }
        public void HandleNewOrder(Object sender, OrderEventArguments args)
        {
            Console.WriteLine("--------------SMS  Service--------------");
            Console.WriteLine("SMS Service Object Received A New Order Event");
            Console.WriteLine($"Order Id : {args.OrderId}");
            Console.WriteLine($"Order Details : {args.OrderDetails}");
            Console.WriteLine($"Client Address: {args.ClientAddress}");
            Console.WriteLine($"Order Total Price : {args.OrderTotalPrice}");
            Console.WriteLine($"Client Email : {args.ClientEmail}");
            Console.WriteLine("-------------- Send SMS  --------------");
            Console.WriteLine("\n----------------------------------------------\n");
           
        }
    }
    public class ShippingService
    {
        public void SubScribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }
        public void UnSubScribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }
        public void HandleNewOrder(Object sender, OrderEventArguments args)
        {
            Console.WriteLine("--------------Shipping Service--------------");
            Console.WriteLine("SMS Service Object Received A New Order Event");
            Console.WriteLine($"Order Id : {args.OrderId}");
            Console.WriteLine($"Order Details : {args.OrderDetails}");
            Console.WriteLine($"Client Address: {args.ClientAddress}");
            Console.WriteLine($"Order Total Price : {args.OrderTotalPrice}");
            Console.WriteLine($"Client Email : {args.ClientEmail}");
            Console.WriteLine("-------------- Send Shipping --------------");
            Console.WriteLine("\n----------------------------------------------\n");

        }
    }

    #endregion

    #region Program Class
    public class  Program
    {
        static void Main(string[] args)
        {


            #region Example Event 1 Temperature Change

            //Thermostat thermostat = new Thermostat();
            //Display display = new Display();

            //display.Subscribe(thermostat);

            //thermostat.SetTemperature(20);
            //Console.WriteLine();

            //thermostat.SetTemperature(30);
            //thermostat.SetTemperature(30);
            //thermostat.SetTemperature(30);
            //Console.WriteLine();

            #endregion


            #region Example Event 2 News Publisher Subscriber

            //NewsPublisher publisher = new NewsPublisher();
            //NewsSubscriber subscriber1 = new NewsSubscriber("Subscriber 1");
            //NewsSubscriber subscriber2 = new NewsSubscriber("Subscriber 2");

            //subscriber1.Subscribe(publisher);
            //subscriber2.Subscribe(publisher);

            //publisher.NewNewsPublisher("Breakin News", "A Significant Event Just Happened !");

            //subscriber2.UnSubscribe(publisher);
            //publisher.NewNewsPublisher("Tech Update", "New Gagets Are Hitting The Market .");

            #endregion

            #region Example Event 3 New Order

            var  order = new Order();

            EmailService emailService = new EmailService();
            SMSService sMSService = new SMSService();
            ShippingService shippingService = new ShippingService();

            emailService.SubScribe(order);
            shippingService.SubScribe(order);
           

            order.Create(1, "abderrahmane@gmail.com", 100, "259 Rue Des Capucine", "Books");

            emailService.UnSubScribe(order);
            shippingService.UnSubScribe(order);
            sMSService.SubScribe(order);
            order.Create(2, "ahmed@gmail.com", 50, "111 Rue Des Chataigners", "Phone , Scrin");

            #endregion
        }

    }
    #endregion


}