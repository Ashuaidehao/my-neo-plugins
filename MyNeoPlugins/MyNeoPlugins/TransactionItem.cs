using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo;
using Neo.IO.Json;
using Neo.Network.P2P.Payloads;

namespace MyNeoPlugins
{
    public class TransactionItem
    {
        public TransactionItem(Transaction tx)
        {
            TxId = tx.Hash;
            SystemFee = tx.SystemFee;
            NetworkFee = tx.NetworkFee;
            TotalFee = tx.SystemFee + tx.NetworkFee;
        }
        public UInt256 TxId { get; set; }

        public Fixed8 SystemFee { get; set; }
        public Fixed8 NetworkFee { get; set; }

        public Fixed8 TotalFee { get; set; }


        public JObject ToJObject()
        {
            var o = new JObject();
            o["txid"] = TxId.ToString();
            o["systemFee"] = SystemFee.ToString();
            o["networkFee"] = NetworkFee.ToString();
            o["fee"] = TotalFee.ToString();
            return o;
        }
    }
}
