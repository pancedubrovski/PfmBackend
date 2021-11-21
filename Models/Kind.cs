using System.ComponentModel;


namespace PmfBackend.Models {
    public enum Kind {
        [Description("Deposit")]
        dep,
        [Description("Withdrawal")]
        wdw,
        [Description("Payment")]
        pmt,
        [Description("Fee")]
        fee,
        [Description("Interest")]
        inc,
        [Description("Reversal")]
        rev,
        [Description("Adjustment")] 
        adj,
        [Description("Loan disbursement")] 
        lnd,
        [Description("Loan repayment")] 
        lnr,
        [Description("Foreign currency exchange")] 
        fcx,
        [Description("Account openning")] 
        aop,
        [Description("Account closing")] 
        acl,
        [Description("Split payment")] 
        spl,
        [Description("Salary")] 
        sal
    }
}