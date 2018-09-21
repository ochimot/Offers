// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.Public
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections.Generic;

namespace AutoLeadGUI
{
  public class Public
  {
    public void LoadinfoIp()
    {
      List<infoIphone> infoIphoneList = new List<infoIphone>()
      {
        new infoIphone()
        {
          Generation = "iPhone 5",
          Identifier = "iPhone5,1",
          ANumber = "A1428",
          Bootrom = "Bootrom 1145.3",
          Storage = "16G",
          Weight = "112 g.",
          Products = "Apple A6",
          Model = "MD293|MD634|MD638|ME486|MD295|MD636|ME488|MD295|MD642|MD644|ME490|MD294|MD635|ME487|MD296|MD637|ME489|MD643|MD645|ME491"
        },
        new infoIphone()
        {
          Generation = "iPhone 5",
          Identifier = "iPhone5,2",
          ANumber = "A1429|A1442",
          Bootrom = "Bootrom 1145.3",
          Storage = "16G",
          Weight = "112 g.",
          Products = "Apple A6",
          Model = "MD297|MD654|MD656|ME039|MD299|MD658|MD660|ME041|MD662|MD664|MD667|ME043|MD298|MD655|MD657|ME040|MD300|MD659|MD661|ME042|MD663|MD665|MD668|ME044"
        },
        new infoIphone()
        {
          Generation = "iPhone 5c",
          Identifier = "iPhone5,3",
          ANumber = "A1456|A1532",
          Bootrom = "Bootrom 1145.3",
          Storage = "16G",
          Weight = "112 g.",
          Products = "Apple A6",
          Model = "ME507|MF136|ME508|MF137|ME509|MF138|MF133|ME505|MF134|MGF12|ME506|MF135"
        },
        new infoIphone()
        {
          Generation = "iPhone 5c",
          Identifier = "iPhone5,4",
          ANumber = "A1507|A1516|A1526|A1529",
          Bootrom = "Bootrom 1145.3",
          Storage = "16G",
          Weight = "112 g.",
          Products = "Apple A6",
          Model = "MG0T2|MG1U2|MG902|ME555|MF156|MG0V2|MG1W2|MG912|ME556|MF157|MG0V2|MG1W2|MG922|ME557|MF158|MG0Q2|MG1Q2|MG8X2|ME553|MF154|MG0R2|MG1R2|MG8Y2|ME554|MF155"
        },
        new infoIphone()
        {
          Generation = "iPhone 5s",
          Identifier = "iPhone6,1",
          ANumber = "A1453|A1533",
          Bootrom = "Bootrom 1704.10",
          Storage = "16G",
          Weight = "112 g.",
          Products = "Apple A7|Apple M7",
          Model = "ME298|ME307|ME325|ME343|ME301|ME310|ME328|ME346|ME304|ME313|ME331|ME349|ME297|ME306|ME324|ME333|ME342|ME300|ME309|ME327|ME345|ME303|ME312|ME436|ME348|ME296|ME305|ME323|ME341|ME299|ME308|ME326|ME344|ME302|ME311|ME329|ME347"
        },
        new infoIphone()
        {
          Generation = "iPhone 5s",
          Identifier = "iPhone6,2",
          ANumber = "A1457|A1518|A1528|A1530",
          Bootrom = "Bootrom 1704.10",
          Storage = "16G",
          Weight = "112 g.",
          Products = "Apple A7|Apple M7",
          Model = "ME434|MF354|MF398|ME437|ME440|ME433|ME436|ME439|ME432|ME435|ME438"
        },
        new infoIphone()
        {
          Generation = "iPhone 6",
          Identifier = "iPhone7,2",
          ANumber = "A1549|A1586|A1589",
          Bootrom = "Bootrom 1992.0.0.1.19",
          Storage = "16G",
          Weight = "129 g.",
          Products = "Apple A8|Apple M8",
          Model = "MG3D2|MG492|MG4Q2|MG3L2|MG4J2|MG3G2|MG4E2|MG3C2|MG4P2|MG482|MG5X2|MG552|MG6A2|MG3K2|MG4H2|MG4X2|MG5C2|MG6H2|MG642|MG3F2|MG4C2|MG3A2|MG4N2|MG472|MG5W2|MG542|MG692|MG3H2|MG4F2|MG4W2|MG5A2|MG6G2|MG632|MG3E2|MG4A2"
        },
        new infoIphone()
        {
          Generation = "iPhone 6 Plus",
          Identifier = "iPhone7,1",
          ANumber = "A1522|A1524|A1593",
          Bootrom = "Bootrom 1992.0.0.1.19",
          Storage = "16G",
          Weight = "172 g.",
          Products = "Apple A8|Apple M8",
          Model = "MGAA2|MGAN2|MGC12|MGAK2|MGAW2|MGC72|MGAF2|MGAR2|MGC42|MGA92|MGC92|MGAM2|MGC02|MGAJ2|MGAV2|MGC62|MGAE2|MGAQ2|MGC32|MGA82|MGAL2|MGAX2|MGAH2|MGAU2|MGC52|MGAC2|MGAP2|MGC22"
        },
        new infoIphone()
        {
          Generation = "iPhone 6s",
          Identifier = "iPhone8,1",
          ANumber = "A1633|A1688|A1691|A1700",
          Bootrom = "Bootrom 1992.0.0.1.19",
          Storage = "16G",
          Weight = "143 g.",
          Products = "Apple A9|Apple M9",
          Model = "MKQL2|MKQ72|MKR12|MKRE2|MKRW2|MKT92|ML7E2|MN0P2|MN172|MN1K2|MN1U2|MN1Y2|MKQC2|MKQQ2|MKR52|MKRJ2|MKT12|MKTE2|ML7J2|MKQG2|MKQV2|MKR92|MKRP2|MKT52|MKTJ2|ML7N2|MKQM2|MKQ82|MKRF2|MKRX2|MKR22|MKTA2|ML7F2|MN0V2|MN192|MN1L2|MN1V2|MN202|MKQD2|MKQR2|MKR62|MKRK2|MKT22|MKTF2|ML7K2|MKQH2|MKQW2|MKRA2|MKRQ2|MKT62|MKTK2|ML7P2|MKQ62|MKQK2|MKQY2|MKRD2|MKRT2|MKT82|ML7D2|NKQJ2|MN0N2|MN162|MN1G2|MN1Q2|MN1X2|MKQA2|MKQP2|MKR42|MKRH2|MKT02|MKTD2|ML7H2|MKQF2|MKQU2|MKR82|MKRM2|MKT42|MKTH2|ML7M2|MKQ52|MKQJ2|MKQX2|MKRC2|MKRR2|MKT72|ML7C2|MN0M2|MN132|MN1E2|MN1M2|MN1W2|MKQN2|MKQ92|MKR32|MKRG2|MKRY2|MKTC2|ML7G2|MKQE2|MKQT2|MKR72|MKRL2|MKT32|MKTG2|ML7L2"
        },
        new infoIphone()
        {
          Generation = "iPhone 6s Plus",
          Identifier = "iPhone8,2",
          ANumber = "A1634|A1687|A1690|A1699",
          Bootrom = "Bootrom 2234.0.0.3.3|Bootrom 2234.0.0.2.22",
          Storage = "16G",
          Weight = "192 g.",
          Products = "Apple A9|Apple M9",
          Model = "MKTN2|MKU32|MKUN2|MKV62|MKVQ2|MKW72|ML6D2|MKTT2|MKU82|MKUV2|MKVD2|MKVX2|MKWD2|ML6H2|MKTX2|MKUF2|MKV12|MKVH2|MKW22|MKWH2|ML6M2|MKU52|ML6E2|MKU92|ML6J2|MKUG2|MKV22|ML6N2|MKTM2|MKU22|MKUJ2|MKV52|MKVP2|MKW62|ML6C2|MKTR2|MKU72|MKUU2|MKV92|MKVW2|MKWC2|ML6G2|MKTW2|MKUE2|MKUY2|MKVG2|MKW12|MKWG2|ML6L2|MKU12|ML6A2|MKU62|ML6F2|MKUD2|ML6K2"
        },
        new infoIphone()
        {
          Generation = "iPhone 7",
          Identifier = "iPhone9,1",
          ANumber = "A1660|A1779|A1780",
          Bootrom = "Bootrom 2696.0.0.1.33",
          Storage = "16G",
          Weight = "138 g.",
          Products = "Apple A10|Apple M10",
          Model = "MN8G2|MNAC2|MNAY2|MNCE2|MN8L2|MNAJ2|MNC32|MNCK2|MN8R2|MNAQ2|MNC82|MNCQ2|MN8J2|MNAE2|MNC12|MNCG2|MN8N2|MNAL2|MNC52|MNCM2|MN8U2|MNAV2|MNCA2|MNCT2|MN8Q2|MNAP2|MNC72|MNCP2|MN8W2|MNAX2|MNCD2|MNCV2|MN8K2|MNAF2|MNC22|MNCJ2|MN8P2|MNAM2|MNC62|MNH12|MNCN2|MN8V2|MNAW2|MNCC2|MNCU2|MN8H2|MNAD2|MNC02|MNCF2|MN8M2|MNAK2|MNC42|MNCL2|MN8T2|MNAU2|MNC92|MNCR2"
        },
        new infoIphone()
        {
          Generation = "iPhone 7",
          Identifier = "iPhone9,3",
          ANumber = "A1778",
          Bootrom = "Bootrom 2696.0.0.1.33",
          Storage = "16G",
          Weight = "188 g.",
          Products = "Apple A10|Apple M10",
          Model = "MN9D2|MN9U2|MN9H2|MN9Y2|MN9N2|MNA62|MN9F2|MN9W2|MN9K2|MNA32|MN9Q2|MNA82|MN9M2|MNA52|MN9T2|MNAA2|MN9G2|MN9X2|MN9L2|MNA42|MN9R2|MNA92|MN9E2|MN9V2|MN9J2|MNA02|MN9P2|MNA72"
        },
        new infoIphone()
        {
          Generation = "iPhone 7 Plus",
          Identifier = "iPhone9,2",
          ANumber = "A1661|A1785||A1786",
          Bootrom = "Bootrom 2696.0.0.1.33",
          Storage = "16G",
          Model = "MNQH2|MNR12|MNR52|MNR92|MN482|MN5T2|MN642|MN6F2|MN4E2|MN5Y2|MN692|MN6L2|MNQK2|MNR32|MNR72|MNRC2|MN4A2|MN5V2|MN662|MN6H2|MN4J2|MN612|MN6C2|MN6N2|MN4D2|MN5X2|MN682|MN6K2|MN4L2|MN632|MN6E2|MN6Q2|MNQL2|MNR42|MNR82|MNRD2|MN4C2|MN5W2|MN672|MN6J2|MN4K2|MN622|MN6D2|MN6P2|MNQJ2|MNR22|MNR62|MNRA2|MN492|MN5U2|MN652|MN6G2|MN4F2|MN602|MN6A2|MN6M2"
        },
        new infoIphone()
        {
          Generation = "iPhone 7 Plus",
          Identifier = "iPhone9,4",
          ANumber = "A1784",
          Bootrom = "Bootrom 2696.0.0.1.33",
          Storage = "16G",
          Model = "MNQR2|MNQW2|MN522|MN5G2|MN592|MN5M2|MNQU2|MNQY2|MN552|MN5J2|MN5D2|MN5P2|MN572|MN5L2|MN5F2|MN5R2|MNQV2|MNR02|MN562|MN5K2|MN5E2|MN5Q2|MNQT2|MNQX2|MN532|MN5H2|MN5C2|MN5N2"
        }
      };
    }

    internal static void SetListBK(int lanchay, int phantrambk)
    {
      frmMain.ListVitriBk.Clear();
      if (phantrambk == 100)
      {
        for (int index = 1; index < lanchay + 1; ++index)
          frmMain.ListVitriBk.Add(new BkList()
          {
            vitri = index
          });
      }
      else
      {
        int int32 = Convert.ToInt32(lanchay * phantrambk / 100);
        if (int32 > 0)
        {
          for (int index = 1; index < int32 + 1; ++index)
          {
            Random random = new Random();
            int vitri;
            do
            {
              vitri = random.Next(1, lanchay + 1);
            }
            while (frmMain.ListVitriBk.Find((Predicate<BkList>) (c => c.vitri == vitri)) != null);
            frmMain.ListVitriBk.Add(new BkList()
            {
              vitri = vitri
            });
          }
        }
      }
    }
  }
}
