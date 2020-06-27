using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace InViBuS.API.Models
{
    [Table("Filters", Schema = "ProjectData")]
    public class Filter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFilter { get; set; }
        [ForeignKey("AnalysisMetadata")]
        public int IdAnalysisMetadata { get; set; }
        public int NumFilter { get; set; }
        public double MinVal { get; set; }
        public double MaxVal { get; set; }

        [JsonIgnore]
        public AnalysisMetadata AnalysisMetadata { get; set; }
    }
}