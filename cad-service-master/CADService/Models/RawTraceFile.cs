//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

namespace CADService.Models
{
    

    public  class RawTraceFile
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Nullable<byte> FileTypeID { get; set; }
        public string ResultTable { get; set; }
        public int JobID { get; set; }
        public Nullable<short> ProductVersionID { get; set; }
        public Nullable<short> ProductComponentID { get; set; }

    }
}
