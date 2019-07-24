using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData
{
 public   class HomePage: Entity
    {
        public string AdmNo { get; set; }
        public string InHosDep { get; set; }
        public string OutHosDep { get; set; }
        public string YLFKFS { get; set; }
        /// <summary>
        /// 护士办出院时间
        /// </summary>
        public DateTime? NurseOutHosDay { get; set; }
        public string JKKH { get; set; }

        public int? ZYCS { get; set; }

        public string BAH { get; set; }

        public string XM { get; set; }

        public string XB { get; set; }

        public DateTime? CSRQ { get; set; }

        public int? NL { get; set; }

        public string GJ { get; set; }

        public int? BZYZSNL { get; set; }

        public int? XSECSTZ { get; set; }
        public int? XSERYTZ { get; set; }
        public string CSD_SG { get; set; }
        public string CSD_SI { get; set; }
        public string CSD_QX { get; set; }
        public string CSD_DZ { get; set; }
        public string GG_SG { get; set; }
        public string GG_SI { get; set; }
        public string MZ { get; set; }//民族
        public string SFZJLB { get; set; }
        public string SFZH { get; set; }
        public string ZY { get; set; }
        public string HY { get; set; }
        public string XZZ_SG { get; set; }
        public string XZZ_SI { get; set; }
        public string XZZ_QX { get; set; }
        public string XZZ_DZ { get; set; }
        public string DH { get; set; }
        public string YB1 { get; set; }
        public string HKDZ_SG { get; set; }
        public string HKDZ_SI { get; set; }
        public string HKDZ_QX { get; set; }
        public string HKDZ_DZ { get; set; }
        public string YB2 { get; set; }
        public string GZDWJDZMC { get; set; }
        public string GZDWJDZ_SG { get; set; }
        public string GZDWJDZ_SI { get; set; }
        public string GZDWJDZ_QX { get; set; }
        public string GZDWJDZ_DZ { get; set; }
        public string DWDH { get; set; }
        public string YB3 { get; set; }
        public string LXRXM { get; set; }
        public string GX { get; set; }
        public string DZ_SG { get; set; }
        public string DZ_SI { get; set; }
        public string DZ_QX { get; set; }
        public string DZ_DZ { get; set; }
        public string DH2 { get; set; }
        public string RYTJ { get; set; }
        public string ZZYLJGMC { get; set; }
        public DateTime? RYSJ { get; set; }
        public int? RYSJS { get; set; }
        public string RYKB { get; set; }
        public string RYKBMC { get; set; }
        public string RYBF { get; set; }
        public string ZKKB { get; set; }
        public string ZKKBMC { get; set; }
        public string ZKKB1 { get; set; }
        public string ZKKB1MC { get; set; }
        public string ZKKB2 { get; set; }
        public string ZKKB2MC { get; set; }
        public DateTime? CYSJ { get; set; }
        public int? CYSJS { get; set; }
        public string CYKB { get; set; }
        public string CYKBMC { get; set; }
        public string CYBF { get; set; }
        public int? SJZYTS { get; set; }
        public string MZZD { get; set; }
        public string JBBM { get; set; }
        public string ZYZD { get; set; }
        public string JBDM { get; set; }
        public string RYBQ { get; set; }
        public string QTZD8 { get; set; }
        public string JBDM8 { get; set; }
        public string RYBQ8 { get; set; }
        public string QTZD1 { get; set; }
        public string JBDM1 { get; set; }
        public string RYBQ1 { get; set; }
        public string QTZD9 { get; set; }
        public string JBDM9 { get; set; }
        public string RYBQ9 { get; set; }
        public string QTZD2 { get; set; }
        public string JBDM2 { get; set; }
        public string RYBQ2 { get; set; }
        public string QTZD10 { get; set; }
        public string JBDM10 { get; set; }
        public string RYBQ10 { get; set; }
        public string QTZD3 { get; set; }
        public string JBDM3 { get; set; }
        public string RYBQ3 { get; set; }
        public string QTZD11 { get; set; }
        public string JBDM11 { get; set; }
        public string RYBQ11 { get; set; }
        public string QTZD4 { get; set; }
        public string JBDM4 { get; set; }
        public string RYBQ4 { get; set; }
        public string QTZD12 { get; set; }
        public string JBDM12 { get; set; }
        public string RYBQ12 { get; set; }
        public string QTZD5 { get; set; }
        public string JBDM5 { get; set; }
        public string RYBQ5 { get; set; }
        public string QTZD13 { get; set; }
        public string JBDM13 { get; set; }
        public string RYBQ13 { get; set; }
        public string QTZD6 { get; set; }
        public string JBDM6 { get; set; }
        public string RYBQ6 { get; set; }
        public string QTZD14 { get; set; }
        public string JBDM14 { get; set; }
        public string RYBQ14 { get; set; }
        public string QTZD7 { get; set; }
        public string JBDM7 { get; set; }
        public string RYBQ7 { get; set; }
        public string QTZD15 { get; set; }
        public string JBDM15 { get; set; }
        public string RYBQ15 { get; set; }
        public string WBYY { get; set; }
        public string H23 { get; set; }
        public string BLZD { get; set; }
        public string BLH { get; set; }
        public string JBMM { get; set; }
        public string ICDO3 { get; set; }
        public string ZGZDYJ { get; set; }
        public string YWGM { get; set; }
        public string GMYW { get; set; }
        public string SWHZSJ { get; set; }
        public string XX { get; set; }
        public string TJHL_T { get; set; }
        public int? YJHL_T { get; set; }
        public int? EJHL_T { get; set; }
        public int? SJHL_T { get; set; }
        public string RH { get; set; }
        public string KZR { get; set; }
        public string YLZZ { get; set; }
        public string ZRYS { get; set; }
        public string ZZYS { get; set; }
        public string ZYYS { get; set; }
        public string ZRHS { get; set; }
        public string JXYS { get; set; }
        public string SXYS { get; set; }
        public string BMY { get; set; }
        public string BAZL { get; set; }
        public string ZKYS { get; set; }
        public string ZKHS { get; set; }
        public DateTime? ZKRQ { get; set; }
        public string SSJCZBM1 { get; set; }
        public DateTime? SSJCZRQ1 { get; set; }
        public string SSJB1 { get; set; }
        public string SSLX1 { get; set; }
        public string SSJCZMC1 { get; set; }
        public string SZ1 { get; set; }
        public string YZ1 { get; set; }
        public string EZ1 { get; set; }
        public string QKDJ1 { get; set; }
        public string QKDJ1MC { get; set; }
        public string QKYHLB1 { get; set; }
        public string MZFS1 { get; set; }
        public string MZYS1 { get; set; }
        public string SSJCZBM2 { get; set; }
        public DateTime? SSJCZRQ2 { get; set; }
        public string SSJB2 { get; set; }
        public string SSLX2 { get; set; }
        public string SSJCZMC2 { get; set; }
        public string SZ2 { get; set; }
        public string YZ2 { get; set; }
        public string EZ2 { get; set; }
        public string QKDJ2 { get; set; }
        public string QKDJ2MC { get; set; }
        public string QKYHLB2 { get; set; }
        public string MZFS2 { get; set; }
        public string MZYS2 { get; set; }
        public string SSJCZBM3 { get; set; }
        public DateTime? SSJCZRQ3 { get; set; }
        public string SSJB3 { get; set; }
        public string SSLX3 { get; set; }
        public string SSJCZMC3 { get; set; }
        public string SZ3 { get; set; }
        public string YZ3 { get; set; }
        public string EZ3 { get; set; }
        public string QKDJ3 { get; set; }
        public string QKDJ3MC { get; set; }
        public string QKYHLB3 { get; set; }
        public string MZFS3 { get; set; }
        public string MZYS3 { get; set; }
        public string SSJCZBM4 { get; set; }
        public DateTime? SSJCZRQ4 { get; set; }
        public string SSJB4 { get; set; }
        public string SSLX4 { get; set; }
        public string SSJCZMC4 { get; set; }
        public string SZ4 { get; set; }
        public string YZ4 { get; set; }
        public string EZ4 { get; set; }
        public string QKDJ4 { get; set; }
        public string QKDJ4MC { get; set; }
        public string QKYHLB4 { get; set; }
        public string MZFS4 { get; set; }
        public string MZYS4 { get; set; }
        public string SSJCZBM5 { get; set; }
        public DateTime? SSJCZRQ5 { get; set; }
        public string SSJB5 { get; set; }
        public string SSLX5 { get; set; }
        public string SSJCZMC5 { get; set; }
        public string SZ5 { get; set; }
        public string YZ5 { get; set; }
        public string EZ5 { get; set; }
        public string QKDJ5 { get; set; }
        public string QKDJ5MC { get; set; }
        public string QKYHLB5 { get; set; }
        public string MZFS5 { get; set; }
        public string MZYS5 { get; set; }
        public string SSJCZBM6 { get; set; }
        public DateTime? SSJCZRQ6 { get; set; }
        public string SSJB6 { get; set; }
        public string SSLX6 { get; set; }
        public string SSJCZMC6 { get; set; }
        public string SZ6 { get; set; }
        public string YZ6 { get; set; }
        public string EZ6 { get; set; }
        public string QKDJ6 { get; set; }
        public string QKDJ6MC { get; set; }
        public string QKYHLB6 { get; set; }
        public string MZFS6 { get; set; }
        public string MZYS6 { get; set; }
        public string SSJCZBM7 { get; set; }
        public DateTime? SSJCZRQ7 { get; set; }
        public string SSJB7 { get; set; }
        public string SSLX7 { get; set; }
        public string SSJCZMC7 { get; set; }
        public string SZ7 { get; set; }
        public string YZ7 { get; set; }
        public string EZ7 { get; set; }
        public string QKDJ7 { get; set; }
        public string QKDJ7MC { get; set; }
        public string QKYHLB7 { get; set; }
        public string MZFS7 { get; set; }
        public string MZYS7 { get; set; }
        public string SSJCZBM8 { get; set; }
        public DateTime? SSJCZRQ8 { get; set; }
        public string SSJB8 { get; set; }
        public string SSLX8 { get; set; }
        public string SSJCZMC8 { get; set; }
        public string SZ8 { get; set; }
        public string YZ8 { get; set; }
        public string EZ8 { get; set; }
        public string QKDJ8 { get; set; }
        public string QKDJ8MC { get; set; }
        public string QKYHLB8 { get; set; }
        public string MZFS8 { get; set; }
        public string MZYS8 { get; set; }
        public string RJQK { get; set; }
        public string WCQK { get; set; }
        public string BYQK { get; set; }
        public string LYFS { get; set; }
        public string YZZY_YLJG { get; set; }
        public string WSY_YLJG { get; set; }
        public string SFZZYJH { get; set; }
        public string MD { get; set; }
        public int? RYQ_T { get; set; }
        public int? RYQ_XS { get; set;}
        public int? RYQ_F { get; set; }
        public int? RYH_T { get; set; }
        public int? RYH_XS { get; set; }
        public int? RYH_F { get; set; }
        public decimal ZFY { get; set; }
        public decimal ZFJE { get; set; }
        public decimal YLFUF { get; set; }
        public decimal ZLCZF { get; set; }
        public decimal HLF { get; set; }
        public decimal QTFY { get; set; }
        public decimal BLZDF { get; set; }
        public decimal SYSZDF { get; set; }
        public decimal YXXZDF { get; set; }
        public decimal LCZDXMF { get; set; }
        public decimal FSSZLXMF { get; set; }
        public decimal WLZLF { get; set; }
        public decimal SSZLF { get; set; }
        public decimal MAF { get; set; }
        public decimal SSF { get; set; }
        public decimal KFF { get; set; }
        public decimal ZYZLF { get; set; }
        public decimal XYF { get; set; }
        public decimal KJYWF { get; set; }
        /// <summary>
        /// 中成药费
        /// </summary>
        public decimal ZCYF { get; set; }
        /// <summary>
        /// 中草药费
        /// </summary>
        public decimal ZCYF1 { get; set; }
        public decimal XF { get; set; }
        public decimal BDBLZPF { get; set; }
        public decimal QDBLZPF { get; set; }
        public decimal NXYZLZPF { get; set; }
        public decimal XBYZLZPF { get; set; }
        public decimal HCYYCLF { get; set; }
        public decimal YYCLF { get; set; }
        public decimal YCXYYCLF { get; set; }
        public decimal QTF { get; set; }

    }
}
