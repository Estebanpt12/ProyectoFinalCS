using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Model
{
    public class ExecutionResult
    {
        private readonly string _name;
        private readonly Dictionary<string, long> _times;

        public ExecutionResult(string name, Dictionary<string, long> times)
        {
            _name = name;
            _times = times;
        }

        public void AddTime(string key, long value)
        {
            _times.Add(key, value);
        }

        public long GetTime(string key)
        {
            return _times.GetValueOrDefault(key, 0);
        }

        public string GetName()
        {
            return _name;
        }
    }
}