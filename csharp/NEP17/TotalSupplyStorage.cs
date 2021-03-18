using Neo.SmartContract.Framework.Services.Neo;
using System.Numerics;

namespace Neo.SmartContract.Examples
{
    public static class TotalSupplyStorage
    {
        public static readonly string mapName = "contract";

        public static readonly string key = "totalSupply";

        public static void Increase(BigInteger value) => Put(Get() + value);

        public static void Reduce(BigInteger value) => Put(Get() - value);

        public static void Put(BigInteger value) => Storage.CurrentContext.CreateMap(mapName).Put(key, value);

        public static BigInteger Get()
        {
            var value = Storage.CurrentContext.CreateMap(mapName).Get(key);
            return value is null ? 0 : (BigInteger)value;
        }
    }
}
