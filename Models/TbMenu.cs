using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbMenu
{
    public int Idmenu { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public bool IsShow { get; set; }

    public int? Position { get; set; }

    public string? Alias { get; set; }
}
