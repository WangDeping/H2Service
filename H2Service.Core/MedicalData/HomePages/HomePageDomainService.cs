using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Events.Bus;
using H2Service.Events;
using H2Service.MedicalData.ICDMaps;
using InterSystems.Data.CacheClient;
using LanSeCheng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2Service.MedicalData.HomePages
{
    public class HomePageDomainService : DomainService
    {
        private IRepository<HomePage> _homePageRepository;
        private ICDMapCaching _ICDMapCaching;
        private IEventBus _eventBus;
        private CacheConnection conn;
      
     
        private string bah = "";
        private DateTime cysj;
        private DateTime DateTime20180901 = DateTime.Parse("2018-09-01");
        private DateTime DateTime20190624 = DateTime.Parse("2019-06-24");
        public HomePageDomainService(IRepository<HomePage> homePageRepository
         , ICDMapCaching ICDMapCaching, IEventBus eventBus)
        {
            _homePageRepository = homePageRepository;
            conn = IntersystemsCache.GetConnection();
            _ICDMapCaching = ICDMapCaching;
            _eventBus = eventBus;
            //conn.ConnectionTimeout = 60 * 15;

        }

        public HomePage GetErpHomePage(string admNo) {
            var homepage = new HomePage();
            var cmd = DHCExportService.GetEprInfo(conn);
            DateTime? dateNull = null;
            cmd.Parameters.Add("InputIpNo", admNo);
            conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                homepage.AdmNo = admNo;
                homepage.BAH = reader["BAH"] + "";
               
                homepage.CSRQ =string.IsNullOrEmpty(reader["CSRQ"]+"")?dateNull: DateTime.Parse(reader["CSRQ"]+"").Date;
                homepage.RYSJ = string.IsNullOrEmpty(reader["RYSJ"] + "") ? dateNull : DateTime.Parse(reader["RYSJ"] + "").Date;
                homepage.CYSJ = string.IsNullOrEmpty(reader["CYSJ"] + "") ? dateNull : DateTime.Parse(reader["CYSJ"] + "").Date;
                homepage.RYSJS = string.IsNullOrEmpty(reader["RYSJ"]+"") ? 0 : DateTime.Parse(reader["RYSJ"] + "").Hour;
                homepage.CYSJS = string.IsNullOrEmpty(reader["CYSJ"] + "") ? 0 : DateTime.Parse(reader["CYSJ"] + "").Hour;
                homepage.SJZYTS = ToInt32(reader["SJZYTS"]);//实际住院天数
                homepage.NL =ToInt32(reader["NL"]);//年龄
                homepage.SFZH = (reader["SFZH"] + "").ToUpper();//身份证
                homepage.YB1 = reader["YB1"]+"";//邮编1
                homepage.YB2 = reader["YB2"] + "";//邮编2
                homepage.YB3 = reader["YB3"] + "";//邮编3
                homepage.JBBM=reader["JBBM"]+"";//门诊诊断编码
                homepage.MZZD = reader["MZZD"] + "";//门诊诊断
                homepage.ZYZD = reader["ZYZD"] + "";//主要诊断
                homepage.JBDM = reader["JBDM"] + "";//主要诊断编码
                homepage.JBDM1 = reader["JBDM1"] + "";
                homepage.JBDM2 = reader["JBDM2"] + "";
                homepage.JBDM3 = reader["JBDM3"] + "";
                homepage.JBDM4 = reader["JBDM4"] + "";
                homepage.JBDM5 = reader["JBDM5"] + "";
                homepage.JBDM6 = reader["JBDM6"] + "";
                homepage.JBDM7 = reader["JBDM7"] + "";
                homepage.JBDM8 = reader["JBDM8"] + "";
                homepage.JBDM9 = reader["JBDM9"] + "";
                homepage.JBDM10 = reader["JBDM10"] + "";
                homepage.JBDM11 = reader["JBDM11"] + "";
                homepage.JBDM12 = reader["JBDM12"] + "";
                homepage.JBDM13 = reader["JBDM13"] + "";
                homepage.JBDM14 = reader["JBDM14"] + "";
                homepage.JBDM15 = reader["JBDM15"] + "";
                homepage.CYKBMC = reader["_CYKB"] + "";
                homepage.RYBQ = reader["RYBQ"] + "";//入院病情
                homepage.H23 = reader["H23"] + "";//中毒外部损伤原因
                homepage.ZKKBMC = reader["_ZKKB"] + "";//
                homepage.ZKKB1MC = reader["_ZKKB1"] + "";//
                homepage.ZKKB2MC = reader["_ZKKB2"] + "";//
                homepage.RYKBMC = reader["_RYKB"] + "";//
                homepage.CYKBMC = reader["_CYKB"] + "";//
            }
            reader.Close();
            conn.Close();
            return homepage;
        }

        public void SynchronousHomePage(string dateFrom, string dateTo)
        {
            try
            {
                var cmd = DHCDATAExportQuery.ExportEprData(conn);
                cmd.Parameters.Add("aDateFrom", dateFrom);
                cmd.Parameters.Add("aDateTo", dateTo);
                
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var admNo = reader["AdmNo"] + "";
                    var homePage = _homePageRepository.FirstOrDefault(T => T.AdmNo ==admNo);                 
                    homePage = homePage == null ? new HomePage() : homePage;
                    homePage.AdmNo = admNo;
                    this.SetHomePage(ref homePage, reader);
                    
                    if (homePage.Id > 0)
                        _homePageRepository.Update(homePage);
                    else
                        _homePageRepository.Insert(homePage);


                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Logger.Error("同步病案首页出错:" + ex.Message + "日期为:" + dateFrom + "bah:" + bah);
                //throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// 更新一条病案首页信息
        /// </summary>
        /// <param name="admNo">就诊记录</param>
        public void UpdateHomepage(string admNo) {
            try
            {
                var homePage = _homePageRepository.FirstOrDefault(T => T.AdmNo == admNo);
                var cmd = DHCExportService.ExportEprData(conn);
                cmd.Parameters.Add("InputIPNo", admNo);
                conn.Open();
                var reader = cmd.ExecuteReader();               
                while (reader.Read())
                {
                    /*******门诊诊断+主要诊断+15住院诊断********/

                    this.SetHomePage(ref homePage, reader);
                    _homePageRepository.Update(homePage);                 


                }
                reader.Close();
            }

            catch (Exception ex)
            {
                Logger.Error("更新病案首页出错:" + ex.Message +"admNo:" + admNo);
                //throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        private void SetHomePage(ref HomePage homePage, CacheDataReader reader) {           
            bah = reader["BAH"] + "";
            cysj = ToDateTime(reader["CYSJ"]).Value;
            var mzzd = MapNatDis(reader["JBBM"] + "");
            var zyzd = MapNatDis(reader["JBDM"] + "");
           
            var qtzd1 = MapNatDis(reader["JBDM1"] + "");
            var qtzd2 = MapNatDis(reader["JBDM2"] + "");
            var qtzd3 = MapNatDis(reader["JBDM3"] + "");
            var qtzd4 = MapNatDis(reader["JBDM4"] + "");
            var qtzd5 = MapNatDis(reader["JBDM5"] + "");
            var qtzd6 = MapNatDis(reader["JBDM6"] + "");
            var qtzd7 = MapNatDis(reader["JBDM7"] + "");
            var qtzd8 = MapNatDis(reader["JBDM8"] + "");
            var qtzd9 = MapNatDis(reader["JBDM9"] + "");
            var qtzd10 = MapNatDis(reader["JBDM10"] + "");
            var qtzd11 = MapNatDis(reader["JBDM11"] + "");
            var qtzd12 = MapNatDis(reader["JBDM12"] + "");
            var qtzd13 = MapNatDis(reader["JBDM13"] + "");
            var qtzd14 = MapNatDis(reader["JBDM14"] + "");
            var qtzd15 = MapNatDis(reader["JBDM15"] + "");
            /***************8个手术与操作编码**********************/
            var sscz1 = MapNatOper(reader["SSJCZBM1"] + "");
            var sscz2 = MapNatOper(reader["SSJCZBM2"] + "");
            var sscz3 = MapNatOper(reader["SSJCZBM3"] + "");
            var sscz4 = MapNatOper(reader["SSJCZBM4"] + "");
            var sscz5 = MapNatOper(reader["SSJCZBM5"] + "");
            var sscz6 = MapNatOper(reader["SSJCZBM6"] + "");
            var sscz7 = MapNatOper(reader["SSJCZBM7"] + "");
            var sscz8 = MapNatOper(reader["SSJCZBM8"] + "");

            var qk1 = GetQKDJYHLB(reader["_QKDJ1"] + "");
            var qk2 = GetQKDJYHLB(reader["_QKDJ2"] + "");
            var qk3 = GetQKDJYHLB(reader["_QKDJ3"] + "");
            var qk4 = GetQKDJYHLB(reader["_QKDJ4"] + "");
            var qk5 = GetQKDJYHLB(reader["_QKDJ5"] + "");
            var qk6 = GetQKDJYHLB(reader["_QKDJ6"] + "");
            var qk7 = GetQKDJYHLB(reader["_QKDJ7"] + "");
            var qk8 = GetQKDJYHLB(reader["_QKDJ8"] + "");
            /******************************************/
            homePage.BAH = reader["BAH"] + "";
            homePage.InHosDep = reader["InHosLoc"] + "";
            homePage.OutHosDep = reader["OutHosLoc"] + "";
            homePage.NurseOutHosDay = DateTime.Parse(reader["NurseOutHosDay"] + "");           
            homePage.BAZL = "1";//reader["BAZL"] + "";
            homePage.BDBLZPF = ToDecimal(reader["BDBLZPF"] + "");
            homePage.BLZD = reader["BLZD"] + "";
            homePage.BLZDF = ToDecimal(reader["BLZDF"] + "");
            homePage.BLH = (reader["BLH"] + "");
            homePage.BMY = reader["BMY"] + "";
            homePage.BYQK = reader["BYQK"] + "";
            homePage.BZYZSNL = ToInt32(reader["BZYZSNL"]);
            homePage.CSRQ = ToDateTime(reader["CSRQ"]);
            homePage.CSD_DZ = reader["CSD_DZ"] + "";
            homePage.CSD_QX = reader["CSD_QX"] + "";
            homePage.CSD_SG = reader["CSD_SG"] + "";
            homePage.CSD_SI = reader["CSD_SI"] + "";
            homePage.CYBF = reader["CYBF"] + "";
            homePage.CYKB = reader["CYKB"] + "";
            homePage.CYSJ = ToDateTime(reader["CYSJ"]);
            homePage.DH = reader["DH"] + "";
            homePage.DH2 = reader["DH2"] + "";
            homePage.DWDH = reader["DWDH"] + "";
            homePage.DZ_DZ = reader["DZ_DZ"] + "";
            homePage.DZ_QX = reader["DZ_QX"] + "";
            homePage.DZ_SG = reader["DZ_SG"] + "";
            homePage.DZ_SI = reader["DZ_SI"] + "";
            homePage.EJHL_T = ToInt32(reader["EJHL_T"]);
            homePage.EZ1 = reader["EZ1"] + "";
            homePage.EZ2 = reader["EZ2"] + "";
            homePage.EZ3 = reader["EZ3"] + "";
            homePage.EZ4 = reader["EZ4"] + "";
            homePage.EZ5 = reader["EZ5"] + "";
            homePage.EZ6 = reader["EZ6"] + "";
            homePage.EZ7 = reader["EZ7"] + "";
            homePage.EZ8 = reader["EZ8"] + "";
            homePage.FSSZLXMF = ToDecimal(reader["FSSZLXMF"] + "");
            homePage.GG_SG = reader["GG_SG"] + "";
            homePage.GG_SI = reader["GG_SI"] + "";
            homePage.GJ = reader["GJ"] + "";
            homePage.GMYW = reader["GMYW"] + "";
            homePage.GX = reader["GX"] + "";
            homePage.GZDWJDZMC = reader["GZDWJDZMC"] + "";
            homePage.GZDWJDZ_DZ = reader["GZDWJDZ_DZ"] + "";
            homePage.GZDWJDZ_QX = reader["GZDWJDZ_QX"] + "";
            homePage.GZDWJDZ_SG = reader["GZDWJDZ_SG"] + "";
            homePage.GZDWJDZ_SI = reader["GZDWJDZ_SI"] + "";
            homePage.H23 = reader["H23"] + "";
            homePage.HCYYCLF = ToDecimal(reader["HCYYCLF"] + "");
            homePage.HKDZ_DZ = reader["HKDZ_DZ"] + "";
            homePage.HKDZ_QX = reader["HKDZ_QX"] + "";
            homePage.HKDZ_SG = reader["HKDZ_SG"] + "";
            homePage.HKDZ_SI = reader["HKDZ_SI"] + "";
            homePage.HLF = ToDecimal(reader["HLF"] + "");
            homePage.HY = reader["HY"] + "";
            homePage.ICDO3 = reader["ICDO3"] + "";
            homePage.JBBM = mzzd[0];
            homePage.JBDM = zyzd[0];
            homePage.JBDM1 = qtzd1[0];
            homePage.JBDM2 = qtzd2[0];
            homePage.JBDM3 = qtzd3[0];
            homePage.JBDM4 = qtzd4[0];
            homePage.JBDM5 = qtzd5[0];
            homePage.JBDM6 = qtzd6[0];
            homePage.JBDM7 = qtzd7[0];
            homePage.JBDM8 = qtzd8[0];
            homePage.JBDM9 = qtzd9[0];
            homePage.JBDM10 = qtzd10[0];
            homePage.JBDM11 = qtzd11[0];
            homePage.JBDM12 = qtzd12[0];
            homePage.JBDM13 = qtzd13[0];
            homePage.JBDM14 = qtzd14[0];
            homePage.JBDM15 = qtzd15[0];
            homePage.JBMM = reader["JBMM"] + "";
            homePage.JKKH = reader["JKKH"] + "";
            homePage.JXYS = reader["JXYS"] + "";
            homePage.KFF = ToDecimal(reader["KFF"] + "");
            homePage.KJYWF = ToDecimal(reader["KJYWF"] + "");
            homePage.KZR = reader["KZR"] + "";
            homePage.LCZDXMF = ToDecimal(reader["LCZDXMF"] + "");
            homePage.LXRXM = reader["LXRXM"] + "";
            homePage.LYFS = reader["LYFS"] + "";
            homePage.MAF = ToDecimal(reader["MAF"] + "");
            homePage.MD = reader["MD"] + "";
            homePage.MZ = reader["MZ"] + "";
            homePage.MZFS1 = reader["MZFS1"] + "";
            homePage.MZFS2 = reader["MZFS2"] + "";
            homePage.MZFS3 = reader["MZFS3"] + "";
            homePage.MZFS4 = reader["MZFS4"] + "";
            homePage.MZFS5 = reader["MZFS5"] + "";
            homePage.MZFS6 = reader["MZFS6"] + "";
            homePage.MZFS7 = reader["MZFS7"] + "";
            homePage.MZFS8 = reader["MZFS8"] + "";
            homePage.MZYS1 = reader["MZYS1"] + "";
            homePage.MZYS2 = reader["MZYS2"] + "";
            homePage.MZYS3 = reader["MZYS3"] + "";
            homePage.MZYS4 = reader["MZYS4"] + "";
            homePage.MZYS5 = reader["MZYS5"] + "";
            homePage.MZYS6 = reader["MZYS6"] + "";
            homePage.MZYS7 = reader["MZYS7"] + "";
            homePage.MZYS8 = reader["MZYS8"] + "";
            homePage.MZZD = mzzd[1];
            homePage.NL = ToInt32(reader["NL"]);
            homePage.NXYZLZPF = ToDecimal(reader["NXYZLZPF"] + "");
            homePage.QDBLZPF = ToDecimal(reader["QDBLZPF"] + "");
            homePage.QKDJ1 = qk1[0];
            homePage.QKDJ2 = qk2[0];
            homePage.QKDJ3 = qk3[0];
            homePage.QKDJ4 = qk4[0];
            homePage.QKDJ5 = qk5[0];
            homePage.QKDJ6 = qk6[0];
            homePage.QKDJ7 = qk7[0];
            homePage.QKDJ8 = qk8[0];
            homePage.QKYHLB1 = qk1[1];
            homePage.QKYHLB2 = qk2[1];
            homePage.QKYHLB3 = qk3[1];
            homePage.QKYHLB4 = qk4[1];
            homePage.QKYHLB5 = qk5[1];
            homePage.QKYHLB6 = qk6[1];
            homePage.QKYHLB7 = qk7[1];
            homePage.QKYHLB8 = qk8[1];
            homePage.QTF = ToDecimal(reader["QTF"] + "");
            homePage.QTFY = ToDecimal(reader["QTFY"] + "");
            homePage.QTZD1 = qtzd1[1];
            homePage.QTZD2 = qtzd2[1];
            homePage.QTZD3 = qtzd3[1];
            homePage.QTZD4 = qtzd4[1];
            homePage.QTZD5 = qtzd5[1];
            homePage.QTZD6 = qtzd6[1];
            homePage.QTZD7 = qtzd7[1];
            homePage.QTZD8 = qtzd8[1];
            homePage.QTZD9 = qtzd9[1];
            homePage.QTZD10 = qtzd10[1];
            homePage.QTZD11 = qtzd11[1];
            homePage.QTZD12 = qtzd12[1];
            homePage.QTZD13 = qtzd13[1];
            homePage.QTZD14 = qtzd14[1];
            homePage.QTZD15 = qtzd15[1];
            homePage.RH = reader["RH"] + "";
            homePage.RJQK = reader["RJQK"] + "";
            homePage.RYBF = reader["RYBF"] + "";
            homePage.RYBQ = reader["RYBQ"] + "";
            homePage.RYBQ1 = reader["RYBQ1"] + "";
            homePage.RYBQ2 = reader["RYBQ2"] + "";
            homePage.RYBQ3 = reader["RYBQ3"] + "";
            homePage.RYBQ4 = reader["RYBQ4"] + "";
            homePage.RYBQ5 = reader["RYBQ5"] + "";
            homePage.RYBQ6 = reader["RYBQ6"] + "";
            homePage.RYBQ7 = reader["RYBQ7"] + "";
            homePage.RYBQ8 = reader["RYBQ8"] + "";
            homePage.RYBQ9 = reader["RYBQ9"] + "";
            homePage.RYBQ10 = reader["RYBQ10"] + "";
            homePage.RYBQ11 = reader["RYBQ11"] + "";
            homePage.RYBQ12 = reader["RYBQ12"] + "";
            homePage.RYBQ13 = reader["RYBQ13"] + "";
            homePage.RYBQ14 = reader["RYBQ14"] + "";
            homePage.RYBQ15 = reader["RYBQ15"] + "";
            homePage.RYH_F = ToInt32(reader["RYH_F"]);
            homePage.RYKB = reader["RYKB"] + "";////////////
            homePage.RYQ_T = ToInt32(reader["RYQ_T"]);
            homePage.RYQ_XS = ToInt32(reader["RYQ_XS"]);
            homePage.RYQ_F = ToInt32(reader["RYQ_F"]);
            homePage.RYSJ = ToDateTime(reader["RYSJ"]);
            homePage.RYSJS = ToInt32(reader["RYSJS"]);
            homePage.CYSJS = ToInt32(reader["CYSJS"]);
            homePage.RYTJ = reader["RYTJ"] + "";
            homePage.SFZH = (reader["SFZH"] + "").ToUpper();
            homePage.SFZJLB = reader["SFZJLB"] + "";
            homePage.SFZZYJH = reader["SFZZYJH"] + "";
            homePage.SJHL_T = ToInt32(reader["SJHL_T"]);
            homePage.SJZYTS = ToInt32(reader["SJZYTS"]);
            homePage.SSF = ToDecimal(reader["SSF"] + "");
            homePage.SSJB1 = reader["SSJB1"] + "";
            homePage.SSJB2 = reader["SSJB2"] + "";
            homePage.SSJB3 = reader["SSJB3"] + "";
            homePage.SSJB4 = reader["SSJB4"] + "";
            homePage.SSJB5 = reader["SSJB5"] + "";
            homePage.SSJB6 = reader["SSJB6"] + "";
            homePage.SSJB7 = reader["SSJB7"] + "";
            homePage.SSJB8 = reader["SSJB8"] + "";
            homePage.SSJCZBM1 = sscz1[0];
            homePage.SSJCZBM2 = sscz2[0];
            homePage.SSJCZBM3 = sscz3[0];
            homePage.SSJCZBM4 = sscz4[0];
            homePage.SSJCZBM5 = sscz5[0];
            homePage.SSJCZBM6 = sscz6[0];
            homePage.SSJCZBM7 = sscz7[0];
            homePage.SSJCZBM8 = sscz8[0];

            homePage.SSJCZMC1 = sscz1[1];
            homePage.SSJCZMC2 = sscz2[1];
            homePage.SSJCZMC3 = sscz3[1];
            homePage.SSJCZMC4 = sscz4[1];
            homePage.SSJCZMC5 = sscz5[1];
            homePage.SSJCZMC6 = sscz6[1];
            homePage.SSJCZMC7 = sscz7[1];
            homePage.SSJCZMC8 = sscz8[1];
            homePage.SSJCZRQ1 = ToDateTime(reader["SSJCZRQ1"]);
            homePage.SSJCZRQ2 = ToDateTime(reader["SSJCZRQ2"]);
            homePage.SSJCZRQ3 = ToDateTime(reader["SSJCZRQ3"]);
            homePage.SSJCZRQ4 = ToDateTime(reader["SSJCZRQ4"]);
            homePage.SSJCZRQ5 = ToDateTime(reader["SSJCZRQ5"]);
            homePage.SSJCZRQ6 = ToDateTime(reader["SSJCZRQ6"]);
            homePage.SSJCZRQ7 = ToDateTime(reader["SSJCZRQ7"]);
            homePage.SSJCZRQ8 = ToDateTime(reader["SSJCZRQ8"]);
            homePage.SSLX1 = reader["SSLX1"] + "";
            homePage.SSLX2 = reader["SSLX2"] + "";
            homePage.SSLX3 = reader["SSLX3"] + "";
            homePage.SSLX4 = reader["SSLX4"] + "";
            homePage.SSLX5 = reader["SSLX5"] + "";
            homePage.SSLX6 = reader["SSLX6"] + "";
            homePage.SSLX7 = reader["SSLX7"] + "";
            homePage.SSLX8 = reader["SSLX8"] + "";
            homePage.SSZLF = ToDecimal(reader["SSZLF"] + "");
            homePage.SWHZSJ = reader["SWHZSJ"] + "";
            homePage.SXYS = reader["SXYS"] + "";
            homePage.SYSZDF = ToDecimal(reader["SYSZDF"] + "");
            homePage.SZ1 = reader["SZ1"] + "";
            homePage.SZ2 = reader["SZ2"] + "";
            homePage.SZ3 = reader["SZ3"] + "";
            homePage.SZ4 = reader["SZ4"] + "";
            homePage.SZ5 = reader["SZ5"] + "";
            homePage.SZ6 = reader["SZ6"] + "";
            homePage.SZ7 = reader["SZ7"] + "";
            homePage.SZ8 = reader["SZ8"] + "";
            homePage.TJHL_T = reader["TJHL_T"] + "";
            homePage.WBYY = reader["WBYY"] + "";
            homePage.WCQK = reader["WCQK"] + "";
            homePage.WLZLF = ToDecimal(reader["WLZLF"] + "");
            homePage.WSY_YLJG = reader["WSY_YLJG"] + "";
            homePage.XB = reader["XB"] + "";
            homePage.XBYZLZPF = ToDecimal(reader["XBYZLZPF"] + "");
            homePage.XF = ToDecimal(reader["XF"] + "");
            homePage.XM = reader["XM"] + "";
            homePage.XSECSTZ = ToInt32(reader["XSECSTZ"]);//
            homePage.XSERYTZ = ToInt32(reader["XSERYTZ"]);
            homePage.XX = reader["XX"] + "";
            homePage.XYF = ToDecimal(reader["XYF"] + "");
            homePage.XZZ_DZ = reader["XZZ_DZ"] + "";
            homePage.XZZ_QX = reader["XZZ_QX"] + "";
            homePage.XZZ_SG = reader["XZZ_SG"] + "";
            homePage.XZZ_SI = reader["XZZ_SI"] + "";
            homePage.YB1 = reader["YB1"] + "";
            homePage.YB2 = reader["YB2"] + "";
            homePage.YB3 = reader["YB3"] + "";
            homePage.YCXYYCLF = ToDecimal(reader["YCXYYCLF"] + "");
            homePage.YJHL_T = ToInt32(reader["YJHL_T"]);
            homePage.YLFKFS = reader["YLFKFS"] + "";
            homePage.YLFUF = ToDecimal(reader["YLFUF"] + "");
            homePage.YLZZ = reader["YLZZ"] + "";
            homePage.YWGM = reader["YWGM"] + "";
            homePage.YXXZDF = ToDecimal(reader["YXXZDF"] + "");
            homePage.YYCLF = ToDecimal(reader["YYCLF"] + "");
            homePage.YZ1 = reader["YZ1"] + "";
            homePage.YZ2 = reader["YZ2"] + "";
            homePage.YZ3 = reader["YZ3"] + "";
            homePage.YZ4 = reader["YZ4"] + "";
            homePage.YZ5 = reader["YZ5"] + "";
            homePage.YZ6 = reader["YZ6"] + "";
            homePage.YZ7 = reader["YZ7"] + "";
            homePage.YZ8 = reader["YZ8"] + "";
            homePage.YZZY_YLJG = reader["YZZY_YLJG"] + "";
            homePage.ZCYF = ToDecimal(reader["ZCYF"] + "");
            homePage.ZCYF1 = ToDecimal(reader["ZCYF1"] + "");
            homePage.ZFJE = ToDecimal(reader["ZFJE"] + "");
            homePage.ZFY = ToDecimal(reader["ZFY"] + "");
            homePage.ZGZDYJ = reader["ZGZDYJ"] + "";
            homePage.ZKHS = reader["ZKHS"] + "";
            homePage.ZKKB = reader["ZKKB"] + "";
            homePage.ZKKB1 = reader["ZKKB1"] + "";
            homePage.ZKKB2 = reader["ZKKB2"] + "";
            homePage.ZKRQ = ToDateTime(reader["ZKRQ"]);
            homePage.ZKYS = reader["ZKYS"] + "";
            homePage.ZLCZF = ToDecimal(reader["ZLCZF"] + "");
            homePage.ZRHS = reader["ZRHS"] + "";
            homePage.ZRYS = reader["ZRYS"] + "";
            homePage.ZY = reader["ZY"] + "";
            homePage.ZYCS = ToInt32(reader["ZYCS"]);
            homePage.ZYYS = reader["ZYYS"] + "";
            homePage.ZYZD = zyzd[1];
            homePage.ZYZLF = ToDecimal(reader["ZYZLF"] + "");
            homePage.ZZYLJGMC = reader["ZZYLJGMC"] + "";
            homePage.ZZYS = reader["ZZYS"] + "";
            homePage.ZKKBMC = reader["_ZKKB"] + "";/////
            homePage.ZKKB1MC = reader["_ZKKB1"] + "";
            homePage.ZKKB2MC = reader["_ZKKB2"] + "";
            homePage.RYKBMC = reader["_RYKB"] + "";
            homePage.CYKBMC = reader["_CYKB"] + "";
            homePage.QKDJ1MC = reader["_QKDJ1"] + "";
            homePage.QKDJ2MC = reader["_QKDJ2"] + "";
            homePage.QKDJ3MC = reader["_QKDJ3"] + "";
            homePage.QKDJ4MC = reader["_QKDJ4"] + "";
            homePage.QKDJ5MC = reader["_QKDJ5"] + "";
            homePage.QKDJ6MC = reader["_QKDJ6"] + "";
            homePage.QKDJ7MC = reader["_QKDJ7"] + "";
            homePage.QKDJ8MC = reader["_QKDJ8"] + "";

        }

        private DateTime? ToDateTime(object obj)
        {
            string dateString = obj.ToString();
            if (string.IsNullOrEmpty(dateString))
                return null;
            else
                return DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        }
        private int? ToInt32(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                    return null;
                else
                    return Convert.ToInt32(obj);
            }
            catch
            {
                return null;
            }
        }
        private decimal ToDecimal(string fy)
        {
            try
            {
                if (string.IsNullOrEmpty(fy))
                    return 0;
                else
                    return decimal.Parse(fy);
            }
            catch
            {
                return 0;
            }
        }

        private string[] MapNatDis(string code)
        {
            var result = new string[2];          
            if (string.IsNullOrEmpty(code))
                return result;
            
            Hos_Nat_ICD10 hos_nat_icd;
            SD_Nat_ICD10 sd_nat_icd;
            InuseICD10 inuse_icd;
            if (cysj <=DateTime20180901)
            {
                hos_nat_icd = _ICDMapCaching.HosNatICD10MapCache.FirstOrDefault(T => T.HosCode == code.Replace(" ", ""));
                if (hos_nat_icd != null)
                {
                    result[0] = hos_nat_icd.NatCode;
                    result[1] = hos_nat_icd.NatDisName;
                }
                else
                    Logger.Error("医院疾病代码没有匹配到:" + code + "病案号&" + bah);
                return result;
            }
            else if (cysj <=DateTime20190624)
            {
                sd_nat_icd = _ICDMapCaching.SDNatICD10MapCache.FirstOrDefault(T => T.SDCode == code.Replace(" ", ""));
                if (sd_nat_icd != null)
                {
                    result[0] = sd_nat_icd.NatCode;
                    result[1] = sd_nat_icd.NatDisName;
                }
                else
                    Logger.Error("医院疾病代码没有匹配到:" + code + "病案号&" + bah);
                return result;
            }
            else {
                inuse_icd= _ICDMapCaching.InuseICD10MapCache.FirstOrDefault(T => T.Code == code.Replace(" ", ""));//国家在用
                if (inuse_icd != null)
                {
                    result[0] = inuse_icd.Code;
                    result[1] = inuse_icd.Name;
                }
                else
                    Logger.Error("医院疾病代码没有匹配到:" + code + "病案号&" + bah);
                return result;
            }
          
          
        }

        public string[] MapNatOper(string code)
        {
            var result = new string[4];
            if (string.IsNullOrEmpty(code))
                return result;
            if (cysj == null)
                cysj = DateTime.Now.Date;
            Hos_Nat_ICD9CM3 hos_nat_icd9;
            SD_Nat_ICD9CM3 sd_nat_icd9;
            InuseICD9CM3 inuse_icd9;
            if (cysj <=DateTime20180901)
            {
                hos_nat_icd9 = _ICDMapCaching.HosNatOperMapCache.FirstOrDefault(T => T.HosCode == code.Replace(" ", ""));
                if (hos_nat_icd9 != null)
                {
                    result[0] = hos_nat_icd9.NatCode;
                    result[1] = hos_nat_icd9.NatOperName;
                    result[2] = hos_nat_icd9.OperKind;
                    result[3] = hos_nat_icd9.Option;
                }
                else
                    Logger.Error("医院手术代码没有匹配到:" + code + "病案号&" + bah);
                return result;
            }
            else if (cysj <=DateTime20190624)
            {
                sd_nat_icd9 = _ICDMapCaching.SDNatOperMapCache.FirstOrDefault(T => T.HosCode == code.Replace(" ", ""));
                if (sd_nat_icd9 != null)
                {
                    result[0] = sd_nat_icd9.NatCode;
                    result[1] = sd_nat_icd9.NatOperName;
                    result[2] = sd_nat_icd9.OperKind;
                    result[3] = sd_nat_icd9.Option;
                }
                else
                    Logger.Error("医院手术代码没有匹配到:" + code + "病案号&" + bah);
                return result;
            }
            else
            {
                inuse_icd9 = _ICDMapCaching.InuseICD9CM3MapCache.FirstOrDefault(T => T.OperCode == code.Replace(" ", ""));
                if (inuse_icd9 != null)
                {
                    result[0] = inuse_icd9.OperCode;
                    result[1] = inuse_icd9.OperName;
                    result[2] = inuse_icd9.OperKind;
                    result[3] = inuse_icd9.Option;
                }
                else
                    Logger.Error("医院手术代码没有匹配到:" + code + "病案号&" + bah);
                return result;
            }          

        }
        
        /// <summary>
        /// 根据切口名称生成省要求格式字段
        /// </summary>
        /// <param name="qkmc">大夫填写</param>
        /// <returns></returns>
        private string[] GetQKDJYHLB(string qkmc) {
            if (qkmc == "")
                return new string[] { "", "" };
            else if(qkmc == "1")
                 return new string[] { "1", "" };
            else                
                 return new string[] { qkmc.Split('/')[0],qkmc.Split('/')[1] };

        }
    }
  
}
