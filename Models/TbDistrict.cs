using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbDistrict
{
    public string District { get; set; } = null!;

    public string? Province { get; set; }

    public virtual TbProvince? ProvinceNavigation { get; set; }

    public virtual ICollection<TbHostel> TbHostels { get; set; } = new List<TbHostel>();
}
