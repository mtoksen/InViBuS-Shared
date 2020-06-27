using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace InViBuS.API.Models
{
    [Table("AnalysisMetadata", Schema = "ProjectData")]
    public class AnalysisMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAnalysisMetadata { get; set; }
        [ForeignKey("Project")]
        public int IdProject { get; set; }
        [Required]
        public int Iteration { get; set; }
        [Required]
        public int NumSim { get; set; }
        [Required]
        public int TimeSim { get; set; }
        [Required]
        public int NumIn { get; set; }
        [Required]
        public int NumOut { get; set; }
        [Required]
        public String Subject { get; set; }
        [Required]
        public string UploadDate { get; set; }
        public String DescriptionLong { get; set; }
        [Required]
        public String Conclusion { get; set; }
        [Required]
        public String SourceFile { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }

        [Required]
        public AnalysisData AnalysisData { get; set; }

        public ICollection<Filter> Filters { get; set; } 
    }
}