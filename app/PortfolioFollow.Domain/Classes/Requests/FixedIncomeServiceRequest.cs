﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class FixedIncomeServiceRequest : RequestBase
    {
        public FixedIncomeServiceRequest() : base(AssetType.RF)
        { }

        public decimal CDIPercentage { get; set; }
        public decimal AppliedAmount { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
