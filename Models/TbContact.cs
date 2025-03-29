using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbContact
{
    public int Idcontact { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Message { get; set; }

    public DateTime? Time { get; set; }
}
