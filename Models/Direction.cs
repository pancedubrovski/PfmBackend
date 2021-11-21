using System.ComponentModel;

namespace PmfBackend.Models {
    public enum Direction 
    {
        [Description("Credit")]
        c,
        [Description("Debit")]
        d
    }
}