using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDExportMetadataAndFile.View
{
    public class DBObj
    {
        public string connectionStr { get; set; }       
    }
    public class DBCommandObj
    {
        public string SQLQuery { get; set; }
        public string SQLQueryFiles { get; set; }
        public string SQLQueryPic { get; set; }
        public string SQLUpdateLog { get; set; }
    }
    public class GlobalObj
    {
        public string NasIP { get; set; }
        public int NasPort { get; set; }
        public string NasUsername { get; set; }
        public string NasPwd { get; set; }
        public string NasPath { get; set; }
        public string SaveFolder { get; set; }
        public string Symbol { get; set; }
        public bool exMediaLowres { get; set; }
        public bool exMediaHighres { get; set; }
        public bool exThumbPic { get; set; }
        public bool exFullPic { get; set; }
        public bool exExcel { get; set; }
        public bool exXml { get; set; }        
        public bool isHead { get; set; }
        public bool useMaBang { get; set; }
        public bool useTenCT { get; set; }
        public bool useTenCTAdd { get; set; }
        public bool useCreateDate { get; set; }
        public bool useBroadcastDate { get; set; }
        public bool useSeason { get; set; }
        public bool useEpisode { get; set; }
    }
    public class FileObj
    {
        public long ID_CLIP { get; set; }
        public string MA_BANG { get; set; }
        public int? EPISODE_NUMBER { get; set; }
        public string ACTOR { get; set; }
        public string DIRECTOR { get; set; }
        public Nullable<int> NAS_ID { get; set; }
        public string FILE_NAME { get; set; }
        public string HD_CLIP { get; set; }
        public string THUMB_FILE_NAME { get; set; }
        public string TEN_CHUONG_TRINH { get; set; }
        public DateTime DATE_TO_BROADCAST { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CommingNextNow { get; set; }
        public string Season { get; set; }
        public string NOI_DUNG { get; set; }
        public string VIDEO_FORMAT { get; set; }
        public string ASPECT_RATIO { get; set; }
        public long FILE_SIZE { get; set; } 
        public string TC_OUT { get; set; }
        public string TYPE { get; set; }
        public string NAS_IP { get; set; }       
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public int? PORT { get; set; }        
        public string DATA1_DIRECTORY { get; set; }        
        public string DATA3_DIRECTORY { get; set; }        
        public string UNC_BASE_PATH_DATA1 { get; set; }        
        public string UNC_BASE_PATH_DATA3 { get; set; }
        public DateTime START_RIGHTS { get; set; }
        public DateTime END_RIGHTS { get; set; }
        public string SECTOR_NAME { get; set; }

    }
    public class PosterObj
    {
        public long ID_CLIP { get; set; }
        public string FILE_NAME { get; set; }
        public Nullable<int> FILE_TYPE { get; set; }        
        public Nullable<int> NAS_ID { get; set; }
        public string NAS_IP { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public int? PORT { get; set; }
        public string DATA1_DIRECTORY { get; set; }
        public string DATA3_DIRECTORY { get; set; }
        public string UNC_BASE_PATH_DATA1 { get; set; }
        public string UNC_BASE_PATH_DATA3 { get; set; }
    }
}
