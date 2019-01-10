using System;
using CoreApplication;

namespace Datalayer
{
    internal sealed class ConcreteRepository : IRepository
    {
        public int GenerateNumber(int seed)
        {
            return 40 + seed;
        }
    }
}