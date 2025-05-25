using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Descrtiption { get; set; }

    public int? RoleLevel { get; set; }

    public virtual ICollection<TbUser> TbUsers { get; set; } = new List<TbUser>();
}
