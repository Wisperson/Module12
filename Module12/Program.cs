using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RacingGame game = new RacingGame();

            SportsCar sportsCar = new SportsCar("Спортивный автомобиль", 10);
            PassengerCar passengerCar = new PassengerCar("Легковой автомобиль", 8);
            Truck truck = new Truck("Грузовик", 5);
            Bus bus = new Bus("Автобус", 4);

            game.StartRace += sportsCar.Start;
            game.StartRace += passengerCar.Start;
            game.StartRace += truck.Start;
            game.StartRace += bus.Start;

            game.MoveCars += sportsCar.Move;
            game.MoveCars += passengerCar.Move;
            game.MoveCars += truck.Move;
            game.MoveCars += bus.Move;

            sportsCar.Finish += game.FinishRace;
            passengerCar.Finish += game.FinishRace;
            truck.Finish += game.FinishRace;
            bus.Finish += game.FinishRace;

            game.RunRace();
        }
    }

    // Абстрактный класс "автомобиль"
    public abstract class Car
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int Distance { get; set; }

        public delegate void RaceEventHandler(string message);
        public event RaceEventHandler Finish;

        public Car(string name, int speed)
        {
            Name = name;
            Speed = speed;
            Distance = 0;
        }

        public void Start()
        {
            Console.WriteLine($"{Name} выходит на старт.");
        }

        public void Move()
        {
            Distance += Speed;
            Console.WriteLine($"{Name} двигается на расстояние {Distance}.");
            if (Distance >= 100)
            {
                FinishRace();
            }
        }

        private void FinishRace()
        {
            Finish?.Invoke($"{Name} финишировал!");
        }
    }

    // Класс спортивного автомобиля
    public class SportsCar : Car
    {
        public SportsCar(string name, int speed) : base(name, speed) { }
    }

    // Класс легкового автомобиля
    public class PassengerCar : Car
    {
        public PassengerCar(string name, int speed) : base(name, speed) { }
    }

    // Класс грузового автомобиля
    public class Truck : Car
    {
        public Truck(string name, int speed) : base(name, speed) { }
    }

    // Класс автобуса
    public class Bus : Car
    {
        public Bus(string name, int speed) : base(name, speed) { }
    }

    public class RacingGame
    {
        public delegate void StartRaceDelegate();
        public event StartRaceDelegate StartRace;

        public delegate void MoveCarsDelegate();
        public event MoveCarsDelegate MoveCars;

        public void RunRace()
        {
            StartRace?.Invoke();
            while (true)
            {
                MoveCars?.Invoke();
                System.Threading.Thread.Sleep(500); // Добавим небольшую паузу для наглядности
            }
        }

        public void FinishRace(string winner)
        {
            Console.WriteLine($"Гонка завершена! Победитель: {winner}");
            Environment.Exit(0);
        }
    }
}
