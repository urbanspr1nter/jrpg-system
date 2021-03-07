using System;
using System.Collections.Generic;

namespace Jrpg.System.Interfaces
{
    public interface IContext<T>
    {
        void Add(string key, T value);
        void Remove(string key);
        T Find(string key);
        List<T> List();
    }
}
