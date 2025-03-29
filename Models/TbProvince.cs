using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbProvince
{
    public string Province { get; set; } = null!;

    public virtual ICollection<TbDistrict> TbDistricts { get; set; } = new List<TbDistrict>();
}
