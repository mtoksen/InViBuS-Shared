using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace InViBuS.API.Models
{
    [Table("AnalysisData", Schema = "ProjectData")]
    public class AnalysisData
    {
        [Required]
        [Key, ForeignKey("AnalysisMetadata")]
        public int IdAnalysisData { get; set; }

        //[Required]
        //[ForeignKey("Project")]
        //public int IdProject { get; set; }

        [Required]
        [DefaultValue("N/A"), MaxLength()]
        public string AnalysisDataJson { get; set; }

        [JsonIgnore]
        public AnalysisMetadata AnalysisMetadata { get; set; }

        //[JsonIgnore]
        //public Project Project { get; set; }
    }
}