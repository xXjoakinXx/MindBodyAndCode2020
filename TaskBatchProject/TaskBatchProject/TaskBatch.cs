using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBatchProject
{
    public class TaskBatch<T>
    {
        public int MaxNumBatch { get; }

        private readonly List<Task<T>> _tasks;
        private readonly List<T> _results;
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public TaskBatch(int maxNumBatch = 20)
        {
            MaxNumBatch = maxNumBatch;
            _tasks = new List<Task<T>>();
            _results = new List<T>();
        }

        public async Task Add(Task<T> task)
        {
            if (_tasks.Count >= MaxNumBatch)
                await ProcessTasks(task);
            else
                _tasks.Add(task);
        }

        public async Task<List<T>> GetResults()
        {
            if (_tasks.Count > 0)
            {
                await ProcessTasks();
            }

            var listTasksCloned = new List<T>(_results);
            _results.Clear();

            return listTasksCloned;
        }

        private async Task ProcessTasks(Task<T> next = null)
        {
            await _semaphore.WaitAsync();

            try
            {
                var results = await Task.WhenAll(_tasks);

                _results.AddRange(results);
                _tasks.Clear();
                if (next != null)
                    _tasks.Add(next);

            }
            finally
            {
                _semaphore.Release();
            }


        }

    }
}
