namespace Praktika
{

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
}


