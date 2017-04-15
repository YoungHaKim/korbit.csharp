﻿using System;
using System.Globalization;
using Korbit.LIB.Queue;
using Korbit.LIB.Types;
using Newtonsoft.Json;

namespace Korbit.API.Public
{
    /// <summary>
    /// 체결 내역 ( List of Filled Orders )
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Timestamp of last filled order.
        /// </summary>
        public long timestamp;

        /// <summary>
        /// Unique ID that identifies the transaction.
        /// </summary>
        public long tid;

        /// <summary>
        /// Transaction price in KRW.
        /// </summary>
        public decimal price;

        /// <summary>
        /// Transaction amount in BTC.
        /// </summary>
        public decimal amount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="tid"></param>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        [JsonConstructor]
        public Transaction(string timestamp, string tid, string price, string amount)
        {
            this.timestamp = Convert.ToInt64(timestamp);
            this.tid = Convert.ToInt64(tid);
            this.price = decimal.Parse(price, NumberStyles.Float);
            this.amount = decimal.Parse(amount, NumberStyles.Float);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TradeHistory ToQueue(CoinType coinType = CoinType.btc, CurrencyType currencyType = CurrencyType.krw)
        {
            return new TradeHistory
            {
                amount = this.amount,
                coin = coinType,
                currency = currencyType,
                dealer = DealerType.korbit,
                price = this.price,
                tid = this.tid,
                timestamp = this.timestamp,
                total = this.price * this.amount,
                type = OrderType.sell
            };
        }
    }
}