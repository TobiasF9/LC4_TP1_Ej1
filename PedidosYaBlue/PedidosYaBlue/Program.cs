using System;
using System.Collections.Generic;

namespace PedidosYaBlue
{
    //Singleton
    public class DeliveryManager
    {
        private static DeliveryManager _instance;

        private DeliveryManager() { }

        public static DeliveryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DeliveryManager();
                }
                return _instance;
            }
        }

        public void ExecuteDelivery(IDeliveryProvider delivery, string address)
        {
            delivery.Deliver(address);
        }
    }

    //Factory Method
    public interface IDeliveryProvider
    {
        void Deliver(string address);
    }

    public class Drone : IDeliveryProvider
    {
        public void Deliver(string address)
        {
            Console.WriteLine($"Entregando a {address} usando un dron.");
        }
    }

    public class GroundVehicle : IDeliveryProvider
    {
        public void Deliver(string address)
        {
            Console.WriteLine($"Entregando a {address} usando un vehículo terrestre.");
        }
    }

    //Observer Pattern
    public interface IObserver
    {
        void Update(string message);
    }

    public class DeliveryStatus : IObserver
    {
        private string _status;

        public void Update(string message)
        {
            _status = message;
            Console.WriteLine($"Estado de entrega actualizado: {_status}");
        }
    }

    public class Observable
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDeliveryProvider drone = new Drone();
            IObserver deliveryStatus = new DeliveryStatus();

            Observable observable = new Observable();
            observable.AddObserver(deliveryStatus);

            DeliveryManager.Instance.ExecuteDelivery(drone, "Calle Principal 123");

            observable.NotifyObservers("Paquete entregado al cliente");
        }
    }
}
