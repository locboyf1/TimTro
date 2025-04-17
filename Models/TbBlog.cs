using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbBlog
{
    public int Idblog { get; set; }

    public string? Title { get; set; }

    public int? Idadmin { get; set; }

    public DateTime? Time { get; set; }

    public string? Discription { get; set; }

    public string? Detail { get; set; }

    public string? Image { get; set; }

    public bool IsShow { get; set; }

    public virtual TbAdmin? IdadminNavigation { get; set; }

    public virtual ICollection<TbBlogComment> TbBlogComments { get; set; } = new List<TbBlogComment>();
}
