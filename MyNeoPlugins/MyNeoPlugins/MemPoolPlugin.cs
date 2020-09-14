using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Neo.IO.Json;
using Neo.Ledger;
using Neo.Network.P2P.Payloads;
using Neo.Plugins;

namespace MyNeoPlugins
{
    public class MemPoolPlugin : Plugin, IRpcPlugin

    {
        public override void Configure()
        {
            //throw new NotImplementedException();
        }

        public void PreProcess(HttpContext context, string method, JArray _params)
        {
            //throw new NotImplementedException();
        }

        public JObject OnProcess(HttpContext context, string method, JArray _params)
        {
            if (method == "getMemPoolTransactions")
            {
                bool shouldGetUnverified = _params.Count >= 1 && _params[0].AsBoolean();
                if (!shouldGetUnverified)
                    return new JArray(Blockchain.Singleton.MemPool.GetVerifiedTransactions().Select(tx => new TransactionItem(tx)).OrderByDescending(t => t.TotalFee).Select(p =>
                        p.ToJObject()));

                JObject json = new JObject();
                json["height"] = Blockchain.Singleton.Height;
                Blockchain.Singleton.MemPool.GetVerifiedAndUnverifiedTransactions(
                    out IEnumerable<Transaction> verifiedTransactions,
                    out IEnumerable<Transaction> unverifiedTransactions);
                json["verified"] = new JArray(verifiedTransactions.Select(tx => new TransactionItem(tx)).OrderByDescending(t => t.TotalFee).Select(p =>
                    p.ToJObject()));
                json["unverified"] = new JArray(unverifiedTransactions.Select(tx => new TransactionItem(tx)).OrderByDescending(t => t.TotalFee).Select(p =>
                    p.ToJObject()));
                return json;
            }

            return null;
        }

        public void PostProcess(HttpContext context, string method, JArray _params, JObject result)
        {
            //throw new NotImplementedException();
        }
    }
}
