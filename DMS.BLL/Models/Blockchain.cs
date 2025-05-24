using System;
using System.Collections.Generic;

namespace DMS.BLL.Models;

public partial class Blockchain
{
    public int BlockIndex { get; set; }

    public string BlockHash { get; set; } = null!;

    public string PreviousHash { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string DataHash { get; set; } = null!;

    public int Nonce { get; set; }
}
