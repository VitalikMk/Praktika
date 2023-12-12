namespace Praktika
{
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
}
