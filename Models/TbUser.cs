using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbUser
{
    public int Iduser { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Birth { get; set; }

    public string? Avatar { get; set; }

    public string? Introduce { get; set; }

    public virtual ICollection<TbBlogComment> TbBlogComments { get; set; } = new List<TbBlogComment>();

    public virtual ICollection<TbComment> TbComments { get; set; } = new List<TbComment>();

    public virtual ICollection<TbHostel> TbHostels { get; set; } = new List<TbHostel>();

    public virtual ICollection<TbNotifice> TbNotifices { get; set; } = new List<TbNotifice>();
}
