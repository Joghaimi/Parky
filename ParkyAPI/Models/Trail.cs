using System.ComponentModel.DataAnnotations;

namespace ParkyAPI.Models
{
    public class Trail
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        public enum DefacultyType { Easy, Modrate, Expert }
        public DefacultyType Defaculty { get; set; }
        public NationalPark nationalPark { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
