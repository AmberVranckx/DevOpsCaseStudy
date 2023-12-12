using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOpsApplication
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Lastname { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; } 
        public string city { get; set; }    
        public string email { get; set; }
        public string image { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
    }
}