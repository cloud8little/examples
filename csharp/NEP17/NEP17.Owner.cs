using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;
using Neo.SmartContract.Framework.Native;
using System;
using Neo;

namespace Template.NEP17.CSharp
{
    public partial class NEP17 : SmartContract
    {
        public static void _deploy(object data,bool update)
        {
            if (update) return;

            if (TotalSupplyStorage.Get() > 0) throw new Exception("Contract has been deployed.");
            Runtime.Log("after get totalSupplyStorage");
            
            TotalSupplyStorage.Increase(InitialSupply);

            Runtime.Log("after increase totalSupplystorage");

            AssetStorage.Increase((UInt160)Owner, InitialSupply);

            Runtime.Log("after increase assetstorage");

            OnTransfer(null, (UInt160)Owner, InitialSupply);
        }

        public static void Update(string nefFile, string manifest)
        {
            if (!IsOwner()) throw new Exception("No authorization.");
            ContractManagement.Update(nefFile, manifest, null);
        }

        public static void Destroy()
        {
            if (!IsOwner()) throw new Exception("No authorization.");
            ContractManagement.Destroy();
        }

        public static void EnablePayment()
        {
            if (!IsOwner()) throw new Exception("No authorization.");
            AssetStorage.Enable();
        }

        public static void DisablePayment()
        {
            if (!IsOwner()) throw new Exception("No authorization.");
            AssetStorage.Disable();
        }

        private static bool IsOwner() => Runtime.CheckWitness((UInt160)Owner);


        public static void Testdynamiccall(Neo.UInt160 hash, string method)
        {
            Contract.Call(hash, method, CallFlags.All, new object[] { });
        }
    }
}
