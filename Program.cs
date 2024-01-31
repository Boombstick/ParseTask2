namespace ParsingTask2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 100, i =>
            {
                if (i % 2 == 0)
                    Server.AddToCount(2);
                else
                    Console.WriteLine(Server.GetCount());
            });
            Console.WriteLine(Server.GetCount());
            Console.WriteLine("Done.");
            Console.ReadKey();
            Console.WriteLine("Hello, World!");
        }
    }
    public static class Server
    {
        static ReaderWriterLockSlim  locker = new ReaderWriterLockSlim();
        private static int count;
        public static int GetCount()
        {
            locker.EnterReadLock();
            try
            {
                return count;
            }
            finally { locker.ExitReadLock(); }
        }
        public static void AddToCount(int value)
        {
            locker.EnterWriteLock();
            Interlocked.Add(ref count,value);
            locker.ExitWriteLock();
        }
    }
}
