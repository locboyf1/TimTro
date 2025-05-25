using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbNoitice
{
    public int NoticeId { get; set; }

    public string? Title { get; set; }

    public string? Noitice { get; set; }

    public DateTime? Time { get; set; }

    public int? UserId { get; set; }

    public virtual TbUser? User { get; set; }
}
