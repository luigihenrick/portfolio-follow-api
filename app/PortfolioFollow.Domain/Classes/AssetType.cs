using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PortfolioFollow.Domain.Classes
{
    public enum AssetType
    {
        [Description("Tesouro Direto")]
        TD,
        [Description("Renda Variável")]
        RV,
        [Description("Renda Fixa")]
        RF
    }
}
