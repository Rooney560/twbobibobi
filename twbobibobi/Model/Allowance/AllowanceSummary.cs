

using System;
using System.Collections.Generic;

namespace twbobibobi.Model.Allowance
{
    /// <summary>
    /// 單一原發票的折讓彙總資訊（查詢/稽核用）。
    /// </summary>
    public class AllowanceSummary
    {
        //    public string InvoiceNumber { get; init; }

        //    public decimal InvoiceSales { get; init; }
        //    public decimal InvoiceTax { get; init; }

        //    public IReadOnlyList<AllowanceRecord> Allowances { get; init; }

        //    public decimal RefundedSales => Allowances.Sum(a => a.SalesAmount);
        //    public decimal RefundedTax => Allowances.Sum(a => a.TaxAmount);

        //    public decimal RemainingSales => InvoiceSales - RefundedSales;
        //    public decimal RemainingTax => InvoiceTax - RefundedTax;
    }

    //public sealed class AllowanceRecord
    //{
    //    public string AllowanceNumber { get; init; }
    //    public int Sequence { get; init; }
    //    public decimal SalesAmount { get; init; }
    //    public decimal TaxAmount { get; init; }
    //    public DateTime CreateTime { get; init; }
    //}
}