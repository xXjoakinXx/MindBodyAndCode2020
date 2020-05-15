using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TaskBatchProject.Test
{
    public class TaskBatchTest
    {
        private class ResultObject
        {
            public ResultObject(int value)
            {
                Value = value;
            }

            public int Value { get; set; }
        }

        private Task<ResultObject> NextSumValue(int first, int second)
        {
            return Task.Run(async () =>
            {
                var rnd = new Random();
                await Task.Delay(1000 * rnd.Next(0, 5));

                return new ResultObject(first + second);
            });
        }

        [Fact]
        public async Task Batch_test()
        {
            var array = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var resultArray = new int[10] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

            List<Task<ResultObject>> tasks = new List<Task<ResultObject>>();

            for (int index = 0; index < array.Length; index++)
                tasks.Add(NextSumValue(index, 10));

            await Task.WhenAll(tasks);

            var result = tasks.Select(t => t.Result);

            Assert.Equal(result.Select(r => r.Value), resultArray);
        }

        [Fact]
        public async Task Batching_Tasks_Less_MaxBatch_Correct()
        {
            var array = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var resultArray = new int[10] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

            TaskBatch<ResultObject> taskBatch = new TaskBatch<ResultObject>(5);

            for (int index = 0; index < array.Length; index++)
                await taskBatch.Add(NextSumValue(index, 10));

            var result = await taskBatch.GetResults();

            Assert.Equal(result.Select(r => r.Value), resultArray);
        }

        //[Fact]
        //public async Task Batching_Tasks_Exact_MaxBatch_Correct()
        //{
        //    var array = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        //    var resultArray = new int[12] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };

        //    TaskBatching<ResultDto> taskBatching = new TaskBatching<ResultDto>(maxNumBatch: 10);

        //    for (int index = 0; index < array.Length; index++)
        //        taskBatching.Add(NexValueSinceValueAsync(index, 10));

        //    var result = await taskBatching.GetResults();

        //    Assert.Equal(result.Select(r => r.Value), resultArray);
        //}

        //[Fact]
        //public async Task Batching_Tasks_Greater_MaxBatch_Correct()
        //{
        //    var array = new int[20] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        //    var resultArray = new int[20] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };

        //    TaskBatching<ResultDto> taskBatching = new TaskBatching<ResultDto>(maxNumBatch: 5);

        //    for (int index = 0; index < array.Length; index++)
        //        taskBatching.Add(NexValueSinceValueAsync(index, 10));

        //    var result = await taskBatching.GetResults();

        //    Assert.Equal(result.Select(r => r.Value), resultArray);
        //}

        //[Fact]
        //public async Task Batching_Tasks_Greater_Parallel_MaxBatch_Correct()
        //{
        //    var array = new int[20] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        //    var resultArray = new int[20] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };

        //    TaskBatching<ResultDto> taskBatching = new TaskBatching<ResultDto>(maxNumBatch: 5);

        //    Parallel.For(0, array.Length, (index, p) =>
        //    {
        //        taskBatching.Add(NexValueSinceValueAsync(index, 10));
        //    });

        //    var result = await taskBatching.GetResults();

        //    Assert.True(result.All(e => resultArray.Contains(e.Value)));

        //}
    }
}
