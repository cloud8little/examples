using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;
using System;
using System.Numerics;
using Neo.SmartContract.Framework.Native;

namespace Template.NEP17.CSharp
{
    public partial class NEP17 : SmartContract
    {
        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
            if (AssetStorage.GetPaymentStatus())
            {
                if (Runtime.CallingScriptHash == NEO.Hash)
                {
                    Runtime.Log("mint neo tokens");
                    Mint(amount * TokensPerNEO);
                }
                else if (Runtime.CallingScriptHash == GAS.Hash)
                {
                    if (from != null) Mint(amount * TokensPerGAS);
                }
                else
                {
                    throw new Exception("Wrong calling script hash");
                }
            }
            else
            {
                throw new Exception("Payment is disable on this contract!");
            }
        }

        private static void Mint(BigInteger amount)
        {
            var totalSupply = TotalSupplyStorage.Get();
            if (totalSupply <= 0) throw new Exception("Contract not deployed.");

            var avaliable_supply = MaxSupply - totalSupply;

            if (amount <= 0) throw new Exception("Amount cannot be zero.");
            if (amount > avaliable_supply)
            {
                Runtime.Log(StdLib.Itoa(amount));
                Runtime.Log(StdLib.Itoa(avaliable_supply));

                throw new Exception("Insufficient supply for mint tokens.");
            }
            Transaction tx = (Transaction)Runtime.ScriptContainer;
            AssetStorage.Increase(tx.Sender, amount);
            TotalSupplyStorage.Increase(amount);

            OnTransfer(null, tx.Sender, amount);
        }
    }
}
