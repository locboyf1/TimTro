using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbComment
{
    public int Idcomment { get; set; }

    public string? Name { get; set; }

    public string? Comment { get; set; }

    public bool IsChildComment { get; set; }

    public int? IdcommentParent { get; set; }

    public int? LikeCount { get; set; }

    public int? Iduser { get; set; }

    public virtual TbUser? IduserNavigation { get; set; }
}
