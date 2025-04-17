using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbHostel
{
    public int Idhostel { get; set; }

    public string? Title { get; set; }

    public int? Price { get; set; }

    public int? Iduser { get; set; }

    public string? Discription { get; set; }

    public int? Area { get; set; }

    public bool IsPrivate { get; set; }

    public bool IsHasParkingLot { get; set; }

    public DateOnly? UploadDate { get; set; }

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public string? AddressDetail { get; set; }

    public bool IsShow { get; set; }

    public bool IsApproval { get; set; }

    public bool IsAvailable { get; set; }

    public virtual TbUser? IduserNavigation { get; set; }

    public virtual ICollection<TbImageHostel> TbImageHostels { get; set; } = new List<TbImageHostel>();

    public virtual ICollection<TbLikeHostelList> TbLikeHostelLists { get; set; } = new List<TbLikeHostelList>();
}
