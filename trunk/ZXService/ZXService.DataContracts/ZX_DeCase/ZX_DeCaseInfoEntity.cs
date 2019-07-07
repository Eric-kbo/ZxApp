using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZXService.DataContracts.ZX_DeCaseImage;

namespace ZXService.DataContracts.ZX_DeCase
{
    [DataContract]
    public class ZX_DeCaseInfoEntity
    { 
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string DesignerID { get; set; }

        [DataMember]
        public string CaseTitle { get; set; }

        [DataMember]
        public string CaseDesc { get; set; }

        [DataMember]
        public string ImageFile { get; set; }

        [DataMember]
        public string UpdateTime { get; set; }

        [DataMember]
        public int SortNo { get; set; }
        
        //[DataMember]
        //public List<ZX_DeCaseImageEntity> ImgList{get;set;}
    }
}
