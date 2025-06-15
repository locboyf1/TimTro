using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbReview
{
    public int Idhostel { get; set; }

    public int Iduser { get; set; }

    public int? Star { get; set; }

    public string? Review { get; set; }

    public virtual TbHostel IdhostelNavigation { get; set; } = null!;

    public virtual TbUser IduserNavigation { get; set; } = null!;
}
