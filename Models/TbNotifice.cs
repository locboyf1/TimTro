using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbNotifice
{
    public int Idnotifice { get; set; }

    public int? Iduser { get; set; }

    public int? Idadmin { get; set; }

    public string? Title { get; set; }

    public string? Detail { get; set; }

    public virtual TbAdmin? IdadminNavigation { get; set; }

    public virtual TbUser? IduserNavigation { get; set; }
}
