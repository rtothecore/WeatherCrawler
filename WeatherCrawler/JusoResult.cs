using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class JusoContents
    {
        public string defBdNmList;
        public string engAddr;
        public string m;
        public string emdNm;
        public string zipNo;
        public string roadAddrPart2;
        public string emdNo;
        public string sggNm;
        public string jibunAddr;
        public string siNm;
        public string roadAddrPart1;
        public string bdNm;
        public string admCd;
        public string udrtYn;
        public string lnbrMnnm;
        public string roadAddr;
        public string lnbrSlno;
        public string buldMnnm;
        public string bdKdcd;
        public string liNm;
        public string rnMgtSn;
        public string mtYn;
        public string bdMgtSn;
        public string buldSlno;
    }

    public class Common
    {
        public string errorMessage;
        public string countPerPage;
        public string totalCount;
        public string errorCode;
        public string currentPage;
    }

    public class JusoResults
    {
        public Common common;
        public List<JusoContents> juso;
    }

    public class JusoResult
    {
        public JusoResults results;
    }
}
