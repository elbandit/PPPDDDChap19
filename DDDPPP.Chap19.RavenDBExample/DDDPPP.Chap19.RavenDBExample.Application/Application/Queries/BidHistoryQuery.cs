﻿using System;
using System.Collections.Generic;
using Raven.Client;
using DDDPPP.Chap19.RavenDBExample.Application.Model.BidHistory;

namespace DDDPPP.Chap19.RavenDBExample.Application.Application.Queries
{
    public class BidHistoryQuery
    {
        private readonly IBidHistory _bidHistory;

        public BidHistoryQuery(IBidHistory bidHistory)
        {
            _bidHistory = bidHistory;         
        }

        public IEnumerable<BidInformation> BidHistoryFor(Guid auctionId)
        {
            var bidHistory = _bidHistory.FindBy(auctionId);

            return Convert(bidHistory.ShowAllBids());
        }

        public IEnumerable<BidInformation> Convert(IEnumerable<BidEvent> bids)
        {
            var bidInfo = new List<BidInformation>();

            foreach (var bid in bids)
            {
                bidInfo.Add(new BidInformation() { Bidder = bid.Bidder, AmountBid = bid.AmountBid.Value, TimeOfBid = bid.TimeOfBid });
            }

            return bidInfo;
        }
    }
}
