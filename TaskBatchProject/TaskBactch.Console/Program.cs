using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskBatchProject;

namespace TaskBactch.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Start TaskBatch Test!");
            System.Console.WriteLine("Input Example:");

            var array = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (var i in array)
                System.Console.Write($"{i}:");

            System.Console.WriteLine("");

            TaskBatch<ResultObject> taskBatch = new TaskBatch<ResultObject>(4);

            for (int index = 0; index < array.Length; index++)
                await taskBatch.Add(NextSumValue(index, 10));

            var result = await taskBatch.GetResults();

            System.Console.WriteLine("End TaskBatch process");
            System.Console.WriteLine("Example Result:");
            foreach (var r in result)
                System.Console.Write($"{r.Value}:");

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
