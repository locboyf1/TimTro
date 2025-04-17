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

    public virtual DbSet<TbAdmin> TbAdmins { get; set; }

    public virtual DbSet<TbBlog> TbBlogs { get; set; }

    public virtual DbSet<TbBlogComment> TbBlogComments { get; set; }

    public virtual DbSet<TbComment> TbComments { get; set; }

    public virtual DbSet<TbContact> TbContacts { get; set; }

    public virtual DbSet<TbHostel> TbHostels { get; set; }

    public virtual DbSet<TbImageHostel> TbImageHostels { get; set; }

    public virtual DbSet<TbLikeHostelList> TbLikeHostelLists { get; set; }

    public virtual DbSet<TbMenu> TbMenus { get; set; }

    public virtual DbSet<TbNotifice> TbNotifices { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("data source= QUANGLOCPC\\QUANGLOC; initial catalog=Hostel; integrated security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAdmin>(entity =>
        {
            entity.HasKey(e => e.Idadmin);

            entity.ToTable("tb_Admin");

            entity.Property(e => e.Idadmin).HasColumnName("IDAdmin");
            entity.Property(e => e.Birth)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbBlog>(entity =>
        {
            entity.HasKey(e => e.Idblog);

            entity.ToTable("tb_Blog");

            entity.Property(e => e.Idblog).HasColumnName("IDBlog");
            entity.Property(e => e.Discription).HasMaxLength(200);
            entity.Property(e => e.Idadmin).HasColumnName("IDAdmin");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Time).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.IdadminNavigation).WithMany(p => p.TbBlogs)
                .HasForeignKey(d => d.Idadmin)
                .HasConstraintName("FK_tb_Blog_tb_Admin");
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
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Time).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbHostel>(entity =>
        {
            entity.HasKey(e => e.Idhostel);

            entity.ToTable("tb_Hostel");

            entity.Property(e => e.Idhostel).HasColumnName("IDHostel");
            entity.Property(e => e.District).HasMaxLength(80);
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Province).HasMaxLength(80);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.Ward).HasMaxLength(80);

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbHostels)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tb_Hostel_tb_User");
        });

        modelBuilder.Entity<TbImageHostel>(entity =>
        {
            entity.HasKey(e => e.Idimage);

            entity.ToTable("tb_ImageHostel");

            entity.Property(e => e.Idimage).HasColumnName("IDImage");
            entity.Property(e => e.Idhostel).HasColumnName("IDHostel");

            entity.HasOne(d => d.IdhostelNavigation).WithMany(p => p.TbImageHostels)
                .HasForeignKey(d => d.Idhostel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_ImageHostel_tb_Hostel");
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

        modelBuilder.Entity<TbNotifice>(entity =>
        {
            entity.HasKey(e => e.Idnotifice);

            entity.ToTable("tb_Notifice");

            entity.Property(e => e.Idnotifice).HasColumnName("IDNotifice");
            entity.Property(e => e.Idadmin).HasColumnName("IDAdmin");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.IdadminNavigation).WithMany(p => p.TbNotifices)
                .HasForeignKey(d => d.Idadmin)
                .HasConstraintName("FK_tb_Notifice_tb_Admin");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TbNotifices)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tb_Notifice_tb_User");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.Iduser);

            entity.ToTable("tb_User");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Birth)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
