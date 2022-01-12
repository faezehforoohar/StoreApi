using System;
using System.ComponentModel;

namespace StoreApi.Helpers
{
    public class Enums
    {
    public enum SortType
        {
            [Description("asc")]
            asc = 1,
            [Description("dsc")]
            dsc=2
        }
    }
}
