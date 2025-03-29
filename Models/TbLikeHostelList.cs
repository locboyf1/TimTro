using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbLikeHostelList
{
    public int IdlikeHostelList { get; set; }

    public int? Idhostel { get; set; }

    public int? Iduser { get; set; }

    public virtual TbHostel? IdhostelNavigation { get; set; }

    public virtual TbUser? IduserNavigation { get; set; }
}
