namespace QLHoiThaoProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BANGDAU",
                c => new
                    {
                        IDBANG = c.Int(nullable: false, identity: true),
                        TENBANG = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDBANG);
            
            CreateTable(
                "dbo.DOI",
                c => new
                    {
                        IDDOI = c.Int(nullable: false, identity: true),
                        IDHT = c.Int(nullable: false),
                        IDMTD = c.Int(nullable: false),
                        IDBANG = c.Int(),
                        IDLOP = c.Int(nullable: false),
                        BILOAI = c.Boolean(nullable: false),
                        TENDOI = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDDOI)
                .ForeignKey("dbo.BANGDAU", t => t.IDBANG)
                .ForeignKey("dbo.MONTHIDAU", t => t.IDMTD)
                .ForeignKey("dbo.HOITHAO", t => t.IDHT)
                .ForeignKey("dbo.LOP", t => t.IDLOP)
                .Index(t => t.IDHT)
                .Index(t => t.IDMTD)
                .Index(t => t.IDBANG)
                .Index(t => t.IDLOP);
            
            CreateTable(
                "dbo.CTTRANDAU",
                c => new
                    {
                        IDDOI = c.Int(nullable: false),
                        IDTD = c.Int(nullable: false),
                        KETQUA = c.String(maxLength: 255),
                        TISO = c.String(maxLength: 255),
                        DIEM = c.Int(),
                    })
                .PrimaryKey(t => new { t.IDDOI, t.IDTD })
                .ForeignKey("dbo.TRANDAU", t => t.IDTD)
                .ForeignKey("dbo.DOI", t => t.IDDOI)
                .Index(t => t.IDDOI)
                .Index(t => t.IDTD);
            
            CreateTable(
                "dbo.TRANDAU",
                c => new
                    {
                        IDTD = c.Int(nullable: false, identity: true),
                        IDVONGDAU = c.Int(nullable: false),
                        IDTRONGTAI = c.Int(nullable: false),
                        IDSTD = c.Int(nullable: false),
                        THOIGIANBD = c.String(maxLength: 255),
                        THOIGIANKT = c.String(maxLength: 255),
                        NGAYTHIDAU = c.String(maxLength: 20, unicode: false),
                        TRANGTHAI = c.Boolean(),
                    })
                .PrimaryKey(t => t.IDTD)
                .ForeignKey("dbo.SANTHIDAU", t => t.IDSTD)
                .ForeignKey("dbo.TRONGTAI", t => t.IDTRONGTAI)
                .ForeignKey("dbo.VONGDAU", t => t.IDVONGDAU)
                .Index(t => t.IDVONGDAU)
                .Index(t => t.IDTRONGTAI)
                .Index(t => t.IDSTD);
            
            CreateTable(
                "dbo.HINHANH",
                c => new
                    {
                        IDHA = c.Int(nullable: false, identity: true),
                        IDTD = c.Int(nullable: false),
                        TENHA = c.String(maxLength: 255),
                        LINK = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDHA)
                .ForeignKey("dbo.TRANDAU", t => t.IDTD)
                .Index(t => t.IDTD);
            
            CreateTable(
                "dbo.SANTHIDAU",
                c => new
                    {
                        IDSTD = c.Int(nullable: false, identity: true),
                        TENSTD = c.String(maxLength: 255),
                        MOTA = c.String(),
                    })
                .PrimaryKey(t => t.IDSTD);
            
            CreateTable(
                "dbo.TRONGTAI",
                c => new
                    {
                        IDTRONGTAI = c.Int(nullable: false, identity: true),
                        TENTRONGTAI = c.String(maxLength: 255),
                        EMAIL = c.String(maxLength: 255),
                        SDT = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDTRONGTAI);
            
            CreateTable(
                "dbo.VONGDAU",
                c => new
                    {
                        IDVONGDAU = c.Int(nullable: false, identity: true),
                        IDMTD = c.Int(nullable: false),
                        IDHT = c.Int(nullable: false),
                        TENVONGDAU = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDVONGDAU)
                .ForeignKey("dbo.MONTHIDAU", t => t.IDMTD)
                .ForeignKey("dbo.HOITHAO", t => t.IDHT)
                .Index(t => t.IDMTD)
                .Index(t => t.IDHT);
            
            CreateTable(
                "dbo.HOITHAO",
                c => new
                    {
                        IDHT = c.Int(nullable: false, identity: true),
                        TENHT = c.String(maxLength: 255),
                        TUNGAY = c.DateTime(),
                        DENNGAY = c.DateTime(),
                        NGAYBDDK = c.DateTime(),
                        NGAYKTDK = c.DateTime(),
                    })
                .PrimaryKey(t => t.IDHT);
            
            CreateTable(
                "dbo.COMTD",
                c => new
                    {
                        IDMTD = c.Int(nullable: false),
                        IDHT = c.Int(nullable: false),
                        GHICHU = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.IDMTD, t.IDHT })
                .ForeignKey("dbo.MONTHIDAU", t => t.IDMTD)
                .ForeignKey("dbo.HOITHAO", t => t.IDHT)
                .Index(t => t.IDMTD)
                .Index(t => t.IDHT);
            
            CreateTable(
                "dbo.MONTHIDAU",
                c => new
                    {
                        IDMTD = c.Int(nullable: false, identity: true),
                        IDLOAIMTD = c.Int(nullable: false),
                        TENMTD = c.String(maxLength: 255),
                        MOTA = c.String(),
                        HINHANH = c.String(),
                    })
                .PrimaryKey(t => t.IDMTD)
                .ForeignKey("dbo.LOAIMTD", t => t.IDLOAIMTD)
                .Index(t => t.IDLOAIMTD);
            
            CreateTable(
                "dbo.GIAITHUONG",
                c => new
                    {
                        IDGT = c.Int(nullable: false, identity: true),
                        IDMTD = c.Int(nullable: false),
                        TENGT = c.String(maxLength: 255),
                        GIATRIGT = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDGT)
                .ForeignKey("dbo.MONTHIDAU", t => t.IDMTD)
                .Index(t => t.IDMTD);
            
            CreateTable(
                "dbo.KINHPHIHOTRO",
                c => new
                    {
                        IDKINHPHI = c.Int(nullable: false, identity: true),
                        IDHT = c.Int(nullable: false),
                        IDMTD = c.Int(nullable: false),
                        GIATRI = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDKINHPHI)
                .ForeignKey("dbo.MONTHIDAU", t => t.IDMTD)
                .ForeignKey("dbo.HOITHAO", t => t.IDHT)
                .Index(t => t.IDHT)
                .Index(t => t.IDMTD);
            
            CreateTable(
                "dbo.LOAIMTD",
                c => new
                    {
                        IDLOAIMTD = c.Int(nullable: false, identity: true),
                        TENLOAIMTD = c.String(maxLength: 255),
                        MOTA = c.String(),
                    })
                .PrimaryKey(t => t.IDLOAIMTD);
            
            CreateTable(
                "dbo.QUYDINH",
                c => new
                    {
                        IDQUYDINH = c.Int(nullable: false, identity: true),
                        IDMTD = c.Int(nullable: false),
                        MOTA = c.String(),
                    })
                .PrimaryKey(t => t.IDQUYDINH)
                .ForeignKey("dbo.MONTHIDAU", t => t.IDMTD)
                .Index(t => t.IDMTD);
            
            CreateTable(
                "dbo.QUANLYHT",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        IDHT = c.Int(nullable: false),
                        GHICHU = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.ID, t.IDHT })
                .ForeignKey("dbo.CBQUANLY", t => t.ID)
                .ForeignKey("dbo.HOITHAO", t => t.IDHT)
                .Index(t => t.ID)
                .Index(t => t.IDHT);
            
            CreateTable(
                "dbo.CBQUANLY",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TENCB = c.String(maxLength: 255),
                        EMAILCB = c.String(maxLength: 255),
                        SDTCB = c.String(maxLength: 255),
                        USERNAME = c.String(maxLength: 255),
                        PASWORD = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LOP",
                c => new
                    {
                        IDLOP = c.Int(nullable: false, identity: true),
                        IDNGANH = c.Int(nullable: false),
                        TENLOP = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDLOP)
                .ForeignKey("dbo.NGANH", t => t.IDNGANH)
                .Index(t => t.IDNGANH);
            
            CreateTable(
                "dbo.NGANH",
                c => new
                    {
                        IDNGANH = c.Int(nullable: false, identity: true),
                        TENNGANH = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDNGANH);
            
            CreateTable(
                "dbo.SINHVIEN",
                c => new
                    {
                        IDSV = c.Int(nullable: false, identity: true),
                        IDLOP = c.Int(nullable: false),
                        HOTEN = c.String(maxLength: 255),
                        EMAIL = c.String(maxLength: 255),
                        SDT = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IDSV)
                .ForeignKey("dbo.LOP", t => t.IDLOP)
                .Index(t => t.IDLOP);
            
            CreateTable(
                "dbo.THANHVIEN",
                c => new
                    {
                        IDTV = c.Int(nullable: false, identity: true),
                        IDLOP = c.Int(nullable: false),
                        TENTV = c.String(maxLength: 255),
                        EMAILTV = c.String(maxLength: 255),
                        SDTTV = c.String(maxLength: 255),
                        USERNAME = c.String(maxLength: 255),
                        PASSWORD = c.String(maxLength: 255),
                        TRANGTHAI = c.Boolean(),
                    })
                .PrimaryKey(t => t.IDTV)
                .ForeignKey("dbo.LOP", t => t.IDLOP)
                .Index(t => t.IDLOP);
            
            CreateTable(
                "dbo.THUOCDOI",
                c => new
                    {
                        IDDOI = c.Int(nullable: false),
                        IDVDVDOI = c.Int(nullable: false),
                        GHICHU = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.IDDOI, t.IDVDVDOI })
                .ForeignKey("dbo.THANHVIENDOI", t => t.IDVDVDOI)
                .ForeignKey("dbo.DOI", t => t.IDDOI)
                .Index(t => t.IDDOI)
                .Index(t => t.IDVDVDOI);
            
            CreateTable(
                "dbo.THANHVIENDOI",
                c => new
                    {
                        IDVDVDOI = c.Int(nullable: false, identity: true),
                        IDSV = c.Int(),
                        TENVDV = c.String(maxLength: 255),
                        EMAILVDV = c.String(maxLength: 255),
                        SDTVDV = c.String(maxLength: 255),
                        BOTHIDAU = c.Boolean(),
                        GHICHU = c.String(),
                    })
                .PrimaryKey(t => t.IDVDVDOI);
            
            CreateTable(
                "dbo.LIENHE",
                c => new
                    {
                        IDLIENHE = c.Int(nullable: false, identity: true),
                        DONVI = c.String(maxLength: 255),
                        TIEUDE = c.String(maxLength: 255),
                        NOIDUNG = c.String(),
                    })
                .PrimaryKey(t => t.IDLIENHE);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.THUOCDOI", "IDDOI", "dbo.DOI");
            DropForeignKey("dbo.THUOCDOI", "IDVDVDOI", "dbo.THANHVIENDOI");
            DropForeignKey("dbo.THANHVIEN", "IDLOP", "dbo.LOP");
            DropForeignKey("dbo.SINHVIEN", "IDLOP", "dbo.LOP");
            DropForeignKey("dbo.LOP", "IDNGANH", "dbo.NGANH");
            DropForeignKey("dbo.DOI", "IDLOP", "dbo.LOP");
            DropForeignKey("dbo.CTTRANDAU", "IDDOI", "dbo.DOI");
            DropForeignKey("dbo.TRANDAU", "IDVONGDAU", "dbo.VONGDAU");
            DropForeignKey("dbo.VONGDAU", "IDHT", "dbo.HOITHAO");
            DropForeignKey("dbo.QUANLYHT", "IDHT", "dbo.HOITHAO");
            DropForeignKey("dbo.QUANLYHT", "ID", "dbo.CBQUANLY");
            DropForeignKey("dbo.KINHPHIHOTRO", "IDHT", "dbo.HOITHAO");
            DropForeignKey("dbo.DOI", "IDHT", "dbo.HOITHAO");
            DropForeignKey("dbo.COMTD", "IDHT", "dbo.HOITHAO");
            DropForeignKey("dbo.VONGDAU", "IDMTD", "dbo.MONTHIDAU");
            DropForeignKey("dbo.QUYDINH", "IDMTD", "dbo.MONTHIDAU");
            DropForeignKey("dbo.MONTHIDAU", "IDLOAIMTD", "dbo.LOAIMTD");
            DropForeignKey("dbo.KINHPHIHOTRO", "IDMTD", "dbo.MONTHIDAU");
            DropForeignKey("dbo.GIAITHUONG", "IDMTD", "dbo.MONTHIDAU");
            DropForeignKey("dbo.DOI", "IDMTD", "dbo.MONTHIDAU");
            DropForeignKey("dbo.COMTD", "IDMTD", "dbo.MONTHIDAU");
            DropForeignKey("dbo.TRANDAU", "IDTRONGTAI", "dbo.TRONGTAI");
            DropForeignKey("dbo.TRANDAU", "IDSTD", "dbo.SANTHIDAU");
            DropForeignKey("dbo.HINHANH", "IDTD", "dbo.TRANDAU");
            DropForeignKey("dbo.CTTRANDAU", "IDTD", "dbo.TRANDAU");
            DropForeignKey("dbo.DOI", "IDBANG", "dbo.BANGDAU");
            DropIndex("dbo.THUOCDOI", new[] { "IDVDVDOI" });
            DropIndex("dbo.THUOCDOI", new[] { "IDDOI" });
            DropIndex("dbo.THANHVIEN", new[] { "IDLOP" });
            DropIndex("dbo.SINHVIEN", new[] { "IDLOP" });
            DropIndex("dbo.LOP", new[] { "IDNGANH" });
            DropIndex("dbo.QUANLYHT", new[] { "IDHT" });
            DropIndex("dbo.QUANLYHT", new[] { "ID" });
            DropIndex("dbo.QUYDINH", new[] { "IDMTD" });
            DropIndex("dbo.KINHPHIHOTRO", new[] { "IDMTD" });
            DropIndex("dbo.KINHPHIHOTRO", new[] { "IDHT" });
            DropIndex("dbo.GIAITHUONG", new[] { "IDMTD" });
            DropIndex("dbo.MONTHIDAU", new[] { "IDLOAIMTD" });
            DropIndex("dbo.COMTD", new[] { "IDHT" });
            DropIndex("dbo.COMTD", new[] { "IDMTD" });
            DropIndex("dbo.VONGDAU", new[] { "IDHT" });
            DropIndex("dbo.VONGDAU", new[] { "IDMTD" });
            DropIndex("dbo.HINHANH", new[] { "IDTD" });
            DropIndex("dbo.TRANDAU", new[] { "IDSTD" });
            DropIndex("dbo.TRANDAU", new[] { "IDTRONGTAI" });
            DropIndex("dbo.TRANDAU", new[] { "IDVONGDAU" });
            DropIndex("dbo.CTTRANDAU", new[] { "IDTD" });
            DropIndex("dbo.CTTRANDAU", new[] { "IDDOI" });
            DropIndex("dbo.DOI", new[] { "IDLOP" });
            DropIndex("dbo.DOI", new[] { "IDBANG" });
            DropIndex("dbo.DOI", new[] { "IDMTD" });
            DropIndex("dbo.DOI", new[] { "IDHT" });
            DropTable("dbo.LIENHE");
            DropTable("dbo.THANHVIENDOI");
            DropTable("dbo.THUOCDOI");
            DropTable("dbo.THANHVIEN");
            DropTable("dbo.SINHVIEN");
            DropTable("dbo.NGANH");
            DropTable("dbo.LOP");
            DropTable("dbo.CBQUANLY");
            DropTable("dbo.QUANLYHT");
            DropTable("dbo.QUYDINH");
            DropTable("dbo.LOAIMTD");
            DropTable("dbo.KINHPHIHOTRO");
            DropTable("dbo.GIAITHUONG");
            DropTable("dbo.MONTHIDAU");
            DropTable("dbo.COMTD");
            DropTable("dbo.HOITHAO");
            DropTable("dbo.VONGDAU");
            DropTable("dbo.TRONGTAI");
            DropTable("dbo.SANTHIDAU");
            DropTable("dbo.HINHANH");
            DropTable("dbo.TRANDAU");
            DropTable("dbo.CTTRANDAU");
            DropTable("dbo.DOI");
            DropTable("dbo.BANGDAU");
        }
    }
}
