namespace Praktika
{
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
}
