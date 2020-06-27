using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace InViBuS.API.Models
{
    [Table("SourceData", Schema = "ProjectData")]
    public class SourceData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSourceData { get; set; }

        [ForeignKey("Project")]
        [Required]
        public int IdProject { get; set; }

        [Required]
        public byte[] SourceDataBLOB { get; set; }

        [JsonIgnore]
        public Project Project { get; set; } 
    }
}