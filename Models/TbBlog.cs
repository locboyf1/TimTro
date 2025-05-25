using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbBlog
{
    public int Idblog { get; set; }

    public string? Title { get; set; }

    public int? Iduser { get; set; }

    public DateTime? Time { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public byte[]? Image { get; set; }

    public bool IsShow { get; set; }

    public string? ImageType { get; set; }

    public virtual TbUser? IduserNavigation { get; set; }

    public virtual ICollection<TbBlogComment> TbBlogComments { get; set; } = new List<TbBlogComment>();
}
