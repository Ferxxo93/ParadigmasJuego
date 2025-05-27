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
            if (pool.Count > 0)
            {
                T obj = pool.Pop();
                Console.WriteLine($"[Pool] Se extrajo un objeto del pool. Objetos restantes: {pool.Count}");
                return obj;
            }
            else
            {
                T newObj = factory();
                Console.WriteLine("[Pool] Pool vacío. Se creó un nuevo objeto.");
                return newObj;
            }
        }

        public void ReturnObject(T obj)
        {
            pool.Push(obj);
            Console.WriteLine($"[Pool] Objeto retornado al pool. Total en pool: {pool.Count}");
        }
    }
}