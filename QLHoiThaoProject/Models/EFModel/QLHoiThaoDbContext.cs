namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLHoiThaoDbContext : DbContext
    {
        public QLHoiThaoDbContext()
            : base("name=QLHoiThaoDbContext")
        {
        }

        public virtual DbSet<BANGDAU> BANGDAUs { get; set; }
        public virtual DbSet<CBQUANLY> CBQUANLies { get; set; }
        public virtual DbSet<COMTD> COMTDs { get; set; }
        public virtual DbSet<CTTRANDAU> CTTRANDAUs { get; set; }
        public virtual DbSet<DOI> DOIs { get; set; }
        public virtual DbSet<GIAITHUONG> GIAITHUONGs { get; set; }
        public virtual DbSet<HINHANH> HINHANHs { get; set; }
        public virtual DbSet<HOITHAO> HOITHAOs { get; set; }
        public virtual DbSet<KINHPHIHOTRO> KINHPHIHOTROes { get; set; }
        public virtual DbSet<LIENHE> LIENHEs { get; set; }
        public virtual DbSet<LOAIMTD> LOAIMTDs { get; set; }
        public virtual DbSet<LOP> LOPs { get; set; }
        public virtual DbSet<MONTHIDAU> MONTHIDAUs { get; set; }
        public virtual DbSet<NGANH> NGANHs { get; set; }
        public virtual DbSet<QUANLYHT> QUANLYHTs { get; set; }
        public virtual DbSet<QUYDINH> QUYDINHs { get; set; }
        public virtual DbSet<SANTHIDAU> SANTHIDAUs { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<THANHVIEN> THANHVIENs { get; set; }
        public virtual DbSet<THANHVIENDOI> THANHVIENDOIs { get; set; }
        public virtual DbSet<THUOCDOI> THUOCDOIs { get; set; }
        public virtual DbSet<TRANDAU> TRANDAUs { get; set; }
        public virtual DbSet<TRONGTAI> TRONGTAIs { get; set; }
        public virtual DbSet<VONGDAU> VONGDAUs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CBQUANLY>()
                .HasMany(e => e.QUANLYHTs)
                .WithRequired(e => e.CBQUANLY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DOI>()
                .HasMany(e => e.CTTRANDAUs)
                .WithRequired(e => e.DOI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DOI>()
                .HasMany(e => e.THUOCDOIs)
                .WithRequired(e => e.DOI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOITHAO>()
                .HasMany(e => e.COMTDs)
                .WithRequired(e => e.HOITHAO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOITHAO>()
                .HasMany(e => e.DOIs)
                .WithRequired(e => e.HOITHAO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOITHAO>()
                .HasMany(e => e.KINHPHIHOTROes)
                .WithRequired(e => e.HOITHAO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOITHAO>()
                .HasMany(e => e.QUANLYHTs)
                .WithRequired(e => e.HOITHAO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOITHAO>()
                .HasMany(e => e.VONGDAUs)
                .WithRequired(e => e.HOITHAO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOAIMTD>()
                .HasMany(e => e.MONTHIDAUs)
                .WithRequired(e => e.LOAIMTD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOP>()
                .HasMany(e => e.DOIs)
                .WithRequired(e => e.LOP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOP>()
                .HasMany(e => e.SINHVIENs)
                .WithRequired(e => e.LOP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOP>()
                .HasMany(e => e.THANHVIENs)
                .WithRequired(e => e.LOP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONTHIDAU>()
                .HasMany(e => e.COMTDs)
                .WithRequired(e => e.MONTHIDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONTHIDAU>()
                .HasMany(e => e.DOIs)
                .WithRequired(e => e.MONTHIDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONTHIDAU>()
                .HasMany(e => e.GIAITHUONGs)
                .WithRequired(e => e.MONTHIDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONTHIDAU>()
                .HasMany(e => e.KINHPHIHOTROes)
                .WithRequired(e => e.MONTHIDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONTHIDAU>()
                .HasMany(e => e.QUYDINHs)
                .WithRequired(e => e.MONTHIDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONTHIDAU>()
                .HasMany(e => e.VONGDAUs)
                .WithRequired(e => e.MONTHIDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGANH>()
                .HasMany(e => e.LOPs)
                .WithRequired(e => e.NGANH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANTHIDAU>()
                .HasMany(e => e.TRANDAUs)
                .WithRequired(e => e.SANTHIDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<THANHVIENDOI>()
                .HasMany(e => e.THUOCDOIs)
                .WithRequired(e => e.THANHVIENDOI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRANDAU>()
                .Property(e => e.NGAYTHIDAU)
                .IsUnicode(false);

            modelBuilder.Entity<TRANDAU>()
                .HasMany(e => e.CTTRANDAUs)
                .WithRequired(e => e.TRANDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRANDAU>()
                .HasMany(e => e.HINHANHs)
                .WithRequired(e => e.TRANDAU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRONGTAI>()
                .HasMany(e => e.TRANDAUs)
                .WithRequired(e => e.TRONGTAI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VONGDAU>()
                .HasMany(e => e.TRANDAUs)
                .WithRequired(e => e.VONGDAU)
                .WillCascadeOnDelete(false);
        }
    }
}
