using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbImageHostel
{
    public int? Idhostel { get; set; }

    public string? Path { get; set; }

    public virtual TbHostel? IdhostelNavigation { get; set; }
}
