using System;
using System.Collections.Generic;

namespace MyGame
{
    public class ObjectPool<T> where T : class
    {
        private readonly Stack<T> pool;
        private readonly Func<T> factory;

        public ObjectPool(Func<T> factory, int initialCapacity = 10)
        {
            this.factory = factory;
            pool = new Stack<T>(initialCapacity);
            for (int i = 0; i < initialCapacity; i++)
            {
                pool.Push(factory());
            }
        }

        public T GetObject()
        {
            return pool.Count > 0 ? pool.Pop() : factory();
        }

        public void ReturnObject(T obj)
        {
            pool.Push(obj);
        }
    }
}