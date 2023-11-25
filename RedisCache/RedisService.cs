using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace RedisCache
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string url)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url); ;
        }
        public IDatabase GetDb(int db = 1)
        {
            return _connectionMultiplexer.GetDatabase(db);
        }
    }
}
