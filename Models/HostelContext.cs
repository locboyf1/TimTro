using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimTro.Models;

public partial class HostelContext : DbContext
{
    public HostelContext()
    {
    }

    public HostelContext(DbContextOptions<HostelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBlog> TbBlogs { get; set; }

    public virtual DbSet<TbBlogComment> TbBlogComments { get; set; }

    public virtual DbSet<TbComment> TbComments { get; set; }

    public virtual DbSet<TbContact> TbContacts { get; set; }

    public virtual DbSet<TbHostel> TbHostels { get; set; }

    public virtual DbSet<TbLikeHostelList> TbLikeHostelLists { get; set; }

    public virtual DbSet<TbMenu> TbMenus { get; set; }

    public virtual DbSet<TbNoitice> TbNoitices { get; set; }

    public virtual DbSet<TbReview> TbReviews { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("data source=QUANGLOCPC\\QUANGLOC; initial catalog=Hostel; integrated security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBlog>(entity =>
        {
            entity.HasKey(e => e.Idblog);

            entity.ToTable("tb_Blog");

            entity.Property(e => e.Idblog).HasColumnName("IDBlog");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.ImageType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Time).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbBlogs)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tb_Blog_tb_User");
        });

        modelBuilder.Entity<TbBlogComment>(entity =>
        {
            entity.HasKey(e => e.IdblogComment);

            entity.ToTable("tb_BlogComment");

            entity.Property(e => e.IdblogComment).HasColumnName("IDBlogComment");
            entity.Property(e => e.Idblog).HasColumnName("IDBlog");
            entity.Property(e => e.IdcommentParent).HasColumnName("IDCommentParent");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.IdblogNavigation).WithMany(p => p.TbBlogComments)
                .HasForeignKey(d => d.Idblog)
                .HasConstraintName("FK_tb_BlogComment_tb_Blog");

            entity.HasOne(d => d.IdcommentParentNavigation).WithMany(p => p.InverseIdcommentParentNavigation)
                .HasForeignKey(d => d.IdcommentParent)
                .HasConstraintName("FK_tb_BlogComment_tb_BlogComment");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbBlogComments)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tb_BlogComment_tb_User");
        });

        modelBuilder.Entity<TbComment>(entity =>
        {
            entity.HasKey(e => e.Idcomment);

            entity.ToTable("tb_Comment");

            entity.Property(e => e.Idcomment)
                .ValueGeneratedNever()
                .HasColumnName("IDComment");
            entity.Property(e => e.IdcommentParent).HasColumnName("IDCommentParent");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbComments)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tb_Comment_tb_User");
        });

        modelBuilder.Entity<TbContact>(entity =>
        {
            entity.HasKey(e => e.Idcontact);

            entity.ToTable("tb_Contact");

            entity.Property(e => e.Idcontact).HasColumnName("IDContact");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Time).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbHostel>(entity =>
        {
            entity.HasKey(e => e.Idhostel);

            entity.ToTable("tb_Hostel");

            entity.Property(e => e.Idhostel).HasColumnName("IDHostel");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.ImageType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.Ward).HasMaxLength(100);

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbHostels)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tb_Hostel_tb_User");
        });

        modelBuilder.Entity<TbLikeHostelList>(entity =>
        {
            entity.HasKey(e => e.IdlikeHostelList);

            entity.ToTable("tb_LikeHostelList");

            entity.Property(e => e.IdlikeHostelList).HasColumnName("IDLikeHostelList");
            entity.Property(e => e.Idhostel).HasColumnName("IDHostel");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IdhostelNavigation).WithMany(p => p.TbLikeHostelLists)
                .HasForeignKey(d => d.Idhostel)
                .HasConstraintName("FK_tb_LikeHostelList_tb_Hostel");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbLikeHostelLists)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tb_LikeHostelList_tb_User");
        });

        modelBuilder.Entity<TbMenu>(entity =>
        {
            entity.HasKey(e => e.Idmenu);

            entity.ToTable("tb_Menu");

            entity.Property(e => e.Idmenu).HasColumnName("IDMenu");
            entity.Property(e => e.Alias)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(20);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<TbNoitice>(entity =>
        {
            entity.HasKey(e => e.NoticeId);

            entity.ToTable("tb_Noitice");

            entity.Property(e => e.NoticeId).HasColumnName("NoticeID");
            entity.Property(e => e.Noitice).HasMaxLength(100);
            entity.Property(e => e.Time).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TbNoitices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tb_Noitice_tb_User");
        });

        modelBuilder.Entity<TbReview>(entity =>
        {
            entity.HasKey(e => new { e.Idhostel, e.Iduser });

            entity.ToTable("TB_Review");

            entity.Property(e => e.Idhostel).HasColumnName("IDHostel");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IdhostelNavigation).WithMany(p => p.TbReviews)
                .HasForeignKey(d => d.Idhostel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_Review_tb_Hostel");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbReviews)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_Review_tb_User");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_tb_Role_1");

            entity.ToTable("tb_Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Descrtiption).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.Iduser);

            entity.ToTable("tb_User");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.AvatarType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_tb_User_tb_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
