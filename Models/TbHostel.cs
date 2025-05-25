using System;
using System.Collections.Generic;

namespace TimTro.Models;

public partial class TbHostel
{
    public int Idhostel { get; set; }

    public string? Title { get; set; }

    public int? Price { get; set; }

    public int? Iduser { get; set; }

    public string? Description { get; set; }

    public int? Area { get; set; }

    public bool IsPrivate { get; set; }

    public bool IsHasParkingLot { get; set; }

    public DateOnly? UploadDate { get; set; }

    public string? Ward { get; set; }

    public int? District { get; set; }

    public int? Province { get; set; }

    public string? AddressDetail { get; set; }

    public bool IsShow { get; set; }

    public bool IsApproval { get; set; }

    public bool IsAvailable { get; set; }

    public byte[]? Image { get; set; }

    public string? ImageType { get; set; }

    public virtual TbUser? IduserNavigation { get; set; }

    public virtual ICollection<TbLikeHostelList> TbLikeHostelLists { get; set; } = new List<TbLikeHostelList>();
}
