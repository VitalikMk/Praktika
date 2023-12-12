using System;
using System.Threading;

// Клас, що представляє маршрутку
class Minibus
{
    private object locker = new object(); // Об'єкт блокування для потокобезпечності
    private int availableSeats; // Кількість вільних місць
    private int standingPassengers; // Кількість пасажирів, що стоять

    public Minibus(int totalSeats, int standingSeats)
    {
        availableSeats = totalSeats;
        standingPassengers = standingSeats;
    }

    // Метод для сідання пасажирів
    public void Embark(int sittingPassengers)
    {
        lock (locker)
        {
            int sittingToSit = Math.Min(sittingPassengers, availableSeats);
            int standingToSit = Math.Min(sittingPassengers - sittingToSit, standingPassengers);

            availableSeats -= sittingToSit;
            standingPassengers -= standingToSit;

            Console.WriteLine($"Сiдає {sittingToSit} пасажирiв на сидячi мiсця та {standingToSit} на стоячi мiсця.");
            Console.WriteLine($"Вiльних сидячих мiсць: {availableSeats}, вiльних стоячих мiсць: {standingPassengers}");
        }
    }

    // Метод для висадки пасажирів
    public void Disembark(int sittingPassengers)
    {
        lock (locker)
        {
            int sittingToStand = Math.Min(sittingPassengers, availableSeats);

            availableSeats += sittingToStand;

            Console.WriteLine($"Висаджуються {sittingToStand} пасажирiв з сидячих мiсць.");
            Console.WriteLine($"Вiльних сидячих мiсць: {availableSeats}, вiльних стоячих мiсць: {standingPassengers}");
        }
    }
}

// Клас для представлення зупинки
class BusStop
{
    private Minibus minibus;
    private int standingCapacity;
    private int sittingCapacity;

    public BusStop(Minibus minibus, int standingCapacity, int sittingCapacity)
    {
        this.minibus = minibus;
        this.standingCapacity = standingCapacity;
        this.sittingCapacity = sittingCapacity;
    }

    // Метод для обслуговування зупинки
    public void ServeStop()
    {
        Random random = new Random();
        int standingPassengers = random.Next(1, standingCapacity + 1);
        int sittingPassengers = random.Next(1, sittingCapacity + 1);

        Console.WriteLine($"Зупинка. Стоячих пасажирiв: {standingPassengers}, сидячих пасажирiв: {sittingPassengers}");

        minibus.Embark(sittingPassengers);
        minibus.Disembark(sittingPassengers);
    }
}

class Program
{
    static void Main()
    {
        Minibus minibus = new Minibus(24, 12);
        BusStop[] busStops = new BusStop[10];

        for (int i = 0; i < 10; i++)
        {
            busStops[i] = new BusStop(minibus, 5, 10);
        }

        // Запускаємо кожну зупинку в окремому потоці
        Thread[] threads = new Thread[10];

        for (int i = 0; i < 10; i++)
        {
            threads[i] = new Thread(busStops[i].ServeStop);
            threads[i].Start();
        }

        // Очікуємо завершення всіх потоків
        for (int i = 0; i < 10; i++)
        {
            threads[i].Join();
        }
    }
}
