namespace HomeWork_Interface
{
    interface ICalc
    {
        int Sum(int a, int b);
    }
    public interface ILogger
    {
        void Event(string message);
        void Error(string message);
    }
    public class Calc : ICalc
    {
        ILogger Logger { get; }
        public Calc(ILogger logger) { 
            Logger = logger;
        }
        public int Sum(int a, int b) {
            Logger.Event("Sum - запись в Logger");
            Console.WriteLine("Сумма ваших чисел");
            Console.WriteLine($"{a} + {b} = {a+b}");
            Logger.Error("Sum - запись в Logger завершена");
            return a + b; 
        }
    }
    public class Logger : ILogger
    {
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Event(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
    public class MyException : Exception
    {
        public MyException(string _exMessage) : base(_exMessage) { }
    }
    internal class Program
    {
        static ILogger Logger { get; set; }
        static void Main(string[] args)
        {
            int intValue;
            Logger = new Logger();
            Calc calc = new Calc(Logger);
            try
            {
                Console.WriteLine("Введите первое число");
                int number1 = CheckIntValue(out intValue);
                Console.WriteLine("Введите второе число");
                int number2 = CheckIntValue(out intValue);
                calc.Sum(number1, number2);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


            Console.ReadKey();
        }
        static int CheckIntValue(out int CorrectValue)
        {
            var value = Console.ReadLine();
            int numValue;
            bool isNumber = int.TryParse(value, out numValue);
            while (!isNumber)
            {
                Console.WriteLine("Значение не число, попробуйте ввести еще раз");
                value = Console.ReadLine();
                isNumber = int.TryParse(value, out numValue);
            }
            return CorrectValue = Convert.ToInt32(value);
        }
    }
}