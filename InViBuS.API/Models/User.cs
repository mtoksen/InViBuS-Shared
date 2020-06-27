using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Optimization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace InViBuS.API.Models
{
    [Table("Users", Schema = "UserData")]
    public class User : IUser
    {

        public virtual String Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Display(Name = "Company")]
        public String Company { get; set; }

        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "UserName")]
        [MaxLength(16, ErrorMessage = "UserName must be 16 characters or less"), MinLength(6, ErrorMessage = "UserName must be 6 characters or more")]
        public String UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(16, ErrorMessage = "Password must be 16 characters or less"), MinLength(6, ErrorMessage = "Pasword must be 6 characters or more")]
        public String Password { get; set; }

        [JsonIgnore]
        public ICollection<Project> Projects { get; set; }
    }
}