using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DesigerReComd
{
   [DataContract]
  public  class ZX_DesInfo
    {
      [DataMember]
      public string ID { get; set; }

      [DataMember]
      public string Eexpert { get; set; }

      [DataMember]
      public int Price { get; set; }

      [DataMember]
      public string Idea { get; set; }

      [DataMember]
      public string DeName { get; set; }

      [DataMember]
      public string DesignerTitle { get; set; }

       [DataMember]
      public string AreaName { get; set; }
    }
}
