using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbAdmin
{
    public int Idadmin { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Birth { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<TbNotifice> TbNotifices { get; set; } = new List<TbNotifice>();
}
