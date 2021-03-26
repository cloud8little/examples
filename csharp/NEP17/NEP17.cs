using Neo;
using Neo.SmartContract.Framework;
using System;
using System.ComponentModel;
using System.Numerics;

namespace Template.NEP17.CSharp
{
    [DisplayName("NEP17Contract")]
    [ManifestExtra("Author", "Neo")]
    [ManifestExtra("Email", "dev@neo.org")]
    [ManifestExtra("Description", "This is a NEP17 example")]
    [SupportedStandards("NEP17", "NEP11")]
    [ContractPermission("*")]
    public partial class NEP17 : SmartContract
    {
        #region Token Settings
        static readonly ulong MaxSupply = 100_000_000_000_000_000;
        //static readonly ulong MaxSupply = 10_000_000_000_000_000;
        static readonly ulong InitialSupply = 2_000_000_001_000_001;
        //static readonly ulong InitialSupply = 2000_000_000_000_000_000;
        //static readonly UInt160 Owner = (byte[])"NXV7ZhHiyM1aHXwpVsRZC6BwNFP2jghXAq".ToScriptHash();
        static readonly byte[] Owner = Helper.ToByteArray("NXV7ZhHiyM1aHXwpVsRZC6BwNFP2jghXAq");
        static readonly ulong TokensPerNEO = 2_000_000_000;
        //static readonly ulong TokensPerNEO = 20_000_000_000_000_000;
        static readonly ulong TokensPerGAS = 1;
        #endregion

        #region Notifications
        [DisplayName("Transfer")]
        public static event Action<UInt160, UInt160, BigInteger> OnTransfer;
        #endregion

        // When this contract address is included in the transaction signature,
        // this method will be triggered as a VerificationTrigger to verify that the signature is correct.
        // For example, this method needs to be called when withdrawing token from the contract.
        public static bool Verify() => IsOwner();

        public static string Symbol() => "NewCompiler";

        public static ulong Decimals() => 8;
    }
}
