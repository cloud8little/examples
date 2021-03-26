using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;
using System.Numerics;

namespace Template.NEP17.CSharp
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
            ByteString totaSupply = Storage.CurrentContext.CreateMap(mapName).Get(key);
            if (totaSupply == null) return 0;
            //return BigInteger.Parse(totaSupply);
            return (BigInteger)totaSupply;
        }

    }
}
