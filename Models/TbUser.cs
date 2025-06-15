using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbUser
{
    public int Iduser { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public DateOnly? Birth { get; set; }

    public byte[]? Avatar { get; set; }

    public string? Introduce { get; set; }

    public string? Password { get; set; }

    public string? AvatarType { get; set; }

    public int? RoleId { get; set; }

    public bool IsLock { get; set; }

    public virtual TbRole? Role { get; set; }

    public virtual ICollection<TbBlogComment> TbBlogComments { get; set; } = new List<TbBlogComment>();

    public virtual ICollection<TbBlog> TbBlogs { get; set; } = new List<TbBlog>();

    public virtual ICollection<TbComment> TbComments { get; set; } = new List<TbComment>();

    public virtual ICollection<TbHostel> TbHostels { get; set; } = new List<TbHostel>();

    public virtual ICollection<TbLikeHostelList> TbLikeHostelLists { get; set; } = new List<TbLikeHostelList>();

    public virtual ICollection<TbNoitice> TbNoitices { get; set; } = new List<TbNoitice>();

    public virtual ICollection<TbReview> TbReviews { get; set; } = new List<TbReview>();
}
