using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbBlogComment
{
    public int IdblogComment { get; set; }

    public int? Idblog { get; set; }

    public string? Comment { get; set; }

    public bool? IsChildComment { get; set; }

    public int? IdcommentParent { get; set; }

    public int? LikeCount { get; set; }

    public int? Iduser { get; set; }

    public DateTime? Time { get; set; }

    public virtual TbBlog? IdblogNavigation { get; set; }

    public virtual TbBlogComment? IdcommentParentNavigation { get; set; }

    public virtual TbUser? IduserNavigation { get; set; }

    public virtual ICollection<TbBlogComment> InverseIdcommentParentNavigation { get; set; } = new List<TbBlogComment>();
}
