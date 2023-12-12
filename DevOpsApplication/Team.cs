using System.ComponentModel.DataAnnotations;

namespace DevOpsApplication
{
    public class Team
    {
        [Key]
        public int Id { get; set; } 
        public string name { get; set; }

        public ICollection<Member> Members { get; set; }

    }
}