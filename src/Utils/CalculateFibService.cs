namespace Utils
{
    using Interface;

    /// <inheritdoc cref="ICalculateFibService"/>
    public class CalculateFibService : ICalculateFibService
    {
        public long CalcNext(long currentValue)
        {
            long prev = 0;
            long next = 1;

            while (true)
            {
                if (currentValue == 0)
                {
                    return 1;
                }
                if (currentValue == 1)
                {
                    return 2; 
                }
                var sum = prev + next;
                if (sum < 0)
                {
                    return 0;
                }
                if (next == currentValue)
                {
                    return sum;
                }
                prev = next;
                next = sum;
            }
        }
    }
}