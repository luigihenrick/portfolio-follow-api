using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Service.ExternalServices.FixedIncome
{
    public class FixedIncomePrice
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public decimal Percentual { get; set; }
        public decimal Fator { get; set; }
        public decimal Taxa { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorCalculado { get; set; }
    }
}
