using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace InViBuS.API.Models
{
    [Table("Projects", Schema = "ProjectData")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProject { get; set; }
        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public String ProjectName { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string LastUploadedDate { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int IdProjectManager { get; set; }

        [ForeignKey("Users")]
        [Required]
        public string ProjectManager { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; }

        //[JsonIgnore]
        //public ICollection<AnalysisMetadata> AnalysisMetadata { get; set; }

        //[JsonIgnore]
        //public ICollection<AnalysisData> AnalysisData { get; set; }

        [JsonIgnore]
        public ICollection<SourceData> SourceDatas { get; set; } 
    }
}