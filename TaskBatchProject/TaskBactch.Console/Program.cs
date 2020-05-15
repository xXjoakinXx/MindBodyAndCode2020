using System;
using System.Threading.Tasks;
using TaskBatchProject;

namespace TaskBactch.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            var array = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var resultArray = new int[10] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

            TaskBatch<ResultObject> taskBatch = new TaskBatch<ResultObject>(5);

            for (int index = 0; index < array.Length; index++)
                await taskBatch.Add(NextSumValue(index, 10));

            var result = await taskBatch.GetResults();

            System.Console.WriteLine("ENDED");
            System.Console.ReadLine();
        }

        private static Task<ResultObject> NextSumValue(int first, int second)
        {
            return Task.Run(async () =>
            {
                var rnd = new Random();
                await Task.Delay(1000 * rnd.Next(0, 5));

                return new ResultObject(first + second);
            });
        }

        private class ResultObject
        {
            public ResultObject(int value)
            {
                Value = value;
            }

            public int Value { get; set; }
        }
    }
}
